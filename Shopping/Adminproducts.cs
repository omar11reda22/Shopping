using DBL.EntityList;
using DBL.entityManager;
using System;
using System.Data;
using System.Windows.Forms;

namespace Shopping
{
    public partial class Adminproducts : Form
    {
        public Adminproducts()
        {
            InitializeComponent();
        }

        private void Adminproducts_Load(object sender, EventArgs e)
        {
            LoadProducts(); // Load products on form load
        }

        private void LoadProducts()
        {
            ProductList pt = ProductManager.GetAllProducts();
            dataGridView1.DataSource = pt;
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == dataGridView1.Columns["UpdateStock"].Index && e.RowIndex >= 0)
            {
                // Get selected Product ID
                int productID = Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells["ProductID"].Value);

                // Show input box for new stock value
                string input = Microsoft.VisualBasic.Interaction.InputBox("Enter new stock quantity:", "Update Stock", "0");

                if (int.TryParse(input, out int newStock) && newStock >= 0)
                {
                    ProductManager.changeproduct(productID, newStock);
                    MessageBox.Show("Stock updated successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadProducts(); // Refresh the list
                }
                else
                {
                    MessageBox.Show("Invalid stock value!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else if (e.ColumnIndex == dataGridView1.Columns["DeleteProduct"].Index && e.RowIndex >= 0)
            {
                // Get selected Product ID
                int productID = Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells["ProductID"].Value);

                DialogResult confirm = MessageBox.Show("Are you sure you want to delete this product?", "Confirm Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

                if (confirm == DialogResult.Yes)
                {
                    ProductManager.deleteproduct(productID);
                    MessageBox.Show("Product deleted successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadProducts(); // Refresh the list
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            AddProduct ad = new AddProduct();
            ad.ShowDialog(); 
        }
    }
}
