using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoFinalAplicadaI.Entidades
{
    public class Cargos
    {
        [Key]
        public int CargoId { get; set; }

        public string Descripcion { get; set; }

        public Cargos()
        {
            CargoId = 0;
            Descripcion = String.Empty; ;
        }
    }
}
