USE DB_IRIS
Go
ALTER TABLE dbo.TBLT_External_Contact ALTER COLUMN No_Telp_Person nvarchar(255)
ALTER TABLE dbo.TBLT_External_Contact ALTER COLUMN No_Telp_Company nvarchar(50)
ALTER TABLE dbo.TBLT_External_Contact ALTER COLUMN Email_Company nvarchar(100)
ALTER TABLE dbo.TBLT_External_Contact ALTER COLUMN Email_Person nvarchar(255)

