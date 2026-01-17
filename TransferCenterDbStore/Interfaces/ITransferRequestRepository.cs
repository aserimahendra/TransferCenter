using TransferCenterDbStore.Entities;

namespace TransferCenterDbStore.Interfaces;

public interface ITransferRequestRepository : IGenericRepository<TransferRequest>
{
    Task<(IEnumerable<TransferRequest> Items, int TotalCount)> GetList(short transferType, int page, int pageSize, string? caseManager = null, DateTime? transferDateFrom = null, DateTime? transferDateTo = null, string? name = null);
    Task<(IEnumerable<TransferRequest> Items, int TotalCount)> GetList(short transferType, string? caseManager = null, DateTime? transferDateFrom = null, DateTime? transferDateTo = null, string? name = null);
}