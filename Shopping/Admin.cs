using DBL.Entity;
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
    public partial class Admin : Form
    {
        public Admin()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Adminproducts p = new Adminproducts();
            p.ShowDialog();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            AllOrders o = new AllOrders();
            o.ShowDialog();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            AllUsers u = new AllUsers();
            u.ShowDialog(); 
        }
    }
}
