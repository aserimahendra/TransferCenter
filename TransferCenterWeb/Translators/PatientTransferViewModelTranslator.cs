using TransferCenter.Translators;

namespace TransferCenterWeb.Translators
{
    public static class PatientTransferViewModelTranslator
    {
        // Web → Core
        public static TransferCenterCore.Models.PatientTransferViewModel ToCoreModel(this TransferCenterWeb.Models.PatientTransferViewModel source)
        {
            if (source == null) return null!;

            return new TransferCenterCore.Models.PatientTransferViewModel
            {
                TransferInfo = PatientTransferInfoTranslator.ToCoreModel(source.PatientTransferInfo, source.Id),
                PatientInfo = PatientDetailsTranslator.ToCoreModel(source.PatientDetails, source.Id),
                AdditionalInfo = AdditionalInfoTranslator.ToCoreModel(source.AdditionalInfo, source.Id)
            };
        }

        // Core → Web
        public static TransferCenterWeb.Models.PatientTransferViewModel ToWebModel(this TransferCenterCore.Models.PatientTransferViewModel source)
        {
            if (source == null) return null!;

            return new TransferCenterWeb.Models.PatientTransferViewModel()
            {
                Id = source.Id,
                PatientTransferInfo = PatientTransferInfoTranslator.ToWebModel(source.TransferInfo),
                PatientDetails = PatientDetailsTranslator.ToWebModel(source.PatientInfo),
                AdditionalInfo = AdditionalInfoTranslator.ToWebModel(source.AdditionalInfo)
            };
        }
    }
}
