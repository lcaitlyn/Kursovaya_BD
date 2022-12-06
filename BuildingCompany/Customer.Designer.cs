
namespace BuildingCompany
{
    partial class Customer
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Customer));
            this.label4 = new System.Windows.Forms.Label();
            this.customersEmailTextBox = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.customersPhoneTextBox = new System.Windows.Forms.TextBox();
            this.addCustomer = new System.Windows.Forms.Button();
            this.customersNameTextBox = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(8, 77);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(53, 20);
            this.label4.TabIndex = 14;
            this.label4.Text = "E-mail";
            // 
            // customersEmailTextBox
            // 
            this.customersEmailTextBox.Location = new System.Drawing.Point(97, 77);
            this.customersEmailTextBox.Name = "customersEmailTextBox";
            this.customersEmailTextBox.Size = new System.Drawing.Size(167, 20);
            this.customersEmailTextBox.TabIndex = 13;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(9, 25);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(47, 20);
            this.label2.TabIndex = 10;
            this.label2.Text = "ФИО";
            // 
            // customersPhoneTextBox
            // 
            this.customersPhoneTextBox.Location = new System.Drawing.Point(97, 51);
            this.customersPhoneTextBox.Name = "customersPhoneTextBox";
            this.customersPhoneTextBox.Size = new System.Drawing.Size(167, 20);
            this.customersPhoneTextBox.TabIndex = 9;
            // 
            // addCustomer
            // 
            this.addCustomer.Location = new System.Drawing.Point(51, 131);
            this.addCustomer.Name = "addCustomer";
            this.addCustomer.Size = new System.Drawing.Size(171, 40);
            this.addCustomer.TabIndex = 8;
            this.addCustomer.Text = "Добавить";
            this.addCustomer.UseVisualStyleBackColor = true;
            this.addCustomer.Click += new System.EventHandler(this.addCustomer_Click);
            // 
            // customersNameTextBox
            // 
            this.customersNameTextBox.Location = new System.Drawing.Point(97, 25);
            this.customersNameTextBox.Name = "customersNameTextBox";
            this.customersNameTextBox.Size = new System.Drawing.Size(167, 20);
            this.customersNameTextBox.TabIndex = 11;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(7, 51);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(79, 20);
            this.label3.TabIndex = 12;
            this.label3.Text = "Телефон";
            // 
            // Customer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(271, 189);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.customersEmailTextBox);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.customersPhoneTextBox);
            this.Controls.Add(this.addCustomer);
            this.Controls.Add(this.customersNameTextBox);
            this.Controls.Add(this.label3);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Customer";
            this.Text = "New Customer";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox customersEmailTextBox;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox customersPhoneTextBox;
        private System.Windows.Forms.Button addCustomer;
        private System.Windows.Forms.TextBox customersNameTextBox;
        private System.Windows.Forms.Label label3;
    }
}