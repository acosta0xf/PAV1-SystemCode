using System;
using System.Data;
using System.Windows.Forms;
using SYSTEMCODE.Capa_de_Negocio;

namespace SYSTEMCODE.Capa_de_Vista
{
    public partial class frmEliminarUsuario : Form
    {
        Usuario usuario;

        public frmEliminarUsuario()
        {
            InitializeComponent();
        }

        private void cargarComboBox(ComboBox cbo, DataTable tabla)
        {
            cbo.DataSource = tabla;
            cbo.DisplayMember = tabla.Columns[1].ColumnName;
            cbo.ValueMember = tabla.Columns[0].ColumnName;
            cbo.SelectedIndex = -1;
        }

        private void btnConsultar_Click(object sender, EventArgs e)
        {
            if (numDNI.Text != "")
            {
                usuario = Usuario.ObtenerUsuario(numDNI.Text);
                if (usuario != null)
                {
                    if (usuario.Dni.ToString() == numDNI.Text && !usuario.Borrado)
                    {
                        lblInformes.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(200)))), ((int)(((byte)(83)))));
                        lblInformes.ForeColor = System.Drawing.Color.White;
                        lblInformes.Text = "USUARIO ENCONTRADO\n¿ESTÁ SEGURO QUE DESEA ELIMINARLO?";

                        btnEliminar.Enabled = true;
                        btnCancelar.Enabled = true;
                        btnVisualizar.Enabled = false;
                        cboPerfiles.SelectedIndex = usuario.Perfil.Id_perfil - 1;
                        txtNombreUsuario.Text = usuario.NombreUsuario.ToString();
                        txtClave.Text = usuario.Clave.ToString();
                        txtEmail.Text = usuario.Email.ToString();
                        cboPerfiles.Enabled = false;
                        txtNombreUsuario.Enabled = false;
                        txtClave.Enabled = false;
                        txtEmail.Enabled = false;
                    }
                    else if (usuario.Dni.ToString() == numDNI.Text && usuario.Borrado)
                    {
                        lblInformes.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(244)))), ((int)(((byte)(67)))), ((int)(((byte)(54)))));
                        lblInformes.ForeColor = System.Drawing.Color.White;
                        lblInformes.Text = "EL USUARIO YA FUE ELIMINADO DEL SISTEMA";

                        btnEliminar.Enabled = false;
                        btnCancelar.Enabled = true;
                        btnVisualizar.Enabled = false;
                        cboPerfiles.SelectedIndex = -1;
                        txtNombreUsuario.Text = "";
                        txtClave.Text = "";
                        txtEmail.Text = "";
                        cboPerfiles.Enabled = false;
                        txtNombreUsuario.Enabled = false;
                        txtClave.Enabled = false;
                        txtEmail.Enabled = false;
                    }
                }
                else
                {
                    lblInformes.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(244)))), ((int)(((byte)(67)))), ((int)(((byte)(54)))));
                    lblInformes.ForeColor = System.Drawing.Color.White;
                    lblInformes.Text = "EL USUARIO NO SE ENCUENTRA REGISTRADO";

                    cboPerfiles.SelectedIndex = -1;
                    txtNombreUsuario.Text = "";
                    txtClave.Text = "";
                    txtEmail.Text = "";
                    btnEliminar.Enabled = false;
                    btnCancelar.Enabled = true;
                    btnVisualizar.Enabled = false;
                    cboPerfiles.Enabled = false;
                    txtNombreUsuario.Enabled = false;
                    txtClave.Enabled = false;
                    txtEmail.Enabled = false;
                }
            }
            else
            {
                lblInformes.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(244)))), ((int)(((byte)(67)))), ((int)(((byte)(54)))));
                lblInformes.ForeColor = System.Drawing.Color.White;
                lblInformes.Text = "DATO OBLIGATORIO: D.N.I";
            }
        }

        private void frmEliminarUsuario_Load(object sender, EventArgs e)
        {
            cargarComboBox(cboPerfiles, Perfil.ObtenerPerfiles());
        }

        private void btnVisualizar_MouseDown(object sender, MouseEventArgs e)
        {
            txtClave.UseSystemPasswordChar = false;
        }

        private void btnVisualizar_MouseUp(object sender, MouseEventArgs e)
        {
            txtClave.UseSystemPasswordChar = true;
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            Close();
            return;
        }

        private void numDNI_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Char.IsDigit(e.KeyChar))
            {
                e.Handled = false;
            }
            else
            {
                if (Char.IsControl(e.KeyChar))
                {
                    e.Handled = false;
                }
                else
                {
                    e.Handled = true;
                }
            }
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            string dni = numDNI.Text.ToString();
            
            Usuario usuarioAuxiliar = new Usuario(dni);
            string error = Usuario.EliminarUsuario(usuarioAuxiliar);

            if (error == "")
            {
                lblInformes.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(200)))), ((int)(((byte)(83)))));
                lblInformes.ForeColor = System.Drawing.Color.White;
                lblInformes.Text = "USUARIO ELIMINADO CON ÉXITO";

                btnEliminar.Enabled = false;
                btnCancelar.Enabled = true;
                btnVisualizar.Enabled = false;
                cboPerfiles.Enabled = false;
                txtNombreUsuario.Enabled = false;
                txtClave.Enabled = false;
                txtEmail.Enabled = false;
            }
            else
            {
                lblInformes.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(244)))), ((int)(((byte)(67)))), ((int)(((byte)(54)))));
                lblInformes.ForeColor = System.Drawing.Color.White;
                lblInformes.Text = error;

                btnEliminar.Enabled = false;
                btnCancelar.Enabled = true;
                btnVisualizar.Enabled = false;
                numDNI.Enabled = false;
                btnConsultar.Enabled = false;
                cboPerfiles.Enabled = false;
                txtNombreUsuario.Enabled = false;
                txtClave.Enabled = false;
                txtEmail.Enabled = false;
            }
        }
    }
}
