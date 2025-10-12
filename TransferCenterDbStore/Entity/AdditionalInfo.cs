namespace TransferCenterDbStore.Entity;

public class AdditionalInfo
{
    public long Id { get; set; }
    // Binary (YES/NO) — represented by bool
    public bool ServicesAvailable { get; set; }
    public bool SitterRequired { get; set; }
    public bool VTIBDrips { get; set; }
    public bool Dialysis { get; set; }
    public bool PFCTTransfer { get; set; }
    public bool CovidWithin3Days { get; set; }
    
    // Tri-state document statuses
    public DocumentStatus FaceSheet { get; set; }
    public DocumentStatus HAndP { get; set; }
    public DocumentStatus CovidTestResults { get; set; }
    public DocumentStatus TransferOrder { get; set; }
    public DocumentStatus ProgressNotes { get; set; }
    public DocumentStatus ConsultationNotes { get; set; }
    public DocumentStatus MostRecentLabResults { get; set; }
    public DocumentStatus RadiologyResults { get; set; }
    public DocumentStatus MedicationList { get; set; }
    public DocumentStatus TreatmentsAndProceduresInED { get; set; }
    public DocumentStatus InsuranceAuthorization { get; set; }

    // Free text fields
    public string OtherNotes { get; set; }
    public string CodeStatus { get; set; }

}

public enum DocumentStatus
{
    Sent,
    Yes,
    NA
}
