use [GestionStage]
GO

CREATE PROC pGetAllStage
AS
SELECT [IDStage], [IDMilieuStage], [Titre], [Description], [NbPostes], [Statut], [PeriodeTravail], [NbHeureSemaine], [DateDebut], [DateFin], [Etat]
FROM Stage
GO
CREATE PROC pGetAllStageWithMilieuTitre
AS
SELECT [IDStage], MilieuStage.Titre, Stage.[Titre], Stage.[Description], [NbPostes], [Statut], [PeriodeTravail], [NbHeureSemaine], [DateDebut], [DateFin], Stage.[Etat]
FROM Stage
left join MilieuStage ON Stage.[IDMilieuStage] = MilieuStage.[IDMilieuStage]
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
SELECT [IDStage],[IDMilieuStage], [Titre], [Description], [NbPostes], [Statut], [PeriodeTravail], [NbHeureSemaine], [DateDebut], [DateFin], [Etat]
FROM Stage
WHERE [IDStage] = @IDStage_IN;
GO
CREATE PROC pGetMilieuStageForStage(@IDStage_IN INT)
AS
SELECT MilieuStage.[Titre]
FROM Stage
Left join MilieuStage ON Stage.[IDMilieuStage] = MilieuStage.[IDMilieuStage]
WHERE [IDStage] = @IDStage_IN;
GO

INSERT INTO Stage ([IDMilieuStage],[IDMilieuStage], [Titre], [Description], [NbPostes], [Statut], [PeriodeTravail], [NbHeureSemaine], [DateDebut], [DateFin], [Etat], [DateHeureCreation], [DateHeureModification])
VALUES ('1', 'Chercheur test2', 'Chercheur pour la Corps.inc', '1', '1', '2', '40', '2020-04-01', '2020-04-30', 'true', GETDATE(), GETDATE())

INSERT INTO MilieuStage ([Titre],[Description],[NoCivique],[Rue],[CodePostal],[Ville],[Province],[Pays],[NoTelephone],[Etat],[DateHeureCreation],[DateHeureModification])
VALUES('Corps.inc','Entrepise Dragon Ball', '00300030we','rue principal','D5B7Z4','ouest city','Ouest','Dragon Ball','000-292-0000','true', GETDATE(), GETDATE())