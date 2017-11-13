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

namespace SHG.Controllers
{
    [Route("api/shg")]
    public class ApiController : Controller
    {
        private ElectContext electContext;

        public ApiController(ElectContext electContext)
        {
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
                    Bulletins bulletinModel = new Bulletins(electContext);
                    if (bulletinModel.saveBulletin(filter.UserId, filter.Data))
                    {
                        return Ok();
                    }
                }
            }
            return BadRequest();
        }
    }
}
