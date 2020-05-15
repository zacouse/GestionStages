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
        public void SaveOneStage(int idStageEtudiant, bool ChoixFinal, int IDSuperviseur)        {
            //repoChoixStage.SaveOneAssignationStage(txtIDStage, choix.Etudiant.IDEtudiant, Convert.ToInt32(Request.Form["DropSuperviseur" + txtIDStage + "-" + choix.Etudiant.IDEtudiant]), Request.Form["FinalChoice" + txtIDStage + "-" + choix.Etudiant.IDEtudiant] == "on");
            repoChoixStage.SaveOneAssignationStage(idStageEtudiant, ChoixFinal, IDSuperviseur);
            Response.Redirect("../AssignationStage/AddSetAssignationStage");        }

        [HttpPost]
        public void SaveAllStage()        {            List<Stage> stagesToAssign = repoStage.GetStagesForAssignement();
            foreach (Stage stage in stagesToAssign)            {
                List<ChoixEtudiant> ChoixEtudiants = repoChoixStage.GetChoixEtudiant(stage.IDStage);
                foreach (ChoixEtudiant choix in ChoixEtudiants)
                {
                    //repoChoixStage.SaveOneAssignationStage(stage.IDStage, choix.Etudiant.IDEtudiant, Convert.ToInt32(Request.Form["DropSuperviseur" + choix.IDStageEtudiant]), Request.Form["FinalChoice" + choix.IDStageEtudiant] == "on");
                    repoChoixStage.SaveOneAssignationStage(choix.IDStageEtudiant, Request.Form["FinalChoice" + choix.IDStageEtudiant] == "on", Convert.ToInt32(Request.Form["DropSuperviseur" + choix.IDStageEtudiant]));
                }            }            Response.Redirect("../AssignationStage/AddSetAssignationStage");        }
    }
}