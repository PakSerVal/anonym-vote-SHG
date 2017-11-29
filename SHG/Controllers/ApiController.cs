using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SHG.Utils;
using System.Security.Cryptography;
using System.Text;
using SHG.Models.Api.Input;
using SHG.Data;
using SHG.Models;
using Org.BouncyCastle.Math;
using SHG.Models.Entites;

namespace SHG.Controllers
{
    [Route("api/shg")]
    public class ApiController : Controller
    {
        private Config config;
        private ElectContext electContext;

        public ApiController(Config config, ElectContext electContext)
        {
            this.config = config;
            this.electContext = electContext;
        }

        [HttpPost("save-bulletin")]
        public IActionResult SaveBulletin([FromBody] SaveBulletinFilter filter)
        {
            if (ModelState.IsValid)
            {
                DigitalSignature ds = new DigitalSignature();
                if (ds.Verify(filter.Data, filter.Signature, filter.SignatureModulus, filter.SignaturePubExponent))
                {
                    Bulletins bulletinModel = new Bulletins(config, electContext);
                    if (bulletinModel.saveBulletin(filter.UserLik, filter.Data))
                    {
                        return Ok();
                    }
                }
            }
            return BadRequest();
        }

        [HttpPost("send-to-mix")]
        public IActionResult SendBulletinsToMix()
        {
            Bulletins bulletinModel = new Bulletins(config, electContext);
            if (bulletinModel.sendBulletinsToMix())
                return Ok();
            return BadRequest();
        }

        [HttpGet("bulletin-board")]
        public IActionResult BulletinBoard()
        {
            List<Bulletin> bulletins = electContext.Bulletins.ToList();
            var LikHashList = new List<Object>();
            HashAlgorithm algorithm = SHA1.Create();
            foreach (Bulletin bulletin in bulletins)
            {
                StringBuilder sb = new StringBuilder();
                foreach (byte b in algorithm.ComputeHash(Encoding.UTF8.GetBytes(bulletin.Data)))
                    sb.Append(b.ToString("X2"));
                LikHashList.Add(new
                {
                    LIK = bulletin.UserLik,
                    VoteHash = sb.ToString()
                }
                );
            }
            return View("BulletinBoard");
        }
    }
}
