using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPI.Models;

namespace WebAPI.Data
{
    public class DataContext : DbContext
    {

        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {

        }

        public DbSet<Filme> Filmes { get; set; }
        public DbSet<Cliente> Clientes { get; set; }
        public DbSet<Locacoes> Locacoes { get; set; }
    }
}
