using System;
using System.Data;
using System.Windows.Forms;
using SYSTEMCODE.Capa_de_Negocio;

namespace SYSTEMCODE.Capa_de_Vista.ABMC_Perfiles
{
    public partial class frmPerfiles : Form
    {
        public frmPerfiles()
        {
            InitializeComponent();
        }

        private void cargarTablaPerfiles(DataGridView dgv, DataTable tablaPerfiles)
        {
            dgv.Rows.Clear();

            for (int i = 0; i < tablaPerfiles.Rows.Count; i++)
            {
                dgv.Rows.Add
                (
                    tablaPerfiles.Rows[i]["id_perfil"].ToString(),
                    tablaPerfiles.Rows[i]["nombre"].ToString()
                );
            }
        }

        private void estadoBotones(bool estado)
        {
            btnEliminar.Enabled = estado;
            btnModificar.Enabled = estado;
        }

        private void frmPerfiles_Load(object sender, EventArgs e)
        {
            estadoBotones(false);

            cargarTablaPerfiles(dgvPerfiles, Perfil.ObtenerPerfiles());
        }

        private void dgvPerfiles_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            estadoBotones(true);
        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            frmAltaPerfil altaPerfil = new frmAltaPerfil();
            altaPerfil.ShowDialog();

            cargarTablaPerfiles(dgvPerfiles, Perfil.ObtenerPerfiles());
        }

        private void btnModificar_Click(object sender, EventArgs e)
        {
            frmModificarPerfil modificarPerfil = new frmModificarPerfil(Convert.ToInt32(dgvPerfiles.CurrentRow.Cells[0].Value));
            modificarPerfil.ShowDialog();

            cargarTablaPerfiles(dgvPerfiles, Perfil.ObtenerPerfiles());
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            frmEliminarPerfil eliminarPerfil = new frmEliminarPerfil(Convert.ToInt32(dgvPerfiles.CurrentRow.Cells[0].Value));
            eliminarPerfil.ShowDialog();

            cargarTablaPerfiles(dgvPerfiles, Perfil.ObtenerPerfiles());
        }
    }
}
