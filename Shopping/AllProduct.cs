using DBL.EntityList;
using DBL.entityManager;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Shopping
{
    public partial class AllProduct : Form
    {
        private int userID; // Store the logged-in user ID

        public AllProduct(int userID) // Receive UserID in Constructor
        {
            InitializeComponent();
            this.userID = userID;
        }

        private void AllProduct_Load(object sender, EventArgs e)
        {
            ProductList products = ProductManager.GetAllProducts();
            dataGridView1.DataSource = products;

            // Add 'Add to Cart' Button Column
            if (!dataGridView1.Columns.Contains("AddToCart"))
            {
                DataGridViewButtonColumn addButton = new DataGridViewButtonColumn();
                addButton.Name = "AddToCart";
                addButton.HeaderText = "Action";
                addButton.Text = "Add to Cart";
                addButton.UseColumnTextForButtonValue = true;
                dataGridView1.Columns.Add(addButton);
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.ColumnIndex == dataGridView1.Columns["AddToCart"].Index && e.RowIndex >= 0)
                {
                    int productID = Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells["ProductID"].Value);

                    // Call CartManager to add product to cart
                    bool success = cartmanager.AddToCart(userID, productID, 1); // Default quantity 1

                    if (success)
                    {
                        MessageBox.Show("Product added to cart successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        MessageBox.Show("Failed to add product to cart.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("Sorry Cant add to Cart it something is wrong");
            }
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            this.Hide();
            Cart c = new Cart(userID);
            c.Show();
        }
    }
}
