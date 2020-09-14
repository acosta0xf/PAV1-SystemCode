using System;
using System.Data;
using System.Windows.Forms;
using SYSTEMCODE.Capa_de_Negocio;

namespace SYSTEMCODE.Capa_de_Vista
{
    public partial class frmConsultarUsuario : Form
    {
        Usuario usuario;
        int dni;

        public frmConsultarUsuario(int dni)
        {
            this.dni = dni;
            InitializeComponent();
        }

        private void cargarComboBox(ComboBox cbo, DataTable tabla)
        {
            cbo.DataSource = tabla;
            cbo.DisplayMember = tabla.Columns[1].ColumnName;
            cbo.ValueMember = tabla.Columns[0].ColumnName;
            cbo.SelectedIndex = -1;
        }

        private void frmConsultarUsuario_Load(object sender, EventArgs e)
        {
            cargarComboBox(cboPerfiles, Perfil.ObtenerPerfiles());
            usuario = Usuario.ObtenerUsuario(dni.ToString());

            numDNI.Text = usuario.Dni.ToString();
            DataTable tablaPerfiles = Perfil.ObtenerPerfiles();
            for (int i = 0; i < tablaPerfiles.Rows.Count; i++)
            {
                if (tablaPerfiles.Rows[i]["nombre"].ToString() == usuario.Perfil.Nombre.ToString())
                {
                    cboPerfiles.SelectedIndex = i;
                    break;
                }
            }
            txtNombreUsuario.Text = usuario.NombreUsuario.ToString();
            txtClave.Text = usuario.Clave.ToString();
            txtEmail.Text = usuario.Email.ToString();
        }

        private void btnVisualizar_MouseDown(object sender, MouseEventArgs e)
        {
            txtClave.UseSystemPasswordChar = false;
        }

        private void btnVisualizar_MouseUp(object sender, MouseEventArgs e)
        {
            txtClave.UseSystemPasswordChar = true;
        }

        private void btnAceptar_Click(object sender, EventArgs e)
        {
            Close();
            return;
        }
    }
}
