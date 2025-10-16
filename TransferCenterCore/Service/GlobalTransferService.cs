using Microsoft.Extensions.Configuration;
using TransferCenterCore.Interface;
using TransferCenterCore.Models;
using TransferCenterCore.Translators;
using TransferCenterDbStore.UnitOfWork;

namespace TransferCenterCore.Service
{
    public class GlobalTransferService : IGlobalTransferService
    {
        public IUnitOfWork _unitOfWork;
        readonly IConfiguration _configuration;
        public GlobalTransferService(IUnitOfWork unitOfWork, IConfiguration configuration)
        {
            _unitOfWork = unitOfWork;
            _configuration = configuration;
        }
        public bool Delete(PatientTransferViewModel patientTransferViewModel)
        {
            throw new NotImplementedException();
        }

        public Task Save(PatientTransferViewModel patientTransferViewModel)
        {
            try
            {
                // wait for all tasks to complete if needed
                Task.WhenAll(SavePatientInfo(patientTransferViewModel.PatientInfo), SavePatientTransferInfo(patientTransferViewModel.TransferInfo), SaveAdditionalInfoInfo(patientTransferViewModel.AdditionalInfo));
                return Task.CompletedTask;
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
        public bool Update(PatientTransferViewModel patientTransferViewModel)
        {
            throw new NotImplementedException();
        }

        public async Task<List<PatientTransferViewModel>> GetList()
        {
            var transferInfo = await _unitOfWork.PatientTransferInfoRepository.GetAllAsync();
            return transferInfo.Select(x => new PatientTransferViewModel() { Id= x.UId,TransferInfo = x.ToCoreModel() }).ToList();
        }

        public async Task<PatientTransferViewModel> Get(Guid uid)
        {
            var transferInfo = await GetTransferInfoAsync(uid);
            var patientDetails = await  GetPatientDetailsAsync(uid);
            var additionalInfo = await GetAdditionalInfoAsync(uid);

            return new PatientTransferViewModel()
            {
                AdditionalInfo = additionalInfo.ToCoreModel(),
                PatientInfo = patientDetails.ToCoreModel(),
                TransferInfo = transferInfo.ToCoreModel(),
                Id = uid,
            };
        }

        private async Task<TransferCenterDbStore.Entity.PatientTransferInfo> GetTransferInfoAsync(Guid uid)
        {
            return await _unitOfWork.PatientTransferInfoRepository.GetAsync(x => x.UId == uid);
        }

        private async Task<TransferCenterDbStore.Entity.PatientDetails> GetPatientDetailsAsync(Guid uid)
        {
            return await _unitOfWork.PatientDetailsRepository.GetAsync(x => x.UId == uid);
        }

        private async Task<TransferCenterDbStore.Entity.AdditionalInfo> GetAdditionalInfoAsync(Guid uid)
        {
            return await _unitOfWork.AdditionalInfoRepository.GetAsync(x => x.UId == uid);
        }

    }
}
