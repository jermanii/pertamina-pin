USE [DB_IRIS]
GO

/****** Object:  View [dbo].[vw_Export_Initiative]    Script Date: 8/2/2023 9:50:04 AM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO



CREATE OR ALTER     VIEW [dbo].[vw_Export_Initiative] (
 [Initiative_Id]
,[Tanggal_Dibuat]
,[Create_By]
,[Judul_Inisiasi]
,[Stream_Business_Id]
,[Stream Business]
,[Entitas_Pertamina_Id]
,[Pertamina]
,[HSH_id]
,[HSH Name]
,[PIC]
,[Negara_Mitra_Id]
,[Negara Mitra]
,[Kawasan_Mitra_Id]
,[Kawasan Mitra]
,[Lokasi Proyek]
,[Nilai_Proyek]
,[Initiative_Type_Id]
,[Initiative_type]
,[Initiative_Stage_Id]
,[Intiative_Stage]
,[Inititative_Status_Id]
,[Initiative_Status]
,[Perusahaan Mitra]
,[Interest_Name]
,[Target_Waktu_Proyek]
,[Scope]
,[Value_For_Indonesia]
,[Progress]
,[Isu_Kendala]
,[Support_Pemerintah]
,[Referal]
,[Potensi Eskalasi]
,[Related_Agreement]
,[Catatan_Tambahan]
,[Kode_Agreement]
)
WITH SCHEMABINDING
AS 
WITH InitiativeEntitasPertamina AS(
   SELECT iep.Initiative_Id
    ,hsh.id AS HSH_Id
    ,hsh.Name AS  [H SH]
	,ep.id AS Entitas_Pertamina_Id
	,ep.Company_Name AS  [Company Name]
   FROM dbo.TBLT_Initiative_Entitas_Pertamina iep
   INNER JOIN dbo.TBLM_Entitas_Pertamina ep
   ON iep.Entitas_Pertamina_Id=ep.Id
   INNER JOIN dbo.TBLM_HSH HSH
   ON ep.HSH_Id=HSH.Id
   --INNER JOIN dbo.vw_Dim_Entitas_Pertamina  ep
   --ON iep.Entitas_Pertamina_Id=ep.Entitas_Pertamina_Id
 ),
 RowInitiativeEntitasPertamina AS (
      SELECT ini.Id AS Initiative_id
		   ,(SELECT ','+iep.[Company Name]
		   FROM InitiativeEntitasPertamina iep
		   WHERE ini.id=iep.Initiative_Id
		   FOR XML PATH('')) AS Entitas_Pertamina
		FROM dbo.TBLT_Initiative ini
		GROUP BY ini.Id  
 ),
 RowInitiativeEntitasPertaminaId AS (
      SELECT ini.Id AS Initiative_id
		   ,(SELECT DISTINCT ','+CAST(iep.Entitas_Pertamina_Id AS VARCHAR(36))
		   FROM InitiativeEntitasPertamina iep
		   WHERE ini.id=iep.Initiative_Id
		   FOR XML PATH('')) AS Entitas_Pertamina_Id
		FROM dbo.TBLT_Initiative ini
		GROUP BY ini.Id  
 ),
 RowInitiativeHSHId AS (
      SELECT ini.Id AS Initiative_id
		   ,(SELECT DISTINCT ','+CAST(iep.HSH_Id AS VARCHAR(36))
		   FROM InitiativeEntitasPertamina iep
		   WHERE ini.id=iep.Initiative_Id
		   FOR XML PATH('')) AS HSH_Id
		FROM dbo.TBLT_Initiative ini
		GROUP BY ini.Id  
 ),
 RowInitiativeHSHName AS (
      SELECT ini.Id AS Initiative_id
		   ,(SELECT DISTINCT ','+iep.[H SH]
		   FROM InitiativeEntitasPertamina iep
		   WHERE ini.id=iep.Initiative_Id
		   FOR XML PATH('')) AS HSH_Name
		FROM dbo.TBLT_Initiative ini
		GROUP BY ini.Id  
 ),
RowInitativeLokasiProyek AS (
     SELECT ini.Id AS Initiative_id
		   ,(SELECT ','+ilp.Lokasi_Proyek
		   FROM dbo.TBLT_Initiative_Lokasi_Proyek ilp
		   WHERE ini.id=ilp.Initiative_Id
		   FOR XML PATH('')) AS Lokasi_Proyek
		FROM dbo.TBLT_Initiative ini
		GROUP BY ini.Id  
),
InitiativeStreamBusiness AS (
    SELECT isb.Initiative_Id
		,isb.Stream_Business_Id
		,sb.Name AS Stream_Business_Name
	FROM dbo.TBLT_Initiative_Stream_Business  isb
	INNER JOIN dbo.TBLM_Stream_Business sb
	ON isb.Stream_Business_Id=sb.Id
),
 RowIntiativeStreamBusiness AS (
      SELECT ini.Id AS Initiative_id
		   ,(SELECT ','+isb.Stream_Business_Name
		   FROM InitiativeStreamBusiness isb
		   WHERE ini.id=isb.Initiative_Id
		   FOR XML PATH('')) AS Stream_Business_Name
		FROM dbo.TBLT_Initiative ini
		GROUP BY ini.Id  
 ),
 RowIntiativeStreamBusinessId AS (
      SELECT ini.Id AS Initiative_id
		   ,(SELECT DISTINCT ','+ CAST(isb.Stream_Business_Id AS VARCHAR(36))
		   FROM InitiativeStreamBusiness isb
		   WHERE ini.id=isb.Initiative_Id
		   FOR XML PATH('')) AS Stream_Business_Id
		FROM dbo.TBLT_Initiative ini
		GROUP BY ini.Id  
 ),
 InitiativePICFungsi AS (
	  SELECT ipf.Initiative_Id
			,f.Fungsi_Name
			,pf.PIC_Name 
	  FROM dbo.TBLT_Initiative_PIC_Fungsi AS ipf
	  INNER JOIN dbo.TBLM_PIC_Fungsi pf
	  ON ipf.PIC_Fungsi_Id=pf.Id
	  INNER JOIN dbo.TBLM_Fungsi f
	  ON pf.Fungsi_Id=f.Id
 ),
 RowInitiativePICFungsi AS (
      SELECT ini.Id AS Initiative_id
		   ,(SELECT ','+CONCAT(ipf.Fungsi_Name,' - ', ipf.PIC_Name)
		   FROM InitiativePICFungsi ipf
		   WHERE ini.id=ipf.Initiative_Id
		   FOR XML PATH('')) AS PIC_Fungsi_Name
		FROM dbo.TBLT_Initiative ini
		GROUP BY ini.Id      
 ),
 InitiativeNegaraMitra AS (
    SELECT inm.Initiative_Id
	  ,inm.Negara_Mitra_Id
	  ,nm.Nama_Negara AS  [Negara Mitra]
	  ,nm.Kawasan_Mitra_Id
	  ,km.Nama_Kawasan AS  Kawasan
	  --,nm.Continent
	FROM dbo.TBLT_Initiative_Negara_Mitra inm
	INNER JOIN dbo.TBLM_Negara_Mitra nm
	ON inm.Negara_Mitra_Id=nm.Id
	INNER JOIN dbo.TBLM_Kawasan_Mitra Km
	ON nm.Kawasan_Mitra_Id=km.Id
 ),
 RowInitiativeNegaraMitra AS (
      SELECT ini.Id AS Initiative_id
		   ,(SELECT ','+inm.[Negara Mitra]
		   FROM InitiativeNegaraMitra inm
		   WHERE ini.id=inm.Initiative_Id
		   FOR XML PATH('')) AS Negara_Mitra
		FROM dbo.TBLT_Initiative ini
		GROUP BY ini.Id      
 ),
 RowInitiativeNegaraMitraId AS (
      SELECT ini.Id AS Initiative_id
		   ,(SELECT ','+ CAST(inm.Negara_Mitra_Id AS VARCHAR(36))
		   FROM InitiativeNegaraMitra inm
		   WHERE ini.id=inm.Initiative_Id
		   FOR XML PATH('')) AS Negara_Mitra_Id
		FROM dbo.TBLT_Initiative ini
		GROUP BY ini.Id      
 ), 
 RowInitiativeKawasanMitraId AS (
      SELECT ini.Id AS Initiative_id
		   ,(SELECT DISTINCT ','+ CAST(inm.Kawasan_Mitra_Id AS VARCHAR(36))
		   FROM InitiativeNegaraMitra inm
		   WHERE ini.id=inm.Initiative_Id
		   FOR XML PATH('')) AS Kawasan_Mitra_Id
		FROM dbo.TBLT_Initiative ini
		GROUP BY ini.Id      
 ), 
 RowInitiativeKawasanMitra AS (
      SELECT ini.Id AS Initiative_id
		   ,(SELECT DISTINCT ','+ inm.Kawasan
		   FROM InitiativeNegaraMitra inm
		   WHERE ini.id=inm.Initiative_Id
		   FOR XML PATH('')) AS Kawasan_Mitra
		FROM dbo.TBLT_Initiative ini
		GROUP BY ini.Id      
 ), 
 RowInitativePartners AS (
     SELECT ini.Id AS Initiative_id
		   ,(SELECT ','+ipt.Partner_Name
		   FROM dbo.TBLT_Initiative_Partners ipt
		   WHERE ini.id=ipt.Initiative_Id
		   FOR XML PATH('')) AS Perusahaan_Mitra
		FROM dbo.TBLT_Initiative ini
		GROUP BY ini.Id  
)

SELECT 
	tin.Id AS Initiative_Id
	,CONVERT(VARCHAR(10),tin.Create_Date,31) AS Tanggal_Dibuat
	,tin.Create_By
	,tin.Judul_Inisiasi
	,IIF(sbi.Stream_Business_Id IS NULL,NULL,SUBSTRING(sbi.Stream_Business_Id,2,LEN(sbi.Stream_Business_Id))) AS Stream_Business_Id
	,IIF(isb.Stream_Business_Name IS NULL,NULL,SUBSTRING(isb.Stream_Business_Name,2,LEN(isb.Stream_Business_Name))) AS [Stream Business]
	,IIF(iepi.Entitas_Pertamina_Id IS NULL,NULL,SUBSTRING(iepi.Entitas_Pertamina_Id,2,LEN(iepi.Entitas_Pertamina_Id))) AS Entitas_Pertamina_Id
	,IIF(ep.Entitas_Pertamina IS NULL,NULL,SUBSTRING(ep.Entitas_Pertamina,2,LEN(ep.Entitas_Pertamina))) AS [Pertamina]
	,IIF(hshi.HSH_Id IS NULL,NULL,SUBSTRING(hshi.HSH_Id,2,LEN(hshi.HSH_Id))) AS HSH_Id
	,IIF(hshn.HSH_Name IS NULL ,NULL,SUBSTRING(hshn.HSH_Name,2,LEN(hshn.HSH_Name))) AS HSH_Name
	,IIF(pf.PIC_Fungsi_Name IS NULL,NULL,SUBSTRING(pf.PIC_Fungsi_Name,2,LEN(pf.PIC_Fungsi_Name))) AS [PIC]
	,IIF(inmi.Negara_Mitra_Id IS NULL,NULL,SUBSTRING(inmi.Negara_Mitra_Id,2,LEN(inmi.Negara_Mitra_Id))) AS Negara_Mitra_Id
	,IIF(inm.Negara_Mitra IS NULL ,NULL,SUBSTRING(inm.Negara_Mitra,2,LEN(inm.Negara_Mitra))) AS [Negara Mitra]
	,IIF(ikmi.Kawasan_Mitra_Id IS NULL,NULL,SUBSTRING(ikmi.Kawasan_Mitra_Id,2,LEN(ikmi.Kawasan_Mitra_Id))) AS Kawasan_Mitra_Id
	,IIF(ikmn.Kawasan_Mitra IS NULL,NULL,SUBSTRING(ikmn.Kawasan_Mitra,2,LEN(ikmn.Kawasan_Mitra))) AS Kawasan_Mitra
	,IIF(lp.Lokasi_Proyek IS NULL,NULL,SUBSTRING(lp.Lokasi_Proyek,2,LEN(lp.Lokasi_Proyek))) AS [Lokasi Proyek]
	,tin.Nilai_Proyek
	,tin.Initiative_Types_Id
	,it.Status_Name AS Initiative_type
	,tin.Initiative_Stage_Id
	,ist.Name AS Intiative_Stage
	,tin.Initiative_Status_Id
	,its.Status_Name AS Initiative_Status
	,IIF(ipt.PErusahaan_Mitra IS NULL,NULL,SUBSTRING(ipt.Perusahaan_Mitra,2,LEN(ipt.Perusahaan_Mitra))) AS [Perusahaan Mitra]
	,itr.Name AS Interest_Name
	,tin.Target_Waktu_Proyek
	,tin.Scope
	,tin.Value_For_Indonesia
	,tin.Progress
	,tin.Isu_Kendala
	,tin.Support_Pemerintah
	,tin.Referal
	,tin.[Potensi Eskalasi]
	,ag.Judul_Perjanjian
	,tin.Catatan_Tambahan
	,ag.Kode_Agreement
FROM dbo.TBLT_Initiative tin
INNER JOIN dbo.TBLM_Initiative_Types it
ON tin.Initiative_Types_Id=it.Id
INNER JOIN dbo.TBLM_Initiative_Stages ist
ON tin.Initiative_Stage_Id=ist.Id
INNER JOIN dbo.TBLM_Interest itr
ON tin.Interest_Id=itr.Id
INNER JOIN dbo.TBLM_Initiative_Status its
ON tin.Initiative_Status_Id=its.Id
LEFT JOIN dbo.TBLT_Agreement ag
ON tin.Agreement_Id=ag.Id
LEFT JOIN  RowIntiativeStreamBusiness isb
ON tin.id=isb.Initiative_id
LEFT JOIN RowInitiativeEntitasPertamina ep
ON tin.id=ep.Initiative_id
LEFT JOIN RowInitiativePICFungsi pf
ON pf.Initiative_id=tin.Id
LEFT JOIN RowInitiativeNegaraMitra inm
ON inm.Initiative_id=tin.Id
LEFT JOIN RowInitativeLokasiProyek lp
ON tin.id=lp.Initiative_id
LEFT JOIN RowInitativePartners ipt
ON tin.id=ipt.Initiative_id
LEFT JOIN RowInitiativeEntitasPertaminaId iepi
ON tin.id=iepi.Initiative_id
LEFT JOIN RowInitiativeHSHId hshi
ON tin.id=hshi.Initiative_id
LEFT JOIN RowInitiativeHSHName hshn
ON tin.id=hshn.Initiative_id
LEFT JOIN RowIntiativeStreamBusinessId sbi
ON tin.Id=sbi.Initiative_id
LEFT JOIN RowInitiativeNegaraMitraId inmi
ON tin.id=inmi.Initiative_id
LEFT JOIN RowInitiativeKawasanMitraId ikmi
ON tin.id=ikmi.Initiative_id
LEFT JOIN RowInitiativeKawasanMitra ikmn
ON tin.id=ikmn.Initiative_id
WHERE tin.Deleted_Date IS NULL AND (tin.Is_Draft IS NULL OR tin.Is_Draft=0)
GO


