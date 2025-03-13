using DBL.Entity;
using DBL.entityManager;
using System;
using System.Drawing;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace Shopping
{
    public partial class RegisterForm : Form
    {
        public RegisterForm()
        {
            InitializeComponent();
            CustomizeUI(); // Apply custom UI styles
        }

        private void btnRegister_Click(object sender, EventArgs e)
        {
            try
            {
                // Get data from form fields
                string username = usernametxt.Text.Trim();
                string email = emailtxt.Text.Trim();
                string password = passwordtxt.Text.Trim();
                string phone = phonetxt.Text.Trim();
                string address = addresstxt.Text.Trim();

                // Validate required fields
                if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password))
                {
                    MessageBox.Show("Username, Email, and Password are required!", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Validate Email Format
                if (!IsValidEmail(email))
                {
                    MessageBox.Show("Invalid email format! Please enter a valid email.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Create User object
                User newUser = new User
                {
                    UserName = username,
                    Email = email,
                    PasswordHash = password,
                    Phone = string.IsNullOrEmpty(phone) ? null : phone,
                    Address = string.IsNullOrEmpty(address) ? null : address,
                };

                // Call Register method
                bool isRegistered = UserManager.Register(newUser);

                // Check if registration was successful
                if (isRegistered)
                {
                    MessageBox.Show("User registered successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.Close();
                    AllProduct allProduct = new AllProduct(newUser.UserID);
                    allProduct.Show();
                }
                else
                {
                    MessageBox.Show("Failed to register user. Try again!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}", "Exception", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Email validation method
        private bool IsValidEmail(string email)
        {
            string pattern = @"^[^@\s]+@[^@\s]+\.[^@\s]+$";
            return Regex.IsMatch(email, pattern);
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            this.Hide();
            LoginForm lg = new LoginForm();
            lg.Show();
        }

        private void RegisterForm_Load(object sender, EventArgs e)
        {
            passwordtxt.PasswordChar = '*';
        }

        private void CustomizeUI()
        {
            this.Text = "User Registration";
            this.BackColor = Color.WhiteSmoke;
            this.StartPosition = FormStartPosition.CenterScreen;
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.Font = new Font("Arial", 10);
            this.MaximizeBox = false;

            // TextBox Styles
            CustomizeTextBox(usernametxt, "Enter your username");
            CustomizeTextBox(emailtxt, "Enter your email");
            CustomizeTextBox(passwordtxt, "Enter your password");
            CustomizeTextBox(phonetxt, "Enter your phone number (optional)");
            CustomizeTextBox(addresstxt, "Enter your address (optional)");

            // Password field styling
            passwordtxt.PasswordChar = '*';

            // Style Button
            button1.Text = "Register";
            button1.Font = new Font("Arial", 12, FontStyle.Bold);
            button1.BackColor = Color.DarkGreen;
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
