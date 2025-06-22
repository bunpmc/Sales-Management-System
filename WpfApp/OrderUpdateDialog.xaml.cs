using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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

namespace WpfApp
{
    /// <summary>
    /// Interaction logic for OrderUpdateDialog.xaml
    /// </summary>
    public partial class OrderUpdateDialog : Window
    {
        public OrderUpdateDialog(Order existingOrder)
        {
            InitializeComponent();

            txtOrderID.Text = existingOrder.OrderID.ToString();
            txtCustomerID.Text = existingOrder.CustomerID.ToString();
            txtEmployeeID.Text = existingOrder.EmployeeID;
            dpOrderDate.SelectedDate = existingOrder.OrderDate;

            txtOrderID.IsReadOnly = true;
        }

        private void OkButton_Click(object sender, RoutedEventArgs e)
        {
            Order order = new Order
            {
                OrderID = int.Parse(txtOrderID.Text),
                CustomerID = int.Parse(txtCustomerID.Text),
                EmployeeID = txtEmployeeID.Text,
                OrderDate = dpOrderDate.SelectedDate ?? DateTime.Now
            };

            DialogResult = true;
            Close();
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
            Close();
        }
    }
}
