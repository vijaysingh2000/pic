
namespace InsuranceCalculator
{
    partial class Admin
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
            this.btnNewCompany = new System.Windows.Forms.Button();
            this.txtXml = new System.Windows.Forms.TextBox();
            this.radAllCompanies = new System.Windows.Forms.RadioButton();
            this.radAllUsers = new System.Windows.Forms.RadioButton();
            this.btnUpdate = new System.Windows.Forms.Button();
            this.btnClose = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnNewCompany
            // 
            this.btnNewCompany.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnNewCompany.Location = new System.Drawing.Point(138, 416);
            this.btnNewCompany.Name = "btnNewCompany";
            this.btnNewCompany.Size = new System.Drawing.Size(244, 33);
            this.btnNewCompany.TabIndex = 0;
            this.btnNewCompany.Text = "&New/Update Company Details";
            this.btnNewCompany.UseVisualStyleBackColor = true;
            this.btnNewCompany.Click += new System.EventHandler(this.btnNewCompany_Click);
            // 
            // txtXml
            // 
            this.txtXml.Location = new System.Drawing.Point(12, 65);
            this.txtXml.Multiline = true;
            this.txtXml.Name = "txtXml";
            this.txtXml.Size = new System.Drawing.Size(979, 335);
            this.txtXml.TabIndex = 1;
            // 
            // radAllCompanies
            // 
            this.radAllCompanies.AutoSize = true;
            this.radAllCompanies.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.radAllCompanies.Location = new System.Drawing.Point(13, 25);
            this.radAllCompanies.Name = "radAllCompanies";
            this.radAllCompanies.Size = new System.Drawing.Size(128, 24);
            this.radAllCompanies.TabIndex = 2;
            this.radAllCompanies.TabStop = true;
            this.radAllCompanies.Text = "All Companies";
            this.radAllCompanies.UseVisualStyleBackColor = true;
            this.radAllCompanies.CheckedChanged += new System.EventHandler(this.radAllCompanies_CheckedChanged);
            // 
            // radAllUsers
            // 
            this.radAllUsers.AutoSize = true;
            this.radAllUsers.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.radAllUsers.Location = new System.Drawing.Point(182, 25);
            this.radAllUsers.Name = "radAllUsers";
            this.radAllUsers.Size = new System.Drawing.Size(90, 24);
            this.radAllUsers.TabIndex = 2;
            this.radAllUsers.TabStop = true;
            this.radAllUsers.Text = "All Users";
            this.radAllUsers.UseVisualStyleBackColor = true;
            this.radAllUsers.CheckedChanged += new System.EventHandler(this.radAllUsers_CheckedChanged);
            // 
            // btnUpdate
            // 
            this.btnUpdate.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnUpdate.Location = new System.Drawing.Point(12, 416);
            this.btnUpdate.Name = "btnUpdate";
            this.btnUpdate.Size = new System.Drawing.Size(111, 33);
            this.btnUpdate.TabIndex = 3;
            this.btnUpdate.Text = "&Update";
            this.btnUpdate.UseVisualStyleBackColor = true;
            this.btnUpdate.Click += new System.EventHandler(this.btnUpdate_Click);
            // 
            // btnClose
            // 
            this.btnClose.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnClose.Location = new System.Drawing.Point(880, 416);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(111, 33);
            this.btnClose.TabIndex = 3;
            this.btnClose.Text = "&Close";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnUpdate_Click);
            // 
            // Admin
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1003, 480);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.btnUpdate);
            this.Controls.Add(this.radAllUsers);
            this.Controls.Add(this.radAllCompanies);
            this.Controls.Add(this.txtXml);
            this.Controls.Add(this.btnNewCompany);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Admin";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Admin";
            this.Load += new System.EventHandler(this.Admin_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button btnNewCompany;
        private System.Windows.Forms.TextBox txtXml;
        private System.Windows.Forms.RadioButton radAllCompanies;
        private System.Windows.Forms.RadioButton radAllUsers;
        private System.Windows.Forms.Button btnUpdate;
        private System.Windows.Forms.Button btnClose;
    }
}