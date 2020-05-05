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
        void SaveMilieuStage(MilieuStage milieu, string idRestriction);
        MilieuStage GetMilieuStageById(int id);
        List<MilieuStage> GetMilieuStage(string titre, string address);
    }

    interface IStageRepository
    {
        List<Stage> GetAllStage();
        List<Stage> GetAllStageActif();
        List<Stage> GetAllStageInactif();
        void SaveStage(Stage stage, string idRestriction);
        Stage GetStageByID(int stageId);
        List<Stage> GetStage(string titre, string descr,string milieu,int minh,int maxh,string minDate,string maxDate, bool chkIsJour, bool chkIsSoir, bool chkIsNuit, bool chkIsActive, bool chkIsInactive);
        List<Stage> GetStagesByIdMilieu(int milieu);
        void SaveChoixStage(int IDChoixStageEtudiant, int IDStage, int IDEtudiant, int NumeroChoix, bool ChoixFinal, bool Etat);
        void RemoveChoixStage(int idEtudiant, int numeroChoix);
        List<ChoixStageEtudiant> getChoixStage(string idEtudiant);
    }

    interface IRestrictionRepository
    {
        List<Restriction> GetRestrictions(string titre,string descr);
        List<Restriction> GetAllRestriction();
        Restriction GetRestrictionByID(int id);
        List<Restriction> GetAllStageRestrictionByIdStage(int idStage);
        List<Restriction> GetAllMilieuStageRestrictionByIdMilieuStage(int idMilieuStage);
        void SaveRestriction(int id, string titre,string descr,bool etat);
        List<int> GetRestrictionIDFromStageID(int StageId);
        List<int> GetRestrictionIDFromMilieuStageID(int MilieuStageId);
    }

    interface IEtudiantRepository
    {
        List<Etudiant> GetAllEtudiants();
    }
}
