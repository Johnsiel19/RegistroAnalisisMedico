using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoFinalAplicadaI.Entidades
{
    public class Analisis
    {
        [Key]
        public int AnalisisId { get; set;}

        public DateTime FechaAnalisis { get; set; }
        public int UsuarioId  { get; set; }

        public virtual List<AnalisisDetalle> Resultado { get; set; }


        public Analisis()
        {
            AnalisisId = 0;
            UsuarioId = 0;
            FechaAnalisis = DateTime.Now;
            Resultado = new List<AnalisisDetalle>();

        }
    }
}
