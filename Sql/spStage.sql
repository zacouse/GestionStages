use [GestionStage]
GO

CREATE PROC pGetAllStage
AS
SELECT [IDStage], [IDMilieuStage], [Titre], [Description], [Statut], [NbPostes], [PeriodeTravail], [NbHeureSemaine], [DateDebut], [DateFin], [Etat]
FROM Stage
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
SELECT [IDMilieuStage], [Titre], [Description], [Statut], [NbPostes], [PeriodeTravail], [NbHeureSemaine], [DateDebut], [DateFin], [Etat]
FROM Stage
WHERE [IDStage] = @IDStage_IN;
GO
