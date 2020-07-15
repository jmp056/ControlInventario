using ControlInventario.Entidades;
using System;
using System.Data.SqlClient;

namespace ControlInventario.BLL
{
    class CuadresDeCajaBLL
    {
        public static bool Guardar(CuadresDeCaja CuadreDeCaja)
        {
            bool paso = false;
            SqlConnection Conexion = new SqlConnection("Data Source =.\\SQLEXPRESS; Initial Catalog = ControlInventario; Integrated Security = True");

            try
            {

                string query = "INSERT INTO CuadresDeCajas (CuadreDeCajaId, Fecha, Dosmil, Mil, Quinientos ,Doscientos, Cien, Cincuenta, Veinticinco, Veinte, Diez, Cinco, Uno, TotalVendido, Diferencia, TotalEnCaja) VALUES" +
                                                          "(@CuadreDeCajaId, @Fecha, @Dosmil, @Mil, @Quinientos, @Doscientos, @Cien, @Cincuenta, @Veinticinco, @Veinte, @Diez, @Cinco, @Uno, @TotalVendido, @Diferencia, @TotalEnCaja)";
                Conexion.Open();
                SqlCommand comando = new SqlCommand(query, Conexion);
                comando.Parameters.AddWithValue("@CuadreDeCajaId", CuadreDeCaja.CuadreDeCajaId);
                comando.Parameters.AddWithValue("@Fecha", CuadreDeCaja.Fecha);
                comando.Parameters.AddWithValue("@Dosmil", CuadreDeCaja.Dosmil);
                comando.Parameters.AddWithValue("@Mil", CuadreDeCaja.Mil);
                comando.Parameters.AddWithValue("@Quinientos", CuadreDeCaja.Quinientos);
                comando.Parameters.AddWithValue("@Doscientos", CuadreDeCaja.Doscientos);
                comando.Parameters.AddWithValue("@Cien", CuadreDeCaja.Cien);
                comando.Parameters.AddWithValue("@Cincuenta", CuadreDeCaja.Cincuenta);
                comando.Parameters.AddWithValue("@Veinticinco", CuadreDeCaja.Veinticinco);
                comando.Parameters.AddWithValue("@Veinte", CuadreDeCaja.Veinte);
                comando.Parameters.AddWithValue("@Diez", CuadreDeCaja.Diez);
                comando.Parameters.AddWithValue("@Cinco", CuadreDeCaja.Cinco);
                comando.Parameters.AddWithValue("@Uno", CuadreDeCaja.Uno);
                comando.Parameters.AddWithValue("@TotalVendido", CuadreDeCaja.TotalVendido);
                comando.Parameters.AddWithValue("@Diferencia", CuadreDeCaja.Diferencia);
                comando.Parameters.AddWithValue("@TotalEnCaja", CuadreDeCaja.TotalEnCaja);

                int FilasAfectadas = comando.ExecuteNonQuery();
                if (FilasAfectadas > 0)
                    paso = true;

            }
            catch (Exception)
            {
                throw;

            }
            finally
            {
                Conexion.Close();

            }

            return paso;
        }
    }
}
