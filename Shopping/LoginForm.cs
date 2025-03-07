using DBL.Entity;
using DBL.entityManager;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

namespace Shopping
{
    public partial class LoginForm : Form
    {
        public LoginForm()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                string email = emailtxt.Text.Trim();
                string password = passwordtxt.Text.Trim();

                if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password))
                {
                    MessageBox.Show("Email, and Password are required!", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (!isvalidemail(email))
                {
                    MessageBox.Show("invalid email , please enter valid email");
                }

                User userlogin = UserManager.login(email, password);
                if (userlogin != null)
                {
                    MessageBox.Show("login succes");
                }
                else
                {
                    MessageBox.Show("this email not found please register");
                }



            }
            catch (Exception ex)
            {
                MessageBox.Show($"error = {ex}");
            }
        }

        private bool isvalidemail(string email)
        {
            string pattern = @"^[^@\s]+@[^@\s]+\.[^@\s]+$";
            return Regex.IsMatch(email, pattern);
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            this.Hide();
            RegisterForm rg = new RegisterForm();
            rg.Show();
        }

        private void LoginForm_Load(object sender, EventArgs e)
        {

        }
    }
}
