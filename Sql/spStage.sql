use [GestionStage]
GO

CREATE PROC pGetAllStage
AS
SELECT [IDStage], Stage.[IDMilieuStage], Stage.[Titre], Stage.[Description], [NbPostes], [Statut], [PeriodeTravail], [NbHeureSemaine], [DateDebut], [DateFin], Stage.[Etat], MilieuStage.Titre
FROM Stage
left join MilieuStage ON Stage.[IDMilieuStage] = MilieuStage.[IDMilieuStage]
GO

CREATE PROC pGetAllStageActif
AS
SELECT [IDStage], Stage.[IDMilieuStage], Stage.[Titre], Stage.[Description], [NbPostes], [Statut], [PeriodeTravail], [NbHeureSemaine], [DateDebut], [DateFin], Stage.[Etat], MilieuStage.Titre
FROM Stage
left join MilieuStage ON Stage.[IDMilieuStage] = MilieuStage.[IDMilieuStage] where Stage.Etat = 1
GO

CREATE PROC pAddSetStage @IDStage_IN INT, @IDMilieuStage_IN INT, @Titre_IN Varchar(100), @Description_IN Varchar(1000), @NbPoste_IN INT, @Statut_IN TINYINT, @PeriodeTravail_IN TINYINT, @NbHeureSemaine_IN INT, @DateDebut_IN DateTime, @DateFin_IN DateTime, @Etat_IN Bit
AS
IF @IDStage_IN = 0
BEGIN
INSERT INTO Stage ([IDMilieuStage], [Titre], [Description], [NbPostes], [Statut], [PeriodeTravail], [NbHeureSemaine], [DateDebut], [DateFin], [Etat], [DateHeureCreation], [DateHeureModification])
VALUES (@IDMilieuStage_IN, @Titre_IN, @Description_IN, @NbPoste_IN, @Statut_IN, @PeriodeTravail_IN, @NbHeureSemaine_IN, @DateDebut_IN, @DateFin_IN, @Etat_IN, GETDATE(), GETDATE())
END
ELSE
BEGIN
UPDATE Stage 
SET [IDMilieuStage] = @IDMilieuStage_IN, [Titre] = @Titre_IN, [Description] = @Description_IN, [NbPostes] = @NbPoste_IN, [Statut] = @Statut_IN, [PeriodeTravail] = @PeriodeTravail_IN, [NbHeureSemaine] = @NbHeureSemaine_IN,
[DateDebut] = @DateDebut_IN, [DateFin] = @DateFin_IN,[Etat] = @Etat_IN, [DateHeureModification] = GETDATE() 
WHERE [IDStage] = @IDStage_IN
END
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
--VALUES ('1', 'Chercheur test2 ultra long parce que ca me tente', 'Chercheur pour la Corps.inc', '1', '1', '2', '40', GETDATE(), GETDATE(), '1', GETDATE(), GETDATE())

--INSERT INTO MilieuStage ([Titre],[Description],[NoCivique],[Rue],[CodePostal],[Ville],[Province],[Pays],[NoTelephone],[Etat],[DateHeureCreation],[DateHeureModification])
--VALUES('Corps.inc','Entrepise Dragon Ball', '00300030we','rue principal','D5B7Z4','ouest city','Ouest','Dragon Ball','000-292-0000','true', GETDATE(), GETDATE())
GO
CREATE PROC pAddSetChoixStage
	@IdStageEtudiant_IN INT,
	@IdStage_IN INT,
	@IdEtudiant_IN INT,
	@NumeroChoix_IN INT,
	@ChoixFinal_IN BIT,
	@Etat_IN BIT
	AS
	IF @IdStageEtudiant_IN = 0
	BEGIN
		UPDATE  [dbo].[StageEtudiant]
		SET [Etat] = '0',[DateHeureModification] = GETDATE()
		WHERE [IDEtudiant] = @IdEtudiant_IN AND [NumeroChoix] = @NumeroChoix_IN ;

		INSERT INTO [dbo].[StageEtudiant]
           ([IDStage],[IDEtudiant],[NumeroChoix],[ChoixFinal],[Etat])
     VALUES
           (@IdStage_IN,@IdEtudiant_IN,@NumeroChoix_IN,@ChoixFinal_IN,@Etat_IN); 
	END
	ELSE
	BEGIN
		UPDATE  [dbo].[StageEtudiant]
		SET [IDStage]				= @IdStage_IN,
			[IDEtudiant]			= @IdEtudiant_IN,
			[NumeroChoix]			= @NumeroChoix_IN,
			[ChoixFinal]			= @ChoixFinal_IN,
			[Etat]					= @Etat_IN,
			[DateHeureModification] = GETDATE()
		WHERE [IDStageEtudiant] = @IdStageEtudiant_IN
	END
GO
--exec pAddSetChoixStage'0','1','1','2','0','1'
CREATE PROC pAddSetRetirerChoixStage
@IdEtudiant_IN INT,
@NumeroChoix_IN INT
AS
UPDATE  [dbo].[StageEtudiant]
		SET [Etat] = '0',[DateHeureModification] = GETDATE()
		WHERE [IDEtudiant] = @IdEtudiant_IN AND [NumeroChoix] = @NumeroChoix_IN ;
GO
CREATE PROC pGetChoixStageByIdEtudiant
@IdEtudiant_IN INT
AS
SELECT [IDStageEtudiant]
      ,[IDStage]
      ,[IDEtudiant]
      ,[NumeroChoix]
      ,[ChoixFinal]
      ,[Etat] FROM StageEtudiant WHERE IDEtudiant = @IdEtudiant_IN and Etat = '1' ;
go
CREATE PROC pGetStagesByIdMilieu(@IdMilieu_IN INT)
AS
SELECT [IDStage],[IDMilieuStage], [Titre], [Description], [NbPostes], [Statut], [PeriodeTravail], [NbHeureSemaine], [DateDebut], [DateFin], [Etat]
FROM Stage
WHERE [IDMilieuStage] = @IdMilieu_IN;
GO