using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.IO;
using System.Text;
using NPOI;
using NPOI.HPSF;
using NPOI.HSSF;
using NPOI.HSSF.UserModel;
using NPOI.HSSF.Util;
using NPOI.POIFS;
using NPOI.Util;
using NPOI.SS.UserModel;
using NPOI.SS.Util;
using System.Text.RegularExpressions;
//using NPOI.HSSF.UserModel.HSSFPatriarch;
//using NPOI.HSSF.UserModel.HSSFRow;
//using NPOI.HSSF.UserModel.HSSFSheet;
//using NPOI.HSSF.UserModel.HSSFWorkbook;


/// <summary>
/// NPOIHelper NPOI导入导出EXCEL操作类
/// </summary>
public class NPOIHelper
{
    public static string countvalue1 = string.Empty;
    public static string column1 = string.Empty;
    public static string countvalue2 = string.Empty;
    public static string column2 = string.Empty;
    public NPOIHelper()
    { }
   


    /// <summary>       
    /// 用于Web导出       
    /// </summary>       
    /// <param name="dtSource">源DataTable</param>       
    /// <param name="strHeaderText">表头文本</param>       
    /// <param name="strFileName">文件名</param>        
    public static void ExportByWeb(string sum1, string col1, string sum2, string col2, DataTable dtSource, string strHeaderText, string strFileName)
    {
        HttpContext curContext = HttpContext.Current;// 设置编码和附件格式   
        countvalue1 = sum1;
        column1 = col1;
        countvalue2 = sum2;
        column2= col2;
        //curContext.Response.Clear();
        curContext.Response.ContentType = "application/vnd.ms-excel";

        curContext.Response.ContentEncoding = Encoding.UTF8;
        curContext.Response.Charset = "";
        curContext.Response.AppendHeader("Content-Disposition", "attachment;filename=" + HttpUtility.UrlEncode(strFileName, Encoding.UTF8));
        curContext.Response.BinaryWrite(Export(dtSource, strHeaderText).GetBuffer());
        //curContext.Response.End();
    }

