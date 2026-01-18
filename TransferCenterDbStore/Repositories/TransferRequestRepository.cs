using Microsoft.EntityFrameworkCore;
using TransferCenterDbStore.Data;
using TransferCenterDbStore.Entities;
using TransferCenterDbStore.Interfaces;

namespace TransferCenterDbStore.Repositories;

public class TransferRequestRepository : GenericRepository<TransferRequest>, ITransferRequestRepository
{
    public TransferRequestRepository(BaseDbContext dbContext) : base(dbContext)
    {
    }

    public async Task<(IEnumerable<TransferRequest> Items, int TotalCount)> GetList(short transferType, int page,
        int pageSize, string? caseManager = null, DateTime? transferDateFrom = null,
        DateTime? transferDateTo = null, string? name = null)
    {
        page = page < 1 ? 1 : page;
        pageSize = pageSize <= 0 ? 10 : pageSize;
        var query = GetTransferRequestsQuery(transferType, caseManager, transferDateFrom, transferDateTo, name);
        var totalCount = await query.CountAsync();
        var items = await query
            .OrderByDescending(x => x.PatientTransferInfo.CreatedOn)
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();
        return (items, totalCount);
    }

    public async Task<(IEnumerable<TransferRequest> Items, int TotalCount)> GetList(short transferType,
        string? caseManager = null, DateTime? transferDateFrom = null,
        DateTime? transferDateTo = null, string? name = null)
    {
        var query = GetTransferRequestsQuery(transferType, caseManager, transferDateFrom, transferDateTo, name);
        var totalCount = await query.CountAsync();

        var items = await query
            .OrderByDescending(x => x.PatientTransferInfo.CreatedOn)
            .ToListAsync();
        return (items, totalCount);
    }

    private IQueryable<TransferRequest> GetTransferRequestsQuery(short transferType, string? caseManager = null,
        DateTime? transferDateFrom = null,
        DateTime? transferDateTo = null, string? name = null)
    {
        DateTime? from = transferDateFrom?.Date;
        DateTime? to = transferDateTo?.Date;
        if (from.HasValue && to.HasValue && from > to)
            (from, to) = (to, from);

        IQueryable<TransferRequest> query;

        if (transferType == 2)
        {
            query = from pti in _dbContext.Set<PatientTransferInfo>()
                join pd in _dbContext.Set<PatientDetails>() on pti.UId equals pd.UId
                join ai in _dbContext.Set<AdditionalInfo>() on pti.UId equals ai.UId
                join c in _dbContext.Set<ComorbiditiesAndRiskScore>() on pti.UId equals c.UId
                where pti.TransferType == transferType
                select new TransferRequest
                {
                    Id = pti.UId,
                    PatientTransferInfo = pti,
                    PatientDetails = pd,
                    AdditionalInfo = ai,
                    ComorbiditiesAndRiskScore = c
                };
        }
        else
        {
            query = from pti in _dbContext.Set<PatientTransferInfo>()
                join pd in _dbContext.Set<PatientDetails>() on pti.UId equals pd.UId
                join ai in _dbContext.Set<AdditionalInfo>() on pti.UId equals ai.UId
                where pti.TransferType == transferType
                select new TransferRequest
                {
                    Id = pti.UId,
                    PatientTransferInfo = pti,
                    PatientDetails = pd,
                    AdditionalInfo = ai,
                    ComorbiditiesAndRiskScore = new ComorbiditiesAndRiskScore()
                };
        }

        if (!string.IsNullOrWhiteSpace(caseManager))
        {
            var cm = caseManager.Trim();
            query = query.Where(t =>
                t.PatientTransferInfo.CaseMgrSwRn != null && t.PatientTransferInfo.CaseMgrSwRn.Contains(cm));
        }

        if (!string.IsNullOrWhiteSpace(name))
        {
            var n = name.Trim().ToLowerInvariant();
            query = query.Where(t =>
                t.PatientDetails != null && t.PatientDetails.Name != null &&
                t.PatientDetails.Name.ToLower().Contains(n));
        }

        if (from.HasValue)
            query = query.Where(t => t.PatientTransferInfo.TransferDate >= from.Value);
        if (to.HasValue)
            query = query.Where(t => t.PatientTransferInfo.TransferDate <= to.Value);

        return query;
    }
}