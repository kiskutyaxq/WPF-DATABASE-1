using ECDL.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECDL
{
    public class DatabaseContext : DbContext
    {
        public DbSet<VizsgatipusModel> vizsgazotipusok { get; set; }
        public DbSet<VizsgazoModel> vizsgazok { get; set; }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseMySql("Server=localhost;Database=ecdl;Uid=root;Pwd=;", ServerVersion.AutoDetect("Server=localhost;Database=ecdl;Uid=root;Pwd=;"));
        }
    }
}
