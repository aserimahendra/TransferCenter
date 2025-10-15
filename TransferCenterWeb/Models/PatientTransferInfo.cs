using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace TransferCenterWeb.Models;

public class PatientTransferInfo
{
    public long Id { get; set; }

    public Guid UId { get; set; }

    [Required]
    [DisplayName("Case Mgr/SW/RN")]
    public string CaseMgrSwRn { get; set; }

    [Required]
    [DisplayName("Phone #")]
    [Phone]
    public string PhoneNumber { get; set; }

    [Required]
    [DisplayName("Fax #")]
    [Phone]
    public string FaxNumber { get; set; }

    [Required]
    [DisplayName("Requesting Facility")]
    public string RequestingFacility { get; set; }

    [Required]
    [DisplayName("Date Of Transfer")]
    [DataType(DataType.Date)]
    public DateTime TransferDate { get; set; } = DateTime.Today;

    [Required]
    [DisplayName("Referring MD")]
    public string ReferringMd { get; set; }

    [Required]
    [DisplayName("Direct Phone #")]
    [Phone]
    public string ReferringMdPhone { get; set; }

    [Required]
    [DisplayName("Direct Phone #")]
    [Phone]
    public string ReferringSpecialistPhone { get; set; }

    [Required]
    [DisplayName("Referring Specialist")]
    public string ReferringSpecialist { get; set; }

    [Required]
    [DisplayName("Admit Date")]
    [DataType(DataType.Date)]
    public DateTime AdmitDate { get; set; } = DateTime.Today;

    [Required]
    public string Unit { get; set; }

    [Required]
    [DisplayName("Unit Phone #")]
    [Phone]
    public string UnitPhone { get; set; }
}
