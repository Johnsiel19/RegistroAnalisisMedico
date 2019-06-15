using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoFinalAplicadaI.Entidades
{
   public class AnalisisDetalle
    {


        [Key]

        public int AnalisisDetalleId { get; set; }

        public int AnalisisId { get; set; }

        public int TipoAnalisisId { get; set; }

        public string Resultado { get; set; }



        public AnalisisDetalle()
        {
            AnalisisDetalleId = 0;
            AnalisisId = 0;
            TipoAnalisisId = 0;
            Resultado = String.Empty;
        }


        public AnalisisDetalle(int analisisid, int tipoanalisisid, string resultado)
        {


            AnalisisId = analisisid;
            TipoAnalisisId = tipoanalisisid;
            Resultado = resultado;


        }

        //Constructor Con parametros con ID
        public AnalisisDetalle(int id, int analisisid, int tipoanalisisid, string resultado)
        {
            AnalisisDetalleId = id;
            AnalisisId = analisisid;
            TipoAnalisisId = tipoanalisisid;
            Resultado = resultado;
        }


    }
}
