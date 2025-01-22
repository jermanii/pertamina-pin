USE [DB_IRIS]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE OR ALTER VIEW vw_Export_Opportunity
	(
		 [Opportunity_Id]
		,[Dibuat_Oleh]
		,[Tanggal_Buat]
		,[Nama_Proyek]
		,[Stream_Business_Id]
		,[Stream_Business]
		,[Entitas_Pertamina_Id]
		,[Entitas_Pertamina]
		,[HSH_Id]
		,[HSH_Name]
		,[Pic_Name]
		,[Negara_Mitra_Id]
		,[Lokasi_Negara_Proyek]
		,[Kawasan_Mitra_Id]
		,[Kawasan_Mitra]
		,[Lokasi_Proyek]
		,[Kesiapan_Proyek]
		,[Project_Profile]
		,[Nilai_Proyek]
		,[Deliverables]
		,[Timeline]
		,[Progress]
		,[Target_Mitra]
		,[Existing_Parties]
		,[Catatan_Tambahan]
	) WITH SCHEMABINDING

AS 

WITH 
OpporunityEntitasPertamina AS (
	 SELECT 
		 oep.Opportunity_Id
		 ,oep.Entitas_Pertamina_Id
		 ,ep.Company_Name AS [Company Name]
		 ,ep.HSH_Id
		 ,hsh.Name AS [H SH]
	 FROM dbo.TBLT_Opportunity_Entitas_Pertamina oep
	 INNER JOIN dbo.TBLM_Entitas_Pertamina ep
	 ON oep.Entitas_Pertamina_Id=ep.Id
	 INNER JOIN dbo.TBLM_HSH hsh
	 ON ep.HSH_Id=hsh.Id
),
RowOpporunityEntitasPertamina AS (
      SELECT opo.Id AS Opportunity_Id
		   ,(SELECT DISTINCT ','+oep.[Company Name]
			   FROM OpporunityEntitasPertamina oep
			   WHERE opo.id=oep.Opportunity_Id
			   FOR XML PATH('')
			) AS Entitas_Pertamina
		FROM dbo.TBLT_Opportunity opo
		GROUP BY opo.Id  
 ),
RowOpporunityEntitasPertaminaId AS (
      SELECT opo.Id AS Opportunity_Id
		   ,(SELECT DISTINCT ','+CAST(oep.Entitas_Pertamina_Id AS VARCHAR(36))
			   FROM OpporunityEntitasPertamina oep
			   WHERE opo.id=oep.Opportunity_Id
			   FOR XML PATH('')
		   ) AS Entitas_Pertamina_Id
		FROM dbo.TBLT_Opportunity opo
		GROUP BY opo.Id  
 ),
RowOpporunityHSHId AS (
      SELECT opo.Id AS Opportunity_Id
		   ,(SELECT DISTINCT ','+CAST(oep.HSH_Id AS VARCHAR(36))
			   FROM OpporunityEntitasPertamina oep
			   WHERE opo.id=oep.Opportunity_Id
			   FOR XML PATH('')
			) AS Entitas_HSH_Id
		FROM dbo.TBLT_Opportunity opo
		GROUP BY opo.Id  
 ),
RowOpporunityHSHName AS (
      SELECT opo.Id AS Opportunity_Id
		   ,(SELECT DISTINCT ','+oep.[H SH]
			   FROM OpporunityEntitasPertamina oep
			   WHERE opo.id=oep.Opportunity_Id
			   FOR XML PATH('')
			) AS Entitas_HSH_Name
		FROM dbo.TBLT_Opportunity opo
		GROUP BY opo.Id  
 ),
OpportunityStreamBusiness AS (
	  SELECT  
			osb.Opportunity_Id
			,osb.Stream_Business_Id
			,sb.Name AS Stream_Business_Name
	  FROM dbo.TBLT_Opportunity_Stream_Business osb
	  INNER JOIN dbo.TBLM_Stream_Business sb
	  ON osb.Stream_Business_Id=sb.Id
 ),
