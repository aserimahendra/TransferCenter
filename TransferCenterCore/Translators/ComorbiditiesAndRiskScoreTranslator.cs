using TransferCenterCore.Models;
using TransferCenterDbStore.Entities;

namespace TransferCenterCore.Translators;

public static class ComorbiditiesAndRiskScoreTranslator
{
    // Convert from Entity to Core Model
    public static TransferCenterCore.Models.ComorbiditiesAndRiskScore? ToCoreModel(this TransferCenterDbStore.Entities.ComorbiditiesAndRiskScore? source)
    {
        if (source == null) return null;

        return new TransferCenterCore.Models.ComorbiditiesAndRiskScore
        {
            Id = source.Id,
            Uid = source.UId,
            None = source.None,
            StrokeTIA = source.StrokeTIA,
            CHF = source.CHF,
            CKD_ESRD = source.CKD_ESRD,
            HTN = source.HTN,
            DiabetesOrSkinIssues = source.DiabetesOrSkinIssues,
            MI_Angina_CAD = source.MI_Angina_CAD,
            COPDOrRespiratoryFailure = source.COPDOrRespiratoryFailure,
            Ventilated = source.Ventilated,
            PulmonaryHTN = source.PulmonaryHTN,
            PulmonaryHTN_Prostacyclin = source.PulmonaryHTN_Prostacyclin,
            ImmunocompromisedOrHIV = source.ImmunocompromisedOrHIV,
            PsychBackground = source.PsychBackground,
            NonCompliantWithCare = source.NonCompliantWithCare,
            UnableToPerformADLs = source.UnableToPerformADLs,
            DrugOrAlcoholDependence = source.DrugOrAlcoholDependence,
            LymphomaLeukemiaCancer = source.LymphomaLeukemiaCancer,
            MalnutritionObesityDigestiveDisease = source.MalnutritionObesityDigestiveDisease,
            UnconsciousOrALOC = source.UnconsciousOrALOC,
            LOSMoreThan2Weeks = source.LOSMoreThan2Weeks,
            OutOfServiceArea = source.OutOfServiceArea,
            DNRCodeStatus = source.DNRCodeStatus,
            CovidPositive = source.CovidPositive,
            RecentSurgeryAtUCI = source.RecentSurgeryAtUCI,
            RecentSurgeryOutsideUCI = source.RecentSurgeryOutsideUCI,
            TotalPoints = source.TotalPoints,
            Comorbidities = source.Comorbidities,
            RiskScore = source.RiskScore,
            CreatedDate = source.CreatedDate,
            UpdatedDate = source.UpdatedDate,
            IsActive = source.IsActive,
            CreatedBy = source.CreatedBy
        };
    }

    // Convert from Core Model to Entity
    public static TransferCenterDbStore.Entities.ComorbiditiesAndRiskScore? ToEntity(this TransferCenterCore.Models.ComorbiditiesAndRiskScore? source)
    {
        if (source == null) return null;

        return new TransferCenterDbStore.Entities.ComorbiditiesAndRiskScore
        {
            Id = source.Id,
            UId = source.Uid,
            None = source.None,
            StrokeTIA = source.StrokeTIA,
            CHF = source.CHF,
            CKD_ESRD = source.CKD_ESRD,
            HTN = source.HTN,
            DiabetesOrSkinIssues = source.DiabetesOrSkinIssues,
            MI_Angina_CAD = source.MI_Angina_CAD,
            COPDOrRespiratoryFailure = source.COPDOrRespiratoryFailure,
            Ventilated = source.Ventilated,
            PulmonaryHTN = source.PulmonaryHTN,
            PulmonaryHTN_Prostacyclin = source.PulmonaryHTN_Prostacyclin,
            ImmunocompromisedOrHIV = source.ImmunocompromisedOrHIV,
            PsychBackground = source.PsychBackground,
            NonCompliantWithCare = source.NonCompliantWithCare,
            UnableToPerformADLs = source.UnableToPerformADLs,
            DrugOrAlcoholDependence = source.DrugOrAlcoholDependence,
            LymphomaLeukemiaCancer = source.LymphomaLeukemiaCancer,
            MalnutritionObesityDigestiveDisease = source.MalnutritionObesityDigestiveDisease,
            UnconsciousOrALOC = source.UnconsciousOrALOC,
            LOSMoreThan2Weeks = source.LOSMoreThan2Weeks,
            OutOfServiceArea = source.OutOfServiceArea,
            DNRCodeStatus = source.DNRCodeStatus,
            CovidPositive = source.CovidPositive,
            RecentSurgeryAtUCI = source.RecentSurgeryAtUCI,
            RecentSurgeryOutsideUCI = source.RecentSurgeryOutsideUCI,
            TotalPoints = source.TotalPoints,
            Comorbidities = source.Comorbidities,
            RiskScore = source.RiskScore,
            CreatedDate = source.CreatedDate,
            CreatedBy = source.CreatedBy,
            UpdatedDate = source.UpdatedDate,
            IsActive = source.IsActive,
            LastUpdatedOn = source.LastUpdatedOn
        };
    }
}