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

Create Proc [dbo].[pAddSetStageRestriction] @IDStage_IN INT, @IDRestriction_IN Varchar(4000),@New_IN Bit
AS
IF @New_IN = 1
BEGIN
Insert into StageRestriction([IDStage],[IDRestriction],[Etat])
select  @IDStage_IN as 'IDStage', IDRestriction, 1 as 'Etat' From Restriction where IDRestriction IN(select value from STRING_SPLIT(@IDRestriction_IN,',')) /*Ajout des restrictions*/
END
ELSE
BEGIN
UPDATE StageRestriction
SET [Etat] = 0 ,[DateHeureModification] =GETDATE()/*Enlever les ancients qui ne sont pas dans la nouvelle liste*/
WHERE [IDRestriction] not in (select value from STRING_SPLIT(@IDRestriction_IN,',')) and IDStage = @IDStage_IN 

Insert into StageRestriction([IDStage],[IDRestriction],[Etat])
select  @IDStage_IN as 'IDStage', IDRestriction, 1 as 'Etat' From Restriction where IDRestriction IN(select value from STRING_SPLIT(@IDRestriction_IN,',')) /*Ajout des nouvelles restrictions*/
and IDRestriction not in (select IDRestriction  from StageRestriction where IDRestriction in(select value from STRING_SPLIT(@IDRestriction_IN,',')) and [IDStage] = @IDStage_IN)

UPDATE StageRestriction
SET [Etat] = 1 ,[DateHeureModification] =GETDATE()/*Actualiser l'état des restrictions*/
WHERE [IDRestriction] in (select value from STRING_SPLIT(@IDRestriction_IN,',')) and IDStage = @IDStage_IN
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