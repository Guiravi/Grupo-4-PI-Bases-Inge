using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data.SqlClient;
using LaCafeteria.Utilidades;

namespace LaCafeteria.Models.Handlers
{
    public class EditorArticuloDBHandler
    {
        public void ActualizarEstadoArticulo(int id, string estadoArticulo) {
            string connectionString = AppSettings.GetConnectionString();

            using ( SqlConnection connection = new SqlConnection(connectionString) )
            {
                connection.Open();
                string cmdString = "UPDATE Articulo SET Articulo.estado = @estadoArticulo WHERE Articulo.idArticuloPK = @id";
                SqlCommand command = new SqlCommand(cmdString, connection);
                command.Parameters.AddWithValue("@id", id);
                command.Parameters.AddWithValue("@estadoArticulo", estadoArticulo);
                command.ExecuteNonQuery();
            }

        }
    }
}
