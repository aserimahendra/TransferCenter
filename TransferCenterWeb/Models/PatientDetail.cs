namespace TransferCenterWeb.Models;

public class PatientDetail
{
    public int Id { get; set; }

    public string PatientName { get; set; } = null!;

    public DateTime DateOfBirth { get; set; }

    public string Sex { get; set; } = null!;

    public bool IsIsolation { get; set; }

    public string IsolationType { get; set; }

    public string Height { get; set; } 

    public string Weight { get; set; } 

    public string Diagnosis { get; set; }

    public string? LevelOfCareNeeded { get; set; }

    public string? AcceptingPhysician { get; set; }

    public string? ReasonForTransfer { get; set; }

    public bool IsLateralTransfer { get; set; } //If true = Lateral, false = HLOC

    public string PatientInsurance { get; set; } 
}
