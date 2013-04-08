using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text.RegularExpressions;
using System.Runtime.Serialization.Json;
using System.Text;
using System.IO;
using System.Security.Cryptography;
using System.Web.Mvc;
using System.Globalization;
using System.Net.Mail;
using System.Web.Security;
using Microsoft.Office.Interop.Excel;
using System.Reflection;
using System.Runtime.InteropServices;
using BANHSAURIENG.Controllers;


namespace BANHSAURIENG.DataAccess
{
    public static class FunctionLibrary
    {
        /// <summary>
        /// Convert Date String as Json Time
        /// </summary>
        private static string ConvertDateStringToJsonDate(Match m)
        {
            string result = string.Empty;
            DateTime dt = DateTime.Parse(m.Groups[0].Value);
            dt = dt.ToUniversalTime();
            TimeSpan ts = dt - DateTime.Parse("1970-01-01");
            result = string.Format("\\/Date({0}+0800)\\/", ts.TotalMilliseconds);
            return result;
        }

        /// <summary>
        /// JSON Deserialization
        /// </summary>
        public static T JsonDeserialize<T>(string jsonString)
        {
            //Convert "yyyy-MM-dd HH:mm:ss" String as "\/Date(1319266795390+0800)\/"
            string p = @"\d{4}-\d{2}-\d{2}\s\d{2}:\d{2}:\d{2}";
            MatchEvaluator matchEvaluator = new MatchEvaluator(ConvertDateStringToJsonDate);
            Regex reg = new Regex(p);
            jsonString = reg.Replace(jsonString, matchEvaluator);
            DataContractJsonSerializer ser = new DataContractJsonSerializer(typeof(T));
            MemoryStream ms = new MemoryStream(Encoding.UTF8.GetBytes(jsonString));
            T obj = (T)ser.ReadObject(ms);
            return obj;
        }

        private static string GetMd5Hash(MD5 md5Hash, string input)
        {
            // Convert the input string to a byte array and compute the hash.
            byte[] data = md5Hash.ComputeHash(Encoding.UTF8.GetBytes(input));
            StringBuilder sBuilder = new StringBuilder();
            for (int i = 0; i < data.Length; i++)
                sBuilder.Append(data[i].ToString("x2"));
            return sBuilder.ToString();
        }
        
        //convert json string to model entity
        private static T Deserialise<T>(string json)
        {
            using (var ms = new MemoryStream(Encoding.Unicode.GetBytes(json)))
            {
                var serialiser = new DataContractJsonSerializer(typeof(T));
                return (T)serialiser.ReadObject(ms);
            }

        }

        /// <summary>
        /// "1": Individual, "2": Organization
        /// </summary>
        /// <param name="value">Object type value</param>
        /// <returns></returns>
        public static string GetObjectType(int value)
        {
            if (value == 1)
                return "Cá nhân";
            else //if (value == 2)
                return "Tổ chức";
        }

        /// <summary>
        /// "1": Active, "2": Inactive
        /// </summary>
        /// <param name="value">Object status value</param>
        /// <returns></returns>
        public static string GetObjectStatus(int value)
        {
            if (value == 1)
                return "Đang hoạt động";
            else //if (value == 0)
                return "Không hoạt động";
        }

