using TransferCenterCore.Models;

namespace TransferCenterCore.Interface
{
    public interface IGlobalTransferService
    {
        public Task Save(PatientTransferViewModel patientTransferViewModel);
        public Task<PatientTransferViewModel> Get(Guid uid);
        public Task<List<PatientTransferViewModel>> GetList();
        public bool Update(PatientTransferViewModel patientTransferViewModel);
        public bool Delete(PatientTransferViewModel patientTransferViewModel);
    }
}
