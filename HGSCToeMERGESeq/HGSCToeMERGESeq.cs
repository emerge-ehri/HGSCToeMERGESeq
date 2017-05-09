using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;
using HGSCToeMERGESeq.Models;

namespace HGSCToeMERGESeq
{
    public class HGSCToeMERGESeq
    {
        public class Code
        {
            public string CodeSystem { get; set; }
            public string CodeText { get; set; }
            public string CodeValue { get; set; }
            public string ValueSetAbbr { get; set; }

            public Code(string codeSystem, string codeText, string codeValue, string valueSetAbbr)
            {
                CodeSystem = codeSystem;
                CodeText = codeText;
                CodeValue = codeValue;
                ValueSetAbbr = valueSetAbbr;
            }
        }

        public const string FieldStart = "{{";
        public const string FieldEnd = "}}";

        private static Regex OmimRegex = new Regex("(.*)\\s\\[MIM\\s([\\d]+)\\]");

        public readonly Dictionary<string, Code> Races = new Dictionary<string, Code>()
        {
            {
                "Black or African American",
                new Code("CDC-NEDSS", "Black or African American", "2054-5", "Black or African American")
            }
        };

        public readonly Dictionary<string, Code> Ethnicities = new Dictionary<string, Code>()
        {
            {
                "Not Hispanic or Latino",
                new Code("Baylor", "Not Hispanic or Latino", "Not Hispanic or Latino", "Not Hispanic or Latino")
            }
        };

        public readonly Dictionary<string, Code> SpecimenTypes = new Dictionary<string, Code>()
        {
            {
                "DNA, Isolated",
                new Code("EMERGE-GIS-LOCAL", "DNA, Isolated", "10037-9", "DNA, Isolated")
            }
        };

        public readonly Dictionary<string, Code> Interpretations = new Dictionary<string, Code>()
        {
            {
                "Positive",
                new Code("LN", "Positive", "LA6576-8", "Positive")
            },
            {
                "Negative",
                new Code("LN", "Negative", "LA6577-6", "Negative")
            }
        };

        public readonly Dictionary<string, Code> Sex = new Dictionary<string, Code>()
        {
            {
                "Male",
                new Code("HL7-TABLE-0001", "Male", "M", "Male")
            },
            {
                "Female",
                new Code("HL7-TABLE-0001", "Female", "F", "Female")
            }
        };

        public string EnvelopeTemplate { get; set; }
        public string CodeTemplate { get; set; }
        public string ReportDiseaseTemplate { get; set; }
        public string TemplatePath { get; set; }

        public HGSCToeMERGESeq(string templatePath)
        {
            TemplatePath = templatePath;
            EnvelopeTemplate = File.ReadAllText(Path.Combine(TemplatePath, "Envelope.txt"));
            CodeTemplate = File.ReadAllText(Path.Combine(TemplatePath, "Code.txt"));
            ReportDiseaseTemplate = File.ReadAllText(Path.Combine(TemplatePath, "ReportDisease.txt"));
        }

        private string ReplaceHgscCodeFieldId(string key)
        {
            return ReplaceFieldId(string.Format("Code.HGSC.{0}", key));
        }

        private string ReplaceHgscFunctionFieldId(string key)
        {
            return ReplaceFieldId(string.Format("Fn.HGSC.{0}", key));
        }

        private string ReplaceHgscFieldId(string key)
        {
            return ReplaceFieldId(string.Format("HGSC.{0}", key));
        }

        private string ReplaceFieldId(string key)
        {
            return string.Format("{0}{1}{2}", FieldStart, key, FieldEnd);
        }

        private string ReplaceStringWithPropertyValue(string results, PropertyInfo property, HGSCResult result)
        {
            return results = results.Replace(ReplaceHgscFieldId(property.Name), property.GetValue(result, null).ToString());            
        }

        private Code DiseaseToDiseaseCode(string disease)
        {
            var match = OmimRegex.Match(disease);
            if (match.Success)
            {
                var diseaseCode = new Code("OMIM", match.Groups[1].Value, match.Groups[2].Value, match.Groups[1].Value);
                return diseaseCode;
            }
            
            return null;
        }

