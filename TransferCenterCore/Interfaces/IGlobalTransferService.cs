using TransferCenterCore.Models;

namespace TransferCenterCore.Interfaces;

public interface IGlobalTransferService
{
    public Task Save(GlobalPatientTransferRequest patientTransferViewModel);
    public Task<GlobalPatientTransferRequest> Get(Guid uid);
    public Task<List<GlobalPatientTransferRequest>> GetList();
    public bool Update(GlobalPatientTransferRequest patientTransferViewModel);
    public bool Delete(GlobalPatientTransferRequest patientTransferViewModel);
}