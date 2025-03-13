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
            LoadProducts();

            // Ensure buttons are added only once
            if (dataGridView1.Columns["UpdateStock"] == null)
            {
                DataGridViewButtonColumn updateButton = new DataGridViewButtonColumn();
                updateButton.Name = "UpdateStock";
                updateButton.HeaderText = "Update Stock";
                updateButton.Text = "Update";
                updateButton.UseColumnTextForButtonValue = true;
                dataGridView1.Columns.Add(updateButton);
            }

            if (dataGridView1.Columns["DeleteProduct"] == null)
            {
                DataGridViewButtonColumn deleteButton = new DataGridViewButtonColumn();
                deleteButton.Name = "DeleteProduct";
                deleteButton.HeaderText = "Delete Product";
                deleteButton.Text = "Delete";
                deleteButton.UseColumnTextForButtonValue = true;
                dataGridView1.Columns.Add(deleteButton);
            }

            // Attach event handler
            dataGridView1.CellContentClick += dataGridView1_CellContentClick;
        }

        private void LoadProducts()
        {
            ProductList pt = ProductManager.GetAllProducts();
            dataGridView1.DataSource = pt;
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return; // Ignore header clicks

            string columnName = dataGridView1.Columns[e.ColumnIndex].Name;
            int productID = Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells["ProductID"].Value);
            int currentStock = Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells["Stock"].Value);
            decimal currentPrice = Convert.ToDecimal(dataGridView1.Rows[e.RowIndex].Cells["Price"].Value);

            if (columnName == "UpdateStock")
            {
                string input = Microsoft.VisualBasic.Interaction.InputBox($"Enter new stock quantity (Current: {currentStock}):", "Update Stock", currentStock.ToString());
                string inputPrice = Microsoft.VisualBasic.Interaction.InputBox($"Enter new price (Current: {currentPrice}):", "Update Price", currentPrice.ToString());

                if (int.TryParse(input, out int newStock) && newStock >= 0 &&
                    decimal.TryParse(inputPrice, out decimal newPrice) && newPrice >= 0)
                {
                    ProductManager.changeproduct(productID, newStock, newPrice);
                    MessageBox.Show("Stock and price updated successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadProducts();
                }
                else
                {
                    MessageBox.Show("Invalid stock or price value!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else if (columnName == "DeleteProduct")
            {
                bool isPending = ProductManager.IsProductInPendingOrder(productID);

                if (isPending)
                {
                    MessageBox.Show("This product is in a pending order and cannot be deleted!", "Deletion Not Allowed", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                DialogResult confirm = MessageBox.Show("Are you sure you want to delete this product?", "Confirm Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

                if (confirm == DialogResult.Yes)
                {
                    ProductManager.deleteproduct(productID);
                    MessageBox.Show("Product deleted successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadProducts();
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            AddProduct ad = new AddProduct();
            ad.ShowDialog();
            LoadProducts();
        }
    }
}
