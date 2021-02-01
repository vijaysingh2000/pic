using InsuranceCalculator.Calculator;
using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InsuranceCalculator.HelperClasses;
using System.Reflection;
using System.Xml.Linq;
using InsuranceCalculator.DataAccessLayer;

namespace InsuranceCalculator
{
    public class Factory
    {
        private static Dictionary<string, BaseCalculator> _companyCalculator = new Dictionary<string, BaseCalculator>();
        private static Dictionary<string, XElement> _companyMetaData = new Dictionary<string, XElement>();

        public static BaseCalculator GetCompanyCalculator(CompanyData company)
        {
            if (_companyCalculator.TryGetValue(company.Code.Trim().ToUpper(), out BaseCalculator baseCalculator) && baseCalculator != null)
            {
                return baseCalculator;
            }

            Type[] types = Assembly.GetExecutingAssembly().GetTypes();

            Type type = types.FirstOrDefault(x => x.Name.IsStringEqual(company.ClassName));

            if(type != null)
            {
                object providerClass = Activator.CreateInstance(type);

                if (providerClass as BaseCalculator != null)
                {
                    BaseCalculator returnItem = providerClass as BaseCalculator;

                    if(_companyCalculator.ContainsKey(company.Code.Trim().ToUpper()))
                        _companyCalculator.Remove(company.Code.Trim().ToUpper());

                    _companyCalculator.Add(company.Code.Trim().ToUpper(), returnItem);

                    return returnItem;
                }
            }

            return null;
        }
        public static XElement GetCompanyMetaData(CompanyData company)
        {
            if (_companyMetaData.TryGetValue(company.Code.Trim().ToUpper(), out XElement element) && element != null)
            {
                return element;
            }

            CompanyData companyData = DataAcessLayer.GetCompanyData(company.Code);

            if (_companyMetaData.ContainsKey(company.Code.Trim().ToUpper()))
                _companyMetaData.Remove(company.Code.Trim().ToUpper());

            _companyMetaData.Add(company.Code.Trim().ToUpper(), companyData.FactorXml);

            return companyData.FactorXml;
        }
    }
}
