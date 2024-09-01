using Assignment_6;
using GroupPrject.Common;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace GroupPrject.Search
{
    internal class SearchLogic
    {
        public static List<Invoice> ReturnQuery(int? num = null, string? date = null, int? total = null)
        {
            try
            {
                List<Invoice> invoices = new List<Invoice>();
                int results = 0;

                //if num was passed (not date or total)
                if (num != null && date == null && total == null)
                {
                    //SearchNum(int num)
                    DataSet ds = DataAccess.ExecuteSQLStatement(SearchSQL.SearchNum(num.Value), ref results);
                    foreach(DataRow row in ds.Tables[0].Rows) { invoices.Add(new Invoice(int.Parse(row[0].ToString()), row[1].ToString(), int.Parse(row[2].ToString()))); }
                }

                //if num and date were passed (not total)
                else if (num != null && date != null && total == null)
                {
                    //SearchNumDate int num string date
                    DataSet ds = DataAccess.ExecuteSQLStatement(SearchSQL.SearchNumDate(num.Value, date), ref results);
                    foreach (DataRow row in ds.Tables[0].Rows) { invoices.Add(new Invoice(int.Parse(row[0].ToString()), row[1].ToString(), int.Parse(row[2].ToString()))); }
                }
                // if num and total wer passed (not date)
                else if (num != null && total != null && date == null)
                {
                    //SearchNumCharges
                    DataSet ds = DataAccess.ExecuteSQLStatement(SearchSQL.SearchNumCharges(num.Value, total.Value), ref results);
                    foreach (DataRow row in ds.Tables[0].Rows) { invoices.Add(new Invoice(int.Parse(row[0].ToString()), row[1].ToString(), int.Parse(row[2].ToString()))); }
                }
                //if num, date, and total wer passed
                else if (num != null && total != null && date != null)
                {
                    //SearchNumDateCharges(num, date, total)
                    DataSet ds = DataAccess.ExecuteSQLStatement(SearchSQL.SearchNumDateCharges(num.Value, date, total.Value), ref results);
                    foreach (DataRow row in ds.Tables[0].Rows) { invoices.Add(new Invoice(int.Parse(row[0].ToString()), row[1].ToString(), int.Parse(row[2].ToString()))); }
                }
                //if date and total were passed (not num)
                else if (num == null && date != null && total != null)
                {
                    //SearchDateCharges
                    DataSet ds = DataAccess.ExecuteSQLStatement(SearchSQL.SearchDateCharges(date, total.Value), ref results);
                    foreach (DataRow row in ds.Tables[0].Rows) { invoices.Add(new Invoice(int.Parse(row[0].ToString()), row[1].ToString(), int.Parse(row[2].ToString()))); }
                }
                //if date was passed (not num or total)
                else if (date != null && total == null && num == null)
                {
                    //SearchDate
                    DataSet ds = DataAccess.ExecuteSQLStatement(SearchSQL.SearchDate(date), ref results);
                    foreach (DataRow row in ds.Tables[0].Rows) { invoices.Add(new Invoice(int.Parse(row[0].ToString()), row[1].ToString(), int.Parse(row[2].ToString()))); }
                }
                //if total was passed (not num or date)
                else if (total != null && date == null && num == null)
                {
                    //SearchCharges
                    DataSet ds = DataAccess.ExecuteSQLStatement(SearchSQL.SearchCharges(total.Value), ref results);
                    foreach (DataRow row in ds.Tables[0].Rows) { invoices.Add(new Invoice(int.Parse(row[0].ToString()), row[1].ToString(), int.Parse(row[2].ToString()))); }
                }

                return invoices;
            }
            catch (Exception ex) { throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message); }

        }


        public static List<Invoice> ReturnAllInvoices()
        {
            try
            {
                List<Invoice> invoices = new List<Invoice>();
                int results = 0;
                DataSet ds = DataAccess.ExecuteSQLStatement(SearchSQL.SearchAll(), ref results);
                foreach(DataRow row in ds.Tables[0].Rows)
                {
                    invoices.Add(new Invoice(int.Parse(row[0].ToString()), row[1].ToString(), int.Parse(row[2].ToString())));
                }
                return invoices;
            }
            catch (Exception ex) { throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message); }
        }

        public static List<string> GetInvoiceNumList()
        {
            try
            {
                List<string> invoiceNums = new List<string>();
                int results = 0;
                DataSet ds = DataAccess.ExecuteSQLStatement(SearchSQL.SearchDistinctNum(), ref results);
                foreach(DataRow row in ds.Tables[0].Rows) { invoiceNums.Add(row[0].ToString()); }
                return invoiceNums;
            }
            catch (Exception ex) { throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message); }
        }

        public static List<string> GetDates()
        {
            try
            {
                List<string> invoiceNums = new List<string>();
                int results = 0;
                DataSet ds = DataAccess.ExecuteSQLStatement(SearchSQL.SearchDistinctDate(), ref results);
                foreach (DataRow row in ds.Tables[0].Rows) { invoiceNums.Add(row[0].ToString()); }
                return invoiceNums;
            }
            catch (Exception ex) { throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message); }
        }


        public static List<string> GetTotals()
        {
            try
            {
                List<string> invoiceNums = new List<string>();
                int results = 0;
                DataSet ds = DataAccess.ExecuteSQLStatement(SearchSQL.SearchDistinctCost(), ref results);
                foreach (DataRow row in ds.Tables[0].Rows) { invoiceNums.Add(row[0].ToString()); }
                return invoiceNums;
            }
            catch (Exception ex) { throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message); }
        }
    }
}
