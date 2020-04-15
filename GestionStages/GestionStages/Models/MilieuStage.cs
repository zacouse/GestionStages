using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace GestionStages.Models
{
    public class MilieuStage
    {
        public int IDMilieuStage { get; set; }
        public string Titre { get; set; }
        public string Description { get; set; }
        public string NoCivique { get; set; }
        public string Rue { get; set; }
        public string CodePostal { get; set; }
        public string Ville { get; set; }
        public string Province { get; set; }
        public string Pays { get; set; }
        public string NoTelephone { get; set; }
        public bool Etat { get; set; }
        public SelectList listTitreMilieu { get; set; }
        public int selectedTitreMilieu { get; set; }
        public MilieuStage()
        {
            IDMilieuStage = 0;
            Titre = "";
            Description = "";
            NoCivique = "";
            Rue = "";
            CodePostal = "";
            Ville = "";
            Province = "";
            Pays = "";
            NoTelephone = "";
            Etat = false;
        }

        public MilieuStage(int idmilieu,string titre,string description, string nocivique, string rue, string codepostal, string ville, string province, string pays, string notelephone, bool etat)
        {
            IDMilieuStage = idmilieu;
            Titre = titre;
            Description = description;
            NoCivique = nocivique;
            Rue = rue;
            CodePostal = codepostal;
            Ville = ville;
            Province = province;
            Pays = pays;
            NoTelephone = notelephone;
            Etat = etat;
        }
    }
}
