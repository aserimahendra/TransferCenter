IF OBJECT_ID(N'[__EFMigrationsHistory]') IS NULL
BEGIN
    CREATE TABLE [__EFMigrationsHistory] (
        [MigrationId] nvarchar(150) NOT NULL,
        [ProductVersion] nvarchar(32) NOT NULL,
        CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY ([MigrationId])
    );
END;
GO

BEGIN TRANSACTION;
CREATE TABLE [AdditionalInfo] (
    [Id] bigint NOT NULL IDENTITY,
    [UId] uniqueidentifier NOT NULL,
    [TransferType] smallint NOT NULL,
    [ServicesAvailable] bit NOT NULL,
    [SitterRequired] bit NOT NULL,
    [VTIBDrips] bit NOT NULL,
    [Dialysis] bit NOT NULL,
    [PFCTTransfer] bit NOT NULL,
    [CovidWithin3Days] bit NOT NULL,
    [FaceSheet] smallint NOT NULL,
    [HAndP] smallint NOT NULL,
    [CovidTestResults] smallint NOT NULL,
    [TransferOrder] smallint NOT NULL,
    [ProgressNotes] smallint NOT NULL,
    [ConsultationNotes] smallint NOT NULL,
    [MostRecentLabResults] smallint NOT NULL,
    [RadiologyResults] smallint NOT NULL,
    [MedicationList] smallint NOT NULL,
    [TreatmentsAndProceduresInED] smallint NOT NULL,
    [InsuranceAuthorization] smallint NOT NULL,
    [OtherNotes] varchar(2000) NOT NULL,
    [CodeStatus] varchar(200) NOT NULL,
    [LifeImageUploadRequested] smallint NOT NULL,
    [SendingFacilityUnableToUseLifeImage] smallint NOT NULL,
    [ColdOrFluSymptoms] bit NOT NULL,
    [NewRashUnknownCause] bit NOT NULL,
    [ContactWithCovidPositive] bit NOT NULL,
    [DiagnosedCovidOrPositiveLab] bit NOT NULL,
    [CovidDiagnosisDates] datetime2 NULL,
    [SickHouseholdMembers] bit NOT NULL,
    [ExposedToMeasles] bit NOT NULL,
    [TraveledOutsideUS] bit NOT NULL,
    [TraveledArabianPeninsula] bit NOT NULL,
    [TraveledAfrica] bit NOT NULL,
    [HasRespiratoryIllnessAfterTravel] bit NOT NULL,
    [AdmittedToKindredHospital] bit NOT NULL,
    [MultiDrugResistantInfection] bit NOT NULL,
    [Microorganisms] varchar(500) NOT NULL,
    [CommunicableDisease] bit NOT NULL,
    [DiseaseConditions] varchar(500) NOT NULL,
    [LabResultsStatus] smallint NOT NULL,
    [DiagnosticsStatus] smallint NOT NULL,
    [MedicationListStatus] smallint NOT NULL,
    [IsActive] bit NOT NULL,
    [CreatedBy] varchar(200) NOT NULL,
    [CreatedOn] datetime2 NOT NULL,
    [LastUpdatedOn] datetime2 NULL,
    CONSTRAINT [PK_AdditionalInfo] PRIMARY KEY ([Id])
);

CREATE TABLE [AuditLog] (
    [Id] bigint NOT NULL IDENTITY,
    [UserId] varchar(200) NOT NULL,
    [ControllerName] varchar(200) NOT NULL,
    [ActionName] varchar(200) NOT NULL,
    [ActionType] varchar(200) NULL,
    [RequestData] varchar(200) NULL,
    [ResponseData] varchar(200) NULL,
    [ExecutionStatus] varchar(200) NULL,
    [ExecutionDate] datetime2 NOT NULL,
    [ClientIp] varchar(200) NULL,
    [Remarks] varchar(200) NULL,
    CONSTRAINT [PK_AuditLog] PRIMARY KEY ([Id])
);

