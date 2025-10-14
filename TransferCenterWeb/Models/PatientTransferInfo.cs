using System;
using System.ComponentModel;

namespace TransferCenterWeb.Models;

public class PatientTransferInfo
{
    public long Id { get; set; }
    public Guid UId { get; set; }

    [DisplayName("Case Mgr/SW/RN")]
    public string CaseMgrSwRn { get; set; }

    [DisplayName("Phone #")]
    public string PhoneNumber { get; set; }

    [DisplayName("Fax #")]
    public string FaxNumber { get; set; }

    [DisplayName("Requesting Facility")]
    public string RequestingFacility { get; set; }

    [DisplayName("Date Of Transfer")]
    public DateTime TransferDate { get; set; }= DateTime.Now;

    [DisplayName("Referring MD")]
    public string ReferringMd { get; set; }

    [DisplayName("Direct Phone #")]
    public string ReferringMdPhone { get; set; }

    [DisplayName("Direct Phone #")]
    public string ReferringSpecialistPhone { get; set; }

    [DisplayName("Referring Specialist")]
    public string ReferringSpecialist { get; set; }

    [DisplayName("Admit Date")]
    public DateTime AdmitDate { get; set; }=DateTime.Now;

    public string Unit { get; set; }

    [DisplayName("Unit Phone #")]
    public string UnitPhone { get; set; }
}
