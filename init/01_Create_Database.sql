CREATE DATABASE Woozle
GO
USE [Woozle]
GO
/****** Object:  Schema [woo]    Script Date: 12/09/2013 19:23:59 ******/
CREATE SCHEMA [woo] AUTHORIZATION [dbo]
GO
/****** Object:  Table [woo].[Role]    Script Date: 12/09/2013 19:23:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [woo].[Role](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [varchar](50) NOT NULL,
	[Description] [varchar](200) NULL,
	[LogicalId] [varchar](50) NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [woo].[Permission]    Script Date: 12/09/2013 19:23:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [woo].[Permission](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [varchar](50) NOT NULL,
	[Description] [varchar](200) NULL,
	[LogicalId] [varchar](50) NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [woo].[ModuleGroup]    Script Date: 12/09/2013 19:23:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [woo].[ModuleGroup](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Icon] [image] NULL,
	[Name] [varchar](50) NOT NULL,
	[Description] [varchar](200) NULL,
 CONSTRAINT [PK_ModuleGroup] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [woo].[City]    Script Date: 12/09/2013 19:23:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [woo].[City](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[ZipCode] [varchar](15) NOT NULL,
	[Name] [varchar](30) NOT NULL,
	[CountryId] [int] NOT NULL,
 CONSTRAINT [PK_City] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [woo].[Language]    Script Date: 12/09/2013 19:23:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [woo].[Language](
	[Id] [int] NOT NULL,
	[Code] [varchar](10) NOT NULL,
	[Name] [varchar](30) NOT NULL,
 CONSTRAINT [PK_Language] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [woo].[MandatorGroup]    Script Date: 12/09/2013 19:23:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [woo].[MandatorGroup](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [varchar](30) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [woo].[Translation]    Script Date: 12/09/2013 19:23:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [woo].[Translation](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[DefaultDescription] [varchar](max) NULL,
 CONSTRAINT [PK_Translation_1] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [woo].[StatusField]    Script Date: 12/09/2013 19:23:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [woo].[StatusField](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [varchar](50) NOT NULL,
 CONSTRAINT [PK_StatusField] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [woo].[Version]    Script Date: 12/09/2013 19:23:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [woo].[Version](
	[Version] [int] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[Version] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [woo].[TranslationItem]    Script Date: 12/09/2013 19:23:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [woo].[TranslationItem](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[TranslationId] [int] NOT NULL,
	[LanguageId] [int] NOT NULL,
	[Description] [varchar](max) NULL,
 CONSTRAINT [PK_TranslationItem] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [woo].[Country]    Script Date: 12/09/2013 19:23:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [woo].[Country](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[TranslationId] [int] NOT NULL,
 CONSTRAINT [PK_Country] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [woo].[Status]    Script Date: 12/09/2013 19:23:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [woo].[Status](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Value] [varchar](50) NOT NULL,
	[StatusFieldId] [int] NOT NULL,
	[TranslationId] [int] NOT NULL,
 CONSTRAINT [PK_StatusFieldValue] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [woo].[Mandator]    Script Date: 12/09/2013 19:23:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [woo].[Mandator](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [varchar](50) NOT NULL,
	[Street] [varchar](40) NULL,
	[Phone] [varchar](25) NULL,
	[CityId] [int] NULL,
	[ChangeCounter] [timestamp] NULL,
	[Email] [varchar](50) NULL,
	[Picture] [image] NULL,
	[MandatorGroupId] [int] NULL,
 CONSTRAINT [PK_Mandator] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [woo].[Module]    Script Date: 12/09/2013 19:23:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [woo].[Module](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Icon] [image] NULL,
	[Name] [varchar](50) NOT NULL,
	[Description] [varchar](200) NULL,
	[Version] [varchar](50) NULL,
	[ModuleGroupId] [int] NOT NULL,
	[LogicalId] [varchar](15) NULL,
	[Sequence] [smallint] NULL,
	[TranslationId] [int] NOT NULL,
 CONSTRAINT [PK_Module] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [woo].[Person]    Script Date: 12/09/2013 19:23:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [woo].[Person](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[LastName] [varchar](40) NULL,
	[FirstName] [varchar](30) NULL,
	[EMail] [varchar](50) NULL,
	[Picture] [image] NULL,
	[MandatorId] [int] NOT NULL,
	[Street] [varchar](40) NULL,
	[CityId] [int] NULL,
	[Phone] [varchar](25) NULL,
	[Mobile] [varchar](25) NULL,
	[Birthdate] [date] NULL,
	[EnterpriseName] [varchar](40) NULL,
	[SalutationStatusId] [int] NULL,
	[ChangeCounter] [timestamp] NULL,
 CONSTRAINT [PK_Person] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [woo].[MandatorRole]    Script Date: 12/09/2013 19:23:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [woo].[MandatorRole](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[MandId] [int] NOT NULL,
	[RoleId] [int] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [woo].[MandatorModules]    Script Date: 12/09/2013 19:23:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [woo].[MandatorModules](
	[ModuleId] [int] NOT NULL,
	[MandatorId] [int] NOT NULL,
 CONSTRAINT [PK_MandatorModules] PRIMARY KEY CLUSTERED 
(
	[ModuleId] ASC,
	[MandatorId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [woo].[Location]    Script Date: 12/09/2013 19:23:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [woo].[Location](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[MandatorId] [int] NOT NULL,
	[Name] [varchar](40) NOT NULL,
	[Street] [varchar](40) NULL,
	[CityId] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [woo].[Settings]    Script Date: 12/09/2013 19:23:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [woo].[Settings](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[MandatorId] [int] NOT NULL,
	[EventManagementPlanningEMail] [varchar](50) NULL,
	[EventManagementPlanningMobile] [varchar](25) NULL,
	[ChangeCounter] [timestamp] NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [woo].[Function]    Script Date: 12/09/2013 19:23:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [woo].[Function](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Icon] [image] NULL,
	[Name] [varchar](50) NOT NULL,
	[Description] [varchar](200) NULL,
	[ModuleId] [int] NOT NULL,
	[LogicalId] [varchar](50) NULL,
	[TranslationId] [int] NOT NULL,
	[Sequence] [smallint] NOT NULL,
 CONSTRAINT [PK_Function] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [woo].[User]    Script Date: 12/09/2013 19:23:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [woo].[User](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Username] [varchar](20) NOT NULL,
	[Password] [varchar](20) NOT NULL,
	[FlagActive] [bit] NOT NULL,
	[LastLogin] [datetime] NULL,
	[LastPasswordChange] [date] NULL,
	[LanguageId] [int] NOT NULL,
	[FlagActiveStatusId] [int] NOT NULL,
	[ChangeCounter] [timestamp] NOT NULL,
	[FirstName] [varchar](20) NULL,
	[LastName] [varchar](20) NULL,
 CONSTRAINT [PK_User] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [woo].[UserMandatorRole]    Script Date: 12/09/2013 19:23:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [woo].[UserMandatorRole](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[UserId] [int] NOT NULL,
	[MandatorRoleId] [int] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [woo].[Customer]    Script Date: 12/09/2013 19:23:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [woo].[Customer](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[MandatorId] [int] NULL,
	[ChangeCounter] [timestamp] NOT NULL,
	[Remark] [varchar](200) NULL,
	[PersonId] [int] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [woo].[FunctionPermission]    Script Date: 12/09/2013 19:23:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [woo].[FunctionPermission](
	[Id] [int] NOT NULL,
	[FunctionId] [int] NOT NULL,
	[PermissionId] [int] NOT NULL,
 CONSTRAINT [PK_FunctionPermission] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [woo].[MandatorRoleFunctionPermission]    Script Date: 12/09/2013 19:23:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [woo].[MandatorRoleFunctionPermission](
	[MandatorRoleId] [int] NOT NULL,
	[FunctionPermissionId] [int] NOT NULL,
 CONSTRAINT [PK_MandatorRoleFunctionPermission] PRIMARY KEY CLUSTERED 
(
	[MandatorRoleId] ASC,
	[FunctionPermissionId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  ForeignKey [FK_Country_Translation]    Script Date: 12/09/2013 19:23:59 ******/
