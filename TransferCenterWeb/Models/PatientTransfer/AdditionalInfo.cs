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

    [Display(Name = "Does the patient have cold or flu-like symptoms? (e.g. Fever ≥ 99°F in the last 24 hours, chills, new cough, new shortness of breath, muscle aches, unexpected fatigue, sore throat, headache, diarrhea, nausea, or change/loss of taste or smell with unknown cause or other cold symptoms?)")]
    public bool ColdOrFluSymptoms { get; set; }

    [Display(Name = "Does the patient have a new rash with unknown cause?")]
    public bool NewRashUnknownCause { get; set; }

    [Display(Name = "Have you been in direct contact with a person who has laboratory confirmed COVID-19 in the last 14 days?")]
    public bool ContactWithCovidPositive { get; set; }

    [Display(Name = "Have you been diagnosed with COVID-19 or tested positive at a non-UCI lab?")]
    public bool DiagnosedCovidOrPositiveLab { get; set; }

    [Display(Name = "If YES, please specify dates")]
    public DateTime? CovidDiagnosisDates { get; set; }

    [Display(Name = "Do you have any sick household members or been in close contact with someone who has cold or flu-like symptoms in the last 14 days?")]
    public bool SickHouseholdMembers { get; set; }

    [Display(Name = "Has the patient been exposed to anyone with the measles in the last 21 days?")]
    public bool ExposedToMeasles { get; set; }

    [Display(Name = "Have you traveled outside of the United States in the last 30 days?")]
    public bool TraveledOutsideUS { get; set; }

    [Display(Name = "Did you travel to the Arabian Peninsula in the past 14 days? (Bahrain, Iraq, Iran, Israel, The West Bank, Gaza, Jordan, Kuwait, Oman, Qatar, Saudi Arabia, Syria, United Arab Emirates, Yemen and/or Lebanon?)")]
    public bool TraveledArabianPeninsula { get; set; }

    [Display(Name = "Did you travel to Africa in the past 21 days? (Democratic Republic Congo, Zaire-East Congo, Former Belgian Congo, Congo-Kinshasa, &/or The Congo)")]
    public bool TraveledAfrica { get; set; }

    [Display(Name = "If the patient answered YES to either of the questions 8 or 9, does the patient have any of the following: respiratory illness, pneumonia, fever, cough, shortness of breath, nausea or vomiting, or ARDS?")]
    public bool HasRespiratoryIllnessAfterTravel { get; set; }

    [Display(Name = "Has the patient been admitted to any of the facilities since July 1, 2020? (Kindred Hospital - Westminster, Los Angeles, Paramount, Baldwin Park, South Bay, San Gabriel Valley, La Mirada)")]
    public bool AdmittedToKindredHospital { get; set; }

    [Display(Name = "Does the patient have current or previous infection or colonization with Multi Drug Resistant Organisms (examples: MRSA, CRE, ESBL, VRE, C. difficile, C. auris)?")]
    public bool MultiDrugResistantInfection { get; set; }

    [Display(Name = "If yes, list microorganism(s)")]
    public string Microorganisms { get; set; }

    [Display(Name = "Does the patient have an active communicable disease (examples: disseminated shingles, norovirus, TB, etc.) or other condition (e.g. lice, scabies)?")]
    public bool CommunicableDisease { get; set; }

    [Display(Name = "If yes, list disease(s)/condition(s)")]
    public string DiseaseConditions { get; set; }

    [Display(Name = "3 Days of Lab Results (including a COVID result within the last 72 hours)")]
    public DocumentStatus LabResultsStatus { get; set; }

    [Display(Name = "Any Diagnostic Reports (MRI, CT, X-Ray, EKG, etc.)")]
    public DocumentStatus DiagnosticsStatus { get; set; }

    [Display(Name = "Medication List")]
    public DocumentStatus MedicationListStatus { get; set; }
        
    [Required]
    [DisplayName("Face Sheet")]
    public DocumentStatus FaceSheet { get; set; }

    [Required]
    [Display(Name = "H&P (History and Physical), Progress & Consultation Notes")]
    public DocumentStatus HAndP { get; set; }
        
    [Required]
    [Display(Name = "COVID within 3 Days")]
    public bool CovidWithin3Days { get; set; }
        
    [Required]
    [Display(Name = "Most Recent Lab Results")]
    public DocumentStatus MostRecentLabResults { get; set; }

    [Required]
    [Display(Name = "Radiology Results")]
    public DocumentStatus RadiologyResults { get; set; }

    [Required]
    [Display(Name = "Medication List (additional)")]
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