RowOportunityStreamBusiness AS (
      SELECT opo.Id AS Opportunity_Id
		   ,(SELECT ','+osb.Stream_Business_Name
			   FROM OpportunityStreamBusiness osb
			   WHERE opo.id=osb.Opportunity_Id
			   FOR XML PATH('')
			) AS Stream_Business_Name
		FROM dbo.TBLT_Opportunity opo
		GROUP BY opo.Id  
 ),
RowOportunityStreamBusinessId AS (
	SELECT opo.Id AS Opportunity_Id
		   ,(SELECT ','+CAST(osb.Stream_Business_Id AS VARCHAR(36))
			   FROM OpportunityStreamBusiness osb
			   WHERE opo.id=osb.Opportunity_Id
			   FOR XML PATH('')
			) AS Stream_Business_Id
	FROM dbo.TBLT_Opportunity opo
	GROUP BY opo.Id  
 ),
OpportunityKesiapanProyek AS (
	SELECT	okp.Opportunity_Id
			,okp.Kesiapan_Proyek_Id
			,kp.Name AS [Kesiapan_Proyek]
			,kp.Order_Seq
	FROM dbo.TBLT_Opportunity_Kesiapan_Proyek okp
	INNER JOIN dbo.TBLM_Kesiapan_Proyek kp
	ON okp.Kesiapan_Proyek_Id=kp.Id
 ),
RowOpportunityKesiapanProyek AS(
	SELECT opo.Id AS Opportunity_Id
		,(SELECT ','+okp.Kesiapan_Proyek
		   FROM OpportunityKesiapanProyek okp
		   WHERE opo.id=okp.Opportunity_Id
		   ORDER BY okp.Order_Seq
		   FOR XML PATH('')
		 ) AS Kesiapan_proyek
	FROM dbo.TBLT_Opportunity opo
	GROUP BY opo.Id  
 ),
