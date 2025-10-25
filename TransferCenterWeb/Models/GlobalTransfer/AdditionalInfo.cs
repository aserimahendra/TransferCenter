using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace TransferCenterWeb.Models
{
    public class AdditionalInfo
    {
        public Guid UId { get; set; }

        public long Id { get; set; }

        [Required]
        [DisplayName("Services Available")]
        public bool ServicesAvailable { get; set; }

        [Required]
        [DisplayName("Sitter Required")]
        public bool SitterRequired { get; set; }

        [Required]
        [DisplayName("Vent/Trach/Intubated/BipAP/Drips")]
        public bool VTIBDrips { get; set; }

        [Required]
        [DisplayName("Dialysis")]
        public bool Dialysis { get; set; }

        [Required]
        [DisplayName("Patient/Family Consent To Transfer")]
        public bool PFCTTransfer { get; set; }

        [Required]
        [DisplayName("COVID Within 3 Days")]
        public bool CovidWithin3Days { get; set; }

        [Required]
        [DisplayName("Face Sheet")]
        public DocumentStatus FaceSheet { get; set; }

        [Required]
        [DisplayName("H&P")]
        public DocumentStatus HAndP { get; set; }

        [Required]
        [DisplayName("COVID Test Results")]
        public DocumentStatus CovidTestResults { get; set; }


        [Required]
        [DisplayName("Transfer Order")]
        public DocumentStatus TransferOrder { get; set; }

        [Required]
        [DisplayName("Progress Notes")]
        public DocumentStatus ProgressNotes { get; set; }

        [Required]
        [DisplayName("Consultation Notes")]
        public DocumentStatus ConsultationNotes { get; set; }

        [Required]
        [DisplayName("Most Recent Lab Results")]
        public DocumentStatus MostRecentLabResults { get; set; }

        [Required]
        [DisplayName("Radiology Results")]
        public DocumentStatus RadiologyResults { get; set; }

        [Required]
        [DisplayName("Medication List")]
        public DocumentStatus MedicationList { get; set; }

        [Required]
        [DisplayName("ED Treatments & Procedures")]
        public DocumentStatus TreatmentsAndProceduresInED { get; set; }

        [Required]
        [DisplayName("Insurance Authorization")]
        public DocumentStatus InsuranceAuthorization { get; set; }

        [Required]
        [DisplayName("Other Notes")]
        public string OtherNotes { get; set; }

        [Required]
        [DisplayName("Code Status")]
        public string CodeStatus { get; set; }
    }
    public enum DocumentStatus
    {
        Sent,
        Yes,
        [Description("N/A")]
        NA
    }
}


