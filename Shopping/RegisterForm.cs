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

namespace Shopping
{
    public partial class RegisterForm : Form
    {
        public RegisterForm()
        {
            InitializeComponent();
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
                    PasswordHash = password, // Secure password
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
                    // Close the form after successful registration
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
    }
}
