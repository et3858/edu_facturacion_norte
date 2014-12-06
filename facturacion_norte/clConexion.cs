using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MySql.Data.MySqlClient;

namespace facturacion_norte
{
    public class clConexion
    {

        MySqlConnection conexion = new MySqlConnection(); //se crea una nueva instancia de conexion con mysql
        //String cadena; //se crea una cadena que se conectara con la base de datos
        public String cadena = "Server=127.0.0.1; Database=bd_facturacion_norte; Uid=root; Pwd='';";

        public void iniciarConexion() {
            try
            {
                //la cadena va a llevar: 
                //la direccion del servidor (puede ser localhost), el nombre de la base de datos, el nombre de usuario, y la contraseña
                //cadena = "Server=127.0.0.1; Database=bd_facturacion_norte; Uid=root; Pwd='';";
                conexion.ConnectionString = cadena;
                conexion.Open();
            }
            catch(MySqlException){ 
                
            }
        }

        public void cerrarConexion() {
            try
            {
                conexion.Close();
            }
            catch (MySqlException) { 

            }
        }

    }
}
