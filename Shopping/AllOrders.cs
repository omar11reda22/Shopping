﻿using DBL.EntityList;
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
    public partial class AllOrders : Form
    {
        public AllOrders()
        {
            InitializeComponent();
        }

        private void AllOrders_Load(object sender, EventArgs e)
        {
            orderList o = new orderList();
            o = orderManager.getallorders(); 
            dataGridView1.DataSource = o;

            DataGridViewButtonColumn approveButton = new DataGridViewButtonColumn();
            approveButton.Name = "ApproveOrder";
            approveButton.HeaderText = "Approve";
            approveButton.Text = "Approve";
            approveButton.UseColumnTextForButtonValue = true;
            dataGridView1.Columns.Add(approveButton);

        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && dataGridView1.Columns[e.ColumnIndex].Name == "ApproveOrder")
            {
                int orderId = Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells["OrderID"].Value);

                if (orderManager.changeorderstatus(orderId))
                {
                    MessageBox.Show("Order approved successfully!");
                    RefreshData();
                }
                else
                {
                    MessageBox.Show("Failed to approve order.");
                }
            }
        }

        // Refresh DataGridView
        private void RefreshData()
        {
            dataGridView1.DataSource = null;
            dataGridView1.DataSource = orderManager.getallorders();
        }

    }
}
