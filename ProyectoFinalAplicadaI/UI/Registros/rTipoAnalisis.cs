using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ProyectoFinalAplicadaI.BLL;
using ProyectoFinalAplicadaI.Entidades;
using ProyectoFinalAplicadaI.DAL;

namespace ProyectoFinalAplicadaI.UI.Registros
{
    public partial class rTipoAnalisis : Form
    {
        public rTipoAnalisis()
        {
            InitializeComponent();
        }


        private void Limpiar()
        {
            UbicacionIdnumericUpDown.Value = 0;
            DescripciontextBox.Text = string.Empty;

        }

        public TipoAnalisis LlenaClase()
        {
            TipoAnalisis tipoanalisis = new TipoAnalisis();
            tipoanalisis.TipoAnalisisId = Convert.ToInt32(UbicacionIdnumericUpDown.Value);
            tipoanalisis.Descripcion = DescripciontextBox.Text;

            return tipoanalisis;
        }

        private void LlenaCampo(TipoAnalisis tipoanalisis)
        {
            UbicacionIdnumericUpDown.Value = tipoanalisis.TipoAnalisisId;
            DescripciontextBox.Text = tipoanalisis.Descripcion;

        }

        private bool ExisteEnLaBaseDeDatos()
        {
            TipoAnalisis tipoanalisis = TipoAnalalisisBLL.Buscar((int)UbicacionIdnumericUpDown.Value);
            return (tipoanalisis != null);

        }





        public static bool NoDuplicado(string descripcion)
        {
            bool paso = false;
            Contexto db = new Contexto();
            try
            {
                if (db.TipoAnalisis.Any(p => p.Descripcion.Equals(descripcion)))
                {
                    paso = true;
                }
            }
            catch (Exception)
            {
                throw;
            }
            return paso;
        }

        private bool ValidarCampos()
        {
            bool paso = true;
            if (DescripciontextBox.Text == string.Empty)
            {
                MessageBox.Show("La descripcion no puede estar vacia");
                DescripciontextBox.Focus();
                paso = false;
            }
            if (NoDuplicado(DescripciontextBox.Text))
            {
                MessageBox.Show("Los nombre no pueden ser iguales");
                DescripciontextBox.Focus();
                paso = false;
            }

            return paso;
        }

        private void Buscarbutton_Click(object sender, EventArgs e)
        {
            int id;

            TipoAnalisis tipoanalisis = new TipoAnalisis();

            int.TryParse(UbicacionIdnumericUpDown.Text, out id);
            Limpiar();

            tipoanalisis = TipoAnalalisisBLL.Buscar(id);

            if (tipoanalisis != null)
            {
                MessageBox.Show("ubicacion encontrado");
                LlenaCampo(tipoanalisis);

            }
            else
            {
                MessageBox.Show("ubicacion no existe");
            }
        }

        private void Eliminarbutton_Click(object sender, EventArgs e)
        {
            errorProvider.Clear();
            int id;
            int.TryParse(UbicacionIdnumericUpDown.Text, out id);
            Limpiar();
            if (TipoAnalalisisBLL.Eliminar(id))
            {
                MessageBox.Show("Eliminado");
            }
            else
            {
                errorProvider.SetError(UbicacionIdnumericUpDown, "No se puede elimina, porque no existe");
            }
        }

        private void Nuevobutton_Click(object sender, EventArgs e)
        {
            Limpiar();
        }

        private void Guardarbutton_Click(object sender, EventArgs e)
        {
           TipoAnalisis tipoanalisis;
            bool paso = false;

  
            if (!ValidarCampos())
                return;

            tipoanalisis = LlenaClase();


            if (UbicacionIdnumericUpDown.Value == 0)
            {
                paso = TipoAnalalisisBLL.Guardar(tipoanalisis);


            }
            else
            {
                if (!ExisteEnLaBaseDeDatos())
                {
                    MessageBox.Show("No se puede modificar un Tipo de analisis que no existe que no existe", "Fallo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                paso = TipoAnalalisisBLL.Modificar(tipoanalisis);


            }

            if (paso)
                MessageBox.Show("Guardado!!", "Exito", MessageBoxButtons.OK, MessageBoxIcon.Information);



            else
                MessageBox.Show("No fue posible guardar!!", "Fallo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            Limpiar();

        }
    }
}
