using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SHG.Models.Entites;
using SHG.Data;
using System.Security.Cryptography;
using System.Text;

namespace SHG.Controllers
{
    public class BulletinBoardController: Controller
    {
        private ElectContext electContext;

        public BulletinBoardController(ElectContext electContext)
        {
            this.electContext = electContext;
        }

        [HttpGet("bulletin-board")]
        public IActionResult BulletinBoard()
        {
            List<Bulletin> bulletins = electContext.Bulletins.ToList();
            Dictionary<string, string> LikHashDict = new Dictionary<string, string>();
            HashAlgorithm algorithm = SHA1.Create();
            foreach (Bulletin bulletin in bulletins)
            {
                StringBuilder sb = new StringBuilder();
                foreach (byte b in algorithm.ComputeHash(Encoding.UTF8.GetBytes(bulletin.Data)))
                    sb.Append(b.ToString("X2"));
                LikHashDict.Add(bulletin.UserLik, sb.ToString());
            }
            return View("BulletinBoard", LikHashDict);
        }
    }
}
