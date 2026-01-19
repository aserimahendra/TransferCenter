using Microsoft.Extensions.Configuration;
using TransferCenterCore.Interfaces;
using TransferCenterCore.Models;
using TransferCenterCore.Translators;
using TransferCenterDbStore.UnitOfWork;

namespace TransferCenterCore.Services;

public class GlobalTransferService : IGlobalTransferService
{
    public IUnitOfWork _unitOfWork;
    readonly IConfiguration _configuration;
    private const int DefaultPageSize = 10;
    private const short GlobalTransferType = 1;
    public GlobalTransferService(IUnitOfWork unitOfWork, IConfiguration configuration)
    {
        _unitOfWork = unitOfWork;
        _configuration = configuration;
    }
    public async Task Delete(GlobalPatientTransferRequest patientTransferViewModel)
    {
        patientTransferViewModel.IsActive = false;
        patientTransferViewModel.AdditionalInfo.IsActive = false;
        patientTransferViewModel.PatientInfo.IsActive = false;
        patientTransferViewModel.TransferInfo.IsActive = false;
        await Update(patientTransferViewModel);
    }
    public async Task<(IEnumerable<GlobalPatientTransferRequest> Items, int TotalCount)> GetList(string? caseMgrSwRn, DateTime? transferDateFrom, DateTime? transferDateTo, string? name = null)
    {
        var (items, totalCount) = await _unitOfWork.TransferRequestRepository.GetList(GlobalTransferType , caseMgrSwRn, transferDateFrom, transferDateTo, name);
        var resultItems = 
            items.Select(x=>new GlobalPatientTransferRequest()
            {
                AdditionalInfo = x.AdditionalInfo.ToCoreModel(),
                PatientInfo = x.PatientDetails.ToCoreModel(),
                TransferInfo = x.PatientTransferInfo.ToCoreModel(),
            }).ToList();
        return (resultItems, totalCount);
    }
    public async Task Save(GlobalPatientTransferRequest patientTransferViewModel)
    {
        try
        {
            // wait for all tasks to complete if needed
            await SavePatientInfo(patientTransferViewModel.PatientInfo);
            await SavePatientTransferInfo(patientTransferViewModel.TransferInfo);
            await SaveAdditionalInfoInfo(patientTransferViewModel.AdditionalInfo);
            await _unitOfWork.CommitAsync();
        }
        catch (Exception)
        {
            throw;
        }

    }
    public async Task Update(GlobalPatientTransferRequest patientTransferViewModel)
    {
        try
        {
            // wait for all tasks to complete if needed
            await UpdatePatientInfo(patientTransferViewModel.PatientInfo);
            await UpdatePatientTransferInfo(patientTransferViewModel.TransferInfo);
            await UpdateAdditionalInfoInfo(patientTransferViewModel.AdditionalInfo);
            await _unitOfWork.CommitAsync();

        }
        catch (Exception)
        {
            throw;
        }
    }
    public async Task<(IEnumerable<GlobalPatientTransferRequest> Items, int TotalCount)> GetList(int page, int pageSize, string? caseMgrSwRn, DateTime? transferDateFrom, DateTime? transferDateTo, string? name = null)
    {
        var (items, totalCount) = await _unitOfWork.TransferRequestRepository.GetList(GlobalTransferType ,page, pageSize, caseMgrSwRn, transferDateFrom, transferDateTo, name);
        var resultItems = items.ToGlobalPatientTransferRequestCoreModel();
        return (resultItems, totalCount);
    }
    public async Task<GlobalPatientTransferRequest> Get(Guid uid)
    {
        var transferInfo = await GetTransferInfoAsync(uid);
        var patientDetails = await  GetPatientDetailsAsync(uid);
        var additionalInfo = await GetAdditionalInfoAsync(uid);

        return new GlobalPatientTransferRequest()
        {
            AdditionalInfo = additionalInfo.ToCoreModel(),
            PatientInfo = patientDetails.ToCoreModel(),
            TransferInfo = transferInfo.ToCoreModel(),
            Id = uid,
        };
    }
    private async Task SavePatientInfo(Models.PatientDetails patientDetails)
    {
        patientDetails.CreatedBy = string.IsNullOrWhiteSpace(patientDetails.CreatedBy)
            ? "system"
            : patientDetails.CreatedBy;
        patientDetails.CreatedOn = DateTime.UtcNow;
        _unitOfWork.PatientDetailsRepository.Add(patientDetails.ToEntity());
    }
    private async Task SavePatientTransferInfo(Models.PatientTransferInfo patientTransferInfo)
    {
        patientTransferInfo.CreatedBy = string.IsNullOrWhiteSpace(patientTransferInfo.CreatedBy)
            ? "system"
            : patientTransferInfo.CreatedBy;
        patientTransferInfo.CreatedOn = DateTime.UtcNow;
        _unitOfWork.PatientTransferInfoRepository.Add(patientTransferInfo.ToEntity());
    }
    private async Task SaveAdditionalInfoInfo(Models.AdditionalInfo additionalInfo)
    {
        additionalInfo.CreatedBy = string.IsNullOrWhiteSpace(additionalInfo.CreatedBy)
            ? "system"
            : additionalInfo.CreatedBy;
        additionalInfo.CreatedOn = DateTime.UtcNow;
        _unitOfWork.AdditionalInfoRepository.Add(additionalInfo.ToEntity());
    }
    private async Task UpdatePatientInfo(Models.PatientDetails patientDetails)
    {
        patientDetails.LastUpdatedOn = DateTime.UtcNow;
        _unitOfWork.PatientDetailsRepository.Update(patientDetails.ToEntity());
    }
    private async Task UpdatePatientTransferInfo(Models.PatientTransferInfo patientTransferInfo)
    {
        patientTransferInfo.LastUpdatedOn = DateTime.UtcNow;
        _unitOfWork.PatientTransferInfoRepository.Update(patientTransferInfo.ToEntity());
    }
    private async Task UpdateAdditionalInfoInfo(Models.AdditionalInfo additionalInfo)
    {
        additionalInfo.LastUpdatedOn = DateTime.UtcNow;
        _unitOfWork.AdditionalInfoRepository.Update(additionalInfo.ToEntity());
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
}