using TransferCenterCore.Models;

namespace TransferCenterCore.Interfaces;

public interface IGlobalTransferService
{
    public Task Save(GlobalPatientTransferRequest patientTransferViewModel);
    public Task<GlobalPatientTransferRequest> Get(Guid uid);
    /// <summary>
    /// Returns a paged list of global transfer requests filtered optionally by case manager and transfer date range.
    /// </summary>
    /// <param name="page">1-based page index.</param>
    /// <param name="pageSize">Number of items per page.</param>
    /// <param name="caseMgrSwRn">Optional case manager / SW / RN search text (partial match, case-insensitive).</param>
    /// <param name="transferDateFrom">Optional inclusive start date (date component only considered).</param>
    /// <param name="transferDateTo">Optional inclusive end date (date component only considered).</param>
    public Task<(IEnumerable<GlobalPatientTransferRequest> Items, int TotalCount)> GetList(int page, int pageSize, string? caseMgrSwRn, DateTime? transferDateFrom, DateTime? transferDateTo);
    public Task Update(GlobalPatientTransferRequest patientTransferViewModel);
    public Task Delete(GlobalPatientTransferRequest patientTransferViewModel);
}