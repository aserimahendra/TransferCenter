using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace TransferCenterWeb.Models.PatientTransfer;

public class AdditionalInfo
{
    public Guid UId { get; set; }

    public long Id { get; set; }
        
    [Required]
    [DisplayName("Dialysis Patient")]
    public bool Dialysis { get; set; }
        
    [Required]
    [DisplayName("Vent/Trach/Intubated/BipAP/Drips")]
    public bool VTIBDrips { get; set; }
        
    [Required]
    [DisplayName("Services Available")]
    public bool ServicesAvailable { get; set; }
        
    [Display(Name = "LifeImage Upload Requested")]
    public DocumentStatus LifeImageUploadRequested { get; set; }

    [Required]
    [DisplayName("Patient/Family Consent To Transfer")]
    public bool PFCTTransfer { get; set; }
        
    [Display(Name = "Sending Unable or Refusing to Use LifeImage (CD Requested)")]
    public DocumentStatus SendingFacilityUnableToUseLifeImage { get; set; }

    [Display(Name = "Does the patient have cold or flu-like symptoms?")]
    public bool ColdOrFluSymptoms { get; set; }

    [Display(Name = "Does the patient have a new rash with unknown cause?")]
    public bool NewRashUnknownCause { get; set; }

    [Display(Name = "Direct contact with confirmed COVID-19 in last 14 days?")]
    public bool ContactWithCovidPositive { get; set; }

    [Display(Name = "Diagnosed with COVID-19 or tested positive at non-UCI lab?")]
    public bool DiagnosedCovidOrPositiveLab { get; set; }

    [Display(Name = "If YES, specify dates")]
    public DateTime? CovidDiagnosisDates { get; set; }

    [Display(Name = "Any sick household members or contact with symptomatic person in last 14 days?")]
    public bool SickHouseholdMembers { get; set; }

    [Display(Name = "Exposed to anyone with measles in last 21 days?")]
    public bool ExposedToMeasles { get; set; }

    [Display(Name = "Traveled outside of the United States in the last 30 days?")]
    public bool TraveledOutsideUS { get; set; }

    [Display(Name = "Traveled to the Arabian Peninsula in the past 14 days?")]
    public bool TraveledArabianPeninsula { get; set; }

    [Display(Name = "Traveled to Africa in the past 21 days?")]
    public bool TraveledAfrica { get; set; }

    [Display(Name = "If YES to either, does patient have respiratory illness or ARDS?")]
    public bool HasRespiratoryIllnessAfterTravel { get; set; }

    [Display(Name = "Admitted to any Kindred Hospital since July 1, 2020?")]
    public bool AdmittedToKindredHospital { get; set; }

    [Display(Name = "Current or previous infection/colonization with Multi-Drug Resistant Organisms?")]
    public bool MultiDrugResistantInfection { get; set; }

    [Display(Name = "If YES, list microorganism(s)")]
    public string Microorganisms { get; set; }

    [Display(Name = "Active communicable disease or other condition (TB, shingles, etc.)?")]
    public bool CommunicableDisease { get; set; }

    [Display(Name = "If YES, list disease(s)/condition(s)")]
    public string DiseaseConditions { get; set; }

    [Display(Name = "3 Days of Lab Results (Including COVID)")]
    public DocumentStatus LabResultsStatus { get; set; }

    [Display(Name = "Diagnostic Reports (MRI, CT, X-Ray, EKG, etc.)")]
    public DocumentStatus DiagnosticsStatus { get; set; }

    [Display(Name = "Medication List")]
    public DocumentStatus MedicationListStatus { get; set; }
        
    [Required]
    [DisplayName("Face Sheet")]
    public DocumentStatus FaceSheet { get; set; }

    [Required]
    [DisplayName("H&P")]
    public DocumentStatus HAndP { get; set; }
        
    [Required]
    [DisplayName("COVID Within 3 Days")]
    public bool CovidWithin3Days { get; set; }
        
    [Required]
    [DisplayName("Most Recent Lab Results")]
    public DocumentStatus MostRecentLabResults { get; set; }

    [Required]
    [DisplayName("Radiology Results")]
    public DocumentStatus RadiologyResults { get; set; }

    [Required]
    [DisplayName("Medication List")]
    public DocumentStatus MedicationList { get; set; }

    public bool IsActive { get; set; } = true;
}
public enum DocumentStatus
{
    Sent,
    Yes,
    [Description("Will Send")]
    NA
}