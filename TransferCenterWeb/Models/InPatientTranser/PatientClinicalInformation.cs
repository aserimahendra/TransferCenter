namespace TransferCenterWeb.Models;

/// <summary>
    /// Section 2: Patient Demographics, Clinical Info, and Transfer Details
    /// </summary>
    public class PatientClinicalInformation
    {
        /// <summary>Patient last name.</summary>
        public string PatientLastName { get; set; }

        /// <summary>Patient first name.</summary>
        public string PatientFirstName { get; set; }

        /// <summary>Patient date of birth.</summary>
        public DateTime? DOB { get; set; }

        /// <summary>Patient age in years.</summary>
        public int? AgeYears { get; set; }

        /// <summary>Patient age in months.</summary>
        public int? AgeMonths { get; set; }

        /// <summary>Patient sex (Female, Male, X/NB).</summary>
        public string Sex { get; set; }

        /// <summary>Patient height (units unspecified in form).</summary>
        public double? Height { get; set; }

        /// <summary>Patient weight (in lbs or kgs as per checkbox).</summary>
        public double? Weight { get; set; }

        /// <summary>Diagnosis provided by referring facility.</summary>
        public string Diagnosis { get; set; }

        /// <summary>Plan of treatment or reason for transfer.</summary>
        public string PlanOfTreatmentOrReasonForTransfer { get; set; }

        /// <summary>Level of care (ICU, SDU, MS, Tele, IP to Procedure Area).</summary>
        public string LevelOfCare { get; set; }

        /// <summary>Indicates if sitter is required.</summary>
        public bool Sitter { get; set; }

        /// <summary>Indicates Jehovah Witness (Bloodless) status.</summary>
        public bool JehovahWitness { get; set; }

        /// <summary>Indicates if patient is capitated.</summary>
        public bool Capitated { get; set; }

        /// <summary>Indicates if isolation is required.</summary>
        public bool IsolationRequired { get; set; }

        /// <summary>Specifies isolation type (if applicable).</summary>
        public string IsolationType { get; set; }

        /// <summary>Code status (Full Code, Full DNR, Modified Code).</summary>
        public string CodeStatus { get; set; }

        /// <summary>Glasgow Coma Scale (GCS) score, e.g. “4–6–5 = 15”.</summary>
        public string GCS { get; set; }

        /// <summary>Additional comments from facility.</summary>
        public string Comments { get; set; }

        /// <summary>Additional or supporting information not covered elsewhere.</summary>
        public string AdditionalInformation { get; set; }
    }