ALTER TABLE [woo].[Country]  WITH CHECK ADD  CONSTRAINT [FK_Country_Translation] FOREIGN KEY([TranslationId])
REFERENCES [woo].[Translation] ([Id])
GO
ALTER TABLE [woo].[Country] CHECK CONSTRAINT [FK_Country_Translation]
GO
/****** Object:  ForeignKey [FK_Customer_Mandator]    Script Date: 12/09/2013 19:23:59 ******/
ALTER TABLE [woo].[Customer]  WITH CHECK ADD  CONSTRAINT [FK_Customer_Mandator] FOREIGN KEY([MandatorId])
REFERENCES [woo].[Mandator] ([Id])
GO
ALTER TABLE [woo].[Customer] CHECK CONSTRAINT [FK_Customer_Mandator]
GO
/****** Object:  ForeignKey [FK_Customer_Person]    Script Date: 12/09/2013 19:23:59 ******/
ALTER TABLE [woo].[Customer]  WITH CHECK ADD  CONSTRAINT [FK_Customer_Person] FOREIGN KEY([PersonId])
REFERENCES [woo].[Person] ([Id])
GO
ALTER TABLE [woo].[Customer] CHECK CONSTRAINT [FK_Customer_Person]
GO
/****** Object:  ForeignKey [FK_Function_Module]    Script Date: 12/09/2013 19:23:59 ******/
ALTER TABLE [woo].[Function]  WITH CHECK ADD  CONSTRAINT [FK_Function_Module] FOREIGN KEY([ModuleId])
REFERENCES [woo].[Module] ([Id])
GO
ALTER TABLE [woo].[Function] CHECK CONSTRAINT [FK_Function_Module]
GO
/****** Object:  ForeignKey [FK_Function_Translation]    Script Date: 12/09/2013 19:23:59 ******/
ALTER TABLE [woo].[Function]  WITH CHECK ADD  CONSTRAINT [FK_Function_Translation] FOREIGN KEY([TranslationId])
REFERENCES [woo].[Translation] ([Id])
GO
ALTER TABLE [woo].[Function] CHECK CONSTRAINT [FK_Function_Translation]
GO
/****** Object:  ForeignKey [FK__FunctionP__Funct__4183B671]    Script Date: 12/09/2013 19:23:59 ******/
ALTER TABLE [woo].[FunctionPermission]  WITH CHECK ADD  CONSTRAINT [FK__FunctionP__Funct__4183B671] FOREIGN KEY([FunctionId])
REFERENCES [woo].[Function] ([Id])
GO
ALTER TABLE [woo].[FunctionPermission] CHECK CONSTRAINT [FK__FunctionP__Funct__4183B671]
GO
/****** Object:  ForeignKey [FK__FunctionP__Permi__4277DAAA]    Script Date: 12/09/2013 19:23:59 ******/
ALTER TABLE [woo].[FunctionPermission]  WITH CHECK ADD  CONSTRAINT [FK__FunctionP__Permi__4277DAAA] FOREIGN KEY([PermissionId])
REFERENCES [woo].[Permission] ([Id])
GO
ALTER TABLE [woo].[FunctionPermission] CHECK CONSTRAINT [FK__FunctionP__Permi__4277DAAA]
GO
/****** Object:  ForeignKey [FK_Location_City]    Script Date: 12/09/2013 19:23:59 ******/
ALTER TABLE [woo].[Location]  WITH CHECK ADD  CONSTRAINT [FK_Location_City] FOREIGN KEY([CityId])
REFERENCES [woo].[City] ([Id])
GO
ALTER TABLE [woo].[Location] CHECK CONSTRAINT [FK_Location_City]
GO
/****** Object:  ForeignKey [FK_Location_Mandator]    Script Date: 12/09/2013 19:23:59 ******/
ALTER TABLE [woo].[Location]  WITH CHECK ADD  CONSTRAINT [FK_Location_Mandator] FOREIGN KEY([MandatorId])
REFERENCES [woo].[Mandator] ([Id])
GO
ALTER TABLE [woo].[Location] CHECK CONSTRAINT [FK_Location_Mandator]
GO
/****** Object:  ForeignKey [FK_Mandator_CityId]    Script Date: 12/09/2013 19:23:59 ******/
ALTER TABLE [woo].[Mandator]  WITH CHECK ADD  CONSTRAINT [FK_Mandator_CityId] FOREIGN KEY([CityId])
REFERENCES [woo].[City] ([Id])
GO
ALTER TABLE [woo].[Mandator] CHECK CONSTRAINT [FK_Mandator_CityId]
GO
/****** Object:  ForeignKey [FK_Mandator_MandatorGroup]    Script Date: 12/09/2013 19:23:59 ******/
ALTER TABLE [woo].[Mandator]  WITH CHECK ADD  CONSTRAINT [FK_Mandator_MandatorGroup] FOREIGN KEY([MandatorGroupId])
REFERENCES [woo].[MandatorGroup] ([Id])
GO
ALTER TABLE [woo].[Mandator] CHECK CONSTRAINT [FK_Mandator_MandatorGroup]
GO
/****** Object:  ForeignKey [FK_MandatorHasModules_Mandator]    Script Date: 12/09/2013 19:23:59 ******/
ALTER TABLE [woo].[MandatorModules]  WITH CHECK ADD  CONSTRAINT [FK_MandatorHasModules_Mandator] FOREIGN KEY([MandatorId])
REFERENCES [woo].[Mandator] ([Id])
GO
ALTER TABLE [woo].[MandatorModules] CHECK CONSTRAINT [FK_MandatorHasModules_Mandator]
GO
/****** Object:  ForeignKey [FK_MandatorHasModules_Module]    Script Date: 12/09/2013 19:23:59 ******/
ALTER TABLE [woo].[MandatorModules]  WITH CHECK ADD  CONSTRAINT [FK_MandatorHasModules_Module] FOREIGN KEY([ModuleId])
REFERENCES [woo].[Module] ([Id])
GO
ALTER TABLE [woo].[MandatorModules] CHECK CONSTRAINT [FK_MandatorHasModules_Module]
GO
/****** Object:  ForeignKey [FK__MandatorR__MandI__412EB0B6]    Script Date: 12/09/2013 19:23:59 ******/
ALTER TABLE [woo].[MandatorRole]  WITH CHECK ADD FOREIGN KEY([MandId])
REFERENCES [woo].[Mandator] ([Id])
GO
/****** Object:  ForeignKey [FK__MandatorR__RoleI__4222D4EF]    Script Date: 12/09/2013 19:23:59 ******/
ALTER TABLE [woo].[MandatorRole]  WITH CHECK ADD FOREIGN KEY([RoleId])
REFERENCES [woo].[Role] ([Id])
GO
/****** Object:  ForeignKey [FK_MandatorRoleFunctionPermission_FunctionPermission]    Script Date: 12/09/2013 19:23:59 ******/
ALTER TABLE [woo].[MandatorRoleFunctionPermission]  WITH CHECK ADD  CONSTRAINT [FK_MandatorRoleFunctionPermission_FunctionPermission] FOREIGN KEY([FunctionPermissionId])
REFERENCES [woo].[FunctionPermission] ([Id])
GO
ALTER TABLE [woo].[MandatorRoleFunctionPermission] CHECK CONSTRAINT [FK_MandatorRoleFunctionPermission_FunctionPermission]
GO
/****** Object:  ForeignKey [FK_MandatorRoleFunctionPermission_MandatorRole]    Script Date: 12/09/2013 19:23:59 ******/
ALTER TABLE [woo].[MandatorRoleFunctionPermission]  WITH CHECK ADD  CONSTRAINT [FK_MandatorRoleFunctionPermission_MandatorRole] FOREIGN KEY([MandatorRoleId])
REFERENCES [woo].[MandatorRole] ([Id])
GO
ALTER TABLE [woo].[MandatorRoleFunctionPermission] CHECK CONSTRAINT [FK_MandatorRoleFunctionPermission_MandatorRole]
GO
/****** Object:  ForeignKey [FK_Module_ModuleGroup]    Script Date: 12/09/2013 19:23:59 ******/
ALTER TABLE [woo].[Module]  WITH CHECK ADD  CONSTRAINT [FK_Module_ModuleGroup] FOREIGN KEY([ModuleGroupId])
REFERENCES [woo].[ModuleGroup] ([Id])
GO
ALTER TABLE [woo].[Module] CHECK CONSTRAINT [FK_Module_ModuleGroup]
GO
/****** Object:  ForeignKey [FK_Module_Translation]    Script Date: 12/09/2013 19:23:59 ******/
ALTER TABLE [woo].[Module]  WITH CHECK ADD  CONSTRAINT [FK_Module_Translation] FOREIGN KEY([TranslationId])
REFERENCES [woo].[Translation] ([Id])
GO
ALTER TABLE [woo].[Module] CHECK CONSTRAINT [FK_Module_Translation]
GO
/****** Object:  ForeignKey [FK_Person_City]    Script Date: 12/09/2013 19:23:59 ******/
ALTER TABLE [woo].[Person]  WITH CHECK ADD  CONSTRAINT [FK_Person_City] FOREIGN KEY([CityId])
REFERENCES [woo].[City] ([Id])
GO
ALTER TABLE [woo].[Person] CHECK CONSTRAINT [FK_Person_City]
GO
/****** Object:  ForeignKey [FK_Person_Mandator]    Script Date: 12/09/2013 19:23:59 ******/
ALTER TABLE [woo].[Person]  WITH CHECK ADD  CONSTRAINT [FK_Person_Mandator] FOREIGN KEY([MandatorId])
REFERENCES [woo].[Mandator] ([Id])
GO
ALTER TABLE [woo].[Person] CHECK CONSTRAINT [FK_Person_Mandator]
GO
/****** Object:  ForeignKey [FK_Person_Status_Salutation]    Script Date: 12/09/2013 19:23:59 ******/
ALTER TABLE [woo].[Person]  WITH CHECK ADD  CONSTRAINT [FK_Person_Status_Salutation] FOREIGN KEY([SalutationStatusId])
REFERENCES [woo].[Status] ([Id])
GO
ALTER TABLE [woo].[Person] CHECK CONSTRAINT [FK_Person_Status_Salutation]
GO
/****** Object:  ForeignKey [FK_Settings_Mandator]    Script Date: 12/09/2013 19:23:59 ******/
ALTER TABLE [woo].[Settings]  WITH CHECK ADD  CONSTRAINT [FK_Settings_Mandator] FOREIGN KEY([MandatorId])
REFERENCES [woo].[Mandator] ([Id])
GO
ALTER TABLE [woo].[Settings] CHECK CONSTRAINT [FK_Settings_Mandator]
GO
/****** Object:  ForeignKey [FK_Status_StatusField]    Script Date: 12/09/2013 19:23:59 ******/
ALTER TABLE [woo].[Status]  WITH CHECK ADD  CONSTRAINT [FK_Status_StatusField] FOREIGN KEY([StatusFieldId])
REFERENCES [woo].[StatusField] ([Id])
GO
ALTER TABLE [woo].[Status] CHECK CONSTRAINT [FK_Status_StatusField]
GO
/****** Object:  ForeignKey [FK_Status_Translation]    Script Date: 12/09/2013 19:23:59 ******/
ALTER TABLE [woo].[Status]  WITH CHECK ADD  CONSTRAINT [FK_Status_Translation] FOREIGN KEY([TranslationId])
REFERENCES [woo].[Translation] ([Id])
GO
ALTER TABLE [woo].[Status] CHECK CONSTRAINT [FK_Status_Translation]
GO
/****** Object:  ForeignKey [FK_TranslationItem_Language]    Script Date: 12/09/2013 19:23:59 ******/
ALTER TABLE [woo].[TranslationItem]  WITH CHECK ADD  CONSTRAINT [FK_TranslationItem_Language] FOREIGN KEY([LanguageId])
REFERENCES [woo].[Language] ([Id])
GO
ALTER TABLE [woo].[TranslationItem] CHECK CONSTRAINT [FK_TranslationItem_Language]
GO
/****** Object:  ForeignKey [FK_TranslationItem_Translation]    Script Date: 12/09/2013 19:23:59 ******/
ALTER TABLE [woo].[TranslationItem]  WITH CHECK ADD  CONSTRAINT [FK_TranslationItem_Translation] FOREIGN KEY([TranslationId])
REFERENCES [woo].[Translation] ([Id])
GO
ALTER TABLE [woo].[TranslationItem] CHECK CONSTRAINT [FK_TranslationItem_Translation]
GO
/****** Object:  ForeignKey [FK_User_Language]    Script Date: 12/09/2013 19:23:59 ******/
ALTER TABLE [woo].[User]  WITH CHECK ADD  CONSTRAINT [FK_User_Language] FOREIGN KEY([LanguageId])
REFERENCES [woo].[Language] ([Id])
GO
ALTER TABLE [woo].[User] CHECK CONSTRAINT [FK_User_Language]
GO
/****** Object:  ForeignKey [FK_User_Status]    Script Date: 12/09/2013 19:23:59 ******/
ALTER TABLE [woo].[User]  WITH CHECK ADD  CONSTRAINT [FK_User_Status] FOREIGN KEY([FlagActiveStatusId])
REFERENCES [woo].[Status] ([Id])
GO
ALTER TABLE [woo].[User] CHECK CONSTRAINT [FK_User_Status]
GO
/****** Object:  ForeignKey [FK_UserMandatorRole_MandatorRole]    Script Date: 12/09/2013 19:23:59 ******/
ALTER TABLE [woo].[UserMandatorRole]  WITH CHECK ADD  CONSTRAINT [FK_UserMandatorRole_MandatorRole] FOREIGN KEY([MandatorRoleId])
REFERENCES [woo].[MandatorRole] ([Id])
GO
ALTER TABLE [woo].[UserMandatorRole] CHECK CONSTRAINT [FK_UserMandatorRole_MandatorRole]
GO
/****** Object:  ForeignKey [FK_UserMandatorRole_User]    Script Date: 12/09/2013 19:23:59 ******/
ALTER TABLE [woo].[UserMandatorRole]  WITH CHECK ADD  CONSTRAINT [FK_UserMandatorRole_User] FOREIGN KEY([UserId])
REFERENCES [woo].[User] ([Id])
GO
ALTER TABLE [woo].[UserMandatorRole] CHECK CONSTRAINT [FK_UserMandatorRole_User]
GO
