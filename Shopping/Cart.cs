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
            CartList cts =  cartmanager.getcartbyuserid(userid);
            dataGridView1.DataSource = cts; 
        }
    }
}
