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
using ProyectoFinalAplicadaI.DAL;

namespace ProyectoFinalAplicadaI
{
    public partial class rUsuario : Form
    {
        public rUsuario()
        {
            InitializeComponent();
            
           
        }

        private void Nuevobutton_Click(object sender, EventArgs e)
        {
            Limpiar();

        }

        public static bool NoDuplicado(string descripcion)
        {
            bool paso = false;
            Contexto db = new Contexto();
            try
            {
                if (db.Usuarios.Any(p => p.Usuario.Equals(descripcion)))
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
        private void Limpiar()
        {
            UsuarioIdnumericUpDown.Value = 0;
            NombretextBox.Text = string.Empty;
            EmailtextBox.Text = string.Empty;
            NivelUsuariocomboBox.Text = string.Empty;
            ClavetextBox.Text = string.Empty;
            UsuariotextBox.Text = string.Empty;
            ConfirmarClavetextBox.Text = string.Empty;
            errorProvider.Clear();
          

        }

        private Usuarios LlenaClase()
        {
            Usuarios usuario = new Usuarios();
            usuario.UsuarioId = Convert.ToInt32(UsuarioIdnumericUpDown.Value);
            usuario.Nombres = NombretextBox.Text;
            usuario.Email = EmailtextBox.Text;
            usuario.NivelUsuario = NivelUsuariocomboBox.Text;
            usuario.Clave = ClavetextBox.Text;
            usuario.Usuario = UsuariotextBox.Text;
            usuario.FechaIngreso = FechaIngresodateTimePicker.Value;
            return usuario;
            
        }

        private void LlenaCampo(Usuarios usuario)
        {
            UsuarioIdnumericUpDown.Value = usuario.UsuarioId;
            NombretextBox.Text = usuario.Nombres;
            EmailtextBox.Text = usuario.Email;
            NivelUsuariocomboBox.Text = usuario.NivelUsuario;
            ClavetextBox.Text = usuario.Clave;
            UsuariotextBox.Text = usuario.Usuario;
            FechaIngresodateTimePicker.Value = usuario.FechaIngreso;

        }
        private bool ExisteEnLaBaseDeDatos()
        {
            Usuarios usuario = UsuariosBLL.Buscar((int)UsuarioIdnumericUpDown.Value);
            return (usuario != null);

        }

        private bool Validar()
        {

            bool paso = true;
            errorProvider.Clear();

            string Clave = ClavetextBox.Text;
            string Confirmar = ConfirmarClavetextBox.Text;

            int Resultado = 0;

            Resultado = string.Compare(Clave, Confirmar);

            if(Resultado != 0)
            {
                errorProvider.SetError(ConfirmarClavetextBox, "Clave no coincide!");
                ConfirmarClavetextBox.Focus();
                paso = false;
            }


            if (NombretextBox.Text == string.Empty)
            {
                errorProvider.SetError(NombretextBox, "El campo Nombre no puede estar vacio");
                NombretextBox.Focus();
                paso = false;
            }

            if (EmailtextBox.Text == string.Empty)
            {
                errorProvider.SetError(EmailtextBox, "El campo Email no puede estar vacio");
                EmailtextBox.Focus();
                paso = false;

            }

            if (NivelUsuariocomboBox.Text == string.Empty)
            {
                errorProvider.SetError(NivelUsuariocomboBox, "El campo Nivel de Usuario no puede estar vacio");
                NivelUsuariocomboBox.Focus();
                paso = false;

            }

            if (UsuariotextBox.Text == string.Empty)
            {
                errorProvider.SetError(UsuariotextBox, "El campo Usuario no puede estar vacio");
                UsuariotextBox.Focus();
                paso = false;

            }

            if (ClavetextBox.Text == string.Empty)
            {
                errorProvider.SetError(ClavetextBox, "El campo Clave no puede estar vacio");
                ClavetextBox.Focus();
                paso = false;

            }

            if (ConfirmarClavetextBox.Text == string.Empty)
            {
                errorProvider.SetError(ConfirmarClavetextBox, "El campo Confirmar no puede estar vacio");
                ConfirmarClavetextBox.Focus();
                paso = false;

            }

            if (NoDuplicado(UsuariotextBox.Text))
            {
                MessageBox.Show("Los Usuarios no pueden ser iguales");
                UsuariotextBox.Focus();
                paso = false;
            }


            return paso;

        }

        private void Guardarbutton_Click(object sender, EventArgs e)
        {
            Usuarios usuario;
            bool paso = false;

            if (!Validar())
                return;

            usuario = LlenaClase();


            if (UsuarioIdnumericUpDown.Value == 0) { 
                paso = UsuariosBLL.Guardar(usuario);
            }
            else
            {
                if (!ExisteEnLaBaseDeDatos())
                {
                    MessageBox.Show("No se puede modificar una persona que no existe", "Fallo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                paso = UsuariosBLL.Modificar(usuario);

            }

            if (paso)
                MessageBox.Show("Guardado!!", "Exito", MessageBoxButtons.OK, MessageBoxIcon.Information);
            else
                MessageBox.Show("No fue posible guardar!!", "Fallo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            Limpiar();
        }

        private void Buscarbutton_Click(object sender, EventArgs e)
        {
            int id;
            Usuarios usuario = new Usuarios();

            int.TryParse(UsuarioIdnumericUpDown.Text, out id);
            Limpiar();

            usuario = UsuariosBLL.Buscar(id);

            if(usuario != null)
            {
                MessageBox.Show("Usuario encontrado");
                LlenaCampo(usuario);

            }
            else
            {
                MessageBox.Show("Usuario no existe");
            }
       
        }

        private void Eliminarbutton_Click(object sender, EventArgs e)
        {
            errorProvider.Clear();
            int id;
            int.TryParse(UsuarioIdnumericUpDown.Text, out id);
            Limpiar();
            if (UsuariosBLL.Eliminar(id))
            {
                MessageBox.Show("Eliminado");
            }
            else
            {
                errorProvider.SetError(UsuarioIdnumericUpDown, "No se puede elimina, porque no existe");
            }
        }

        private void NivelUsuariocomboBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;
        }

        private void UsuariotextBox_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
