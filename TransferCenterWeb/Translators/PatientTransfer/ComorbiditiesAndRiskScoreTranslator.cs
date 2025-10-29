using TransferCenterWeb.Models.PatientTransfer;

namespace TransferCenterWeb.Translators.PatientTransfer;

public static class ComorbiditiesAndRiskScoreTranslator
{
    public static ComorbiditiesAndRiskScore ToWebModel(this TransferCenterCore.Models.ComorbiditiesAndRiskScore source)
    {
        if (source == null) return null!;

        return new ComorbiditiesAndRiskScore
        {
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
            TotalPoints = GetValueOrDefault(source.TotalPoints),
            IsActive = source.IsActive,
            Id = source.Id,
            UId = source.Uid
        };
    }

    public static TransferCenterCore.Models.ComorbiditiesAndRiskScore ToCoreModel(this ComorbiditiesAndRiskScore source, Guid guid)
    {
        if (source == null) return null!;

        return new TransferCenterCore.Models.ComorbiditiesAndRiskScore
        {
            Id = source.Id,
            Uid = guid,
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
            IsActive = source.IsActive,
        };
    }

    private static int GetValueOrDefault(int? value)
    {
        return value ?? 0;
    }
}