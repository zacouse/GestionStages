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
