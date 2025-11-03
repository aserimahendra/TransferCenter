using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace TransferCenterWeb.Models.PatientTransfer;

public class PatientDetails : AuditLogMeta
{
    public long Id { get; set; }

    public Guid UId { get; set; }

    [Required]
    [DisplayName("Patient First Name")]
    public string FirstName { get; set; }

    [Required]
    [DisplayName("Patient Last Name")]
    public string LastName { get; set; }

    [Required]
    [DisplayName("Date of Birth")]
    [DataType(DataType.Date)]
    [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}", ApplyFormatInEditMode = true)]
    public DateTime DOB { get; set; } = DateTime.Today;

    [Required]
    [DisplayName("Gender")]
    public short Gender { get; set; }

    [Required]
    [DisplayName("Height")]
    public double Height { get; set; }

    [Required]
    [DisplayName("Weight")]
    public double Weight { get; set; }
    
    [DisplayName("Weight In LBS/KGS")]
    public short WeightIn { get; set; }

    [Required]
    [DisplayName("Diagnosis")]
    public string Diagnosis { get; set; }
    
    [DisplayName("Reason for Transfer")]
    public string? ReasonForTransfer { get; set; }

    [DisplayName("Level of Care Needed")]
    public string? LevelOfCareNeeded { get; set; }

    public bool Sitter { get; set; }

    [DisplayName("Jehovah Witness")]
    public bool JehovahWitness { get; set; }
    
    public bool Capitated { get; set; }
    
    [Required]
    [DisplayName("Is In Isolation?")]
    public bool IsIsolation { get; set; }
    
    [Required]
    [DisplayName("Isolation Type")]
    public string IsolationType { get; set; }
    
    [DisplayName("Code Status")]
    public string CodeStatus { get; set; }
    
    public string GCS { get; set; }

    public bool IsActive { get; set; } = true;

}
