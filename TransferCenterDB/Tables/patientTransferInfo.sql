-- Use TransferCenter database
USE TransferCenter;
GO

-- Drop table if exists
IF OBJECT_ID('dbo.PatientTransferInfo', 'U') IS NOT NULL
    DROP TABLE dbo.PatientTransferInfo;
GO

CREATE TABLE dbo.PatientTransferInfo (
    Id BIGINT PRIMARY KEY IDENTITY(1,1),
    UId UNIQUEIDENTIFIER NOT NULL DEFAULT NEWID(),
    CaseMgrSwRn NVARCHAR(255) NOT NULL,

    PhoneNumber NVARCHAR(50) NOT NULL,

    FaxNumber NVARCHAR(50) NOT NULL,

    RequestingFacility NVARCHAR(255) NOT NULL,

    TransferDate DATETIME NOT NULL,

    ReferringMd NVARCHAR(255) NOT NULL,

    ReferringMdPhone NVARCHAR(50) NOT NULL,

    ReferringSpecialistPhone NVARCHAR(50) NOT NULL,

    ReferringSpecialist NVARCHAR(255) NOT NULL,

    AdmitDate DATETIME NOT NULL,

    Unit NVARCHAR(100) NOT NULL,

    UnitPhone NVARCHAR(50) NOT NULL
);
GO
-- Use TransferCenter database
USE TransferCenter;
GO


