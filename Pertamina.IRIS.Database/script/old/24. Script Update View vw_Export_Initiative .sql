USE [DB_IRIS]
GO


SET QUOTED_IDENTIFIER ON
GO


/*
   View For Export Initiative Transaction
      Update : 2023-09-06 11:40  Change Transpose Data from XmL Function to STR_AGG 
			2023-09-07 17:16  Change Inner join to Left Join  

*/
CREATE  OR Alter    VIEW [dbo].[vw_Export_Initiative]
(
    [Initiative_Id]
,   [Tanggal_Dibuat]
,   [Create_By]
,   [Judul_Inisiasi]
,   [Stream_Business_Id]
,   [Stream Business]
,   [Entitas_Pertamina_Id]
,   [Pertamina]
,   [HSH_id]
,   [HSH Name]
,   [PIC]
,   [Negara_Mitra_Id]
,   [Negara Mitra]
,   [Kawasan_Mitra_Id]
,   [Kawasan Mitra]
,   [Lokasi Proyek]
,   [Nilai_Proyek]
,   [Initiative_Type_Id]
,   [Initiative_type]
,   [Initiative_Stage_Id]
,   [Intiative_Stage]
,   [Inititative_Status_Id]
,   [Initiative_Status]
,   [Perusahaan Mitra]
,   [Interest_Name]
,   [Target_Waktu_Proyek]
,   [Scope]
,   [Value_For_Indonesia]
,   [Progress]
,   [Isu_Kendala]
,   [Support_Pemerintah]
,   [Referal]
,   [Potensi Eskalasi]
,   [Related_Agreement]
,   [Catatan_Tambahan]
,   [Kode_Agreement]
)
WITH
    SCHEMABINDING
