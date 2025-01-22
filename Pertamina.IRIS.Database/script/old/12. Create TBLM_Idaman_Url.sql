USE [DB_IRIS]
GO
/****** Object:  Table [dbo].[TBLM_Idaman_Url]    Script Date: 7/24/2023 11:44:44 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TBLM_Idaman_Url](
	[Id] [uniqueidentifier] NOT NULL,
	[Created_By] [nvarchar](50) NULL,
	[Created_Date] [datetime2](7) NULL,
	[Updated_By] [nvarchar](50) NULL,
	[Updated_Date] [datetime2](7) NULL,
	[Deleted_Date] [datetime2](7) NULL,
	[Name_End_Point] [nvarchar](250) NULL,
	[End_Point_Url] [nvarchar](500) NULL,
	[Parameter_Take] [nvarchar](500) NULL,
 CONSTRAINT [PK_TBLM_Idaman_Url] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
INSERT [dbo].[TBLM_Idaman_Url] ([Id], [Created_By], [Created_Date], [Updated_By], [Updated_Date], [Deleted_Date], [Name_End_Point], [End_Point_Url], [Parameter_Take]) VALUES (N'720846bb-d3d9-4978-82ee-252db2a94a75', NULL, NULL, NULL, NULL, NULL, N'GetUserBySearchTextAsync', N'users/company/1010?searchText=', N'&page=1&take=25')
INSERT [dbo].[TBLM_Idaman_Url] ([Id], [Created_By], [Created_Date], [Updated_By], [Updated_Date], [Deleted_Date], [Name_End_Point], [End_Point_Url], [Parameter_Take]) VALUES (N'5d79d8da-2f33-42f7-b80e-34eb4b75ad06', NULL, NULL, NULL, NULL, NULL, N'GetPositionByRoleIdAplicationAsync', N'roles/{0}/positions', NULL)
INSERT [dbo].[TBLM_Idaman_Url] ([Id], [Created_By], [Created_Date], [Updated_By], [Updated_Date], [Deleted_Date], [Name_End_Point], [End_Point_Url], [Parameter_Take]) VALUES (N'8b860e59-94d5-4485-9648-65f9fa18ffd9', NULL, NULL, NULL, NULL, NULL, N'GetUserAsync', N'users/', NULL)
INSERT [dbo].[TBLM_Idaman_Url] ([Id], [Created_By], [Created_Date], [Updated_By], [Updated_Date], [Deleted_Date], [Name_End_Point], [End_Point_Url], [Parameter_Take]) VALUES (N'a7aa14f7-56cb-435b-bea5-67059dde12f6', NULL, NULL, NULL, NULL, NULL, N'GetPositionsBySearchTextAsync', N'positions?searchText=', N'&page=1&take=25')
INSERT [dbo].[TBLM_Idaman_Url] ([Id], [Created_By], [Created_Date], [Updated_By], [Updated_Date], [Deleted_Date], [Name_End_Point], [End_Point_Url], [Parameter_Take]) VALUES (N'8eb4551a-eb63-4726-a2e2-93c973805282', NULL, NULL, NULL, NULL, NULL, N'GetWhitelistsAsync', N'positions/whiteLists/', N'?take=25')
INSERT [dbo].[TBLM_Idaman_Url] ([Id], [Created_By], [Created_Date], [Updated_By], [Updated_Date], [Deleted_Date], [Name_End_Point], [End_Point_Url], [Parameter_Take]) VALUES (N'93580aa2-7a01-4372-9f72-cd49b22b16d2', NULL, NULL, NULL, NULL, NULL, N'GetApplicationRolesAsync', N'Applications/Roles/', NULL)
INSERT [dbo].[TBLM_Idaman_Url] ([Id], [Created_By], [Created_Date], [Updated_By], [Updated_Date], [Deleted_Date], [Name_End_Point], [End_Point_Url], [Parameter_Take]) VALUES (N'e2074c3e-b548-4845-8b6f-de275cc5decf', NULL, NULL, NULL, NULL, NULL, N'GetPositionUserAsync', N'positions/users/', NULL)
INSERT [dbo].[TBLM_Idaman_Url] ([Id], [Created_By], [Created_Date], [Updated_By], [Updated_Date], [Deleted_Date], [Name_End_Point], [End_Point_Url], [Parameter_Take]) VALUES (N'd7eaa065-0075-453d-87ef-e1896fe64c3e', NULL, NULL, NULL, NULL, NULL, N'GetUserParentAsync', N'positions/user/parent/', NULL)
INSERT [dbo].[TBLM_Idaman_Url] ([Id], [Created_By], [Created_Date], [Updated_By], [Updated_Date], [Deleted_Date], [Name_End_Point], [End_Point_Url], [Parameter_Take]) VALUES (N'39ccc9ae-e4a3-4720-825d-e6e739a131c8', NULL, NULL, NULL, NULL, NULL, N'GetUserOrganizationsAsync', N'organizations/', NULL)
GO
