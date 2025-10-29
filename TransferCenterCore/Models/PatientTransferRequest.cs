namespace TransferCenterCore.Models;

/// <summary>
/// Root object representing the complete ICU In-Patient Transfer Request Form.
/// </summary>
public class PatientTransferRequest
{
    public Guid Id { get; set; }

    // Section 1: Basic Transfer Info (e.g. who referred, unit info)
    public PatientTransferInfo PatientTransferInfo { get; set; } = new();

    // Section 2: Detailed Patient Info (e.g. diagnosis, insurance, isolation)
    public PatientDetails PatientDetails { get; set; } = new();

    // Section 3: Additional Metadata or Key-Value Info
    public AdditionalInfo AdditionalInfo { get; set; } = new();
    public ComorbiditiesAndRiskScore ComorbiditiesAndRiskScore { get; set; } = new();

}