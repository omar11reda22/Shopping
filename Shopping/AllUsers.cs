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
    public partial class AllUsers : Form
    {
        public AllUsers()
        {
            InitializeComponent();
            dataGridView1.CellClick += dataGridView1_CellClick;
        }

        private void AllUsers_Load(object sender, EventArgs e)
        {
            UserList u = UserManager.selectallusers();
            dataGridView1.DataSource = u;

            // Add "Change User Type" Button
            DataGridViewButtonColumn userTypeButton = new DataGridViewButtonColumn();
            userTypeButton.Name = "ChangeUserType";
            userTypeButton.HeaderText = "User Type";
            userTypeButton.Text = "Toggle Type";
            userTypeButton.UseColumnTextForButtonValue = true;
            dataGridView1.Columns.Add(userTypeButton);

            // Add "Toggle Status" Button
            DataGridViewButtonColumn statusButton = new DataGridViewButtonColumn();
            statusButton.Name = "ToggleStatus";
            statusButton.HeaderText = "Status";
            statusButton.Text = "Toggle Status";
            statusButton.UseColumnTextForButtonValue = true;
            dataGridView1.Columns.Add(statusButton);
        }


        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                int userId = Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells["UserID"].Value);

                // Check which button was clicked
                if (dataGridView1.Columns[e.ColumnIndex].Name == "ChangeUserType")
                {
                    if (UserManager.ChangeUserType(userId))
                    {
                        MessageBox.Show("User type updated successfully!");
                        RefreshData();
                    }
                    else
                    {
                        MessageBox.Show("Failed to update user type.");
                    }
                }
                else if (dataGridView1.Columns[e.ColumnIndex].Name == "ToggleStatus")
                {
                    if (UserManager.ToggleUserStatus(userId))
                    {
                        MessageBox.Show("User status updated successfully!");
                        RefreshData();
                    }
                    else
                    {
                        MessageBox.Show("Failed to update status.");
                    }
                }
            }
        }

        // Refresh Data
        private void RefreshData()
        {
            dataGridView1.DataSource = null;
            dataGridView1.DataSource = UserManager.selectallusers();
        }
    }
}