        public string Transform(HGSCResult result)
        {
            string results = EnvelopeTemplate;
            results = results.Replace(ReplaceFieldId("CurrentTimestamp"),(DateTime.Now.Ticks / TimeSpan.TicksPerMillisecond).ToString());

            PropertyInfo[] properties = typeof(HGSCResult).GetProperties();
            foreach (PropertyInfo property in properties)
            {
                if (property.PropertyType == typeof (string))
                {
                    if (property.Name == "Race")
                    {
                        results = ReplaceCodeField(results, property, result, Races);
                    }
                    else if (property.Name == "Ethnicity")
                    {
                        results = ReplaceCodeField(results, property, result, Ethnicities);
                    }
                    else if (property.Name == "OverallInterpretation")
                    {
                        results = ReplaceCodeField(results, property, result, Interpretations);
                    }
                    else if (property.Name == "Sex")
                    {
                        results = ReplaceCodeField(results, property, result, Sex);
                    }
                    else if (property.Name == "SpecimenType")
                    {
                        results = ReplaceCodeField(results, property, result, SpecimenTypes);
                    }
                    else if (property.Name == "OrderingPhysicianName")
                    {
                        var nameParts = property.GetValue(result, null).ToString().Split(' ');
                        if (nameParts.Length == 1)
                        {
                            results = results.Replace(ReplaceHgscFunctionFieldId("PhysicianFirstName"), nameParts[0]);
                            results = results.Replace(ReplaceHgscFunctionFieldId("PhysicianLastName"), "");
                        }
                        else if (nameParts.Length == 2)
                        {
                            results = results.Replace(ReplaceHgscFunctionFieldId("PhysicianFirstName"), nameParts[0]);
                            results = results.Replace(ReplaceHgscFunctionFieldId("PhysicianLastName"), nameParts[1]);
                        }
                        else if (nameParts.Length > 2)
                        {
                            results = results.Replace(ReplaceHgscFunctionFieldId("PhysicianFirstName"), nameParts[0]);
                            results = results.Replace(ReplaceHgscFunctionFieldId("PhysicianLastName"), string.Join(" ", nameParts.Skip(1).ToArray()));
                        }
                        else
                        {
                            results = results.Replace(ReplaceHgscFunctionFieldId("PhysicianFirstName"), "");
                            results = results.Replace(ReplaceHgscFunctionFieldId("PhysicianLastName"), "");
                        }
                    }
                    else
                    {
                        results = ReplaceStringWithPropertyValue(results, property, result);                        
                    }
                }
            }

            // Now handle the more complex results
            var combinedDiseaseXml = "";
            if (result.Diseases.Count > 0)
            {
                foreach (var disease in result.Diseases)
                {
                    var diseaseCode = DiseaseToDiseaseCode(disease);
                    if (diseaseCode != null)
                    {
                        var diseaseXml = ReportDiseaseTemplate;
                        diseaseXml = diseaseXml.Replace(ReplaceFieldId("Name"), diseaseCode.CodeText);
                        diseaseXml = CodeTemplateReplace(diseaseXml, diseaseCode);
                        combinedDiseaseXml += "\r\n" + diseaseXml;
                    }
                }
            }

            results = results.Replace(ReplaceFieldId("InterpretedDiseaseXml"), combinedDiseaseXml);

            if (results.Contains(FieldStart) || results.Contains(FieldEnd))
            {
                throw new Exception("Unhandled field detected");
            }

            return results;
        }

        private string CodeTemplateReplace(string codeXml, Code code)
        {
            codeXml = codeXml.Replace(ReplaceFieldId("CodeSystem"), code.CodeSystem);
            codeXml = codeXml.Replace(ReplaceFieldId("CodeText"), code.CodeText);
            codeXml = codeXml.Replace(ReplaceFieldId("Code"), code.CodeValue);
            codeXml = codeXml.Replace(ReplaceFieldId("ValueSetAbbr"), code.ValueSetAbbr);
            return codeXml;
        }

        private string ReplaceCodeField(string results, PropertyInfo property, HGSCResult result, Dictionary<string, Code> codeLookup)
        {
            var codeKey = property.GetValue(result, null).ToString();
            if (!codeLookup.ContainsKey(codeKey))
            {
                throw new Exception("Unknown code detected");
            }

            var code = codeLookup[codeKey];
            var codeXml = CodeTemplate;
            codeXml = CodeTemplateReplace(codeXml, code);
            //codeXml = codeXml.Replace(ReplaceFieldId("CodeSystem"), code.CodeSystem);
            //codeXml = codeXml.Replace(ReplaceFieldId("CodeText"), code.CodeText);
            //codeXml = codeXml.Replace(ReplaceFieldId("Code"), code.CodeValue);
            //codeXml = codeXml.Replace(ReplaceFieldId("ValueSetAbbr"), code.ValueSetAbbr);
            return results.Replace(ReplaceHgscCodeFieldId(property.Name), codeXml);
        }
    }
}
