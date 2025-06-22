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

        private Customer CreateCustomerFromForm()
        {
            return new Customer
            {
                CustomerID = int.Parse(txtCustomerID.Text),
                CompanyName = txtCompany.Text,
                Address = txtAddress.Text,
                Phone = txtPhone.Text,
                ContactName = txtContactName.Text,
                ContactTitle = txtContactTitle.Text,
            };
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

        private void lvCustomer_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (lvCustomer.SelectedItem is Customer selectedCustomer) {
                txtCustomerID.Text = selectedCustomer.CustomerID.ToString();
                txtContactName.Text = selectedCustomer.ContactName.ToString();
                txtCompany.Text = selectedCustomer.CompanyName.ToString();
                txtAddress.Text = selectedCustomer.Address.ToString();
                txtPhone.Text = selectedCustomer.Phone.ToString();
                txtContactTitle.Text = selectedCustomer.ContactTitle.ToString();
            }
        }

        private void btnAddCustomer_Click(object sender, RoutedEventArgs e)
        {
            if(txtCustomerID == null || txtContactTitle == null
                || txtContactName == null || txtCompany == null ||
                txtAddress == null || txtPhone == null)
            {
                MessageBox.Show("Hay nhap du lieu de them"); return;
            }

            if (!iv.isPhoneValidation(txtPhone.Text)) {
                MessageBox.Show("Sai format so dien thoai, hay nhap nhu vi du sau:\n+84901234567\r\n\r\n84901234567\r\n\r\n0901234567");
                return;
            }

            try
            {
                Customer customer = CreateCustomerFromForm();

                bool isSuccess = cs.AddCustomer(customer);

                if (isSuccess)
                {
                    DisplayCustomer();
                }
                else
                {
                    MessageBox.Show("Loi them khach hang");
                }
            }catch
            {
                MessageBox.Show("Khong the them khach hang vui long kiem tra thong tin");
            }
        }

        private void btnRemoveCustomer_Click(object sender, RoutedEventArgs e)
        {
            if(txtCustomerID == null )
            {
                MessageBox.Show("Please select a customer on the list"); 
                return;
            }
        }

        private void btnUpdateCustomer_Click(object sender, RoutedEventArgs e)
        {
            if (txtCustomerID == null || txtContactTitle == null
                || txtContactName == null || txtCompany == null ||
                txtAddress == null || txtPhone == null)
            {
                MessageBox.Show("Hay nhap du lieu de chinh sua"); return;
            }

            if(!iv.isPhoneValidation(txtPhone.Text))
            {
                MessageBox.Show("Sai format so dien thoai, hay nhap nhu vi du sau:\n+84901234567\r\n\r\n84901234567\r\n\r\n0901234567");
                return;
            }
            try
            {
                Customer customer = CreateCustomerFromForm();
                bool isSucess = cs.UpdateCustomer(customer);

                if (isSucess) {
                    DisplayCustomer();
                } else
                {
                    MessageBox.Show("Khong the cap nhat thong tin khach hang");
                }
            }catch
            {
                MessageBox.Show("Loi khi cap nhat thong tin khach hang");
                
            }            
        }
    }
}
