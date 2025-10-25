using TransferCenterWeb.Models;
using TransferCenterWeb.Translators.PatientTransfer;

namespace TransferCenterWeb.Translators;

public static class PatientTransferRequestTranslator
{
    // Web → Core
    public static TransferCenterCore.Models.PatientTransferRequest ToCoreModel(this TransferCenterWeb.Models.PatientTransferRequest source)
    {
        if (source == null) return new TransferCenterCore.Models.PatientTransferRequest();

        return new TransferCenterCore.Models.PatientTransferRequest
        {
            PatientTransferInfo = Translators.PatientTransfer.PatientTransferInfoTranslator.ToCoreModel(source.PatientTransferInfo, source.Id),
            PatientDetails = Translators.PatientTransfer.PatientDetailsTranslator.ToCoreModel(source.PatientDetails, source.Id),
            AdditionalInfo = Translators.PatientTransfer.AdditionalInfoTranslator.ToCoreModel(source.AdditionalInfo, source.Id)
        };
    }

    // Core → Web
    public static TransferCenterWeb.Models.PatientTransferRequest ToWebModel(this TransferCenterCore.Models.PatientTransferRequest source)
    {
        if (source == null) return new PatientTransferRequest();

        return new TransferCenterWeb.Models.PatientTransferRequest()
        {
            Id = source.Id,
            PatientTransferInfo = Translators.PatientTransfer.PatientTransferInfoTranslator.ToWebModel(source.PatientTransferInfo),
            PatientDetails = Translators.PatientTransfer.PatientDetailsTranslator.ToWebModel(source.PatientDetails),
            AdditionalInfo = AdditionalInfoTranslator.ToWebModel(source.AdditionalInfo)
        };
    }
}