namespace TransferCenterWeb.Models;

public class PatientTransferViewModel
{
    // Section 1: Basic Transfer Info (e.g. who referred, unit info)
    public PatientTransferInfo TransferInfo { get; set; } = new();

    // Section 2: Detailed Patient Info (e.g. diagnosis, insurance, isolation)
    public PatientDetail Detail { get; set; } = new();

    // Section 3: Additional Metadata or Key-Value Info
    public List<AdditionalInfo> AdditionalInfos { get; set; } = new();
}
