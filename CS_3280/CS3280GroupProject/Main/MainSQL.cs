using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace GroupPrject.Main
{
    internal class MainSQL
    {
        /// <summary>
        /// Returns SQL statements to update invoice cost
        /// </summary>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public static string UpdateInvoiceCost(int cost, int invoiceID)
        {
            try { return $"UPDATE Invoices SET TotalCost = {cost} WHERE InvoiceNum = {invoiceID}"; }
            catch (Exception ex) { throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message); }
        }
        /// <summary>
        /// Returns SQL statements to add line item to invoice
        /// </summary>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public static string AddLineItem(int invoiceNum, int lineItemNum, string itemCode)
        {
            try { return $"INSERT INTO LineItems (InvoiceNum, LineItemNum, ItemCode) Values ({invoiceNum}, {lineItemNum}, '{itemCode}')"; }
            catch (Exception ex) { throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message); }
        }
        /// <summary>
        /// Returns SQL statements to add new invoice
        /// </summary>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public static string AddInvoice(string date, int totalCost)
        {
            try { return $"INSERT INTO Invoices (InvoiceDate, TotalCost) Values (#{date}#, {totalCost})"; }
            catch (Exception ex) { throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message); }
        }
        /// <summary>
        /// Returns SQL statements to get latest added invoice
        /// </summary>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public static string GetLatestAddedInvoiceID()
        {
            try { return $"SELECT MAX(InvoiceNum) FROM Invoices"; }
            catch (Exception ex) { throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message); }
        }
        /// <summary>
        /// Returns SQL statements to get num, date, and cost from invoice data
        /// </summary>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public static string GetInvoiceData(int invoiceNum)
        {
            try { return $"SELECT InvoiceNum, InvoiceDate, TotalCost FROM Invoices WHERE InvoiceNum = {invoiceNum}"; }
            catch (Exception ex) { throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message); }
        }
        /// <summary>
        /// Returns SQL statements to get Item Description data
        /// </summary>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public static string GetItemDescriptionData()
        {
            try { return "SELECT ItemCode, ItemDesc, Cost from ItemDesc"; }
            catch (Exception ex) { throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message); }
        }
        /// <summary>
        /// Returns SQL statements to get all line items in an invoice
        /// </summary>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public static string GetAllLineItemsInInvoice(int invoiceNum)
        {
            try { return $"SELECT LineItems.ItemCode, ItemDesc.ItemDesc, ItemDesc.Cost FROM LineItems, ItemDesc Where LineItems.ItemCode = ItemDesc.ItemCode And LineItems.InvoiceNum = {invoiceNum}"; }
            catch (Exception ex) { throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message); }
        }
        /// <summary>
        /// Returns SQL statements to delete all line items from matching invoice number
        /// </summary>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public static string DeleteLineItemsFromInvoice(int invoicerNum)
        {
            try { return $"DELETE FROM LineItems WHERE InvoiceNum = {invoicerNum}"; }
            catch (Exception ex) { throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message); }
        }
    }
}
