
namespace TransferCenterCore.Translators
{
    public static class AdditionalInfoTranslator
    {
        // Web to Core
        public static TransferCenterDbStore.Entity.AdditionalInfo ToEntity(this Models.AdditionalInfo source)
        {
            if (source == null) return null!;

            return new TransferCenterDbStore.Entity.AdditionalInfo
            {
                Id = source.Id,
                ServicesAvailable = source.ServicesAvailable,
                SitterRequired = source.SitterRequired,
                VTIBDrips = source.VTIBDrips,
                Dialysis = source.Dialysis,
                PFCTTransfer = source.PFCTTransfer,
                CovidWithin3Days = source.CovidWithin3Days,

                FaceSheet = (TransferCenterDbStore.Entity.DocumentStatus)source.FaceSheet,
                HAndP = (TransferCenterDbStore.Entity.DocumentStatus)source.HAndP,
                CovidTestResults = (TransferCenterDbStore.Entity.DocumentStatus)source.CovidTestResults,
                TransferOrder = (TransferCenterDbStore.Entity.DocumentStatus)source.TransferOrder,
                ProgressNotes = (TransferCenterDbStore.Entity.DocumentStatus)source.ProgressNotes,
                ConsultationNotes = (TransferCenterDbStore.Entity.DocumentStatus)source.ConsultationNotes,
                MostRecentLabResults = (TransferCenterDbStore.Entity.DocumentStatus)source.MostRecentLabResults,
                RadiologyResults = (TransferCenterDbStore.Entity.DocumentStatus)source.RadiologyResults,
                MedicationList = (TransferCenterDbStore.Entity.DocumentStatus)source.MedicationList,
                TreatmentsAndProceduresInED = (TransferCenterDbStore.Entity.DocumentStatus)source.TreatmentsAndProceduresInED,
                InsuranceAuthorization = (TransferCenterDbStore.Entity.DocumentStatus)source.InsuranceAuthorization,

                OtherNotes = source.OtherNotes,
                CodeStatus = source.CodeStatus
            };
        }

        // Core to Web
        public static Models.AdditionalInfo ToCoreModel(TransferCenterDbStore.Entity.AdditionalInfo source)
        {
            if (source == null) return null!;

            return new Models.AdditionalInfo
            {
                Id = source.Id,
                ServicesAvailable = source.ServicesAvailable,
                SitterRequired = source.SitterRequired,
                VTIBDrips = source.VTIBDrips,
                Dialysis = source.Dialysis,
                PFCTTransfer = source.PFCTTransfer,
                CovidWithin3Days = source.CovidWithin3Days,
                FaceSheet = (Models.DocumentStatus)source.FaceSheet,
                HAndP = (Models.DocumentStatus)source.HAndP,
                CovidTestResults = (Models.DocumentStatus)source.CovidTestResults,
                TransferOrder = (Models.DocumentStatus)source.TransferOrder,
                ProgressNotes = (Models.DocumentStatus)source.ProgressNotes,
                ConsultationNotes = (Models.DocumentStatus)source.ConsultationNotes,
                MostRecentLabResults = (Models.DocumentStatus)source.MostRecentLabResults,
                RadiologyResults = (Models.DocumentStatus)source.RadiologyResults,
                MedicationList = (Models.DocumentStatus)source.MedicationList,
                TreatmentsAndProceduresInED = (Models.DocumentStatus)source.TreatmentsAndProceduresInED,
                InsuranceAuthorization = (Models.DocumentStatus)source.InsuranceAuthorization,
                OtherNotes = source.OtherNotes,
                CodeStatus = source.CodeStatus
            };
        }
    }
}
