using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;
namespace InsuranceCalculator.HelperClasses
{
    public class CompanyData
    {
        public CompanyData()
        {
            this.Code = string.Empty;
            this.Name = string.Empty;
            this.ClassName = string.Empty;
        }
        public string Code { get; set; }
        public string Name { get; set; }
        public string ClassName { get; set; }
        public XElement FactorXml { get; set; }

        public void Deserialize(XElement element)
        {
            this.Code = element.GetAttribute("code");
            this.Name = element.GetAttribute("name");
            this.ClassName = element.GetAttribute("classname");
            this.FactorXml = element.Element("factorsheet");
        }
        public XElement Serialize()
        {
            XElement element = new XElement("company");
            element.SetAttributeValue("code", this.Code);
            element.SetAttributeValue("name", this.Name);
            element.SetAttributeValue("classname", this.ClassName);

            element.Add(FactorXml);

            return element;
        }
    }
}
