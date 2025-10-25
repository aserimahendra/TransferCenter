using System.ComponentModel.DataAnnotations;

namespace TransferCenterDbStore.Entities;

public class PatientTransferInfo
{
    [Key]
    public long Id { get; set; }
    public Guid UId { get; set; }

    public short TransferType { get; set; }
    public string CaseMgrSwRn { get; set; }

    public string PhoneNumber { get; set; }

    public string FaxNumber { get; set; }
    public string RequestingFacility { get; set; }

    public DateTime TransferDate { get; set; }

    public string ReferringMd { get; set; }
    public string ReferringMdPhone { get; set; }
    public string ReferringSpecialistPhone { get; set; }

    public string ReferringSpecialist { get; set; }

    public DateTime AdmitDate { get; set; }

    public string Unit { get; set; }

    public string UnitPhone { get; set; }
    
    public bool IsHloc { get; set; } 
    public int? MR { get; set; }
    
    public string? SecondPhoneNumber { get; set; }

    public string? SecondFaxNumber { get; set; }
    
    public string? PrimaryCallerName { get; set; }
    
    public string? SecondaryCallerName { get; set; }

}
