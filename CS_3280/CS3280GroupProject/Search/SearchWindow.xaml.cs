using Assignment_6;
using GroupPrject.Common;
using System;
using System.Collections.Generic;
using System.Data;
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

namespace GroupPrject.Search
{
    /// <summary>
    /// Interaction logic for SearchWindow.xaml
    /// </summary>
    public partial class SearchWindow : Window
    {
        /// <summary>
        /// public invoice number for MainWindow access
        /// </summary>
        public int? invoiceNumber = null;

        //initialize logic object
        SearchLogic logic;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="home"></param>
        /// <exception cref="Exception"></exception>
        public SearchWindow()
        {
            try
            {
                InitializeComponent();
                
                logic = new SearchLogic();
            }
            catch (Exception ex) { throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message); }
        }

        /// <summary>
        /// Called when search window is opened to refresh data
        /// </summary>
        /// <exception cref="Exception"></exception>
        private void RefreshData()
        {
            try
            {
                cboInvNum.ItemsSource = SearchLogic.GetInvoiceNumList();
                cboInvDate.ItemsSource = SearchLogic.GetDates();
                cboInvCharge.ItemsSource = SearchLogic.GetTotals();
            }
            catch (Exception ex) { throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message); }
        }

        /// <summary>
        /// Receives the Users invoice number, sends to SearchLogic
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void InvNumPass(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                FillDataGrid();
            }
            catch (Exception ex) { throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message); }

        }
        /// <summary>
        /// Receives the Users invoice date, sends to SearchLogic
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void InvDatePass(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                FillDataGrid();
            }
            catch (Exception ex) { throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message); }
        }

        /// <summary>
        /// Receives the Users Invoice charges list and sends to search logic
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void InvChargePass(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                FillDataGrid();
            }
            catch (Exception ex) { throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message); }
        }

        /// <summary>
        /// Helper function to populate data grid
        /// </summary>
        /// <exception cref="Exception"></exception>
        private void FillDataGrid()
        {
            try
            {
                int selectedInvoiceNumber = cboInvNum.SelectedItem != null ? Convert.ToInt32(cboInvNum.SelectedItem.ToString()) : -1;
                string selectedInvoiceDate = cboInvDate.SelectedItem != null ? cboInvDate.SelectedItem.ToString() : "";
                int selectedInvoiceCharge = cboInvCharge.SelectedItem != null ? Convert.ToInt32(cboInvCharge.SelectedItem.ToString()) : -1;

                if (selectedInvoiceNumber > -1)
                {
                    if (selectedInvoiceDate != "")
                    {
                        if (selectedInvoiceCharge > -1)
                        {
                            dgResults.ItemsSource = SearchLogic.ReturnQuery(selectedInvoiceNumber, selectedInvoiceDate, selectedInvoiceCharge);
                        
                        }
                        else
                        {
                            dgResults.ItemsSource = SearchLogic.ReturnQuery(selectedInvoiceNumber, selectedInvoiceDate);
                        }

                    }
                    else if (selectedInvoiceCharge>-1)
                    {
                        dgResults.ItemsSource = SearchLogic.ReturnQuery(selectedInvoiceNumber, null, selectedInvoiceCharge);
                    }
                    else
                    {
                        dgResults.ItemsSource = SearchLogic.ReturnQuery(selectedInvoiceNumber);
                    }
                }
                else if (selectedInvoiceDate != "" && selectedInvoiceNumber == -1)
                {
                    if(selectedInvoiceCharge > -1) 
                    {
                        dgResults.ItemsSource = SearchLogic.ReturnQuery(null, selectedInvoiceDate, selectedInvoiceCharge);
                    }
                    else
                    {
                        dgResults.ItemsSource = SearchLogic.ReturnQuery(null, selectedInvoiceDate);
                    }
                }
                else if(selectedInvoiceNumber==-1 && selectedInvoiceDate == "" && selectedInvoiceCharge > -1)
                {
                    dgResults.ItemsSource = SearchLogic.ReturnQuery(null, null, selectedInvoiceCharge);
                }
                else
                {
                    dgResults.ItemsSource = SearchLogic.ReturnAllInvoices();
                }
            }
            catch (Exception ex) { throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message); }
        }

        /// <summary>
        /// Gets the selected invoice.
        /// Calls "GetInvoice" from SearchLogic and return it to the MainWindow via ReturnFromSearchWindow().
        /// When a item is selected from the datagrid, the lblSelectStatus will change and appear to
        /// display the selected item to the user before they click the select button.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SelectInvoice(object sender, RoutedEventArgs e)
        {
            try
            {
                invoiceNumber = ((Invoice) dgResults.SelectedItem).InvNum;
                this.Hide();
            }
            catch (Exception ex) { throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message); }

        }

        /// <summary>
        /// Cancel will close the window and return to the Main window.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ClearSearch(object sender, RoutedEventArgs e)
        {
            //Used to have this.close() then this.Hide(), but this button doesn't close the form
            cboInvNum.SelectedItem = null;
            cboInvDate.SelectedItem = null;
            cboInvCharge.SelectedItem = null;
        }

        /// <summary>
        /// Called when window is visible to update data grid
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <exception cref="Exception"></exception>
        private void Window_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            try
            {
                if (this.Visibility == Visibility.Visible)
                {
                    ClearSearch(null, null);
                    RefreshData();
                    FillDataGrid();
                }
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
                if(!MainWindow.ClosedFromMain)
                {
                    this.Hide();
                    e.Cancel = true;
                }
            }
            catch (Exception ex) { throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message); }
        }
    }
}
