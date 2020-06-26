using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using LaCafeteria.Utilidades;

namespace LaCafeteria.Models.Handlers
{
    public class InformacionArticuloDBHandler
    {
        public List<MiembroModel> GetAutoresDeArticulo(int id) {
            List<MiembroModel> autores = new List<MiembroModel>();
            string connectionString = AppSettings.GetConnectionString();

            using ( SqlConnection connection = new SqlConnection(connectionString) )
            {
                connection.Open();
                string cmdString = "SELECT M.nombre, M.apellido1, M.apellido2 " +
                    " FROM Miembro M JOIN MiembroAutorDeArticulo MAA " +
                    " ON M.usernamePK = MAA.usernameMiemFK " +
                    " WHERE MAA.idArticuloFK = @id;";
                SqlCommand command = new SqlCommand(cmdString, connection);
                command.Parameters.AddWithValue("@id", id);

                SqlDataReader reader = command.ExecuteReader();
                while ( reader.Read() )
                {
                    MiembroModel autor = new MiembroModel()
                    {
                        nombre = (string) reader["nombre"],
                        apellido1 = (string) reader["apellido1"],
                        apellido2 = (string) reader["apellido2"],
                    };
                    autores.Add(autor);
                }

                reader.Close();
            }
            return autores;
        }

        public ArticuloModel GetInformacionArticulo(int id) {
            string connectionString = AppSettings.GetConnectionString();

            ArticuloModel articulo = null;

            using ( SqlConnection connection = new SqlConnection(connectionString) )
            {
                connection.Open();

                SqlCommand cmd = new SqlCommand("SELECT * FROM Articulo WHERE Articulo.idArticuloPK = @id", connection);
                cmd.Parameters.AddWithValue("@id", id);

                SqlDataReader reader = cmd.ExecuteReader();

                while ( reader.Read() )
                {
                    articulo = new ArticuloModel()
                    {
                        idArticuloPK = id,
                        titulo = (string) reader["titulo"],
                        tipo = (string) reader["tipo"],
                        fechaPublicacion = reader["fechaPublicacion"].ToString().Remove(reader["fechaPublicacion"].ToString().Length - 12, 12),
                        resumen = (string) reader["resumen"],
                        contenido = (string) reader["contenido"],
                        estado = (string) reader["estado"],
                        visitas = (int) reader["visitas"],
                        puntajeTotalRev = (!DBNull.Value.Equals(reader["puntajeTotalRev"])) ? (double?) reader["puntajeTotalRev"] : null,
                        calificacionTotalMiem = (int) reader["calificacionTotalMiem"]
                    };
                }

                reader.Close();
            }
            return articulo;
        }

    }
}
