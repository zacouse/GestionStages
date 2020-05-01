use [GestionStage]
GO

CREATE PROC pGetAllStage
AS
SELECT [IDStage], Stage.[IDMilieuStage], Stage.[Titre], Stage.[Description], [NbPostes], [Statut], [PeriodeTravail], [NbHeureSemaine], [DateDebut], [DateFin], Stage.[Etat], MilieuStage.Titre
FROM Stage
left join MilieuStage ON Stage.[IDMilieuStage] = MilieuStage.[IDMilieuStage]
GO

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

Create PROC [dbo].[pAddSetStage] @IDStage_IN INT, @IDMilieuStage_IN INT, @Titre_IN Varchar(100), @Description_IN Varchar(1000), @NbPostes_IN INT, @Statut_IN TINYINT, @PeriodeTravail_IN TINYINT, @NbHeureSemaine_IN INT, @DateDebut_IN DateTime, @DateFin_IN DateTime, @Etat_IN Bit, @IDRestriction_IN Varchar(4000)
AS
DECLARE @isNew Bit;
IF @IDStage_IN = 0
BEGIN
INSERT INTO Stage ([IDMilieuStage], [Titre], [Description], [NbPostes], [Statut], [PeriodeTravail], [NbHeureSemaine], [DateDebut], [DateFin], [Etat], [DateHeureCreation], [DateHeureModification])
VALUES (@IDMilieuStage_IN, @Titre_IN, @Description_IN, @NbPostes_IN, @Statut_IN, @PeriodeTravail_IN, @NbHeureSemaine_IN, @DateDebut_IN, @DateFin_IN, @Etat_IN, GETDATE(), GETDATE())
set @IDStage_IN =SCOPE_IDENTITY()
Set @isNew = 1
END
ELSE
BEGIN
UPDATE Stage 
SET [IDMilieuStage] = @IDMilieuStage_IN, [Titre] = @Titre_IN, [Description] = @Description_IN, [NbPostes] = @NbPostes_IN, [Statut] = @Statut_IN, [PeriodeTravail] = @PeriodeTravail_IN, [NbHeureSemaine] = @NbHeureSemaine_IN,
[DateDebut] = @DateDebut_IN, [DateFin] = @DateFin_IN,[Etat] = @Etat_IN, [DateHeureModification] = GETDATE() 
WHERE [IDStage] = @IDStage_IN
Set @isNew = 0
END
exec pAddSetStageRestriction @IDStage_IN, @IDRestriction_IN , @isNew
GO

CREATE PROC pGetStageByID(@IDStage_IN INT)
AS
SELECT [IDStage],MilieuStage.[IDMilieuStage], Stage.[Titre], Stage.[Description], [NbPostes], [Statut], [PeriodeTravail], [NbHeureSemaine], [DateDebut], [DateFin], Stage.[Etat],MilieuStage.[Titre]
FROM Stage
Left Join MilieuStage ON MilieuStage.IDMilieuStage = Stage.IDMilieuStage
WHERE [IDStage] = @IDStage_IN;
GO

CREATE PROC pGetStage(
@Titre_IN VARCHAR(100) = '',@Description_IN VARCHAR(1000)= '',@Milieu_IN VARCHAR(100) = '',@Minh_IN INT = 0,@Maxh_IN INT = 0,@MinDate_IN DATETIME = 0,@MaxDate_IN DATETIME = 0,@isJour_IN BIT = 0,@isSoir_IN BIT = 0,@isNuit_IN BIT = 0,@isActive_IN BIT = 0,@isInactive_IN BIT = 0
)AS
	
DECLARE @SQL NVARCHAR(4000)

SET @SQL = ' SELECT [IDStage], Stage.IDMilieuStage, Stage.[Titre], Stage.[Description], [NbPostes], [Statut], [PeriodeTravail], [NbHeureSemaine], [DateDebut], [DateFin], Stage.[Etat],MilieuStage.Titre '
         + ' FROM Stage '
         + ' INNER JOIN MilieuStage ON MilieuStage.IDMilieuStage = Stage.IDMilieuStage '

