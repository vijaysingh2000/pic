using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using InsuranceCalculator.DataAccessLayer;
using InsuranceCalculator.HelperClasses;
namespace InsuranceCalculator
{
    public partial class Login : Form
    {
        public Login()
        {
            InitializeComponent();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(txtLoginId.Text)) throw new Exception("Login id cannot be blank !!!");

                UserData userData = null;
                if (txtLoginId.Text.IsStringEqual(Constants.VijayAdmin) && txtPassword.Text.IsStringEqual(Constants.VijayPassword))
                {
                    userData = CommonFunctions.GetAdminUserData();
                }
                else
                {
                    userData = DataAcessLayer.GetUserData(txtLoginId.Text);
                }

                if (userData == null) throw new Exception("login id not found.");

                if (!userData.Password.IsStringEqual(txtPassword.Text)) throw new Exception("Incorrect password");

                CommonData.LoggedInUser = userData;
                DialogResult = DialogResult.OK;
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, ex.Message, CommonFunctions.GetMessageCaption(), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void btnSignUp_Click(object sender, EventArgs e)
        {
            User user = new User(true);
            user.ShowDialog();
        }
    }
}
