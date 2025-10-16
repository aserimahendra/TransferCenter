using System.ComponentModel.DataAnnotations;

namespace TransferCenterDbStore.Entity;

public class PatientTransferInfo
{
    [Key]
    public long Id { get; set; }
    public Guid UId { get; set; }

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
}
