using Entidades;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Datos
{
    public class TicketDatos
    {
        public async Task<DataTable> DevolverListaAsync()
        {
            DataTable dt = new DataTable();
            try
            {
                string sql = "SELECT * FROM ticket;";

                using (MySqlConnection _conexion = new MySqlConnection(CadenaConexion.Cadena))
                {
                    await _conexion.OpenAsync();
                    using (MySqlCommand comando = new MySqlCommand(sql, _conexion))
                    {
                        comando.CommandType = System.Data.CommandType.Text;
                        MySqlDataReader dr = (MySqlDataReader)await comando.ExecuteReaderAsync();
                        dt.Load(dr);
                    }
                }
            }
            catch (Exception ex)
            {
            }
            return dt;
        }

        public async Task<bool> InsertarAsync(Ticket ticket)
        {
            bool inserto = false;
            
            try
            {
                string sql = "INSERT INTO ticket VALUES (@Codigo, @Fecha, @CodigoUsuario, @NombreCliente, @TipoSoporte, @Descripcion, @DescripcionRespuesta, @Precio, @Impuesto, @Descuento, @SubTotal, @Total);";

                using (MySqlConnection _conexion = new MySqlConnection(CadenaConexion.Cadena))
                {
                    await _conexion.OpenAsync();
                    using (MySqlCommand comando = new MySqlCommand(sql, _conexion))
                    {
                        comando.CommandType = System.Data.CommandType.Text;                       
                        comando.Parameters.Add("@Codigo", MySqlDbType.Int32).Value = ticket.Codigo;
                        comando.Parameters.Add("@Fecha", MySqlDbType.DateTime).Value = ticket.Fecha;
                        comando.Parameters.Add("@CodigoUsuario", MySqlDbType.VarChar, 20).Value = ticket.CodigoUsuario;
                        comando.Parameters.Add("@NombreCliente", MySqlDbType.VarChar, 20).Value = ticket.NombreCliente;
                        comando.Parameters.Add("@TipoSoporte", MySqlDbType.VarChar, 50).Value = ticket.TipoSoporte;
                        comando.Parameters.Add("@Descripcion", MySqlDbType.VarChar, 50).Value = ticket.Descripcion;
                        comando.Parameters.Add("@DescripcionRespuesta", MySqlDbType.VarChar, 50).Value = ticket.DescripcionRespuesta;
                        comando.Parameters.Add("@Precio", MySqlDbType.Decimal).Value = ticket.Precio;
                        comando.Parameters.Add("@Impuesto", MySqlDbType.Decimal).Value = ticket.Impuesto;
                        comando.Parameters.Add("@Descuento", MySqlDbType.Decimal).Value = ticket.Descuento;
                        comando.Parameters.Add("@SubTotal", MySqlDbType.Decimal).Value = ticket.SubTotal;
                        comando.Parameters.Add("@Total", MySqlDbType.Decimal).Value = ticket.Total;

                        

                        await comando.ExecuteNonQueryAsync();
                        inserto = true;
                    }
                }
            }
            catch (Exception)
            {
            }
            return inserto;
        }
    }
}
