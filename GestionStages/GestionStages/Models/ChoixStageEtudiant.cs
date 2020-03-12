using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GestionStages.Models
{
    public class ChoixStageEtudiant
    {
        public int IDChoixStageEtudiant { get; set; }
        public int IDStage { get; set; }
        public int IDEtudiant { get; set; }
        public int Ordre { get; set; }
        public int Etat { get; set; }

        public ChoixStageEtudiant()
        {
            IDChoixStageEtudiant = 0;
            IDStage = 0;
            IDEtudiant = 0;
            Ordre = 0;
            Etat = 0;
        }

        public ChoixStageEtudiant(int idchoix,int idstage, int idetudiant, int ordre, int etat)
        {
            IDChoixStageEtudiant = idchoix;
            IDStage = idstage;
            IDEtudiant = idetudiant;
            Ordre = ordre;
            Etat = etat;
        }
    }
}
