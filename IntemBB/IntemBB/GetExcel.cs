using Microsoft.Office.Interop.Excel;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Web;

namespace IntemBB
{
    public class GetExcel
    {
        public static string Create_Excel(string may, string chat, string some, string solo, string thoigian, string hansd, string nguoilam, string TGKT, string real_num, string plan_id, string tenbieu, string tenbieu1, string tenbieu2, string tenbieu3, string pathFile,string oem)
        {
            try
            {
                FileInfo newFile = new FileInfo(pathFile);
                // Neu file da ton tai thi xoa di
                if (newFile.Exists)
                {
                    newFile.Delete(); // ensures we create a new workbook
                    newFile = new FileInfo(pathFile);
                }
                string update_kvs = "KVS3JIC001. 10 Rev. 4";
                if (may == "01" || may == "02")
                    update_kvs = "KVS3JIC001. 11 Rev. 4";

                //-------------------------------
                //ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
                using (ExcelPackage package = new ExcelPackage(newFile))
                {
                    

                    ExcelWorksheet worksheet = package.Workbook.Worksheets.Add("1");

                    worksheet.Cells["C1:I2"].Value = "建大橡膠（越南）有限公司";
                    if (oem == "OEM")
                    {
                        worksheet.Cells["J1:K2"].Value = "QC-OEM";
                    }
                    //worksheet.Cells["A1:K2"].Value = "建大橡膠（越南）有限公司";
                    worksheet.Cells["A3:K4"].Value = "Công ty Cao su Kenda(Việt Nam)";
                    worksheet.Cells["A4:K6"].Value = tenbieu1;
                    worksheet.Cells["A7:K8"].Value = tenbieu;
                    worksheet.Cells["A9:B10"].Value = tenbieu2;
                    worksheet.Cells["A11:B12"].Value = tenbieu3;
                    worksheet.Cells["A13:B14"].Value = "機台";
                    worksheet.Cells["A15:B16"].Value = "Máy";
                    worksheet.Cells["A17:B18"].Value = "生產首數";
                    worksheet.Cells["A19:B20"].Value = "Số mẻ sản xuất";
                    worksheet.Cells["A21:B22"].Value = update_kvs;
                    worksheet.Cells["F9:H9"].Value = "批號";
                    worksheet.Cells["F10:H10"].Value = "Số lô";
                    worksheet.Cells["F11:H11"].Value = "生產時間";
                    worksheet.Cells["F12:H12"].Value = "Thời gian sản xuất";
                    worksheet.Cells["F13:H14"].Value = "有效日";
                    worksheet.Cells["F15:H16"].Value = "Ngày hiệu lực";
                    worksheet.Cells["F17:H18"].Value = "作業員";
                    worksheet.Cells["F19:H20"].Value = "Người thao tác";

                
                    //worksheet.Cells["C27:I28"].Value = "建大橡膠（越南）有限公司";
                    //if (oem == "OEM")
                    //{
                    //    worksheet.Cells["J27:K28"].Value = "QC-OEM";
                    //}
                    //worksheet.Cells["A29:K30"].Value = "Công ty Cao su Kenda(Việt Nam)";
                    //worksheet.Cells["A31:K32"].Value = "藥品配合劑標示卡";
                    //worksheet.Cells["A33:K34"].Value = tenbieu;
                    //worksheet.Cells["A35:B36"].Value = tenbieu2;
                    //worksheet.Cells["A37:B38"].Value = tenbieu3;
                    //worksheet.Cells["A39:B40"].Value = "機台";
                    //worksheet.Cells["A41:B42"].Value = "Máy";
                    //worksheet.Cells["A43:B44"].Value = "生產首數";
                    //worksheet.Cells["A45:B46"].Value = "Số mẻ sản xuất";
                    //worksheet.Cells["A47:B48"].Value = update_kvs;
                    //worksheet.Cells["F35:H35"].Value = "批號";
                    //worksheet.Cells["F36:H36"].Value = "Số lô";
                    //worksheet.Cells["F37:H37"].Value = "生產時間";
                    //worksheet.Cells["F38:H38"].Value = "Thời gian sản xuất";
                    //worksheet.Cells["F39:H40"].Value = "有效日";
                    //worksheet.Cells["F41:H42"].Value = "Ngày hiệu lực";
                    //worksheet.Cells["F43:H44"].Value = "作業員";
                    //worksheet.Cells["F45:H46"].Value = "Người thao tác";

                    worksheet.Cells["C9:E12"].Value = chat.Trim();
                    worksheet.Cells["C13:E16"].Value = may.Trim();
                    worksheet.Cells["C17:E20"].Value = real_num + "/" + some.Trim();
                    worksheet.Cells["I9:K10"].Value = solo.Trim();
                    worksheet.Cells["I11:K12"].Value = thoigian.Trim();
                    worksheet.Cells["I13:K16"].Value = hansd.Trim();
                    worksheet.Cells["I17:K20"].Value = nguoilam.Trim();
                    worksheet.Cells["F21:K23"].Value = "*" + plan_id.Trim() + "*";
                    worksheet.Cells["F24:K24"].Value = "*" + plan_id.Trim() + "*";
                   
                    //worksheet.Cells["C35:E38"].Value = chat.Trim();
                    //worksheet.Cells["C39:E42"].Value = may.Trim();
                    //worksheet.Cells["C43:E46"].Value = some.Trim() + "/" + real_num;
                    //worksheet.Cells["I35:K36"].Value = solo.Trim();
                    //worksheet.Cells["I37:K38"].Value = thoigian.Trim();
                    //worksheet.Cells["I39:K42"].Value = hansd.Trim();
                    //worksheet.Cells["I43:K46"].Value = nguoilam.Trim();
                    //worksheet.Cells["F47:K49"].Value = "*" + plan_id.Trim() + "*";
                    //worksheet.Cells["F50:K50"].Value = "*" + plan_id.Trim() + "*";
                   

                    //Formatting style for cell
                    worksheet.Cells["A1:K20"].Style.Font.Name = "Times New Roman";
                    worksheet.Cells["A21:B22"].Style.Font.Name = "Times New Roman";
                    worksheet.Cells["F21:K23"].Style.Font.Name = "Code39AzaleaWide2";
                    worksheet.Cells["F21:K23"].Style.Font.Size = 30;

                    worksheet.Cells["F24:K24"].Style.Font.Name = "Times New Roman";
                    worksheet.Cells["F24:K24"].Style.Font.Size = 11;
                    //worksheet.Cells["F21:K24"].Style.Font.Name = "Code39AzaleaWide2";
                    //worksheet.Cells["F21:K24"].Style.Font.Size = 36;

                    //worksheet.Cells["A27:K46"].Style.Font.Name = "Times New Roman";
                    //worksheet.Cells["A47:B48"].Style.Font.Name = "Times New Roman";
                    //worksheet.Cells["F47:K49"].Style.Font.Name = "Code39AzaleaWide2";
                    //worksheet.Cells["F47:K49"].Style.Font.Size = 30;

                    //worksheet.Cells["F50:K50"].Style.Font.Name = "Times New Roman";
                    //worksheet.Cells["F50:K50"].Style.Font.Size = 11;
                 

                    worksheet.Cells["J1:K2"].Style.Font.Bold = true;
                    worksheet.Cells["C9:E12"].Style.Font.Bold = true;
                    worksheet.Cells["C13:E16"].Style.Font.Bold = true;
                    worksheet.Cells["C17:E20"].Style.Font.Bold = true;
                    worksheet.Cells["I9:K10"].Style.Font.Bold = true;
                    worksheet.Cells["I11:K12"].Style.Font.Bold = true;
                    worksheet.Cells["I13:K16"].Style.Font.Bold = true;
                    worksheet.Cells["I17:K20"].Style.Font.Bold = true;

                    //worksheet.Cells["J27:K28"].Style.Font.Bold = true;
                    //worksheet.Cells["C35:E38"].Style.Font.Bold = true;
                    //worksheet.Cells["C39:E42"].Style.Font.Bold = true;
                    //worksheet.Cells["C43:E46"].Style.Font.Bold = true;
                    //worksheet.Cells["I35:K36"].Style.Font.Bold = true;
                    //worksheet.Cells["I37:K38"].Style.Font.Bold = true;
                    //worksheet.Cells["I39:K42"].Style.Font.Bold = true;
                    //worksheet.Cells["I43:K46"].Style.Font.Bold = true;

                    worksheet.Cells["A1:K2"].Style.Font.Size = 22;
                    worksheet.Cells["A3:K4"].Style.Font.Size = 22;
                    worksheet.Cells["A4:K6"].Style.Font.Size = 18;
                    worksheet.Cells["A7:K8"].Style.Font.Size = 18;
                    worksheet.Cells["A9:B10"].Style.Font.Size = 16;
                    worksheet.Cells["A11:B12"].Style.Font.Size = 16;
                    worksheet.Cells["A13:B14"].Style.Font.Size = 16;
                    worksheet.Cells["A15:B16"].Style.Font.Size = 16;
                    worksheet.Cells["A17:B18"].Style.Font.Size = 16;
                    worksheet.Cells["A19:B20"].Style.Font.Size = 16;
                    worksheet.Cells["A21:B22"].Style.Font.Size = 11;
                    worksheet.Cells["F9:H9"].Style.Font.Size = 16;
                    worksheet.Cells["F10:H10"].Style.Font.Size = 16;
                    worksheet.Cells["F11:H11"].Style.Font.Size = 16;
                    worksheet.Cells["F12:H12"].Style.Font.Size = 16;
                    worksheet.Cells["F13:H14"].Style.Font.Size = 16;
                    worksheet.Cells["F15:H16"].Style.Font.Size = 16;
                    worksheet.Cells["F17:H18"].Style.Font.Size = 16;
                    worksheet.Cells["F19:H20"].Style.Font.Size = 16;

                    //worksheet.Cells["A27:K28"].Style.Font.Size = 22;
                    //worksheet.Cells["A29:K30"].Style.Font.Size = 22;
                    //worksheet.Cells["A31:K32"].Style.Font.Size = 18;
                    //worksheet.Cells["A33:K34"].Style.Font.Size = 18;
                    //worksheet.Cells["A35:B36"].Style.Font.Size = 16;
                    //worksheet.Cells["A37:B38"].Style.Font.Size = 16;
                    //worksheet.Cells["A39:B40"].Style.Font.Size = 16;
                    //worksheet.Cells["A41:B42"].Style.Font.Size = 16;
                    //worksheet.Cells["A43:B44"].Style.Font.Size = 16;
                    //worksheet.Cells["A45:B46"].Style.Font.Size = 16;
                    //worksheet.Cells["A47:B48"].Style.Font.Size = 11;
                    //worksheet.Cells["F35:H35"].Style.Font.Size = 16;
                    //worksheet.Cells["F36:H36"].Style.Font.Size = 16;
                    //worksheet.Cells["F37:H37"].Style.Font.Size = 16;
                    //worksheet.Cells["F38:H38"].Style.Font.Size = 16;
                    //worksheet.Cells["F39:H40"].Style.Font.Size = 16;
                    //worksheet.Cells["F41:H42"].Style.Font.Size = 16;
                    //worksheet.Cells["F43:H44"].Style.Font.Size = 16;
                    //worksheet.Cells["F45:H46"].Style.Font.Size = 16;

                    worksheet.Cells["C9:E12"].Style.Font.Size = 20;
                    worksheet.Cells["C13:E16"].Style.Font.Size = 26;
                    if (real_num.Length > 6)
                    {
                        worksheet.Cells["C17:E20"].Style.Font.Size = 20;
                    }
                    else
                    {
                        worksheet.Cells["C17:E20"].Style.Font.Size = 26;
                    }
                       
                    worksheet.Cells["I9:K10"].Style.Font.Size = 26;
                    worksheet.Cells["I11:K12"].Style.Font.Size = 16;
                    worksheet.Cells["I13:K16"].Style.Font.Size = 26;
                    worksheet.Cells["I17:K20"].Style.Font.Size = 26;

                    //worksheet.Cells["C35:E38"].Style.Font.Size = 26;
                    //worksheet.Cells["C39:E42"].Style.Font.Size = 26;
                    //if (real_num.Length > 6) 
                    //{
                    //    worksheet.Cells["C43:E46"].Style.Font.Size = 20;
                    //}
                    //else
                    //{
                    //    worksheet.Cells["C43:E46"].Style.Font.Size = 26;
                    //}
                   
                    //worksheet.Cells["I35:K36"].Style.Font.Size = 26;
                    //worksheet.Cells["I37:K38"].Style.Font.Size = 16;
                    //worksheet.Cells["I39:K42"].Style.Font.Size = 26;
                    //worksheet.Cells["I43:K46"].Style.Font.Size = 26;

                    //merge cells
                    worksheet.Cells["A1:B2"].Merge = true;
                    worksheet.Cells["C1:I2"].Merge = true;
                    worksheet.Cells["J1:K2"].Merge = true;
                    //worksheet.Cells["A1:K2"].Merge = true;
                    worksheet.Cells["A3:K4"].Merge = true;
                    worksheet.Cells["A5:K6"].Merge = true;
                    worksheet.Cells["A7:K8"].Merge = true;
                    worksheet.Cells["A9:B10"].Merge = true;
                    worksheet.Cells["A11:B12"].Merge = true;
                    worksheet.Cells["A13:B14"].Merge = true;
                    worksheet.Cells["A15:B16"].Merge = true;
                    worksheet.Cells["A17:B18"].Merge = true;
                    worksheet.Cells["A19:B20"].Merge = true;
                    worksheet.Cells["A21:B22"].Merge = true;
                    worksheet.Cells["C9:E12"].Merge = true;
                    worksheet.Cells["C13:E16"].Merge = true;
                    worksheet.Cells["C17:E20"].Merge = true;
                    worksheet.Cells["F9:H9"].Merge = true;
                    worksheet.Cells["F10:H10"].Merge = true;
                    worksheet.Cells["F11:H11"].Merge = true;
                    worksheet.Cells["F12:H12"].Merge = true;
                    worksheet.Cells["F13:H14"].Merge = true;
                    worksheet.Cells["F15:H16"].Merge = true;
                    worksheet.Cells["F17:H18"].Merge = true;
                    worksheet.Cells["F19:H20"].Merge = true;
                    worksheet.Cells["I9:K10"].Merge = true;
                    worksheet.Cells["I11:K12"].Merge = true;
                    worksheet.Cells["I13:K16"].Merge = true;
                    worksheet.Cells["I17:K20"].Merge = true;
                    worksheet.Cells["F21:K23"].Merge = true;

                    worksheet.Cells["F24:K24"].Merge = true;
               

                   
                    //worksheet.Cells["A27:B28"].Merge = true;
                    //worksheet.Cells["C27:I28"].Merge = true;
                    //worksheet.Cells["J27:K28"].Merge = true;
                    //worksheet.Cells["A29:K30"].Merge = true;
                    //worksheet.Cells["A31:K32"].Merge = true;
                    //worksheet.Cells["A33:K34"].Merge = true;
                    //worksheet.Cells["A35:B36"].Merge = true;
                    //worksheet.Cells["A37:B38"].Merge = true;
                    //worksheet.Cells["A39:B40"].Merge = true;
                    //worksheet.Cells["A41:B42"].Merge = true;
                    //worksheet.Cells["A43:B44"].Merge = true;
                    //worksheet.Cells["A45:B46"].Merge = true;
                    //worksheet.Cells["A47:B48"].Merge = true;
                    //worksheet.Cells["F35:H35"].Merge = true;
                    //worksheet.Cells["F36:H36"].Merge = true;
                    //worksheet.Cells["F37:H37"].Merge = true;
                    //worksheet.Cells["F38:H38"].Merge = true;
                    //worksheet.Cells["F39:H40"].Merge = true;
                    //worksheet.Cells["F41:H42"].Merge = true;
                    //worksheet.Cells["F43:H44"].Merge = true;
                    //worksheet.Cells["F45:H46"].Merge = true;

                    //worksheet.Cells["F47:K49"].Merge = true;
                    //worksheet.Cells["F50:K50"].Merge = true;

                    //worksheet.Cells["C35:E38"].Merge = true;
                    //worksheet.Cells["C39:E42"].Merge = true;
                    //worksheet.Cells["C43:E46"].Merge = true;
                    //worksheet.Cells["I35:K36"].Merge = true;
                    //worksheet.Cells["I37:K38"].Merge = true;
                    //worksheet.Cells["I39:K42"].Merge = true;
                    //worksheet.Cells["I43:K46"].Merge = true;


                    //Formatting Width,Height
                    worksheet.Column(2).Width = 12.86;
                    worksheet.Column(8).Width = 3.86;

                    worksheet.Row(9).Height = 19;
                    worksheet.Row(10).Height = 19;
                    worksheet.Row(11).Height = 19;
                    worksheet.Row(12).Height = 19;
                    worksheet.Row(35).Height = 19;
                    worksheet.Row(36).Height = 19;
                    worksheet.Row(37).Height = 19;
                    worksheet.Row(38).Height = 19;
                    worksheet.Row(15).Height = 19;
                    worksheet.Row(43).Height = 17;
                    worksheet.Row(46).Height = 19;

                    //Formatting Align cells
                    worksheet.Cells["A1:K24"].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                    worksheet.Cells["A1:K24"].Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center;
                    worksheet.Cells["C9:E12"].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Distributed;
                    worksheet.Cells["A5:K6"].Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Bottom;
                    worksheet.Cells["A7:K8"].Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Top;
                    worksheet.Cells["A9:B10"].Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Bottom;
                    worksheet.Cells["A11:B12"].Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Top;
                    worksheet.Cells["A13:B14"].Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Bottom;
                    worksheet.Cells["A15:B16"].Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Top;
                    worksheet.Cells["A17:B18"].Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Bottom;
                    worksheet.Cells["A19:B20"].Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Top;
                    worksheet.Cells["F9:H9"].Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Top;
                    worksheet.Cells["F10:H10"].Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Bottom;
                    worksheet.Cells["F11:H11"].Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Top;
                    worksheet.Cells["F12:H12"].Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Bottom;
                    worksheet.Cells["F13:H14"].Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Bottom;
                    worksheet.Cells["F15:H16"].Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Top;
                    worksheet.Cells["F17:H18"].Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Bottom;
                    worksheet.Cells["F19:H20"].Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Top;

                    //worksheet.Cells["A27:K50"].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                    //worksheet.Cells["A27:K50"].Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center;
                    //worksheet.Cells["C35:E38"].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Distributed;
                    //worksheet.Cells["A31:K32"].Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Bottom;
                    //worksheet.Cells["A33:K34"].Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Top;
                    //worksheet.Cells["A35:B36"].Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Bottom;
                    //worksheet.Cells["A37:B38"].Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Top;
                    //worksheet.Cells["A39:B40"].Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Bottom;
                    //worksheet.Cells["A41:B42"].Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Top;
                    //worksheet.Cells["A43:B44"].Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Bottom;
                    //worksheet.Cells["A45:B46"].Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Top;
                    //worksheet.Cells["F35:H35"].Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Top;
                    //worksheet.Cells["F36:H36"].Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Bottom;
                    //worksheet.Cells["F37:H37"].Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Top;
                    //worksheet.Cells["F38:H38"].Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Bottom;
                    //worksheet.Cells["F39:H40"].Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Bottom;
                    //worksheet.Cells["F41:H42"].Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Top;
                    //worksheet.Cells["F43:H44"].Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Bottom;
                    //worksheet.Cells["F45:H46"].Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Top;

                
                    worksheet.Cells["A5:K20"].Style.Border.BorderAround(OfficeOpenXml.Style.ExcelBorderStyle.Thin);

                    var cell = worksheet.Cells["A5:K20"];
                    var border = cell.Style.Border;
                    border.Top.Style = border.Left.Style = border.Right.Style = border.Bottom.Style =
                        OfficeOpenXml.Style.ExcelBorderStyle.Thin;

                    cell = worksheet.Cells["A5:K6"];
                    border = cell.Style.Border;
                    border.Bottom.Style = OfficeOpenXml.Style.ExcelBorderStyle.None;
                    cell = worksheet.Cells["A7:K8"];
                    border = cell.Style.Border;
                    border.Top.Style = OfficeOpenXml.Style.ExcelBorderStyle.None;

                    cell = worksheet.Cells["A9:B10"];
                    border = cell.Style.Border;
                    border.Bottom.Style = OfficeOpenXml.Style.ExcelBorderStyle.None;
                    cell = worksheet.Cells["A11:B12"];
                    border = cell.Style.Border;
                    border.Top.Style = OfficeOpenXml.Style.ExcelBorderStyle.None;

                    cell = worksheet.Cells["A13:B14"];
                    border = cell.Style.Border;
                    border.Bottom.Style = OfficeOpenXml.Style.ExcelBorderStyle.None;
                    cell = worksheet.Cells["A15:B16"];
                    border = cell.Style.Border;
                    border.Top.Style = OfficeOpenXml.Style.ExcelBorderStyle.None;

                    cell = worksheet.Cells["A17:B18"];
                    border = cell.Style.Border;
                    border.Bottom.Style = OfficeOpenXml.Style.ExcelBorderStyle.None;
                    cell = worksheet.Cells["A19:B20"];
                    border = cell.Style.Border;
                    border.Top.Style = OfficeOpenXml.Style.ExcelBorderStyle.None;

                    cell = worksheet.Cells["F9:H9"];
                    border = cell.Style.Border;
                    border.Bottom.Style = OfficeOpenXml.Style.ExcelBorderStyle.None;
                    cell = worksheet.Cells["F10:H10"];
                    border = cell.Style.Border;
                    border.Top.Style = OfficeOpenXml.Style.ExcelBorderStyle.None;

                    cell = worksheet.Cells["F11:H11"];
                    border = cell.Style.Border;
                    border.Bottom.Style = OfficeOpenXml.Style.ExcelBorderStyle.None;
                    cell = worksheet.Cells["F12:H12"];
                    border = cell.Style.Border;
                    border.Top.Style = OfficeOpenXml.Style.ExcelBorderStyle.None;

                    cell = worksheet.Cells["F13:H14"];
                    border = cell.Style.Border;
                    border.Bottom.Style = OfficeOpenXml.Style.ExcelBorderStyle.None;
                    cell = worksheet.Cells["F15:H16"];
                    border = cell.Style.Border;
                    border.Top.Style = OfficeOpenXml.Style.ExcelBorderStyle.None;

                    cell = worksheet.Cells["F17:H18"];
                    border = cell.Style.Border;
                    border.Bottom.Style = OfficeOpenXml.Style.ExcelBorderStyle.None;
                    cell = worksheet.Cells["F19:H20"];
                    border = cell.Style.Border;
                    border.Top.Style = OfficeOpenXml.Style.ExcelBorderStyle.None;

                    cell = worksheet.Cells["A5:K20"];
                    cell.Style.Border.BorderAround(OfficeOpenXml.Style.ExcelBorderStyle.Medium);


                    //worksheet.Cells["A31:K46"].Style.Border.BorderAround(OfficeOpenXml.Style.ExcelBorderStyle.Thin);

                    //var cell1 = worksheet.Cells["A31:K46"];
                    //var border1 = cell1.Style.Border;
                    //border1.Top.Style = border1.Left.Style = border1.Right.Style = border1.Bottom.Style =
                    //    OfficeOpenXml.Style.ExcelBorderStyle.Thin;

                    //cell1 = worksheet.Cells["A31:K32"];
                    //border1 = cell1.Style.Border;
                    //border1.Bottom.Style = OfficeOpenXml.Style.ExcelBorderStyle.None;
                    //cell1 = worksheet.Cells["A33:K34"];
                    //border1 = cell1.Style.Border;
                    //border1.Top.Style = OfficeOpenXml.Style.ExcelBorderStyle.None;

                    //cell1 = worksheet.Cells["A35:B36"];
                    //border1 = cell1.Style.Border;
                    //border1.Bottom.Style = OfficeOpenXml.Style.ExcelBorderStyle.None;
                    //cell1 = worksheet.Cells["A37:B38"];
                    //border1 = cell1.Style.Border;
                    //border1.Top.Style = OfficeOpenXml.Style.ExcelBorderStyle.None;

                    //cell1 = worksheet.Cells["A39:B40"];
                    //border1 = cell1.Style.Border;
                    //border1.Bottom.Style = OfficeOpenXml.Style.ExcelBorderStyle.None;
                    //cell1 = worksheet.Cells["A41:B42"];
                    //border1 = cell1.Style.Border;
                    //border1.Top.Style = OfficeOpenXml.Style.ExcelBorderStyle.None;

                    //cell1 = worksheet.Cells["A43:B44"];
                    //border1 = cell1.Style.Border;
                    //border1.Bottom.Style = OfficeOpenXml.Style.ExcelBorderStyle.None;
                    //cell1 = worksheet.Cells["A45:B46"];
                    //border1 = cell1.Style.Border;
                    //border1.Top.Style = OfficeOpenXml.Style.ExcelBorderStyle.None;

                    //cell1 = worksheet.Cells["F35:H35"];
                    //border1 = cell1.Style.Border;
                    //border1.Bottom.Style = OfficeOpenXml.Style.ExcelBorderStyle.None;
                    //cell1 = worksheet.Cells["F36:H36"];
                    //border1 = cell1.Style.Border;
                    //border1.Top.Style = OfficeOpenXml.Style.ExcelBorderStyle.None;

                    //cell1 = worksheet.Cells["F37:H37"];
                    //border1 = cell1.Style.Border;
                    //border1.Bottom.Style = OfficeOpenXml.Style.ExcelBorderStyle.None;
                    //cell1 = worksheet.Cells["F38:H38"];
                    //border1 = cell1.Style.Border;
                    //border1.Top.Style = OfficeOpenXml.Style.ExcelBorderStyle.None;

                    //cell1 = worksheet.Cells["F39:H40"];
                    //border1 = cell1.Style.Border;
                    //border1.Bottom.Style = OfficeOpenXml.Style.ExcelBorderStyle.None;
                    //cell1 = worksheet.Cells["F41:H42"];
                    //border1 = cell1.Style.Border;
                    //border1.Top.Style = OfficeOpenXml.Style.ExcelBorderStyle.None;

                    //cell1 = worksheet.Cells["F43:H44"];
                    //border1 = cell1.Style.Border;
                    //border1.Bottom.Style = OfficeOpenXml.Style.ExcelBorderStyle.None;
                    //cell1 = worksheet.Cells["F45:H46"];
                    //border1 = cell1.Style.Border;
                    //border1.Top.Style = OfficeOpenXml.Style.ExcelBorderStyle.None;

                    //cell1 = worksheet.Cells["A31:K46"];
                    //cell1.Style.Border.BorderAround(OfficeOpenXml.Style.ExcelBorderStyle.Medium);
                
                    worksheet.PrinterSettings.TopMargin = (decimal)0;
                    worksheet.PrinterSettings.LeftMargin = (decimal)0;
                    worksheet.PrinterSettings.RightMargin = (decimal)0;
                    worksheet.PrinterSettings.PaperSize = ePaperSize.A5;
                    worksheet.PrinterSettings.Orientation = eOrientation.Landscape;
                    worksheet.PrinterSettings.HorizontalCentered = true;
                    worksheet.PrinterSettings.FitToPage = true;
                    package.Save();
                    return "";
                }

            }
            catch (Exception ex)
            { return "Lỗi file Excel!" + ex; }
        }
        public class PrintExcel
        {
            public static string Print_xls_file(string printername, string pathFile, ref bool flag, ref string messager)
            {
                bool flag1 = true;
                string messager1 = "";
                try
                {
                    Microsoft.Office.Interop.Excel.Application excelApp = new Microsoft.Office.Interop.Excel.Application();
                    excelApp.Visible = true;
                    FileInfo newFile = new FileInfo(pathFile);

                    // Open the Workbook:
                    Microsoft.Office.Interop.Excel.Workbook wb = excelApp.Workbooks.Open(newFile.ToString(),
                            Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing,
                            Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing,
                            Type.Missing, Type.Missing, Type.Missing, Type.Missing);
                   



                    wb.Worksheets.PrintOut(Type.Missing, Type.Missing, Type.Missing, Type.Missing,
                        printername, Type.Missing, Type.Missing, Type.Missing);

                    // Cleanup:
                    GC.Collect();
                    GC.WaitForPendingFinalizers();

                    //Marshal.FinalReleaseComObject(ws);

                    wb.Close(false, Type.Missing, Type.Missing);
                    Marshal.FinalReleaseComObject(wb);

                    excelApp.Quit();
                    Marshal.FinalReleaseComObject(excelApp);
                    return printername;
                }
                catch (Exception ex)
                {
                    flag = false;
                    messager = ex.ToString();
                    return messager;
                }
            }
        }
    }
}