using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SHG.Models.Api.Input
{
    public class SendBulletinsToMixFilter
    {
        [Required]
        public string AdminLogin { get; set; }

        [Required]
        public string AdminPassword { get; set; }

    }
}
