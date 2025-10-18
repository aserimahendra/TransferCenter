CREATE TABLE AuditLog (
                          Id BIGINT IDENTITY(1,1) PRIMARY KEY,         -- Auto-increment primary key
                          UserId varchar(100) NOT NULL,                         -- User performing the action
                          ControllerName VARCHAR(100) NOT NULL,       -- Controller or Module name
                          ActionName VARCHAR(100) NOT NULL,           -- Specific method or API endpoint
                          ActionType VARCHAR(50) NULL,                -- Optional: Create / Update / Delete / View
                          RequestData VARCHAR(MAX) NULL,              -- Input data (as JSON)
                          ResponseData VARCHAR(MAX) NULL,             -- Output or response data
                          ExecutionStatus VARCHAR(20) NULL,           -- Success / Failed / Exception
                          ExecutionDate DATETIME2 NOT NULL DEFAULT SYSUTCDATETIME(), -- UTC timestamp
                          ClientIp VARCHAR(50) NULL,                  -- IP address of request
                          Remarks VARCHAR(500) NULL                   -- Additional info or error details
);