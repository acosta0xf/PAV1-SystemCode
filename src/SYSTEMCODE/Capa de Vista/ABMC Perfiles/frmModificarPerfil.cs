using System;
using System.Windows.Forms;
using SYSTEMCODE.Capa_de_Negocio;

namespace SYSTEMCODE.Capa_de_Vista.ABMC_Perfiles
{
    public partial class frmModificarPerfil : Form
    {
        Perfil perfil;
        int id_perfil;

        public frmModificarPerfil(int id_perfil)
        {
            this.id_perfil = id_perfil;
            InitializeComponent();
        }

        private void labelInforme(string mensaje, bool estado)
        {
            if (!estado)
            {
                lblInformes.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(244)))), ((int)(((byte)(67)))), ((int)(((byte)(54)))));
                lblInformes.ForeColor = System.Drawing.Color.White;
                lblInformes.Text = mensaje;
            }
            else
            {
                lblInformes.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(200)))), ((int)(((byte)(83)))));
                lblInformes.ForeColor = System.Drawing.Color.White;
                lblInformes.Text = mensaje;
            }
        }

        private void deshabilitarControles()
        {
            btnModificar.Enabled = false;
            btnCancelar.Enabled = true;
        }

        private void frmModificarUsuario_Load(object sender, EventArgs e)
        {
            perfil = Perfil.ObtenerPerfilPorID(id_perfil);

            txtNombrePerfil.Text = perfil.Nombre.ToString();
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            Close();
            return;
        }

        private void btnModificar_Click(object sender, EventArgs e)
        {
            if (txtNombrePerfil.Text == "")
            {
                labelInforme("DATO OBLIGATORIO: NOMBRE", false);
                txtNombrePerfil.Focus();

                return;
            }

            string nombrePerfil = txtNombrePerfil.Text.ToString();

            Perfil perfilAuxiliar = new Perfil(id_perfil, nombrePerfil, false);
            string error = Perfil.ModificarPerfil(perfilAuxiliar);

            if (error == "")
            {
                labelInforme("PERFIL MODIFICADO CON ÉXITO", true);

                txtNombrePerfil.Enabled = false;

                deshabilitarControles();
            }
            else
            {
                labelInforme(error, false);

                deshabilitarControles();
            }
        }
    }
}
