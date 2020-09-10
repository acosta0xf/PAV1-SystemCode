using BugTracker.Capa_de_Datos;
using System;
using System.Collections.Generic;
using System.Data;
using SYSTEMCODE.Capa_de_Negocio;

namespace SYSTEMCODE.Capa_de_Datos
{
    public static class PerfilDatos
    {
        public static IList<Perfil> ConsultarPerfil(int id_perfil)
        {
            string consultaSQL = "SELECT perfiles.* " +
                                 "FROM Perfiles perfiles " +
                                 "WHERE id_perfil = '" + id_perfil.ToString() + "'";

            IList<Perfil> listaPerfiles = new List<Perfil>();
            DataTable tabla = GestorBD.Consultar(consultaSQL);
            if (tabla.Rows.Count > 0)
            {
                int idPerfil = Convert.ToInt32(tabla.Rows[0]["id_perfil"].ToString());
                string nombre = tabla.Rows[0]["nombre"].ToString();
                bool borrado = Convert.ToBoolean(tabla.Rows[0]["borrado"]);

                listaPerfiles.Add(new Perfil(idPerfil, nombre, borrado));
            }

            return listaPerfiles;
        }

        public static DataTable ConsultarPerfiles()
        {
            return GestorBD.ConsultarTabla("Perfiles");
        }
    }
}
