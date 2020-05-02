using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GestionStages.Models
{
    public class Restriction
    {
        public int IDRestriction { get; set; }
        public string Titre { get; set; }
        public string Description { get; set; }
        public bool Etat { get; set; }
    }
}
