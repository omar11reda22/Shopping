using DBL.Entity;
using DBL.entityManager;
using System;
using System.Drawing;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace Shopping
{
    public partial class LoginForm : Form
    {
        public LoginForm()
        {
            InitializeComponent();
            CustomizeUI(); // Apply custom UI styles
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                string email = emailtxt.Text.Trim();
                string password = passwordtxt.Text.Trim();

                if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password))
                {
                    MessageBox.Show("Email and Password are required!", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (!IsValidEmail(email))
                {
                    MessageBox.Show("Invalid email format! Please enter a valid email.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                User userlogin = UserManager.login(email, password);
                if (userlogin != null)
                {
                    MessageBox.Show("Login successful!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.Hide();

                    // Pass UserID to the next form
                    User u = UserManager.getuserbyemail(email);
                    if (u.UserType == "Admin")
                    {
                        Admin a = new Admin();
                        a.Show();
                    }
                    else
                    {
                        AllProduct allProduct = new AllProduct(u.UserID);
                        allProduct.Show();
                    }
                }
                else
                {
                    MessageBox.Show("This email is not registered or is inactive.", "Login Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}", "Exception", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private bool IsValidEmail(string email)
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
            passwordtxt.PasswordChar = '*';
        }

        private void CustomizeUI()
        {
            this.Text = "User Login";
            this.BackColor = Color.WhiteSmoke;
            this.StartPosition = FormStartPosition.CenterScreen;
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.Font = new Font("Arial", 10);
            this.MaximizeBox = false;

            // TextBox Styles
            CustomizeTextBox(emailtxt, "Enter your email");
            CustomizeTextBox(passwordtxt, "Enter your password");

            // Password field styling
            passwordtxt.PasswordChar = '*';

            // Style Button
            button1.Text = "Login";
            button1.Font = new Font("Arial", 12, FontStyle.Bold);
            button1.BackColor = Color.DarkBlue;
            button1.ForeColor = Color.White;
            button1.FlatStyle = FlatStyle.Flat;
            button1.FlatAppearance.BorderSize = 0;
            button1.Cursor = Cursors.Hand;
            button1.AutoSize = true;
        }

        private void CustomizeTextBox(TextBox txtBox, string placeholder)
        {
            txtBox.Font = new Font("Arial", 10);
            txtBox.ForeColor = Color.Gray;
            txtBox.Text = placeholder;
            txtBox.GotFocus += (s, e) =>
            {
                if (txtBox.Text == placeholder)
                {
                    txtBox.Text = "";
                    txtBox.ForeColor = Color.Black;
                }
            };
            txtBox.LostFocus += (s, e) =>
            {
                if (string.IsNullOrWhiteSpace(txtBox.Text))
                {
                    txtBox.Text = placeholder;
                    txtBox.ForeColor = Color.Gray;
                }
            };
            txtBox.BorderStyle = BorderStyle.FixedSingle;
        }
    }
}
