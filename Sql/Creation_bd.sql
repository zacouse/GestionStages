USE [master]
GO

IF NOT EXISTS (SELECT * FROM sys.databases WHERE name = N'GestionStage')
BEGIN
	CREATE DATABASE GestionStage 
END
GO

use GestionStage
GO

CREATE TABLE MilieuStage (
	IDMilieuStage INT IDENTITY PRIMARY KEY,
	Titre VARCHAR(100),
	[Description] VARCHAR(1000),
	NoCivique VARCHAR(10),
	Rue VARCHAR(100),
	CodePostal VARCHAR(7),
	Ville VARCHAR(100),
	Province VARCHAR(100),
	Pays VARCHAR(100),
	NoTelephone VARCHAR(20),
	Etat BIT,
	DateHeureCreation DATETIME DEFAULT GetDate(),
	DateHeureModification DATETIME DEFAULT GetDate()
)
GO

CREATE TABLE Stage (
	IDStage INT IDENTITY PRIMARY KEY,
	IDMilieuStage INT,
	Titre VARCHAR(100),
	[Description] VARCHAR(1000),
	NbPostes INT,
	Statut TINYINT,
	PeriodeTravail TINYINT,
	NbHeureSemaine INT,
	DateDebut DATETIME,
	DateFin DATETIME,
	Etat BIT,
	DateHeureCreation DATETIME DEFAULT GetDate(),
	DateHeureModification DATETIME DEFAULT GetDate(),
	FOREIGN KEY (IDMilieuStage) REFERENCES MilieuStage(IDMilieuStage) 
)
GO

CREATE TABLE Restriction (
	IDRestriction INT IDENTITY PRIMARY KEY,
	Titre VARCHAR(100),
	[Description] VARCHAR(1000),
	Etat BIT,
	DateHeureCreation DATETIME DEFAULT GetDate(),
	DateHeureModification DATETIME DEFAULT GetDate()
)
GO

CREATE TABLE MilieuStageRestriction (
	IDMilieuStageRestriction INT IDENTITY PRIMARY KEY,
	IDRestriction INT,
	IDMilieuStage INT,
	Titre VARCHAR(100),
	[Description] VARCHAR(1000),
	Etat BIT,
	DateHeureCreation DATETIME DEFAULT GetDate(),
	DateHeureModification DATETIME DEFAULT GetDate(),
	FOREIGN KEY (IDMilieuStage) REFERENCES MilieuStage(IDMilieuStage),
	FOREIGN KEY (IDRestriction) REFERENCES Restriction(IDRestriction)
)
GO

CREATE TABLE StageRestriction (
	IDStageRestriction INT IDENTITY PRIMARY KEY,
	IDStage INT,
	IDRestriction INT,
	Titre VARCHAR(100),
	[Description] VARCHAR(1000),
	Etat BIT,
	DateHeureCreation DATETIME DEFAULT GetDate(),
	DateHeureModification DATETIME DEFAULT GetDate(),
	FOREIGN KEY (IDStage) REFERENCES Stage(IDStage),
	FOREIGN KEY (IDRestriction) REFERENCES Restriction(IDRestriction)
)
GO

CREATE TABLE PersonneContact (
	IDPersonneContact INT IDENTITY PRIMARY KEY,
	Prenom VARCHAR(50),
	Nom VARCHAR(50),
	Fonction VARCHAR(100),
	NoTelephone VARCHAR(20),
	Courriel VARCHAR(50),
	Etat BIT,
	DateHeureCreation DATETIME DEFAULT GetDate(),
	DateHeureModification DATETIME DEFAULT GetDate()
)
GO

