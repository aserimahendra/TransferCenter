using TransferCenterCore.Models;

namespace TransferCenterCore.Translators;

public static class TransferRequestTranslator
{
    public static IEnumerable<GlobalPatientTransferRequest> ToGlobalPatientTransferRequestCoreModel(
        this IEnumerable<TransferCenterDbStore.Entities.TransferRequest>? source)
    {
        if (source is null)
        {
            return new List<GlobalPatientTransferRequest>();
        }
        return source.Select(x=>x.ToGlobalPatientTransferRequestCoreModel());
    }
    
    public static IEnumerable<PatientTransferRequest> ToPatientTransferRequestCoreModel(
        this IEnumerable<TransferCenterDbStore.Entities.TransferRequest>? source)
    {
        if (source is null)
        {
            return new List<PatientTransferRequest>();
        }
        return source.Select(x=>x.ToPatientTransferRequestCoreModel());
    }
    
    private static GlobalPatientTransferRequest ToGlobalPatientTransferRequestCoreModel(this TransferCenterDbStore.Entities.TransferRequest? x)
    {
        if (x == null)
            return new GlobalPatientTransferRequest();
        return new GlobalPatientTransferRequest
        {
            Id = x.PatientTransferInfo.UId,
            TransferInfo = x.PatientTransferInfo.ToCoreModel(),
            Name = x.PatientDetails?.Name ?? String.Empty,
        };
    }
    private static PatientTransferRequest ToPatientTransferRequestCoreModel(this TransferCenterDbStore.Entities.TransferRequest? x)
    {
        if (x == null)
            return new PatientTransferRequest();
        return new PatientTransferRequest
        {
            Id = x.PatientTransferInfo.UId,
            PatientTransferInfo = x.PatientTransferInfo.ToCoreModel(),
            Name = x?.PatientDetails.Name ?? String.Empty,
        };
    }
}