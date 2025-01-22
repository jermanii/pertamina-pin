/*
  Manual Insert Table data Master

*/
/*===========================================================================
Hirarchical Table
		TBLM_HSH					-> TBLM_Entitas_Pertamina	-> TBLM_Fungsi		-> TBLM_PIC_Fungsi
		TBLM_Continent				-> TBLM_Kawasan_Mitra		-> TBLM_Negara_Mitra 
		TBLM_Klasifikasi_Perjanjian	-> TBLM_Jenis_Perjanjian		
=============================================================================
*/

-------------------------------Holding----------------------------------------
INSERT INTO [dbo].[TBLM_HSH]
           ([Id]
           ,[Create_By]
           ,[Create_Date]
           ,[Update_By]
           ,[Update_Date]
           ,[Name]
           ,[Description]
           ,[Order_seq])
     VALUES
           (
		   NEWID()
		   ,'Admin' 
		   ,getdate()
		   ,'Admin'
		   ,GETDATE()
		   ,'NAMA HOLDING'  ---Nama Holding
		   ,'DESCRIPTION HOLDING'
		   ,1  ---Order Sequensial
		   )
GO

INSERT INTO [dbo].[TBLM_Entitas_Pertamina]
           ([Id]
           ,[Create_By]
           ,[Create_Date]
           ,[Update_By]
           ,[Update_Date]
           ,[HSH_Id]
           ,[Code]
           ,[Company_Name]
           ,[Order_Seq])
     VALUES
           (
		    NEWID()
			,'Admin'
			,GETDATE()
			,'Admin'
			,GETDATE()
			,'5790371F-CF72-4D56-A262-4CD05C04F8FF' ---ID HOlding lihat table TBLM_HSH
			,'3333' ---Code Entitas Pertamina
			,'Nama Perusahaan'
			,1  --Urutan data
		   )
GO


INSERT INTO [dbo].[TBLM_Fungsi]
           ([Id]
           ,[Create_By]
           ,[Create_Date]
           ,[Update_By]
           ,[Update_Date]
           ,[Entitas_Pertamina_Id]
           ,[Fungsi_Name]
           ,[Description])
     VALUES
           (
		    NEWID()
			,'Admin'
			,GETDATE()
			,'Admin'
			,GETDATE()
			,'168099F7-E1B0-4F4F-8FE1-4A20EC5999B4'   --ID Entitas Pertamina lihat TBLM_Entitas_Pertamina
			,'Nama Fungsi'
			,'Deskripsi Fungsi'
		   )
GO

INSERT INTO [dbo].[TBLM_PIC_Fungsi]
           ([Id]
           ,[Create_By]
           ,[Create_Date]
           ,[Update_By]
           ,[Update_Date]
           ,[Fungsi_Id]
           ,[PIC_Name]
           ,[Phone])
     VALUES
           (
		    NEWID()
			,'Admin'
			,GETDATE()
			,'Admin'
			,GETDATE()
			,'968E8323-93F1-4427-AEDA-DA365FC85C24' --ID Fungsi Lihat table TBLM_Fungsi
			,'Nama PIC'
			,'No Phone'
		   )
GO


------------------------Continent--------------------------------

INSERT INTO [dbo].[TBLM_Continent]
           ([Id]
           ,[Create_By]
           ,[Create_Date]
           ,[Update_By]
           ,[Update_Date]
           ,[Continent_Name]
           ,[Description]
           ,[Order_Seq])
     VALUES
           (
		    NEWID()
			,'Admin'
			,GETDATE()
			,'Admin'
			,GETDATE()
			,'Nama Continent'
			,'Deskripsi'
			,1   ---No urut
		   )
GO

INSERT INTO [dbo].[TBLM_Kawasan_Mitra]
           ([Id]
           ,[Create_By]
           ,[Create_Date]
           ,[Update_By]
           ,[Update_Date]
           ,[Continent_Id]
           ,[Nama_Kawasan]
           ,[Description]
           ,[Order_seq])
     VALUES
           (
		    NEWID()
			,'Admin'
			,GETDATE()
			,'Admin'
			,GETDATE()
			,'5429C041-6A58-4893-A9AC-7F78A98EEC19' -- ID Continent lihat TBLM_Continent
			,'Nama Kawasan'
			,'Deskripsi'
			,1 -- No Urut
		   )
GO

