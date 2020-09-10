using System.Data;
using SYSTEMCODE.Capa_de_Datos;

namespace SYSTEMCODE.Capa_de_Negocio
{
    public class Perfil
    {
        private int id_perfil;
        private string nombre;
        private bool borrado;

        public Perfil()
        {

        }

        public Perfil(int id_perfil, string nombre, bool borrado)
        {
            this.Id_perfil = id_perfil;
            this.Nombre = nombre;
            this.Borrado = borrado;
        }

        public int Id_perfil { get => id_perfil; set => id_perfil = value; }
        public string Nombre { get => nombre; set => nombre = value; }
        public bool Borrado { get => borrado; set => borrado = value; }

        public static DataTable ObtenerPerfiles()
        {
            return PerfilDatos.ConsultarPerfiles();
        }
    }
}
