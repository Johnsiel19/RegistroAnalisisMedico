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
using ProyectoFinalAplicadaI.UI.Consultas;

namespace ProyectoFinalAplicadaI
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        private void UsuarioToolStripMenuItem_Click(object sender, EventArgs e)
        {
            rUsuario frm = new rUsuario();
            frm.StartPosition = FormStartPosition.CenterScreen;
            frm.Show();
        }

        private void CargoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            rTipoAnalisis frm = new rTipoAnalisis();
            frm.StartPosition = FormStartPosition.CenterScreen;
            frm.Show();
        }

        private void UsuariosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            cUsuarios frm = new cUsuarios();
            frm.StartPosition = FormStartPosition.CenterScreen;
            frm.Show();

        }

        private void CargosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            cCargos frm = new cCargos();
            frm.StartPosition = FormStartPosition.CenterScreen;
            frm.Show();

        }

        private void AnalisisToolStripMenuItem_Click(object sender, EventArgs e)
        {
            rAnalisis frm = new rAnalisis();
            frm.Show();
        }
    }
}
