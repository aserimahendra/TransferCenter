using TransferCenterCore.Models;

namespace TransferCenterCore.Interfaces;

public interface IPatientTransferService
{
    public Task Save(PatientTransferRequest patientTransferViewModel);
    public Task<PatientTransferRequest> Get(Guid uid);
    public Task<List<PatientTransferRequest>> GetList();
    public Task Update(PatientTransferRequest patientTransferViewModel);
    public bool Delete(PatientTransferRequest patientTransferViewModel);
}