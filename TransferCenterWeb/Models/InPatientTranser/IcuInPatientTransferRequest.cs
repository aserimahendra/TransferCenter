namespace TransferCenterWeb.Models;

/// <summary>
/// Root object representing the complete ICU In-Patient Transfer Request Form.
/// </summary>
public class IcuInPatientTransferRequest
{
        public TransferringFacilityInformation TransferringFacility { get; set; }
        public PatientClinicalInformation PatientClinicalInfo { get; set; }
        public ComorbiditiesAndRiskScore ComorbiditiesInfo { get; set; }
        public TransferLogisticsAndScreening TransferLogistics { get; set; }

        public IcuInPatientTransferRequest()
        {
            TransferringFacility = new TransferringFacilityInformation();
            PatientClinicalInfo = new PatientClinicalInformation();
            ComorbiditiesInfo = new ComorbiditiesAndRiskScore();
            TransferLogistics = new TransferLogisticsAndScreening();
        }
    
}