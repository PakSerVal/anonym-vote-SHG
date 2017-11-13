using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SHG.Models.Api.Input
{
    public class SaveBulletinFilter
    {
        public int UserId { get; set; }

        public string Data { get; set; }

        public string Signature { get; set; }

        public string SignaturePubExponent { get; set; }

        public string SignatureModulus { get; set; }
    }
}