RowOpportunityLokasiProyek AS (
	SELECT 
		opo.Id AS Opportunity_Id
		,(SELECT ','+okp.Lokasi_Proyek
			FROM dbo.TBLT_Opportunity_Lokasi_Proyek okp
				   WHERE opo.id=okp.Opportunity_Id
				   FOR XML PATH('')
		 ) AS Lokasi_Proyek
	FROM dbo.TBLT_Opportunity opo
	GROUP BY opo.Id     
 ),
 RowOpportunityPartners AS (
        SELECT opo.Id AS Opportunity_Id
		   ,(SELECT ','+op.Partner_Name
			   FROM dbo.TBLT_Opportunity_Partners op
			   WHERE opo.id=op.Opportunity_Id
			   FOR XML PATH('')
		   )AS Partners
		FROM dbo.TBLT_Opportunity opo
		GROUP BY opo.Id  
 ),
 OpportunityNegaraMitra AS(
	  SELECT onm.Opportunity_Id 
		  ,nm.Id AS Negara_Mitra_Id
		  ,nm.Nama_Negara As [Negara Mitra]
		  ,nm.Kawasan_Mitra_Id
		  ,km.Nama_Kawasan as Kawasan
		  ,km.Continent_Id
		  ,ct.Continent_Name AS  Continent
	  FROM dbo.TBLT_Opportunity_Negara_Mitra onm
	  INNER JOIN dbo.TBLM_Negara_Mitra nm
	  ON onm.Negara_Mitra_Id=nm.Id
	  INNER JOIN dbo.TBLM_Kawasan_Mitra km
	  ON nm.Kawasan_Mitra_Id=km.Id
	  INNER JOIN dbo.TBLM_Continent ct
	  ON km.Continent_Id=ct.Id
 ),
 RowOpportunityNegaraMitra AS (
        SELECT opo.Id AS Opportunity_Id
		   ,(SELECT ','+onm.[Negara Mitra]
			   FROM OpportunityNegaraMitra onm
			   WHERE opo.id=onm.Opportunity_Id
			   FOR XML PATH('')
		    ) AS Negara_Mitra
		FROM dbo.TBLT_Opportunity opo
		GROUP BY opo.Id 
 ),
 RowOpportunityNegaraMitraId AS (
	SELECT opo.Id AS Opportunity_Id
		,(SELECT ','+CAST(onm.Negara_Mitra_Id AS VARCHAR(36))
		   FROM OpportunityNegaraMitra onm
		   WHERE opo.id=onm.Opportunity_Id
		   FOR XML PATH('')
		   ) AS Negara_Mitra_Id
	FROM dbo.TBLT_Opportunity opo
	GROUP BY opo.Id 
 ),
 RowOpportunityKawasanMitraId AS (
	SELECT opo.Id AS Opportunity_Id
		,(SELECT DISTINCT  ','+CAST(onm.Kawasan_Mitra_Id AS VARCHAR(36))
		   FROM OpportunityNegaraMitra onm
		   WHERE opo.id=onm.Opportunity_Id
		   FOR XML PATH('')
		   ) AS Kawasan_Mitra_Id
	FROM dbo.TBLT_Opportunity opo
	GROUP BY opo.Id 
 ),
 RowOpportunityKawasanMitraName AS (
	SELECT opo.Id AS Opportunity_Id
		,(SELECT DISTINCT ',' + onm.Kawasan
		   FROM OpportunityNegaraMitra onm
		   WHERE opo.id=onm.Opportunity_Id
		   FOR XML PATH('')
		   ) AS Kawasan_Mitra
	FROM dbo.TBLT_Opportunity opo
	GROUP BY opo.Id 
 ),
 OpportunityPicFungsi AS(
	   SELECT  opf.Opportunity_Id
	   ,f.Fungsi_Name
	   ,pf.PIC_Name
	   FROM dbo.TBLT_Opportunity_PIC_Fungsi opf
	   INNER JOIN dbo.TBLM_PIC_Fungsi pf
	   ON opf.PIC_Fungsi_Id=pf.Id
	   INNER JOIN dbo.TBLM_Fungsi f
	   ON pf.Fungsi_Id=f.Id
),
RowOpportunityPicFungsi AS (
        SELECT opo.Id AS Opportunity_Id
		   ,(SELECT ','+CONCAT(opf.Fungsi_Name,' - ', opf.PIC_Name)
			   FROM OpportunityPicFungsi opf
			   WHERE opo.id=opf.Opportunity_Id
			   FOR XML PATH('')
			 ) AS Pic_Name
		FROM dbo.TBLT_Opportunity opo
		GROUP BY opo.Id 
),
OpportunityTargetMitra AS (
	  SELECT otm.Opportunity_Id
			,tm.Name AS Target_Mitra
	  FROM dbo.TBLT_Opportunity_Target_Mitra otm
	  INNER JOIN dbo.TBLM_Target_Mitra tm
	  ON otm.Target_Mitra_Id=tm.id
),
RowOpportunityTargetMitra AS (
	SELECT opo.Id AS Opportunity_Id
		   ,(SELECT ','+otm.Target_Mitra
			   FROM OpportunityTargetMitra otm
			   WHERE opo.id=otm.Opportunity_Id
			   FOR XML PATH('')
			 ) AS Target_Mitra
	FROM dbo.TBLT_Opportunity opo
	GROUP BY opo.Id 
)

