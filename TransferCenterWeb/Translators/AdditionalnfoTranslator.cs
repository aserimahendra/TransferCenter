
namespace TransferCenter.Translators
{
    public static class AdditionalInfoTranslator
    {
        // Web to Core
        public static TransferCenterCore.Models.AdditionalInfo ToCoreModel(this TransferCenterWeb.Models.AdditionalInfo source)
        {
            if (source == null) return null!;

            return new TransferCenterCore.Models.AdditionalInfo
            {
                Id = source.Id,
                ServicesAvailable = source.ServicesAvailable,
                SitterRequired = source.SitterRequired,
                VTIBDrips = source.VTIBDrips,
                Dialysis = source.Dialysis,
                PFCTTransfer = source.PFCTTransfer,
                CovidWithin3Days = source.CovidWithin3Days,

                FaceSheet = (TransferCenterCore.Models.DocumentStatus)source.FaceSheet,
                HAndP = (TransferCenterCore.Models.DocumentStatus)source.HAndP,
                CovidTestResults = (TransferCenterCore.Models.DocumentStatus)source.CovidTestResults,
                TransferOrder = (TransferCenterCore.Models.DocumentStatus)source.TransferOrder,
                ProgressNotes = (TransferCenterCore.Models.DocumentStatus)source.ProgressNotes,
                ConsultationNotes = (TransferCenterCore.Models.DocumentStatus)source.ConsultationNotes,
                MostRecentLabResults = (TransferCenterCore.Models.DocumentStatus)source.MostRecentLabResults,
                RadiologyResults = (TransferCenterCore.Models.DocumentStatus)source.RadiologyResults,
                MedicationList = (TransferCenterCore.Models.DocumentStatus)source.MedicationList,
                TreatmentsAndProceduresInED = (TransferCenterCore.Models.DocumentStatus)source.TreatmentsAndProceduresInED,
                InsuranceAuthorization = (TransferCenterCore.Models.DocumentStatus)source.InsuranceAuthorization,

                OtherNotes = source.OtherNotes,
                CodeStatus = source.CodeStatus
            };
        }

        // Core to Web
        public static TransferCenterWeb.Models.AdditionalInfo ToWebModel(this TransferCenterCore.Models.AdditionalInfo source)
        {
            if (source == null) return null!;

            return new TransferCenterWeb.Models.AdditionalInfo
            {
                Id = source.Id,
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
}
