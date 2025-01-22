using AutoMapper;
using Pertamina.IRIS.Models.Entities;
using Pertamina.IRIS.Models.ViewModels;
using Pertamina.IRIS.Repositories.Interfaces;
using Pertamina.IRIS.Utility.Wording.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Pertamina.IRIS.Repositories.Implement
{
    public class DownloadPresentationRepository : IDownloadPresentationRepository
    {
        protected readonly DB_PINMContext _context;
        protected readonly IMapper _mapper;

        public DownloadPresentationRepository(DB_PINMContext context, IMapper mapper)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public DownloadPresentationChartAgreementViewModel GetDataChartAgreement()
        {
            var tenYearsAgo = DateTime.Now.AddYears(-10);

            var aggregatedCounts =
                from a in _context.TbltAgreements
                join b in _context.TblmJenisPerjanjians on a.JenisPerjanjianId equals b.Id into jb
                from b in jb.DefaultIfEmpty()
                join sb in _context.TblmStatusBerlakus on a.StatusBerlakuId equals sb.Id into jsb
                from sb in jsb.DefaultIfEmpty()
                where a.TanggalTtd >= tenYearsAgo
                group new { b, sb } by b.Shortname into g
                select new
                {
                    Shortname = g.Key,
                    CountIsSecret = g.Count(x => x.b.IsSecret == true && (x.sb.RelationSequence == 1 || x.sb.RelationSequence == 2)),
                    CountIsFirst = g.Count(x => x.b.IsFirst == true && (x.sb.RelationSequence == 1 || x.sb.RelationSequence == 2)),
                    CountIsNext = g.Count(x => x.b.IsNext == true && (x.sb.RelationSequence == 1 || x.sb.RelationSequence == 2)),
                    CountIsValue = g.Count(x => x.b.IsValue == true && (x.sb.RelationSequence == 1 || x.sb.RelationSequence == 2)),
                    CountIsOther = g.Count(x => x.b.IsOther == true && (x.sb.RelationSequence == 1 || x.sb.RelationSequence == 2))
                };

            var result = new DownloadPresentationChartAgreementViewModel
            {
                ShortNameSecret = string.Join("/", aggregatedCounts.Where(x => x.CountIsSecret > 0).Select(x => x.Shortname)),
                ShortNameFirst = string.Join("/", aggregatedCounts.Where(x => x.CountIsFirst > 0).Select(x => x.Shortname)),
                ShortNameIsNext = string.Join("/", aggregatedCounts.Where(x => x.CountIsNext > 0).Select(x => x.Shortname)),
                ShortNameIsValue = string.Join("/", aggregatedCounts.Where(x => x.CountIsValue > 0).Select(x => x.Shortname)),
                ShortNameIsOther = string.Join("/", aggregatedCounts.Where(x => x.CountIsOther > 0).Select(x => x.Shortname))
            };


            var resultCount =
                    (from a in _context.TbltAgreements
                     join jp in _context.TblmJenisPerjanjians on a.JenisPerjanjianId equals jp.Id
                     join sb in _context.TblmStatusBerlakus on a.StatusBerlakuId equals sb.Id
                     where a.TanggalTtd >= tenYearsAgo && sb.RelationSequence == 1 || sb.RelationSequence == 2

                     select new
                     {
                         IsSecret = jp.IsSecret == true,
                         IsFirst = jp.IsFirst == true,
                         IsNext = jp.IsNext == true,
                         IsValue = jp.IsValue == true,
                         IsOther = jp.IsOther == true,
                         IsLNG = a.Scope.Contains("jual beli lng") && jp.IsValue == true,
                         AnyConditionMet = jp.IsSecret == true || jp.IsFirst == true || jp.IsNext == true || jp.IsValue == true || jp.IsOther == true
                     }).ToList();


            result.CountIsSecret = resultCount.Count(x => x.IsSecret);
            result.CountIsFirst = resultCount.Count(x => x.IsFirst);
            result.CountIsNext = resultCount.Count(x => x.IsNext);
            result.CountIsValue = resultCount.Count(x => x.IsValue);
            result.CountIsOther = resultCount.Count(x => x.IsOther);
            result.CountStatusBerlaku = resultCount.Count(x => x.AnyConditionMet);
            result.CountLNG = resultCount.Count(x => x.IsLNG);

            return result;
        }

        public DownloadPresentationChartStrategicViewModel GetDataChartStrategic()
        {
            var result =
                 (from a in _context.TbltAgreements
                  join jp in _context.TblmJenisPerjanjians on a.JenisPerjanjianId equals jp.Id into jpGroup
                  from jp in jpGroup.DefaultIfEmpty()
                  join b in _context.TbltAgreementStreamBusinesses on a.Id equals b.AgreementId into bGroup
                  from b in bGroup.DefaultIfEmpty()
                  join c in _context.TblmStreamBusinesses on b.StreamBusinessId equals c.Id into cGroup
                  from c in cGroup.DefaultIfEmpty()
                  select new
                  {
                      IsGtg = a.IsGtg == true,
                      IsStrategic = jp != null && jp.IsStrategic == true,
                      IsBDBusiness = (a.PotentialRevenuePerYear + a.Capex + a.Valuation) > 500000000,
                      IsBDGreen = c != null && (c.Id == Guid.Parse("487AF41D-B30E-42D7-95B5-B5D38D6E6174") || c.Id == Guid.Parse("690B5B90-6AF5-4C7B-888F-182CB250EA0E"))
                  }).ToList();

            var aggregatedResult = new DownloadPresentationChartStrategicViewModel
            {
                CountGtg = result.Count(x => x.IsGtg),
                CountStrategic = result.Count(x => x.IsStrategic),
                CountBDBusiness = result.Count(x => x.IsBDBusiness),
                CountBDGreen = result.Count(x => x.IsBDGreen)
            };


            return aggregatedResult;
        }

        public List<DownloadPresentationChartAgreementDescriptionViewModel> GetDataChartAgreementDescription()
        {
            var result =
                (from a in _context.TbltAgreements
                 join ap in _context.TbltAgreementPartners on a.Id equals ap.AgreementId into apGroup
                 from ap in apGroup.DefaultIfEmpty()
                 join aep in _context.TbltAgreementEntitasPertaminas on a.Id equals aep.AgreementId into aepGroup
                 from aep in aepGroup.DefaultIfEmpty()
                 join ep in _context.TblmEntitasPertaminas on aep.EntitasPertaminaId equals ep.Id into epGroup
                 from ep in epGroup.DefaultIfEmpty()
                 join jp in _context.TblmJenisPerjanjians on a.JenisPerjanjianId equals jp.Id into jpGroup
                 from jp in jpGroup.DefaultIfEmpty()
                 where jp.IsFirst == true || jp.IsNext == true
                 select new DownloadPresentationChartAgreementDescriptionViewModel
                 {
                     PartnerName = ap.PartnerName,
                     Code = ep.Code,
                     JudulPerjanjian = a.JudulPerjanjian
                 })
                 //.Take(2)
                 .ToList();

            return result;
        }

        public List<DownloadPresentationChartTemplate2ViewModel> GetDataChartCountryPartner()
        {
            var result =
                (from an in _context.TbltAgreementNegaraMitras
                 join n in _context.TblmNegaraMitras on an.NegaraMitraId equals n.Id
                 group an by n.NamaNegara into grouped  
                 select new DownloadPresentationChartTemplate2ViewModel
                 {
                     name = grouped.Key,
                     value = grouped.Count()  
                 })
                .OrderByDescending(x => x.value)  
                .ToList();  

            return result;
        }

        public List<DownloadPresentationChartTemplate2ViewModel> GetDataChartBusinessStream()
        {
            var result =
                (from sb in _context.TbltAgreementStreamBusinesses
                 join s in _context.TblmStreamBusinesses on sb.StreamBusinessId equals s.Id
                 group sb by s.Name into grouped
                 select new DownloadPresentationChartTemplate2ViewModel
                 {
                     name = grouped.Key,
                     value = grouped.Count()
                 })
                .OrderByDescending(x => x.value)
                .ToList();

            return result;
        }

        public List<DownloadPresentationChartTemplate2ViewModel> GetDataChartAgreementHolder()
        {
            var result =
                 (from ep in _context.TbltAgreementEntitasPertaminas
                  join e in _context.TblmEntitasPertaminas on ep.EntitasPertaminaId equals e.Id
                  join h in _context.TblmHshes on e.HshId equals h.Id
                  group ep by h.Name into grouped
                  select new DownloadPresentationChartTemplate2ViewModel
                 {
                     name = grouped.Key,
                     value = grouped.Count()
                 })
                .OrderByDescending(x => x.value)
                .ToList();

            return result;
        }

        public List<DownloadPresentationChartTemplate2ViewModel> GetDataChartDiscussion()
        {
            var result =
                 (from a in _context.TbltAgreements
                  join d in _context.TblmDiscussionStatuses on a.DiscussionStatusId equals d.Id
                  group a by d.Name into grouped
                  select new DownloadPresentationChartTemplate2ViewModel
                  {
                      name = grouped.Key,
                      value = grouped.Count()
                  })
                .OrderByDescending(x => x.value)
                .ToList();

            return result;
        }



    }
}
