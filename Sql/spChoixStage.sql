USE GestionStage
GO
CREATE PROC pGetChoixEtudiantByIdStage
@IdStage_IN int
AS
SELECT etu.IDEtudiant,prog.Nom as 'NomProgramme',etu.NoDA,etu.Prenom,etu.Nom,etu.Courriel,etu.Photo,etu.Etat,StageEtudiant.NumeroChoix,StageEtudiant.ChoixFinal FROM StageEtudiant 
left join Etudiant etu ON StageEtudiant.IDEtudiant = etu.IDEtudiant 
left join Programme prog on etu.IDProgramme = prog.IDProgramme
WHERE IDStage = @IdStage_IN AND StageEtudiant.Etat = 1
GO
