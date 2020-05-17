using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GestionStages.Models
{
    public class Personne
    {
        public string Nom { get; set; }
        public string Prenom { get; set; }
        public string Courriel { get; set; }
        public bool Etat { get; set; }
        public Personne()
        {
            Nom = "";
            Prenom = "";
            Courriel = "";
            Etat = false;
        }

        public Personne(string nom, string prenom, string courriel, bool etat)
        {
            Nom = nom;
            Prenom = prenom;
            Courriel = courriel;
            Etat = etat;
        }
    }
}
