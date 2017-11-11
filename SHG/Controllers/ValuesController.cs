using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SHG.Utils;
using System.Security.Cryptography;
using System.Text;
using SHG.Models.Filters;

namespace SHG.Controllers
{
    [Route("api/[controller]")]
    public class ValuesController : Controller
    {
        // GET api/values
        [HttpGet]
        public IActionResult Get()
        {
            return Ok();
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody] IncomeVote inVote)
        {
            if (ModelState.IsValid)
            {
                string n = "76f71678c1a460d9748fc711d5e9987cff8c9a9d9eeb4226cae33709ff12b5888fcb92962be020c7acd09d97ccffe1c5ed2b8c5528345d44c5590cdcf4c824fddd9fbeeb5c626d3fc760b7ae0e576a720acf9de7a4e2c9d8c4fe92f0c76e4b74d6141bfd76af13091a1413693d735975e704731f6b81ea948f0ec3a91f515b2b";
                string e = "10001";

                DigitalSignature dg = new DigitalSignature();

                bool k = dg.Verify(inVote.Data, inVote.Signature, n, e);
            }
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
        public static byte[] StringToByteArray(string hex)
        {
            return Enumerable.Range(0, hex.Length)
                             .Where(x => x % 2 == 0)
                             .Select(x => Convert.ToByte(hex.Substring(x, 2), 16))
                             .ToArray();
        }
    }
}
