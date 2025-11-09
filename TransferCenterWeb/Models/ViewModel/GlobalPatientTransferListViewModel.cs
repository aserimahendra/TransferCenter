using TransferCenterWeb.Models.GlobalPatientTransfer;

namespace TransferCenterWeb.Models.GlobalPatientTransfer;

public class GlobalPatientTransferListViewModel
{
    public required List<GlobalPatientTransferRequest> Items { get; init; }
    public int TotalCount { get; init; }
    public string? CaseMgrSwRn { get; init; }
    public DateTime? TransferDateFrom { get; init; }
    public DateTime? TransferDateTo { get; init; }
}
