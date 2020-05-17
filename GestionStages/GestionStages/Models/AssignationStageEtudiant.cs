using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GestionStages.Models
{
    public class AssignationStageEtudiant
    {
        public List<ChoixEtudiant> LesChoixEtudiants { get; set; }

        private Stage stage;

        public AssignationStageEtudiant()
        {
            LesChoixEtudiants = new List<ChoixEtudiant>();
            stage = new Stage();
        }

        public AssignationStageEtudiant(List<ChoixEtudiant> lesChoixEtudiants, Stage stage)
        {
            LesChoixEtudiants = lesChoixEtudiants;
            this.stage = stage;
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
