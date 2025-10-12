namespace TransferCenterDbStore.Entity;


public class PatientDetails
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public DateTime DOB { get; set; }

    public string Gender { get; set; } = null!;

    public bool IsIsolation { get; set; }

    public string IsolationType { get; set; }

    public string Height { get; set; }

    public string Weight { get; set; }

    public string Diagnosis { get; set; }

    public string? LevelOfCareNeeded { get; set; }

    public string? AcceptingPhysician { get; set; }

    public string? ReasonForTransfer { get; set; }

    public bool Lateral { get; set; }
    public bool HLOC { get; set; }

    public string PatientInsurance { get; set; }
}
