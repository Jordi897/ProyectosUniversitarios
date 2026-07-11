using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using System.Data.SqlClient;

namespace LibraryEN_CAD.ConversionDivisa
{

    /// <summary>
    /// Esta clase representa
    /// el CAD de ConversionDivisa
    /// </summary>
    public class CADConversionDivisa
    {

        /// <summary>
        /// Propiedad que representa
        /// la conexión con la base de datos
        /// </summary>
        private string conexion;

        /// <summary>
        /// Constructor por defecto
        /// </summary>
        public CADConversionDivisa()
        {
            conexion = ConfigurationManager.ConnectionStrings["miconexion"].ToString();
        }

        /// <summary>
        /// Este método crea una conversión
        /// de divisas en la base de datos
        /// </summary>
        /// <param name="c">
        /// Conversión a crear
        /// </param>
        /// <returns>
        /// Verdadero si se crea, falso si no
        /// </returns>
        public bool Create(ENConversionDivisa c)
        {
            SqlConnection con = new SqlConnection(conexion);
            bool correcto = false;


            try
            {
                con.Open();
                SqlCommand command = new SqlCommand("Insert into dbo.conversiondivisa (moneda,valorvirtual)" + "VALUES (@moneda,@valorvirtual)", con);

                command.Parameters.AddWithValue("@moneda", c.Moneda);
                command.Parameters.AddWithValue("@valorvirtual", c.ValorVirtual);
                correcto = true;
                command.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
              
            }
            finally
            {
                con.Close();
            }
            return correcto;

        }

        /// <summary>
        /// Este método lee una conversión
        /// de divisas en la base de datos
        /// </summary>
        /// <param name="c">
        /// Conversión a leer
        /// </param>
        /// <returns>
        /// Verdadero si se lee,falso si no
        /// </returns>
        public bool Read(ENConversionDivisa c)
        {
            SqlConnection con = new SqlConnection(conexion);
            bool correcto = false;
            try
            {
                con.Open();
                SqlCommand command = new SqlCommand("select * from dbo.conversiondivisa where moneda=@moneda", con);
                command.Parameters.AddWithValue("@moneda", c.Moneda);

                SqlDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    c.ValorVirtual = decimal.Parse(reader["valorvirtual"].ToString());
                    correcto = true;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
               
            }
            finally
            {
                con.Close();
            }
            return correcto;
        }


        /// <summary>
        /// Este método actualiza una Conversión de
        /// divisas en la base de datos
        /// </summary>
        /// <param name="c">
        /// Conversión a actualizar
        /// </param>
        /// <returns>
        /// Verdadero si se actualiza, falso si no
        /// </returns>
        public bool Update(ENConversionDivisa c)
        {
            SqlConnection con = new SqlConnection(conexion);
            bool correcto = false;
            try
            {
                con.Open();
                SqlCommand command = new SqlCommand("Update dbo.conversiondivisa set valorvirtual=@valorvirtual where moneda=@moneda;", con);

                command.Parameters.AddWithValue("@moneda", c.Moneda);
                command.Parameters.AddWithValue("@valorvirtual", c.ValorVirtual);
                correcto = true;
                command.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
               
            }
            finally
            {
                con.Close();
            }
            return correcto;
        }

        /// <summary>
        /// Este método borra una conversión 
        /// de divisas en la base de datos
        /// </summary>
        /// <param name="c">
        /// Conversión a borrar
        /// </param>
        /// <returns>
        /// Verdadero si lo borra, falso si no
        /// </returns>
        public bool Delete(ENConversionDivisa c)
        {
            SqlConnection con = new SqlConnection(conexion);
            bool correcto = false;
            try
            {
                con.Open();
                SqlCommand command = new SqlCommand("Delete from conversiondivisa where moneda=@moneda", con);

                command.Parameters.AddWithValue("@moneda", c.Moneda);
                correcto = true;
                command.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
               
            }
            finally
            {
                con.Close();
            }
            return correcto;
        }


        /// <summary>
        /// Este método lee todas
        /// las conversiones de divisas
        /// de la base de datos
        /// </summary>
        /// <returns>
        /// Verdadero si lee todas, falso si no
        /// </returns>
        public List<ENConversionDivisa> ReadAll()
        {


            List<ENConversionDivisa> lista = new List<ENConversionDivisa>();
            SqlConnection conn = null;

            string comando = "Select * from conversiondivisa";
            conn = new SqlConnection(conexion);
            conn.Open();
            SqlCommand cmd = new SqlCommand(comando, conn);
            SqlDataReader dr = cmd.ExecuteReader();
            try
            {
                while (dr.Read())
                {
                    ENConversionDivisa en = new ENConversionDivisa();
                    en.Moneda = dr["moneda"].ToString();
                    en.ValorVirtual = decimal.Parse(dr["valorvirtual"].ToString());

                    lista.Add(en);

                }
                dr.Close();

            }
            catch (SqlException e)
            {
                throw e;
            }
            finally
            {
                if (conn != null)
                {
                    conn.Close();
                }
            }

            return lista;

        }



    }
}