using System;
using System.Windows.Forms;
using SYSTEMCODE.Capa_de_Negocio;

namespace SYSTEMCODE.Capa_de_Vista
{
    public partial class frmEliminarUsuario : Form
    {
        Usuario usuario;
        int dni;

        public frmEliminarUsuario(int dni)
        {
            this.dni = dni;
            InitializeComponent();
        }
        
        private void btnCancelar_Click(object sender, EventArgs e)
        {
            Close();
            return;
        }

        private void frmEliminarUsuario_Load(object sender, EventArgs e)
        {
            usuario = Usuario.ObtenerUsuario(dni.ToString());

            labelInforme("¿DESEAS DAR DE BAJA AL USUARIO?", false);
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

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            string error = Usuario.EliminarUsuario(usuario);

            if (error == "")
            {
                labelInforme("USUARIO ELIMINADO CON ÉXITO", true);

                btnEliminar.Enabled = false;
                btnCancelar.Enabled = true;
            }
            else
            {
                labelInforme(error, false);

                btnEliminar.Enabled = false;
                btnCancelar.Enabled = true;
            }
        }
    }
}
