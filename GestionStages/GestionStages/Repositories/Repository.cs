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
        public abstract List<Stage> GetAllStage();
    }
}
