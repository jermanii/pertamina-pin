using OfficeOpenXml;
using Pertamina.IRIS.Models.Base;
using Pertamina.IRIS.Models.Entities;
using Pertamina.IRIS.Models.ViewModels;
using Pertamina.IRIS.Repositories.Interfaces;
using Pertamina.IRIS.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pertamina.IRIS.Services.Implement
{
    public class ExternalContactService : IExternalContactService
    {
        private readonly IExternalContactRepository _repository;

        public ExternalContactService(IExternalContactRepository repository)
        {
            _repository = repository;
        }
        public async Task<PaginationBaseModel<ExternalContactViewModel>> GetListPaging(RequestFormDtBaseModel request)
        {
            var resultData = await _repository.GetList(request);

            var result = new PaginationBaseModel<ExternalContactViewModel>
            {
                Data = resultData.Data,
                RecordsFiltered = resultData.PageInfo.TotalRecord,
                RecordsTotal = resultData.PageInfo.TotalRecord
            };

            return result;
        }

        public List<ExternalContactViewModel> Save(ExternalContactViewModel model, string userName)
        {
            List<ExternalContactViewModel> duplicatedData = new List<ExternalContactViewModel>();

            try
            {
                model.IsError = false;
                DateTime now = DateTime.Now;
                if (model.Persons != null)
                {

                    for (int i = 0; i < model.Persons.Count; i++)
                    {

                        string namaPerson = model.Persons[i].NamaPerson;
                        string jabatanPerson = model.Persons[i].JabatanPerson;
                        string emailPerson = model.Persons[i].EmailPerson;
                        string noTelpPerson = model.Persons[i].NoTelpPerson;

                        if (!string.IsNullOrEmpty(namaPerson) && !string.IsNullOrEmpty(jabatanPerson) && !string.IsNullOrEmpty(emailPerson) && !string.IsNullOrEmpty(noTelpPerson))
                        {

                            ExternalContactViewModel duplicatedModel = new ExternalContactViewModel();
                            duplicatedModel.Id = Guid.NewGuid();
                            duplicatedModel.CreateBy = userName;
                            duplicatedModel.CreateDate = now;

                            // Duplikasi properti yang sama
                            duplicatedModel.NamaCompany = model.NamaCompany;
                            duplicatedModel.KoneksiCompany = model.KoneksiCompany;
                            duplicatedModel.AlamatCompany = model.AlamatCompany;
                            duplicatedModel.EmailCompany = model.EmailCompany;
                            duplicatedModel.NoTelpCompany = model.NoTelpCompany;
                            duplicatedModel.Remark = model.Remark;

                            // Set properti yang berbeda sesuai dengan data dari form repeater
                            duplicatedModel.NamaPerson = namaPerson;
                            duplicatedModel.JabatanPerson = jabatanPerson;

                            duplicatedModel.EmailPerson = emailPerson;
                            duplicatedModel.NoTelpPerson = noTelpPerson;

                            // Simpan model yang diduplikasi
                            _repository.Save(duplicatedModel);
                            duplicatedData.Add(duplicatedModel);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                model.ErrorMessage = ex.Message;
                model.IsError = true;
                duplicatedData.Clear();
                duplicatedData.Add(model);
            }

            return duplicatedData;
        }
        public ExternalContactViewModel Delete(Guid guid, string userName)
        {
            ExternalContactViewModel model = new ExternalContactViewModel();
            model.Id = guid;
            try
            {
                if (model.IsError)
                {
                    return model;
                }
                else
                {
                    model.IsError = false;
                    _repository.Delete(guid, userName);

                }
            }
            catch (Exception ex)
            {
                model.ErrorMessage = ex.Message;
                model.IsError = true;
                return model;
            }

            return model;
        }

        public ExternalContactViewModel Update(ExternalContactViewModel model, string userName)
        {
            try
            {
                if (model.IsError)
                {
                    return model;
                }
                else
                {
                    model.IsError = false;
                    _repository.Update(model, userName);
                }
            }
            catch (Exception ex)
            {
                model.ErrorMessage = ex.Message;
                model.IsError = true;
                return model;
            }

            return model;
        }

        public ExternalContactViewModel GetExternalContactById(Guid guid)
        {

            ExternalContactViewModel model = new ExternalContactViewModel();

            try
            {
                if (guid != Guid.Empty)
                {
                    model = _repository.GetExternalContactById(guid);
                    model.IsError = true;
                }
            }
            catch (Exception ex)
            {
                model.ErrorMessage = ex.Message;
                model.IsError = true;
                return model;
            }
            return model;
        }

        public ExcelPackage ExportXLS(bool withData, string searchQuery)
        {
            var package = new ExcelPackage();
            package.Workbook.Properties.Title = "External Contact Download";
            package.Workbook.Properties.Author = "Pertamina";
            package.Workbook.Properties.Subject = "Data Eksport";
            package.Workbook.Properties.Keywords = "IRIS - Pertamina";

            var worksheet = package.Workbook.Worksheets.Add("External Contact");

            // First: add the header
            worksheet.Cells[1, 1].Value = "DATA EXTERNAL CONTACT";
            worksheet.Cells[2, 1].Value = "NAMA PERUSAHAAN / LEMBAGA";
            worksheet.Cells[2, 2].Value = "ALAMAT PERUSAHAAN / LEMBAGA";
            worksheet.Cells[2, 3].Value = "EMAIL PERUSAHAAN / LEMBAGA";
            worksheet.Cells[2, 4].Value = "REMARKS";
            worksheet.Cells[2, 5].Value = "NAMA PIC";
            worksheet.Cells[2, 6].Value = "JABATAN PIC";
            worksheet.Cells[2, 7].Value = "NO HP PIC";
            worksheet.Cells[2, 8].Value = "EMAIL PIC";
            worksheet.Cells["A1:ZZ1"].Style.Font.Bold = true;
            worksheet.Cells["A1:ZZ"].Style.Font.Name = "Arial";
            worksheet.Cells["A1:ZZ1"].Style.Font.Size = 14;
            worksheet.Cells["A2:ZZ2"].Style.Font.Size = 14;

            worksheet.Cells["A2:ZZ2"].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
            var range = worksheet.Cells["A3:ZZ3"];

            int dataRowCount = worksheet.Dimension.End.Row - 2;
            if (dataRowCount > 0)
            {
                var dataContent = worksheet.Cells["A3:ZZ" + worksheet.Dimension.End.Row];
                dataContent.Style.Font.Size = 12;
            }

            worksheet.Cells["A1:ZZ" + worksheet.Dimension.End.Row].AutoFitColumns();

            if (withData)
            {
                List<TbltExternalContact> datas = _repository.GetAllExternalContact().Where(x => x.DeletedDate == null).OrderBy(x => x.NamaCompany).ToList();

                if (!string.IsNullOrEmpty(searchQuery))
                {
                    // Filter the data based on the search query
                    datas = datas.Where(x =>
                        (x.NamaCompany != null && x.NamaCompany.ToLower().Contains(searchQuery.ToLower())) ||
                        (x.AlamatCompany != null && x.AlamatCompany.ToLower().Contains(searchQuery.ToLower())) ||
                        (x.EmailCompany != null && x.EmailCompany.ToLower().Contains(searchQuery.ToLower())) ||
                        (x.Remark != null && x.Remark.ToLower().Contains(searchQuery.ToLower())) ||
                        (x.NamaPerson != null && x.NamaPerson.ToLower().Contains(searchQuery.ToLower())) ||
                        (x.JabatanPerson != null && x.JabatanPerson.ToLower().Contains(searchQuery.ToLower())) ||
                        (x.NoTelpPerson != null && x.NoTelpPerson.ToLower().Contains(searchQuery.ToLower())) ||
                        (x.EmailPerson != null && x.EmailPerson.ToLower().Contains(searchQuery.ToLower()))
                    ).ToList();
                }

                int x = 3;
                foreach (var rec in datas)
                {
                    worksheet.Cells[x, 1].Value = rec.NamaCompany;
                    worksheet.Cells[x, 2].Value = rec.AlamatCompany;
                    worksheet.Cells[x, 3].Value = rec.EmailCompany;
                    worksheet.Cells[x, 4].Value = rec.Remark;
                    worksheet.Cells[x, 5].Value = rec.NamaPerson;
                    worksheet.Cells[x, 6].Value = rec.JabatanPerson;
                    worksheet.Cells[x, 7].Value = rec.NoTelpPerson;
                    worksheet.Cells[x, 8].Value = rec.EmailPerson;

                    x++;
                }
            }

            // Set the autofilter for the data range
            if (withData)
            {
                // Get the range of data (including headers)
                var dataRange = worksheet.Cells[2, 1, worksheet.Dimension.End.Row, worksheet.Dimension.End.Column];

                // Set autofilter on the data range
                dataRange.AutoFilter = false;
            }

            return package;
        }

        public ExcelPackage ExportXLSThisPage(bool withData, string searchQuery, int pageNumber, int pageSize)
        {
            var package = new ExcelPackage();
            package.Workbook.Properties.Title = "External Contact Download";
            package.Workbook.Properties.Author = "Pertamina";
            package.Workbook.Properties.Subject = "Data Eksport";
            package.Workbook.Properties.Keywords = "IRIS - Pertamina";

            var worksheet = package.Workbook.Worksheets.Add("External Contact");

            // First: add the header
            worksheet.Cells[1, 1].Value = "DATA EXTERNAL CONTACT";
            worksheet.Cells[2, 1].Value = "NAMA PERUSAHAAN / LEMBAGA";
            worksheet.Cells[2, 2].Value = "ALAMAT PERUSAHAAN / LEMBAGA";
            worksheet.Cells[2, 3].Value = "EMAIL PERUSAHAAN / LEMBAGA";
            worksheet.Cells[2, 4].Value = "REMARKS";
            worksheet.Cells[2, 5].Value = "NAMA PIC";
            worksheet.Cells[2, 6].Value = "JABATAN PIC";
            worksheet.Cells[2, 7].Value = "NO HP PIC";
            worksheet.Cells[2, 8].Value = "EMAIL PIC";
            worksheet.Cells["A1:ZZ1"].Style.Font.Bold = true;
            worksheet.Cells["A1:ZZ"].Style.Font.Name = "Arial";
            worksheet.Cells["A1:ZZ1"].Style.Font.Size = 14;
            worksheet.Cells["A2:ZZ2"].Style.Font.Size = 14;

            worksheet.Cells["A2:ZZ2"].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
            var range = worksheet.Cells["A3:ZZ3"];

            int dataRowCount = worksheet.Dimension.End.Row - 2;
            if (dataRowCount > 0)
            {
                var dataContent = worksheet.Cells["A3:ZZ" + worksheet.Dimension.End.Row];
                dataContent.Style.Font.Size = 12;
            }

            worksheet.Cells["A1:ZZ" + worksheet.Dimension.End.Row].AutoFitColumns();

            return package;
        }

        public async Task<ExternalContactViewModel> readExcelPackage(Stream fileInfo, string fileName)
        {

            ExternalContactViewModel model = new ExternalContactViewModel();

            try
            {
                using (var package = new ExcelPackage(fileInfo))
                {
                    ExcelWorksheet worksheet = package.Workbook.Worksheets[0];

                    var rowCount = worksheet.Dimension?.Rows;
                    var colCount = worksheet.Dimension?.Columns;

                    if (!rowCount.HasValue || !colCount.HasValue)
                    {
                        model.IsError = true;
                        model.ErrorMessage = "File Upload Failed!";
                        return model;
                    }

                    List<List<string>> xlsData = new List<List<string>>();
                    List<string> errorMessages = new List<string>(); 

                    // Read the data
                    if (rowCount.Value >= 3)
                    {
                        for (int row = 3; row <= rowCount.Value; row++)
                        {
                            List<string> fieldCol = new List<string>();

                            bool isRowEmpty = true; 

                            for (int col = 1; col <= colCount.Value; col++)
                            {
                                string cellValue = worksheet.Cells[row, col].Value != null ? worksheet.Cells[row, col].Value.ToString() : string.Empty;

                                if (col == 1 || col == 2 || col == 5 || col == 6 || col == 7 || col == 8)
                                {
                                    if (String.IsNullOrWhiteSpace(cellValue))
                                    {
                                        errorMessages.Add("\nRow " + (row - 2) + " column " + GetColumnName(col) + " can't be empty!, ");
                                    }
                                }

                                fieldCol.Add(cellValue);
                                isRowEmpty = false; 
                            }

                            if (!isRowEmpty)
                            {
                                xlsData.Add(fieldCol);
                            }
                        }

                        if (errorMessages.Any())
                        {
                            // Set properti IsError dan ErrorMessage dengan pesan error yang dikumpulkan
                            model.IsError = true;
                            model.ErrorMessage = string.Join("<br>", errorMessages);
                            return model;
                        }

                        var fileSize = fileInfo.Length;
                        model = await _repository.ImportXLSData(xlsData, fileName, fileSize);

                        return model;
                    }

                    model.IsError = true;
                    model.ErrorMessage = "File Upload Failed!";
                    return model;
                }
            }
            catch (Exception ex)
            {
                model.ErrorMessage = ex.Message;
                model.IsError = true;
                return model;
            }
        }

        // Helper function to get the column name based on column index
        private string GetColumnName(int columnNumber)
        {
            int dividend = columnNumber;
            string columnName = string.Empty;

            while (dividend > 0)
            {
                int modulo = (dividend - 1) % 26;
                columnName = Convert.ToChar(65 + modulo) + columnName;
                dividend = (dividend - modulo) / 26;
            }

            return columnName;
        }

    }
}

