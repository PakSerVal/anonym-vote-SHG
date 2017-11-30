using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SHG.Models;
using SHG.Data;
using SHG.Models.Api.Input;

namespace SHG.Controllers
{
    public class MixController: Controller
    {
        private Config config;
        private ElectContext electContext;

        public MixController(Config config, ElectContext electContext)
        {
            this.config = config;
            this.electContext = electContext;
        }

        [HttpGet("finish-election")]
        public ViewResult FinishElection()
        {
            return View("AdminForm");
        }

        [HttpPost("send-to-mix")]
        public IActionResult SendBulletinsToMix([FromForm] SendBulletinsToMixFilter filter)
        {
            if (ModelState.IsValid)
            {
                if (filter.AdminLogin == config.ADMIN_LOGIN && filter.AdminPassword == config.ADMIN_PASSWORD)
                {
                    Bulletins bulletinModel = new Bulletins(config, electContext);
                    if (bulletinModel.sendBulletinsToMix())
                    {
                        ViewData["message"] = "Buletins have been sent";
                        return View("AdminNotifications");
                    }
                    ViewData["error"] = "Mix channel is unavailable";
                    return View("AdminForm");
                }
            }
            ViewData["error"] = "Invalid login or password";
            return View("AdminForm");
        }
    }
}
