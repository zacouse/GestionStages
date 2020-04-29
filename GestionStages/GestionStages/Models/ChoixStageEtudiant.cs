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
        public int NumeroChoix { get; set; }
        public bool ChoixFinal {get;set;} 
        public bool Etat { get; set; }

        public ChoixStageEtudiant()
        {
            IDChoixStageEtudiant = 0;
            IDStage = 0;
            IDEtudiant = 0;
            NumeroChoix = 0;
            ChoixFinal = false;
            Etat = false;
        }

        public ChoixStageEtudiant(int idchoix,int idstage, int idetudiant, int numeroChoix,bool choixFinal, bool etat)
        {
            IDChoixStageEtudiant = idchoix;
            IDStage = idstage;
            IDEtudiant = idetudiant;
            NumeroChoix = numeroChoix;
            ChoixFinal = choixFinal;
            Etat = etat;
        }
    }
}
