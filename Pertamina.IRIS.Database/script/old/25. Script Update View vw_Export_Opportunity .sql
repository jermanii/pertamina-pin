USE [DB_IRIS]
GO

/****** Object:  View [dbo].[vw_Export_Opportunity]    Script Date: 9/7/2023 5:11:37 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


/*
   View For Export Opportunity Transaction
      Update : 2023-09-06 11:40  Change Transpose Data from XmL Function to STR_AGG 
			2023-09-07 17:16  Change Inner join to Left Join  

*/

CREATE OR ALTER     VIEW [dbo].[vw_Export_Opportunity]
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
)
WITH
    SCHEMABINDING

AS

    WITH
        RowOpporunityEntitasPertamina
        AS
        (
            SELECT DISTINCT
                oep.Opportunity_Id
				   , STRING_AGG(CAST(oep.Entitas_Pertamina_Id AS NVARCHAR(36)),',') AS Entitas_Pertamina_Id
				   , STRING_AGG(ep.Company_Name,',') AS Company_Name
				   , STRING_AGG(CAST(ep.HSH_Id AS NVARCHAR(36)),',') AS HSH_Id
				   , STRING_AGG(hsh.Name,',')  AS HSH_Name
            FROM dbo.TBLT_Opportunity_Entitas_Pertamina oep
                INNER JOIN dbo.TBLM_Entitas_Pertamina ep
                ON oep.Entitas_Pertamina_Id=ep.Id
                INNER JOIN dbo.TBLM_HSH hsh
                ON ep.HSH_Id=hsh.Id
            GROUP BY oep.Opportunity_Id
        ),
        RowOportunityStreamBusiness
        AS
        (
            SELECT
                osb.Opportunity_Id
			, STRING_AGG(CAST(osb.Stream_Business_Id AS NVARCHAR(36)),',') AS Stream_Business_Id
			, STRING_AGG(sb.Name,',') AS Stream_Business_Name
            FROM dbo.TBLT_Opportunity_Stream_Business osb
                INNER JOIN dbo.TBLM_Stream_Business sb
                ON osb.Stream_Business_Id=sb.Id
            GROUP BY osb.Opportunity_Id
        ),
        OpportunityKesiapanProyek
        AS
        (
            SELECT okp.Opportunity_Id
			, okp.Kesiapan_Proyek_Id
			, kp.Name AS [Kesiapan_Proyek]
			, kp.Order_Seq
            FROM dbo.TBLT_Opportunity_Kesiapan_Proyek okp
                INNER JOIN dbo.TBLM_Kesiapan_Proyek kp
                ON okp.Kesiapan_Proyek_Id=kp.Id
        ),
        RowOpportunityKesiapanProyek
        AS
        (
            SELECT opo.Id AS Opportunity_Id
		, (SELECT ','+okp.Kesiapan_Proyek
                FROM OpportunityKesiapanProyek okp
                WHERE opo.id=okp.Opportunity_Id
                ORDER BY okp.Order_Seq
                FOR XML PATH('')
		 ) AS Kesiapan_proyek
            FROM dbo.TBLT_Opportunity opo
            GROUP BY opo.Id
        ),
        RowOpportunityLokasiProyek
        AS
        (
            SELECT
                lp.Opportunity_Id
       , STRING_AGG(lp.Lokasi_Proyek,',') AS Lokasi_Proyek
            FROM dbo.TBLT_Opportunity_Lokasi_Proyek lp
            GROUP BY lp.Opportunity_Id
        ),
        RowOpportunityPartners
        AS
        (
            SELECT op.Opportunity_Id
				, STRING_AGG(op.Partner_Name,',')	AS Partners
            FROM dbo.TBLT_Opportunity_Partners op
            GROUP BY op.Opportunity_Id
        ),
        RowOpportunityNegaraMitra
        AS
        (
            SELECT onm.Opportunity_Id 
					, STRING_AGG(CAST(onm.Negara_Mitra_Id  AS NVARCHAR(36)),',') AS Negara_Mitra_Id 
					, STRING_AGG(nm.Nama_Negara,',') AS Negara_Mitra
					, STRING_AGG( CAST(nm.Kawasan_Mitra_Id AS NVARCHAR(36)),',') AS Kawasan_Mitra_Id
					, STRING_AGG(km.Nama_Kawasan,',') AS Kawasan_Mitra
					, STRING_AGG(CAST(km.Continent_Id AS NVARCHAR(36)),',') AS Continent_Id
					, STRING_AGG(ct.Continent_Name,',') AS  Continent
			FROM dbo.TBLT_Opportunity_Negara_Mitra onm
                INNER JOIN dbo.TBLM_Negara_Mitra nm
                ON onm.Negara_Mitra_Id=nm.Id
                INNER JOIN dbo.TBLM_Kawasan_Mitra km
                ON nm.Kawasan_Mitra_Id=km.Id
                INNER JOIN dbo.TBLM_Continent ct
                ON km.Continent_Id=ct.Id
            GROUP BY onm.Opportunity_Id
        ),
        RowOpportunityPicFungsi
        AS
        (
            SELECT opf.Opportunity_Id
	   , STRING_AGG(CONCAT(f.Fungsi_Name,'-',pf.PIC_Name),',') AS PIC_Name
            --,STRING_AGG(pf.PIC_Name,',') AS PIC_Name
            FROM dbo.TBLT_Opportunity_PIC_Fungsi opf
                INNER JOIN dbo.TBLM_PIC_Fungsi pf
                ON opf.PIC_Fungsi_Id=pf.Id
                INNER JOIN dbo.TBLM_Fungsi f
                ON pf.Fungsi_Id=f.Id
            GROUP BY opf.Opportunity_Id
        ),
        RowOpportunityTargetMitra
        AS
        (
            SELECT otm.Opportunity_Id
			, STRING_AGG(tm.Name,',') AS Target_Mitra
            FROM dbo.TBLT_Opportunity_Target_Mitra otm
                INNER JOIN dbo.TBLM_Target_Mitra tm
                ON otm.Target_Mitra_Id=tm.id
            GROUP BY otm.Opportunity_Id
        )

    SELECT
        op.id AS opportunity_Id
		, op.Create_By AS Dibuat_Oleh
		, CONVERT(VARCHAR(10),op.Create_Date,31) AS Tanggal_Buat
		, op.Nama_Proyek
		, osb.Stream_Business_Id 
		, osb.Stream_Business_name  AS Stream_Business
		, ep.Entitas_Pertamina_Id 
		, ep.Company_Name  AS Entitas_Pertamina
		, ep.HSH_Id
		, ep.HSH_Name 
		, pic.Pic_Name
		, nm.Negara_Mitra_Id 
		, nm.Negara_Mitra
		, nm.Kawasan_Mitra_Id 
		, nm.Kawasan_Mitra
		, lp.Lokasi_Proyek 
		, IIF(kp.Kesiapan_proyek IS NULL,NULL,SUBSTRING(kp.Kesiapan_proyek,2,LEN(kp.Kesiapan_proyek))) AS Kesiapan_Proyek
		, op.Project_Profile
		, op.Nilai_Proyek
		, op.Deliverables
		, op.Timeline
		, op.Progress
		, tm.Target_Mitra 
		, prt.Partners  AS Existing_Parties
		, op.Catatan_Tambahan
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
        LEFT JOIN RowOpportunityLokasiProyek lp
        ON op.Id=lp.Opportunity_Id
        LEFT JOIN RowOpportunityKesiapanProyek kp
        ON op.Id=kp.Opportunity_Id
        LEFT JOIN RowOpportunityTargetMitra tm
        ON op.Id=tm.Opportunity_Id
        LEFT JOIN RowOpportunityPartners prt
        ON op.id=prt.Opportunity_Id
    WHERE op.Deleted_Date IS NULL 
	-- AND (op.Is_Draft IS NULL OR op.Is_Draft<>1)
GO


