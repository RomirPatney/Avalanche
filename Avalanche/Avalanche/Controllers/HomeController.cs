﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Avalanche.Models;
using Avalanche.Models.ModelViewDataTables;
using Avalanche.Services;
//using ArkNet;
//using ArkNet.Utils.Enum;

namespace Avalanche.Controllers
{
    public class HomeController : Controller
    {
        public async Task<IActionResult> Index()
        {
            //await ArkNetApi.Instance.Start(NetworkType.DevNet);
            //var obj = new APIcaller_Service();1
            //var x = obj.GetRequestArk();
            //var y = obj.PostRequestArk("12,55,23");
            return View();
        }

        public IActionResult PullDataForm()
        {
            return View();
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult DataLog(PullDataForm model)
        {
            //model.Frequency;
            //model.ValueOption;

            //ViewBag.Posts = APIcaller_Service.(context);

            return View();
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
