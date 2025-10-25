using System.ComponentModel;

namespace TransferCenterCore.Models;

public class AdditionalInfo
{
    public Guid UId { get; set; }
    public long Id { get; set; }
    public bool ServicesAvailable { get; set; }
    public bool SitterRequired { get; set; }
    public bool VTIBDrips { get; set; }
    public bool Dialysis { get; set; }
    public bool PFCTTransfer { get; set; }
    public bool CovidWithin3Days { get; set; }
    public short FaceSheet { get; set; }
    public short HAndP { get; set; }
    public short CovidTestResults { get; set; }
    public short TransferOrder { get; set; }
    public short ProgressNotes { get; set; }
    public short ConsultationNotes { get; set; }
    public short MostRecentLabResults { get; set; }
    public short RadiologyResults { get; set; }
    public short MedicationList { get; set; }
    public short TreatmentsAndProceduresInED { get; set; }
    public short InsuranceAuthorization { get; set; }

    public string OtherNotes { get; set; }
    public string CodeStatus { get; set; }
    
    public short LifeImageUploadRequested { get; set; }

    /// <summary>A7. Sending facility unable/refusing to use LifeImage (CD requested after support offered).</summary>
    public short SendingFacilityUnableToUseLifeImage { get; set; }
    
    public bool ColdOrFluSymptoms { get; set; }

    /// <summary>Q2. Does the patient have a new rash with unknown cause?</summary>
    public bool NewRashUnknownCause { get; set; }

    /// <summary>Q3. Direct contact with confirmed COVID-19 case in last 14 days?</summary>
    public bool ContactWithCovidPositive { get; set; }

    /// <summary>Q4. Diagnosed with or tested positive for COVID-19 at non-UCI lab?</summary>
    public bool DiagnosedCovidOrPositiveLab { get; set; }

    /// <summary>Q4.1. If YES, specify diagnosis/test date(s).</summary>
    public DateTime? CovidDiagnosisDates { get; set; }

    /// <summary>Q5. Sick household members or contact with symptomatic person?</summary>
    public bool SickHouseholdMembers { get; set; }

    /// <summary>Q6. Exposure to measles in last 21 days?</summary>
    public bool ExposedToMeasles { get; set; }

    /// <summary>Q7. Travel outside the U.S. in last 30 days?</summary>
    public bool TraveledOutsideUS { get; set; }

    /// <summary>Q8. Travel to Arabian Peninsula (past 14 days)?</summary>
    public bool TraveledArabianPeninsula { get; set; }

    /// <summary>Q9. Travel to Africa (past 21 days)?</summary>
    public bool TraveledAfrica { get; set; }

    /// <summary>Q9.1. If YES to Q8 or Q9, respiratory illness, pneumonia, or ARDS?</summary>
    public bool HasRespiratoryIllnessAfterTravel { get; set; }

    /// <summary>Q10. Admitted to any Kindred Hospital since July 1, 2020?</summary>
    public bool AdmittedToKindredHospital { get; set; }

    /// <summary>Q11. Current or previous infection with Multi-Drug Resistant Organisms?</summary>
    public bool MultiDrugResistantInfection { get; set; }

    /// <summary>Q11.1. If YES, list microorganism(s).</summary>
    public string Microorganisms { get; set; }

    /// <summary>Q12. Active communicable disease (TB, shingles, lice, scabies, etc.)?</summary>
    public bool CommunicableDisease { get; set; }

    /// <summary>Q12.1. If YES, list disease(s) or condition(s).</summary>
    public string DiseaseConditions { get; set; }
    
    public short LabResultsStatus { get; set; }

    public short DiagnosticsStatus { get; set; }

    public short MedicationListStatus { get; set; }

}