        /// <param name="date">Like 25-10-2012 or 24/10/2012</param>
        public static DateTime? ConvertDate(string date)
        {
            if (string.IsNullOrWhiteSpace(date))
                return null;
            DateTime? result = DateTime.MinValue;
            try
            {
                string[] dateArr = date.Split(new char[] { '-', '/' });

                if (dateArr.Length == 3)
                {
                    int day = Convert.ToInt32(dateArr[0]);
                    int month = Convert.ToInt32(dateArr[1]);
                    int year = Convert.ToInt32(dateArr[2]);
                    result = new DateTime(year, month, day);
                }
                else
                    result = null;
            }
            catch (Exception) { }
            return result;
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="modelState"></param>
        /// <param name="key">Like: "ProductName"</param>
        /// <param name="rawValue">Like: "Your new value"</param>
        public static void SetModelValue(this ModelStateDictionary modelState, string key, object rawValue)
        {
            modelState.SetModelValue(key, new ValueProviderResult(rawValue, String.Empty, CultureInfo.InvariantCulture));
        }

        public static void CreateWorkbook(string FileName, List<BANHSAURIENG.Models.spGetBillDetailsByBillID_Result> bills, string from, string to)
        {
            Microsoft.Office.Interop.Excel.Application xl = null;
            Microsoft.Office.Interop.Excel._Workbook wb = null;
            Microsoft.Office.Interop.Excel._Worksheet sheet = null;
            //VBIDE.VBComponent module = null;
            bool SaveChanges = false;
            try
            {

                if (File.Exists(FileName)) { File.Delete(FileName); }

                GC.Collect();

                // Create a new instance of Excel from scratch

                xl = new Microsoft.Office.Interop.Excel.Application();
                xl.Visible = false;

                // Add one workbook to the instance of Excel

                wb = (Microsoft.Office.Interop.Excel._Workbook)(xl.Workbooks.Add(Missing.Value));
                wb.Sheets.Add(Missing.Value, Missing.Value, Missing.Value, Missing.Value);
                wb.Sheets.Add(Missing.Value, Missing.Value, Missing.Value, Missing.Value);
                wb.Sheets.Add(Missing.Value, Missing.Value, Missing.Value, Missing.Value);
                wb.Sheets.Add(Missing.Value, Missing.Value, Missing.Value, Missing.Value);
                wb.Sheets.Add(Missing.Value, Missing.Value, Missing.Value, Missing.Value);



                // Get a reference to the one and only worksheet in our workbook

                //sheet = (Excel._Worksheet)wb.ActiveSheet;
                sheet = (Microsoft.Office.Interop.Excel._Worksheet)(wb.Sheets[1]);

                // set come column heading names

                //sheet.Cells[1, 2] = "FirstName";
                //sheet.Cells[1, 3] = "LastName";

                // Fill spreadsheet with sample data
                //sheet.Name = "Test";
                sheet.Cells[1,1] = "BÁO CÁO DOANH THU TỪ NGÀY "+from+" ĐẾN NGÀY "+to;
                for (int r = 0, j = 2; r < bills.Count; r++)
                {
                    sheet.Cells[j + 1, 1] = bills[r].BillID;
                    if (String.IsNullOrEmpty(bills[r].ComboName))
                    {
                        sheet.Cells[j + 1, 2] = bills[r].ProductName;
                        sheet.Cells[j + 1, 3] = bills[r].ProductPrice;
                        sheet.Cells[j + 1, 4] = bills[r].ProductQuantity;
                        sheet.Cells[j + 1, 5] = bills[r].ProductQuantity * bills[r].ProductPrice;
                    }
                    else
                    {
                        sheet.Cells[j + 1, 2] = bills[r].ComboName;
                        sheet.Cells[j + 1, 3] = bills[r].ComboPrice;
                        sheet.Cells[j + 1, 4] = bills[r].ComboQUantity;
                        sheet.Cells[j + 1, 5] = bills[r].ComboQUantity * bills[r].ComboPrice;
                    }
                    j++;
                }

                sheet.Name = "Report";
                sheet.Cells[2, 1] = "Mã hóa đơn";
                sheet.Cells[2, 2] = "Sản phẩm";
                sheet.Cells[2, 3] = "Đơn giá";
                sheet.Cells[2, 4] = "Số lượng";
                sheet.Cells[2, 5] = "Thành tiền";
                // Let loose control of the Excel instance

                xl.Visible = false;
                xl.UserControl = false;

                // Set a flag saying that all is well and it is ok to save our changes to a file.

                SaveChanges = true;

                //  Save the file to disk

                wb.SaveAs(FileName, Microsoft.Office.Interop.Excel.XlFileFormat.xlWorkbookNormal,
                          null, null, false, false, Microsoft.Office.Interop.Excel.XlSaveAsAccessMode.xlShared,
                          false, false, null, null, null);

            }
            catch (Exception err)
            {
                String msg;
                msg = "Error: ";
                msg = String.Concat(msg, err.Message);
                msg = String.Concat(msg, " Line: ");
                msg = String.Concat(msg, err.Source);
                Console.WriteLine(msg);
            }
            finally
            {

                try
                {
                    // Repeat xl.Visible and xl.UserControl releases just to be sure
                    // we didn't error out ahead of time.

                    xl.Visible = false;
                    xl.UserControl = false;
                    // Close the document and avoid user prompts to save if our method failed.
                    wb.Close(SaveChanges, null, null);
                    xl.Workbooks.Close();
                }
                catch { }

                // Gracefully exit out and destroy all COM objects to avoid hanging instances
                // of Excel.exe whether our method failed or not.

                xl.Quit();

                //if (module != null) { Marshal.ReleaseComObject(module); }
                if (sheet != null) { Marshal.ReleaseComObject(sheet); }
                if (wb != null) { Marshal.ReleaseComObject(wb); }
                if (xl != null) { Marshal.ReleaseComObject(xl); }

                //module = null;
                sheet = null;
                wb = null;
                xl = null;
                GC.Collect();
            }
        }

        public static System.Data.DataTable ConvertListToDataTable<T>(this List<T> items)
        {
            var tb = new System.Data.DataTable(typeof(T).Name);

            System.Reflection.PropertyInfo[] props = typeof(T).GetProperties(System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Instance);

            foreach (var prop in props)
            {
                tb.Columns.Add(prop.Name, prop.PropertyType);
            }

            foreach (var item in items)
            {
                var values = new object[props.Length];
                for (var i = 0; i < props.Length; i++)
                {
                    values[i] = props[i].GetValue(item, null);
                }

                tb.Rows.Add(values);
            }

            return tb;
        }

    }
}