using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using InsuranceCalculator.DataAccessLayer;
using InsuranceCalculator.HelperClasses;
using System.IO;
using System.Globalization;
namespace InsuranceCalculator
{
    public class CommonFunctions
    {
        public static string GetMessageCaption()
        {
            return "Pet Insurance Calculator";
        }
        public static void ShowInformationMessage(string message)
        {
            MessageBox.Show(message, GetMessageCaption(), MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        public static void ShowErrorMessage(string message)
        {
            MessageBox.Show(message, GetMessageCaption(), MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
        public static UserData GetAdminUserData()
        {
            UserData userData = new UserData();
            userData.LoginId = Constants.VijayAdmin;
            userData.FirstName = "Vijay";
            userData.LastName = "Singh";
            userData.Address = string.Empty;
            userData.IsAdmin = true;
            userData.Phone = string.Empty;
            userData.Password = Constants.VijayPassword;

            return userData;
        }
        public static string GetTempPath()
        {
            return Path.GetTempPath() + @"pic";
        }
        public static double GetDoubleSafely(string value, double defaultValue = 0)
        {
            if (double.TryParse(value, out double returnValue))
                return returnValue;

            return defaultValue;
        }

        public static bool IfEquals(string val1, string val2)
        {
            var cultureInfo = new System.Globalization.CultureInfo("en-US");
            bool b1 = double.TryParse(val1, NumberStyles.Integer | 
                                            NumberStyles.Float | 
                                            NumberStyles.AllowThousands | 
                                            NumberStyles.AllowCurrencySymbol | 
                                            NumberStyles.AllowDecimalPoint | 
                                            NumberStyles.AllowThousands | 
                                            NumberStyles.Currency, cultureInfo, out double result1);

            bool b2 = double.TryParse(val2, NumberStyles.Integer |
                                NumberStyles.Float |
                                NumberStyles.AllowThousands |
                                NumberStyles.AllowCurrencySymbol |
                                NumberStyles.AllowDecimalPoint |
                                NumberStyles.AllowThousands |
                                NumberStyles.Currency, cultureInfo, out double result2);

            if(b1 && b2)
            {
                return (result1 == result2);
            }
            else
            {
                return val1.IsStringEqual(val2);
            }
        }
    }
}
