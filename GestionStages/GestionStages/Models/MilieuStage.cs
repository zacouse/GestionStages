using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GestionStages.Models
{
    public class MilieuStage
    {
        public int IDMilieuStage { get; set; }
        public string Titre { get; set; }
        public string NoCivique { get; set; }
        public string Rue { get; set; }
        public string CodePostal { get; set; }
        public string Province { get; set; }
        public string Pays { get; set; }
        public string NoTelephone { get; set; }
        public int Etat { get; set; }

        public MilieuStage()
        {
            IDMilieuStage = 0;
            Titre = "";
            NoCivique = "";
            Rue = "";
            CodePostal = "";
            Province = "";
            Pays = "";
            NoTelephone = "";
            Etat = 0;
        }

        public MilieuStage(int idmilieu,string titre, string nocivique, string rue, string codepostal, string province, string pays, string notelephone, int etat)
        {
            IDMilieuStage = idmilieu;
            Titre = titre;
            NoCivique = nocivique;
            Rue = rue;
            CodePostal = codepostal;
            Province = province;
            Pays = pays;
            NoTelephone = notelephone;
            Etat = etat;
        }
    }
}
