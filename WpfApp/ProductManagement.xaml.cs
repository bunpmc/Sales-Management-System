using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using BusinessLayer;
using Services;

namespace WpfApp
{
    /// <summary>
    /// Interaction logic for ProductManagement.xaml
    /// </summary>
    public partial class ProductManagement : Window
    {
        private ProductService ps = new ProductService(); // Service xử lý logic
        public ProductManagement()
        {
            InitializeComponent();
            DisplayProducts();
        }

        private void DisplayProducts()
        {
            ps.GenerateSampleDataset();
            lvProduct.ItemsSource = null;
            lvProduct.ItemsSource = ps.GetProducts();
        }

        private Product CreateProductFromForm()
        {
            return new Product
            {
                ProductID = int.Parse(txtProductID.Text),
                ProductName = txtProductName.Text,
                SupplierID = int.Parse(txtSupplierID.Text),
                CategoryID = int.Parse(txtCategoryID.Text),
                QuantityPerUnit = int.Parse(txtQuantityPerUnit.Text),
                UnitPrice = double.Parse(txtUnitPrice.Text),
                UnitsInStock = int.Parse(txtUnitsInStock.Text),
                UnitsOnOrder = int.Parse(txtUnitsOnOrder.Text),
                ReorderLevel = int.Parse(txtReorderLevel.Text),
                Discontinued = chkDiscontinued.IsChecked ?? false
            };
        }

        private void lvProduct_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (lvProduct.SelectedItem is Product selectedProduct)
            {
                txtProductID.Text = selectedProduct.ProductID.ToString();
                txtProductName.Text = selectedProduct.ProductName;
                txtSupplierID.Text = selectedProduct.SupplierID.ToString();
                txtCategoryID.Text = selectedProduct.CategoryID.ToString();
                txtQuantityPerUnit.Text = selectedProduct.QuantityPerUnit.ToString();
                txtUnitPrice.Text = selectedProduct.UnitPrice.ToString("F2");
                txtUnitsInStock.Text = selectedProduct.UnitsInStock.ToString();
                txtUnitsOnOrder.Text = selectedProduct.UnitsOnOrder.ToString();
                txtReorderLevel.Text = selectedProduct.ReorderLevel.ToString();
                chkDiscontinued.IsChecked = selectedProduct.Discontinued;
            }
        }

        private void btnAddProduct_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtProductID.Text) ||
                string.IsNullOrWhiteSpace(txtProductName.Text))
            {
                MessageBox.Show("Hãy nhập đầy đủ thông tin sản phẩm.");
                return;
            }

            try
            {
                Product product = CreateProductFromForm();
                bool isSuccess = ps.AddProduct(product);

                if (isSuccess)
                {
                    DisplayProducts();
                }
                else
                {
                    MessageBox.Show("Không thể thêm sản phẩm. Có thể trùng ID.");
                }
            }
            catch
            {
                MessageBox.Show("Lỗi khi thêm sản phẩm. Vui lòng kiểm tra dữ liệu nhập vào.");
            }
        }

        private void btnUpdateProduct_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtProductID.Text))
            {
                MessageBox.Show("Hãy chọn sản phẩm để cập nhật.");
                return;
            }

            try
            {
                Product product = CreateProductFromForm();
                bool isSuccess = ps.UpdateProduct(product);

                if (isSuccess)
                {
                    DisplayProducts();
                }
                else
                {
                    MessageBox.Show("Không thể cập nhật sản phẩm.");
                }
            }
            catch
            {
                MessageBox.Show("Lỗi khi cập nhật sản phẩm.");
            }
        }

        private void btnRemoveProduct_Click(object sender, RoutedEventArgs e)
        {
            if (!int.TryParse(txtProductID.Text, out int productId))
            {
                MessageBox.Show("Hãy chọn một sản phẩm để xóa.");
                return;
            }

            bool isSuccess = ps.RemoveProduct(productId);

            if (isSuccess)
            {
                DisplayProducts();
            }
            else
            {
                MessageBox.Show("Không thể xóa sản phẩm.");
            }
        }

        private void btnSearchProduct_Click(object sender, RoutedEventArgs e)
        {
            if (!int.TryParse(txtSearchProductID.Text, out int id))
            {
                MessageBox.Show("ID không hợp lệ.");
                return;
            }

            Product product = ps.SearchProduct(id);

            if (product != null)
            {
                lvProduct.ItemsSource = null;
                lvProduct.ItemsSource = new List<Product> { product };
            }
            else
            {
                MessageBox.Show($"Không tìm thấy sản phẩm với ID = {id}");
            }
        }
    }
}
