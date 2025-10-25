using System;

namespace TransferCenter.Translators;

public static class AdditionalInfoTranslator
{
    // Web to Core
    public static TransferCenterCore.Models.AdditionalInfo ToCoreModel(this TransferCenterWeb.Models.GlobalPatientTransfer.AdditionalInfo source, Guid guid)
    {
        if (source == null) return null!;

        return new TransferCenterCore.Models.AdditionalInfo
        {
            Id = source.Id,
            UId= guid,
            ServicesAvailable = source.ServicesAvailable,
            SitterRequired = source.SitterRequired,
            VTIBDrips = source.VTIBDrips,
            Dialysis = source.Dialysis,
            PFCTTransfer = source.PFCTTransfer,
            CovidWithin3Days = source.CovidWithin3Days,
            FaceSheet = (short)source.FaceSheet,
            HAndP = (short)source.HAndP,
            CovidTestResults = (short)source.CovidTestResults,
            TransferOrder = (short)source.TransferOrder,
            ProgressNotes = (short)source.ProgressNotes,
            ConsultationNotes = (short)source.ConsultationNotes,
            MostRecentLabResults = (short)source.MostRecentLabResults,
            RadiologyResults = (short)source.RadiologyResults,
            MedicationList = (short)source.MedicationList,
            TreatmentsAndProceduresInED = (short)source.TreatmentsAndProceduresInED,
            InsuranceAuthorization = (short)source.InsuranceAuthorization,

            OtherNotes = source.OtherNotes,
            CodeStatus = source.CodeStatus
        };
    }

    // Core to Web
    public static TransferCenterWeb.Models.GlobalPatientTransfer.AdditionalInfo ToWebModel(this TransferCenterCore.Models.AdditionalInfo source)
    {
        if (source == null) return null!;

        return new TransferCenterWeb.Models.GlobalPatientTransfer.AdditionalInfo
        {
            Id = source.Id,
            UId = source.UId,
            ServicesAvailable = source.ServicesAvailable,
            SitterRequired = source.SitterRequired,
            VTIBDrips = source.VTIBDrips,
            Dialysis = source.Dialysis,
            PFCTTransfer = source.PFCTTransfer,
            CovidWithin3Days = source.CovidWithin3Days,
            FaceSheet = (TransferCenterWeb.Models.DocumentStatus)source.FaceSheet,
            HAndP = (TransferCenterWeb.Models.DocumentStatus)source.HAndP,
            CovidTestResults = (TransferCenterWeb.Models.DocumentStatus)source.CovidTestResults,
            TransferOrder = (TransferCenterWeb.Models.DocumentStatus)source.TransferOrder,
            ProgressNotes = (TransferCenterWeb.Models.DocumentStatus)source.ProgressNotes,
            ConsultationNotes = (TransferCenterWeb.Models.DocumentStatus)source.ConsultationNotes,
            MostRecentLabResults = (TransferCenterWeb.Models.DocumentStatus)source.MostRecentLabResults,
            RadiologyResults = (TransferCenterWeb.Models.DocumentStatus)source.RadiologyResults,
            MedicationList = (TransferCenterWeb.Models.DocumentStatus)source.MedicationList,
            TreatmentsAndProceduresInED = (TransferCenterWeb.Models.DocumentStatus)source.TreatmentsAndProceduresInED,
            InsuranceAuthorization = (TransferCenterWeb.Models.DocumentStatus)source.InsuranceAuthorization,
            OtherNotes = source.OtherNotes,
            CodeStatus = source.CodeStatus
        };
    }
}