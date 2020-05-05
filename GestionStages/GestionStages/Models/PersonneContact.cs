using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GestionStages.Models
{
    public class PersonneContact : Personne
    {
        public int IDPersonneContact { get; set; }

        public PersonneContact(int iDPersonneContact,string nom,string prenom,string courriel,bool etat)
            :base(nom,prenom,courriel,etat)
        {
            IDPersonneContact = iDPersonneContact;
        }

        public PersonneContact()
            :base()
        {
            IDPersonneContact = 0;
        }
    }
}
