using TransferCenter.Translators;
using TransferCenterCore.Context;

namespace TransferCenterWeb.Translators;

public static class GlobalPatientTransferTranslator
{
    // Web → Core
    public static TransferCenterCore.Models.GlobalPatientTransferRequest ToCoreModel(this TransferCenterWeb.Models.GlobalPatientTransfer.GlobalPatientTransferRequest source)
    {
        if (source == null) return null!;

        return new TransferCenterCore.Models.GlobalPatientTransferRequest
        {
            TransferInfo = PatientTransferInfoTranslator.ToCoreModel(source.PatientTransferInfo, source.Id),
            PatientInfo = PatientDetailsTranslator.ToCoreModel(source.PatientDetails, source.Id),
            AdditionalInfo = AdditionalInfoTranslator.ToCoreModel(source.AdditionalInfo, source.Id)
            
        };
    }

    // Core → Web
    public static TransferCenterWeb.Models.GlobalPatientTransfer.GlobalPatientTransferRequest ToWebModel(this TransferCenterCore.Models.GlobalPatientTransferRequest source)
    {
        if (source == null) return null!;

        return new TransferCenterWeb.Models.GlobalPatientTransfer.GlobalPatientTransferRequest()
        {
            Id = source.Id,
            PatientTransferInfo = PatientTransferInfoTranslator.ToWebModel(source.TransferInfo),
            PatientDetails = PatientDetailsTranslator.ToWebModel(source.PatientInfo),
            AdditionalInfo = AdditionalInfoTranslator.ToWebModel(source.AdditionalInfo)
        };
    }
}