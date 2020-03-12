using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GestionStages.Models
{
    public class Etudiant : Personne
    {
        public int IDEtudiant { get; set; }
        public int NoDA { get; set; }
        public string Programme { get; set; }
        public Byte[] Photo { get; set; }

        public Etudiant()
        {
            IDEtudiant = 0;
            Nom = "";
            Prenom = "";
            NoDA = 0;
            Courriel = "";
            Programme = "";
            Photo = null;
        }

        public Etudiant(int id,string prenom,string nom,int noDA,string email,string progamme,byte[] photo)
        {
            IDEtudiant = id;
            Prenom = prenom;
            Nom = nom;
            NoDA = noDA;
            Courriel = email;
            Programme = progamme;
            Photo = photo;
        }
    }
}
