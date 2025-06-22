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
using Services;

namespace WpfApp
{
    /// <summary>
    /// Interaction logic for OrderDialog.xaml
    /// </summary>
    public partial class OrderDialog : Window
    {
        private OrderService os = new OrderService();
        public OrderDialog()
        {
            InitializeComponent();
        }

        private Order CreateOrderFromForm()
        {
            return new Order
            {
                OrderID = int.Parse(txtOrderID.Text),
                CustomerID = int.Parse(txtCustomerID.Text),
                EmployeeID = txtEmployeeID.Text,
                OrderDate = dpOrderDate.SelectedDate ?? DateTime.Now
            };
        }

        private void OkButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Order order = CreateOrderFromForm();

                bool isSuccess = os.AddOrder(order);

                if(isSuccess)
                {
                    MessageBox.Show("Them don hang thanh cong");
                    OrderProcessing op = new OrderProcessing();
                    op.Show();
                } else
                {
                    MessageBox.Show("Them don hang that bai");
                }
                
            }
            catch (Exception ex) {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
