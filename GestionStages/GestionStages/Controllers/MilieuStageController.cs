﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace GestionStages.Controllers
{
    public class MilieuStageController : HomeController
    {
        public IActionResult ListeMilieuStage()
        {
            ViewBag.lesMilieus = repo.getAllMilieuStage();
            return View();
        }
    }
}