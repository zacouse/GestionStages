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
        public bool Etat { get; set; }

        public ChoixStageEtudiant()
        {
            IDChoixStageEtudiant = 0;
            IDStage = 0;
            IDEtudiant = 0;
            Ordre = 0;
            Etat = false;
        }

        public ChoixStageEtudiant(int idchoix,int idstage, int idetudiant, int ordre, bool etat)
        {
            IDChoixStageEtudiant = idchoix;
            IDStage = idstage;
            IDEtudiant = idetudiant;
            Ordre = ordre;
            Etat = etat;
        }
    }
}
