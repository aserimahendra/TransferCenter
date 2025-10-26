using System.ComponentModel.DataAnnotations;
namespace TransferCenterDbStore.Entities;

public class AdditionalInfo : AuditLogMeta
{
    [Key]
    public long Id { get; set; }
    public Guid UId { get; set; }
    
    public short TransferType { get; set; }
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

    public short SendingFacilityUnableToUseLifeImage { get; set; }

    public bool ColdOrFluSymptoms { get; set; }

    public bool NewRashUnknownCause { get; set; }

    public bool ContactWithCovidPositive { get; set; }

    public bool DiagnosedCovidOrPositiveLab { get; set; }

    public DateTime? CovidDiagnosisDates { get; set; }

    public bool SickHouseholdMembers { get; set; }

    public bool ExposedToMeasles { get; set; }

    public bool TraveledOutsideUS { get; set; }

    public bool TraveledArabianPeninsula { get; set; }

    public bool TraveledAfrica { get; set; }

    public bool HasRespiratoryIllnessAfterTravel { get; set; }

    public bool AdmittedToKindredHospital { get; set; }

    public bool MultiDrugResistantInfection { get; set; }

    public string Microorganisms { get; set; }

    public bool CommunicableDisease { get; set; }

    public string DiseaseConditions { get; set; }
    
    public short LabResultsStatus { get; set; }

    public short DiagnosticsStatus { get; set; }

    public short MedicationListStatus { get; set; }

    public bool IsActive { get; set; } = true;
}


