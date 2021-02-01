namespace InsuranceCalculator
{
    partial class Main
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
            this.lblChooseInputFile = new System.Windows.Forms.Label();
            this.txtFileName = new System.Windows.Forms.TextBox();
            this.btnBrowse = new System.Windows.Forms.Button();
            this.lblChooseCompany = new System.Windows.Forms.Label();
            this.cmbCompanies = new System.Windows.Forms.ComboBox();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.btnCalculate = new System.Windows.Forms.Button();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.lblProgress = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.txtSampleData = new System.Windows.Forms.TextBox();
            this.btnDownloadSampleInputSheet = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // lblChooseInputFile
            // 
            this.lblChooseInputFile.AutoSize = true;
            this.lblChooseInputFile.Location = new System.Drawing.Point(26, 177);
            this.lblChooseInputFile.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblChooseInputFile.Name = "lblChooseInputFile";
            this.lblChooseInputFile.Size = new System.Drawing.Size(61, 16);
            this.lblChooseInputFile.TabIndex = 0;
            this.lblChooseInputFile.Text = "Input File";
            // 
            // txtFileName
            // 
            this.txtFileName.Location = new System.Drawing.Point(26, 200);
            this.txtFileName.Margin = new System.Windows.Forms.Padding(4);
            this.txtFileName.Name = "txtFileName";
            this.txtFileName.Size = new System.Drawing.Size(767, 22);
            this.txtFileName.TabIndex = 1;
            // 
            // btnBrowse
            // 
            this.btnBrowse.Location = new System.Drawing.Point(802, 199);
            this.btnBrowse.Margin = new System.Windows.Forms.Padding(4);
            this.btnBrowse.Name = "btnBrowse";
            this.btnBrowse.Size = new System.Drawing.Size(40, 28);
            this.btnBrowse.TabIndex = 2;
            this.btnBrowse.Text = "...";
            this.btnBrowse.UseVisualStyleBackColor = true;
            this.btnBrowse.Click += new System.EventHandler(this.btnBrowse_Click);
            // 
            // lblChooseCompany
            // 
            this.lblChooseCompany.AutoSize = true;
            this.lblChooseCompany.Location = new System.Drawing.Point(24, 69);
            this.lblChooseCompany.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblChooseCompany.Name = "lblChooseCompany";
            this.lblChooseCompany.Size = new System.Drawing.Size(66, 16);
            this.lblChooseCompany.TabIndex = 3;
            this.lblChooseCompany.Text = "Company";
            // 
            // cmbCompanies
            // 
            this.cmbCompanies.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbCompanies.FormattingEnabled = true;
            this.cmbCompanies.Location = new System.Drawing.Point(24, 89);
            this.cmbCompanies.Margin = new System.Windows.Forms.Padding(4);
            this.cmbCompanies.Name = "cmbCompanies";
            this.cmbCompanies.Size = new System.Drawing.Size(767, 24);
            this.cmbCompanies.TabIndex = 4;
            this.cmbCompanies.SelectedIndexChanged += new System.EventHandler(this.cmbCompanies_SelectedIndexChanged);
            // 
            // progressBar1
            // 
            this.progressBar1.Location = new System.Drawing.Point(24, 317);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(767, 23);
            this.progressBar1.TabIndex = 5;
            // 
            // btnCalculate
            // 
            this.btnCalculate.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCalculate.Location = new System.Drawing.Point(701, 234);
            this.btnCalculate.Name = "btnCalculate";
            this.btnCalculate.Size = new System.Drawing.Size(89, 61);
            this.btnCalculate.TabIndex = 6;
            this.btnCalculate.Text = "Calculate";
            this.btnCalculate.UseVisualStyleBackColor = true;
            this.btnCalculate.Click += new System.EventHandler(this.btnCalculate_Click);
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // lblProgress
            // 
            this.lblProgress.AutoSize = true;
            this.lblProgress.Location = new System.Drawing.Point(24, 298);
            this.lblProgress.Name = "lblProgress";
            this.lblProgress.Size = new System.Drawing.Size(63, 16);
            this.lblProgress.TabIndex = 7;
            this.lblProgress.Text = "Progress";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.Blue;
            this.label1.Location = new System.Drawing.Point(254, 24);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(271, 25);
            this.label1.TabIndex = 8;
            this.label1.Text = "Pet Insurance Calculator";
            // 
            // txtSampleData
            // 
            this.txtSampleData.BackColor = System.Drawing.SystemColors.Info;
            this.txtSampleData.Location = new System.Drawing.Point(79, 361);
            this.txtSampleData.Multiline = true;
            this.txtSampleData.Name = "txtSampleData";
            this.txtSampleData.ScrollBars = System.Windows.Forms.ScrollBars.Horizontal;
            this.txtSampleData.Size = new System.Drawing.Size(636, 21);
            this.txtSampleData.TabIndex = 9;
            this.txtSampleData.Visible = false;
            // 
            // btnDownloadSampleInputSheet
            // 
            this.btnDownloadSampleInputSheet.Location = new System.Drawing.Point(570, 120);
            this.btnDownloadSampleInputSheet.Name = "btnDownloadSampleInputSheet";
            this.btnDownloadSampleInputSheet.Size = new System.Drawing.Size(223, 28);
            this.btnDownloadSampleInputSheet.TabIndex = 10;
            this.btnDownloadSampleInputSheet.Text = "Download Sample Input Sheet";
            this.btnDownloadSampleInputSheet.UseVisualStyleBackColor = true;
            this.btnDownloadSampleInputSheet.Click += new System.EventHandler(this.btnDownloadSampleInputSheet_Click);
            // 
            // Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.ClientSize = new System.Drawing.Size(855, 358);
            this.Controls.Add(this.btnDownloadSampleInputSheet);
            this.Controls.Add(this.txtSampleData);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.lblProgress);
            this.Controls.Add(this.btnCalculate);
            this.Controls.Add(this.progressBar1);
            this.Controls.Add(this.cmbCompanies);
            this.Controls.Add(this.lblChooseCompany);
            this.Controls.Add(this.btnBrowse);
            this.Controls.Add(this.txtFileName);
            this.Controls.Add(this.lblChooseInputFile);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Main";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Pet Insurance Calculator";
            this.Load += new System.EventHandler(this.Main_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblChooseInputFile;
        private System.Windows.Forms.TextBox txtFileName;
        private System.Windows.Forms.Button btnBrowse;
        private System.Windows.Forms.Label lblChooseCompany;
        private System.Windows.Forms.ComboBox cmbCompanies;
        private System.Windows.Forms.ProgressBar progressBar1;
        private System.Windows.Forms.Button btnCalculate;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.Label lblProgress;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtSampleData;
        private System.Windows.Forms.Button btnDownloadSampleInputSheet;
    }
}

