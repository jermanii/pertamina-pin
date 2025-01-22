USE [DB_IRIS]
GO

/****** Object:  View [dbo].[vw_Export_Agreement]    Script Date: 9/7/2023 5:08:35 PM ******/
SET ANSI_NULLS ON
GO


/*
   View For Export Initiative Transaction
   Update : 2023-09-06 11:40  Change Transpose Data from XmL Function to STR_AGG 
			2023-09-07 17:16  Change Inner join to Left Join  

*/

SET QUOTED_IDENTIFIER ON
GO


CREATE  OR ALTER   VIEW  [dbo].[vw_Export_Agreement]
(
    [Agreement_id]
,   [Partners]
,   [HSH_Id]
,   [HSH_Name]
,   [Entitas_Pertamina_ID]
,   [Entitas Pertamina]
,   [Fungsi Team Sebagai]
,   [Jenis_Perjanjian_Id]
,   [Jenis Perjanjian]
,   [Judul_Perjanjian]
,   [Kawsan_Mitra_Id]
,   [Kawasan_Mitra_Name]
,   [Negara_Mitra_Id]
,   [Negara_Mitra]
,   [Lokasi Proyek]
,   [Scope]
,   [Stream_Business_Id]
,   [Stream Business]
,   [Tanggal_TTD]
,   [Tanggal_Berakhir]
,   [Status_Berlaku_Id]
,   [Status Berlaku]
,   [Discussion_Status_Id]
,   [Discussion Status]
,   [Progress]
,   [Klasifikasi Kendala]
,   [Faktor kendala]
,   [Deskripsi_Kendala]
,   [Support_Pemerintah]
,   [Keterlibatan Kementrian]
,   [Nilai_Proyek]
,   [Potensi_Eskalasi]
,   [Related Perjanjian]
,   [Catatan_Tambahan]
,   [Tanggal_Dibuat]
,   [Kode_Agreement]
,   [Kode_Related_Perjanjian]
)
WITH
    SCHEMABINDING
