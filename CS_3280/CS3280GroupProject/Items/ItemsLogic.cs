using Assignment_6;
using GroupPrject.Common;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace GroupPrject.Items
{
    internal class ItemsLogic
    {
        //GetAllItems returns List<clsItems>
        //AddItem(clsItem)
        //EditItem(clsItem OldItem, clsItem NewItem)
        //DeleteItem(clsItem ItemToDelete)
        //IsItemOnInvoice(clsItem)

        /// <summary>
        /// Returns all items from the Item table in a list of clsItems to display in a datagrid
        /// </summary>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        /// 
        public List<Item> GetAllItems()
        {
            try
            {
                List<Item> Items = new List<Item>();
                int i = 0;
                DataSet ds = new DataSet();
                ds = DataAccess.ExecuteSQLStatement(ItemsSQL.GetItems(), ref i);

                DataTable ItemTable = ds.Tables[0];

                foreach (DataRow row in ItemTable.Rows)
                {
                    Item Item = new Item(row[0].ToString(), row[1].ToString(), row[2].ToString());
                    Items.Add(Item);
                }
                return Items;
            }
            catch (Exception ex) { throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message); }
        }

        /// <summary>
        /// adds a passed in item to the datatbase
        /// </summary>
        /// <param name="item"></param>
        /// <exception cref="Exception"></exception>
        public void AddItem(Item item)
        {
            try
            {
                DataAccess.ExecuteNonQuery(ItemsSQL.InsertIntoItemDesc(item.ItemCode, item.ItemDesc, item.Cost));
            }
            catch (Exception ex) { throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message); }
        }

        /// <summary>
        /// deletes a passed in item from the database
        /// </summary>
        /// <param name="item"></param>
        /// <exception cref="Exception"></exception>
        public void DeleteItem(Item item)
        {
            try
            {
                DataAccess.ExecuteNonQuery(ItemsSQL.DeleteFromItemDesc(item.ItemCode));
            }
            catch (Exception ex) { throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message); }
        }

        /// <summary>
        /// Gets the invoice number of a given item
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public string GetInvoiceNum(Item item)
        {
            try
            {
                string invoice = DataAccess.ExecuteScalarSQL(ItemsSQL.GetInvoiceNum(item.ItemCode));
                return invoice;
            }
            catch (Exception ex) { throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message); }
        }

        /// <summary>
        /// Updates a given item with a new description and cost
        /// </summary>
        /// <param name="item"></param>
        /// <exception cref="Exception"></exception>
        public void UpdateItem(Item item)
        {
            try
            {
                DataAccess.ExecuteNonQuery(ItemsSQL.UpdateItemDesc(item.ItemDesc, item.ItemCode, item.Cost));
            }
            catch (Exception ex) { throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message); }
        }
    }
}
