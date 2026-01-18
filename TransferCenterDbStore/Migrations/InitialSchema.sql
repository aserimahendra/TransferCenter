create database TransferCenter collate SQL_Latin1_General_CP1_CI_AS
go

grant connect on database :: TransferCenter to dbo
go

grant view any column encryption key definition, view any column master key definition on database :: TransferCenter to [public]
go

create table dbo.AdditionalInfo
(
    Id                                  bigint identity
        constraint PK_AdditionalInfo
            primary key,
    UId                                 uniqueidentifier not null,
    TransferType                        smallint         not null,
    ServicesAvailable                   bit              not null,
    SitterRequired                      bit              not null,
    VTIBDrips                           bit              not null,
    Dialysis                            bit              not null,
    PFCTTransfer                        bit              not null,
    CovidWithin3Days                    bit              not null,
    FaceSheet                           smallint         not null,
    HAndP                               smallint         not null,
    CovidTestResults                    smallint         not null,
    TransferOrder                       smallint         not null,
    ProgressNotes                       smallint         not null,
    ConsultationNotes                   smallint         not null,
    MostRecentLabResults                smallint         not null,
    RadiologyResults                    smallint         not null,
    MedicationList                      smallint         not null,
    TreatmentsAndProceduresInED         smallint         not null,
    InsuranceAuthorization              smallint         not null,
    OtherNotes                          varchar(1000),
    CodeStatus                          varchar(200),
    LifeImageUploadRequested            smallint         not null,
    SendingFacilityUnableToUseLifeImage smallint         not null,
    ColdOrFluSymptoms                   bit              not null,
    NewRashUnknownCause                 bit              not null,
    ContactWithCovidPositive            bit              not null,
    DiagnosedCovidOrPositiveLab         bit              not null,
    CovidDiagnosisDates                 datetime2,
    SickHouseholdMembers                bit              not null,
    ExposedToMeasles                    bit              not null,
    TraveledOutsideUS                   bit              not null,
    TraveledArabianPeninsula            bit              not null,
    TraveledAfrica                      bit              not null,
    HasRespiratoryIllnessAfterTravel    bit              not null,
    AdmittedToKindredHospital           bit              not null,
    MultiDrugResistantInfection         bit              not null,
    Microorganisms                      varchar(300),
    CommunicableDisease                 bit              not null,
    DiseaseConditions                   varchar(300),
    LabResultsStatus                    smallint         not null,
    DiagnosticsStatus                   smallint         not null,
    MedicationListStatus                smallint         not null,
    IsActive                            bit              not null,
    CreatedBy                           varchar(100)    not null,
    CreatedOn                           datetime2        not null,
    LastUpdatedOn                       datetime2
)
    go

create table dbo.AuditLog
(
    Id              bigint identity
        constraint PK_AuditLog
            primary key,
    UserId          varchar(100) not null,
    ControllerName  varchar(100) not null,
    ActionName      varchar(50) not null,
    ActionType      varchar(50),
    RequestData     varchar(500),
    ResponseData    varchar(500),
    ExecutionStatus varchar(50),
    ExecutionDate   datetime2     not null,
    ClientIp        varchar(30),
    Remarks         varchar(200)
)
    go

