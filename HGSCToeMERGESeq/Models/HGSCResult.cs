using System.Collections.Generic;
using Newtonsoft.Json;

namespace HGSCToeMERGESeq.Models
{
    public class Variant
    {
        [JsonProperty("zygosity")]
        public string Zygosity { get; set; }

        [JsonProperty("interpretation")]
        public string Interpretation { get; set; }

        [JsonProperty("inheritance")]
        public string Inheritance { get; set; }

        [JsonProperty("variantInterpretation")]
        public string VariantInterpretation { get; set; }

        [JsonProperty("transcript")]
        public string Transcript { get; set; }

        [JsonProperty("notes")]
        public string Notes { get; set; }

        [JsonProperty("proteinChange")]
        public string ProteinChange { get; set; }

        [JsonProperty("disease")]
        public string Disease { get; set; }

        [JsonProperty("confirmedBySanger")]
        public bool ConfirmedBySanger { get; set; }

        [JsonProperty("variantCuration")]
        public string VariantCuration { get; set; }

        [JsonProperty("cDNA")]
        public string cDNA { get; set; }

        [JsonProperty("dnaChange")]
        public string DnaChange { get; set; }

        [JsonProperty("genomic")]
        public string Genomic { get; set; }

        [JsonProperty("position")]
        public string Position { get; set; }

        [JsonProperty("alt")]
        public string Alt { get; set; }

        [JsonProperty("gene")]
        public string Gene { get; set; }

        [JsonProperty("ref")]
        public string Ref { get; set; }

        [JsonProperty("geneDiseaseText")]
        public string GeneDiseaseText { get; set; }

        [JsonProperty("chromosome")]
        public string Chromosome { get; set; }
    }

    public class HGSCResult
    {
        [JsonProperty("indicationForTesting")]
        public string IndicationForTesting { get; set; }

        [JsonProperty("sampleCollectedDate")]
        public string SampleCollectedDate { get; set; }

        [JsonProperty("patientLastName")]
        public string PatientLastName { get; set; }

        [JsonProperty("vipFile")]
        public string VipFile { get; set; }

        [JsonProperty("familyID")]
        public string FamilyId { get; set; }

        [JsonProperty("patientID")]
        public string PatientId { get; set; }

        [JsonProperty("sex")]
        public string Sex { get; set; }

        [JsonProperty("reportStatus")]
        public string ReportStatus { get; set; }

        [JsonProperty("sangerSiteNote")]
        public string SangerSiteNote { get; set; }

        [JsonProperty("ethnicity")]
        public string Ethnicity { get; set; }

        [JsonProperty("patientSampleID")]
        public string PatientSampleId { get; set; }

        [JsonProperty("labStatus")]
        public string LabStatus { get; set; }

        [JsonProperty("sampleCollectionSource")]
        public string SampleCollectionSource { get; set; }

        [JsonProperty("caseType")]
        public string CaseType { get; set; }

        [JsonProperty("localID")]
        public string LocalId { get; set; }

        [JsonProperty("dateOfBirth")]
        public string DateOfBirth { get; set; }

        [JsonProperty("addendums")]
        public List<string> Addendums { get; set; }

        [JsonProperty("overallInterpretation")]
        public string OverallInterpretation { get; set; }

        [JsonProperty("methodology")]
        public string Methodology { get; set; }

        [JsonProperty("phenotypeTerms")]
        public string PhenotypeTerms { get; set; }

        [JsonProperty("neptuneVersion")]
        public string NeptuneVersion { get; set; }

        [JsonProperty("familyRelationship")]
        public string FamilyRelationship { get; set; }

        [JsonProperty("patientName")]
        public string PatientName { get; set; }

        [JsonProperty("orderingPhysicianAddress")]
        public string OrderingPhysicianAddress { get; set; }

        /// <summary>
        /// NOTE THERE IS A TYPO IN THE JSON FIELD NAME
        /// </summary>
        [JsonProperty("concentation")]
        public string Concentration { get; set; }

        [JsonProperty("genomicSource")]
        public string GenomicSource { get; set; }

        [JsonProperty("reportDate")]
        public string ReportDate { get; set; }

        [JsonProperty("diseases")]
        public List<string> Diseases { get; set; }

        [JsonProperty("patientFirstName")]
        public string PatientFirstName { get; set; }

        [JsonProperty("barcode")]
        public string Barcode { get; set; }

        [JsonProperty("byName")]
        public string ByName { get; set; }

        [JsonProperty("geneCoverage")]
        public List<List<string>> GeneCoveragey { get; set; }

        [JsonProperty("totalDNA")]
        public string TotalDna { get; set; }

        [JsonProperty("reportIdentifier")]
        public string ReportIdentifier { get; set; }

        [JsonProperty("clinicalNotes")]
        public string ClinicalNotes { get; set; }

        [JsonProperty("isPreliminary")]
        public string IsPreliminary { get; set; }

        // TODO: Complex type
        [JsonProperty("variants")]
        public List<Variant> Variants { get; set; } 

        [JsonProperty("orderingPhysicianName")]
        public string OrderingPhysicianName { get; set; }

        [JsonProperty("diseaseStatus")]
        public string DiseaseStatus { get; set; }

        [JsonProperty("sampleReceivedDate")]
        public string SampleReceivedDate { get; set; }

        [JsonProperty("accessionNumber")]
        public string AccessionNumber { get; set; }

        [JsonProperty("reportComment")]
        public string ReportComment { get; set; }

        [JsonProperty("clinicalSite")]
        public string ClinicalSite { get; set; }

        [JsonProperty("age")]
        public string Age { get; set; }

        [JsonProperty("testName")]
        public string TestName { get; set; }

        [JsonProperty("initialVolume")]
        public string InitialVolume { get; set; }

        [JsonProperty("onDate")]
        public string OnDate { get; set; }

        [JsonProperty("race")]
        public string Race { get; set; }

        [JsonProperty("specimenType")]
        public string SpecimenType { get; set; }

        [JsonProperty("rackLocation")]
        public string RackLocation { get; set; }

        [JsonProperty("interrogatedButNotFound")]
        public string InterrogatedButNotFound { get; set; }

        [JsonProperty("patientMiddleInitial")]
        public string PatientMiddleInitial { get; set; }

        [JsonProperty("deidentified")]
        public string Deidentified { get; set; }

        [JsonProperty("notInterpreted")]
        public string NotInterpreted { get; set; }
    }
}
