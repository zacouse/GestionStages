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

CREATE PROC pGetRestrictionByID(@IDRestriction_IN INT)
AS

SELECT IDRestriction,Titre,[Description],Etat FROM Restriction
WHERE IDRestriction = @IDRestriction_IN

GO

CREATE PROC pAddSetRestriction(@IDRestriction_IN INT,@Titre_IN VARCHAR(100),@Descr_IN VARCHAR(1000),@Etat_IN BIT)
AS
IF @IDRestriction_IN = 0
BEGIN
    INSERT INTO Restriction(Titre,[Description],Etat)
    VALUES(@Titre_IN,@Descr_IN,@Etat_IN)
END
ELSE
BEGIN
    UPDATE Restriction
    SET Titre = @Titre_IN,
        [Description] = @Descr_IN,
        Etat = @Etat_IN,
        DateHeureModification = GETDATE()
    WHERE IDRestriction = @IDRestriction_IN
END
GO