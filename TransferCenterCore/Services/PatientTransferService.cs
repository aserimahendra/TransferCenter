using Microsoft.Extensions.Configuration;
using TransferCenterCore.Interfaces;
using TransferCenterCore.Models;
using TransferCenterCore.Translators;
using TransferCenterDbStore.UnitOfWork;

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

    public bool Delete(PatientTransferRequest patientTransferRequest)
    {
        throw new NotImplementedException();
    }

    public async Task Save(PatientTransferRequest patientTransferRequest)
    {
        try
        {
            await SavePatientInfo(patientTransferRequest.PatientDetails);
            await SavePatientTransferInfo(patientTransferRequest.PatientTransferInfo);
            await SaveAdditionalInfo(patientTransferRequest.AdditionalInfo);
        }
        catch (Exception)
        {
            throw;
        }
    }

    private async Task SavePatientInfo(PatientDetails patientDetails)
    {
        _unitOfWork.PatientDetailsRepository.Add(patientDetails.ToEntity());
        await _unitOfWork.CommitAsync();
    }

    private async Task SavePatientTransferInfo(PatientTransferInfo patientTransferInfo)
    {
        _unitOfWork.PatientTransferInfoRepository.Add(patientTransferInfo.ToEntity());
        await _unitOfWork.CommitAsync();
    }

    private async Task SaveAdditionalInfo(AdditionalInfo additionalInfo)
    {
        _unitOfWork.AdditionalInfoRepository.Add(additionalInfo.ToEntity());
        await _unitOfWork.CommitAsync();
    }

    public bool Update(PatientTransferRequest patientTransferRequest)
    {
        throw new NotImplementedException();
    }

    public async Task<List<PatientTransferRequest>> GetList()
    {
        var transferInfo = await _unitOfWork.PatientTransferInfoRepository.GetAllAsync();
        return transferInfo.Select(x => new PatientTransferRequest
        {
            Id = x.UId,
            PatientTransferInfo = x.ToCoreModel()
        }).ToList();
    }

    public async Task<PatientTransferRequest> Get(Guid uid)
    {
        var transferInfo = await GetTransferInfoAsync(uid);
        var patientDetails = await GetPatientDetailsAsync(uid);
        var additionalInfo = await GetAdditionalInfoAsync(uid);

        return new PatientTransferRequest
        {
            AdditionalInfo = additionalInfo.ToCoreModel(),
            PatientDetails = patientDetails.ToCoreModel(),
            PatientTransferInfo = transferInfo.ToCoreModel(),
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
}