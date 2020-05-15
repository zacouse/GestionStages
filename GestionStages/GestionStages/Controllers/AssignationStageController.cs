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
        Repositories.IPersonneContactRepository repoPersonneContact;

        public AssignationStageController(IConfiguration configuration) : base()
        {
            repoStage = new Repositories.repoStageMSSQL(configuration);
            repoEtudiant = new Repositories.repoEtudiantMSSQL(configuration);
            repoChoixStage = new Repositories.repoChoixStageEtudiant(configuration);
            repoPersonneContact = new Repositories.repoPersonneContactMSSQL(configuration);
        }

        public IActionResult AddSetAssignationStage()
        {
            List <AssignationStageEtudiant> lesStages = new List<AssignationStageEtudiant>();
            List<Stage> stagesToAssign = repoStage.GetStagesForAssignement();
            foreach(Stage stage in stagesToAssign)            {                List<ChoixEtudiant> stageEtudiants = repoChoixStage.GetChoixEtudiant(stage.IDStage);                lesStages.Add(new AssignationStageEtudiant(stageEtudiants, stage, repoPersonneContact.GetPersonneContactByStageID(stage.IDStage)));            }
            ViewBag.lesStages = lesStages;
            ViewBag.lesPersonnesContact = repoPersonneContact.GetAllActivePersonneContact();
            ViewBag.lesEtudiants = repoEtudiant.GetAllEtudiants();
            return View();
        }

        [HttpPost]
        public void SaveOneStage(string )        {
            //SaveStage(txtIDStage);            List<ChoixEtudiant> ChoixEtudiants = repoChoixStage.GetChoixEtudiant(txtIDStage);            foreach (ChoixEtudiant choix in ChoixEtudiants)
            {
                repoChoixStage.SaveOneAssignationStage(txtIDStage, choix.Etudiant.IDEtudiant, Convert.ToInt32(Request.Form["DropSuperviseur" + txtIDStage + "-" + choix.Etudiant.IDEtudiant]), Request.Form["FinalChoice" + txtIDStage + "-" + choix.Etudiant.IDEtudiant] == "on");
            }            Response.Redirect("../AssignationStage/AddSetAssignationStage");        }

        [HttpPost]
        public void SaveAllStage()        {            List<Stage> stagesToAssign = repoStage.GetStagesForAssignement();
            foreach (Stage stage in stagesToAssign)            {
                SaveStage(stage.IDStage);            }            Response.Redirect("../AssignationStage/AddSetAssignationStage");        }

        private void SaveStage(int idStage)
        {
            List<ChoixEtudiant> ChoixEtudiants = repoChoixStage.GetChoixEtudiant(idStage);            foreach (ChoixEtudiant choix in ChoixEtudiants)
            {
                repoChoixStage.SaveOneAssignationStage(idStage, choix.Etudiant.IDEtudiant, Convert.ToInt32(Request.Form["DropSuperviseur" + idStage + "-" + choix.Etudiant.IDEtudiant]), Request.Form["FinalChoice" + idStage + "-" + choix.Etudiant.IDEtudiant] == "on");
            }
        }
    }
}