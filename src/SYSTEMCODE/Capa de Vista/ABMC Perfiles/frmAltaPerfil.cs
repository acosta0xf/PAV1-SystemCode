using System;
using System.Windows.Forms;
using SYSTEMCODE.Capa_de_Negocio;

namespace SYSTEMCODE.Capa_de_Vista.ABMC_Perfiles
{
    public partial class frmAltaPerfil : Form
    {
        Perfil perfil;

        public frmAltaPerfil()
        {
            InitializeComponent();
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            Close();
            return;
        }

        private void labelInforme(string mensaje, bool estado, bool defecto)
        {
            if (defecto)
            {
                lblInformes.BackColor = System.Drawing.Color.LightGray;
                lblInformes.ForeColor = System.Drawing.Color.Black;
                lblInformes.Text = mensaje;
            }
            else
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
        }

        private void deshabilitarControles()
        {
            txtNombrePerfil.Enabled = false;
            btnRegistrar.Enabled = false;
            btnCancelar.Enabled = true;
        }

        private void btnRegistrar_Click(object sender, EventArgs e)
        {
            if (txtNombrePerfil.Text == "")
            {
                labelInforme("DATO OBLIGATORIO: NOMBRE", false, false);
                txtNombrePerfil.Focus();

                return;
            }

            perfil = Perfil.ObtenerPerfilPorNombre(txtNombrePerfil.Text);

            string nombrePerfil = txtNombrePerfil.Text.ToString();

            Perfil perfilAuxiliar;

            if (perfil != null)
            {
                perfilAuxiliar = new Perfil(perfil.Id_perfil, nombrePerfil, false);
            }
            else
            {
                perfilAuxiliar = new Perfil(nombrePerfil, false);
            }

            string error;
            
            if (perfil != null)
            {
                if (perfil.Borrado)
                {
                    error = Perfil.ModificarPerfil(perfilAuxiliar);
                }
                else
                {
                    labelInforme("EL PERFIL YA SE ENCUENTRA REGISTRADO", false, false);

                    txtNombrePerfil.Focus();

                    return;
                }
            }
            else
            {
                error = Perfil.AgregarPerfil(perfilAuxiliar);
            }

            if (error == "")
            {
                labelInforme("PERFIL REGISTRADO CON ÉXITO", true, false);

                deshabilitarControles();
            }
            else
            {
                labelInforme(error, false, false);

                deshabilitarControles();
            }
        }

        private void txtNombrePerfil_TextChanged(object sender, EventArgs e)
        {
            labelInforme("INFORME", false, true);
        }
    }
}
