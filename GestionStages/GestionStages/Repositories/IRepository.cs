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
    }

    interface IStageRepository
    {
        List<Stage> GetAllStage();
        List<Stage> GetAllStageWithMilieuTitre();
        List<Stage> GetAllStageActif();
        List<Stage> GetAllStageInactif();
        void SaveStage(Stage stage);
        Stage GetStageByID(int stageId);
        Stage GetMilieuStageForStage(int stageId);
    }

    interface IEtudiantRepository
    {
        List<Etudiant> GetAllEtudiants();
    }
}
