using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace TransferCenterWeb.Models.PatientTransfer;

public class PatientDetails : AuditLogMeta
{
    [DisplayName("Height (Text)")]
    public long Id { get; set; }
    
    public Guid UId { get; set; }

    [Required]
    [DisplayName("Patient First Name")]
    public string FirstName { get; set; }

    [Required]
    [DisplayName("Patient Last Name")]
    public string LastName { get; set; }

    [Required]
    [DisplayName("Date Of Birth (MM/DD/YYYY)")]
    [DataType(DataType.Date)]
    [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}", ApplyFormatInEditMode = true)]
    public DateTime DOB { get; set; } = DateTime.Today;

    [Required]
    [DisplayName("Gender")]
    public short Gender { get; set; }

    [DisplayName("Gender (Text)")]
    public string GenderText => Gender switch
    {
        0 => "Male",
        1 => "Female",
        _ => "Other"
    };

    [Required]
    [DisplayName("Height")]
    public double Height { get; set; }

    [DisplayName("Height (ft)")]
    [Range(0, 9, ErrorMessage = "Feet must be 0-9")]
    public int? HeightFeet { get; set; }

    [DisplayName("Height (in)")]
    [Range(0, 11, ErrorMessage = "Inches must be between 0 and 11")]
    public int? HeightInches { get; set; }

    public string? HeightText => $"{(HeightFeet ?? 0)} ft {(HeightInches ?? 0)} in";
    
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
