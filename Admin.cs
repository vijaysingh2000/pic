using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Xml.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using InsuranceCalculator.DataAccessLayer;
using InsuranceCalculator.HelperClasses;
namespace InsuranceCalculator
{
    public partial class Admin : Form
    {
        public Admin()
        {
            InitializeComponent();
        }

        private void radAllCompanies_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (radAllCompanies.Checked)
                {
                    XElement companies = DataAcessLayer.GetCompaniesXml();
                    txtXml.Text = companies.ToString();
                }
            }
            catch (Exception ex)
            {
                txtXml.Text = string.Empty;
                CommonFunctions.ShowErrorMessage("No companies exists yet !!!");
            }
        }

        private void radAllUsers_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (radAllUsers.Checked)
                {
                    XElement users = DataAcessLayer.GetUsersXml();
                    txtXml.Text = users.ToString();
                }
            }
            catch (Exception ex)
            {
                txtXml.Text = string.Empty;
                CommonFunctions.ShowErrorMessage("No users exists yet !!!");
            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if(string.IsNullOrEmpty(txtXml.Text))
            {
                CommonFunctions.ShowErrorMessage("No xml exists !!!");
            }

            XElement updateXml = XElement.Parse(txtXml.Text);

            bool result = false;
            if (radAllCompanies.Checked)
                result = DataAcessLayer.UpdateCompanies(updateXml);
            else if (radAllUsers.Checked)
                result = DataAcessLayer.UpdateUsers(updateXml);

            if (result)
                CommonFunctions.ShowInformationMessage("Update successful !!!");
        }

        private void btnNewCompany_Click(object sender, EventArgs e)
        {
            Company company = new Company();
            company.ShowDialog();
        }

        private void Admin_Load(object sender, EventArgs e)
        {
            this.Text = CommonFunctions.GetMessageCaption();
        }
    }
}