SELECT 
		op.id AS opportunity_Id
		,op.Create_By AS Dibuat_Oleh
		,CONVERT(VARCHAR(10),op.Create_Date,31) AS Tanggal_Buat
		,op.Nama_Proyek
		,IIF(sbi.Stream_Business_Id IS NULL,NULL,SUBSTRING(sbi.Stream_Business_Id,2,LEN(sbi.Stream_Business_Id))) AS Stream_Business_Id
		,IIF(osb.Stream_Business_name IS NULL,NULL,SUBSTRING(osb.Stream_Business_name,2,LEN(osb.Stream_Business_name))) AS Stream_Business
		,IIF(epi.Entitas_Pertamina_Id IS NULL,NULL,SUBSTRING(epi.Entitas_Pertamina_Id,2,LEN(epi.Entitas_Pertamina_Id))) AS Entitas_Pertamina_Id
		,IIF(ep.Entitas_Pertamina IS NULL,NULL,SUBSTRING(ep.Entitas_Pertamina,2,LEN(ep.Entitas_Pertamina))) AS Entitas_Pertamina
		,IIF(hshi.Entitas_HSH_Id IS NULL,NULL,SUBSTRING(hshi.Entitas_HSH_Id,2,LEN(hshi.Entitas_HSH_Id))) AS HSH_Id
		,IIF(hshn.Entitas_HSH_Name IS NULL,NULL,SUBSTRING(hshn.Entitas_HSH_Name,2,LEN(hshn.Entitas_HSH_Name))) AS HSH_Name
		,IIF(pic.Pic_Name IS NULL,NULL,SUBSTRING(pic.Pic_Name,2,LEN(pic.Pic_Name))) AS Pic_Name
		,IIF(nmi.Negara_Mitra_Id IS NULL,NULL,SUBSTRING(nmi.Negara_Mitra_Id,2,LEN(nmi.Negara_Mitra_Id))) AS Negara_Mitra_Id
		,IIF(nm.Negara_Mitra IS NULL,NULL,SUBSTRING(nm.Negara_Mitra,2,LEN(nm.Negara_Mitra))) AS Negara_Mitra
		,IIF(kmi.Kawasan_Mitra_Id IS NULL,NULL,SUBSTRING(kmi.Kawasan_Mitra_Id,2,LEN(kmi.Kawasan_Mitra_Id))) AS Kawasan_Mitra_Id
		,IIF(kmn.Kawasan_Mitra IS NULL ,NULL,SUBSTRING(kmn.Kawasan_Mitra,2,LEN(kmn.Kawasan_Mitra))) AS Kawasan_Mitra
		,IIF(lp.Lokasi_Proyek IS NULL,NULL,SUBSTRING(lp.Lokasi_Proyek,2,LEN(lp.Lokasi_Proyek))) AS Lokasi_Proyek
		,IIF(kp.Kesiapan_proyek IS NULL,NULL,SUBSTRING(kp.Kesiapan_proyek,2,LEN(kp.Kesiapan_proyek))) AS Kesiapan_Proyek
		,op.Project_Profile
		,op.Nilai_Proyek
		,op.Deliverables
		,op.Timeline
		,op.Progress
		,IIF(tm.Target_Mitra IS NULL,NULL,SUBSTRING(tm.Target_Mitra,2,LEN(tm.Target_Mitra))) AS Target_Mitra
		,IIF(prt.Partners IS NULL,NULL,SUBSTRING(prt.Partners,2,LEN(prt.Partners))) AS Existing_Parties
		,op.Catatan_Tambahan
FROM
dbo.TBLT_Opportunity op
LEFT JOIN RowOportunityStreamBusiness osb
ON op.id=osb.Opportunity_Id
LEFT JOIN RowOpporunityEntitasPertamina ep
ON op.id=ep.Opportunity_Id
LEFT JOIN RowOpportunityPicFungsi pic
ON op.Id=pic.Opportunity_Id
LEFT JOIN RowOpportunityNegaraMitra nm
ON op.id=nm.Opportunity_Id
LEFT JOIN RowOpportunityNegaraMitraId nmi
ON op.id=nmi.Opportunity_Id
LEFT JOIN RowOpportunityLokasiProyek lp
ON op.Id=lp.Opportunity_Id
LEFT JOIN RowOpportunityKesiapanProyek kp
ON op.Id=kp.Opportunity_Id
LEFT JOIN RowOpportunityTargetMitra tm
ON op.Id=tm.Opportunity_Id
LEFT JOIN RowOpportunityPartners prt
ON op.id=prt.Opportunity_Id
LEFT JOIN RowOportunityStreamBusinessId sbi
ON op.id=sbi.Opportunity_Id
LEFT JOIN RowOpporunityEntitasPertaminaId epi
ON op.Id=epi.Opportunity_Id
LEFT JOIN RowOpporunityHSHId hshi
ON op.Id=hshi.Opportunity_Id
LEFT JOIN RowOpporunityHSHName hshn
ON op.Id=hshn.Opportunity_Id
LEFT JOIN RowOpportunityKawasanMitraId kmi
ON op.id=kmi.Opportunity_Id
LEFT JOIN RowOpportunityKawasanMitraName kmn
ON op.id=kmn.Opportunity_Id
WHERE op.Deleted_Date IS NULL AND op.Is_Draft IS NULL OR op.Is_Draft<>1
GO


