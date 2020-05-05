using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GestionStages.Models
{
    public class ChoixEtudiant
    {
        public Etudiant Etudiant { get; set; }
        public string NoChoix { get; set; }
        public bool ChoixFinal { get; set; }

        public ChoixEtudiant()
        {
            Etudiant = new Etudiant();
            NoChoix = "";
            ChoixFinal = false;
        }
        public ChoixEtudiant(Etudiant etudiant, string noChoix, bool choixFinal)
        {
            Etudiant = etudiant;
            NoChoix = noChoix;
            ChoixFinal = choixFinal;
        }
    }
}
