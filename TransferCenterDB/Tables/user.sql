



/****** Object:  Table [dbo].[tblUser]    Script Date: 02-10-2025 11.33.06 PM ******/

CREATE TABLE [dbo].[User](
	[UserId] [bigint] IDENTITY(1,1) NOT NULL,
	[FirstName] [varchar](200) NOT NULL,
	[LastName] [varchar](200) NOT NULL,
	[EmailId] [varchar](200) NOT NULL,
	[IsActive] [bit] NOT NULL,
	[DomainID] [varchar](200) NULL,
	[LoginId] [varchar](200) NOT NULL,
	[Password] [varchar](200) NULL,
	[CreatedOn] [datetime] NOT NULL DEFAULT (GETDATE()),
	[Role] [smallint] NOT NULL DEFAULT ((0)),
	[CreatedBy] [varchar](200) NOT NULL,

    
 CONSTRAINT [PK_tblUser] PRIMARY KEY CLUSTERED 
(
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO




