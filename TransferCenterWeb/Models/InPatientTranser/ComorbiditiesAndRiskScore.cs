namespace TransferCenterWeb.Models;

    /// <summary>
    /// Comorbidities and Risk Scoring (RF Score section)
    /// </summary>
    public class ComorbiditiesAndRiskScore
    {
        /// <summary>No comorbidities (0 points).</summary>
        public bool None { get; set; }

        /// <summary>Stroke/TIA (1 point).</summary>
        public bool StrokeTIA { get; set; }

        /// <summary>Congestive Heart Failure (1 point).</summary>
        public bool CHF { get; set; }

        /// <summary>Chronic Kidney Disease/End-Stage Renal Disease (1 point).</summary>
        public bool CKD_ESRD { get; set; }

        /// <summary>Hypertension (1 point).</summary>
        public bool HTN { get; set; }

        /// <summary>Diabetes or skin issues (1 point).</summary>
        public bool DiabetesOrSkinIssues { get; set; }

        /// <summary>Myocardial Infarction, Angina, or CAD (1 point each).</summary>
        public bool MI_Angina_CAD { get; set; }

        /// <summary>COPD or Respiratory Failure (1 point each).</summary>
        public bool COPDOrRespiratoryFailure { get; set; }

        /// <summary>Patient is ventilated (ETT/Trach) (1 point).</summary>
        public bool Ventilated { get; set; }

        /// <summary>Pulmonary Hypertension (2 points).</summary>
        public bool PulmonaryHTN { get; set; }

        /// <summary>Pulmonary Hypertension on prostacyclin (2 points).</summary>
        public bool PulmonaryHTN_Prostacyclin { get; set; }

        /// <summary>Immunocompromised / HIV (1 point).</summary>
        public bool ImmunocompromisedOrHIV { get; set; }

        /// <summary>Psychiatric background (2 points).</summary>
        public bool PsychBackground { get; set; }

        /// <summary>Non-compliant with care (1 point).</summary>
        public bool NonCompliantWithCare { get; set; }

        /// <summary>Unable to perform ADLs (1 point).</summary>
        public bool UnableToPerformADLs { get; set; }

        /// <summary>Drug or alcohol dependence (2 points each).</summary>
        public bool DrugOrAlcoholDependence { get; set; }

        /// <summary>Lymphoma, Leukemia, or Cancer (1 point).</summary>
        public bool LymphomaLeukemiaCancer { get; set; }

        /// <summary>Malnutrition, obesity, or digestive disease (1 point).</summary>
        public bool MalnutritionObesityDigestiveDisease { get; set; }

        /// <summary>Unconscious/ALOC (2 points).</summary>
        public bool UnconsciousOrALOC { get; set; }

        /// <summary>Length of stay > 2 weeks (16 points).</summary>
        public bool LOSMoreThan2Weeks { get; set; }

        /// <summary>Out of service area (10 points).</summary>
        public bool OutOfServiceArea { get; set; }

        /// <summary>DNR Code Status (10 points).</summary>
        public bool DNRCodeStatus { get; set; }

        /// <summary>COVID Positive (16 points).</summary>
        public bool CovidPositive { get; set; }

        /// <summary>Recent surgery performed at UCI (2 points).</summary>
        public bool RecentSurgeryAtUCI { get; set; }

        /// <summary>Recent surgery NOT performed at UCI and related to transfer reason (16 points).</summary>
        public bool RecentSurgeryOutsideUCI { get; set; }

        /// <summary>Total calculated risk factor points (for internal use).</summary>
        public int? TotalPoints { get; set; }
    }
