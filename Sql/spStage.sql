use [GestionStage]
GO

CREATE PROC pGetAllStage
AS
SELECT [IDStage], [IDMilieuStage], [Titre], [Description], [Statut], [NbPostes], [PeriodeTravail], [NbHeureSemaine], [DateDebut], [DateFin], [Etat]
FROM Stage
GO

CREATE PROC pAddSetStage(@IDMilieuStage_IN INT, @Titre_IN Varchar(100),@Description_IN Varchar(1000),
@NbPoste_IN INT, @Statut_IN TINYINT, @PeriodeTravail_IN TINYINT, @NbHeureSemaine_IN INT, @DateDebut_IN DateTime, @DateFin_IN DateTime, @Etat_IN Bit)
AS
BEGIN
INSERT INTO Stage ([IDMilieuStage], [Titre], [Description], [NbPostes], [Statut], [PeriodeTravail], [NbHeureSemaine], [DateDebut], [DateFin], [Etat], [DateHeureCreation], [DateHeureModification])
VALUES (@IDMilieuStage_IN, @Titre_IN, @Description_IN, @NbPoste_IN, @Statut_IN, @PeriodeTravail_IN, @NbHeureSemaine_IN, @DateDebut_IN, @DateFin_IN, @Etat_IN, GETDATE(), null)
END
GO