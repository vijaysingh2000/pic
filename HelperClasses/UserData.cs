using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;
namespace InsuranceCalculator.HelperClasses
{
    public class UserData
    {
        public UserData()
        {
            this.LoginId = string.Empty;
            this.Password = string.Empty;
            this.FirstName = string.Empty;
            this.LastName = string.Empty;
            this.EmailId = string.Empty;
            this.Address = string.Empty;
            this.Phone = string.Empty;
            this.IsAdmin = false;
        }

        public string LoginId { get; set; }
        public string Password { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string EmailId { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public bool IsAdmin { get; set; }

        public void Deserialize(XElement userMetaData)
        {
            this.LoginId = userMetaData.GetAttribute("loginid");
            this.Password = userMetaData.GetAttribute("password");
            this.FirstName = userMetaData.GetAttribute("firstname");
            this.LastName = userMetaData.GetAttribute("lastname");
            this.Address = userMetaData.GetAttribute("address");
            this.EmailId = userMetaData.GetAttribute("email");
            this.Phone = userMetaData.GetAttribute("phone");
        }
        public XElement Serialize()
        {
            XElement returnData = new XElement("userdata");
            returnData.SetAttributeValue("loginid", this.LoginId);
            returnData.SetAttributeValue("password", this.Password);
            returnData.SetAttributeValue("firstname", this.FirstName);
            returnData.SetAttributeValue("lastname", this.LastName);
            returnData.SetAttributeValue("address", this.Address);
            returnData.SetAttributeValue("email", this.EmailId);
            returnData.SetAttributeValue("phone", this.Phone);

            return returnData;
        }
    }
}
