USE [DB_IRIS]
GO

/****** Object:  View [dbo].[vw_Export_Agreement]    Script Date: 7/20/2023 11:46:29 AM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO





ALTER  VIEW  [dbo].[vw_Export_Agreement] (
 [Agreement_id]
,[Partners]
,[HSH_Id]
,[HSH_Name]
,[Entitas_Pertamina_ID]
,[Entitas Pertamina]
,[Fungsi Team Sebagai]
,[Jenis_Perjanjian_Id]
,[Jenis Perjanjian]
,[Judul_Perjanjian]
,[Kawsan_Mitra_Id]
,[Kawasan_Mitra_Name]
,[Negara_Mitra_Id]
,[Negara_Mitra]
,[Lokasi Proyek]
,[Scope]
,[Stream_Business_Id]
,[Stream Business]
,[Tanggal_TTD]
,[Tanggal_Berakhir]
,[Status_Berlaku_Id]
,[Status Berlaku]
,[Discussion_Status_Id]
,[Discussion Status]
,[Progress]
,[Klasifikasi Kendala]
,[Faktor kendala]
,[Deskripsi_Kendala]
,[Support_Pemerintah]
,[Keterlibatan Kementrian]
,[Nilai_Proyek]
,[Potensi_Eskalasi]
,[Related Perjanjian]
,[Catatan_Tambahan]
,[Tanggal_Dibuat]
)
WITH SCHEMABINDING
AS 

WITH AgreementEntitasPertamina AS (
   SELECT taep.Agreement_Id
            ,taep.Entitas_Pertamina_Id
		   ,tep.Company_Name AS Entitas_Pertamina_Name 
		   ,hsh.id AS HSH_Id
		   ,hsh.Name AS HSH_Name 
		   ,CONCAT(hsh.Name,' - ',tep.Company_Name) AS HSH_Entitas_Pertamina
   FROM dbo.TBLT_Agreement_Entitas_Pertamina  Taep
   INNER JOIN dbo.TBLM_Entitas_Pertamina tep
   ON taep.Entitas_Pertamina_Id=tep.Id AND tep.Deleted_Date IS NULL
   INNER JOIN dbo.TBLM_HSH hsh
   ON tep.HSH_Id=hsh.Id AND hsh.Deleted_Date IS NULL
   WHERE taep.Deleted_Date IS NULL
   ),
   RowAgreementEntitasPertamina AS (
        SELECT Ag.Id AS Agreement_id
		   ,(SELECT ','+aep.Entitas_Pertamina_Name 
		   FROM AgreementEntitasPertamina aep
		   WHERE Ag.id=aep.Agreement_Id
		   FOR XML PATH('')) AS Entitas_Pertamina_Name
		FROM dbo.TBLT_Agreement ag
		GROUP BY ag.Id
   ),
    RowAgreementEntitasPertaminaID AS (
        SELECT Ag.Id AS Agreement_id
		   ,(SELECT ','+CAST(aep.Entitas_Pertamina_Id AS VARCHAR(36))
		   FROM AgreementEntitasPertamina aep
		   WHERE Ag.id=aep.Agreement_Id
		   FOR XML PATH('')) AS Entitas_Pertamina_Id
		FROM dbo.TBLT_Agreement ag
		GROUP BY ag.Id
   ),
   RowAgreementHSHId AS (
        SELECT DISTINCT Ag.Id AS Agreement_id
		   ,(SELECT DISTINCT ','+CAST(aep.HSH_Id AS VARCHAR(36))
		   FROM AgreementEntitasPertamina aep
		   WHERE Ag.id=aep.Agreement_Id
		   FOR XML PATH('')) AS HSH_Id
		FROM dbo.TBLT_Agreement ag
		GROUP BY ag.Id
   ),
   RowAgreementHSHName AS (
        SELECT DISTINCT Ag.Id AS Agreement_id
		   ,(SELECT DISTINCT ','+aep.HSH_Name
		   FROM AgreementEntitasPertamina aep
		   WHERE Ag.id=aep.Agreement_Id
		   FOR XML PATH('')) AS HSH_Name
		FROM dbo.TBLT_Agreement ag
		GROUP BY ag.Id
   ),
   AgreementStreamBusiness AS (
      SELECT Asb.Agreement_Id
		,asb.Stream_Business_Id
		,sb.Name AS Stream_Business_Name
	  FROM dbo.TBLT_Agreement_Stream_Business asb
	  INNER JOIN dbo.TBLM_Stream_Business sb
	  ON Asb.Stream_Business_Id=sb.id
   ),
   RowAgreementStreamBusiness AS (
        SELECT Ag.Id AS Agreement_id
		   ,(SELECT ','+asb.Stream_Business_Name
		   FROM AgreementStreamBusiness asb
		   WHERE Ag.id=asb.Agreement_Id
		   FOR XML PATH('')) AS Stream_Business_Name
		FROM dbo.TBLT_Agreement ag
		GROUP BY ag.Id     
   ),
   RowAgreementStreamBusinessId AS (
        SELECT Ag.Id AS Agreement_id
		   ,(SELECT DISTINCT ','+CAST(asb.Stream_Business_Id AS VARCHAR(36))
		   FROM AgreementStreamBusiness asb
		   WHERE Ag.id=asb.Agreement_Id
		   FOR XML PATH('')) AS Stream_Business_Id
		FROM dbo.TBLT_Agreement ag
		GROUP BY ag.Id     
   ),
   AgreementNegaraMitra AS (
      SELECT anm.Agreement_Id
	   ,nm.Kawasan_Mitra_Id
	   ,km.Nama_Kawasan AS Kawasan_Mitra_Name
	   ,anm.Negara_Mitra_Id
	   ,nm.Nama_Negara AS Negara_Mitra
	  FROM dbo.TBLT_Agreement_Negara_Mitra anm
	  INNER JOIN dbo.TBLM_Negara_Mitra nm
	  --INNER JOIN dbo.vw_Dim_Negara_Mitra nm
	  ON anm.Negara_Mitra_Id=nm.Id
	  INNER JOIN dbo.TBLM_Kawasan_Mitra km
	  ON nm.Kawasan_Mitra_Id=km.Id
   ),
   AgreementPICFungsi AS (
      SELECT DISTINCT apf.Agreement_Id,
	   CONCAT(f.Fungsi_Name,' - ',pf.PIC_Name) AS Fungsi_Name
	  FROM dbo.TBLT_Agreement_PIC_Fungsi apf
	  INNER JOIN dbo.TBLM_PIC_Fungsi pf
	  ON apf.PIC_Fungsi_Id=pf.Id
	  INNER JOIN dbo.TBLM_Fungsi f
	  ON pf.Fungsi_Id=f.Id
   ),
   RowAgreementPICFungsi AS (
        SELECT Ag.Id AS Agreement_id
		   ,(SELECT ','+apf.Fungsi_Name
		   FROM AgreementPICFungsi apf
		   WHERE Ag.id=apf.Agreement_Id
		   FOR XML PATH('')) AS Fungsi
		FROM dbo.TBLT_Agreement ag
		GROUP BY ag.Id  
   ),
   RowAgreementLokasiProyek AS (
        SELECT Ag.Id AS Agreement_id
		   ,(SELECT ','+alp.Lokasi_Proyek
		   FROM dbo.TBLT_Agreement_Lokasi_Proyek alp
		   WHERE Ag.id=alp.Agreement_Id
		   FOR XML PATH('')) AS Lokasi_Proyek
		FROM dbo.TBLT_Agreement ag
		GROUP BY ag.Id  
   ),
    RowAgreementPartners AS (
        SELECT Ag.Id AS Agreement_id
		   ,(SELECT ','+ap.Partner_Name
		   FROM dbo.TBLT_Agreement_Partners ap
		   WHERE Ag.id=ap.Agreement_Id
		   FOR XML PATH('')) AS Partners
		FROM dbo.TBLT_Agreement ag
		GROUP BY ag.Id  
   ),
   AgreementKementrianLembaga AS (
     SELECT akl.Agreement_Id
	 ,kl.Lembaga_Name
	 FROM dbo.TBLT_Agreement_Kementrian_Lembaga akl
	 INNER JOIN dbo.TBLM_Kementrian_Lembaga kl
	 ON akl.Kementrian_Lembaga_Id=kl.id
   ),
   RowAgreementKementrianLembaga AS (
        SELECT Ag.Id AS Agreement_id
		   ,(SELECT ','+akl.Lembaga_Name
		   FROM AgreementKementrianLembaga akl
		   WHERE Ag.id=akl.Agreement_Id
		   FOR XML PATH('')) AS Kementerian_Lembaga
		FROM dbo.TBLT_Agreement ag
		GROUP BY ag.Id    
   )
   SELECT  
     Ag.id AS Agreement_id
	-- ,ap.Partners
   	,IIF(ap.Partners IS NULL,NULL,SUBSTRING(ap.Partners,2,LEN(ap.Partners))) AS Parties
	,IIF(hshi.HSH_Id IS NULL ,NULL,SUBSTRING(hshi.HSH_Id,2,LEN(hshi.HSH_Id))) AS HSH_Id
	,IIF(hshA.HSH_Name IS NULL ,NULL,SUBSTRING(hsha.HSH_Name,2,LEN(hsha.HSH_Name))) AS HSH_Name
	,IIF(aepi.Entitas_Pertamina_Id IS NULL,NULL,SUBSTRING(aepi.Entitas_Pertamina_Id,2,LEN(aepi.Entitas_Pertamina_Id))) AS Pertamina_Id
	,IIF(ep.Entitas_Pertamina_Name IS NULL,NULL,SUBSTRING(ep.Entitas_Pertamina_Name,2,LEN(ep.Entitas_Pertamina_Name))) AS [Pertamina]
	,IIF(apf.Fungsi IS NULL ,NULL,SUBSTRING(apf.Fungsi,2,LEN(apf.Fungsi))) AS [Fungsi Team Sebagai]
	,ag.Jenis_Perjanjian_Id
	,jp.Name AS [Jenis Perjanjian]
    ,ag.Judul_Perjanjian
	,anm.Kawasan_Mitra_Id
	,anm.Kawasan_Mitra_Name
	,anm.Negara_Mitra_Id
	,anm.Negara_Mitra
	,IIF(lp.Lokasi_Proyek IS NULL,NULL,SUBSTRING(lp.Lokasi_Proyek,2,LEN(lp.Lokasi_Proyek))) AS [Lokasi Proyek]
	,ag.Scope
	,IIF(asbi.Stream_Business_Id IS NULL ,NULL,SUBSTRING(asbi.Stream_Business_Id,2,LEN(asbi.Stream_Business_Id))) AS Stream_Business_Id
	,IIF(sb.Stream_Business_Name IS NULL,NULL,SUBSTRING(sb.Stream_Business_Name,2,LEN(sb.Stream_Business_Name))) AS [Stream Business]
	,ag.Tanggal_TTD
	,ag.Tanggal_Berakhir
	,ag.Status_Berlaku_Id
	,stb.Status_Name AS [Status Berlaku]
	,ag.Discussion_Status_Id
	,ds.Name AS [Discussion Status]
	,ag.Progress
	,kk.Name AS [Klasifikasi Kendala]
	,fk.Name AS [Faktor kendala]
	,ag.Deskripsi_Kendala
	,ag.Support_Pemerintah
	,IIF(kl.Kementerian_Lembaga IS NULL,NULL,SUBSTRING(kl.Kementerian_Lembaga,2,LEN(kl.Kementerian_Lembaga))) AS [Keterlibatan Kementrian]
	,ag.Nilai_Proyek
	,ag.Potensi_Eskalasi
	,ag2.Judul_Perjanjian AS Related_Perjanjian
	,ag.Catatan_Tambahan
	,CONVERT(VARCHAR(10),ag.Create_Date,31) AS Tanggal_Dibuat
   FROM dbo.TBLT_Agreement Ag
   INNER JOIN dbo.TBLM_Jenis_Perjanjian jp
		ON ag.Jenis_Perjanjian_Id=jp.Id
	INNER JOIN dbo.TBLM_Discussion_Status ds
	ON ag.Discussion_Status_Id=ds.id
	INNER JOIN AgreementNegaraMitra anm
	ON ag.id=anm.Agreement_Id
	INNER JOIN dbo.TBLM_Faktor_Kendala fk
	ON ag.Faktor_Kendala_Id=fk.Id
	INNER JOIN dbo.TBLM_Klasifikasi_Kendala kk
	ON ag.Klasifikasi_Kendala_Id=kk.Id
	INNER JOIN dbo.TBLM_Status_Berlaku stb
	ON ag.Status_Berlaku_Id=stb.Id
	LEFT JOIN RowAgreementKementrianLembaga kl
	ON ag.id=kl.Agreement_id
	LEFT JOIN RowAgreementEntitasPertamina ep
	ON ag.id=ep.Agreement_id
	LEFT JOIN RowAgreementEntitasPertaminaID aepi
	ON ag.id=aepi.Agreement_id
	LEFT JOIN RowAgreementStreamBusiness sb
	ON ag.id=sb.Agreement_id
	LEFT JOIN RowAgreementLokasiProyek lp
	ON ag.id=lp.Agreement_id
	LEFT JOIN RowAgreementPartners ap
	ON ag.id=ap.Agreement_id
	LEFT JOIN RowAgreementPICFungsi apf
	ON ag.id=apf.Agreement_id
	LEFT JOIN RowAgreementHSHId hshi
	ON ag.Id=hshi.Agreement_id
	LEFT JOIN RowAgreementHSHName hsha
	ON ag.Id=hsha.Agreement_id
	LEFT JOIN RowAgreementStreamBusinessId asbi
	ON ag.id=asbi.Agreement_id
	LEFT JOIN dbo.TBLT_Agreement ag2
	ON ag.id=ag2.Related_Agreement_Id
	WHERE ag.Deleted_Date IS NULL AND (ag.Is_Draft<>1 OR ag.Is_Draft IS NULL)
GO


