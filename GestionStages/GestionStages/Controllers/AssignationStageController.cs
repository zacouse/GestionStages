using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GestionStages.Models;
using GestionStages.Properties;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace GestionStages.Controllers
{
    public class AssignationStageController : Controller
    {
        Repositories.IStageRepository repoStage;
        Repositories.IEtudiantRepository repoEtudiant;
        Repositories.IChoixStageEtudiantRepository repoChoixStage;
        Repositories.ISuperviseurRepository repoSuperviseur;

        public AssignationStageController(IConfiguration configuration) : base()
        {
            repoStage = new Repositories.repoStageMSSQL(configuration);
            repoEtudiant = new Repositories.repoEtudiantMSSQL(configuration);
            repoChoixStage = new Repositories.repoChoixStageEtudiant(configuration);
            repoSuperviseur = new Repositories.repoSuperviseurMSSQL(configuration);
        }

        public IActionResult AddSetAssignationStage()
        {
            List<AssignationStageEtudiant> lesStages = new List<AssignationStageEtudiant>();
            List<Stage> stagesToAssign = repoStage.GetStagesForAssignement();
            foreach (Stage stage in stagesToAssign)            {                List<ChoixEtudiant> stageEtudiants = repoChoixStage.GetChoixEtudiant(stage.IDStage);                lesStages.Add(new AssignationStageEtudiant(stageEtudiants, stage));            }
            ViewBag.lesStages = lesStages;
            ViewBag.lesPersonnesContact = repoSuperviseur.GetAllActiveSuperviseur();
            ViewBag.lesEtudiants = repoEtudiant.GetAllEtudiants();
            return View();
        }

        [HttpPost]
        public string SaveOneStage(int idStageEtudiant, bool ChoixFinal, int IDSuperviseur)        {
            string message = "";
            if (IDSuperviseur != 0)
            {
                repoChoixStage.SaveOneAssignationStage(idStageEtudiant, ChoixFinal, IDSuperviseur);
            }
            else
            {
                message = "error";
            }            return message;        }

        [HttpPost]
        public string SaveOneVeto(int idStage,int idEtudiant, bool ChoixFinal, int IDSuperviseur)        {
            string message = "";
            if (IDSuperviseur != 0)
            {
                repoChoixStage.SaveOneVeto(idStage, idEtudiant, ChoixFinal, IDSuperviseur);
            }
            else
            {
                message = "error";
            }
            return message;
        }
    }
}