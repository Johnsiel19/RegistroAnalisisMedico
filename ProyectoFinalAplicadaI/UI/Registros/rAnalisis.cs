using ProyectoFinalAplicadaI.BLL;
using ProyectoFinalAplicadaI.DAL;
using ProyectoFinalAplicadaI.Entidades;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ProyectoFinalAplicadaI.UI.Registros;

namespace ProyectoFinalAplicadaI.UI.Consultas
{
    public partial class rAnalisis : Form
    {
        public List<AnalisisDetalle> Detalle { get; set; }
        public rAnalisis()
        {
            InitializeComponent();
            LlenarComboBox();
            LLenarComboBox2();
            this.Detalle = new List<AnalisisDetalle>();

            UsuariocomboBox.Text = null;
            TipoAnalisiscomboBox.Text = null;

        }


        private void LlenarComboBox()
        {
            var listado = new List<Usuarios>();
            listado = UsuariosBLL.GetList(p => true);
            UsuariocomboBox.DataSource = listado;
            UsuariocomboBox.DisplayMember = "Usuario";
            UsuariocomboBox.ValueMember = "UsuarioId";


        }

        private void LLenarComboBox2()
        {
            var listado2 = new List<TipoAnalisis>();
            listado2 = TipoAnalalisisBLL.GetList(l => true);
            TipoAnalisiscomboBox.DataSource = listado2;
            TipoAnalisiscomboBox.DisplayMember = "Descripcion";
            TipoAnalisiscomboBox.ValueMember = "TipoAnalisisId";


        }



        private void Limpiar()
        {
            AnalisisIdnumericUpDown.Value = 0;
            UsuariocomboBox.Text = string.Empty;
            TipoAnalisiscomboBox.Text = string.Empty;
            ResultadotextBox.Text = string.Empty;
            errorProvider.Clear();
            this.Detalle = new List<AnalisisDetalle>();
            CargarGrid();

        }

        private Analisis LlenaClase()
        {
            Analisis analisis = new Analisis();
            analisis.AnalisisId = Convert.ToInt32(AnalisisIdnumericUpDown.Value);
            analisis.UsuarioId = Convert.ToInt32( UsuariocomboBox.SelectedValue);
            analisis.FechaAnalisis = FechadateTimePicker.Value;
            analisis.Resultado = this.Detalle;
            return analisis;

        }

        private void LlenaCampo(Analisis analisis)
        {
            AnalisisIdnumericUpDown.Value = analisis.AnalisisId;
            FechadateTimePicker.Value = analisis.FechaAnalisis;
            UsuariocomboBox.Text =  analisis.UsuarioId.ToString();
            this.Detalle = analisis.Resultado;
            CargarGrid();



        }
        private bool ExisteEnLaBaseDeDatos()
        {
            Analisis analisis = AnalisBLL.Buscar((int)AnalisisIdnumericUpDown.Value);
            return (analisis != null);

        }

        private void CargarGrid()
        {
            detalleDataGridView.DataSource = null;
            detalleDataGridView.DataSource = Detalle;
        }

        private bool Validar()
        {
           
            bool paso = true;
            errorProvider.Clear();


            if (UsuariocomboBox.Text == string.Empty)
            {
                errorProvider.SetError(UsuariocomboBox, "El campo Nivel de Usuario no puede estar vacio");
                UsuariocomboBox.Focus();
                paso = false;

            }

            if (TipoAnalisiscomboBox.Text == string.Empty)
            {
                errorProvider.SetError(TipoAnalisiscomboBox, "El campo Nivel de Usuario no puede estar vacio");
                TipoAnalisiscomboBox.Focus();
                paso = false;

            }

            if (ResultadotextBox.Text == string.Empty)
            {
                errorProvider.SetError(ResultadotextBox, "El campo Usuario no puede estar vacio");
                ResultadotextBox.Focus();
                paso = false;
            }
            return paso;

        }

  


        private void Buscarbutton_Click(object sender, EventArgs e)
        {

            int id;
            Analisis analisis = new Analisis();

            int.TryParse(AnalisisIdnumericUpDown.Text, out id);
            Limpiar();

            analisis = AnalisBLL.Buscar(id);

            if (analisis != null)
            {
    
                LlenaCampo(analisis);

            }
            else
            {
                MessageBox.Show("Producto no existe");
            }

        }

        private void AgragarTiposAnalisisbutton_Click(object sender, EventArgs e)
        {
            rTipoAnalisis frm = new rTipoAnalisis();
            frm.ShowDialog();

        }

        private void Nuevobutton_Click(object sender, EventArgs e)
        {
            Limpiar();
        }

        private void Eliminarbutton_Click(object sender, EventArgs e)
        {
            errorProvider.Clear();
            int id;
            int.TryParse(AnalisisIdnumericUpDown.Text, out id);
            Limpiar();
            if (AnalisBLL.Eliminar(id))
            {
                MessageBox.Show("Eliminado");
            }
            else
            {
                errorProvider.SetError(AnalisisIdnumericUpDown, "No se puede elimina, porque no existe");
            }
        }

        private void Guardarbutton_Click(object sender, EventArgs e)
        {
            Analisis analisis;
            bool paso = false;

            if (!Validar())
                return;

            analisis = LlenaClase();


            if (AnalisisIdnumericUpDown.Value == 0)
            {
                paso = AnalisBLL.Guardar(analisis);
            }
            else
            {
                if (!ExisteEnLaBaseDeDatos())
                {
                    MessageBox.Show("No se puede modificar una persona que no existe", "Fallo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                paso = AnalisBLL.Modificar(analisis);

            }

            if (paso)
                MessageBox.Show("Guardado!!", "Exito", MessageBoxButtons.OK, MessageBoxIcon.Information);
            else
                MessageBox.Show("No fue posible guardar!!", "Fallo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            Limpiar();
        }

        private void Agregarbutton_Click(object sender, EventArgs e)
        {
            if (detalleDataGridView.DataSource != null)
                this.Detalle = (List<AnalisisDetalle>)detalleDataGridView.DataSource;

            this.Detalle.Add(
                new AnalisisDetalle(
                    id: 0,
                    analisisid: (int)AnalisisIdnumericUpDown.Value,
                    tipoanalisisid: Convert.ToInt32(TipoAnalisiscomboBox.SelectedValue),
                    resultado: ResultadotextBox.Text
                    )
               );

            CargarGrid();
        }

        private void Removerbutton_Click(object sender, EventArgs e)
        {
            if (detalleDataGridView.Rows.Count > 0 && detalleDataGridView.CurrentRow != null)
            {
                //remover la fila
                Detalle.RemoveAt(detalleDataGridView.CurrentRow.Index);

                CargarGrid();
            }
        }

    

    }
}
