using Syncfusion.Presentation;
using Syncfusion.OfficeChart;
using Pertamina.IRIS.Services.Interfaces;
using System.IO;
using System;
using static Pertamina.IRIS.Services.Implement.PowerPointService;
using OfficeOpenXml.Drawing.Chart;
using System.Collections.Generic;
using Pertamina.IRIS.Repositories.Interfaces;
using Pertamina.IRIS.Models.ViewModels;
using System.Linq;
using Newtonsoft.Json.Linq;

namespace Pertamina.IRIS.Services.Implement
{
    public class PowerPointService : IPowerPointService
    {
        private readonly IDownloadPresentationRepository _repository; 
        public PowerPointService(IDownloadPresentationRepository repository)
        {
            _repository = repository; 
        }

        public void AddTextBeforeChart(ISlide slide, ITable table, int rowIndex, int columnIndex, string text, bool isBold = false)
        {
            // Get the position and size of the column
            double columnX = table.Left + CalculateColumnOffset(table, columnIndex);
            double columnY = table.Top; // Start at the top of the table
            double columnWidth = table.Columns[columnIndex].Width;
            double textHeight = 50; // Height of the text box

            // Add the text above the chart
            IShape textShape = slide.Shapes.AddTextBox(columnX, columnY, columnWidth, textHeight);
            textShape.TextBody.Text = text;
            textShape.TextBody.Paragraphs[0].Font.FontSize = 14;
            textShape.TextBody.Paragraphs[0].Font.Bold = isBold;
            textShape.TextBody.Paragraphs[0].Font.Color = ColorObject.FromArgb(0, 0, 0); // Black color 
        }
      
