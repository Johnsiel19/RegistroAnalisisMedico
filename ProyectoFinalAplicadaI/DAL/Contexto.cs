using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProyectoFinalAplicadaI.Entidades;
using System.Data.Entity;

namespace ProyectoFinalAplicadaI.DAL
{
    public class Contexto : DbContext
    {
        public DbSet<Usuarios> Usuarios { get; set; }

        public DbSet<Cargos> Cargos { get; set; }

        public DbSet<TipoAnalisis> TipoAnalisis { get; set; }

        public DbSet<Analisis> Analisis { get; set; }


        public Contexto() : base("ConStr")
        { }

    }
}
