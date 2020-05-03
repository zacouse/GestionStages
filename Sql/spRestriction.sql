USE MASTER
GO
USE GestionStage
GO
CREATE PROC pGetRestriction(@Titre_IN VARCHAR(100) = '',@Descr_IN VARCHAR(1000) = '')
AS
SELECT IDRestriction,Titre,Description,Etat 
FROM Restriction 
WHERE Titre LIKE '%'+@Titre_IN+'%'
AND Description LIKE '%'+@Descr_IN+'%'
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
Create Proc pGetAllRestriction
AS
    Select [IDRestriction],[Titre],[Description],[Etat] FROM Restriction
GO
--RestrictionStage

Create Proc pAddSetStageRestriction @IDStageRestriction_IN INT, @IDStage_IN INT,@Titre_IN Varchar(100), @Description_IN Varchar(1000),@Etat_IN Bit
AS
IF @IDStageRestriction_IN = 0
BEGIN
    Insert into StageRestriction([IDStage],[Etat],[DateHeureCreation],[DateHeureModification])
    Values (@IDStage_IN,@Etat_IN, GETDATE(), GETDATE())
END
ELSE
BEGIN
    UPDATE StageRestriction
    SET [IDStage] = @IDStage_IN,[Etat] = @Etat_IN,[DateHeureModification] = GETDATE()
    WHERE [IDStageRestriction] = @IDStageRestriction_IN
END
GO

CREATE PROC pGetRestrictionIdByIdStage(@IDStage_IN INT)
AS
    SELECT IDRestriction FROM StageRestriction WHERE IDStage=@IDStage_IN AND Etat = 1
go

create Proc pGetAllStageRestrictionByIdStage @IDStage_IN INT
AS
    SELECT DISTINCT Restriction.Titre, Restriction.Description 
    From Restriction 
        Left join StageRestriction ON StageRestriction.IDRestriction = Restriction.IDRestriction
    WHERE StageRestriction.IDStage = @IDStage_IN  AND StageRestriction.Etat = 1
GO
--RestrictionMilieu

CREATE PROC pGetRestrictionIdByIdMilieu(@IDMilieu_IN INT)
AS
    SELECT IDRestriction FROM MilieuStageRestriction WHERE IDMilieuStage=@IDMilieu_IN AND Etat = 1
go

create Proc pGetAllMilieuStageRestrictionByIdMilieuStage @IDMilieuStage_IN INT
AS
    SELECT DISTINCT Restriction.Titre, Restriction.Description 
    From Restriction 
        Left join MilieuStageRestriction ON MilieuStageRestriction.IDRestriction = Restriction.IDRestriction
    WHERE MilieuStageRestriction.IDMilieuStage = @IDMilieuStage_IN  AND MilieuStageRestriction.Etat = 1
GO