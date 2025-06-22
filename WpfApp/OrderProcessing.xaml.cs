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
using Services;
using BusinessLayer;

namespace WpfApp
{
    /// <summary>
    /// Interaction logic for OrderProcessing.xaml
    /// </summary>
    public partial class OrderProcessing : Window
    {
        private OrderService os = new OrderService();
        public OrderProcessing()
        {
            InitializeComponent();
            DisplayOrders();
        }

        private void DisplayOrders ()
        {
            lvOrder.ItemsSource = null;
            os.GenerateSampleDataset();
            lvOrder.ItemsSource = os.GetOrders();
        }

        private void btnAddOrder_Click(object sender, RoutedEventArgs e)
        {
            if(lvOrder.SelectedItem is Order selectedOrder)
            {
                OrderDialog orderDialog = new OrderDialog();
                orderDialog.ShowDialog();
            }
            
        }

        private void btnUpdateOrder_Click(object sender, RoutedEventArgs e)
        {
            if (lvOrder.SelectedItem is Order selectedOrder)
            {
                OrderUpdateDialog orderDialog = new OrderUpdateDialog(selectedOrder);
                orderDialog.ShowDialog();
            }

        }

    }
}
