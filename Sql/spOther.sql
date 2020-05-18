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
values('Deblois','Charles','DCharles@hotmail.ca',1)
GO
insert into Superviseur(Nom,Prenom,Courriel,Etat)
values('LaFortune','Charles','LCharles@hotmail.ca',1)
GO