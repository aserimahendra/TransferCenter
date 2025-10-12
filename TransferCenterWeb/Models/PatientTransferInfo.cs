namespace TransferCenterWeb.Models;

public class PatientTransferInfo
{
    public int Id { get; set; }

    public string CaseMgrSwRn { get; set; }

    public string PhoneNumber { get; set; }

    public string FaxNumber { get; set; }

    public DateTime TransferDate { get; set; }

    public string ReferringMdPhone { get; set; }

    public string ReferringSpecialist { get; set; }

    public string SpecialistPhone { get; set; }

    public DateTime AdmitDate { get; set; }

    public string Unit { get; set; }

    public string UnitPhone { get; set; }
}
