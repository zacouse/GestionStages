USE GestionStage
GO

CREATE PROC pGetChoixEtudiantByIdStage
@IdStage_IN int
AS
SELECT etu.IDEtudiant,prog.Nom as 'NomProgramme',etu.NoDA,etu.Prenom,etu.Nom,etu.Courriel,etu.Photo,etu.Etat,StageEtudiant.NumeroChoix,StageEtudiant.ChoixFinal, ISNULL(StageEtudiant.IDSuperviseur,0), StageEtudiant.IDStageEtudiant
FROM StageEtudiant 
left join Etudiant etu ON StageEtudiant.IDEtudiant = etu.IDEtudiant 
left join Programme prog on etu.IDProgramme = prog.IDProgramme
WHERE IDStage = @IdStage_IN AND StageEtudiant.Etat = 1
order by StageEtudiant.NumeroChoix
GO

CREATE PROC pAddSetOneAssignationStage(@IDStageEtudiant_IN INT, @ChoixFinal_IN BIT, @IDSuperviseur_IN INT)
AS
    UPDATE StageEtudiant
    SET ChoixFinal = @ChoixFinal_IN
    ,   IDSuperviseur = @IDSuperviseur_IN
    ,   DateHeureModification = GetDate()
    WHERE IDStageEtudiant = @IDStageEtudiant_IN
GO