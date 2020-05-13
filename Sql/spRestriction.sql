USE MASTER
GO
USE GestionStage
GO
CREATE PROC pGetRestriction(@Titre_IN VARCHAR(100) = '',@Descr_IN VARCHAR(1000) = '',@isActive_IN BIT = 0,@isInactive_IN BIT = 0)
AS
DECLARE @SQL NVARCHAR(4000)
SET @SQL = 'SELECT IDRestriction,Titre,Description,Etat FROM Restriction WHERE 0 = 0'

IF ISNULL(@Titre_IN,'') <> ''
    SET @SQL = @SQL + ' AND Restriction.Titre LIKE ''%' + @Titre_IN + '%'' '

IF ISNULL(@Descr_IN,'') <> ''
    SET @SQL = @SQL + ' AND Restriction.Description LIKE ''%' + @Descr_IN + '%'' '
	IF @isActive_IN = 1 OR @isInactive_IN = 1
BEGIN
    SET @SQL = @SQL + ' AND ( 1 = 0 '

    IF @isActive_IN = 1
        SET @SQL = @SQL + ' OR Restriction.Etat = 1 '

    IF @isInactive_IN = 1
        SET @SQL = @SQL + ' OR Restriction.Etat = 0 '

    SET @SQL = @SQL + ' ) '
END

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

CREATE PROC pGetAllRestrictionForStageWithMilieuIncludedByIds
@IDMilieu_IN int,
@IDStage_IN int
AS
SELECT DISTINCT Restriction.Titre, Restriction.Description From Restriction 
        left join MilieuStageRestriction ON MilieuStageRestriction.IDRestriction = Restriction.IDRestriction
		left join StageRestriction ON StageRestriction.IDRestriction = Restriction.IDRestriction
    WHERE (MilieuStageRestriction.IDMilieuStage = @IDMilieu_IN  AND MilieuStageRestriction.Etat = 1) OR (StageRestriction.IDStage = @IDStage_IN  AND StageRestriction.Etat = 1)
GO