using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GestionStages.Models;

namespace GestionStages.Repositories
{
    public abstract class Repository
    {
        public abstract List<MilieuStage> getAllMilieuStage();
        public abstract List<Etudiant> getAllEtudiants();
    }
}
