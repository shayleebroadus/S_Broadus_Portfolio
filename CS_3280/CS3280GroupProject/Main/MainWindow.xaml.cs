using GroupPrject.Common;
using GroupPrject.Items;
using GroupPrject.Main;
using GroupPrject.Search;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace GroupPrject
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        /// <summary>
        /// Helper function to allow dialog windows to close without needing to reinstantiate
        /// </summary>
        public static bool ClosedFromMain { get; private set; }  = false;

        /// <summary>
        /// Search window instance
        /// </summary>
        SearchWindow searchWindow;

        /// <summary>
        /// Items window instance
        /// </summary>
        ItemsWindow itemsWindow;

        /// <summary>
        /// Invoice object. Can be null if no invoice is selected
        /// </summary>
        Invoice invoice;

        /// <summary>
        /// Used to determine if invoice is being edited or not
        /// </summary>
        bool isEditing = false;

        /// <summary>
        /// Items for an invoice
        /// </summary>
        List<Item> items = new List<Item>();

        /// <summary>
        /// Main window constructor
        /// </summary>
        /// <exception cref="Exception"></exception>
        public MainWindow()
        {
            try
            {
                InitializeComponent();
                searchWindow = new SearchWindow();
                itemsWindow = new ItemsWindow();

                ItemsDropdown.ItemsSource = itemsWindow.Items;
                SetInvoiceEditable(false);
            }
            catch (Exception ex) { HandleError(ex); }
        }

        /// <summary>
        /// Called on "Search" menu item click
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <exception cref="Exception"></exception>
        private void OpenSearch(object sender, RoutedEventArgs e)
        {
            try
            {
                searchWindow.ShowDialog();
                if (searchWindow.invoiceNumber != null)
                {
                    int invoiceNumber = searchWindow.invoiceNumber.Value;
                    searchWindow.invoiceNumber = null;
                    invoice = MainLogic.GetInvoice(invoiceNumber);
                    items = MainLogic.GetAllLineItemsFromInvoiceID(invoice);
                    UpdateInvoiceUI();
                }
            }
            catch (Exception ex) { HandleError(ex); }
        }

        /// <summary>
        /// Called on "Items" menu item click
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <exception cref="Exception"></exception>
        private void OpenItems(object sender, RoutedEventArgs e)
        {
            try
            {
                itemsWindow.ShowDialog();
                if (itemsWindow.ItemsHaveChanged)
                {
                    ItemsDropdown.ItemsSource = itemsWindow.Items;
                    ItemsDropdown.SelectedItem = null;
                    SelectedItemCostLabel.Content = null;
                    itemsWindow.ItemsHaveChanged = false;
                }
            }
            catch (Exception ex) { HandleError(ex); }
        }

        /// <summary>
        /// Called when Items dropdown is changed
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ItemsDropdownSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                if(ItemsDropdown.SelectedItem != null) SelectedItemCostLabel.Content = ((Item)ItemsDropdown.SelectedItem).Cost.ToString();
            }
            catch (Exception ex) { HandleError(ex); }
        }

        /// <summary>
        /// Helper function for enabling/disabling item UI
        /// </summary>
        /// <param name="editable"></param>
        private void SetInvoiceEditable(bool editable)
        {
            try
            {
                ItemsDropdown.IsEnabled = editable;
                AddItemButton.IsEnabled = editable;
                RemoveItemButton.IsEnabled = editable;
                SaveButton.IsEnabled = editable;
                EditButton.IsEnabled = !editable && invoice != null;
            }
            catch (Exception ex) { HandleError(ex); }
        }

        /// <summary>
        /// Helper function to update UI when invoice is loaded
        /// </summary>
        /// <exception cref="Exception"></exception>
        private void UpdateInvoiceUI()
        {
            try
            {
                ItemsGrid.ItemsSource = null;
                if (invoice != null)
                {
                    InvoiceDateLabel.Content = invoice.InvDate.ToString();
                    InvoiceNumberLabel.Content = invoice.InvNum.ToString();
                    ItemsGrid.ItemsSource = items;

                    int cost = 0;
                    foreach(Item item in items) cost += int.Parse(item.Cost);
                    InvoiceCostLabel.Content = cost.ToString();
                    invoice.InvCharge = cost;

                    SetInvoiceEditable(isEditing);
                }
                else
                {
                    InvoiceDateLabel.Content = "";
                    InvoiceNumberLabel.Content = "";
                    InvoiceCostLabel.Content = "";
                    SetInvoiceEditable(false);
                }
            }
            catch (Exception ex) { HandleError(ex); }
        }

        /// <summary>
        /// Called when create invoice is pressed
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <exception cref="Exception"></exception>
        private void CreateInvoice(object sender, RoutedEventArgs e)
        {
            try
            {
                invoice = MainLogic.AddInvoice(DateTime.Now.ToString("MM/dd/yyyy"), 0);
                items = new List<Item>();
                UpdateInvoiceUI();
            }
            catch (Exception ex) { HandleError(ex); }
        }

        /// <summary>
        /// Called when edit invoice is pressed
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <exception cref="Exception"></exception>
        private void EditInvoice(object sender, RoutedEventArgs e)
        {
            try
            {
                isEditing = true;
                UpdateInvoiceUI();
            }
            catch (Exception ex) { HandleError(ex); }
        }

        /// <summary>
        /// Called when add item is pressed
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <exception cref="Exception"></exception>
        private void AddItem(object sender, RoutedEventArgs e)
        {
            try
            {
                Item selected = ItemsDropdown.SelectedItem as Item;
                items.Add(selected);
                UpdateInvoiceUI();
            }
            catch (Exception ex) { HandleError(ex); }
        }

        /// <summary>
        /// Called when remove item is pressed
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <exception cref="Exception"></exception>
        private void RemoveItem(object sender, RoutedEventArgs e)
        {
            try
            {
                if (ItemsGrid.SelectedItem == null) return;
                Item selected = ItemsGrid.SelectedItem as Item;
                items.Remove(selected);
                UpdateInvoiceUI();
            }
            catch (Exception ex) { HandleError(ex); }
        }

        /// <summary>
        /// Called when save invoice is pressed
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <exception cref="Exception"></exception>
        private void SaveInvoice(object sender, RoutedEventArgs e)
        {
            try
            {
                MainLogic.DeleteLines(invoice);
                for(int x = 0 ; x < items.Count ; x++)
                {
                    MainLogic.AddLineItem(items[x], invoice, x + 1);
                }
                MainLogic.UpdateInvoiceCost(invoice);

                ItemsDropdown.SelectedItem = null;
                SelectedItemCostLabel.Content = null;

                isEditing = false;
                UpdateInvoiceUI();
            }
            catch (Exception ex) { HandleError(ex); }
        }

        /// <summary>
        /// Called on window closing. Sets a static variable so any other windows know this is a full close.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <exception cref="Exception"></exception>
        private void WindowClosing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            try
            {
                ClosedFromMain = true;
            }
            catch (Exception ex) { HandleError(ex); }
        }

        private void HandleError(Exception ex)
        {
            try
            {
                MessageBox.Show("Error", ex.Message, MessageBoxButton.OK, MessageBoxImage.Error);
            }
            catch (Exception e) { throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + e.Message); }
        }
    }
}
