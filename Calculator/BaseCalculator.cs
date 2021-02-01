using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.CodeDom;
using System.CodeDom.Compiler;
using Microsoft.CSharp;
using System.Xml.Linq;
//using Z.Expressions;
namespace InsuranceCalculator.Calculator
{
    public class BaseCalculator
    {
        public virtual double GetFactor(string parameter, string parameterValue, XElement factorDefinition)
        {
            XElement parameterElement = factorDefinition.Elements("parameter").FirstOrDefault(x => x.GetAttribute("name").IsStringEqual(parameter));

            if (parameterElement != null)
            {
                XElement parameterElementFactor = parameterElement.Elements("item").FirstOrDefault(x => x.GetAttribute("value").IsStringEqual("*") || x.GetAttribute("value").IsStringEqual(parameterValue));

                if (double.TryParse(parameterElementFactor.GetAttribute("factor"), out double returnVal))
                    return returnVal;
            }

            return 0;
        }
        public virtual double Calculate(Dictionary<string, string> userParameters, XElement factorDefinition)
        {
            string formula = factorDefinition.Element("formula").Value;
            IEnumerable<XElement> parameterElements = factorDefinition.Elements("parameter");
            foreach (XElement parameterElement in parameterElements)
            {
                string parameterName = parameterElement.GetAttribute("name").Trim().ToUpper();
                IEnumerable<XElement> items = parameterElement.Elements("item");
                string factorValue = string.Empty;

                if (userParameters.ContainsKey(parameterName))
                {
                    string valueToLookFor = userParameters[parameterName];
                    XElement item = items.FirstOrDefault(x => x.GetAttribute("value").IsStringEqual(valueToLookFor));
                    factorValue = item.GetAttribute("factor");
                }
                else
                {
                    XElement item = items.FirstOrDefault();
                    factorValue = item.GetAttribute("factor");
                }

                parameterName = parameterName.Replace(" ", "_");
                if (double.TryParse(factorValue, out double factorValueN))
                    formula = formula.Replace(parameterName, factorValueN.ToString());
                else
                    formula = formula.Replace(parameterName, "0");
            }

            return 0;
            //return Eval.Execute<double>(formula);
        }
    }
}
