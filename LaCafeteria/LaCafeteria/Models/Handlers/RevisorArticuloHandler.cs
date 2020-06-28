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

        public void ActualizarEstadoRevisionArticulo(double merito, RevisionModel revision)
        {
            double puntaje = merito*(revision.opinion+revision.contribucion+revision.forma);
            string connectionString = AppSettings.GetConnectionString();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string cmdString = "UPDATE NucleoRevisaArticulo SET puntaje = @puntaje, opinionGeneral = @opinionGeneral, " +
                    "contribucion = @contribucion, forma = @forma, estadoRevision = @estadoRevision,  comentarios = @comentarios " +
                    "recomendacion = @recomendacion WHERE usernameMiemFK = @username AND idArticuloFK = @idArticulo";
                SqlCommand command = new SqlCommand(cmdString, connection);
                command.Parameters.AddWithValue("@puntaje", puntaje);
                command.Parameters.AddWithValue("@opinion", revision.opinion);
                command.Parameters.AddWithValue("@contribucion", revision.contribucion);
                command.Parameters.AddWithValue("@forma", revision.forma);
                command.Parameters.AddWithValue("@estadoRevision", revision.estadoRevision);
                command.Parameters.AddWithValue("@comentarios", revision.comentarios);
                command.Parameters.AddWithValue("@recomendacion", revision.recomendacion);
                command.Parameters.AddWithValue("@username", revision.usernameMiemFK);
                command.Parameters.AddWithValue("@idArticulo", revision.idArticuloFK);
                command.ExecuteNonQuery();
            }
        }

        public bool ArticuloConRevisionFinalizada(int id) {
            bool finalizada = false;
            string connectionString = AppSettings.GetConnectionString();

            using ( SqlConnection connection = new SqlConnection(connectionString) )
            {
                connection.Open();

                string cmdString = "SELECT COUNT(*) AS 'Cantidad' FROM NucleoRevisaArticulo WHERE idArticuloFK = @id AND estadoRevision = 'En Revisión'";
                SqlCommand command = new SqlCommand(cmdString, connection);
                SqlDataReader reader = command.ExecuteReader();

                reader.Read();
                int articulosEnRevision = (int) reader["Cantidad"];

                if ( articulosEnRevision == 0)
                {
                    finalizada = true;
                }
            }
            return finalizada;
        }
    }
}
