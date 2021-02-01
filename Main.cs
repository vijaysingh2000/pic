using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Xml.Linq;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.CodeDom.Compiler;
using Microsoft.CSharp;
using InsuranceCalculator.DataAccessLayer;
using InsuranceCalculator.HelperClasses;
namespace InsuranceCalculator
{
    public partial class Main : Form
    {
        public Main()
        {
            InitializeComponent();
        }

        private void btnBrowse_Click(object sender, EventArgs e)
        {
            DialogResult dialogResult = openFileDialog1.ShowDialog();
            if (dialogResult == DialogResult.OK)
                txtFileName.Text = openFileDialog1.FileName;
        }

        private void Main_Load(object sender, EventArgs e)
        {
            List<CompanyData> companies = DataAcessLayer.GetCompanies();
            cmbCompanies.DataSource = companies;
            cmbCompanies.DisplayMember = "Name";
        }

        private void btnCalculate_Click(object sender, EventArgs e)
        {
            Processor processor = new Processor(this.txtFileName.Text, cmbCompanies.SelectedItem as CompanyData, progressAction, completedAction);
            processor.StartAsync();
        }

        private void progressAction(ProgressChangedEventArgs e)
        {
            if (e.ProgressPercentage == 0)
            {
                progressBar1.Minimum = 1;
                progressBar1.Maximum = int.Parse(e.UserState.ToString());
                progressBar1.Increment(1);
            }
            else
            {
                lblProgress.Text = e.UserState.ToString() + "...";
                progressBar1.Increment(1);
            }
        }
        private void completedAction(RunWorkerCompletedEventArgs e, string fileName)
        {
            lblProgress.Text = "Operation Complete.";
            progressBar1.Value = progressBar1.Minimum;

            MessageBox.Show("Operation Complete." + Environment.NewLine + fileName);
        }

        private void cmbCompanies_SelectedIndexChanged(object sender, EventArgs e)
        {
            /*
            txtSampleData.Text = string.Empty;

            if (cmbCompanies.SelectedItem as CompanyData == null)
                return;

            XElement element = Factory.GetCompanyMetaData(cmbCompanies.SelectedItem as CompanyData);
            IEnumerable<XElement> items = element.Elements("parameter");

            foreach (XElement item in items)
                txtSampleData.Text += item.GetAttribute("name") + "," + Environment.NewLine;
            */
        }

        private void btnDownloadSampleInputSheet_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog folderBrowserDialog = new FolderBrowserDialog();
            if(folderBrowserDialog.ShowDialog() == DialogResult.OK)
            {
                CompanyData cdata = cmbCompanies.SelectedItem as CompanyData;

                DataAcessLayer.DownloadInputSheet(cdata.Code, folderBrowserDialog.SelectedPath);

                CommonFunctions.ShowInformationMessage($"Sample Input sheet download to {folderBrowserDialog.SelectedPath}\\{cdata.Code}.csv");
            }
        }
    }
}
