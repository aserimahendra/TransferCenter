CREATE TABLE dbo.AdditionalInfo (
    Id BIGINT PRIMARY KEY IDENTITY(1,1),
   UId UNIQUEIDENTIFIER NOT NULL DEFAULT NEWID(),
    ServicesAvailable BIT NOT NULL,
    SitterRequired BIT NOT NULL,
    VTIBDrips BIT NOT NULL,
    Dialysis BIT NOT NULL,
    PFCTTransfer BIT NOT NULL,
    CovidWithin3Days BIT NOT NULL,

    FaceSheet INT NOT NULL,
    HAndP INT NOT NULL,
    CovidTestResults INT NOT NULL,
    TransferOrder INT NOT NULL,
    ProgressNotes INT NOT NULL,
    ConsultationNotes INT NOT NULL,
    MostRecentLabResults INT NOT NULL,
    RadiologyResults INT NOT NULL,
    MedicationList INT NOT NULL,
    TreatmentsAndProceduresInED INT NOT NULL,
    InsuranceAuthorization INT NOT NULL,
    OtherNotes NVARCHAR(255) NULL,
    CodeStatus NVARCHAR(255) NULL
);
GO


