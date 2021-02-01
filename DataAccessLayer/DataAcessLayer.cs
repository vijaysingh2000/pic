using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using System.Windows.Forms;
using System.Net;
using InsuranceCalculator.HelperClasses;
namespace InsuranceCalculator.DataAccessLayer
{
    public class DataAcessLayer
    {
        public static XElement GetCompaniesXml()
        {
            XElement companiesXml = RemoteAccess.GetXmlFromServer("companies.xml");
            return companiesXml;
        }
        public static XElement GetUsersXml()
        {
            XElement usersXml = RemoteAccess.GetXmlFromServer("users.xml");
            return usersXml;
        }
        public static List<CompanyData> GetCompanies()
        {
            List<CompanyData> companies = new List<CompanyData>();

            try
            {
                XElement companiesXml = RemoteAccess.GetXmlFromServer("companies.xml");

                IEnumerable<XElement> companiesElements = companiesXml.Elements("company");

                foreach (XElement companyElement in companiesElements)
                {
                    CompanyData company = new CompanyData();
                    company.Deserialize(companyElement);
                    if (string.IsNullOrEmpty(company.ClassName))
                        company.ClassName = "BaseCalculator";

                    companies.Add(company);
                }
            }
            catch
            {

            }

            return companies;
        }
        public static List<UserData> GetUsers()
        {
            List<UserData> users = new List<UserData>();

            try
            {
                XElement userXml = RemoteAccess.GetXmlFromServer("users.xml");

                IEnumerable<XElement> userElements = userXml.Elements("user");

                foreach (XElement userElement in userElements)
                {
                    UserData user = new UserData();
                    user.LoginId = userElement.GetAttribute("loginid");
                    user.Password = userElement.GetAttribute("password");
                    user.FirstName = userElement.GetAttribute("firstname");
                    user.LastName = userElement.GetAttribute("lastname");
                    user.Address = userElement.GetAttribute("address");
                    user.Phone = userElement.GetAttribute("phone");
                }
            }
            catch
            {
            }

            return users;
        }
        public static CompanyData GetCompanyData(string companyCode)
        {
            try
            {
                XElement companymetadata = RemoteAccess.GetXmlFromServer("companies", $"{companyCode}.xml");

                CompanyData companyData = new CompanyData();
                companyData.Deserialize(companymetadata);

                return companyData;
            }
            catch
            {
                return null;
            }
        }
        public static UserData GetUserData(string loginId)
        {
            try
            {
                XElement userMetaData = RemoteAccess.GetXmlFromServer("users", $"{loginId}.xml");

                UserData user = new UserData();
                user.Deserialize(userMetaData);

                return user;
            }
            catch
            {
                return null;
            }
        }
        public static void DownloadInputSheet(string companyCode, string targetFolder)
        {
            string newFileName = $"{companyCode}.csv";
            newFileName = RemoteAccess.GetFileFromServer("sampleinputsheets", newFileName);

            string targetFileName = Path.GetFileName(newFileName);
            targetFileName = Path.Combine(targetFolder, targetFileName);

            File.Copy(newFileName, targetFileName, true);
        }
        public static bool UpdateUserData(UserData userData)
        {
            try
            {
                XElement xmlUserData = userData.Serialize();
                string filePath = Path.Combine(CommonFunctions.GetTempPath(), $"{userData.LoginId}.xml");
                xmlUserData.Save(filePath);

                RemoteAccess.PutToServer("users", filePath);

                XElement users;
                try 
                {
                    users = RemoteAccess.GetXmlFromServer("users.xml");
                }
                catch
                {
                    users = new XElement("users");
                }
                
                XElement user = users.Elements().FirstOrDefault(x => x.GetAttribute("loginid").IsStringEqual(userData.LoginId));
                if (user != null)
                    user.Remove();

                users.Add(xmlUserData);

                filePath = Path.Combine(CommonFunctions.GetTempPath(), "users.xml");
                users.Save(filePath);

                RemoteAccess.PutToServer(filePath);

                return true;
            }
            catch
            {
                return false;
            }
        }
        public static bool UpdateCompanyData(CompanyData companyData)
        {
            try
            {
                XElement xmlCompanyData = companyData.Serialize();
                string filePath = Path.Combine(CommonFunctions.GetTempPath(), $"{companyData.Code}.xml");
                xmlCompanyData.Save(filePath);

                RemoteAccess.PutToServer("companies", filePath);

                XElement companies;
                try
                {
                    companies = RemoteAccess.GetXmlFromServer("companies.xml");
                }
                catch
                {
                    companies = new XElement("companies");
                }

                XElement company = companies.Elements().FirstOrDefault(x => x.GetAttribute("code").IsStringEqual(companyData.Code));
                if (company != null)
                    company.Remove();

                XElement factorSheet = xmlCompanyData.Element("factorsheet");
                if (factorSheet != null)
                    factorSheet.Remove();

                companies.Add(xmlCompanyData);

                filePath = Path.Combine(CommonFunctions.GetTempPath(), "companies.xml");
                companies.Save(filePath);

                RemoteAccess.PutToServer(filePath);

                return true;
            }
            catch
            {
                return false;
            }
        }
        public static bool UpdateInputSheet(string companyCode, string fileName)
        {
            string newFileName = $"{companyCode}.csv";
            RemoteAccess.PutToServerWithNewName("sampleinputsheets", fileName, newFileName);

            return true;
        }
        public static bool UpdateCompanies(XElement companies)
        {
            string filePath = Path.Combine(CommonFunctions.GetTempPath(), "companies.xml");
            companies.Save(filePath);

            RemoteAccess.PutToServer(filePath);

            return true;
        }
        public static bool UpdateUsers(XElement users)
        {
            string filePath = Path.Combine(CommonFunctions.GetTempPath(), "users.xml");
            users.Save(filePath);

            RemoteAccess.PutToServer(filePath);

            return true;
        }
        public static bool DoesUserExists(string loginId)
        {
            try
            {
                XElement userMetaData = RemoteAccess.GetXmlFromServer("users", $"{loginId}.xml");
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
