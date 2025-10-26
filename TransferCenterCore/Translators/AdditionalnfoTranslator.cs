
using TransferCenterDbStore.Entities;

namespace TransferCenterCore.Translators;

public static class AdditionalInfoTranslator
{
    // Web to Core
    public static TransferCenterDbStore.Entities.AdditionalInfo ToEntity(this Models.AdditionalInfo source)
    {
        if (source == null) return null!;

        return new TransferCenterDbStore.Entities.AdditionalInfo
        {
            Id = source.Id,
            UId = source.UId,
            ServicesAvailable = source.ServicesAvailable,
            SitterRequired = source.SitterRequired,
            VTIBDrips = source.VTIBDrips,
            Dialysis = source.Dialysis,
            PFCTTransfer = source.PFCTTransfer,
            CovidWithin3Days = source.CovidWithin3Days,
            FaceSheet = source.FaceSheet,
            HAndP = source.HAndP,
            CovidTestResults = source.CovidTestResults,
            TransferOrder = source.TransferOrder,
            ProgressNotes = source.ProgressNotes,
            ConsultationNotes = source.ConsultationNotes,
            MostRecentLabResults = source.MostRecentLabResults,
            RadiologyResults = source.RadiologyResults,
            MedicationList = source.MedicationList,
            TreatmentsAndProceduresInED = source.TreatmentsAndProceduresInED,
            InsuranceAuthorization = source.InsuranceAuthorization,
            OtherNotes = source.OtherNotes,
            CodeStatus = source.CodeStatus,
            IsActive = source.IsActive
        };
    }

    // Core to Web
    public static Models.AdditionalInfo ToCoreModel(this AdditionalInfo? source)
    {
        if (source == null) return null!;

        return new Models.AdditionalInfo
        {
            Id = source.Id,
            UId = source.UId,
            ServicesAvailable = source.ServicesAvailable,
            SitterRequired = source.SitterRequired,
            VTIBDrips = source.VTIBDrips,
            Dialysis = source.Dialysis,
            PFCTTransfer = source.PFCTTransfer,
            CovidWithin3Days = source.CovidWithin3Days,
            FaceSheet = source.FaceSheet,
            HAndP = source.HAndP,
            CovidTestResults = source.CovidTestResults,
            TransferOrder = source.TransferOrder,
            ProgressNotes = source.ProgressNotes,
            ConsultationNotes = source.ConsultationNotes,
            MostRecentLabResults = source.MostRecentLabResults,
            RadiologyResults = source.RadiologyResults,
            MedicationList = source.MedicationList,
            TreatmentsAndProceduresInED = source.TreatmentsAndProceduresInED,
            InsuranceAuthorization = source.InsuranceAuthorization,
            OtherNotes = source.OtherNotes,
            CodeStatus = source.CodeStatus,
            IsActive = source.IsActive
        };
    }
}