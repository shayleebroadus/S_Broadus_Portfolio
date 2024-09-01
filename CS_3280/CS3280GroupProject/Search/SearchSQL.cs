using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Automation;

namespace GroupPrject.Search
{
    /// <summary>
    /// class used to get all needed SQL statements for Search functionality
    /// </summary>
    internal class SearchSQL
    {
        /// <summary>
        /// Returns SQL statements to get all invoice data
        /// </summary>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public static string SearchAll()
        {
            try { return "SELECT * FROM Invoices"; }
            catch (Exception ex) { throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message); }
        }

        /// <summary>
        /// Returns SQL statements to search invoices by invoice number
        /// </summary>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public static string SearchNum(int num)
        {
            try{ return "SELECT * FROM Invoices WHERE InvoiceNum = " + num; }
            catch (Exception ex) { throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message); }
        }

        /// <summary>
        /// Returns SQL statements to search invoices by number and data
        /// </summary>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public static string SearchNumDate(int num, string date)
        {
            try{ return "SELECT * FROM Invoices WHERE InvoiceNum=" + num+ " AND InvoiceDate = #" + date+ "#"; }
            catch (Exception ex) { throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message); }
        }

        /// <summary>
        /// Returns SQL statements to search invoices by number, date, and cost
        /// </summary>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public static string SearchNumDateCharges(int num, string date, int charges)
        {
            try{ return "SELECT * FROM Invoices WHERE InvoiceNum = " + num + " AND InvoiceDate = #" + date + "# AND TotalCost = " + charges; }
            catch (Exception ex) { throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message); }
        }

        /// <summary>
        /// Returns SQL statements to search invoices by cost
        /// </summary>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public static string SearchCharges(int charges) 
        {
            try{ return "SELECT * FROM Invoices WHERE TotalCost = " + charges; }
            catch (Exception ex) { throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message); }
        }

        /// <summary>
        /// Returns SQL statements to search invoices by date and cost
        /// </summary>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public static string SearchDateCharges(string date, int charges)
        {
            try{ return "SELECT * FROM Invoices WHERE InvoiceDate = #" + date + "# AND TotalCost = " + charges; }
            catch (Exception ex) { throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message); }
        }

        /// <summary>
        /// Returns SQL statements to search invoices by date
        /// </summary>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public static string SearchDate(string date)
        {
            try{ return "SELECT * FROM Invoices WHERE InvoiceDate = #" + date + "#"; }
            catch (Exception ex) { throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message); }
        }

        /// <summary>
        /// Returns SQL statements to search invoices by number and cost
        /// </summary>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public static string SearchNumCharges(int num, int charges)
        {
            try { return "SELECT * FROM Invoices WHERE InvoiceNum = " + num + "AND TotalCost = " + charges; }
            catch (Exception ex) { throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message); }
        }

        /// <summary>
        /// Returns SQL statements to return all of the invoice numbers from all existing invoices
        /// </summary>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public static string SearchDistinctNum()
        {
            try{ return "SELECT DISTINCT(InvoiceNum) From Invoices order by InvoiceNum"; }
            catch (Exception ex) { throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message); }
        }

        /// <summary>
        /// Returns SQL statements to return all dates from all existing invoices
        /// </summary>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public static string SearchDistinctDate()
        {
            try{ return "SELECT DISTINCT(InvoiceDate) From Invoices order by InvoiceDate"; }
            catch (Exception ex) { throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message); }
        }

        /// <summary>
        /// Returns SQL statements to return costs from all existing invoices
        /// </summary>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public static string SearchDistinctCost()
        {
            try{ return "SELECT DISTINCT(TotalCost) From Invoices order by TotalCost"; }
            catch (Exception ex) { throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message); }
        }

    }
}
