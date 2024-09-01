using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace GroupPrject.Items
{
    /// <summary>
    /// Class used to retrieve SQL statements for Item functionality
    /// </summary>
    internal class ItemsSQL
    {
        /// <summary>
        /// Returns SQL SELECT statement to get all items from ItemDesc
        /// </summary>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public static string GetItems()
        {
            try { return "SELECT ItemCode, ItemDesc, Cost from ItemDesc"; }
            catch (Exception ex) { throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message); }
        }

        /// <summary>
        /// Returns SQL SELECT Statment to get a specific invoice
        /// </summary>
        /// <param name="ItemCode"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public static string GetInvoiceNum(string ItemCode)
        {
            try { return "SELECT distinct(InvoiceNum) from LineItems where ItemCode = '" + ItemCode + "'"; }
            catch (Exception ex) { throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message); }
        }

        /// <summary>
        /// Returns SQL UPDATE Statment to update a specific item description
        /// </summary>
        /// <param name="ItemDesc"></param>
        /// <param name="ItemCode"></param>
        /// <param name="Cost"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public static string UpdateItemDesc(string ItemDesc, string ItemCode, string Cost)
        {
            try { return "Update ItemDesc Set ItemDesc = '" + ItemDesc + "', Cost = " + Cost + " where ItemCode = '" + ItemCode + "'"; }
            catch (Exception ex) { throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message); }
        }

        /// <summary>
        /// Returns SQL INSERT Statment to insert passed in values into ItemDesc table
        /// </summary>
        /// <param name="ItemCode"></param>
        /// <param name="ItemDesc"></param>
        /// <param name="Cost"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public static string InsertIntoItemDesc(string ItemCode, string ItemDesc, string Cost)
        {
            try { return "Insert into ItemDesc(ItemCode, ItemDesc, Cost) Values('" + ItemCode + "', '" + ItemDesc + "', " + Cost + ")"; }
            catch (Exception ex) { throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message); }
        }

        /// <summary>
        /// Returns SQL DELETE Statment to delete specific item from ItemDesc table
        /// </summary>
        /// <param name="ItemCode"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public static string DeleteFromItemDesc(string ItemCode)
        {
            try { return "Delete from ItemDesc Where ItemCode = '" + ItemCode + "'"; }
            catch (Exception ex) { throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message); }
        }
    }
}
