using System;
using TransferCenterWeb.Models.PatientTransfer;
using AdditionalInfo = TransferCenterWeb.Models.PatientTransfer.AdditionalInfo;

namespace TransferCenterWeb.Translators.PatientTransfer
{
    public static class AdditionalInfoTranslator
    {
        // Web to Core
        public static TransferCenterCore.Models.AdditionalInfo ToCoreModel(this AdditionalInfo source, Guid guid)
        {
            if (source == null) return new  TransferCenterCore.Models.AdditionalInfo();

            return new TransferCenterCore.Models.AdditionalInfo
            {
                Id = source.Id,
                UId = guid,
                Dialysis = source.Dialysis,
                VTIBDrips = source.VTIBDrips,
                ServicesAvailable = source.ServicesAvailable,
                LifeImageUploadRequested = (short)source.LifeImageUploadRequested,
                PFCTTransfer = source.PFCTTransfer,
                SendingFacilityUnableToUseLifeImage = (short)source.SendingFacilityUnableToUseLifeImage,
                ColdOrFluSymptoms = source.ColdOrFluSymptoms,
                NewRashUnknownCause = source.NewRashUnknownCause,
                ContactWithCovidPositive = source.ContactWithCovidPositive,
                DiagnosedCovidOrPositiveLab = source.DiagnosedCovidOrPositiveLab,
                CovidDiagnosisDates = source.CovidDiagnosisDates,
                SickHouseholdMembers = source.SickHouseholdMembers,
                ExposedToMeasles = source.ExposedToMeasles,
                TraveledOutsideUS = source.TraveledOutsideUS,
                TraveledArabianPeninsula = source.TraveledArabianPeninsula,
                TraveledAfrica = source.TraveledAfrica,
                HasRespiratoryIllnessAfterTravel = source.HasRespiratoryIllnessAfterTravel,
                AdmittedToKindredHospital = source.AdmittedToKindredHospital,
                MultiDrugResistantInfection = source.MultiDrugResistantInfection,
                Microorganisms = source.Microorganisms,
                CommunicableDisease = source.CommunicableDisease,
                DiseaseConditions = source.DiseaseConditions,
                LabResultsStatus = (short)source.LabResultsStatus,
                DiagnosticsStatus = (short)source.DiagnosticsStatus,
                MedicationListStatus = (short)source.MedicationListStatus,
                FaceSheet = (short)source.FaceSheet,
                HAndP = (short)source.HAndP,
                CovidWithin3Days = source.CovidWithin3Days,
                MostRecentLabResults = (short)source.MostRecentLabResults,
                RadiologyResults = (short)source.RadiologyResults,
                MedicationList = (short)source.MedicationList,
                IsActive = source.IsActive,
                TransferType = (short)Models.TransferType.PatientTransfer
            };
        }

        // Core to Web
        public static AdditionalInfo ToWebModel(this TransferCenterCore.Models.AdditionalInfo source)
        {
            if (source == null) return new AdditionalInfo();

            return new AdditionalInfo
            {
                Id = source.Id,
                UId = source.UId,
                Dialysis = source.Dialysis,
                VTIBDrips = source.VTIBDrips,
                ServicesAvailable = source.ServicesAvailable,
                LifeImageUploadRequested = (DocumentStatus)source.LifeImageUploadRequested,
                PFCTTransfer = source.PFCTTransfer,
                SendingFacilityUnableToUseLifeImage = (DocumentStatus)source.SendingFacilityUnableToUseLifeImage,
                ColdOrFluSymptoms = source.ColdOrFluSymptoms,
                NewRashUnknownCause = source.NewRashUnknownCause,
                ContactWithCovidPositive = source.ContactWithCovidPositive,
                DiagnosedCovidOrPositiveLab = source.DiagnosedCovidOrPositiveLab,
                CovidDiagnosisDates = source.CovidDiagnosisDates,
                SickHouseholdMembers = source.SickHouseholdMembers,
                ExposedToMeasles = source.ExposedToMeasles,
                TraveledOutsideUS = source.TraveledOutsideUS,
                TraveledArabianPeninsula = source.TraveledArabianPeninsula,
                TraveledAfrica = source.TraveledAfrica,
                HasRespiratoryIllnessAfterTravel = source.HasRespiratoryIllnessAfterTravel,
                AdmittedToKindredHospital = source.AdmittedToKindredHospital,
                MultiDrugResistantInfection = source.MultiDrugResistantInfection,
                Microorganisms = source.Microorganisms,
                CommunicableDisease = source.CommunicableDisease,
                DiseaseConditions = source.DiseaseConditions,
                LabResultsStatus = (DocumentStatus)source.LabResultsStatus,
                DiagnosticsStatus = (DocumentStatus)source.DiagnosticsStatus,
                MedicationListStatus = (DocumentStatus)source.MedicationListStatus,
                FaceSheet = (DocumentStatus)source.FaceSheet,
                HAndP = (DocumentStatus)source.HAndP,
                CovidWithin3Days = source.CovidWithin3Days,
                MostRecentLabResults = (DocumentStatus)source.MostRecentLabResults,
                RadiologyResults = (DocumentStatus)source.RadiologyResults,
                MedicationList = (DocumentStatus)source.MedicationList,
                IsActive = source.IsActive
            };
        }
    }
}