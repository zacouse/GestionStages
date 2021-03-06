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
EXEC sp_addextendedproperty 
@name=N'MS_Description',
@value='0 = Jour, 1 = Soir, 2 = Nuit',
@Level0Type=N'Schema',
@Level0Name='dbo',
@Level1Type=N'Table',
@Level1Name='Stage',
@Level2Type=N'Column',
@Level2Name='PeriodeTravail' 
GO
EXEC sp_addextendedproperty 
@name=N'MS_Description',
@value='0 = Temps partiel, 1 = Temps plein',
@Level0Type=N'Schema',
@Level0Name='dbo',
@Level1Type=N'Table',
@Level1Name='Stage',
@Level2Type=N'Column',
@Level2Name='Statut' 
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
	Etat BIT,
	DateHeureCreation DATETIME DEFAULT GetDate(),
	DateHeureModification DATETIME DEFAULT GetDate(),
	FOREIGN KEY (IDMilieuStage) REFERENCES MilieuStage(IDMilieuStage),
	FOREIGN KEY (IDRestriction) REFERENCES Restriction(IDRestriction)
)
GO

Create TABLE StageRestriction (
	IDStageRestriction INT IDENTITY PRIMARY KEY,
	IDStage INT,
	IDRestriction INT,
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

CREATE TABLE Superviseur(
    IDSuperviseur INT IDENTITY PRIMARY KEY,
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

CREATE TABLE StageEtudiant (
	IDStageEtudiant INT IDENTITY PRIMARY KEY,
	IDStage INT,
	IDEtudiant INT,
	NumeroChoix INT,
	ChoixFinal BIT,
    IDSuperviseur INT,
	Etat BIT,
	DateHeureCreation DATETIME DEFAULT GetDate(),
	DateHeureModification DATETIME DEFAULT GetDate(),
	FOREIGN KEY (IDStage) REFERENCES Stage(IDStage),
	FOREIGN KEY (IDEtudiant) REFERENCES Etudiant(IDEtudiant),
    FOREIGN KEY (IDSuperviseur) REFERENCES Superviseur(IDSuperviseur)
)
GO

--INSERT INTO Programme([Nom], [Sigle], [Etat])
--VALUES ('Hamon','123',1)
--		,('Stand','456',1)

--INSERT INTO Etudiant([IDProgramme], [NoDA], [Prenom], [Nom], [Courriel], [Photo], [Etat])
--VALUES (1,'820001','Jonathan','Joestar','',NULL,1)
--		,(2,'950003','Jotaro','Kujo','',NULL,1)
--GO
IF EXISTS (SELECT * FROM sys.objects WHERE type = 'P' AND name = 'pGetAllActiveEtudiant')
DROP PROCEDURE pGetAllActiveEtudiant
GO
CREATE PROC pGetAllActiveEtudiant
AS
	SELECT Etudiant.[IDEtudiant], Programme.Nom, Etudiant.[NoDA], Etudiant.[Prenom], Etudiant.[Nom], Etudiant.[Courriel], Etudiant.[Photo]
	FROM Etudiant
	INNER JOIN Programme ON Etudiant.IDProgramme = Programme.IDProgramme
	WHERE Etudiant.Etat = 1
GO
IF EXISTS (SELECT * FROM sys.objects WHERE type = 'P' AND name = 'pGetAllActivePersonneContact')
DROP PROCEDURE pGetAllActivePersonneContact
GO
CREATE PROC pGetAllActivePersonneContact
AS
	SELECT IDPersonneContact,Nom,Prenom,Courriel,Etat
	FROM PersonneContact WHERE Etat=1
GO

--insert into PersonneContact(Nom,Prenom,Courriel,Etat)
--values('Zeppeli','Cesar','Jo@Joke.ca',1)
--GO
IF EXISTS (SELECT * FROM sys.objects WHERE type = 'P' AND name = 'pGetPersonneContactByStageID')
DROP PROCEDURE pGetPersonneContactByStageID
GO
CREATE PROC pGetPersonneContactByStageID(@IDStage_IN INT)
AS
SELECT PersonneContact.IDPersonneContact,PersonneContact.Nom,PersonneContact.Prenom,PersonneContact.Courriel,PersonneContact.Etat 
FROM PersonneContact
INNER JOIN PersonneContactStage ON PersonneContactStage.IDPersonneContact = PersonneContact.IDPersonneContact AND @IDStage_IN = PersonneContactStage.IDStage
GO
IF EXISTS (SELECT * FROM sys.objects WHERE type = 'P' AND name = 'pGetAllSuperviseur')
DROP PROCEDURE pGetAllSuperviseur
GO
CREATE PROC pGetAllSuperviseur
AS
    SELECT IDSuperviseur,Nom,Prenom,Courriel,Etat
	FROM Superviseur WHERE Etat=1
GO
insert into Superviseur(Nom,Prenom,Courriel,Etat)
values('Zeppeli','Cesar','Jo@Joke.ca',1)
GO

--INSERT INTO MilieuStage([Titre], [Description], [NoCivique], [Rue], [CodePostal], [Ville], [Province], [Pays], [NoTelephone], [Etat])
--VALUES('Milieu1','descr','1','1','A1A 1A1','Quebec','QC','Canada','(123) 123-5678',1),
--	('Milieu2','descr','2','2','B2B 2B2','Quebec','QC','Canada','(123) 123-5678',1)
--GO

--INSERT INTO Stage([IDMilieuStage], [Titre], [Description], [NbPostes], [Statut], [PeriodeTravail], [NbHeureSemaine], [DateDebut], [DateFin], [Etat])
--VALUES(1,'Stage1','descr',1,1,1,40,GETDATE(),GETDATE()+1000,1),
--	(2,'Stage2','descr',1,1,1,40,GETDATE(),GETDATE()+1000,1)
--GO



