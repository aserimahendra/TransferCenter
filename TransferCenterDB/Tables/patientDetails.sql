CREATE TABLE dbo.PatientDetails (
    Id BIGINT PRIMARY KEY IDENTITY(1,1),

    Name NVARCHAR(255) NOT NULL,

    DOB DATE NOT NULL,

    Gender NVARCHAR(50) NOT NULL,

    IsIsolation BIT NOT NULL,

    IsolationType NVARCHAR(255) NULL,

    Height NVARCHAR(50) NULL,

    Weight NVARCHAR(50) NULL,

    Diagnosis NVARCHAR(MAX) NULL,

    LevelOfCareNeeded NVARCHAR(MAX) NULL,

    AcceptingPhysician NVARCHAR(255) NULL,

    ReasonForTransfer NVARCHAR(MAX) NULL,

    Lateral BIT NOT NULL,

    HLOC BIT NOT NULL,

    PatientInsurance NVARCHAR(255) NOT NULL
);
GO
