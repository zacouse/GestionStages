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
        Repositories.IMilieuStageRepository repoMilieuStage;
        Repositories.IStageRepository repoStage;
        Repositories.IEtudiantRepository repoEtudiant;

        public AssignationStageController(IConfiguration configuration) : base()
        {
            repoMilieuStage = new Repositories.repoMilieuStageMSSQL(configuration);
            repoStage = new Repositories.repoStageMSSQL(configuration);
            repoEtudiant = new Repositories.repoEtudiantMSSQL(configuration);
        }

        public IActionResult AddSetAssignationStage()
        {
            return View();
        }
    }
}