CREATE TABLE PersonneContactMilieuStage (
	IDPersonneContactMilieuStage INT IDENTITY PRIMARY KEY,
	IDPersonneContact INT,
	IDMilieuStage INT,
	Prenom VARCHAR(50),
	Nom VARCHAR(50),
	Fonction VARCHAR(100),
	NoTelephone VARCHAR(20),
	Courriel VARCHAR(50),
	Etat BIT,
	DateHeureCreation DATETIME DEFAULT GetDate(),
	DateHeureModification DATETIME DEFAULT GetDate(),
	FOREIGN KEY (IDPersonneContact) REFERENCES PersonneContact(IDPersonneContact),
	FOREIGN KEY (IDMilieuStage) REFERENCES MilieuStage(IDMilieuStage)
)
GO

CREATE TABLE PersonneContactStage (
	IDPersonneContactStage INT IDENTITY PRIMARY KEY,
	IDPersonneContact INT,
	IDStage INT,
	Prenom VARCHAR(50),
	Nom VARCHAR(50),
	Fonction VARCHAR(100),
	NoTelephone VARCHAR(20),
	Courriel VARCHAR(50),
	Etat BIT,
	DateHeureCreation DATETIME DEFAULT GetDate(),
	DateHeureModification DATETIME DEFAULT GetDate(),
	FOREIGN KEY (IDPersonneContact) REFERENCES PersonneContact(IDPersonneContact),
	FOREIGN KEY (IDStage) REFERENCES Stage(IDStage)
)
GO

CREATE TABLE Programme (
	IDProgramme INT IDENTITY PRIMARY KEY,
	Nom VARCHAR(100),
	Sigle VARCHAR(15),
	Etat BIT,
	DateHeureCreation DATETIME DEFAULT GetDate(),
	DateHeureModification DATETIME DEFAULT GetDate()
)
GO

CREATE TABLE ProgrammeStage (
	IDProgrammeStage INT IDENTITY PRIMARY KEY,
	IDProgramme INT,
	IDStage INT,
	Etat BIT,
	DateHeureCreation DATETIME DEFAULT GetDate(),
	DateHeureModification DATETIME DEFAULT GetDate(),
	FOREIGN KEY (IDProgramme) REFERENCES Programme(IDProgramme),
	FOREIGN KEY (IDStage) REFERENCES Stage(IDStage)
)
GO

CREATE TABLE Etudiant (
	IDEtudiant INT IDENTITY PRIMARY KEY,
	IDProgramme INT,
	NoDA INT,
	Prenom VARCHAR(100),
	Nom VARCHAR(100),
	Courriel VARCHAR(100),
	Photo VARBINARY(8000),
	Etat BIT,
	DateHeureCreation DATETIME DEFAULT GetDate(),
	DateHeureModification DATETIME DEFAULT GetDate()
)

GO

CREATE TABLE StageEtudiant (
	IDStageEtudiant INT IDENTITY PRIMARY KEY,
	IDStage INT,
	IDEtudiant INT,
	Etat BIT,
	DateHeureCreation DATETIME DEFAULT GetDate(),
	DateHeureModification DATETIME DEFAULT GetDate(),
	FOREIGN KEY (IDStage) REFERENCES Stage(IDStage),
	FOREIGN KEY (IDEtudiant) REFERENCES Etudiant(IDEtudiant)
)

CREATE TABLE ChoixStageEtudiant (
	IDChoixStageEtudiant INT IDENTITY PRIMARY KEY,
	IDStage INT,
	IDEtudiant INT,
	Ordre TINYINT,
	Etat BIT,
	DateHeureCreation DATETIME DEFAULT GetDate(),
	DateHeureModification DATETIME DEFAULT GetDate(),
	FOREIGN KEY (IDStage) REFERENCES Stage(IDStage),
	FOREIGN KEY (IDEtudiant) REFERENCES Etudiant(IDEtudiant)
)

INSERT INTO Programme([Nom], [Sigle], [Etat])
VALUES ('Hamon','123',1)
		,('Stand','456',1)

