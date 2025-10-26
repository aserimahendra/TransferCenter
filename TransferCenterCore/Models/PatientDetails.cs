namespace TransferCenterCore.Models;

public class PatientDetails : AuditLogMeta
{
    public long Id { get; set; }

    public Guid UId { get; set; }
    public string Name { get; set; } 

    public short TransferType { get; set; }
    public DateTime DOB { get; set; } = DateTime.Now;

    public short Gender { get; set; }

    public bool IsIsolation { get; set; }

    public string IsolationType { get; set; }

    public double Height { get; set; }
    public double Weight { get; set; }
    
    
    public string Diagnosis { get; set; }

    public string? LevelOfCareNeeded { get; set; }

    public string? AcceptingPhysician { get; set; }

    public string? ReasonForTransfer { get; set; }

    public bool Lateral { get; set; } 
    public bool HLOC { get; set; } 

    public string? PatientInsurance { get; set; }
    
    public string? CodeStatus { get; set; }
    public bool Sitter { get; set; }
    public bool JehovahWitness { get; set; }
    public bool Capitated { get; set; }
    public string? GCS { get; set; }
    public short WeightIn { get; set; }

    public bool IsActive { get; set; } = true;

}
