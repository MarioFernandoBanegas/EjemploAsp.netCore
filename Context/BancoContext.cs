using AAAAMMDDMFBO0801199707477.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AAAAMMDDMFBO0801199707477.Context
{
    public class BancoContext : DbContext
    {
        public BancoContext(DbContextOptions<BancoContext> options) : base(options)
        {

        }

        public DbSet<Clientes> clientes { get; set; }
        public DbSet<Cuentas> cuentas { get; set; }

        public DbSet<AAAAMMDDMFBO0801199707477.Models.Cuentas> Cuentas { get; set; }

    }
}
