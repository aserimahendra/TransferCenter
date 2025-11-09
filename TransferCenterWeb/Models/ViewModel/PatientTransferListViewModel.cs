using TransferCenterWeb.Models;

namespace TransferCenterWeb.Models.PatientTransfer;

public class PatientTransferListViewModel
{
    public required List<PatientTransferRequest> Items { get; init; }
    public int TotalCount { get; init; }
    // Filters
    public string? CaseManager { get; init; }
    public DateTime? TransferDateFrom { get; init; }
    public DateTime? TransferDateTo { get; init; }
}
