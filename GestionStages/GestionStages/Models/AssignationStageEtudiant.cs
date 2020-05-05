using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GestionStages.Models
{
    public class AssignationStageEtudiant
    {
        public List<ChoixEtudiant> LesChoixEtudiants { get; set; }
        public PersonneContact Superviseur { get; set; }

        private Stage stage;

        public AssignationStageEtudiant()
        {
            LesChoixEtudiants = new List<ChoixEtudiant>();
            stage = new Stage();
            Superviseur = new PersonneContact();
        }

        public AssignationStageEtudiant(List<ChoixEtudiant> lesChoixEtudiants, Stage stage, PersonneContact superviseur)
        {
            LesChoixEtudiants = lesChoixEtudiants;
            this.stage = stage;
            Superviseur = superviseur;
        }

        public int getIDStage()
        {
            return stage.IDStage;
        }

        public void setIDStage(int inID)
        {
            stage.IDStage = inID;
        }

        public string getTitreStage()
        {
            return stage.Titre;
        }

        public int getNbPostesStage()
        {
            return stage.NbPostes;
        }

        public string getTitreMilieuStage()
        {
            return stage.TitreMilieuStage;
        }
    }
}
