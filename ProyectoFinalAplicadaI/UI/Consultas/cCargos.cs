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

namespace ProyectoFinalAplicadaI.UI.Consultas
{
    public partial class cCargos : Form
    {
        public cCargos()
        {
            InitializeComponent();
            FiltrocomboBox.Text = "Todo";
        }

        private void Consultarbutton_Click(object sender, EventArgs e)
        {
            var listado = new List<Cargos>();

            if (CriteriotextBox.Text.Trim().Length > 0)
            {
                switch (FiltrocomboBox.Text)
                {
                    case "Todo":
                        listado = CargosBLL.GetList(p => true);
                        break;

                    case "Id":
                        int id = Convert.ToInt32(CriteriotextBox.Text);
                        listado = CargosBLL.GetList(p => p.CargoId == id);
                        break;

                    case "Descripcion":
                        listado = CargosBLL.GetList(p => p.Descripcion.Contains(CriteriotextBox.Text));
                        break;

                }
                
            }
            else
            {
                listado = CargosBLL.GetList(p => true);
            }

            ConsultadataGridView.DataSource = null;
            ConsultadataGridView.DataSource = listado;
        }

        private void CriteriotextBox_TextChanged(object sender, EventArgs e)
        {

        }

        private void FiltrocomboBox_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void ConsultadataGridView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void Label4_Click(object sender, EventArgs e)
        {

        }

        private void Label3_Click(object sender, EventArgs e)
        {

        }
    }
}