INSERT INTO [dbo].[TBLM_Negara_Mitra]
           ([Id]
           ,[Create_By]
           ,[Create_Date]
           ,[Update_By]
           ,[Update_Date]
           ,[Kawasan_Mitra_Id]
           ,[Nama_Negara]
           ,[Description])
     VALUES
           (
		    NEWID()
			,'Admin'
			,GETDATE()
			,'Admin'
			,GETDATE()
			,'1D42FFDB-28D3-4D37-86BE-F19BCBF1D91F' -- ID Kawasan Mitra lihat TBLM_Kawasan_Mitra
			,'Nama Negara'
			,'Deskripsi'
			)
GO

----------------------Perjanjian ---------------------------

INSERT INTO [dbo].[TBLM_Klasifikasi_Jenis_Perjanjian]
           ([Id]
           ,[Create_By]
           ,[Create_Date]
           ,[Update_By]
           ,[Update_Date]
           ,[Name]
           ,[Description]
           ,[Order_Seq])
     VALUES
           (
		    NEWID()
			,'Admin'
			,GETDATE()
			,'Admin'
			,GETDATE()
			,'Nama Klasifikasi Perjanjian'
			,'Deskripsi'
			,1  --No Urut
		   )
GO
INSERT INTO [dbo].[TBLM_Jenis_Perjanjian]
           ([Id]
           ,[Create_By]
           ,[Create_Date]
           ,[Update_By]
           ,[Update_Date]
		   ,[Klasifikasi_Jenis_Perjanjian_Id]
           ,[Name]
           ,[Description]
           ,[Order_Seq]
           )
     VALUES
           (
		    NEWID()
			,'Admin'
			,GETDATE()
			,'Admin'
			,GETDATE()
			,'7AB37D71-BCAA-4B1A-B608-45E3D2E988C1' --ID Klasifikasi Perjanjian table TBLM_Klasifikasi_Perjanjian
			,'Nama Jenis Perjanjian'
			,'Deskripsi'
			,1 -- No Urut
		   )
GO


------------Non Hirarchical Table-----------------------------------

INSERT INTO [dbo].[TBLM_Faktor_Kendala]
           ([Id]
           ,[Create_By]
           ,[Create_Date]
           ,[Update_By]
           ,[Update_Date]
           ,[Name]
           ,[Order_Seq])
     VALUES
           (
		    NEWID()
			,'Admin'
			,GETDATE()
			,'Admin'
			,GETDATE()
			,'Faktor Kendala'
			,1  -- no urut
		   )
GO

INSERT INTO [dbo].[TBLM_Kementrian_Lembaga]
           ([Id]
           ,[Create_By]
           ,[Create_Date]
           ,[Update_By]
           ,[Update_Date]
           ,[Lembaga_Name]
           ,[Description])
     VALUES
           (
		    NEWID()
			,'Admin'
			,GETDATE()
			,'Admin'
			,GETDATE()
			,'Nama Lembaga Negara'
			,'Deskripsi'
		   )
GO

INSERT INTO [dbo].[TBLM_Stream_Business]
           ([Id]
           ,[Create_By]
           ,[Create_Date]
           ,[Update_By]
           ,[Update_Date]
           ,[Name]
           ,[Description]
           ,[Order_Seq])
     VALUES
           (
		    NEWID()
			,'Admin'
			,GETDATE()
			,'Admin'
			,GETDATE()
			,'Nama Stream Business'
			,'Deskripsi'
			,1 -- no urut
		   )
GO


INSERT INTO [dbo].[TBLM_Target_Mitra]
           ([Id]
           ,[Create_By]
           ,[Create_Date]
           ,[Update_By]
           ,[Update_Date]
           ,[Name]
           ,[Description]
           ,[Order_Seq])
     VALUES
           (
		    NEWID()
			,'Admin'
			,GETDATE()
			,'Admin'
			,GETDATE()
			,'Nama Target Mitra'
			,'Deskripsi'
			,1 --no Urut
		   )
GO

INSERT INTO [dbo].[TBLM_Status_Berlaku]
           ([Id]
           ,[Create_By]
           ,[Create_Date]
           ,[Update_By]
           ,[Update_Date]
           ,[Status_Name]
           ,[Color_Hexa]
           ,[Color_Name]
           ,[Order_Seq])
     VALUES
           (
		    NEWID()
			,'Admin'
			,GETDATE()
			,'Admin'
			,GETDATE()
			,'Nama Status'
			,'Kode hexa warna'
			,'nama warna'
			,1 --no urut
		   )
GO



--ALTER TABLE TBLT_Agreement Alter Column Related_Agreement_Id uniqueidentifier null