    /// <summary>  
    /// DataTable导出到Excel的MemoryStream  
    /// </summary>  
    /// <param name="dtSource">源DataTable</param>  
    /// <param name="strHeaderText">表头文本</param>        
    public static MemoryStream Export(DataTable dtSource, string strHeaderText)
    {
        HSSFWorkbook workbook = new HSSFWorkbook();
        Sheet sheet = workbook.CreateSheet();
        // HSSFSheet sheet1 = (HSSFSheet)workbook.CreateSheet("Sheet1");


        //右击文件 属性信息
        #region
        {
            DocumentSummaryInformation dsi = PropertySetFactory.CreateDocumentSummaryInformation();
            dsi.Company = "";
            workbook.DocumentSummaryInformation = dsi;

            SummaryInformation si = PropertySetFactory.CreateSummaryInformation();
            si.Author = "";
            si.ApplicationName = "";
            si.LastAuthor = "";
            si.Comments = "";
            si.Title = "";
            si.Subject = "";
            si.CreateDateTime = DateTime.Now;
            workbook.SummaryInformation = si;
        }
        #endregion

        CellStyle dateStyle = workbook.CreateCellStyle();
        DataFormat format = workbook.CreateDataFormat();
        dateStyle.DataFormat = format.GetFormat("yyyy-mm-dd");

        //取得列宽  
        int[] arrColWidth = new int[dtSource.Columns.Count];
        foreach (DataColumn item in dtSource.Columns)
        {
            arrColWidth[item.Ordinal] = Encoding.GetEncoding(936).GetBytes(item.ColumnName.ToString()).Length;
        }
        for (int i = 0; i < dtSource.Rows.Count; i++)
        {
            for (int j = 0; j < dtSource.Columns.Count; j++)
            {
                int intTemp = Encoding.GetEncoding(936).GetBytes(dtSource.Rows[i][j].ToString()).Length;
                if (intTemp > arrColWidth[j])
                {
                    arrColWidth[j] = intTemp;
                }
            }
        }



        int rowIndex = 0;
        Row dataRow = null;
        Cell newCell = null;
        string[] img = new string[dtSource.Rows.Count];
        int xz = 1; int yz = 1;
        HSSFPicture picture = null;
        HSSFPatriarch patriarch = (HSSFPatriarch)sheet.CreateDrawingPatriarch();
        HSSFClientAnchor anchor;
        foreach (DataRow row in dtSource.Rows)
        {
            //新建表，填充表头，填充列头，样式
            #region
            if (rowIndex == 65535 || rowIndex == 0)
            {
                if (rowIndex != 0)
                {
                    sheet = workbook.CreateSheet();
                }
                //表头及样式
                #region
                {
                    Row hrow = sheet.CreateRow(0);
                    hrow.Height = 30 * 20;
                    Cell cellTitle = (Cell)hrow.CreateCell(0);
                    cellTitle.SetCellValue(strHeaderText);

                    Row headerRow = sheet.CreateRow(1);
                    //headerRow.HeightInPoints = 0;
                    //sheet.CreateRow(0).Height = 200 * 20;
                    headerRow.CreateCell(0).SetCellValue(strHeaderText);
                    headerRow.Height = 233 * 20;
                    CellStyle headStyle = workbook.CreateCellStyle();
                    headStyle.Alignment = HorizontalAlignment.CENTER;
                    headStyle.VerticalAlignment = VerticalAlignment.CENTER;
                    Font font = workbook.CreateFont();
                    font.FontHeightInPoints = 20;
                    font.FontHeight = 20 * 20;

                    font.Boldweight = 700;
                    headStyle.SetFont(font);
                    cellTitle.CellStyle = headStyle;


                    headerRow.GetCell(0).CellStyle = headStyle;

                    sheet.AddMergedRegion(new CellRangeAddress(0, 0, 0, dtSource.Columns.Count - 1));

                }
                #endregion

                //列头及样式
                #region
                {
                    Row headerRow = sheet.CreateRow(1);


                    CellStyle headStyle = workbook.CreateCellStyle();
                    headStyle.Alignment = HorizontalAlignment.CENTER;
                    Font font = workbook.CreateFont();
                    font.FontHeightInPoints = 10;
                    font.Boldweight = 700;
                    headStyle.SetFont(font);
                    sheet.SetColumnWidth(0, 0);
                    sheet.SetColumnWidth(1, 6000);
                    sheet.SetColumnWidth(2, 6000);
                    sheet.SetColumnWidth(2, 7000);
                    sheet.SetColumnWidth(3, 4000);
                    sheet.SetColumnWidth(7, 6000);
                    sheet.SetColumnWidth(8, 6000);
                    sheet.SetColumnWidth(9, 6000);
                    sheet.SetColumnWidth(10, 4000);
                    foreach (DataColumn column in dtSource.Columns)
                    {
                        headerRow.CreateCell(column.Ordinal).SetCellValue(column.ColumnName);
                        headerRow.GetCell(column.Ordinal).CellStyle = headStyle;
                        
                        //设置列宽  
                        if (column.ColumnName.IndexOf("标识") > -1)
                            sheet.SetColumnWidth(column.Ordinal, 0);
                        else
                        sheet.SetColumnWidth(column.Ordinal, (arrColWidth[column.Ordinal] + 1) * 256);

                    }

                }
                #endregion

                rowIndex = 2;
            }
            #endregion

            //填充内容
            #region
            dataRow = sheet.CreateRow(rowIndex);
            dataRow.Height = 500;
        
            foreach (DataColumn column in dtSource.Columns)
            {
                newCell = dataRow.CreateCell(column.Ordinal);
                
                string drValue = string.Empty;
                if (column.ColumnName == "详细信息" || column.ColumnName == "缩略图")
                {
                    drValue = GetNoHTMLString(row[column].ToString());
                }
                else if (column.ColumnName == "等级")
                {
                    switch (row[column].ToString())
                    {
                        case "0": drValue = "普通会员";

                            break;
                        case "1": drValue = "黄金会员";

                            break;
                        case "2": drValue = "白金会员";

                            break;
                    }

                }
                else if (column.ColumnName == "会员类别")
                {
                    switch (row[column].ToString())
                    {
                        case "0": drValue = "未指定";
                            break;
                        case "1": drValue = "包装会员";
                            break;
                        case "2": drValue = "货运会员";
                            break;
                        case "3": drValue = "果行会员";
                            break;
                        case "4": drValue = "代购会员";
                            break;
                    }

                }
                else if (column.ColumnName == "信息类型")
                {
                    if (row[column].ToString() == "1")
                    {
                        drValue = "包装信息";
                    }
                    else
                    {
                        drValue = "配货信息";
                    }
                }
                else if (column.ColumnName == "照片")
                {
                    string bb = row[column].ToString(); ;
                    img[rowIndex-2] = row[column].ToString();
                    //System.Collections.IList pictures = workbook.GetAllPictures();
                    //int i = 0;
                    //foreach
                    //(HSSFPictureData pic in pictures)
                    //{
                    //    string ext = pic.SuggestFileExtension();
                    //    if
                    //        (ext.Equals("jpeg"))
                    //    {
                    //        System.Drawing.Image jpg = System.Drawing.Image.FromStream(new MemoryStream(pic.Data));
                    //        jpg.Save(string.Format(row[column].ToString(), i++));
                    //    }
                    //    else

                    //        if
                    //        (ext.Equals("png"))
                    //        {
                    //            System.Drawing.Image png = System.Drawing.Image.FromStream(new MemoryStream(pic.Data));
                    //            png.Save(string.Format("pic{0}.png", i++));
                    //        }
                    //}

                   // FileStream fs = new FileStream("1.xls", FileMode.Open);          
                       // HSSFWorkbook hssfworkbook = new HSSFWorkbook(fs);  

                     
                    // HSSFSheet sheet1 = (HSSFSheet)hssfworkbook.GetSheet("sheet1");      
                   

                   // System.Collections.IList p = workbook.GetAllPictures();

                    
                    //anchor.AnchorType = 2;            
                    //load the picture and get the picture index in the workbook      
                   
                    //Reset the image to the original size.       
                   // picture.Resize(1.2);
                    
                    // FileStream file = new FileStream(@"1.xls", FileMode.Create);
                    // workbook.Write(file);    
                    // fs.Close();          
                    // file.Close();     
                
               
                }
                else
                {
                    drValue = row[column].ToString();
                }
           

                //byte[] bytes = System.IO.File.ReadAllBytes(@"D:\MyProject\NPOIDemo\ShapeImage\image1.jpg");
                //int pictureIdx =workbook.AddPicture(bytes, HSSFWorkbook.PICTURE_TYPE_JPEG);

                ////create sheet
                //HSSFSheet sheet = hssfworkbook.CreateSheet("Sheet1");

                //// Create the drawing patriarch.  This is the top level container for all shapes. 
                //HSSFPatriarch patriarch = sheet.CreateDrawingPatriarch();

                ////add a picture
                //HSSFClientAnchor anchor = new HSSFClientAnchor(0, 0, 1023, 0, 0, 0, 1, 3);
                //HSSFPicture pict = patriarch.CreatePicture(anchor, pictureIdx);
 












                //byte[] bytes = System.IO.File.ReadAllBytes(@"D:\MyProject\NPOIDemo\ShapeImage\image1.jpg");
                //int pictureIdx =Workbook .AddPicture(bytes, HSSFWorkbook. PICTURE_TYPE_JPEG);
                //HSSFClientAnchor anchor = new HSSFClientAnchor(0, 0, 1023, 0, 0, 0, 1, 3);
                
                //HSSFPatriarch patriarch = (HSSFPatriarch)sheet.CreateDrawingPatriarch();
                //HSSFPicture pict = patriarch.CreatePicture(anchor, pictureIdx);
                //anchor = new HSSFClientAnchor(0, 0, 0, 255, 2, 2, 4, 7);
                //anchor.AnchorType = 2;
                //  HSSFPicture picture = (HSSFPicture)
                //      patriarch.CreatePicture(anchor, LoadImage("1.jpg", hssfworkbook)); 
                ////Reset the image to the original size.         
                //picture.Resize();          
                //picture.LineStyle = HSSFPicture.LINESTYLE_DASHDOTGEL;

                switch (column.DataType.ToString())
                {

                    case "System.String"://字符串类型  
                        if (column.ColumnName == "应交金额" || column.ColumnName == "实交金额")
                        {
                            string s = drValue;
                            if (drValue.Trim() != string.Empty)
                            {
                                newCell.SetCellValue(double.Parse(drValue));
                            }
                        }
                        else
                        {
                            newCell.SetCellValue(drValue);
                        }
                        break;
                    case "System.DateTime"://日期类型  
                        DateTime dateV;
                        DateTime.TryParse(drValue, out dateV);
                        if (drValue == "")
                        {
                            newCell.SetCellValue(drValue);

                        }
                        else
                        {
                            newCell.SetCellValue(dateV);
                        }

                        newCell.CellStyle = dateStyle;//格式化显示  
                        break;
                    case "System.Boolean"://布尔型  
                        bool boolV = false;
                        bool.TryParse(drValue, out boolV);
                        newCell.SetCellValue(boolV);
                        break;
                    case "System.Int16"://整型  
                    case "System.Int32":
                    case "System.Int64":
                    case "System.Byte":
                        int intV = 0;
                        int.TryParse(drValue, out intV);
                        newCell.SetCellValue(drValue);
                        break;
                    case "System.Decimal"://浮点型  
                    case "System.Double":
                        double doubV = 0;
                        double.TryParse(drValue, out doubV);
                        newCell.SetCellValue(doubV);
                        break;
                    case "System.DBNull"://空值处理  
                        newCell.SetCellValue("");
                        break;
                    default:
                        newCell.SetCellValue(drValue);
                        break;
                }

            }
            #endregion
            rowIndex++;
        }
        try
        {
            for (int x = 0; x < img.Length; x++)
            {
                anchor = new HSSFClientAnchor(1, 1, 1, 1, 2, x + 2, 3, x + 3);
                try
                {
                    if (File.Exists(img[x]))
                    {
                 
                        string newimgurl = img[x].Substring(26);
                    newimgurl=newimgurl.Replace("/", @"\");
                    picture = (HSSFPicture)patriarch.CreatePicture(anchor, LoadImage((@"F:\orderImagelist\" + newimgurl + ""), workbook));
                        picture.LineStyle = HSSFPicture.LINESTYLE_DASHDOTGEL;
                    }
                    else
                    {
                        picture = (HSSFPicture)patriarch.CreatePicture(anchor, LoadImage(HttpContext.Current.Server.MapPath(img[x]), workbook));
                        picture.LineStyle = HSSFPicture.LINESTYLE_DASHDOTGEL;
                    }
                }
                catch (Exception ex)
                {
                  
                }
              
            }
        }
        catch (Exception ex)
        {

        }
        dataRow = sheet.CreateRow(rowIndex);
        Cell cell1 = null;
        Cell cell2 = null;
        for (int i = 0; i < dtSource.Columns.Count; i++)
        {
            HSSFFont font = (HSSFFont)workbook.CreateFont();
            HSSFCellStyle style1 = (HSSFCellStyle)workbook.CreateCellStyle();
            font.Color = HSSFColor.RED.index;
            font.Boldweight = 700;
            style1.SetFont(font);
            
            style1.FillBackgroundColor = NPOI.HSSF.Util.HSSFColor.RED.index;

            if (dtSource.Columns[i].Ordinal == 0)
            {
                //cell1 = dataRow.CreateCell(dtSource.Columns[i].Ordinal);
               // cell1.SetCellValue("合计");
            }
            if (column1 != string.Empty)
            {
                if (dtSource.Columns[i].Ordinal == int.Parse(column1))
                {
                    cell1 = dataRow.CreateCell(dtSource.Columns[i].Ordinal);
                    cell1.CellStyle = style1;
                    if (countvalue1 != string.Empty)
                    {
                        cell1.SetCellValue(double.Parse(countvalue1));
                    }
                }
            }
            if (column2 != string.Empty)
            {
                if (dtSource.Columns[i].Ordinal == int.Parse(column2))
                {
                    cell2 = dataRow.CreateCell(dtSource.Columns[i].Ordinal);
                    cell2.CellStyle = style1;
                    if (countvalue2 != string.Empty)
                    {
                        cell2.SetCellValue(double.Parse(countvalue2));
                    }
                }
            }
            
           
        }





        using (MemoryStream ms = new MemoryStream())
        {
            workbook.Write(ms);
            ms.Flush();
            ms.Position = 0;

            workbook.Dispose();

            return ms;
        }

    }



    public static int LoadImage(string path, HSSFWorkbook wb)
    {
        FileStream file = new FileStream(path, FileMode.Open, FileAccess.Read);
        byte[] buffer = new byte[file.Length];
        file.Read(buffer, 0, (int)file.Length);
        return wb.AddPicture(buffer, PictureType.JPEG);
    } 
    public static string GetNoHTMLString(string Htmlstring)
    {
        //删除脚本     
        Htmlstring = Regex.Replace(Htmlstring, @"<script[^>]*?>.*?</script>", "", RegexOptions.IgnoreCase);
        //删除HTML     
        Htmlstring = Regex.Replace(Htmlstring, @"<(.[^>]*)>", "", RegexOptions.IgnoreCase);
        Htmlstring = Regex.Replace(Htmlstring, @"([\r\n])[\s]+", "", RegexOptions.IgnoreCase);
        Htmlstring = Regex.Replace(Htmlstring, @"-->", "", RegexOptions.IgnoreCase);
        Htmlstring = Regex.Replace(Htmlstring, @"<!--.*", "", RegexOptions.IgnoreCase);


        Htmlstring = Regex.Replace(Htmlstring, @"&(quot|#34);", "\"", RegexOptions.IgnoreCase);
        Htmlstring = Regex.Replace(Htmlstring, @"&(amp|#38);", "&", RegexOptions.IgnoreCase);
        Htmlstring = Regex.Replace(Htmlstring, @"&(lt|#60);", "<", RegexOptions.IgnoreCase);
        Htmlstring = Regex.Replace(Htmlstring, @"&(gt|#62);", ">", RegexOptions.IgnoreCase);
        Htmlstring = Regex.Replace(Htmlstring, @"&(nbsp|#160);", "   ", RegexOptions.IgnoreCase);
        Htmlstring = Regex.Replace(Htmlstring, @"&(iexcl|#161);", "\xa1", RegexOptions.IgnoreCase);
        Htmlstring = Regex.Replace(Htmlstring, @"&(cent|#162);", "\xa2", RegexOptions.IgnoreCase);
        Htmlstring = Regex.Replace(Htmlstring, @"&(pound|#163);", "\xa3", RegexOptions.IgnoreCase);
        Htmlstring = Regex.Replace(Htmlstring, @"&(copy|#169);", "\xa9", RegexOptions.IgnoreCase);
        Htmlstring = Regex.Replace(Htmlstring, @"&#(\d+);", "", RegexOptions.IgnoreCase);


        Htmlstring.Replace("<", "");
        Htmlstring.Replace(">", "");
        Htmlstring.Replace("\r\n", "");
        Htmlstring = HttpContext.Current.Server.HtmlEncode(Htmlstring).Trim();


        return Htmlstring;
    }

    //-----------------------------------------------------------------------------------
}


