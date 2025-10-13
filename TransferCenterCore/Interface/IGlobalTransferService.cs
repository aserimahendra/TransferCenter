using TransferCenterCore.Models;

namespace TransferCenterCore.Interface
{
    public interface IGlobalTransferService
    {
        public Task Save(PatientTransferViewModel patientTransferViewModel);
        public bool Update(PatientTransferViewModel patientTransferViewModel);
        public bool Delete(PatientTransferViewModel patientTransferViewModel);
    }
}
