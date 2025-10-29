using System.ComponentModel.DataAnnotations;

namespace TransferCenterDbStore.Entities;

public class ComorbiditiesAndRiskScore : AuditLogMeta
{
    [Key]
    public long Id { get; set; }
    public bool None { get; set; }
    public Guid UId { get; set; }
    public bool StrokeTIA { get; set; }
    public bool CHF { get; set; }
    public bool CKD_ESRD { get; set; }
    public bool HTN { get; set; }
    public bool DiabetesOrSkinIssues { get; set; }
    public bool MI_Angina_CAD { get; set; }
    public bool COPDOrRespiratoryFailure { get; set; }
    public bool Ventilated { get; set; }
    public bool PulmonaryHTN { get; set; }
    public bool PulmonaryHTN_Prostacyclin { get; set; }
    public bool ImmunocompromisedOrHIV { get; set; }
    public bool PsychBackground { get; set; }
    public bool NonCompliantWithCare { get; set; }
    public bool UnableToPerformADLs { get; set; }
    public bool DrugOrAlcoholDependence { get; set; }
    public bool LymphomaLeukemiaCancer { get; set; }
    public bool MalnutritionObesityDigestiveDisease { get; set; }
    public bool UnconsciousOrALOC { get; set; }
    public bool LOSMoreThan2Weeks { get; set; }
    public bool OutOfServiceArea { get; set; }
    public bool DNRCodeStatus { get; set; }
    public bool CovidPositive { get; set; }
    public bool RecentSurgeryAtUCI { get; set; }
    public bool RecentSurgeryOutsideUCI { get; set; }
    public int? TotalPoints { get; set; }
    public string? Comorbidities { get; set; }
    public double RiskScore { get; set; }
    public DateTime CreatedDate { get; set; }
    public DateTime UpdatedDate { get; set; }

    public bool IsActive { get; set; } = true;
}