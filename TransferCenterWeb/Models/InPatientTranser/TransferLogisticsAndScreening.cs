namespace TransferCenterWeb.Models;

/// <summary>
/// Section 4: Transfer Logistics and Infectious Disease Screening (Page 1 & 2)
/// </summary>
public class TransferLogisticsAndScreening
{
    // ------------------------------------------------------------
    //  A. TRANSFER LOGISTICS  (bottom of Page 1)
    // ------------------------------------------------------------

    /// <summary>A1. Indicates patient is a dialysis patient.</summary>
    public bool IsDialysisPatient { get; set; }

    /// <summary>A2. Indicates patient is ventilated (Trach/ETT).</summary>
    public bool IsVentilated { get; set; }

    /// <summary>A3. Indicates patient is on BiPAP or IV drips.</summary>
    public bool OnBiPAPOrDrips { get; set; }

    /// <summary>A4. Services available at referring facility (Yes/No).</summary>
    public bool ServicesAvailable { get; set; }

    /// <summary>A5. Indicates LifeImage upload requested by referring site.</summary>
    public bool LifeImageUploadRequested { get; set; }

    /// <summary>A6. Indicates patient/family consent to transfer provided.</summary>
    public bool PatientConsentToTransfer { get; set; }

    /// <summary>A7. Sending facility unable/refusing to use LifeImage (CD requested after support offered).</summary>
    public bool SendingFacilityUnableToUseLifeImage { get; set; }


    // ------------------------------------------------------------
    //  B. INFECTIOUS DISEASE SCREENING FORM  (Page 2)
    // ------------------------------------------------------------

    /// <summary>
    /// Q1. Does the patient have cold or flu-like symptoms?
    /// (Fever ≥99°F, chills, cough, SOB, sore throat, etc.)
    /// </summary>
    public bool ColdOrFluSymptoms { get; set; }

    /// <summary>Q2. Does the patient have a new rash with unknown cause?</summary>
    public bool NewRashUnknownCause { get; set; }

    /// <summary>Q3. Direct contact with confirmed COVID-19 case in last 14 days?</summary>
    public bool ContactWithCovidPositive { get; set; }

    /// <summary>Q4. Diagnosed with or tested positive for COVID-19 at non-UCI lab?</summary>
    public bool DiagnosedCovidOrPositiveLab { get; set; }

    /// <summary>Q4.1. If YES, specify diagnosis/test date(s).</summary>
    public string CovidDiagnosisDates { get; set; }

    /// <summary>Q5. Sick household members or contact with symptomatic person?</summary>
    public bool SickHouseholdMembers { get; set; }

    /// <summary>Q6. Exposure to measles in last 21 days?</summary>
    public bool ExposedToMeasles { get; set; }

    /// <summary>Q7. Travel outside the U.S. in last 30 days?</summary>
    public bool TraveledOutsideUS { get; set; }

    /// <summary>Q8. Travel to Arabian Peninsula (past 14 days)?</summary>
    public bool TraveledArabianPeninsula { get; set; }

    /// <summary>Q9. Travel to Africa (past 21 days)?</summary>
    public bool TraveledAfrica { get; set; }

    /// <summary>Q9.1. If YES to Q8 or Q9, respiratory illness, pneumonia, or ARDS?</summary>
    public bool HasRespiratoryIllnessAfterTravel { get; set; }

    /// <summary>Q10. Admitted to any Kindred Hospital since July 1, 2020?</summary>
    public bool AdmittedToKindredHospital { get; set; }

    /// <summary>Q11. Current or previous infection with Multi-Drug Resistant Organisms?</summary>
    public bool MultiDrugResistantInfection { get; set; }

    /// <summary>Q11.1. If YES, list microorganism(s).</summary>
    public string Microorganisms { get; set; }

    /// <summary>Q12. Active communicable disease (TB, shingles, lice, scabies, etc.)?</summary>
    public bool CommunicableDisease { get; set; }

    /// <summary>Q12.1. If YES, list disease(s) or condition(s).</summary>
    public string DiseaseConditions { get; set; }


    // ------------------------------------------------------------
    //  C. CLINICAL DOCUMENT TRANSMISSION CHECKLIST  (Bottom of Page 2)
    // ------------------------------------------------------------

    /// <summary>
    /// C1. Face Sheet transmission status (Sent / Will Send / N/A).
    /// </summary>
    public DocumentTransmissionStatus FaceSheetStatus { get; set; }

    /// <summary>
    /// C2. H&P, Progress Notes, and Consultation Notes transmission status (Sent / Will Send / N/A).
    /// </summary>
    public DocumentTransmissionStatus HPAndConsultNotesStatus { get; set; }

    /// <summary>
    /// C3. Lab results (3 days of results incl. recent COVID) transmission status (Sent / Will Send / N/A).
    /// </summary>
    public DocumentTransmissionStatus LabResultsStatus { get; set; }

    /// <summary>
    /// C4. Diagnostic reports (MRI, CT, X-Ray, EKG, etc.) transmission status (Sent / Will Send / N/A).
    /// </summary>
    public DocumentTransmissionStatus DiagnosticsStatus { get; set; }

    /// <summary>
    /// C5. Current medication list transmission status (Sent / Will Send / N/A).
    /// </summary>
    public DocumentTransmissionStatus MedicationListStatus { get; set; }
}
 
/// <summary>
/// Represents document transmission status for required clinical materials.
/// </summary>
public enum DocumentTransmissionStatus
{
    /// <summary>Document has already been sent to UCI Transfer Center.</summary>
    Sent = 1,

    /// <summary>Document will be sent shortly (pending).</summary>
    WillSend = 2,

    /// <summary>Document not applicable for this patient/case.</summary>
    NotApplicable = 3
}
