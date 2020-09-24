using System;
using System.Windows.Forms;
using SYSTEMCODE.Capa_de_Negocio;
using SYSTEMCODE.Capa_de_Vista;
using SYSTEMCODE.Capa_de_Vista.ABMC_Perfiles;
using SYSTEMCODE.Capa_de_Vista.ABMC_Usuarios;

namespace SYSTEMCODE
{
    public partial class frmSystemCode : Form
    {
        static Usuario usuarioActual = null;

        public frmSystemCode()
        {
            InitializeComponent();
        }

        public static Usuario UsuarioActual { get => usuarioActual; set => usuarioActual = value; }

        private void frmSystemCode_Load(object sender, EventArgs e)
        {
            frmLogin login = new frmLogin();
            login.ShowDialog();

            if (login.Cerrado)
            {
                Close();
                return;
            }

            UsuarioActual = frmLogin.UsuarioActual;

            lblBienvenida.Text = "¡Bienvenido, " + UsuarioActual.NombreUsuario + "!";

            switch (Usuario.ObtenerPerfil(UsuarioActual))
            {
                case "Encargado General":
                    menuUsuarios.Enabled = true;
                    menuPerfiles.Enabled = true;
                    menuClientes.Enabled = true;
                    menuBarrios.Enabled = true;
                    menuContactos.Enabled = true;
                    menuProyectos.Enabled = true;
                    menuVentas.Enabled = true;
                    menuInformes.Enabled = true;
                    break;

                case "Encargado de Administración":
                    menuUsuarios.Enabled = false;
                    menuPerfiles.Enabled = false;
                    menuClientes.Enabled = true;
                    menuBarrios.Enabled = true;
                    menuContactos.Enabled = true;
                    menuProyectos.Enabled = true;
                    menuVentas.Enabled = false;
                    menuInformes.Enabled = true;
                    break;

                case "Encargado de Ventas":
                    menuUsuarios.Enabled = false;
                    menuPerfiles.Enabled = false;
                    menuClientes.Enabled = false;
                    menuBarrios.Enabled = false;
                    menuContactos.Enabled = false;
                    menuProyectos.Enabled = false;
                    menuVentas.Enabled = true;
                    menuInformes.Enabled = false;
                    break;
            }
        }

        private void menuSalir_Click(object sender, EventArgs e)
        {
            Close();
            return;
        }

        private void menuUsuarios_Click(object sender, EventArgs e)
        {
            frmUsuarios usuarios = new frmUsuarios();
            usuarios.ShowDialog();
        }

        private void menuPerfiles_Click(object sender, EventArgs e)
        {
            frmPerfiles perfiles = new frmPerfiles();
            perfiles.ShowDialog();
        }
    }
}
