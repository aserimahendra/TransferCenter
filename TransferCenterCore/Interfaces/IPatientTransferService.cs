using TransferCenterCore.Models;

namespace TransferCenterCore.Interfaces;

public interface IPatientTransferService
{
    public Task Save(PatientTransferRequest patientTransferViewModel);
    public Task<PatientTransferRequest> Get(Guid uid);
    public Task<(IEnumerable<PatientTransferRequest> Items, int TotalCount)> GetList(int page, int pageSize);
    public Task Update(PatientTransferRequest patientTransferViewModel);
    public Task Delete(PatientTransferRequest patientTransferViewModel);
}