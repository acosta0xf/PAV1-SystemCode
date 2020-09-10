using BugTracker.Capa_de_Datos;
using System;
using System.Data;
using SYSTEMCODE.Capa_de_Negocio;

namespace SYSTEMCODE.Capa_de_Datos
{
    public static class UsuarioDatos
    {
        private static Usuario diseniarUsuario(int posicion, DataTable tabla)
        {
            int id_usuario = Convert.ToInt32(tabla.Rows[posicion]["id_usuario"].ToString());
            string usuario = tabla.Rows[posicion]["usuario"].ToString();
            string dni = tabla.Rows[posicion]["dni"].ToString();
            int id_perfil = Convert.ToInt32(tabla.Rows[posicion]["id_perfil"].ToString());
            string clave = tabla.Rows[posicion]["password"].ToString();
            string email = tabla.Rows[posicion]["email"].ToString();
            bool borrado = Convert.ToBoolean(tabla.Rows[posicion]["borrado"]);

            Perfil perfil = PerfilDatos.ConsultarPerfil(id_perfil)[0];

            return new Usuario(id_usuario, dni, usuario, perfil, clave, email, borrado);
        }

        public static Usuario ConsultarUsuarioPorNombreUsuario(string nombreUsuario)
        {
            string SQL = "SELECT usuarios.* " +
                         "FROM Usuarios usuarios " +
                         "WHERE " +
                            "borrado = 0 AND " +
                            "usuario = '" + nombreUsuario + "'";

            DataTable tabla = GestorBD.Consultar(SQL);
            
            return (tabla.Rows.Count > 0) ? diseniarUsuario(0, tabla) : null;
        }

        public static Usuario ConsultarUsuarioPorDNI(string DNI)
        {
            string SQL = "SELECT usuarios.* " +
                         "FROM Usuarios usuarios " +
                         "WHERE dni = '" + DNI + "'";

            DataTable tabla = GestorBD.Consultar(SQL);

            return (tabla.Rows.Count > 0) ? diseniarUsuario(0, tabla) : null;
        }

        public static string InsertarUsuario(Usuario usuario)
        {
            string SQL = "INSERT INTO Usuarios " +
                         "VALUES " +
                         "(" +
                            usuario.Perfil.Id_perfil + ", '" +
                            usuario.Dni.ToString() + "', '" +
                            usuario.NombreUsuario.ToString() + "', '" +
                            usuario.Clave.ToString() + "', '" +
                            usuario.Email.ToString() + "', " +
                            "'N', " +
                            "0" +
                         ")";

            return GestorBD.Ejecutar(SQL);
        }

        public static string ModificarUsuario(Usuario usuario)
        {
            string SQL = "UPDATE Usuarios " +
                         "SET " +
                            "id_perfil = " + usuario.Perfil.Id_perfil.ToString() + ", " +
                            "usuario = '" + usuario.NombreUsuario.ToString() + "', " +
                            "password = '" + usuario.Clave.ToString() + "', " +
                            "email = '" + usuario.Email.ToString() + "', " +
                            "borrado = " + Convert.ToInt32(usuario.Borrado) + " " +
                         "WHERE dni = '" + usuario.Dni.ToString() + "'";

            return GestorBD.Ejecutar(SQL);
        }

        public static string EliminarUsuario(Usuario usuario)
        {
            string SQL = "UPDATE Usuarios " +
                         "SET " +
                            "borrado = 1 " +
                         "WHERE dni = '" + usuario.Dni.ToString() + "'";

            return GestorBD.Ejecutar(SQL);
        }
    }
}
