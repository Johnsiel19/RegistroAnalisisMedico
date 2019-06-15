using ProyectoFinalAplicadaI.Entidades;
using ProyectoFinalAplicadaI.BLL;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ProyectoFinalAplicadaI.UI.Consultas
{
    public partial class cUsuarios : Form
    {
        public cUsuarios()
        {
            InitializeComponent();
            FiltrocomboBox.Text = "Todo";
        }


        private void Consultarbutton_Click(object sender, EventArgs e)
        {
            var listado = new List<Usuarios>();

            if(CriteriotextBox.Text.Trim().Length > 0)
            {
                switch (FiltrocomboBox.Text)
                {
                    case "Todo":
                        listado = UsuariosBLL.GetList(p => true);
                        break;

                    case "Id":
                        int id = Convert.ToInt32(CriteriotextBox.Text);
                        listado = UsuariosBLL.GetList(p => p.UsuarioId == id);
                        break;

                    case "Nombre":
                        listado = UsuariosBLL.GetList(p => p.Nombres.Contains(CriteriotextBox.Text));
                        break;

                    case "Usuario":
                        listado = UsuariosBLL.GetList(p => p.Usuario.Contains(CriteriotextBox.Text));
                        break;
                    default:
                        break;
                }
                listado = listado.Where(c => c.FechaIngreso.Date >= DesdedateTimePicker.Value.Date && c.FechaIngreso.Date <= HastadateTimePicker.Value.Date).ToList();
            }
            else
            {
                listado = UsuariosBLL.GetList(p => true);
            }

            ConsultadataGridView.DataSource = null;
            ConsultadataGridView.DataSource = listado;
        }
    }
}
