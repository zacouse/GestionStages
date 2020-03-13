using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GestionStages.Models;

namespace GestionStages.Repositories
{
    public abstract class Repository
    {
        public abstract List<MilieuStage> GetAllMilieuStage();
        public abstract List<Etudiant> GetAllEtudiants();
        public abstract Stage pAddSetStage(int idStage, int idMilieuStage, string titre, string description, int nbPoste, int statut, int periodeTravail, int nbHeureSemaine, DateTime dateDebut, DateTime dateFin, bool etat, DateTime dateHeureCreation);
    }
}
