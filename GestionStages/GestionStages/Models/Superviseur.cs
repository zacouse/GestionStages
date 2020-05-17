using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GestionStages.Models
{
    public class Superviseur : Personne
    {
        public int IDSuperviseur { get; set; }

        public Superviseur(int iDSuperviseur, string nom,string prenom,string courriel,bool etat)
            :base(nom,prenom,courriel,etat)
        {
            IDSuperviseur = iDSuperviseur;
        }

        public Superviseur()
            :base()
        {
            IDSuperviseur = 0;
        }
    }
}
