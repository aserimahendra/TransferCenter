CREATE TABLE AuditLog (
                          Id BIGINT IDENTITY(1,1) PRIMARY KEY,         -- Auto-increment primary key
                          UserId INT NOT NULL,                         -- User performing the action
                          ControllerName NVARCHAR(100) NOT NULL,       -- Controller or Module name
                          ActionName NVARCHAR(100) NOT NULL,           -- Specific method or API endpoint
                          ActionType NVARCHAR(50) NULL,                -- Optional: Create / Update / Delete / View
                          RequestData NVARCHAR(MAX) NULL,              -- Input data (as JSON)
                          ResponseData NVARCHAR(MAX) NULL,             -- Output or response data
                          ExecutionStatus NVARCHAR(20) NULL,           -- Success / Failed / Exception
                          ExecutionDate DATETIME2 NOT NULL DEFAULT SYSUTCDATETIME(), -- UTC timestamp
                          ClientIp NVARCHAR(50) NULL,                  -- IP address of request
                          Remarks NVARCHAR(500) NULL                   -- Additional info or error details
);