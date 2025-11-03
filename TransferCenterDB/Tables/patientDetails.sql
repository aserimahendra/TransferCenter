CREATE TABLE dbo.PatientDetails (
    Id BIGINT PRIMARY KEY IDENTITY(1,1),
    UId UNIQUEIDENTIFIER NOT NULL DEFAULT NEWID(),
    Name NVARCHAR(255) NOT NULL,

    DOB DATETIME NOT NULL,

    Gender SMALLINT NOT NULL,

    IsIsolation BIT NOT NULL,

    IsolationType NVARCHAR(255) NULL,

    Height FLOAT NOT NULL,

    Weight FLOAT NOT NULL,

    Diagnosis VARCHAR(200) NULL,

    LevelOfCareNeeded VARCHAR(200) NULL,

    AcceptingPhysician NVARCHAR(255) NULL,

    ReasonForTransfer VARCHAR(200) NULL,

    Lateral BIT NOT NULL,

    HLOC BIT NOT NULL,

    PatientInsurance NVARCHAR(255) NULL,

    CodeStatus VARCHAR(200) NULL,

    Sitter BIT NOT NULL,

    JehovahWitness BIT NOT NULL,

    Capitated BIT NOT NULL,

    GCS NVARCHAR(255) NULL,

    WeightIn SMALLINT NOT NULL
);
GO
