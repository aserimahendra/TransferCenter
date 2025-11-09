using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace TransferCenterWeb.Models.PatientTransfer;

public class PatientTransferInfo : AuditLogMeta
{
    public long Id { get; set; }

    public Guid UId { get; set; }

    [Required]
    [DisplayName("TC Staff Name")]
    public string CaseMgrSwRn { get; set; }

    [Required]
    [DisplayName("Date & Time Of Call")]
    [DataType(DataType.Date)]
    [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}", ApplyFormatInEditMode = true)]
    public DateTime TransferDate { get; set; } = DateTime.Today;
    
    [Required]
    [DisplayName("MR #")]
    public int MR { get; set; }
    
    [DisplayName("Intial Caller Name & Title ")]
    [Required]
    public string PrimaryCallerName { get; set; }

    [Required]
    [DisplayName("Phone #")]
    [Phone]
    [RegularExpression(@"^(?:\+?\d[\d\s().-]{8,18}\d)$", ErrorMessage = "Enter a valid primary phone number (10-20 digits, may include +, space, (), - or .)")]
    public string PhoneNumber { get; set; }

    [Required]
    [DisplayName("Fax #")]
    [Phone]
    [RegularExpression(@"^(?:\+?\d[\d\s().-]{8,18}\d)$", ErrorMessage = "Enter a valid fax number (10-20 digits, may include +, space, (), - or .)")]
    public string FaxNumber { get; set; }

    
    [DisplayName("Secondary Caller Name & Title ")]
    [Required]
    public string SecondaryCallerName { get; set; }
    
    
    [DisplayName("Phone #")]
    [RegularExpression(@"^(?:\+?\d[\d\s().-]{8,18}\d)$", ErrorMessage = "Enter a valid secondary phone number (10-20 digits, may include +, space, (), - or .)")]
    public string SecondPhoneNumber { get; set; }
    
    
    [DisplayName("Fax #")]
    [RegularExpression(@"^(?:\+?\d[\d\s().-]{8,18}\d)$", ErrorMessage = "Enter a valid fax number (10-20 digits, may include +, space, (), - or .)")]
    public string SecondFaxNumber { get; set; }
    
    
    [Required]
    [DisplayName("Referring Facility")]
    public string RequestingFacility { get; set; }

    [DisplayName("Click here if HLOC")]
    public bool IsHloc { get; set; } 
    
    [Required]
    [DisplayName("Referring/Primary MD")]
    public string ReferringMd { get; set; }

    [Required]
    [DisplayName("Direct Phone #")]
    [Phone]
    [RegularExpression(@"^(?:\+?\d[\d\s().-]{8,18}\d)$", ErrorMessage = "Enter a valid referring MD phone (10-20 digits, may include +, space, (), - or .)")]
    public string ReferringMdPhone { get; set; }
    
    [Required]
    [DisplayName("Secondary / Specialist MD")]
    public string ReferringSpecialist { get; set; }
    
    [Required]
    [DisplayName("Direct Phone #")]
    [Phone]
    [RegularExpression(@"^(?:\+?\d[\d\s().-]{8,18}\d)$", ErrorMessage = "Enter a valid specialist phone (10-20 digits, may include +, space, (), - or .)")]
    public string ReferringSpecialistPhone { get; set; }


    [Required]
    [DisplayName("Admit Date")]
    [DataType(DataType.Date)]
    [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}", ApplyFormatInEditMode = true)]
    public DateTime AdmitDate { get; set; } = DateTime.Today;

    [Required]
    public string Unit { get; set; }

    [Required]
    [DisplayName("Unit Phone #")]
    [Phone]
    [RegularExpression(@"^(?:\+?\d[\d\s().-]{8,18}\d)$", ErrorMessage = "Enter a valid unit phone number (10-20 digits, may include +, space, (), - or .)")]
    public string UnitPhone { get; set; }
    
    public bool IsActive { get; set; } = true;
    
}
