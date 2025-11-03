using System.ComponentModel.DataAnnotations;

namespace TransferCenterWeb.Models.PatientTransfer;

    /// <summary>
    /// Comorbidities and Risk Scoring (RF Score section)
    /// </summary>
    public class ComorbiditiesAndRiskScore : AuditLogMeta
    {
        public Guid UId { get; set; }

        public long Id { get; set; }

        [Display(Name = "None (0 points)")]
        public bool None { get; set; }

        [Display(Name = "Stroke/TIA (1 point)")]
        public bool StrokeTIA { get; set; }

        [Display(Name = "CHF (1 point)")]
        public bool CHF { get; set; }

        [Display(Name = "CKD/ESRD (1 point)")]
        public bool CKD_ESRD { get; set; }

        [Display(Name = "HTN (1 point)")]
        public bool HTN { get; set; }

        [Display(Name = "Diabetes / Skin Issues (1 point)")]
        public bool DiabetesOrSkinIssues { get; set; }

        [Display(Name = "MI/ANGINA/CAD (1 point each)")]
        public bool MI_Angina_CAD { get; set; }

        [Display(Name = "COPD / Respiratory Failure (1 point each)")]
        public bool COPDOrRespiratoryFailure { get; set; }

        [Display(Name = "Ventilated (ETT / Trach) (1 point)")]
        public bool Ventilated { get; set; }

        [Display(Name = "Pulmonary HTN (2 points)")]
        public bool PulmonaryHTN { get; set; }

        [Display(Name = "Pulmonary HTN on Prostacyclin (2 points)")]
        public bool PulmonaryHTN_Prostacyclin { get; set; }

        [Display(Name = "Immunocompromised / HIV (1 point)")]
        public bool ImmunocompromisedOrHIV { get; set; }

        [Display(Name = "Psych Background (2 points)")]
        public bool PsychBackground { get; set; }

        [Display(Name = "Non-Compliant with Care (1 point)")]
        public bool NonCompliantWithCare { get; set; }

        [Display(Name = "Unable to Perform ADL’s (1 point)")]
        public bool UnableToPerformADLs { get; set; }

        [Display(Name = "Drug / Alcohol Dependence (2 points each)")]
        public bool DrugOrAlcoholDependence { get; set; }

        [Display(Name = "Lymphoma / Leukemia / Cancer (1 point)")]
        public bool LymphomaLeukemiaCancer { get; set; }

        [Display(Name = "Malnutrition / Obesity / Digestive Disease (1 point)")]
        public bool MalnutritionObesityDigestiveDisease { get; set; }

        [Display(Name = "Unconscious / ALOC (2 points)")]
        public bool UnconsciousOrALOC { get; set; }

        [Display(Name = "LOS > 2 Weeks (16 points)")]
        public bool LOSMoreThan2Weeks { get; set; }

        [Display(Name = "Out of Service Area (10 points)")]
        public bool OutOfServiceArea { get; set; }

        [Display(Name = "DNR Code Status (10 points)")]
        public bool DNRCodeStatus { get; set; }

        [Display(Name = "COVID Positive (16 points)")]
        public bool CovidPositive { get; set; }

        [Display(Name = "Recent Surgery performed at UCI (2 points)")]
        public bool RecentSurgeryAtUCI { get; set; }

        [Display(Name = "Recent Surgery NOT performed at UCI & Related to Reason for Transfer (16 points)")]
        public bool RecentSurgeryOutsideUCI { get; set; }

        [Display(Name = "Total Points")]
        public int? TotalPoints { get; set; }

        public bool IsActive { get; set; } = true;
    }
