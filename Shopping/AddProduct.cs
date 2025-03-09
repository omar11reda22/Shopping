using DBL.Entity;
using DBL.entityManager;
using System;
using System.Windows.Forms;

namespace Shopping
{
    public partial class AddProduct : Form
    {
        public AddProduct()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            // Get input values
            string pname = nametxt.Text.Trim();
            string desc = desctxt.Text.Trim();
            decimal price = pricenum.Value;   // NumericUpDown ensures it's a valid number
            int stock = (int)stocknum.Value;  // NumericUpDown ensures it's a valid integer

            // Validation checks
            if (string.IsNullOrWhiteSpace(pname) || string.IsNullOrWhiteSpace(desc))
            {
                MessageBox.Show("Please enter all required fields.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (price <= 0 || stock < 0)
            {
                MessageBox.Show("Price must be greater than 0 and stock cannot be negative.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Check if product already exists
            if (ProductManager.ProductExists(pname))
            {
                MessageBox.Show("This product already exists!", "Duplicate Product", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Create new product object
            Product newProduct = new Product
            {
                Name = pname,
                Description = desc,
                Price = price,
                Stock = stock
            };

            // Attempt to add the product
            bool success = ProductManager.addnewproduct(newProduct);

            if (success)
            {
                MessageBox.Show("Product added successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Close();  // Close the form after successful addition
            }
            else
            {
                MessageBox.Show("Failed to add product. Please try again.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void AddProduct_Load(object sender, EventArgs e)
        {
            // Any initialization logic if needed
        }
    }
}
