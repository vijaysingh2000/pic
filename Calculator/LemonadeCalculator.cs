using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace InsuranceCalculator.Calculator
{
    public class LemonadeCalculator : BaseCalculator
    {
        public override double Calculate(Dictionary<string, string> userParameters, XElement factorDefinition)
        {
            double annualBaseRateFactor = CommonFunctions.GetDoubleSafely(GetValue("annual_base_rate", userParameters, factorDefinition));
            double zipcodeFactor = CommonFunctions.GetDoubleSafely(GetValue("zip_code_factors", userParameters, factorDefinition));
            double breedFactor = CommonFunctions.GetDoubleSafely(GetValue("breed_factors", userParameters, factorDefinition));
            double ageFactor = CommonFunctions.GetDoubleSafely(GetValue("age_factors", userParameters, factorDefinition));
            double coinsuranceFactor = CommonFunctions.GetDoubleSafely(GetValue("Coinsurance_Factors", userParameters, factorDefinition));
            double deductibleFactor = CommonFunctions.GetDoubleSafely(GetValue("Deductible_Factors", userParameters, factorDefinition));
            double prescriptionmedicationFactor = CommonFunctions.GetDoubleSafely(GetValue("Prescription_Medication", userParameters, factorDefinition));
            double vetvisitsFactor = CommonFunctions.GetDoubleSafely(GetValue("Vet_Visits", userParameters, factorDefinition));
            double physicalTherapyFactor = CommonFunctions.GetDoubleSafely(GetValue("Physical_Therapy", userParameters, factorDefinition));
            double strategyPartnerDiscountFactor = CommonFunctions.GetDoubleSafely(GetValue("Strategy_Partner_Discount", userParameters, factorDefinition));
            double annualPaymentDiscountFactor = CommonFunctions.GetDoubleSafely(GetValue("Annual_Payment_Discount", userParameters, factorDefinition));
            double newEntrantDiscountFactor = CommonFunctions.GetDoubleSafely(GetValue("New_Entrant_Factor", userParameters, factorDefinition));
            double affinityGroupFactor = CommonFunctions.GetDoubleSafely(GetValue("Affinity_Group_Discount", userParameters, factorDefinition));
            double minimumAnnualPremiumFactor = CommonFunctions.GetDoubleSafely(GetValue("Minimum_Annual_Premium", userParameters, factorDefinition));
            double preventativeCarePremiumFactor = CommonFunctions.GetDoubleSafely(GetValue("Preventative_Care_Premium", userParameters, factorDefinition));

            double basePremium = Math.Round(annualBaseRateFactor * zipcodeFactor * breedFactor * ageFactor * coinsuranceFactor * deductibleFactor * prescriptionmedicationFactor, 0);
            double adjustedbasePremium = Math.Round(basePremium * vetvisitsFactor * physicalTherapyFactor, 0);
            adjustedbasePremium = adjustedbasePremium + Math.Round(adjustedbasePremium * strategyPartnerDiscountFactor * annualPaymentDiscountFactor * newEntrantDiscountFactor * affinityGroupFactor, 0);
            adjustedbasePremium = adjustedbasePremium + minimumAnnualPremiumFactor + preventativeCarePremiumFactor;

            return adjustedbasePremium;
        }
        public string GetValue(string factorKey, Dictionary<string, string> userParameters, XElement factorDefinition)
        {
            XElement factorElement = factorDefinition.Elements("parameter").FirstOrDefault(x => x.GetAttribute("name").IsStringEqual(factorKey));

            if (factorElement == null) return string.Empty;

            IEnumerable<XElement> items = factorElement.Elements("item");

            foreach (XElement item in items)
            {
                bool matched = true;
                string factor = string.Empty;

                foreach (XAttribute attr in item.Attributes())
                {
                    string attrKey = attr.Name.LocalName;
                    string attrValue = attr.Value;

                    if (attrKey.IsStringEqual("factor"))
                    {
                        factor = attrValue;
                        continue;
                    }

                    if (!userParameters.ContainsKey(attrKey)) continue;

                    string compareValue = userParameters[attrKey];
                    if (!CommonFunctions.IfEquals(compareValue, attrValue))
                    {
                        matched = false;
                        break;
                    }
                }

                if (matched)
                    return factor;
            }

            return "";
        }
    }
}
