using System;
using System.Collections.Generic;
using System.Windows.Forms;
using SYSTEMCODE.Capa_de_Datos;
using SYSTEMCODE.Capa_de_Negocio;

namespace SYSTEMCODE.Capa_de_Vista.ABMC_Perfiles
{
    public partial class frmEliminarPerfil : Form
    {
        Perfil perfil;
        int id_perfil;

        public frmEliminarPerfil(int id_perfil)
        {
            this.id_perfil = id_perfil;
            InitializeComponent();
        }
        
        private void btnCancelar_Click(object sender, EventArgs e)
        {
            Close();
            return;
        }

        private void frmEliminarPerfil_Load(object sender, EventArgs e)
        {
            perfil = Perfil.ObtenerPerfilPorID(id_perfil);

            labelInforme("¿DESEAS DAR DE BAJA AL PERFIL?", false);
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
            IList<Usuario> listaUsuarios = UsuarioDatos.ConsultarTablaUsuarios();
            for (int i = 0; i < listaUsuarios.Count; i++)
            {
                if (listaUsuarios[i].Perfil.Id_perfil.Equals(id_perfil))
                {
                    labelInforme("EXISTEN USUARIOS ASIGNADOS A ESTE PERFIL", false);
                    btnEliminar.Enabled = false;
                    return;
                }
            }

            string error = Perfil.EliminarPerfil(perfil);

            if (error == "")
            {
                labelInforme("PERFIL ELIMINADO CON ÉXITO", true);

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
