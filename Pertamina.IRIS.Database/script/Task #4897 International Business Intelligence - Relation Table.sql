USE [DB_IRIS_V2]
GO
/*
-- Task #4897 
-- Create table TBLM_Type_Study
*/

CREATE TABLE [dbo].[TBLM_Type_Study](
	[Id] [uniqueidentifier] NOT NULL,
	[Created_By] [nvarchar](50) NULL,
	[Created_Date] [datetime2](7) NULL,
	[Updated_By] [nvarchar](50) NULL,
	[Updated_Date] [datetime2](7) NULL,
	[Deleted_Date] [datetime2](7) NULL,
	[Name] [nvarchar](200) NULL,
 CONSTRAINT [PK_TBLM_Type_Study_Id] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO


--create table TBLM_Confidentiality


CREATE TABLE [dbo].[TBLM_Confidentiality](
	[Id] [uniqueidentifier] NOT NULL,
	[Created_By] [nvarchar](50) NULL,
	[Created_Date] [datetime2](7) NULL,
	[Updated_By] [nvarchar](50) NULL,
	[Updated_Date] [datetime2](7) NULL,
	[Deleted_Date] [datetime2](7) NULL,
	[Name] [nvarchar](200) NULL,
	[Hex_Color] NVARCHAR(25) NULL,
 CONSTRAINT [PK_TBLM_Confidentiality_Id] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO


---Table TBLM_Directory
CREATE TABLE [dbo].[TBLM_Directory](
	[Id] [uniqueidentifier] NOT NULL,
	[Created_By] [nvarchar](50) NULL,
	[Created_Date] [datetime2](7) NULL,
	[Updated_By] [nvarchar](50) NULL,
	[Updated_Date] [datetime2](7) NULL,
	[Deleted_Date] [datetime2](7) NULL,
	[Feature_Name] [nvarchar](200) NULL,
	[Path] NVARCHAR(max) NULL,
 CONSTRAINT [PK_TBLM_Directory_Id] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO


---TBLM Negara_Mitra_Infomation
CREATE TABLE [dbo].[TBLM_Negara_Mitra_Infomation](
	[Id] [uniqueidentifier] NOT NULL,
	[Created_By] [nvarchar](50) NULL,
	[Created_Date] [datetime2](7) NULL,
	[Updated_By] [nvarchar](50) NULL,
	[Updated_Date] [datetime2](7) NULL,
	[Deleted_Date] [datetime2](7) NULL,
	[Negara_Mitra_Id] UNIQUEIDENTIFIER NULL,
	[Population] [nvarchar](200) NULL,
	[GDP] NVARCHAR(200) NULL,
	[GDP_Per_Capita] NVARCHAR(200) NULL,
	[Oil_Gas_Reserves] NVARCHAR(200) NULL,
	[Oil_Production] NVARCHAR(200) NULL,
 CONSTRAINT [PK_TBLM_Negara_Mitra_Infomation_Id] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO


ALTER TABLE dbo.TBLM_Negara_Mitra_Infomation 
ADD CONSTRAINT FK_TBLM_Negara_Mitra_Infomation_TBLM_Negara_Mitra
FOREIGN KEY (Negara_Mitra_Id)
REFERENCES dbo.TBLM_Negara_Mitra(Id)
 
 --create table TBLT_International_Business_Intelligence
 CREATE TABLE [dbo].[TBLT_International_Business_Intelligence](
	[Id] [uniqueidentifier] NOT NULL,
	[Created_By] [nvarchar](50) NULL,
	[Created_Date] [datetime2](7) NULL,
	[Updated_By] [nvarchar](50) NULL,
	[Updated_Date] [datetime2](7) NULL,
	[Deleted_Date] [datetime2](7) NULL,
	[Type_Study_Id] UNIQUEIDENTIFIER NULL,
	[Research_Date] DATETIME NULL,
	[Title] NVARCHAR(400) NULL,
	[Negara_Mitra_Id] UNIQUEIDENTIFIER null,
	[Confidentiality_Id] UNIQUEIDENTIFIER null,
	[Description] NVARCHAR(max) NULL,
	[Is_Draft] BIT null,
 CONSTRAINT [PK_TBLT_International_Business_Intelligence_Id] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE dbo.TBLT_International_Business_Intelligence
ADD CONSTRAINT FK_TBLT_International_Business_Intelligence_TBLM_Type_Study
FOREIGN KEY (Type_Study_Id)
REFERENCES dbo.TBLM_Type_Study(Id)
GO

ALTER TABLE dbo.TBLT_International_Business_Intelligence
ADD CONSTRAINT FK_TBLT_International_Business_Intelligence_TBLM_Negara_Mitra
FOREIGN KEY (Negara_Mitra_Id)
REFERENCES dbo.TBLM_Negara_Mitra(Id)
GO

ALTER TABLE dbo.TBLT_International_Business_Intelligence
ADD CONSTRAINT FK_TBLT_International_Business_Intelligence_TBLM_Confidentiality
FOREIGN KEY (Confidentiality_Id)
REFERENCES dbo.TBLM_Confidentiality(Id)

--- Create table TBLT_International_Business_Intelligence_Country
CREATE TABLE [dbo].[TBLT_International_Business_Intelligence_Country](
	[Id] [uniqueidentifier] NOT NULL,
	[Created_By] [nvarchar](50) NULL,
	[Created_Date] [datetime2](7) NULL,
	[Updated_By] [nvarchar](50) NULL,
	[Updated_Date] [datetime2](7) NULL,
	[Deleted_Date] [datetime2](7) NULL,
	[International_Business_Intelligence_Id] UNIQUEIDENTIFIER NULL,
	[Negara_Mitra_Id] UNIQUEIDENTIFIER null,
 CONSTRAINT [PK_TBLT_International_Business_Intelligence_Country_Id] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE TBLT_International_Business_Intelligence_Country 
ADD CONSTRAINT FK_TBLT_International_Business_Intelligence_Country_TBLT_International_Business_Intelligence
FOREIGN KEY (International_Business_Intelligence_Id)
REFERENCES dbo.TBLT_International_Business_Intelligence(Id)
GO

ALTER TABLE TBLT_International_Business_Intelligence_Country 
ADD CONSTRAINT FK_TBLT_International_Business_Intelligence_Country_TBLM_Negara_Mitra
FOREIGN KEY (Negara_Mitra_Id)
REFERENCES dbo.TBLM_Negara_Mitra(Id)

CREATE TABLE [dbo].[TBLT_International_Business_Intelligence_Authors](
	[Id] [uniqueidentifier] NOT NULL,
	[Created_By] [nvarchar](50) NULL,
	[Created_Date] [datetime2](7) NULL,
	[Updated_By] [nvarchar](50) NULL,
	[Updated_Date] [datetime2](7) NULL,
	[Deleted_Date] [datetime2](7) NULL,
	[International_Business_Intelligence_Id] UNIQUEIDENTIFIER NULL,
	[Name] NVARCHAR(200) null,
 CONSTRAINT [PK_TBLT_International_Business_Intelligence_Authors_Id] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE TBLT_International_Business_Intelligence_Authors 
ADD CONSTRAINT FK_TBLT_International_Business_Intelligence_Authors_TBLT_International_Business_Intelligence
FOREIGN KEY (International_Business_Intelligence_Id)
REFERENCES dbo.TBLT_International_Business_Intelligence(Id)


--Create table TBTL_International_Business_Intelligence_Document
CREATE TABLE [dbo].[TBLT_International_Business_Intelligence_Document](
	[Id] [uniqueidentifier] NOT NULL,
	[Created_By] [nvarchar](50) NULL,
	[Created_Date] [datetime2](7) NULL,
	[Updated_By] [nvarchar](50) NULL,
	[Updated_Date] [datetime2](7) NULL,
	[Deleted_Date] [datetime2](7) NULL,
	[International_Business_Intelligence_Id] UNIQUEIDENTIFIER NULL,
	[File_Name_System] NVARCHAR(200) null,
	[File_Name_User] NVARCHAR(200) null,
 CONSTRAINT [PK_TBLT_International_Business_Intelligence_Document_Id] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE TBLT_International_Business_Intelligence_Document 
ADD CONSTRAINT FK_TBLT_International_Business_Intelligence_Document_TBLT_International_Business_Intelligence
FOREIGN KEY (International_Business_Intelligence_Id)
REFERENCES dbo.TBLT_International_Business_Intelligence(Id)