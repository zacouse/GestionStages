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
        void SaveMilieuStage(MilieuStage milieu);
        MilieuStage GetMilieuStageById(int id);
        List<MilieuStage> GetMilieuStage(string titre, string descr, string address);
    }

    interface IStageRepository
    {
        List<Stage> GetAllStage();
        List<Stage> GetAllStageActif();
        List<Stage> GetAllStageInactif();
        void SaveStage(Stage stage);
        Stage GetStageByID(int stageId);
        List<Stage> GetStage(string titre, string descr,string milieu,int minh,int maxh,string minDate,string maxDate, bool chkIsJour, bool chkIsSoir, bool chkIsNuit, bool chkIsActive, bool chkIsInactive);
        List<Stage> GetStagesByIdMilieu(int milieu);
    }

    interface IEtudiantRepository
    {
        List<Etudiant> GetAllEtudiants();
    }
}