CREATE TABLE [ComorbiditiesAndRiskScores] (
    [Id] bigint NOT NULL IDENTITY,
    [UId] uniqueidentifier NOT NULL,
    [None] bit NOT NULL,
    [StrokeTIA] bit NOT NULL,
    [CHF] bit NOT NULL,
    [CKD_ESRD] bit NOT NULL,
    [HTN] bit NOT NULL,
    [DiabetesOrSkinIssues] bit NOT NULL,
    [MI_Angina_CAD] bit NOT NULL,
    [COPDOrRespiratoryFailure] bit NOT NULL,
    [Ventilated] bit NOT NULL,
    [PulmonaryHTN] bit NOT NULL,
    [PulmonaryHTN_Prostacyclin] bit NOT NULL,
    [ImmunocompromisedOrHIV] bit NOT NULL,
    [PsychBackground] bit NOT NULL,
    [NonCompliantWithCare] bit NOT NULL,
    [UnableToPerformADLs] bit NOT NULL,
    [DrugOrAlcoholDependence] bit NOT NULL,
    [LymphomaLeukemiaCancer] bit NOT NULL,
    [MalnutritionObesityDigestiveDisease] bit NOT NULL,
    [UnconsciousOrALOC] bit NOT NULL,
    [LOSMoreThan2Weeks] bit NOT NULL,
    [OutOfServiceArea] bit NOT NULL,
    [DNRCodeStatus] bit NOT NULL,
    [CovidPositive] bit NOT NULL,
    [RecentSurgeryAtUCI] bit NOT NULL,
    [RecentSurgeryOutsideUCI] bit NOT NULL,
    [TotalPoints] int NULL,
    [Comorbidities] varchar(500) NULL,
    [RiskScore] float NOT NULL,
    [CreatedDate] datetime2 NOT NULL,
    [UpdatedDate] datetime2 NOT NULL,
    [IsActive] bit NOT NULL,
    [CreatedBy] varchar(200) NOT NULL,
    [CreatedOn] datetime2 NOT NULL,
    [LastUpdatedOn] datetime2 NULL,
    CONSTRAINT [PK_ComorbiditiesAndRiskScores] PRIMARY KEY ([Id])
);

CREATE TABLE [PatientDetails] (
    [Id] bigint NOT NULL IDENTITY,
    [UId] uniqueidentifier NOT NULL,
    [TransferType] smallint NOT NULL,
    [Name] varchar(200) NOT NULL,
    [DOB] datetime2 NOT NULL,
    [Gender] smallint NOT NULL,
    [IsIsolation] bit NOT NULL,
    [IsolationType] varchar(200) NOT NULL,
    [Height] float NOT NULL,
    [Weight] float NOT NULL,
    [Diagnosis] varchar(200) NOT NULL,
    [LevelOfCareNeeded] varchar(200) NULL,
    [AcceptingPhysician] varchar(200) NULL,
    [ReasonForTransfer] varchar(1000) NULL,
    [Lateral] bit NOT NULL,
    [HLOC] bit NOT NULL,
    [PatientInsurance] varchar(200) NULL,
    [CodeStatus] varchar(200) NULL,
    [Sitter] bit NOT NULL,
    [JehovahWitness] bit NOT NULL,
    [Capitated] bit NOT NULL,
    [GCS] varchar(200) NULL,
    [WeightIn] smallint NOT NULL,
    [IsActive] bit NOT NULL,
    [CreatedBy] varchar(200) NOT NULL,
    [CreatedOn] datetime2 NOT NULL,
    [LastUpdatedOn] datetime2 NULL,
    CONSTRAINT [PK_PatientDetails] PRIMARY KEY ([Id])
);

CREATE TABLE [PatientTransferInfo] (
    [Id] bigint NOT NULL IDENTITY,
    [UId] uniqueidentifier NOT NULL,
    [TransferType] smallint NOT NULL,
    [CaseMgrSwRn] varchar(200) NOT NULL,
    [PhoneNumber] varchar(200) NOT NULL,
    [FaxNumber] varchar(200) NOT NULL,
    [RequestingFacility] varchar(200) NOT NULL,
    [TransferDate] datetime2 NOT NULL,
    [ReferringMd] varchar(200) NOT NULL,
    [ReferringMdPhone] varchar(200) NOT NULL,
    [ReferringSpecialistPhone] varchar(200) NOT NULL,
    [ReferringSpecialist] varchar(200) NOT NULL,
    [AdmitDate] datetime2 NOT NULL,
    [Unit] varchar(200) NOT NULL,
    [UnitPhone] varchar(200) NOT NULL,
    [IsHloc] bit NOT NULL,
    [MR] int NULL,
    [SecondPhoneNumber] varchar(200) NULL,
    [SecondFaxNumber] varchar(200) NULL,
    [PrimaryCallerName] varchar(200) NULL,
    [SecondaryCallerName] varchar(200) NULL,
    [IsActive] bit NOT NULL,
    [CreatedBy] varchar(200) NOT NULL,
    [CreatedOn] datetime2 NOT NULL,
    [LastUpdatedOn] datetime2 NULL,
    CONSTRAINT [PK_PatientTransferInfo] PRIMARY KEY ([Id])
);

CREATE TABLE [User] (
    [UserId] bigint NOT NULL IDENTITY,
    [FirstName] varchar(200) NOT NULL,
    [LastName] varchar(200) NOT NULL,
    [EmailId] varchar(200) NOT NULL,
    [Password] varchar(200) NULL,
    [DomainID] varchar(200) NOT NULL,
    [LoginId] varchar(200) NOT NULL,
    [Role] smallint NOT NULL,
    [IsActive] bit NOT NULL,
    [CreatedBy] varchar(200) NOT NULL,
    [CreatedOn] datetime2 NOT NULL,
    [LastUpdatedOn] datetime2 NULL,
    CONSTRAINT [PK_User] PRIMARY KEY ([UserId])
);

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20251028190438_InitialSchema', N'9.0.9');

COMMIT;
GO

