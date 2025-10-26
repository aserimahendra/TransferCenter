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
    public GlobalTransferService(IUnitOfWork unitOfWork, IConfiguration configuration)
    {
        _unitOfWork = unitOfWork;
        _configuration = configuration;
    }
    public bool Delete(GlobalPatientTransferRequest patientTransferViewModel)
    {
        throw new NotImplementedException();
    }

    public async Task Save(GlobalPatientTransferRequest patientTransferViewModel)
    {
        try
        {
            // wait for all tasks to complete if needed
            await SavePatientInfo(patientTransferViewModel.PatientInfo);
            await SavePatientTransferInfo(patientTransferViewModel.TransferInfo);
            await SaveAdditionalInfoInfo(patientTransferViewModel.AdditionalInfo);
        }
        catch (Exception)
        {
            throw;
        }

    }

    private async Task SavePatientInfo(Models.PatientDetails patientDetails)
    {
        _unitOfWork.PatientDetailsRepository.Add(patientDetails.ToEntity());
        await _unitOfWork.CommitAsync();
    }
    private async Task SavePatientTransferInfo(Models.PatientTransferInfo patientTransferInfo)
    {
        _unitOfWork.PatientTransferInfoRepository.Add(patientTransferInfo.ToEntity());
        await _unitOfWork.CommitAsync();
    }
    private async Task SaveAdditionalInfoInfo(Models.AdditionalInfo additionalInfo)
    {
        _unitOfWork.AdditionalInfoRepository.Add(additionalInfo.ToEntity());
        await _unitOfWork.CommitAsync();
    }
    public async Task Update(GlobalPatientTransferRequest patientTransferViewModel)
    {
        try
        {
            await UpdatePatientInfo(patientTransferViewModel.PatientInfo);
            await UpdatePatientTransferInfo(patientTransferViewModel.TransferInfo);
            await UpdateAdditionalInfo(patientTransferViewModel.AdditionalInfo);
        }
        catch (Exception)
        {
            throw;
        }
    }

    public async Task<List<GlobalPatientTransferRequest>> GetList()
    {
        var transferInfo = await _unitOfWork.PatientTransferInfoRepository.GetAllAsync();
        return transferInfo.Select(x => new GlobalPatientTransferRequest() { Id= x.UId,TransferInfo = x.ToCoreModel() }).ToList();
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

    private async Task UpdatePatientInfo(Models.PatientDetails patientDetails)
    {
        _unitOfWork.PatientDetailsRepository.Update(patientDetails.ToEntity());
        await _unitOfWork.CommitAsync();
    }

    private async Task UpdatePatientTransferInfo(Models.PatientTransferInfo patientTransferInfo)
    {
        _unitOfWork.PatientTransferInfoRepository.Update(patientTransferInfo.ToEntity());
        await _unitOfWork.CommitAsync();
    }

    private async Task UpdateAdditionalInfo(Models.AdditionalInfo additionalInfo)
    {
        _unitOfWork.AdditionalInfoRepository.Update(additionalInfo.ToEntity());
        await _unitOfWork.CommitAsync();
    }

}