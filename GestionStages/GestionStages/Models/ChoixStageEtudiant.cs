using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GestionStages.Models
{
    public class ChoixStageEtudiant
    {
        public int IDChoixStageEtudiant { get; set; }
        //public int IDStage { get; set; }
        public int IDEtudiant { get; set; }
        public int NumeroChoix { get; set; }
        public bool ChoixFinal { get; set; }
        public bool Etat { get; set; }
        public List<ChoixEtudiant> LesChoixEtudiants { get; set; }

        private Stage stage;

        public ChoixStageEtudiant()
        {
            IDChoixStageEtudiant = 0;
            stage = new Stage();
            IDEtudiant = 0;
            NumeroChoix = 0;
            ChoixFinal = false;
            Etat = false;
        }

        public ChoixStageEtudiant(int idchoix, int idetudiant, int numeroChoix,bool choixFinal, bool etat,Stage inStage)
        {
            IDChoixStageEtudiant = idchoix;
            stage = inStage;
            IDEtudiant = idetudiant;
            NumeroChoix = numeroChoix;
            ChoixFinal = choixFinal;
            Etat = etat;
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
