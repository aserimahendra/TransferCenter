using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using TransferCenterWeb.Models.GlobalPatientTransfer;

namespace TransferCenterWeb.Models.GlobalPatientTransfer;

public class GlobalPatientTransferListViewModel
{
    public required List<GlobalPatientTransferRequest> Items { get; init; }
    public int TotalCount { get; init; }
    public string? CaseMgrSwRn { get; init; }

    [DisplayName("Patient Name")]
    public string? PatientName { get; init; }

    [DisplayName("Transfer Date From (MM/DD/YYYY)")]
    [DataType(DataType.Date)]
    [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}", ApplyFormatInEditMode = true)]
    public DateTime? TransferDateFrom { get; init; }

    [DisplayName("Transfer Date To (MM/DD/YYYY)")]
    [DataType(DataType.Date)]
    [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}", ApplyFormatInEditMode = true)]
    public DateTime? TransferDateTo { get; init; }

    // Error message for invalid date range
    public string? ErrorMessage { get; set; }
}