        public void AddChartWaterFall(ISlide slide, ITable table, int rowIndex, int columnIndex, double column1Width, double column1Height, List<DownloadPresentationChartTemplate2ViewModel> data, string title = "", string bgcolor = "pink")
        {
            // Add the chart to Body Column 1
            IPresentationChart chart = slide.Shapes.AddChart(rowIndex, columnIndex, column1Width, column1Height);

            // Set the chart type to Waterfall
            chart.ChartType = OfficeChartType.WaterFall;
            chart.ChartTitle = title;
            // Customize the chart title area 
            chart.ChartTitleArea.Size = 14; // Set the font size to 24 points
            chart.ChartTitleArea.Bold = true; // Make the font bold 
            // Configure the primary Y-axis to hide it
            chart.PrimaryValueAxis.HasMajorGridLines = false; // Disable grid lines
            chart.PrimaryValueAxis.Visible = false; // Hide the Y-axis 

            var agreementData = new List<(string AgreementType, int Value, bool IsCumulative)>();
            int total = 0;
            foreach (var item in data) {
                total += item.value;
                agreementData.Add((item.name, item.value, false));
            }
            agreementData.Add(("Total", total, true));

            // Populate chart data dynamically
            chart.ChartData.SetValue(1, 1, "Agreement Type");
            chart.ChartData.SetValue(1, 2, "Values");

            rowIndex = 2; // Start after header
            foreach (var (AgreementType, Value, IsCumulative) in agreementData)
            {
                chart.ChartData.SetValue(rowIndex, 1, AgreementType);
                chart.ChartData.SetValue(rowIndex, 2, Value);
                rowIndex++;
            }

            // Set data range
            int startRow = 2; // Start after header
            int endRow = agreementData.Count + 1; // Include all rows
            chart.DataRange = chart.ChartData[startRow, 1, endRow, 2];

            chart.ChartArea.Fill.FillType = (OfficeFillType)FillType.None;
            chart.PlotArea.Fill.FillType = (OfficeFillType)FillType.None;

            // Configure chart properties
            chart.IsSeriesInRows = false; // Series are in columns  

            // Configure the series
            IOfficeChartSerie series = chart.Series[0];

            // Set category labels and values
            series.CategoryLabels = chart.ChartData[startRow, 1, endRow, 1]; // Set category labels
            series.Values = chart.ChartData[startRow, 2, endRow, 2]; // Set values  

            // Set category labels
            series.CategoryLabels = chart.ChartData[startRow, 1, endRow, 1];

            // Configure category axis properties
            IOfficeChartAxis categoryAxis = chart.PrimaryCategoryAxis;
            categoryAxis.TextRotationAngle = -45; 

            // Highlight the "Total" bar
            for (int i = 0; i < agreementData.Count; i++)
            {
                IOfficeChartDataPoint dataPoint = series.DataPoints[i];
                if (agreementData[i].IsCumulative) // Last point is the "Total"
                {
                    dataPoint.DataFormat.Fill.FillType = (OfficeFillType)FillType.Solid;
                    dataPoint.DataFormat.Fill.ForeColor = Syncfusion.Drawing.Color.DarkGreen; // Green for the total bar
                    if (bgcolor == "blue")
                    {
                        dataPoint.DataFormat.Fill.FillType = (OfficeFillType)FillType.Solid;
                        dataPoint.DataFormat.Fill.ForeColor = Syncfusion.Drawing.Color.DarkBlue;
                        dataPoint.DataFormat.Fill.BackColor = Syncfusion.Drawing.Color.DarkBlue;
                    }
                    else
                    {
                        dataPoint.DataFormat.Fill.FillType = (OfficeFillType)FillType.Solid;
                        dataPoint.DataFormat.Fill.ForeColor = Syncfusion.Drawing.Color.Pink;
                        dataPoint.DataFormat.Fill.BackColor = Syncfusion.Drawing.Color.Pink;
                    }

                    dataPoint.SetAsTotal = true; // This ensures the total bar starts from 0
                }
                else
                {
                    if (bgcolor == "blue")
                    {
                        dataPoint.DataFormat.Fill.FillType = (OfficeFillType)FillType.Solid;
                        dataPoint.DataFormat.Fill.ForeColor = Syncfusion.Drawing.Color.DarkBlue;
                        dataPoint.DataFormat.Fill.BackColor = Syncfusion.Drawing.Color.DarkBlue;
                    }
                    else
                    {
                        dataPoint.DataFormat.Fill.FillType = (OfficeFillType)FillType.Solid;
                        dataPoint.DataFormat.Fill.ForeColor = Syncfusion.Drawing.Color.Pink;
                        dataPoint.DataFormat.Fill.BackColor = Syncfusion.Drawing.Color.Pink;
                    }
                }
            }

            //Hiding major gridlines.
            chart.PrimaryValueAxis.HasMajorGridLines = false;
            //Showing minor gridlines.
            chart.PrimaryValueAxis.HasMinorGridLines = false;

            // Enable data labels for all points
            series.DataPoints.DefaultDataPoint.DataLabels.IsValue = true;


            chart.HasLegend = false;
        }
        public void AddChartAgreement(ISlide slide, ITable table, int rowIndex, int columnIndex, double column1Width, double column1Height, DownloadPresentationChartAgreementViewModel data, string title = "", string bgcolor = "pink")
        {
            // Add the chart to Body Column 1
            IPresentationChart chart = slide.Shapes.AddChart(rowIndex, columnIndex, column1Width, column1Height);

            // Set the chart type to Waterfall
            chart.ChartType = OfficeChartType.WaterFall;
            chart.ChartTitle = title;
            // Customize the chart title area 
            chart.ChartTitleArea.Size = 14; // Set the font size to 24 points
            chart.ChartTitleArea.Bold = true; // Make the font bold 
            // Configure the primary Y-axis to hide it
            chart.PrimaryValueAxis.HasMajorGridLines = false; // Disable grid lines
            chart.PrimaryValueAxis.Visible = false; // Hide the Y-axis 

            var agreementData = new List<(string AgreementType, int Value, bool IsCumulative)>
            {
                //(data.ShortNameSecret, data.CountIsSecret, false),
                //(data.ShortNameFirst, data.CountIsFirst, false),
                //(data.ShortNameIsNext, data.CountIsNext, false),
                //(data.ShortNameIsValue, data.CountIsValue, false),
                //(data.ShortNameIsOther, data.CountIsOther, false),
                //("Total", data.CountStatusBerlaku, true) // Mark as cumulative total 

                ("Perjanjian\nKerahasian \n(" + data.ShortNameSecret + ")", data.CountIsSecret, false),
                ("Perjanjian\nTahap Awal \n(" + data.ShortNameFirst + ")", data.CountIsFirst, false),
                ("Perjanjian\nLanjutan \n(" + data.ShortNameIsNext + ")", data.CountIsNext, false),
                ("Perjanjian\ndengan Nila \n(" + data.ShortNameIsValue + ")", data.CountIsValue, false),
                ("Lainnya\n(" + data.ShortNameIsOther + ")", data.CountIsOther, false),
                ("Total", data.CountStatusBerlaku, true) // Mark as cumulative total 
            };

            // Populate chart data dynamically
            chart.ChartData.SetValue(1, 1, "Agreement Type");
            chart.ChartData.SetValue(1, 2, "Values");

            rowIndex = 2; // Start after header
            foreach (var (AgreementType, Value, IsCumulative) in agreementData)
            {
                chart.ChartData.SetValue(rowIndex, 1, AgreementType);
                chart.ChartData.SetValue(rowIndex, 2, Value);
                rowIndex++;
            }

            // Set data range
            int startRow = 2; // Start after header
            int endRow = agreementData.Count + 1; // Include all rows
            chart.DataRange = chart.ChartData[startRow, 1, endRow, 2];

            chart.ChartArea.Fill.FillType = (OfficeFillType)FillType.None;
            chart.PlotArea.Fill.FillType = (OfficeFillType)FillType.None;

            // Configure chart properties
            chart.IsSeriesInRows = false; // Series are in columns  

            // Configure the series
            IOfficeChartSerie series = chart.Series[0];

            // Set category labels and values
            series.CategoryLabels = chart.ChartData[startRow, 1, endRow, 1]; // Set category labels
            series.Values = chart.ChartData[startRow, 2, endRow, 2]; // Set values  

            // Set category labels
            series.CategoryLabels = chart.ChartData[startRow, 1, endRow, 1];

            // Configure category axis properties
            IOfficeChartAxis categoryAxis = chart.PrimaryCategoryAxis;  
            categoryAxis.TextRotationAngle = -45; 

            // Highlight the "Total" bar
            for (int i = 0; i < agreementData.Count; i++)
            {
                IOfficeChartDataPoint dataPoint = series.DataPoints[i];
                if (agreementData[i].IsCumulative) // Last point is the "Total"
                {
                    dataPoint.DataFormat.Fill.FillType = (OfficeFillType)FillType.Solid;
                    dataPoint.DataFormat.Fill.ForeColor = Syncfusion.Drawing.Color.DarkGreen; // Green for the total bar
                    if (bgcolor == "blue")
                    {
                        dataPoint.DataFormat.Fill.FillType = (OfficeFillType)FillType.Solid;
                        dataPoint.DataFormat.Fill.ForeColor = Syncfusion.Drawing.Color.DarkBlue;
                        dataPoint.DataFormat.Fill.BackColor = Syncfusion.Drawing.Color.DarkBlue;
                    }
                    else
                    {
                        dataPoint.DataFormat.Fill.FillType = (OfficeFillType)FillType.Solid;
                        dataPoint.DataFormat.Fill.ForeColor = Syncfusion.Drawing.Color.Pink;
                        dataPoint.DataFormat.Fill.BackColor = Syncfusion.Drawing.Color.Pink;
                    }

                    dataPoint.SetAsTotal = true; // This ensures the total bar starts from 0
                }
                else
                {
                    if (bgcolor == "blue")
                    {
                        dataPoint.DataFormat.Fill.FillType = (OfficeFillType)FillType.Solid;
                        dataPoint.DataFormat.Fill.ForeColor = Syncfusion.Drawing.Color.DarkBlue;
                        dataPoint.DataFormat.Fill.BackColor = Syncfusion.Drawing.Color.DarkBlue;
                    }
                    else
                    {
                        dataPoint.DataFormat.Fill.FillType = (OfficeFillType)FillType.Solid;
                        dataPoint.DataFormat.Fill.ForeColor = Syncfusion.Drawing.Color.Pink;
                        dataPoint.DataFormat.Fill.BackColor = Syncfusion.Drawing.Color.Pink;
                    }
                }
            }

            //Hiding major gridlines.
            chart.PrimaryValueAxis.HasMajorGridLines = false;
            //Showing minor gridlines.
            chart.PrimaryValueAxis.HasMinorGridLines = false;

            // Enable data labels for all points
            series.DataPoints.DefaultDataPoint.DataLabels.IsValue = true;


            chart.HasLegend = false;
        }
        public void AddChartStrategic(ISlide slide, ITable table, int rowIndex, int columnIndex, DownloadPresentationChartStrategicViewModel data)
        {
            // Calculate the position and size of Body Column 1
            double column1Width = 450; // Half of the table width
            double column1Height = 200; // Total table height

            double columnX = table.Left + CalculateColumnOffset(table, columnIndex);
            double columnY = table.Top; // Start at the top of the table


            // Add the chart to Body Column 1
            IPresentationChart chart = slide.Shapes.AddChart(columnX, 100, column1Width, column1Height);

            // Set the chart type to Waterfall
            chart.ChartType = OfficeChartType.Bar_Clustered;
            chart.ChartTitle = "";

            int total = data.CountBDGreen + data.CountBDBusiness + data.CountStrategic + data.CountGtg;
            // Dynamic data source
            var agreementData = new List<(string AgreementType, int Value)>
            {
                ("Total", total),
                ("(4) BD agreement untuk green business", data.CountBDGreen),
                ("(3) BD agreement untuk caore business (> 500 juta USD transaction atau > 500 juta USD annualy)", data.CountBDBusiness),
                ("(2) Strategic Partnership", data.CountStrategic),
                ("(1) G2G driven", data.CountGtg)
            };


            // Add headers
            chart.ChartData.SetValue(1, 1, "Agreement Type");
            chart.ChartData.SetValue(1, 2, "Values");

            // Populate rows
            rowIndex = 2;
            foreach (var (AgreementType, Value) in agreementData)
            {
                chart.ChartData.SetValue(rowIndex, 1, AgreementType);
                chart.ChartData.SetValue(rowIndex, 2, Value);
                rowIndex++;
            }

            // Set data range
            int startRow = 2; // After the header
            int endRow = agreementData.Count + 1;
            chart.DataRange = chart.ChartData[startRow, 1, endRow, 2];

            // Configure series
            chart.IsSeriesInRows = false; // Data is in columns
            IOfficeChartSerie series = chart.Series[0];
            series.CategoryLabels = chart.ChartData[startRow, 1, endRow, 1]; // Set category labels
            series.Values = chart.ChartData[startRow, 2, endRow, 2]; // Set values  
            // Enable data labels for all points
            series.DataPoints.DefaultDataPoint.DataLabels.IsValue = true;
            series.SerieFormat.Fill.FillType = (OfficeFillType)FillType.Solid;
            series.SerieFormat.Fill.ForeColor = Syncfusion.Drawing.Color.Pink;
            series.SerieFormat.Fill.BackColor = Syncfusion.Drawing.Color.Pink;

            //Hiding major gridlines.
            chart.PrimaryValueAxis.HasMajorGridLines = false;

            //Showing minor gridlines.
            chart.PrimaryValueAxis.HasMinorGridLines = false;

            chart.HasLegend = false; // Hides the legend

            // Set chart title
            chart.ChartTitle = "";

        }
        public void AddImageFooter(ISlide slide, ITable table, int rowIndex, int columnIndex, string imagePath)
        {
            // Calculate the cell position manually based on the table's position and column widths
            double cellX = table.Left + CalculateColumnOffset(table, columnIndex);
            double cellY = table.Top + CalculateRowOffset(table, rowIndex);
            double cellWidth = 100;
            double cellHeight = 20;

            // Add the image to the slide within the bounds of the cell
            using (FileStream imageStream = new FileStream(imagePath, FileMode.Open, FileAccess.Read))
            {
                slide.Shapes.AddPicture(imageStream, -2, 520, cellWidth, cellHeight);
            }
        }
        public void AddImageToCell(ISlide slide, ITable table, int rowIndex, int columnIndex, string imagePath)
        {
            // Calculate the cell position manually based on the table's position and column widths
            double cellX = table.Left + CalculateColumnOffset(table, columnIndex);
            double cellY = table.Top + CalculateRowOffset(table, rowIndex);
            double cellWidth = 180;
            double cellHeight = 60;

            // Add the image to the slide within the bounds of the cell
            using (FileStream imageStream = new FileStream(imagePath, FileMode.Open, FileAccess.Read))
            {
                slide.Shapes.AddPicture(imageStream, cellX, cellY, cellWidth, cellHeight);
            }
        }
        private double CalculateColumnOffset(ITable table, int columnIndex)
        {
            double offset = 0;
            for (int i = 0; i < columnIndex; i++)
            {
                offset += table.Columns[i].Width;
            }
            return offset;
        }
        private double CalculateRowOffset(ITable table, int rowIndex)
        {
            double offset = 0;
            for (int i = 0; i < rowIndex; i++)
            {
                offset += table.Rows[i].Height;
            }
            return offset;
        }    
        public void AddChartAgreementDescription(ISlide slide, DownloadPresentationChartAgreementViewModel data, int rowIndex, int columnIndex, double column1Width, double column1Height, string freeText)
        {
            // Add a text box for the bulleted list
            IShape textBox = slide.Shapes.AddTextBox(rowIndex, columnIndex, column1Width, column1Height);
             
            var dataChartAgreementDesc = _repository.GetDataChartAgreementDescription();
           
            var lastItem = dataChartAgreementDesc.LastOrDefault();
            string partners = string.Empty;
            foreach (var ad in dataChartAgreementDesc)
            {
                partners += ad.PartnerName +
                    " dengan " + ad.Code +
                    " untuk pengembangan " + ad.JudulPerjanjian + (ad == lastItem ? ".":", "); 
            }

            int IsValueDikurangiLNG = data.CountIsValue - data.CountLNG;

            string text = "Terdapat " + IsValueDikurangiLNG + " perjanjian dengan Nilai ("+data.ShortNameIsValue+") ";
                   text += "merupakan transaksi pelumas dan fuel supply sedangkan sisanya sebanyak " +data.CountLNG+" SPA ";
            text += "merupakan transaksi jual beli LNG. " + freeText;
           
            // Add the bulleted list to the text box
            string[] content = {
            "Terdapat 5 Perjanjian Tahap Awal ("+ data.ShortNameFirst +") yang berlanjut menjadi Perjanjian Lanjutan ("+ data.ShortNameIsNext + "/" + data.ShortNameIsValue +") dengan partner yang terdiri " + partners,
            text
            };

            AddBulletedList(textBox.TextBody, content);
        }
        public void AddContentListAfterChart2(ISlide slide, ITable table, int rowIndex, int columnIndex, string[] content)
        {
            // Get the position and size of the column
            double columnX = table.Left + CalculateColumnOffset(table, columnIndex);
            double columnY = table.Top; // Start at the top of the table
            double columnWidth = table.Columns[columnIndex].Width;

            // Add the text above the chart
            IShape textShape = slide.Shapes.AddTextBox(columnX, 300, columnWidth, 20);
            textShape.TextBody.Text = "Note :";
            textShape.TextBody.Paragraphs[0].Font.FontSize = 14;
            textShape.TextBody.Paragraphs[0].Font.Color = ColorObject.FromArgb(0, 0, 0); // Black color 

            // Add a text box for the bulleted list
            IShape textBox = slide.Shapes.AddTextBox(columnX, 310, columnWidth, 150);

            // Add the bulleted list to the text box
            AddDashList(textBox.TextBody, content);
        }
        private void AddBulletedList(ITextBody textBody, string[] content)
        {
            // Clear default content in the text body
            textBody.Text = string.Empty;

            // Iterate through the content array
            foreach (string item in content)
            {
                // Add a new paragraph for each item
                IParagraph paragraph = textBody.Paragraphs.Add();

                // Set the text for the paragraph
                paragraph.Text = item;

                // Format as a bulleted list
                paragraph.ListFormat.Type = ListType.Bulleted;


                // Optional: Customize font and bullet style
                paragraph.Font.Color = ColorObject.FromArgb(0, 0, 0); // Set font color to black 
                paragraph.Font.FontSize = 11;
                // Set text alignment to justify
                paragraph.HorizontalAlignment = HorizontalAlignmentType.Justify;
                // Set indentation for the bullet
                paragraph.FirstLineIndent = -20; // Adjust bullet position (negative for outdenting)
                paragraph.LeftIndent = 20; // Align the text properly
                // Add padding (spacing) between paragraphs
                paragraph.SpaceAfter = 10; // Set spacing after each paragraph (in points)

            }
        }
        private void AddDashList(ITextBody textBody, string[] content)
        {

            // Clear default content in the text body
            textBody.Text = string.Empty;

            // Iterate through the content array
            foreach (string item in content)
            {
                // Add a new paragraph for each item
                IParagraph paragraph = textBody.Paragraphs.Add();

                // Set the text for the paragraph
                paragraph.Text = item;

                // Format as a bulleted list
                paragraph.ListFormat.Type = ListType.Bulleted;

                // Customize bullet to be a dash
                paragraph.ListFormat.BulletCharacter = '-'; // Set dash as the bullet character
                // Optional: Customize font and bullet style
                paragraph.Font.Color = ColorObject.FromArgb(0, 0, 0); // Set font color to black 
                paragraph.Font.FontSize = 11;
                // Set text alignment to justify
                paragraph.HorizontalAlignment = HorizontalAlignmentType.Justify;
                // Set indentation for the bullet
                paragraph.FirstLineIndent = -20; // Adjust bullet position (negative for outdenting)
                paragraph.LeftIndent = 20; // Align the text properly
                // Add padding (spacing) between paragraphs
                paragraph.SpaceAfter = 10; // Set spacing after each paragraph (in points)

            }
        }
        public void AddChartCountryPartner(ISlide slide, ITable table, int rowIndex, int columnIndex, double column1Width, double column1Height, List<DownloadPresentationChartTemplate2ViewModel> data)
        {
            // Add the chart to Body Column 1
            IPresentationChart chart = slide.Shapes.AddChart(rowIndex, columnIndex, column1Width, column1Height);

            // Set the chart type to Waterfall
            chart.ChartType = OfficeChartType.Column_Clustered;
            chart.ChartTitle = "";


            // Dynamic data source 
            var agreementData = new List<(string AgreementType, int Value)>();
            int total = 0;
            foreach (var item in data)
            {
                total += item.value;
                agreementData.Add((item.name, item.value));
            }
            agreementData.Add(("Total", total));

            // Populate chart data dynamically
            chart.ChartData.SetValue(1, 1, "Agreement Type");
            chart.ChartData.SetValue(1, 2, "Values");

            rowIndex = 1;
            foreach (var (AgreementType, Value) in agreementData)
            {
                chart.ChartData.SetValue(rowIndex, 1, AgreementType);
                chart.ChartData.SetValue(rowIndex, 2, Value);
                rowIndex++;
            }

            // Set data range
            int startRow = 1; // After the header
            int endRow = agreementData.Count;
            chart.DataRange = chart.ChartData[startRow, 1, endRow, 2];

            chart.ChartArea.Fill.FillType = (OfficeFillType)FillType.None;
            chart.PlotArea.Fill.FillType = (OfficeFillType)FillType.None;


            // Configure chart properties
            chart.IsSeriesInRows = false; // Series are in columns  

            // Configure the series
            IOfficeChartSerie series = chart.Series[0];

            series.CategoryLabels = chart.ChartData[startRow, 1, endRow, 1]; // Set category labels
            series.Values = chart.ChartData[startRow, 2, endRow, 2]; // Set values  

            series.SerieFormat.Fill.FillType = (OfficeFillType)FillType.Solid;
            series.SerieFormat.Fill.ForeColor = Syncfusion.Drawing.Color.DarkBlue;
            series.SerieFormat.Fill.BackColor = Syncfusion.Drawing.Color.DarkBlue;


            //Hiding major gridlines.
            chart.PrimaryValueAxis.HasMajorGridLines = false;
            //Showing minor gridlines.
            chart.PrimaryValueAxis.HasMinorGridLines = false;

            // Enable data labels for all points
            series.DataPoints.DefaultDataPoint.DataLabels.IsValue = true;
            //series.DataPoints.DefaultDataPoint.DataLabels.Color = OfficeKnownColors.White;


            chart.HasLegend = false;
        }

        public void AddChartStatusKegiatanDescription(ISlide slide, string[] freeText, int rowIndex, int columnIndex, double column1Width, double column1Height)
        {
            // Add a text box for the bulleted list
            IShape textBox = slide.Shapes.AddTextBox(rowIndex, columnIndex, column1Width, column1Height);
             
            // Add the bulleted list to the text box
            string[] content = { "58 dari 11 perjanjian pada stang on going discussion dan postponed akan expired dalam waktu 6 bulan ke depan.",
                "Done discussion adalah perjanjian yang masuk dalam tahap implementation /SPA/transaction/disepakati kedua belah pihak untuk diselesaikan."};

            string[] mergedContent = content.Concat(freeText).ToArray();
            AddBulletedList(textBox.TextBody, mergedContent);
        }


    }


}