AS

    WITH
        RowAgreementEntitasPertamina
        AS
        (
            SELECT DISTINCT taep.Agreement_Id
		  , STRING_AGG(CAST(hsh.Id AS VARCHAR(36)),',') AS HSH_Id
		  , STRING_AGG(hsh.Name,',') AS HSH_Name   
		  , STRING_AGG(CAST(taep.Id AS VARCHAR(36)),',') AS Pertamina_Id
		  , STRING_AGG(tep.Company_Name,',')  AS Entitas_Pertamina_Name
            FROM dbo.TBLT_Agreement_Entitas_Pertamina  Taep
                INNER JOIN dbo.TBLM_Entitas_Pertamina tep
                ON taep.Entitas_Pertamina_Id=tep.Id AND tep.Deleted_Date IS NULL
                INNER JOIN dbo.TBLM_HSH hsh
                ON tep.HSH_Id=hsh.Id AND hsh.Deleted_Date IS NULL
            WHERE taep.Deleted_Date IS NULL
            GROUP BY Taep.Agreement_Id
        ),
        RowAgreementStreamBusiness
        AS
        (
            SELECT DISTINCT Asb.Agreement_Id
		    , STRING_AGG(CAST( asb.Stream_Business_Id AS VARCHAR(36)),',') AS Stream_Business_Id
		    , STRING_AGG(sb.Name ,',') AS Stream_Business_Name
            FROM dbo.TBLT_Agreement_Stream_Business asb
                INNER JOIN dbo.TBLM_Stream_Business sb
                ON Asb.Stream_Business_Id=sb.id
            GROUP BY asb.Agreement_Id
        ),

        AgreementNegaraMitra
        AS
        (
            SELECT DISTINCT anm.Agreement_Id
	        , STRING_AGG(CAST(nm.Kawasan_Mitra_Id AS VARCHAR(36)),',') AS Kawasan_Mitra_Id
	        , STRING_AGG(km.Nama_Kawasan,',') AS Kawasan_Mitra_Name
	        , STRING_AGG(CAST(anm.Negara_Mitra_Id AS VARCHAR(36)),',') AS Negara_Mitra_Id
	        , STRING_AGG(nm.Nama_Negara,',') AS Negara_Mitra
            FROM dbo.TBLT_Agreement_Negara_Mitra anm
                INNER JOIN dbo.TBLM_Negara_Mitra nm
                --INNER JOIN dbo.vw_Dim_Negara_Mitra nm
                ON anm.Negara_Mitra_Id=nm.Id
                INNER JOIN dbo.TBLM_Kawasan_Mitra km
                ON nm.Kawasan_Mitra_Id=km.Id
            GROUP BY anm.Agreement_Id
        ),
        RowAgreementPICFungsi
        AS
        (
            SELECT DISTINCT apf.Agreement_Id,
                STRING_AGG(CONCAT(f.Fungsi_Name,' - ',pf.PIC_Name),',') AS Fungsi_Name
            FROM dbo.TBLT_Agreement_PIC_Fungsi apf
                INNER JOIN dbo.TBLM_PIC_Fungsi pf
                ON apf.PIC_Fungsi_Id=pf.Id
                INNER JOIN dbo.TBLM_Fungsi f
                ON pf.Fungsi_Id=f.Id
            GROUP BY apf.Agreement_Id
        ),
        RowAgreementLokasiProyek
        AS
        (
            SELECT Agreement_id
				, STRING_AGG(ag.Lokasi_Proyek,',') AS Lokasi_Proyek
            FROM dbo.TBLT_Agreement_Lokasi_Proyek ag
            GROUP BY ag.Agreement_Id
        ),
        RowAgreementPartners
        AS
        (
            SELECT Agreement_id
			, STRING_AGG(ag.Partner_Name,',') AS Partners
            FROM dbo.TBLT_Agreement_Partners ag
            GROUP BY ag.Agreement_Id
        ),
        RowAgreementKementrianLembaga
        AS
        (
            SELECT
                akl.Agreement_Id
				, STRING_AGG(kl.Lembaga_Name,',') AS Kementerian_Lembaga
            FROM dbo.TBLT_Agreement_Kementrian_Lembaga akl
                INNER JOIN dbo.TBLM_Kementrian_Lembaga kl
                ON akl.Kementrian_Lembaga_Id=kl.id
            GROUP BY akl.Agreement_Id
        )
    SELECT
        Ag.id AS Agreement_id
   	, ap.Partners  AS Parties
	, ep.HSH_Id AS HSH_Id
	, ep.HSH_Name AS HSH_Name
	, ep.Pertamina_Id AS Pertamina_Id
	, ep.Entitas_Pertamina_Name 
	, apf.Fungsi_Name  AS [Fungsi Team Sebagai]
	, ag.Jenis_Perjanjian_Id
	, jp.Name AS [Jenis Perjanjian]
    , ag.Judul_Perjanjian
	, anm.Kawasan_Mitra_Id
	, anm.Kawasan_Mitra_Name
	, anm.Negara_Mitra_Id
	, anm.Negara_Mitra
	, lp.Lokasi_Proyek  AS [Lokasi Proyek]
	, ag.Scope
	, sb.Stream_Business_Id 
	, sb.Stream_Business_Name  AS [Stream Business]
	, ag.Tanggal_TTD
	, ag.Tanggal_Berakhir
	, ag.Status_Berlaku_Id
	, stb.Status_Name AS [Status Berlaku]
	, ag.Discussion_Status_Id
	, ds.Name AS [Discussion Status]
	, ag.Progress
	, kk.Name AS [Klasifikasi Kendala]
	, fk.Name AS [Faktor kendala]
	, ag.Deskripsi_Kendala
	, ag.Support_Pemerintah
	, kl.Kementerian_Lembaga AS [Keterlibatan Kementrian]
	, ag.Nilai_Proyek
	, ag.Potensi_Eskalasi
	, ag2.Judul_Perjanjian AS Related_Perjanjian
	, ag.Catatan_Tambahan
	, CONVERT(VARCHAR(10),ag.Create_Date,31) AS Tanggal_Dibuat
	, ag.Kode_Agreement
	, ag2.Kode_Agreement
    FROM dbo.TBLT_Agreement Ag
        LEFT JOIN dbo.TBLM_Jenis_Perjanjian jp
        ON ag.Jenis_Perjanjian_Id=jp.Id
        LEFT JOIN dbo.TBLM_Discussion_Status ds
        ON ag.Discussion_Status_Id=ds.id
        LEFT JOIN AgreementNegaraMitra anm
        ON ag.id=anm.Agreement_Id
        LEFT JOIN dbo.TBLM_Faktor_Kendala fk
        ON ag.Faktor_Kendala_Id=fk.Id
        LEFT JOIN dbo.TBLM_Klasifikasi_Kendala kk
        ON ag.Klasifikasi_Kendala_Id=kk.Id
        LEFT JOIN dbo.TBLM_Status_Berlaku stb
        ON ag.Status_Berlaku_Id=stb.Id
        LEFT JOIN RowAgreementKementrianLembaga kl
        ON ag.id=kl.Agreement_id
        LEFT JOIN RowAgreementEntitasPertamina ep
        ON ag.id=ep.Agreement_id
        LEFT JOIN RowAgreementStreamBusiness sb
        ON ag.id=sb.Agreement_id
        LEFT JOIN RowAgreementLokasiProyek lp
        ON ag.id=lp.Agreement_id
        LEFT JOIN RowAgreementPartners ap
        ON ag.id=ap.Agreement_id
        LEFT JOIN RowAgreementPICFungsi apf
        ON ag.id=apf.Agreement_id
        LEFT JOIN dbo.TBLT_Agreement ag2
        ON ag.Related_Agreement_Id=ag2.Id
    WHERE ag.Deleted_Date IS NULL 
	-- AND (ag.Is_Draft<>1  OR ag.Is_Draft IS NULL)
	
GO


