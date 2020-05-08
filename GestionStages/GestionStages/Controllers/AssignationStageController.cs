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
            foreach(Stage stage in stagesToAssign)
            {
                List<ChoixEtudiant> stageEtudiants = repoChoixStage.GetChoixEtudiant(stage.IDStage);
                lesStages.Add(new AssignationStageEtudiant(stageEtudiants, stage, repoPersonneContact.GetPersonneContactByStageID(stage.IDStage)));
            }
            ViewBag.lesStages = lesStages;
            ViewBag.lesPersonnesContact = repoPersonneContact.GetAllActivePersonneContact();
            ViewBag.lesEtudiants = repoEtudiant.GetAllEtudiants();
            return View();
        }

        public void SaveOneStage(int txtIDStage,string chkEtudiant,int TxtSuperviseur)
        {
            repoChoixStage.SaveOneAssignationStage(txtIDStage, "", TxtSuperviseur);
            Response.Redirect("../AssignationStage/AddSetAssignationStage");
        }
    }
}