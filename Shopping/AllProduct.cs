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
                    int availableStock = Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells["Stock"].Value);

                    // Show input box for quantity
                    string input = Microsoft.VisualBasic.Interaction.InputBox("Enter quantity:", "Add to Cart", "1");

                    if (int.TryParse(input, out int quantity) && quantity > 0)
                    {
                        if (quantity <= availableStock)
                        {
                            // Check if product is already in cart
                            bool productExists = cartmanager.IsProductInCart(userID, productID);

                            if (productExists)
                            {
                                // Update the quantity if product is already in cart
                                bool updated = cartmanager.UpdateCartQuantity(userID, productID, quantity);
                                if (updated)
                                {
                                    MessageBox.Show("Product quantity updated in cart successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                }
                                else
                                {
                                    MessageBox.Show("Failed to update product quantity in cart.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                }
                            }
                            else
                            {
                                // Add new product to cart if not already present
                                bool success = cartmanager.AddToCart(userID, productID, quantity);
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
                        else
                        {
                            MessageBox.Show("Entered quantity exceeds available stock!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                    else
                    {
                        MessageBox.Show("Invalid quantity!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Sorry, can't add to cart. Something went wrong.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            //this.Hide();
            Cart c = new Cart(userID);
            c.Show();
        }

        private void linkLabel2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            UserOrder u = new UserOrder(userID);
            u.Show();   
        }
    }
}