IF ISNULL(@Titre_IN,'') <> '' OR ISNULL(@Description_IN,'') <> '' OR ISNULL(@Milieu_IN,'') <> '' OR ISNULL(@Minh_IN,0) <> 0 OR ISNULL(@Maxh_IN,0) <> 0 OR ISNULL(@MinDate_IN,0) <> 0 OR ISNULL(@MaxDate_IN,0) <> 0
    SET @SQL = @SQL + ' WHERE 0 = 0 '
	
IF ISNULL(@Titre_IN,'') <> ''
    SET @SQL = @SQL + ' AND Stage.Titre LIKE ''%' + @Titre_IN + '%'' '

IF ISNULL(@Description_IN,'') <> ''
    SET @SQL = @SQL + ' AND Stage.Description LIKE ''%' + @Description_IN + '%'' '

IF ISNULL(@Milieu_IN,'') <> ''
    SET @SQL = @SQL + ' AND MilieuStage.Titre LIKE ''%' + @Milieu_IN + '%'' '

IF ISNULL(@Minh_IN,0) <> 0
    SET @SQL = @SQL + ' AND NbHeureSemaine > ' + CONVERT(VARCHAR,@Minh_IN) + ' '

IF ISNULL(@Maxh_IN,0) <> 0
    SET @SQL = @SQL + ' AND NbHeureSemaine < ' + CONVERT(VARCHAR,@Maxh_IN) + ' '

IF ISNULL(@MinDate_IN,0) <> 0
    SET @SQL = @SQL + ' AND DateDebut >= ''' + CONVERT(VARCHAR,@MinDate_IN) + ''' '

IF ISNULL(@MaxDate_IN,0) <> 0
    SET @SQL = @SQL + ' AND DateFin <= ''' + CONVERT(VARCHAR,@MaxDate_IN) + ''' '

IF @isJour_IN = 1 OR @isSoir_IN = 1 OR @isNuit_IN = 1
BEGIN
    SET @SQL = @SQL + ' AND ( 1 = 0 '

    IF @isJour_IN = 1
        SET @SQL = @SQL + ' OR Stage.PeriodeTravail = 0 '

    IF @isSoir_IN = 1
        SET @SQL = @SQL + ' OR Stage.PeriodeTravail = 1 '

    IF @isNuit_IN = 1
        SET @SQL = @SQL + ' OR Stage.PeriodeTravail = 2 '

    SET @SQL = @SQL + ' ) '
END

IF @isActive_IN = 1 OR @isInactive_IN = 1
BEGIN
    SET @SQL = @SQL + ' AND ( 1 = 0 '

    IF @isActive_IN = 1
        SET @SQL = @SQL + ' OR Stage.Etat = 1 '

    IF @isInactive_IN = 1
        SET @SQL = @SQL + ' OR Stage.Etat = 0 '

    SET @SQL = @SQL + ' ) '
END

EXEC sp_executesql @SQL
GO

ALTER PROC [dbo].[pGetStageByID](@IDStage_IN INT)
AS
SELECT [IDStage],MilieuStage.[IDMilieuStage], Stage.[Titre], Stage.[Description], [NbPostes], [Statut], [PeriodeTravail], [NbHeureSemaine], [DateDebut], [DateFin], Stage.[Etat],MilieuStage.[Titre]
FROM Stage
Left Join MilieuStage ON MilieuStage.IDMilieuStage = Stage.IDMilieuStage
WHERE [IDStage] = @IDStage_IN;

--INSERT INTO Stage ([IDMilieuStage], [Titre], [Description], [NbPostes], [Statut], [PeriodeTravail], [NbHeureSemaine], [DateDebut], [DateFin], [Etat], [DateHeureCreation], [DateHeureModification])
--VALUES ('1', 'Chercheur test2', 'Chercheur pour la Corps.inc', '1', '1', '2', '40', GETDATE(), GETDATE(), 'true', GETDATE(), GETDATE())

--INSERT INTO MilieuStage ([Titre],[Description],[NoCivique],[Rue],[CodePostal],[Ville],[Province],[Pays],[NoTelephone],[Etat],[DateHeureCreation],[DateHeureModification])
--VALUES('Corps.inc','Entrepise Dragon Ball', '00300030we','rue principal','D5B7Z4','ouest city','Ouest','Dragon Ball','000-292-0000','true', GETDATE(), GETDATE())

GO

