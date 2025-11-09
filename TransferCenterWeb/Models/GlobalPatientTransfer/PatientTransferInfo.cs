using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace TransferCenterWeb.Models.GlobalPatientTransfer;

public class PatientTransferInfo:AuditLogMeta
{
    public long Id { get; set; }

    public Guid UId { get; set; }

    [Required]
    [DisplayName("Case Mgr/SW/RN")]
    public string CaseMgrSwRn { get; set; } = string.Empty;

    [Required]
    [DisplayName("Phone #")]
    [Phone]
    [RegularExpression(@"^(?:\+?\d[\d\s().-]{8,18}\d)$", ErrorMessage = "Enter a valid phone number (10-20 digits, may include +, space, (), - or .)")]
    public string PhoneNumber { get; set; } = string.Empty;

    [Required]
    [DisplayName("Fax #")]
    [Phone]
    [RegularExpression(@"^(?:\+?\d[\d\s().-]{8,18}\d)$", ErrorMessage = "Enter a valid fax number (10-20 digits, may include +, space, (), - or .)")]
    public string FaxNumber { get; set; } = string.Empty;

    [Required]
    [DisplayName("Requesting Facility")]
    public string RequestingFacility { get; set; } = string.Empty;

    [Required]
    [DisplayName("Date Of Transfer")]
    [DataType(DataType.Date)]
    [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}", ApplyFormatInEditMode = true)]
    public DateTime TransferDate { get; set; } = DateTime.Today;

    [Required]
    [DisplayName("Referring MD")]
    public string ReferringMd { get; set; } = string.Empty;

    [Required]
    [DisplayName("Direct Phone #")]
    [Phone]
    [RegularExpression(@"^(?:\+?\d[\d\s().-]{8,18}\d)$", ErrorMessage = "Enter a valid phone number (10-20 digits, may include +, space, (), - or .)")]
    public string ReferringMdPhone { get; set; } = string.Empty;

    [Required]
    [DisplayName("Direct Phone #")]
    [Phone]
    [RegularExpression(@"^(?:\+?\d[\d\s().-]{8,18}\d)$", ErrorMessage = "Enter a valid phone number (10-20 digits, may include +, space, (), - or .)")]
    public string ReferringSpecialistPhone { get; set; } = string.Empty;

    [Required]
    [DisplayName("Referring Specialist")]
    public string ReferringSpecialist { get; set; } = string.Empty;

    [Required]
    [DisplayName("Admit Date")]
    [DataType(DataType.Date)]
    [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}", ApplyFormatInEditMode = true)]
    public DateTime AdmitDate { get; set; } = DateTime.Today;

    [Required]
    public string Unit { get; set; } = string.Empty;

    [Required]
    [DisplayName("Unit Phone #")]
    [Phone]
    [RegularExpression(@"^(?:\+?\d[\d\s().-]{8,18}\d)$", ErrorMessage = "Enter a valid phone number (10-20 digits, may include +, space, (), - or .)")]
    public string UnitPhone { get; set; } = string.Empty;

    public bool IsActive { get; set; } = true;
}
