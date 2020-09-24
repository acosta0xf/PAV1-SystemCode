using System;
using System.Collections.Generic;
using System.Windows.Forms;
using SYSTEMCODE.Capa_de_Negocio;

namespace SYSTEMCODE.Capa_de_Vista.ABMC_Usuarios
{
    public partial class frmUsuarios : Form
    {
        public frmUsuarios()
        {
            InitializeComponent();
        }
        
        private void cargarTablaUsuarios(DataGridView dgv, IList<Usuario> listaUsuarios)
        {
            dgv.Rows.Clear();

            for (int i = 0; i < listaUsuarios.Count; i++)
            {
                dgv.Rows.Add
                (
                    listaUsuarios[i].Dni,
                    listaUsuarios[i].NombreUsuario,
                    listaUsuarios[i].Email
                );
            }
        }

        private void estadoBotones(bool estado)
        {
            btnConsultar.Enabled = estado;
            btnEliminar.Enabled = estado;
            btnModificar.Enabled = estado;
        }

        private void frmUsuarios_Load(object sender, EventArgs e)
        {
            estadoBotones(false);

            cargarTablaUsuarios(dgvUsuarios, Usuario.ObtenerTablaUsuarios());
        }

        private void dgvUsuarios_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            estadoBotones(true);
        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            frmAltaUsuario altaUsuario = new frmAltaUsuario();
            altaUsuario.ShowDialog();

            cargarTablaUsuarios(dgvUsuarios, Usuario.ObtenerTablaUsuarios());
        }

        private void btnModificar_Click(object sender, EventArgs e)
        {
            frmModificarUsuario modificarUsuario = new frmModificarUsuario(Convert.ToInt32(dgvUsuarios.CurrentRow.Cells[0].Value));
            modificarUsuario.ShowDialog();

            cargarTablaUsuarios(dgvUsuarios, Usuario.ObtenerTablaUsuarios());
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            frmEliminarUsuario eliminarUsuario = new frmEliminarUsuario(Convert.ToInt32(dgvUsuarios.CurrentRow.Cells[0].Value));
            eliminarUsuario.ShowDialog();

            cargarTablaUsuarios(dgvUsuarios, Usuario.ObtenerTablaUsuarios());
        }

        private void btnConsultar_Click(object sender, EventArgs e)
        {
            frmConsultarUsuario consultarUsuario = new frmConsultarUsuario(Convert.ToInt32(dgvUsuarios.CurrentRow.Cells[0].Value));
            consultarUsuario.ShowDialog();
        }
    }
}
