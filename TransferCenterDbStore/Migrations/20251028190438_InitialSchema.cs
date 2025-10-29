using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TransferCenterDbStore.Migrations
{
    /// <inheritdoc />
    public partial class InitialSchema : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AdditionalInfo",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TransferType = table.Column<short>(type: "smallint", nullable: false),
                    ServicesAvailable = table.Column<bool>(type: "bit", nullable: false),
                    SitterRequired = table.Column<bool>(type: "bit", nullable: false),
                    VTIBDrips = table.Column<bool>(type: "bit", nullable: false),
                    Dialysis = table.Column<bool>(type: "bit", nullable: false),
                    PFCTTransfer = table.Column<bool>(type: "bit", nullable: false),
                    CovidWithin3Days = table.Column<bool>(type: "bit", nullable: false),
                    FaceSheet = table.Column<short>(type: "smallint", nullable: false),
                    HAndP = table.Column<short>(type: "smallint", nullable: false),
                    CovidTestResults = table.Column<short>(type: "smallint", nullable: false),
                    TransferOrder = table.Column<short>(type: "smallint", nullable: false),
                    ProgressNotes = table.Column<short>(type: "smallint", nullable: false),
                    ConsultationNotes = table.Column<short>(type: "smallint", nullable: false),
                    MostRecentLabResults = table.Column<short>(type: "smallint", nullable: false),
                    RadiologyResults = table.Column<short>(type: "smallint", nullable: false),
                    MedicationList = table.Column<short>(type: "smallint", nullable: false),
                    TreatmentsAndProceduresInED = table.Column<short>(type: "smallint", nullable: false),
                    InsuranceAuthorization = table.Column<short>(type: "smallint", nullable: false),
                    OtherNotes = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CodeStatus = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LifeImageUploadRequested = table.Column<short>(type: "smallint", nullable: false),
                    SendingFacilityUnableToUseLifeImage = table.Column<short>(type: "smallint", nullable: false),
                    ColdOrFluSymptoms = table.Column<bool>(type: "bit", nullable: false),
                    NewRashUnknownCause = table.Column<bool>(type: "bit", nullable: false),
                    ContactWithCovidPositive = table.Column<bool>(type: "bit", nullable: false),
                    DiagnosedCovidOrPositiveLab = table.Column<bool>(type: "bit", nullable: false),
                    CovidDiagnosisDates = table.Column<DateTime>(type: "datetime2", nullable: true),
                    SickHouseholdMembers = table.Column<bool>(type: "bit", nullable: false),
                    ExposedToMeasles = table.Column<bool>(type: "bit", nullable: false),
                    TraveledOutsideUS = table.Column<bool>(type: "bit", nullable: false),
                    TraveledArabianPeninsula = table.Column<bool>(type: "bit", nullable: false),
                    TraveledAfrica = table.Column<bool>(type: "bit", nullable: false),
                    HasRespiratoryIllnessAfterTravel = table.Column<bool>(type: "bit", nullable: false),
                    AdmittedToKindredHospital = table.Column<bool>(type: "bit", nullable: false),
                    MultiDrugResistantInfection = table.Column<bool>(type: "bit", nullable: false),
                    Microorganisms = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CommunicableDisease = table.Column<bool>(type: "bit", nullable: false),
                    DiseaseConditions = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LabResultsStatus = table.Column<short>(type: "smallint", nullable: false),
                    DiagnosticsStatus = table.Column<short>(type: "smallint", nullable: false),
                    MedicationListStatus = table.Column<short>(type: "smallint", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastUpdatedOn = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AdditionalInfo", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AuditLog",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ControllerName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ActionName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ActionType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RequestData = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ResponseData = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ExecutionStatus = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ExecutionDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ClientIp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Remarks = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AuditLog", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ComorbiditiesAndRiskScores",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    None = table.Column<bool>(type: "bit", nullable: false),
                    StrokeTIA = table.Column<bool>(type: "bit", nullable: false),
                    CHF = table.Column<bool>(type: "bit", nullable: false),
                    CKD_ESRD = table.Column<bool>(type: "bit", nullable: false),
                    HTN = table.Column<bool>(type: "bit", nullable: false),
                    DiabetesOrSkinIssues = table.Column<bool>(type: "bit", nullable: false),
                    MI_Angina_CAD = table.Column<bool>(type: "bit", nullable: false),
                    COPDOrRespiratoryFailure = table.Column<bool>(type: "bit", nullable: false),
                    Ventilated = table.Column<bool>(type: "bit", nullable: false),
                    PulmonaryHTN = table.Column<bool>(type: "bit", nullable: false),
                    PulmonaryHTN_Prostacyclin = table.Column<bool>(type: "bit", nullable: false),
                    ImmunocompromisedOrHIV = table.Column<bool>(type: "bit", nullable: false),
                    PsychBackground = table.Column<bool>(type: "bit", nullable: false),
                    NonCompliantWithCare = table.Column<bool>(type: "bit", nullable: false),
                    UnableToPerformADLs = table.Column<bool>(type: "bit", nullable: false),
                    DrugOrAlcoholDependence = table.Column<bool>(type: "bit", nullable: false),
                    LymphomaLeukemiaCancer = table.Column<bool>(type: "bit", nullable: false),
                    MalnutritionObesityDigestiveDisease = table.Column<bool>(type: "bit", nullable: false),
                    UnconsciousOrALOC = table.Column<bool>(type: "bit", nullable: false),
                    LOSMoreThan2Weeks = table.Column<bool>(type: "bit", nullable: false),
                    OutOfServiceArea = table.Column<bool>(type: "bit", nullable: false),
                    DNRCodeStatus = table.Column<bool>(type: "bit", nullable: false),
                    CovidPositive = table.Column<bool>(type: "bit", nullable: false),
                    RecentSurgeryAtUCI = table.Column<bool>(type: "bit", nullable: false),
                    RecentSurgeryOutsideUCI = table.Column<bool>(type: "bit", nullable: false),
                    TotalPoints = table.Column<int>(type: "int", nullable: true),
                    Comorbidities = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RiskScore = table.Column<double>(type: "float", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastUpdatedOn = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ComorbiditiesAndRiskScores", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PatientDetails",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TransferType = table.Column<short>(type: "smallint", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DOB = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Gender = table.Column<short>(type: "smallint", nullable: false),
                    IsIsolation = table.Column<bool>(type: "bit", nullable: false),
                    IsolationType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Height = table.Column<double>(type: "float", nullable: false),
                    Weight = table.Column<double>(type: "float", nullable: false),
                    Diagnosis = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LevelOfCareNeeded = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AcceptingPhysician = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ReasonForTransfer = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Lateral = table.Column<bool>(type: "bit", nullable: false),
                    HLOC = table.Column<bool>(type: "bit", nullable: false),
                    PatientInsurance = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CodeStatus = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Sitter = table.Column<bool>(type: "bit", nullable: false),
                    JehovahWitness = table.Column<bool>(type: "bit", nullable: false),
                    Capitated = table.Column<bool>(type: "bit", nullable: false),
                    GCS = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    WeightIn = table.Column<short>(type: "smallint", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastUpdatedOn = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PatientDetails", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PatientTransferInfo",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TransferType = table.Column<short>(type: "smallint", nullable: false),
                    CaseMgrSwRn = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FaxNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RequestingFacility = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TransferDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ReferringMd = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ReferringMdPhone = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ReferringSpecialistPhone = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ReferringSpecialist = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AdmitDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Unit = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UnitPhone = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsHloc = table.Column<bool>(type: "bit", nullable: false),
                    MR = table.Column<int>(type: "int", nullable: true),
                    SecondPhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SecondFaxNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PrimaryCallerName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SecondaryCallerName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastUpdatedOn = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PatientTransferInfo", x => x.Id);
                });

        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AdditionalInfo");

            migrationBuilder.DropTable(
                name: "AuditLog");

            migrationBuilder.DropTable(
                name: "ComorbiditiesAndRiskScores");

            migrationBuilder.DropTable(
                name: "PatientDetails");

            migrationBuilder.DropTable(
                name: "PatientTransferInfo");
        }
    }
}
