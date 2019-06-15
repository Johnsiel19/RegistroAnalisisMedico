using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ProyectoFinalAplicadaI.Entidades;
using ProyectoFinalAplicadaI.BLL;

namespace ProyectoFinalAplicadaI.UI.Registros
{
    public partial class rCargos : Form
    {
        public rCargos()
        {
            InitializeComponent();
        }

        private void Limpiar()
        {
            CargoIdnumericUpDown.Value = 0;
            DescripciontextBox.Text = string.Empty;
            errorProvider.Clear();

        }

        private Cargos LlenaClase()
        {
            Cargos cargo = new Cargos();
            cargo.CargoId = Convert.ToInt32(CargoIdnumericUpDown.Value);
            cargo.Descripcion = DescripciontextBox.Text;

            return cargo;

        }

        private void LlenaCampo(Cargos cargo)
        {
            CargoIdnumericUpDown.Value = cargo.CargoId;
            DescripciontextBox.Text = cargo.Descripcion;

        }
        private bool ExisteEnLaBaseDeDatos()
        {
            Usuarios usuario = UsuariosBLL.Buscar((int)CargoIdnumericUpDown.Value);
            return (usuario != null);

        }

        private bool Validar()
        {
            bool paso = true;
            errorProvider.Clear();

            if (DescripciontextBox.Text == string.Empty)
            {
                errorProvider.SetError(DescripciontextBox, "El campo Descripción no puede estar vacio");
                DescripciontextBox.Focus();
                paso = false;
            }

            return paso;

        }
        private void Buscarbutton_Click(object sender, EventArgs e)
        {
            int id;
            Cargos cargo = new Cargos();

            int.TryParse(CargoIdnumericUpDown.Text, out id);
            Limpiar();

            cargo = CargosBLL.Buscar(id);

            if (cargo != null)
            {
                MessageBox.Show("Cargo encontrado");
                LlenaCampo(cargo);

            }
            else
            {
                MessageBox.Show("Cargo existe");
            }
        }

        private void Eliminarbutton_Click(object sender, EventArgs e)
        {
            errorProvider.Clear();
            int id;
            int.TryParse(CargoIdnumericUpDown.Text, out id);
            Limpiar();
            if (CargosBLL.Eliminar(id))
            {
                MessageBox.Show("Eliminado");
            }
            else
            {
                errorProvider.SetError(CargoIdnumericUpDown, "No se puede eliminar, porque no existe");
            }
        }

        private void Nuevobutton_Click(object sender, EventArgs e)
        {
            Limpiar();
        }

        private void Guardarbutton_Click(object sender, EventArgs e)
        {
            Cargos cargo;
            bool paso = false;

            if (!Validar())
                return;

            cargo = LlenaClase();


            if (CargoIdnumericUpDown.Value == 0)
            {
                paso = CargosBLL.Guardar(cargo);
            }
            else
            {
                if (!ExisteEnLaBaseDeDatos())
                {
                    MessageBox.Show("No se puede modificar un Crago que no existe", "Fallo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                paso = CargosBLL.Modificar(cargo);

            }

            if (paso)
                MessageBox.Show("Guardado!!", "Exito", MessageBoxButtons.OK, MessageBoxIcon.Information);
            else
                MessageBox.Show("No fue posible guardar!!", "Fallo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            Limpiar();
        }
    }
}
