using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SHG.Models.Entites;

namespace SHG.Data
{
    public class ElectContext : DbContext
    {
        public ElectContext(DbContextOptions<ElectContext> options) : base(options) {}

        public DbSet<Bulletin> Bulletins { get; set; }
    }
}

