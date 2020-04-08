using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GestionStages.Models;

namespace GestionStages.Repositories
{
    interface IMilieuStageRepository
    {
        List<MilieuStage> GetAllMilieuStage();
        List<MilieuStage> GetMilieuStage(string titre, string descr, string address);
    }

    interface IStageRepository
    {
        List<Stage> GetAllStage();
        List<Stage> GetStage(string titre, string descr);
    }

    interface IEtudiantRepository
    {
        List<Etudiant> GetAllEtudiants();
    }
}