INSERT INTO Etudiant([IDProgramme], [NoDA], [Prenom], [Nom], [Courriel], [Photo], [Etat])
VALUES (1,'820001','Jonathan','Joestar','',NULL,1)
		,(2,'950003','Jotaro','Kujo','',NULL,1)
GO

CREATE PROC pGetAllActiveEtudiant
AS
	SELECT Etudiant.[IDEtudiant], Programme.Nom, Etudiant.[NoDA], Etudiant.[Prenom], Etudiant.[Nom], Etudiant.[Courriel], Etudiant.[Photo]
	FROM Etudiant
	INNER JOIN Programme ON Etudiant.IDProgramme = Programme.IDProgramme
	WHERE Etudiant.Etat = 1
GO

INSERT INTO MilieuStage([Titre], [Description], [NoCivique], [Rue], [CodePostal], [Ville], [Province], [Pays], [NoTelephone], [Etat])
VALUES('Milieu1','descr','1','1','A1A 1A1','Quebec','QC','Canada','(123) 123-5678',1),
	('Milieu2','descr','2','2','B2B 2B2','Quebec','QC','Canada','(123) 123-5678',1)
GO

INSERT INTO Stage([IDMilieuStage], [Titre], [Description], [NbPostes], [Statut], [PeriodeTravail], [NbHeureSemaine], [DateDebut], [DateFin], [Etat])
VALUES(1,'Stage1','descr',1,1,1,40,GETDATE(),GETDATE()+1000,1),
	(2,'Stage2','descr',1,1,1,40,GETDATE(),GETDATE()+1000,1)
GO

ALTER PROC pGetAllStage
AS
	SELECT [IDStage], Stage.[IDMilieuStage], Stage.[Titre], Stage.[Description], [NbPostes], [Statut], [PeriodeTravail], [NbHeureSemaine], [DateDebut], [DateFin], Stage.[Etat],MilieuStage.Titre
	FROM Stage
	INNER JOIN MilieuStage ON MilieuStage.IDMilieuStage = Stage.IDMilieuStage
GO

CREATE PROC pGetMilieuStage(
@Titre_IN VARCHAR(100) = '',@Description_IN VARCHAR(1000)= '',@Adresse_IN VARCHAR(1000) =''
)AS
	
DECLARE @SQL NVARCHAR(4000)

SET @SQL = 'SELECT [IDMilieuStage], [Titre], [Description], [NoCivique], [Rue], [CodePostal], [Ville], [Province], [Pays], [NoTelephone], [Etat] '
         + 'FROM MilieuStage '

IF @Titre_IN <> '' OR @Description_IN <> '' OR @Adresse_IN <> ''
    SET @SQL = @SQL + ' WHERE 0 = 0 '
	
IF @Titre_IN <> ''
    SET @SQL = @SQL + ' AND Titre LIKE ''%' + @Titre_IN + '%'' '

IF @Description_IN <> ''
    SET @SQL = @SQL + ' AND Description LIKE ''%' + @Description_IN + '%'' '

IF @Adresse_IN <> ''
    SET @SQL = @SQL + ' AND NoCivique + '' '' + Rue + '', '' + Ville + ''   '' + Province + '', '' + Pays LIKE ''%' + @Adresse_IN + '%'' '

EXEC sp_executesql @SQL
GO

ALTER PROC pGetStage(
@Titre_IN VARCHAR(100) = '',@Description_IN VARCHAR(1000)= '',@Milieu_IN VARCHAR(100) = '',@Minh_IN INT = 0,@Maxh_IN INT = 0,@MinDate_IN DATETIME = 0,@MaxDate_IN DATETIME = 0
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
    SET @SQL = @SQL + ' AND DateDebut > ' + CONVERT(VARCHAR,@MinDate_IN) + ' '

IF ISNULL(@MaxDate_IN,0) <> 0
    SET @SQL = @SQL + ' AND DateFin < ' + CONVERT(VARCHAR,@MaxDate_IN) + ' '

EXEC sp_executesql @SQL
GO