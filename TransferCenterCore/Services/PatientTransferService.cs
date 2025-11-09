using Microsoft.Extensions.Configuration;
using TransferCenterCore.Context;
using TransferCenterCore.Interfaces;
using TransferCenterCore.Models;
using TransferCenterCore.Translators;
using TransferCenterDbStore.UnitOfWork;
using TransferCenterCore.Extensions;
using TransferCenterCore.Utility;

namespace TransferCenterCore.Services;

public class PatientTransferService : IPatientTransferService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IConfiguration _configuration;

    public PatientTransferService(IUnitOfWork unitOfWork, IConfiguration configuration)
    {
        _unitOfWork = unitOfWork;
        _configuration = configuration;
    }

    public async Task Delete(PatientTransferRequest patientTransferRequest)
    {
        patientTransferRequest.AdditionalInfo.IsActive = false;
        patientTransferRequest.PatientDetails.IsActive = false;
        patientTransferRequest.PatientTransferInfo.IsActive = false;
        patientTransferRequest.ComorbiditiesAndRiskScore.IsActive = false;
        await Update(patientTransferRequest);
    }

    public async Task Save(PatientTransferRequest patientTransferRequest)
    {
        try
        {
            await SavePatientInfo(patientTransferRequest.PatientDetails);
            await SavePatientTransferInfo(patientTransferRequest.PatientTransferInfo);
            await SaveAdditionalInfo(patientTransferRequest.AdditionalInfo);
            await SaveComorbiditiesAndRiskScore(patientTransferRequest.ComorbiditiesAndRiskScore);
            await _unitOfWork.CommitAsync();

        }
        catch (Exception)
        {
            throw;
        }
    }

    private async Task SavePatientInfo(PatientDetails patientDetails)
    {
        patientDetails.CreatedBy = CallContextScope.Current?.EmailId ?? string.Empty;
        patientDetails.CreatedOn = DateTime.UtcNow;
        _unitOfWork.PatientDetailsRepository.Add(patientDetails.ToEntity());
    }

    private async Task SavePatientTransferInfo(PatientTransferInfo patientTransferInfo)
    {
        patientTransferInfo.CreatedBy = CallContextScope.Current?.EmailId ?? string.Empty;
        patientTransferInfo.CreatedOn = DateTime.UtcNow;
        _unitOfWork.PatientTransferInfoRepository.Add(patientTransferInfo.ToEntity());
    }

    private async Task SaveAdditionalInfo(AdditionalInfo additionalInfo)
    {
        additionalInfo.CreatedBy = CallContextScope.Current?.EmailId ?? string.Empty;
        additionalInfo.CreatedOn = DateTime.UtcNow;
        _unitOfWork.AdditionalInfoRepository.Add(additionalInfo.ToEntity());
    }
    
    private async Task SaveComorbiditiesAndRiskScore(ComorbiditiesAndRiskScore comorbiditiesAndRiskScore)
    {
        comorbiditiesAndRiskScore.CreatedBy = CallContextScope.Current?.EmailId ?? string.Empty;
        comorbiditiesAndRiskScore.CreatedOn = DateTime.UtcNow;
        _unitOfWork.ComorbiditiesAndRiskScoreRepository.Add(comorbiditiesAndRiskScore.ToEntity());
    }

    public async Task Update(PatientTransferRequest patientTransferRequest)
    {
        try
        {
            await UpdatePatientInfo(patientTransferRequest.PatientDetails);
            await UpdatePatientTransferInfo(patientTransferRequest.PatientTransferInfo);
            await UpdateAdditionalInfo(patientTransferRequest.AdditionalInfo);
            await UpdateComorbiditiesAndRiskScore(patientTransferRequest.ComorbiditiesAndRiskScore);
            await _unitOfWork.CommitAsync();
        }
        catch (Exception)
        {
            throw;
        }
    }

    private const int DefaultPageSize = 10;
    private const short InPatientTransferType = 2;

    public async Task<(IEnumerable<PatientTransferRequest> Items, int TotalCount)> GetList(int page, int pageSize, string? caseManager = null, DateTime? transferDateFrom = null, DateTime? transferDateTo = null)
    {
        page = page < 1 ? 1 : page;
        pageSize = pageSize <= 0 ? DefaultPageSize : pageSize;
        // Normalize date range
        DateTime? from = transferDateFrom?.Date;
        DateTime? to = transferDateTo?.Date;
        if (from.HasValue && to.HasValue && from > to)
            (from, to) = (to, from);

        var baseQuery = _unitOfWork.PatientTransferInfoRepository
            .Query(x => x.IsActive && x.TransferType == InPatientTransferType);

        var filteredQuery = baseQuery
            .StartBuilder()
            .ByContains(QueryPropertyNames.CaseManager, caseManager)
            .ByDateFrom(QueryPropertyNames.TransferDate, from)
            .ByDateTo(QueryPropertyNames.TransferDate, to)
            .Build();

        // total count AFTER applying filters
        var totalCount = filteredQuery.Count();

        var pagedTransfers = filteredQuery
            .OrderByDescending(x => x.CreatedOn)
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToList();

        var items = pagedTransfers.Select(x => new PatientTransferRequest
        {
            Id = x.UId,
            PatientTransferInfo = x.ToCoreModel(),
        }).ToList();

        return (items, totalCount);
    }

    public async Task<PatientTransferRequest> Get(Guid uid)
    {
        var transferInfo = await GetTransferInfoAsync(uid);
        var patientDetails = await GetPatientDetailsAsync(uid);
        var additionalInfo = await GetAdditionalInfoAsync(uid);
        var comorbiditiesAndRiskScore = await GetComorbiditiesAndRiskScore(uid);
        return new PatientTransferRequest
        {
            AdditionalInfo = additionalInfo.ToCoreModel(),
            PatientDetails = patientDetails.ToCoreModel(),
            PatientTransferInfo = transferInfo.ToCoreModel(),
            ComorbiditiesAndRiskScore = comorbiditiesAndRiskScore.ToCoreModel(),
            Id = uid
        };
    }

    private async Task<TransferCenterDbStore.Entities.PatientTransferInfo> GetTransferInfoAsync(Guid uid)
    {
        return await _unitOfWork.PatientTransferInfoRepository.GetAsync(x => x.UId == uid);
    }

    private async Task<TransferCenterDbStore.Entities.PatientDetails> GetPatientDetailsAsync(Guid uid)
    {
        return await _unitOfWork.PatientDetailsRepository.GetAsync(x => x.UId == uid);
    }

    private async Task<TransferCenterDbStore.Entities.AdditionalInfo> GetAdditionalInfoAsync(Guid uid)
    {
          return await _unitOfWork.AdditionalInfoRepository.GetAsync(x => x.UId == uid);
    }
    
    private async Task<TransferCenterDbStore.Entities.ComorbiditiesAndRiskScore> GetComorbiditiesAndRiskScore(Guid uid)
    {
        return await _unitOfWork.ComorbiditiesAndRiskScoreRepository.GetAsync(x => x.UId == uid);
    }

    private async Task UpdatePatientInfo(PatientDetails patientDetails)
    {
        if (patientDetails != null)
        {
            patientDetails.LastUpdatedOn = DateTime.UtcNow;
            _unitOfWork.PatientDetailsRepository.Update(patientDetails.ToEntity());
        }
    }

    private async Task UpdatePatientTransferInfo(PatientTransferInfo patientTransferInfo)
    {
        if (patientTransferInfo != null)
        {
            patientTransferInfo.LastUpdatedOn = DateTime.UtcNow;
            _unitOfWork.PatientTransferInfoRepository.Update(patientTransferInfo.ToEntity());
        }
    }

    private async Task UpdateComorbiditiesAndRiskScore(ComorbiditiesAndRiskScore comorbiditiesAndRiskScore)
    {
        if (comorbiditiesAndRiskScore != null)
        {
            comorbiditiesAndRiskScore.LastUpdatedOn = DateTime.UtcNow;
            _unitOfWork.ComorbiditiesAndRiskScoreRepository.Update(comorbiditiesAndRiskScore.ToEntity());
        }
    }
    
    private async Task UpdateAdditionalInfo(AdditionalInfo additionalInfo)
    {
        if (additionalInfo != null)
        {
            additionalInfo.LastUpdatedOn = DateTime.UtcNow;
            _unitOfWork.AdditionalInfoRepository.Update(additionalInfo.ToEntity());
        }
    }
}