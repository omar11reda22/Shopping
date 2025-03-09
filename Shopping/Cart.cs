using DBL.EntityList;
using DBL.entityManager;
using Microsoft.Data.SqlClient;
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
    public partial class Cart : Form
    {
        int userid;
        public Cart(int userid)
        {
            InitializeComponent();
            this.userid = userid;
        }

        private void Cart_Load(object sender, EventArgs e)
        {
            CartList cts = cartmanager.getcartbyuserid(userid);
            dataGridView1.DataSource = cts;

            // Add 'Delete' Button Column
            if (!dataGridView1.Columns.Contains("DeleteProduct"))
            {
                DataGridViewButtonColumn deleteButton = new DataGridViewButtonColumn();
                deleteButton.Name = "DeleteProduct";
                deleteButton.HeaderText = "Action";
                deleteButton.Text = "Delete";
                deleteButton.UseColumnTextForButtonValue = true;
                dataGridView1.Columns.Add(deleteButton);
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == dataGridView1.Columns["DeleteProduct"].Index && e.RowIndex >= 0)
            {
                int productID = Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells["ProductID"].Value);

                DialogResult result = MessageBox.Show("Are you sure you want to remove this product from the cart?", "Confirm Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

                if (result == DialogResult.Yes)
                {
                    bool success = cartmanager.deletefromcart(productID, userid);

                    if (success)
                    {
                        MessageBox.Show("Product removed successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        dataGridView1.DataSource = null;
                        dataGridView1.DataSource = cartmanager.getcartbyuserid(userid);
                    }
                    else
                    {
                        MessageBox.Show("Failed to remove product.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
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
    }
}
