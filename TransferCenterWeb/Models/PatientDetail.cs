using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace TransferCenterWeb.Models;

public class PatientDetails
{
    public long Id { get; set; }

    public Guid UId { get; set; }

    [Required]
    [DisplayName("Patient Name")]
    public string Name { get; set; } = null!;

    [Required]
    [DisplayName("Date of Birth")]
    [DataType(DataType.Date)]
    public DateTime DOB { get; set; } = DateTime.Today;

    [Required]
    [DisplayName("Gender")]
    public string Gender { get; set; } = null!;

    [Required]
    [DisplayName("Is In Isolation?")]
    public bool IsIsolation { get; set; }

    [Required]
    [DisplayName("Isolation Type")]
    public string IsolationType { get; set; }

    [Required]
    [DisplayName("Height")]
    public string Height { get; set; }

    [Required]
    [DisplayName("Weight")]
    public string Weight { get; set; }

    [Required]
    [DisplayName("Diagnosis")]
    public string Diagnosis { get; set; }

    [DisplayName("Level of Care Needed")]
    public string? LevelOfCareNeeded { get; set; }

    [DisplayName("Accepting Physician")]
    public string? AcceptingPhysician { get; set; }

    [DisplayName("Reason for Transfer")]
    public string? ReasonForTransfer { get; set; }

    [Required]
    [DisplayName("Lateral")]
    public bool Lateral { get; set; }

    [Required]
    public bool HLOC { get; set; }

    [Required]
    [DisplayName("Patient Insurance")]
    public string PatientInsurance { get; set; }
}
