using GroupPrject.Common;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace GroupPrject.Items
{
    /// <summary>
    /// Interaction logic for ItemsWindow.xaml
    /// </summary>
    public partial class ItemsWindow : Window
    {

        /// <summary>
        /// itemslogic class to hold the business logic
        /// </summary>
        ItemsLogic itemsLogic = new ItemsLogic();
        /// <summary>
        /// Used by the save button to determine if it needs to create a new item
        /// or update an existing one
        /// </summary>
        public bool isEditing;
        /// <summary>
        /// stores the item that was changed so the main window knows how to update the data
        /// </summary>
        public bool ItemsHaveChanged = false;
        /// <summary>
        /// List of all items, used for MainWindow to retrieve
        /// </summary>
        public IEnumerable<Item> Items => (IEnumerable<Item>) dgItems.ItemsSource;
        public ItemsWindow()
        {
            try
            {
                InitializeComponent();

                dgItems.ItemsSource = itemsLogic.GetAllItems();
                txtCode.IsEnabled = false;
                txtCost.IsEnabled = false;
                txtDesc.IsEnabled = false;
            }
            catch (Exception ex) { throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message); }
        }

        /// <summary>
        /// Called when the save button is pressed.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SaveItem(object sender, RoutedEventArgs e)
        {
            try
            {
                // if the text boxes are not null
                if (txtDesc.Text != "" && txtCost.Text != "")
                {
                    // add the information to a new item
                    Item item = new Item(txtCode.Text, txtDesc.Text, txtCost.Text);

                    // if the code box isn't null and the user is not editing
                    if (txtCode.Text != "" && isEditing == false)
                    {
                        // add the new item
                        itemsLogic.AddItem(item);
                    }
                    else if (isEditing == true)
                    {
                        // keep the item code the same, so the user can't change it
                        Item selectedItem = (Item)dgItems.SelectedItem;
                        item.ItemCode = selectedItem.ItemCode;
                        // update the existing item
                        itemsLogic.UpdateItem(item);

                        // No longer editing
                        isEditing = false;
                    }

                    // disable and reset text boxes
                    txtCode.IsEnabled = false;
                    txtCost.IsEnabled = false;
                    txtDesc.IsEnabled = false;
                    txtCode.Text = "";
                    txtCost.Text = "";
                    txtDesc.Text = "";

                    btnEditItem.IsEnabled = true;
                    btnDeleteItem.IsEnabled = true;
                    btnAddItem.IsEnabled = true;
                    // update data grid
                    dgItems.ItemsSource = itemsLogic.GetAllItems();

                    ItemsHaveChanged = true;
                }

            }
            catch (Exception ex) { throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message); }
        }

        /// <summary>
        /// Event handler for add item button click
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <exception cref="Exception"></exception>
        private void btnAddItem_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                // enable text boxes
                txtCode.IsEnabled = true;
                txtCost.IsEnabled = true;
                txtDesc.IsEnabled = true;

                btnEditItem.IsEnabled = false;
                btnDeleteItem.IsEnabled = false;
            }
            catch (Exception ex) { throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message); }
        }

        /// <summary>
        /// Event handler for delete button click
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <exception cref="Exception"></exception>
        private void buttonDeleteItem_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                // if there is a selected item
                if(dgItems.SelectedItem != null)
                {
                    // create new item set to values of the selected item info
                    Item item = (Item)dgItems.SelectedItem;

                    // check if the item is connected to an invoice
                    string invoice = itemsLogic.GetInvoiceNum(item);
                    if (invoice == "")
                    {
                        // delete the item from the database
                        itemsLogic.DeleteItem(item);
                        // update datagrid
                        dgItems.ItemsSource = itemsLogic.GetAllItems();

                        ItemsHaveChanged = true;

                        // home.NotifyOfItemsWindowChange();
                    }
                    else
                    {
                        MessageBox.Show("You cannot delete this item because it is used on this invoice: " + invoice, "Error");
                    }
                }
            }
            catch (Exception ex) { throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message); }
        }

        /// <summary>
        /// Event handler for edit item button click
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <exception cref="Exception"></exception>
        private void btnEditItem_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                // enable text boxes
                txtCost.IsEnabled = true;
                txtDesc.IsEnabled = true;
                btnAddItem.IsEnabled = false;
                btnDeleteItem.IsEnabled = false;

                txtCost.Text = ((Item) dgItems.SelectedItem).Cost.ToString();
                txtDesc.Text = ((Item)dgItems.SelectedItem).ItemDesc.ToString();
                txtCode.Text = ((Item)dgItems.SelectedItem).ItemCode.ToString();

                isEditing = true;
            }
            catch (Exception ex) { throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message); }
        }

        /// <summary>
        /// Called when window is closing
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <exception cref="Exception"></exception>
        private void WindowClosing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            try
            {
                if (!MainWindow.ClosedFromMain)
                {
                    this.Hide();
                    e.Cancel = true;
                }
            }
            catch (Exception ex) { throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message); }
        }
    }
}
