using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data.SqlClient;
using LaCafeteria.Utilidades;

namespace LaCafeteria.Models.Handlers
{
    public class RevisorArticuloHandler
    {
        public RevisorArticuloHandler() { }

        public void ActualizarEstadoRevisionArticulo(int opinion, int contribucion, int forma, string estadoRevision, 
                                                    string comentarios, string recomendacion, string username, int idArticulo)
        {
            int puntaje = 65;
            string connectionString = AppSettings.GetConnectionString();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string cmdString = "UPDATE NucleoRevisaArticulo SET puntaje = @puntaje, opinionGeneral = @opinionGeneral, " +
                    "contribucion = @contribucion, forma = @forma, estadoRevision = @estadoRevision,  comentarios = @comentarios " +
                    "recomendacion = @recomendacion WHERE usernameMiemFK = @username AND idArticuloFK = @idArticulo";
                SqlCommand command = new SqlCommand(cmdString, connection);
                command.Parameters.AddWithValue("@puntaje", puntaje);
                command.Parameters.AddWithValue("@opinion", opinion);
                command.Parameters.AddWithValue("@contribucion", contribucion);
                command.Parameters.AddWithValue("@forma", forma);
                command.Parameters.AddWithValue("@estadoRevision", estadoRevision);
                command.Parameters.AddWithValue("@comentarios", comentarios);
                command.Parameters.AddWithValue("@recomendacion", recomendacion);
                command.Parameters.AddWithValue("@username", username);
                command.Parameters.AddWithValue("@idArticulo", idArticulo);
                command.ExecuteNonQuery();
            }

        }
    }
}
