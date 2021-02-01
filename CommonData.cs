using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InsuranceCalculator.HelperClasses;
namespace InsuranceCalculator
{
    public class CommonData
    {
        public static UserData LoggedInUser;
        public static List<CompanyData> Companies;
        public static List<UserData> Users;
    }

    public class Constants
    {
        public const string VijayAdmin = "vijayadmin";
        public const string VijayPassword = "Thts92618!";
    }
}
