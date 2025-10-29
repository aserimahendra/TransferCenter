using TransferCenterWeb.Models.GlobalPatientTransfer;

namespace TransferCenterWeb.Models.GlobalPatientTransfer;

public class GlobalPatientTransferListViewModel
{
    public required List<GlobalPatientTransferRequest> Items { get; init; }
    public int TotalCount { get; init; }
}
