using TransferCenterCore.Models;

namespace TransferCenterCore.Interfaces;

public interface IGlobalTransferService
{
    public Task Save(GlobalPatientTransferRequest patientTransferViewModel);
    public Task<GlobalPatientTransferRequest> Get(Guid uid);
    public Task<(IEnumerable<GlobalPatientTransferRequest> Items, int TotalCount)> GetList(int page, int pageSize);
    public Task Update(GlobalPatientTransferRequest patientTransferViewModel);
    public Task Delete(GlobalPatientTransferRequest patientTransferViewModel);
}