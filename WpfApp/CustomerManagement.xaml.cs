using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using BusinessLayer;
using Services;

namespace WpfApp
{
    /// <summary>
    /// Interaction logic for CustomerManagement.xaml
    /// </summary>
    public partial class CustomerManagement : Window
    {
        private CustomerService cs = new CustomerService();
        private InputValidator iv = new InputValidator();
        public CustomerManagement()
        {
            InitializeComponent();
            DisplayCustomer();
        }

        private void DisplayCustomer()
        {
            cs.GenerateSampleDataset();
            lvCustomer.ItemsSource = null;
            lvCustomer.ItemsSource = cs.GetCustomers();
        }

        private void btnSearch_Click(object sender, RoutedEventArgs e)
        {
            int id = int.Parse(txtCustomerID_Search.Text);

            Customer customer = cs.SearchCustomer(id);

            if(customer != null) {
                lvCustomer.ItemsSource = null;
                lvCustomer.ItemsSource = new List<Customer> { customer};
            } else
            {
                MessageBox.Show($"No Customer with {id} found");
            }
        }

        private void btnAddCustomer_Click(object sender, RoutedEventArgs e)
        {
            AddCustomerDialog addCustomerDialog = new AddCustomerDialog();
            if (addCustomerDialog.ShowDialog() == true)
            {
                DisplayCustomer();
            }
        }

        private void btnRemoveCustomer_Click(object sender, RoutedEventArgs e)
        {
            if(lvCustomer.SelectedItem is not Customer customer)
            {
                MessageBox.Show("Please select a customer on the list"); 
                return;
            }

            cs.RemoveCustomer(customer.CustomerID);
        }

        private void btnUpdateCustomer_Click(object sender, RoutedEventArgs e)
        {
            if (lvCustomer.SelectedItem is Customer selectedCustomer)
            {
                UpdateCustomerDialog customerDialog = new UpdateCustomerDialog(selectedCustomer);
                if (customerDialog.ShowDialog() == true)
                {
                    DisplayCustomer();
                }
            } else
            {
                MessageBox.Show("Chon 1 khach hang de update");
            }
        }
    }
}