AS
    WITH
        RowInitiativeEntitasPertamina
        AS
        (
            SELECT DISTINCT
                iep.Initiative_Id
                , STRING_AGG(CAST(hsh.id AS VARCHAR(36)),',') AS HSH_Id
                , STRING_AGG(hsh.Name,',') AS  [HSH_Name]
                , STRING_AGG(CAST(ep.id  AS VARCHAR(36)),',') AS Entitas_Pertamina_Id
                , STRING_AGG(ep.Company_Name,',') AS  [Company_Name]
            FROM dbo.TBLT_Initiative_Entitas_Pertamina iep
                INNER JOIN dbo.TBLM_Entitas_Pertamina ep
                ON iep.Entitas_Pertamina_Id=ep.Id
                INNER JOIN dbo.TBLM_HSH HSH
                ON ep.HSH_Id=HSH.Id
            GROUP BY iep.Initiative_Id
        ),
        RowInitativeLokasiProyek
        AS
        (
            SELECT DISTINCT Initiative_Id
			, STRING_AGG(Lokasi_Proyek,',') AS Lokasi_Proyek
            FROM dbo.TBLT_Initiative_Lokasi_Proyek
            GROUP BY Initiative_Id
        ),
        RowIntiativeStreamBusiness
        AS
        (
            SELECT DISTINCT
                isb.Initiative_Id
                , STRING_AGG(CAST(isb.Stream_Business_Id AS NVARCHAR(36)),',') AS Stream_Business_Id
                , STRING_AGG(sb.Name,',') AS Stream_Business_Name
            FROM dbo.TBLT_Initiative_Stream_Business  isb
                INNER JOIN dbo.TBLM_Stream_Business sb
                ON isb.Stream_Business_Id=sb.Id
            GROUP BY isb.Initiative_Id
        ),
        RowInitiativePICFungsi
        AS
        (
            SELECT DISTINCT ipf.Initiative_Id
			, STRING_AGG(CONCAT(f.Fungsi_Name,' - ',pf.PIC_Name),',') AS PIC_Fungsi_Name
            FROM dbo.TBLT_Initiative_PIC_Fungsi AS ipf
                INNER JOIN dbo.TBLM_PIC_Fungsi pf
                ON ipf.PIC_Fungsi_Id=pf.Id
                INNER JOIN dbo.TBLM_Fungsi f
                ON pf.Fungsi_Id=f.Id
            GROUP BY ipf.Initiative_Id
        ),
        RowInitiativeNegaraMitra
        AS
        (
            SELECT DISTINCT 
				inm.Initiative_Id
				, STRING_AGG(CAST(inm.Negara_Mitra_Id AS NVARCHAR(36)),',') AS Negara_Mitra_Id
				, STRING_AGG(nm.Nama_Negara,',') AS  Negara_Mitra
				, STRING_AGG(CAST(nm.Kawasan_Mitra_Id AS NVARCHAR(36)),',') AS Kawasan_Mitra_Id
				, STRING_AGG(km.Nama_Kawasan,',') AS  Kawasan
            FROM dbo.TBLT_Initiative_Negara_Mitra inm
                INNER JOIN dbo.TBLM_Negara_Mitra nm
                ON inm.Negara_Mitra_Id=nm.Id
                INNER JOIN dbo.TBLM_Kawasan_Mitra Km
                ON nm.Kawasan_Mitra_Id=km.Id
            GROUP BY inm.Initiative_Id
        ),
        RowInitativePartners
        AS
        (
            SELECT DISTINCT Initiative_id
			, STRING_AGG(ini.Partner_Name,',') AS Perusahaan_Mitra
            FROM dbo.TBLT_Initiative_Partners ini
            GROUP BY ini.Initiative_Id
        )

    SELECT
        tin.Id AS Initiative_Id
	, CONVERT(VARCHAR(10),tin.Create_Date,31) AS Tanggal_Dibuat
	, tin.Create_By
	, tin.Judul_Inisiasi
	, isb.Stream_Business_Id 
	, isb.Stream_Business_Name AS [Stream Business]
	, ep.Entitas_Pertamina_Id  AS Entitas_Pertamina_Id
	, ep.Company_Name AS [Pertamina]
	, ep.HSH_Id   AS HSH_Id
	, ep.HSH_Name AS HSH_Name
	, pf.PIC_Fungsi_Name  AS [PIC]
	, inm.Negara_Mitra_Id 
	, inm.Negara_Mitra AS [Negara Mitra]
	, inm.Kawasan_Mitra_Id  
	, inm.Kawasan AS Kawasan_Mitra
	, lp.Lokasi_Proyek AS [Lokasi Proyek]
	, tin.Nilai_Proyek
	, tin.Initiative_Types_Id
	, it.Status_Name AS Initiative_type
	, tin.Initiative_Stage_Id
	, ist.Name AS Intiative_Stage
	, tin.Initiative_Status_Id
	, its.Status_Name AS Initiative_Status
	, ipt.PErusahaan_Mitra AS [Perusahaan Mitra]
	, itr.Name AS Interest_Name
	, tin.Target_Waktu_Proyek
	, tin.Scope
	, tin.Value_For_Indonesia
	, tin.Progress
	, tin.Isu_Kendala
	, tin.Support_Pemerintah
	, tin.Referal
	, tin.[Potensi Eskalasi]
	, ag.Judul_Perjanjian
	, tin.Catatan_Tambahan
	, ag.Kode_Agreement
    FROM dbo.TBLT_Initiative tin
        LEFT JOIN dbo.TBLM_Initiative_Types it
        ON tin.Initiative_Types_Id=it.Id
        LEFT JOIN dbo.TBLM_Initiative_Stages ist
        ON tin.Initiative_Stage_Id=ist.Id
        LEFT JOIN dbo.TBLM_Interest itr
        ON tin.Interest_Id=itr.Id
        LEFT JOIN dbo.TBLM_Initiative_Status its
        ON tin.Initiative_Status_Id=its.Id
        LEFT JOIN dbo.TBLT_Agreement ag
        ON tin.Agreement_Id=ag.Id
        LEFT JOIN RowIntiativeStreamBusiness isb
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
    WHERE tin.Deleted_Date IS NULL 
	-- AND (tin.Is_Draft IS NULL OR tin.Is_Draft=0)
	
GO


