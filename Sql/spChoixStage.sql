USE GestionStage
GO
CREATE PROC pGetChoixEtudiantByIdStage
@IdStage_IN int
AS
SELECT etu.IDEtudiant,prog.Nom as 'NomProgramme',etu.NoDA,etu.Prenom,etu.Nom,etu.Courriel,etu.Photo,etu.Etat,StageEtudiant.NumeroChoix,StageEtudiant.ChoixFinal FROM StageEtudiant 
left join Etudiant etu ON StageEtudiant.IDEtudiant = etu.IDEtudiant 
left join Programme prog on etu.IDProgramme = prog.IDProgramme
WHERE IDStage = @IdStage_IN AND StageEtudiant.Etat = 1
order by StageEtudiant.NumeroChoix
GO

CREATE PROC pAddSetOneAssignationStage(@IDStage_IN INT, @ListEtudiants_IN VARCHAR(200))
AS
    DECLARE @List AS TABLE(value varchar)

    INSERT INTO @List
    select value from STRING_SPLIT(@ListEtudiants_IN,',')

    UPDATE StageEtudiant
    SET ChoixFinal = 1
    WHERE IDStage = @IDStage_IN
    AND IDEtudiant IN (select value from @List)

    UPDATE StageEtudiant
    SET ChoixFinal = 0
    WHERE IDStage = @IDStage_IN
    AND IDEtudiant NOT IN (select value from @List)
GO