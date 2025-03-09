namespace Shopping
{
    partial class AddProduct
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            label1 = new Label();
            label3 = new Label();
            label4 = new Label();
            label5 = new Label();
            nametxt = new TextBox();
            desctxt = new TextBox();
            stocknum = new NumericUpDown();
            pricenum = new NumericUpDown();
            button1 = new Button();
            button2 = new Button();
            ((System.ComponentModel.ISupportInitialize)stocknum).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pricenum).BeginInit();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(97, 70);
            label1.Name = "label1";
            label1.Size = new Size(106, 20);
            label1.TabIndex = 0;
            label1.Text = "Product_Name";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(97, 198);
            label3.Name = "label3";
            label3.Size = new Size(102, 20);
            label3.TabIndex = 2;
            label3.Text = "Product_Stock";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(97, 136);
            label4.Name = "label4";
            label4.Size = new Size(98, 20);
            label4.TabIndex = 3;
            label4.Text = "Product_Price";
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(97, 267);
            label5.Name = "label5";
            label5.Size = new Size(142, 20);
            label5.TabIndex = 4;
            label5.Text = "Product_Description";
            // 
            // nametxt
            // 
            nametxt.Location = new Point(245, 70);
            nametxt.Multiline = true;
            nametxt.Name = "nametxt";
            nametxt.Size = new Size(232, 30);
            nametxt.TabIndex = 5;
            // 
            // desctxt
            // 
            desctxt.Location = new Point(245, 267);
            desctxt.Multiline = true;
            desctxt.Name = "desctxt";
            desctxt.Size = new Size(374, 128);
            desctxt.TabIndex = 6;
            // 
            // stocknum
            // 
            stocknum.Location = new Point(245, 198);
            stocknum.Name = "stocknum";
            stocknum.Size = new Size(233, 27);
            stocknum.TabIndex = 7;
            // 
            // pricenum
            // 
            pricenum.Location = new Point(244, 144);
            pricenum.Name = "pricenum";
            pricenum.Size = new Size(234, 27);
            pricenum.TabIndex = 8;
            // 
            // button1
            // 
            button1.Location = new Point(32, 335);
            button1.Name = "button1";
            button1.Size = new Size(134, 60);
            button1.TabIndex = 9;
            button1.Text = "Add";
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_Click;
            // 
            // button2
            // 
            button2.Location = new Point(694, 399);
            button2.Name = "button2";
            button2.Size = new Size(94, 29);
            button2.TabIndex = 10;
            button2.Text = "Exit";
            button2.UseVisualStyleBackColor = true;
            // 
            // AddProduct
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(button2);
            Controls.Add(button1);
            Controls.Add(pricenum);
            Controls.Add(stocknum);
            Controls.Add(desctxt);
            Controls.Add(nametxt);
            Controls.Add(label5);
            Controls.Add(label4);
            Controls.Add(label3);
            Controls.Add(label1);
            Name = "AddProduct";
            Text = "AddProduct";
            Load += AddProduct_Load;
            ((System.ComponentModel.ISupportInitialize)stocknum).EndInit();
            ((System.ComponentModel.ISupportInitialize)pricenum).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label1;
        private Label label3;
        private Label label4;
        private Label label5;
        private TextBox nametxt;
        private TextBox desctxt;
        private NumericUpDown stocknum;
        private NumericUpDown pricenum;
        private Button button1;
        private Button button2;
    }
}