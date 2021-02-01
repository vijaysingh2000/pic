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
    public partial class User : Form
    {
        private bool _newUser = false;

        public User(bool newUser)
        {
            InitializeComponent();

            this._newUser = newUser;
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void User_Load(object sender, EventArgs e)
        {
            this.Text = CommonFunctions.GetMessageCaption();
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            UserData userData = DataAcessLayer.GetUserData(txtLoginId.Text);

            if (_newUser)
            {
                if (userData != null)
                {
                    CommonFunctions.ShowErrorMessage("User with similar login already exists !!! Try different login Id !!!");
                    return;
                }

                userData = new UserData();
            }

            if (!txtConfirmPassword.Text.IsStringEqual(txtPassword.Text))
            {
                CommonFunctions.ShowErrorMessage("Password didn't match !!!");
                return;
            }

            userData.LoginId = txtLoginId.Text;
            userData.FirstName = txtFirstName.Text;
            userData.LastName = txtLastName.Text;
            userData.Password = txtPassword.Text;
            userData.Phone = txtPhone.Text;
            userData.EmailId = txtEmailAddress.Text;
            userData.Address = txtAddress.Text;

            bool updateStatus = DataAcessLayer.UpdateUserData(userData);

            if(updateStatus)
            {
                CommonFunctions.ShowInformationMessage("Update successful !!!");
                this.Close();
            }
        }
    }
}
