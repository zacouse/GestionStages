USE MASTER
GO
USE GestionStage
GO
CREATE PROC pGetRestriction(@Titre_IN VARCHAR(100) = '',@Descr_IN VARCHAR(1000) = '')
AS

DECLARE @SQL NVARCHAR(4000)

SET @SQL = 'SELECT IDRestriction,Titre,Description,Etat FROM Restriction '

IF ISNULL(@Titre_IN,'') <> '' OR ISNULL(@Descr_IN,'') <> ''
    SET @SQL = @SQL + ' WHERE 0 = 0 '

IF ISNULL(@Titre_IN,'') <> ''
    SET @SQL = @SQL + ' AND Titre LIKE ''%'+@Titre_IN+'%'''

IF ISNULL(@Descr_IN,'') <> ''
    SET @SQL = @SQL + ' AND Description LIKE ''%'+@Descr_IN+'%'''

EXEC sp_executesql @SQL

GO