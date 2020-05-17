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

CREATE PROC pAddSetDroitVeto(@IDStage_IN INT,@IDEtudiant_IN INT, @ChoixFinal_IN BIT, @IDSuperviseur_IN INT)
AS
IF NOT EXISTS(SELECT * FROM StageEtudiant WHERE IDStage = @IDStage_IN AND IDEtudiant = @IDEtudiant_IN)
BEGIN
    INSERT INTO StageEtudiant(IDStage,IDEtudiant,ChoixFinal,NumeroChoix,IDSuperviseur,Etat)
    VALUES(@IDStage_IN,@IDEtudiant_IN,@ChoixFinal_IN,0,@IDSuperviseur_IN,1)
END
GO