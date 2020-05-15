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
        List<MilieuStage> GetMilieuStage(string titre, string address, bool chkIsActive, bool chkIsInactive);
    }

    interface IStageRepository
    {
        List<Stage> GetAllStage();
        List<Stage> GetAllStageActif();
        List<Stage> GetAllStageInactif();
        void SaveStage(Stage stage, string idRestriction);
        Stage GetStageByID(int stageId);
        List<Stage> GetStage(string titre, string descr,string milieu,int minh,int maxh,string minDate,string maxDate, bool chkIsJour, bool chkIsSoir, bool chkIsNuit, bool chkIsActive, bool chkIsInactive,string chosenStages);
        List<Stage> GetStagesByIdMilieu(int milieu);
        List<Stage> GetStagesForAssignement();

        
    }

    interface IRestrictionRepository
    {
        List<Restriction> GetRestrictions(string titre,string descr, bool chkIsActive, bool chkIsInactive);
        List<Restriction> GetAllRestriction();
        Restriction GetRestrictionByID(int id);
        List<Restriction> GetAllStageRestrictionByIdStage(int idStage);
        List<Restriction> GetAllMilieuStageRestrictionByIdMilieuStage(int idMilieuStage);
        void SaveRestriction(int id, string titre,string descr,bool etat);
        List<int> GetRestrictionIDFromStageID(int StageId);
        List<int> GetRestrictionIDFromMilieuStageID(int MilieuStageId);
        List<Restriction> GetAllRestrictionForStageWithMilieuIncludedByIds(int idStage, int idMilieuStage);
    }

    interface IEtudiantRepository
    {
        List<Etudiant> GetAllEtudiants();
    }

    interface IChoixStageEtudiantRepository
    {
        void SaveChoixStage(int IDChoixStageEtudiant, int IDStage, int IDEtudiant, int NumeroChoix, bool ChoixFinal, bool Etat);
        void RemoveChoixStage(int idEtudiant, int numeroChoix);
        List<ChoixStageEtudiant> GetChoixStage(string idEtudiant);
        List<ChoixEtudiant> GetChoixEtudiant(int idStage);
        void SaveOneAssignationStage(int IDStage, string listEtudiants,int idSuperviseur);
        void SaveOneAssignationStage(int IDStage, int idEtudiant,int idSuperviseur,bool choixFinal);
    }

    interface IPersonneContactRepository
    {
        List<PersonneContact> GetAllActivePersonneContact();
        PersonneContact GetPersonneContactByStageID(int stageID);
    }
}
