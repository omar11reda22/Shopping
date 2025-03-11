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
    public partial class UserOrder : Form
    {
        orderManager orderManager = new orderManager();
        int userid;
        public UserOrder(int userid)
        {
            
            InitializeComponent();
            this.userid = userid; 
        }

        private void UserOrder_Load(object sender, EventArgs e)
        {
            // loading all orders by user id 
            orderList o = new orderList();
            o = orderManager.getordersbyuserid(userid);
            dataGridView1.DataSource = o;  // bind all orders to grid view 


        }
    }
}
