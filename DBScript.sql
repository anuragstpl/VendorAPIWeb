USE [VendorAPIWeb]
GO
/****** Object:  Table [dbo].[Phase]    Script Date: 4/5/2020 7:35:56 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Phase](
	[PhaseID] [int] IDENTITY(1,1) NOT NULL,
	[Phase] [nvarchar](50) NULL,
 CONSTRAINT [PK_Phase] PRIMARY KEY CLUSTERED 
(
	[PhaseID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[ServiceProviderIssue]    Script Date: 4/5/2020 7:35:56 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[ServiceProviderIssue](
	[SPIssueID] [int] IDENTITY(1,1) NOT NULL,
	[ServiceProviderCode] [varchar](50) NULL,
	[IssueItem] [nvarchar](50) NULL,
	[Issue] [nvarchar](max) NULL,
	[Owner] [varchar](50) NULL,
 CONSTRAINT [PK_ServiceProviderIssu] PRIMARY KEY CLUSTERED 
(
	[SPIssueID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[ServiceProviders]    Script Date: 4/5/2020 7:35:56 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[ServiceProviders](
	[ServiceProviderID] [int] IDENTITY(1,1) NOT NULL,
	[ServiceProviderCode] [varchar](50) NOT NULL,
	[Name] [nvarchar](50) NULL,
	[StatusID] [int] NULL,
	[GoLiveDate] [varchar](50) NULL,
	[ProjectManagerID] [int] NULL,
	[PhaseID] [int] NULL,
	[Fees] [nvarchar](50) NULL,
	[TypeID] [int] NULL,
	[Update] [nvarchar](max) NULL,
 CONSTRAINT [PK_ServiceProviders_1] PRIMARY KEY CLUSTERED 
(
	[ServiceProviderCode] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Status]    Script Date: 4/5/2020 7:35:56 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Status](
	[StatusID] [int] IDENTITY(1,1) NOT NULL,
	[StatusName] [nvarchar](50) NULL,
 CONSTRAINT [PK_Status] PRIMARY KEY CLUSTERED 
(
	[StatusID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Type]    Script Date: 4/5/2020 7:35:56 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Type](
	[TypeID] [int] IDENTITY(1,1) NOT NULL,
	[Type] [nvarchar](50) NULL,
 CONSTRAINT [PK_Type] PRIMARY KEY CLUSTERED 
(
	[TypeID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[User]    Script Date: 4/5/2020 7:35:56 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[User](
	[UserID] [int] NOT NULL,
	[Username] [nvarchar](50) NULL,
	[Password] [nvarchar](50) NULL,
	[FirstName] [nvarchar](50) NULL,
	[LastName] [nvarchar](50) NULL,
	[Email] [nvarchar](50) NULL,
	[PhoneNo] [varchar](50) NULL,
	[UserRoleID] [int] NULL,
 CONSTRAINT [PK_User] PRIMARY KEY CLUSTERED 
(
	[UserID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[UserRoles]    Script Date: 4/5/2020 7:35:56 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UserRoles](
	[UserRoleID] [int] IDENTITY(1,1) NOT NULL,
	[UserRole] [nvarchar](50) NULL,
 CONSTRAINT [PK_UserRoles] PRIMARY KEY CLUSTERED 
(
	[UserRoleID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET IDENTITY_INSERT [dbo].[Phase] ON 

GO
INSERT [dbo].[Phase] ([PhaseID], [Phase]) VALUES (1, N'Testing')
GO
INSERT [dbo].[Phase] ([PhaseID], [Phase]) VALUES (2, N'QA Certification')
GO
INSERT [dbo].[Phase] ([PhaseID], [Phase]) VALUES (3, N'Planning')
GO
INSERT [dbo].[Phase] ([PhaseID], [Phase]) VALUES (4, N'Testing Quotes')
GO
INSERT [dbo].[Phase] ([PhaseID], [Phase]) VALUES (5, N'White Listing')
GO
INSERT [dbo].[Phase] ([PhaseID], [Phase]) VALUES (6, N'New')
GO
SET IDENTITY_INSERT [dbo].[Phase] OFF
GO
SET IDENTITY_INSERT [dbo].[ServiceProviderIssue] ON 

GO
INSERT [dbo].[ServiceProviderIssue] ([SPIssueID], [ServiceProviderCode], [IssueItem], [Issue], [Owner]) VALUES (11, N'TG001', N'1', N'Quote bug fix required in FmPilot- QA testing being completed and added to branch', N'Lordell')
GO
INSERT [dbo].[ServiceProviderIssue] ([SPIssueID], [ServiceProviderCode], [IssueItem], [Issue], [Owner]) VALUES (12, N'PK001', N'1', N'Need confirmation that provder is approved to do business with Davita', N'Josh Casto')
GO
INSERT [dbo].[ServiceProviderIssue] ([SPIssueID], [ServiceProviderCode], [IssueItem], [Issue], [Owner]) VALUES (13, N'PK001', N'2', N'Need AC repair', N'Test Owner')
GO
INSERT [dbo].[ServiceProviderIssue] ([SPIssueID], [ServiceProviderCode], [IssueItem], [Issue], [Owner]) VALUES (14, N'TS001', N'1', N'Test issue', N'Irfan')
GO
SET IDENTITY_INSERT [dbo].[ServiceProviderIssue] OFF
GO
SET IDENTITY_INSERT [dbo].[ServiceProviders] ON 

GO
INSERT [dbo].[ServiceProviders] ([ServiceProviderID], [ServiceProviderCode], [Name], [StatusID], [GoLiveDate], [ProjectManagerID], [PhaseID], [Fees], [TypeID], [Update]) VALUES (22, N'AF001', N'Academy Fire', 1, N'TBD', 1, 3, N'TBD', 2, N'Attended kick off meeting Jan 21st. Links to API vendor support web page and connector manual provided. Provider declined STPL developer option.')
GO
INSERT [dbo].[ServiceProviders] ([ServiceProviderID], [ServiceProviderCode], [Name], [StatusID], [GoLiveDate], [ProjectManagerID], [PhaseID], [Fees], [TypeID], [Update]) VALUES (18, N'BV001', N'Brightview', 3, N'March', 1, 1, N'$12,000.00', 1, N'White listing and tokens issued 1/6/20. API L1 Help Desk providing support.   Call scheduled for 2/25/20 to discuss API use with Home Depot.              ')
GO
INSERT [dbo].[ServiceProviders] ([ServiceProviderID], [ServiceProviderCode], [Name], [StatusID], [GoLiveDate], [ProjectManagerID], [PhaseID], [Fees], [TypeID], [Update]) VALUES (24, N'CP001', N'Century Fire protection', 3, N'TBD', 1, 3, N'$12,000.00', 2, N'Agreements sent to provide Jan 29th. Provider also sent link to register for weekly provider kick off call along with presentation deck.')
GO
INSERT [dbo].[ServiceProviders] ([ServiceProviderID], [ServiceProviderCode], [Name], [StatusID], [GoLiveDate], [ProjectManagerID], [PhaseID], [Fees], [TypeID], [Update]) VALUES (17, N'HS001', N'Henry Schein', 3, N'March', 1, 1, N'Waived for SBI', 1, N'Test work orders and script samples provided 2/23/20. Henry Schein advised that they are targeting some UAT testing the week of 2/24.')
GO
INSERT [dbo].[ServiceProviders] ([ServiceProviderID], [ServiceProviderCode], [Name], [StatusID], [GoLiveDate], [ProjectManagerID], [PhaseID], [Fees], [TypeID], [Update]) VALUES (23, N'MX001', N'MaintenX', 1, N'TBD', 1, 3, N'Waived', 1, N'Agreements signed 1/23. Fees waived per Josh. QA testing requirements provided. Maintenx sent response to follow up email stating that they are still in planning stages and not yet ready for testing.')
GO
INSERT [dbo].[ServiceProviders] ([ServiceProviderID], [ServiceProviderCode], [Name], [StatusID], [GoLiveDate], [ProjectManagerID], [PhaseID], [Fees], [TypeID], [Update]) VALUES (29, N'NA001', N'NA', 1, N'TBD', 1, 2, N'Waived', 4, N'High priority provider, pending information on move of all UPS vendors to SI7')
GO
INSERT [dbo].[ServiceProviders] ([ServiceProviderID], [ServiceProviderCode], [Name], [StatusID], [GoLiveDate], [ProjectManagerID], [PhaseID], [Fees], [TypeID], [Update]) VALUES (25, N'NX001', N'Nixon', 2, N'TBD', 1, 1, N'Waived', 3, N'Nixon attended kick off call 11/14/19 and is now adding Home Depot, Office Depot and Davita.')
GO
INSERT [dbo].[ServiceProviders] ([ServiceProviderID], [ServiceProviderCode], [Name], [StatusID], [GoLiveDate], [ProjectManagerID], [PhaseID], [Fees], [TypeID], [Update]) VALUES (21, N'PB001', N'PTR Baler', 2, N'TBD', 1, 1, N'$12,000.00', 3, N'Provider has been white listed and testing token provided Jan 24th. Help desk has been providing test work orders.')
GO
INSERT [dbo].[ServiceProviders] ([ServiceProviderID], [ServiceProviderCode], [Name], [StatusID], [GoLiveDate], [ProjectManagerID], [PhaseID], [Fees], [TypeID], [Update]) VALUES (20, N'PC001', N'PC Maintenance/Smartsheet', 3, N'02/27/2020', 1, 2, N'Waive Cost for HD', 1, N'Provider attempted QA certification 2/19/20 but API call resulted in duplicate work orders in vendor Smartsheet. QA Certification re-scheduled for 2/28/20.')
GO
INSERT [dbo].[ServiceProviders] ([ServiceProviderID], [ServiceProviderCode], [Name], [StatusID], [GoLiveDate], [ProjectManagerID], [PhaseID], [Fees], [TypeID], [Update]) VALUES (30, N'PC002', N'Park Construction', 1, N'TBD', 1, 5, N'Waived', 4, N'High priority provider, ')
GO
INSERT [dbo].[ServiceProviders] ([ServiceProviderID], [ServiceProviderCode], [Name], [StatusID], [GoLiveDate], [ProjectManagerID], [PhaseID], [Fees], [TypeID], [Update]) VALUES (28, N'PK001', N'Park Construction', 1, N'TBD', 1, 3, N'Waived', 1, N'2/25 in process to white list and issue a token for Home Depot and Davita.')
GO
INSERT [dbo].[ServiceProviders] ([ServiceProviderID], [ServiceProviderCode], [Name], [StatusID], [GoLiveDate], [ProjectManagerID], [PhaseID], [Fees], [TypeID], [Update]) VALUES (19, N'PT001', N'Paintzen', 1, N'02/28/2020', 1, 2, N'$12,000.00', 2, N'Provider passed QA prep, QA certification call scheduled for 2/27/20.')
GO
INSERT [dbo].[ServiceProviders] ([ServiceProviderID], [ServiceProviderCode], [Name], [StatusID], [GoLiveDate], [ProjectManagerID], [PhaseID], [Fees], [TypeID], [Update]) VALUES (26, N'PX001', N'Phoenix Technologies', 3, N'TBD', 1, 1, N'$12,000.00', 1, N'2/10 provider will be testing with Office Depot. They have not specifically asked for any test work orders at this time.')
GO
INSERT [dbo].[ServiceProviders] ([ServiceProviderID], [ServiceProviderCode], [Name], [StatusID], [GoLiveDate], [ProjectManagerID], [PhaseID], [Fees], [TypeID], [Update]) VALUES (27, N'TG001', N'Telgian', 3, N'Ph 2 - In process', 1, 4, N'$12,000.00', 1, N'The provider went live in January with phase 1.They are currently adding the quote feature to their API. A bug fix is being released in production on 2/26 so that they can roll their quote API into production.')
GO
INSERT [dbo].[ServiceProviders] ([ServiceProviderID], [ServiceProviderCode], [Name], [StatusID], [GoLiveDate], [ProjectManagerID], [PhaseID], [Fees], [TypeID], [Update]) VALUES (31, N'TS001', N'Test', 4, N'April', 1, 6, N'Waived', 5, N'test')
GO
SET IDENTITY_INSERT [dbo].[ServiceProviders] OFF
GO
SET IDENTITY_INSERT [dbo].[Status] ON 

GO
INSERT [dbo].[Status] ([StatusID], [StatusName]) VALUES (1, N'On Boarding')
GO
INSERT [dbo].[Status] ([StatusID], [StatusName]) VALUES (2, N'Staging Whitelisted')
GO
INSERT [dbo].[Status] ([StatusID], [StatusName]) VALUES (3, N'Live')
GO
INSERT [dbo].[Status] ([StatusID], [StatusName]) VALUES (4, N'New')
GO
SET IDENTITY_INSERT [dbo].[Status] OFF
GO
SET IDENTITY_INSERT [dbo].[Type] ON 

GO
INSERT [dbo].[Type] ([TypeID], [Type]) VALUES (1, N'FMO')
GO
INSERT [dbo].[Type] ([TypeID], [Type]) VALUES (2, N'IFM')
GO
INSERT [dbo].[Type] ([TypeID], [Type]) VALUES (3, N'FMO/IFM')
GO
INSERT [dbo].[Type] ([TypeID], [Type]) VALUES (4, N'FMO UPS')
GO
INSERT [dbo].[Type] ([TypeID], [Type]) VALUES (5, N'New')
GO
SET IDENTITY_INSERT [dbo].[Type] OFF
GO
INSERT [dbo].[User] ([UserID], [Username], [Password], [FirstName], [LastName], [Email], [PhoneNo], [UserRoleID]) VALUES (1, N'martha', N'martha@123', N'Robert', N'StandFord', N'irfan.siddiqui@gmail.com', N'1111111111', 2)
GO
INSERT [dbo].[User] ([UserID], [Username], [Password], [FirstName], [LastName], [Email], [PhoneNo], [UserRoleID]) VALUES (2, N'admin', N'admin@123', N'CBRE', N'FS', N'irfanetal@gmail.com', N'1234555566', 1)
GO
SET IDENTITY_INSERT [dbo].[UserRoles] ON 

GO
INSERT [dbo].[UserRoles] ([UserRoleID], [UserRole]) VALUES (1, N'admin')
GO
INSERT [dbo].[UserRoles] ([UserRoleID], [UserRole]) VALUES (2, N'vendor')
GO
SET IDENTITY_INSERT [dbo].[UserRoles] OFF
GO
ALTER TABLE [dbo].[ServiceProviderIssue]  WITH CHECK ADD  CONSTRAINT [FK_ServiceProviderIssue_ServiceProviders] FOREIGN KEY([ServiceProviderCode])
REFERENCES [dbo].[ServiceProviders] ([ServiceProviderCode])
GO
ALTER TABLE [dbo].[ServiceProviderIssue] CHECK CONSTRAINT [FK_ServiceProviderIssue_ServiceProviders]
GO
ALTER TABLE [dbo].[ServiceProviders]  WITH CHECK ADD  CONSTRAINT [FK_ServiceProviders_Phase] FOREIGN KEY([PhaseID])
REFERENCES [dbo].[Phase] ([PhaseID])
GO
ALTER TABLE [dbo].[ServiceProviders] CHECK CONSTRAINT [FK_ServiceProviders_Phase]
GO
ALTER TABLE [dbo].[ServiceProviders]  WITH CHECK ADD  CONSTRAINT [FK_ServiceProviders_Status] FOREIGN KEY([StatusID])
REFERENCES [dbo].[Status] ([StatusID])
GO
ALTER TABLE [dbo].[ServiceProviders] CHECK CONSTRAINT [FK_ServiceProviders_Status]
GO
ALTER TABLE [dbo].[ServiceProviders]  WITH CHECK ADD  CONSTRAINT [FK_ServiceProviders_Type] FOREIGN KEY([TypeID])
REFERENCES [dbo].[Type] ([TypeID])
GO
ALTER TABLE [dbo].[ServiceProviders] CHECK CONSTRAINT [FK_ServiceProviders_Type]
GO
ALTER TABLE [dbo].[User]  WITH CHECK ADD  CONSTRAINT [FK_User_UserRoles] FOREIGN KEY([UserRoleID])
REFERENCES [dbo].[UserRoles] ([UserRoleID])
GO
ALTER TABLE [dbo].[User] CHECK CONSTRAINT [FK_User_UserRoles]
GO
