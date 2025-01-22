using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Pertamina.IRIS.Models.Base;
using Pertamina.IRIS.Models.ViewModels;
using Pertamina.IRIS.Services.Interfaces;
using Syncfusion.Presentation;
using Syncfusion.OfficeChart;
using Syncfusion.Drawing;
using static System.Net.Mime.MediaTypeNames;
using System.Drawing;
using System.IO;
using System;
using System.Threading.Tasks;
using Pertamina.IRIS.Repositories.Interfaces;
namespace Pertamina.IRIS.App.Controllers.API
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class ApiDownloadPresentationController : BaseController
    {
        private readonly IDownloadPresentationRepository _repository;
        private readonly IPowerPointService _service;
        private readonly IWebHostEnvironment _env;

        public ApiDownloadPresentationController(IIdamanService idamanService, IPowerPointService service, IWebHostEnvironment env, IDownloadPresentationRepository repository) : base(idamanService)
        {
            _service = service ?? throw new ArgumentNullException(nameof(service));
            _env = env ?? throw new ArgumentNullException(nameof(env));
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        }

        [Route("ExportPresentation")]
        [HttpPost]
        public IActionResult ExportPresentation([FromBody] DownloadPresentationViewModel model)
        {
            try
            {
                if (model.template == "1")
                {
                    Stream stream = CreateChartTempate1(model.freeTextTemplate1);
                    return File(stream, "application/presentation", "Template1.pptx");
                }
                else
                {
                    Stream stream = CreateChartTempate2(model);
                    return File(stream, "application/presentation", "Template2.pptx");
                }

            }
            catch (Exception err)
            {
                Console.WriteLine($"CreatePPTtempate Error: {err.Message}");
                return StatusCode(500, new { ErrorMessage = $"An error occurred: {err.Message}" });
            }
        }


        private Stream CreateChartTempate1(string freeText)
        {
            var dataChartAgreement = _repository.GetDataChartAgreement();
            int totalStatusBerlaku = dataChartAgreement.CountStatusBerlaku;

            // Create a new presentation
            using IPresentation presentation = Presentation.Create();

            // Add a slide to the presentation
            ISlide slide = presentation.Slides.Add(SlideLayoutType.Blank);

            // Define table position and size
            double tableX = 30;   // X position
            double tableY = 10;   // Y position
            double tableWidth = 860; // Total width of the table
            double tableHeight = 80; // Total height of the table

            // Add a table with 1 row and 2 columns
            ITable headerTable = slide.Tables.AddTable(1, 2, tableX, tableY, tableWidth, tableHeight);

            // Set the header table background to transparent
            headerTable.Rows[0].Cells[0].Fill.FillType = FillType.None;
            headerTable.Rows[0].Cells[1].Fill.FillType = FillType.None;

            // Adjust column widths manually (80% for Column 1, 20% for Column 2)
            headerTable.Columns[0].Width = tableWidth * 0.8; // 80% of the total width
            headerTable.Columns[1].Width = tableWidth * 0.2; // 20% of the total width 

            // Add text to the columns 
            headerTable.Rows[0].Cells[0].TextBody.Text = "Dalam 10 tahun terakhir, Pertamina Group memiliki " +
                totalStatusBerlaku + " Perjanjian dengan pihak asing, dimana 30 dikategorikan Perjanjian Strategis Buss. Development";
            headerTable.Rows[0].Cells[0].TextBody.Paragraphs[0].Font.FontSize = 18;
            headerTable.Rows[0].Cells[0].TextBody.Paragraphs[0].Font.Color = ColorObject.FromArgb(0, 0, 0);

            // Add an image into Row 1 Column 2
            string imagePath = Path.Combine(_env.WebRootPath, "img/logo-pertamina.png");
            _service.AddImageToCell(slide, headerTable, 0, 1, imagePath);

            // Set body: Add a table with 1 row and 2 columns for the body
            ITable bodyTable = slide.Tables.AddTable(1, 2, tableX, 70, 880, 460); // Adjusted width and position

            // Set the header table background to transparent 
            bodyTable.Rows[0].Cells[0].Fill.FillType = FillType.Solid; // Set fill type to solid
            bodyTable.Rows[0].Cells[0].Fill.SolidFill.Color = ColorObject.FromArgb(249, 249, 249); //Gray
            bodyTable.Rows[0].Cells[1].Fill.FillType = FillType.None;

            // Add text to Row 1 Column 1 (above chart)
            string textBeforeChart = "Type dan jumlah Perjanjian yang tercatat sejak tahun 2015 sampai dengan " +
                DateTime.Now.ToString("MMMM yyyy");
            _service.AddTextBeforeChart(slide, bodyTable, 0, 0, textBeforeChart);

            // Add a chart to Row 1 Column 1   
            _service.AddChartAgreement(slide, bodyTable, 40, 100, 420, 200, dataChartAgreement, "", "pink");

            // Add chart and bulleted list to the slide 
            _service.AddChartAgreementDescription(slide, dataChartAgreement, 40, 290, 420, 150, freeText);

            // Add an image footer  
            string imagePathFooter = Path.Combine(_env.WebRootPath, "img/footer_ppt.png");
            _service.AddImageFooter(slide, bodyTable, 0, 0, imagePathFooter);

            // Add text to Row 1 Column 2  
            _service.AddTextBeforeChart(slide, bodyTable, 0, 1, "Kategori Perjanjian Strategis:");

            var dataChartStrategic = _repository.GetDataChartStrategic();
            // Add a chart to Row 1 Column 2  
            _service.AddChartStrategic(slide, bodyTable, 0, 1, dataChartStrategic);

            string[] note = {
            "Agreement selain dari 4 kategori di atas dan dari non-core business Pertamina seperti airlines. healthcare dan lainnya yang tidak beririsan dengan 4 kategori di atas tidak termasuk dalam Perjanjian Strategis",
            "Pihak asing adalah perusahaan asing termasuk dengan kantor perwakilan di Indonesia."
            };

            // Add Noted List into Row 1 Column 2  
            _service.AddContentListAfterChart2(slide, bodyTable, 0, 1, note);

            // Save the presentation to a MemoryStream
            MemoryStream stream = new MemoryStream();
            presentation.Save(stream);
            stream.Position = 0; // Reset the stream position for reading
            return stream;
        }

        private Stream CreateChartTempate2(DownloadPresentationViewModel model)
        {
            // Create a new presentation
            using IPresentation presentation = Presentation.Create();
            // Add a slide to the presentation
            ISlide slide = presentation.Slides.Add(SlideLayoutType.Blank);

            // Define table position and size
            double tableX = 30;   // X position
            double tableY = 10;   // Y position
            double tableWidth = 910; // Total width of the table
            double tableHeight = 80; // Total height of the table

            // Add a table with 1 row and 2 columns
            ITable headerTable = slide.Tables.AddTable(1, 2, tableX, tableY, tableWidth, tableHeight);

            // Set the header table background to transparent
            headerTable.Rows[0].Cells[0].Fill.FillType = FillType.None;
            headerTable.Rows[0].Cells[1].Fill.FillType = FillType.None;

            // Adjust column widths manually (80% for Column 1, 20% for Column 2)
            headerTable.Columns[0].Width = tableWidth * 0.8; // 80% of the total width
            headerTable.Columns[1].Width = tableWidth * 0.2; // 20% of the total width 

            // Add text to the columns 
            headerTable.Rows[0].Cells[0].TextBody.Text = model.titleTemplate2;
            headerTable.Rows[0].Cells[0].TextBody.Paragraphs[0].Font.FontSize = 18;
            headerTable.Rows[0].Cells[0].TextBody.Paragraphs[0].Font.Color = ColorObject.FromArgb(0, 0, 0);


            // Add an image into Row 1 Column 2
            string imagePath = Path.Combine(_env.WebRootPath, "img/logo-pertamina.png");
            _service.AddImageToCell(slide, headerTable, 0, 1, imagePath);

            // Set body: Add a table with 1 row and 2 columns for the body
            ITable bodyTable = slide.Tables.AddTable(1, 2, tableX, 70, 910, 400); // Adjusted width and position
            // Adjust column widths manually (70% for Column 1, 30% for Column 2)
            bodyTable.Columns[0].Width = 910 * 0.65; // 70% of the total width
            bodyTable.Columns[1].Width = 910 * 0.35; // 30% of the total width 

            // Set the header table background to transparent 
            bodyTable.Rows[0].Cells[0].Fill.FillType = FillType.None;
            bodyTable.Rows[0].Cells[1].Fill.FillType = FillType.Solid;
            bodyTable.Rows[0].Cells[1].Fill.SolidFill.Color = ColorObject.FromArgb(249, 249, 249); //Gray

            // Add text to Row 1 Column 1 (above chart)
            string textBeforeChart = "Partners Country of Origin";
            _service.AddTextBeforeChart(slide, bodyTable, 0, 0, textBeforeChart, true);

            // Add a chart to Row 1 Column 1  
            var dataCountryPartner = _repository.GetDataChartCountryPartner();
            _service.AddChartCountryPartner(slide, bodyTable, 0, 70, 600, 200, dataCountryPartner);

            // Access the first cell of the first row (Column 0)
            ICell firstCell = bodyTable[0, 0];

            // Create an inner table with 1 row and 2 columns
            ITable innerTable = slide.Tables.AddTable(1, 2, 30, 300, 616, 70);

            // Adjust inner table column widths (50% each for this example)
            innerTable.Columns[0].Width = 616 * 0.5; // 50% of the parent column width
            innerTable.Columns[1].Width = 616 * 0.5; // 50% of the parent column width
            innerTable.Rows[0].Cells[0].Fill.FillType = FillType.None;
            innerTable.Rows[0].Cells[1].Fill.FillType = FillType.None;

            var dataStreamBusiness = _repository.GetDataChartBusinessStream();
            _service.AddChartWaterFall(slide, innerTable, 30, 300, 290, 200, dataStreamBusiness, "Business Stream", "blue");
            var dataHolder = _repository.GetDataChartAgreementHolder();
            _service.AddChartWaterFall(slide, innerTable, 320, 300, 290, 200, dataHolder, "Agreement Holder", "blue");

            // Add an image footer  
            string imagePathFooter = Path.Combine(_env.WebRootPath, "img/footer_ppt.png");
            _service.AddImageFooter(slide, bodyTable, 0, 0, imagePathFooter);

            // Add text to Row 1 Column 2  
            _service.AddTextBeforeChart(slide, bodyTable, 0, 1, "Status Kegiatan dari Perjanjian", true);
            // Add a chart to Row 1 Column 2  
            var dataDiscussion = _repository.GetDataChartDiscussion();
            _service.AddChartWaterFall(slide, bodyTable, 620, 90, 310, 150, dataDiscussion, "", "pink");
            // Add chart and bulleted list to the slide
            _service.AddChartStatusKegiatanDescription(slide, model.freeTextTemplate2, 620, 230, 310, 150);

            // Save the presentation to a MemoryStream
            MemoryStream stream = new MemoryStream();
            presentation.Save(stream);
            stream.Position = 0; // Reset the stream position for reading
            return stream;

        }


    }
}
