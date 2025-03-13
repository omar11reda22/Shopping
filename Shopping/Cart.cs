using DBL.EntityList;
using DBL.entityManager;
using Microsoft.Data.SqlClient;
using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace Shopping
{
    public partial class Cart : Form
    {
        int userid;

        public Cart(int userid)
        {
            InitializeComponent();
            this.userid = userid;
            CustomizeUI(); // Apply custom UI styles
        }

        private void Cart_Load(object sender, EventArgs e)
        {
            CartList cts = cartmanager.getcartbyuserid(userid);
            dataGridView1.DataSource = cts;

            // Add 'Delete' Button Column if not added
            if (!dataGridView1.Columns.Contains("DeleteProduct"))
            {
                DataGridViewButtonColumn deleteButton = new DataGridViewButtonColumn();
                deleteButton.Name = "DeleteProduct";
                deleteButton.HeaderText = "Action";
                deleteButton.Text = "Delete";
                deleteButton.UseColumnTextForButtonValue = true;
                deleteButton.DefaultCellStyle.BackColor = Color.Red;
                deleteButton.DefaultCellStyle.ForeColor = Color.White;
                dataGridView1.Columns.Add(deleteButton);
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == dataGridView1.Columns["DeleteProduct"].Index && e.RowIndex >= 0)
            {
                int productID;
                if (int.TryParse(dataGridView1.Rows[e.RowIndex].Cells["ProductID"].Value?.ToString(), out productID))
                {
                    DialogResult result = MessageBox.Show("Are you sure you want to remove this product from the cart?", "Confirm Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

                    if (result == DialogResult.Yes)
                    {
                        bool success = cartmanager.deletefromcart(productID, userid);
                        if (success)
                        {
                            MessageBox.Show("Product removed successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            dataGridView1.DataSource = cartmanager.getcartbyuserid(userid);
                            dataGridView1.Refresh();
                        }
                        else
                        {
                            MessageBox.Show("Failed to remove product.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
                else
                {
                    MessageBox.Show("Invalid Product ID.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection("Data Source=.;Initial Catalog=ShopDB;Integrated Security=True;Trust Server Certificate=True"))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand("ProcessOrder", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@UserID", userid);
                        cmd.ExecuteNonQuery();
                    }
                }
                MessageBox.Show("Order placed successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                Cart_Load(sender, e); // Refresh cart after placing order
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error placing order: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void CustomizeUI()
        {
            this.Text = "Shopping Cart";
            this.BackColor = Color.WhiteSmoke;
            this.StartPosition = FormStartPosition.CenterScreen;
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.Font = new Font("Arial", 10);

            // Style DataGridView
            dataGridView1.BorderStyle = BorderStyle.None;
            dataGridView1.DefaultCellStyle.Font = new Font("Arial", 10);
            dataGridView1.DefaultCellStyle.ForeColor = Color.Black;
            dataGridView1.DefaultCellStyle.SelectionBackColor = Color.LightSteelBlue;
            dataGridView1.DefaultCellStyle.SelectionForeColor = Color.Black;
            dataGridView1.ColumnHeadersDefaultCellStyle.Font = new Font("Arial", 11, FontStyle.Bold);
            dataGridView1.ColumnHeadersDefaultCellStyle.BackColor = Color.SteelBlue;
            dataGridView1.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dataGridView1.EnableHeadersVisualStyles = false;
            dataGridView1.AlternatingRowsDefaultCellStyle.BackColor = Color.LightGray;
            dataGridView1.GridColor = Color.DarkGray;

            // Style Buttons
            button1.Text = "Place Order";
            button1.Font = new Font("Arial", 12, FontStyle.Bold);
            button1.BackColor = Color.DarkGreen;
            button1.ForeColor = Color.White;
            button1.FlatStyle = FlatStyle.Flat;
            button1.FlatAppearance.BorderSize = 0;
            button1.Cursor = Cursors.Hand;
            button1.AutoSize = true;
        }
    }
}
