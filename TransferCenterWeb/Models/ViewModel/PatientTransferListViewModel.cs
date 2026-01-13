using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using TransferCenterWeb.Models;

namespace TransferCenterWeb.Models.PatientTransfer;

public class PatientTransferListViewModel
{
    public required List<PatientTransferRequest> Items { get; init; }
    public int TotalCount { get; init; }
    // Filters
    [DisplayName("Patient Name")]
    public string? Name { get; init; }

    public string? CaseManager { get; init; }

    [DisplayName("Transfer Date From (MM/DD/YYYY)")]
    [DataType(DataType.Date)]
    [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}", ApplyFormatInEditMode = true)]
    public DateTime? TransferDateFrom { get; init; }

    [DisplayName("Transfer Date To (MM/DD/YYYY)")]
    [DataType(DataType.Date)]
    [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}", ApplyFormatInEditMode = true)]
    public DateTime? TransferDateTo { get; init; }
} 
