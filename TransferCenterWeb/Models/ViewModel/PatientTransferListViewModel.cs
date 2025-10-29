using TransferCenterWeb.Models;

namespace TransferCenterWeb.Models.PatientTransfer;

public class PatientTransferListViewModel
{
    public required List<PatientTransferRequest> Items { get; init; }
    public int TotalCount { get; init; }
}
