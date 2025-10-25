namespace TransferCenterWeb.Models;

    /// <summary>
    /// Section 1: Transferring Facility Information (Top grey bordered box)
    /// </summary>
    public class TransferringFacilityInformation
    {
        /// <summary>TC Staff handling the call.</summary>
        public string TCStaffName { get; set; }

        /// <summary>Date and time when transfer call was received.</summary>
        public DateTime? DateTimeOfCall { get; set; }

        /// <summary>Medical Record Number of patient (MR #).</summary>
        public string MRNumber { get; set; }

        /// <summary>Initial caller’s name.</summary>
        public string InitialCallerName { get; set; }

        /// <summary>Initial caller’s title or designation.</summary>
        public string InitialCallerTitle { get; set; }

        /// <summary>Initial caller’s phone number.</summary>
        public string InitialCallerPhone { get; set; }

        /// <summary>Initial caller’s fax number.</summary>
        public string InitialCallerFax { get; set; }

        /// <summary>Second caller’s name (if applicable).</summary>
        public string SecondCallerName { get; set; }

        /// <summary>Second caller’s title or designation.</summary>
        public string SecondCallerTitle { get; set; }

        /// <summary>Second caller’s phone number.</summary>
        public string SecondCallerPhone { get; set; }

        /// <summary>Second caller’s fax number.</summary>
        public string SecondCallerFax { get; set; }

        /// <summary>Name of the referring facility (hospital or clinic).</summary>
        public string ReferringFacility { get; set; }

        /// <summary>Indicates if referring facility is a High Level of Care (HLOC).</summary>
        public bool IsHLOC { get; set; }

        /// <summary>Name of the referring/primary physician.</summary>
        public string ReferringPrimaryMD { get; set; }

        /// <summary>Direct phone number for the primary MD.</summary>
        public string PrimaryMDDirectPhone { get; set; }

        /// <summary>Name of secondary or specialist physician.</summary>
        public string SecondarySpecialistMD { get; set; }

        /// <summary>Direct phone number for the specialist MD.</summary>
        public string SpecialistMDDirectPhone { get; set; }

        /// <summary>Patient’s admission date at referring facility.</summary>
        public DateTime? AdmissionDate { get; set; }

        /// <summary>Current hospital unit (e.g. ICU, Ward, etc.).</summary>
        public string Unit { get; set; }

        /// <summary>Contact phone number for that unit.</summary>
        public string UnitPhone { get; set; }
    }
