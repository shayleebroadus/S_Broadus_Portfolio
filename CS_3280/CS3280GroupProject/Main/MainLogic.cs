using Assignment_6;
using GroupPrject.Common;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace GroupPrject.Main
{
    internal class MainLogic
    {
        /// <summary>
        /// Gets invoice from database
        /// </summary>
        /// <param name="invoiceNumber"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public static Invoice GetInvoice(int invoiceNumber)
        {
            try
            {
                int results = 0;
                DataSet invoicesData = DataAccess.ExecuteSQLStatement(MainSQL.GetInvoiceData(invoiceNumber), ref results);
                if (results != 1) throw new Exception($"Multiple invoices in database with number {invoiceNumber}!");

                DataRowCollection row = invoicesData.Tables[0].Rows;
                return new Invoice(int.Parse(row[0][0].ToString()), row[0][1].ToString(), int.Parse(row[0][2].ToString()));
            }
            catch (Exception ex) { throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message); }
        }

        /// <summary>
        /// Called when create invoice is called
        /// </summary>
        /// <param name="date"></param>
        /// <param name="totalCost"></param>
        /// <returns>new invoice ID</returns>
        /// <exception cref="Exception"></exception>
        public static Invoice AddInvoice(string date, int totalCost)
        {
            try
            {
                DataAccess.ExecuteNonQuery(MainSQL.AddInvoice(date, totalCost));
                string newID = DataAccess.ExecuteScalarSQL(MainSQL.GetLatestAddedInvoiceID());
                if (int.TryParse(newID, out int id)) { return GetInvoice(id); }
                else throw new Exception("Unable to parse new ID into int!");
            }
            catch (Exception ex) { throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message); }
        }

        /// <summary>
        /// Retrieves all lines items for an invoice
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public static List<Item> GetAllLineItemsFromInvoiceID(Invoice inv)
        {
            try
            {
                List<Item> items = new List<Item>();
                int results = 0;
                DataSet itemsData = DataAccess.ExecuteSQLStatement(MainSQL.GetAllLineItemsInInvoice(inv.InvNum), ref results);
                foreach (DataRow row in itemsData.Tables[0].Rows)
                {
                    items.Add(new Item(row[0].ToString(), row[1].ToString(), row[2].ToString()));
                }

                return items;
            }
            catch (Exception ex) { throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message); }
        }

        /// <summary>
        /// Retrieves all lines items for an invoice
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public static void AddLineItem(Item item, Invoice inv, int lineNum)
        {
            try
            {
                DataAccess.ExecuteNonQuery(MainSQL.AddLineItem(inv.InvNum, lineNum, item.ItemCode));
            }
            catch (Exception ex) { throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message); }
        }

        /// <summary>
        /// Deletes all lines in an invoice
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public static void DeleteLines(Invoice inv)
        {
            try
            {
                DataAccess.ExecuteNonQuery(MainSQL.DeleteLineItemsFromInvoice(inv.InvNum));
            }
            catch (Exception ex) { throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message); }
        }

        /// <summary>
        /// Updates invoice cost
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public static void UpdateInvoiceCost(Invoice inv)
        {
            try
            {
                DataAccess.ExecuteNonQuery(MainSQL.UpdateInvoiceCost(inv.InvCharge, inv.InvNum));
            }
            catch (Exception ex) { throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message); }
        }
    }
}