create table dbo.ComorbiditiesAndRiskScores
(
    Id                                  bigint identity
        constraint PK_ComorbiditiesAndRiskScores
            primary key,
    None                                bit              not null,
    StrokeTIA                           bit              not null,
    CHF                                 bit              not null,
    CKD_ESRD                            bit              not null,
    HTN                                 bit              not null,
    DiabetesOrSkinIssues                bit              not null,
    MI_Angina_CAD                       bit              not null,
    COPDOrRespiratoryFailure            bit              not null,
    Ventilated                          bit              not null,
    PulmonaryHTN                        bit              not null,
    PulmonaryHTN_Prostacyclin           bit              not null,
    ImmunocompromisedOrHIV              bit              not null,
    PsychBackground                     bit              not null,
    NonCompliantWithCare                bit              not null,
    UnableToPerformADLs                 bit              not null,
    DrugOrAlcoholDependence             bit              not null,
    LymphomaLeukemiaCancer              bit              not null,
    MalnutritionObesityDigestiveDisease bit              not null,
    UnconsciousOrALOC                   bit              not null,
    LOSMoreThan2Weeks                   bit              not null,
    OutOfServiceArea                    bit              not null,
    DNRCodeStatus                       bit              not null,
    CovidPositive                       bit              not null,
    RecentSurgeryAtUCI                  bit              not null,
    RecentSurgeryOutsideUCI             bit              not null,
    TotalPoints                         int,
    Comorbidities                       varchar(300),
    RiskScore                           float            not null,
    CreatedDate                         datetime2        not null,
    UpdatedDate                         datetime2        not null,
    IsActive                            bit              not null,
    CreatedBy                           varchar(100)    not null,
    CreatedOn                           datetime2        not null,
    LastUpdatedOn                       datetime2,
    UId                                 uniqueidentifier not null
)
    go

create table dbo.PatientDetails
(
    Id                 bigint identity
        constraint PK_PatientDetails
            primary key,
    UId                uniqueidentifier not null,
    TransferType       smallint         not null,
    Name               varchar(200)    not null,
    DOB                datetime2        not null,
    Gender             smallint         not null,
    IsIsolation        bit              not null,
    IsolationType      varchar(200)    not null,
    Height             float            not null,
    Weight             float            not null,
    Diagnosis          varchar(200)    not null,
    LevelOfCareNeeded  varchar(200),
    AcceptingPhysician varchar(200),
    ReasonForTransfer  nvarchar(max),
    Lateral            bit              not null,
    HLOC               bit              not null,
    PatientInsurance   varchar(1000),
    CodeStatus         varchar(200),
    Sitter             bit              not null,
    JehovahWitness     bit              not null,
    Capitated          bit              not null,
    GCS                varchar(200),
    WeightIn           smallint         not null,
    IsActive           bit              not null,
    CreatedBy          varchar(100)    not null,
    CreatedOn          datetime2        not null,
    LastUpdatedOn      datetime2
)
    go

create table dbo.PatientTransferInfo
(
    Id                       bigint identity
        constraint PK_PatientTransferInfo
            primary key,
    UId                      uniqueidentifier not null,
    TransferType             smallint         not null,
    CaseMgrSwRn              varchar(200)    not null,
    PhoneNumber              varchar(15)    not null,
    FaxNumber                varchar(15)    not null,
    RequestingFacility       varchar(200)    not null,
    TransferDate             datetime2        not null,
    ReferringMd              varchar(200)    not null,
    ReferringMdPhone         varchar(15)    not null,
    ReferringSpecialistPhone varchar(15)    not null,
    ReferringSpecialist      varchar(200)    not null,
    AdmitDate                datetime2        not null,
    Unit                     varchar(200)    not null,
    UnitPhone                varchar(200)    not null,
    IsHloc                   bit              not null,
    MR                       int,
    SecondPhoneNumber        varchar(15),
    SecondFaxNumber          varchar(15),
    PrimaryCallerName        varchar(200),
    SecondaryCallerName      varchar(200),
    IsActive                 bit              not null,
    CreatedBy                varchar(100)    not null,
    CreatedOn                datetime2        not null,
    LastUpdatedOn            datetime2
)
    go

create table dbo.[User]
(
    UserId        bigint identity
    constraint PK_tblUser
    primary key,
    FirstName     varchar(100)                not null,
    LastName      varchar(100)                not null,
    EmailId       varchar(100)                not null,
    IsActive      bit
    constraint DF_User_IsActive default 1 not null,
    DomainID      varchar(100),
    LoginId       varchar(100)                not null,
    Password      varchar(100),
    CreatedOn     datetime                    not null,
    Role          smallint
    constraint DF_User_Role default 0     not null,
    LastUpdatedOn datetime,
    CreatedBy     varchar(100)                not null
    )
    go

create table dbo.__EFMigrationsHistory
(
    MigrationId    nvarchar(150) not null
        constraint PK___EFMigrationsHistory
            primary key,
    ProductVersion nvarchar(32)  not null
)

go
