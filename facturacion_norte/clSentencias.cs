using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

using System.Globalization;
using System.Threading;

namespace facturacion_norte
{
    public class clSentencias
    {
        clConexion conexionMysql = new clConexion();

        public DataTable dtListaProductos() {
            //DataTable dt = new DataTable();
            MySqlConnection conexion = new MySqlConnection(conexionMysql.cadena);
            //conexion.Open();
            MySqlCommand cmdListaProductos = new MySqlCommand("spListaProductos", conexion);
            conexion.Open();
            cmdListaProductos.CommandType = CommandType.StoredProcedure;
            MySqlDataAdapter daListaProductos = new MySqlDataAdapter(cmdListaProductos);
            DataTable dt = new DataTable();
            daListaProductos.Fill(dt);
            conexion.Close();
            return dt;
        }

        public DataTable dtListaClientes() {
            MySqlConnection conexion = new MySqlConnection(conexionMysql.cadena);
            MySqlCommand cmdListaClientes = new MySqlCommand("spListaClientes", conexion);
            conexion.Open();
            cmdListaClientes.CommandType = CommandType.StoredProcedure;
            MySqlDataAdapter daListaClientes = new MySqlDataAdapter(cmdListaClientes);
            DataTable dt = new DataTable();
            daListaClientes.Fill(dt);
            conexion.Close();
            return dt;
        }



        public DataTable dtListaFacturasPorCliente() {

            MySqlConnection conexion = new MySqlConnection(conexionMysql.cadena);
            //conexion.Open();
            MySqlCommand cmdListaFacturasPorCliente = new MySqlCommand("spListaFacturaPorCliente",conexion);
            conexion.Open();
            cmdListaFacturasPorCliente.CommandType = CommandType.StoredProcedure;
            MySqlDataAdapter daListaFacturasPorCliente = new MySqlDataAdapter(cmdListaFacturasPorCliente);
            DataTable dt = new DataTable();
            daListaFacturasPorCliente.Fill(dt);
            conexion.Close();
            return dt;
        }


        
        public DataTable dtBuscarValorProducto(Int32 codProducto) {
            //DataTable dt = new DataTable();
            MySqlConnection conexion = new MySqlConnection(conexionMysql.cadena);
            //conexion.Open();
            MySqlCommand cmdBuscarValorProducto = new MySqlCommand("spBuscarValorProducto", conexion);
            conexion.Open();
            cmdBuscarValorProducto.CommandType = CommandType.StoredProcedure;
            //cmdBuscarValorProducto.Parameters.Add("prodID", MySqlDbType.Int32);
            cmdBuscarValorProducto.Parameters.Add(new MySqlParameter("prodCod", codProducto));
            //cmdBuscarValorProducto.Parameters["prodID"].Value = codProducto;
            //cmdBuscarValorProducto.ExecuteNonQuery();
            MySqlDataAdapter daValorProducto = new MySqlDataAdapter(cmdBuscarValorProducto);
            DataTable dt = new DataTable();
            daValorProducto.Fill(dt);
            conexion.Dispose();
            return dt;

        }

        public void agregarNuevoCliente(String nombre, String apepat, String apemat, Int32 rut, String rutDv, String direccion, String comuna, String ciudad, Int32 telefono, String email) {
            try
            {
                MySqlConnection conexion = new MySqlConnection(conexionMysql.cadena);
                MySqlCommand cmdAgregarNuevoCliente = new MySqlCommand("spAgregarNuevoCliente", conexion);
                conexion.Open();
                cmdAgregarNuevoCliente.CommandType = CommandType.StoredProcedure;
                cmdAgregarNuevoCliente.Parameters.Add(new MySqlParameter("newNombre", nombre));
                cmdAgregarNuevoCliente.Parameters.Add(new MySqlParameter("newApePat", apepat));
                cmdAgregarNuevoCliente.Parameters.Add(new MySqlParameter("newApeMat", apemat));
                cmdAgregarNuevoCliente.Parameters.Add(new MySqlParameter("newRut", rut));
                cmdAgregarNuevoCliente.Parameters.Add(new MySqlParameter("newRutDv", rutDv));
                cmdAgregarNuevoCliente.Parameters.Add(new MySqlParameter("newDireccion", direccion));
                cmdAgregarNuevoCliente.Parameters.Add(new MySqlParameter("newComuna", comuna));
                cmdAgregarNuevoCliente.Parameters.Add(new MySqlParameter("newCiudad", ciudad));
                cmdAgregarNuevoCliente.Parameters.Add(new MySqlParameter("newTelefono", telefono));
                cmdAgregarNuevoCliente.Parameters.Add(new MySqlParameter("newEmail", email));
                cmdAgregarNuevoCliente.ExecuteNonQuery();
                conexion.Close();
                conexion.Dispose();
            }
            catch(MySqlException ex)
            {
                MessageBox.Show("Error en mysql al registrar a un nuevo cliente como persona: " + ex.Message);
            }
        }


        public void agregarNuevoCliente2(String nombre, String giro, Int32 rut, String rutDv, String direccion, String comuna, String ciudad, Int32 telefono, String email)
        {
            try
            {
                MySqlConnection conexion = new MySqlConnection(conexionMysql.cadena);
                MySqlCommand cmdAgregarNuevoCliente = new MySqlCommand("spAgregarNuevoCliente2", conexion);
                conexion.Open();
                cmdAgregarNuevoCliente.CommandType = CommandType.StoredProcedure;
                cmdAgregarNuevoCliente.Parameters.Add(new MySqlParameter("newNombre", nombre));
                cmdAgregarNuevoCliente.Parameters.Add(new MySqlParameter("newGiro", giro));
                cmdAgregarNuevoCliente.Parameters.Add(new MySqlParameter("newRut", rut));
                cmdAgregarNuevoCliente.Parameters.Add(new MySqlParameter("newRutDv", rutDv));
                cmdAgregarNuevoCliente.Parameters.Add(new MySqlParameter("newDireccion", direccion));
                cmdAgregarNuevoCliente.Parameters.Add(new MySqlParameter("newComuna", comuna));
                cmdAgregarNuevoCliente.Parameters.Add(new MySqlParameter("newCiudad", ciudad));
                cmdAgregarNuevoCliente.Parameters.Add(new MySqlParameter("newTelefono", telefono));
                cmdAgregarNuevoCliente.Parameters.Add(new MySqlParameter("newEmail", email));
                cmdAgregarNuevoCliente.ExecuteNonQuery();
                conexion.Close();
                conexion.Dispose();
            }
            catch (MySqlException ex)
            {
                MessageBox.Show("Error en mysql al registrar a un nuevo cliente como empresa: " + ex.Message);
            }
        }









        public void agregarNuevaFactura(Int32 codCliente, Int32 subTotal, Int32 iva, Int32 total, Int32 codMedioPago) {
            try
            {
                String fecha = DateTime.Today.ToString("yyyy-MM-dd");
                String hora = DateTime.Now.ToString("H:mm:ss", new CultureInfo("es-CL"));
                MySqlConnection conexion = new MySqlConnection(conexionMysql.cadena);
                MySqlCommand cmdAgregarNuevaFactura = new MySqlCommand("spAgregarNuevaFactura", conexion);
                conexion.Open();
                cmdAgregarNuevaFactura.CommandType = CommandType.StoredProcedure;
                cmdAgregarNuevaFactura.Parameters.Add(new MySqlParameter("codCliente", codCliente));
                cmdAgregarNuevaFactura.Parameters.Add(new MySqlParameter("subTotal", subTotal));
                cmdAgregarNuevaFactura.Parameters.Add(new MySqlParameter("iva", iva));
                cmdAgregarNuevaFactura.Parameters.Add(new MySqlParameter("total", total));
                cmdAgregarNuevaFactura.Parameters.Add(new MySqlParameter("fechaFacturacion", fecha));
                cmdAgregarNuevaFactura.Parameters.Add(new MySqlParameter("horaFacturacion", hora));
                cmdAgregarNuevaFactura.Parameters.Add(new MySqlParameter("medioPago", codMedioPago));
                cmdAgregarNuevaFactura.ExecuteNonQuery();
                conexion.Close();
                conexion.Dispose();
            }
            catch (MySqlException ex) 
            {
                MessageBox.Show("Error al hacer nueva transacción de factura: " + ex.Message);
            }
        }

        public DataTable ultimoRegistroFactura() {
            MySqlConnection conexion = new MySqlConnection(conexionMysql.cadena);
            MySqlCommand cmdUltimaFactura = new MySqlCommand("spBuscarUltimaFactura", conexion);
            conexion.Open();
            cmdUltimaFactura.CommandType = CommandType.StoredProcedure;
            MySqlDataAdapter daUltimaFactura = new MySqlDataAdapter(cmdUltimaFactura);
            DataTable dt = new DataTable();
            daUltimaFactura.Fill(dt);
            //conexion.Close();
            conexion.Dispose();
            return dt;
        }


        public void agregarDetalleFactura(Int32 codUltimaFact,Int32 codProducto, Int32 cantidad, Int32 totalPorCantidad, String descripcionProd, Int32 valorRegistro) {
            try
            {
                MySqlConnection conexion = new MySqlConnection(conexionMysql.cadena);
                MySqlCommand cmdDetalleFactura = new MySqlCommand("spAgregarDetalleFactura", conexion);
                MySqlCommand cmdRestarProducto = new MySqlCommand("spRestarUnidadesProducto", conexion);
                conexion.Open();

                //Bloque para el registro del detalle de los productos incluidos en la última facturación
                cmdDetalleFactura.CommandType = CommandType.StoredProcedure;
                cmdDetalleFactura.Parameters.Add(new MySqlParameter("codFactura", codUltimaFact));
                cmdDetalleFactura.Parameters.Add(new MySqlParameter("codProducto", codProducto));
                cmdDetalleFactura.Parameters.Add(new MySqlParameter("cantidad", cantidad));
                cmdDetalleFactura.Parameters.Add(new MySqlParameter("totalPorCantidad", totalPorCantidad));
                cmdDetalleFactura.Parameters.Add(new MySqlParameter("descripcionProd", descripcionProd));
                cmdDetalleFactura.Parameters.Add(new MySqlParameter("valorRegistro", valorRegistro));
                cmdDetalleFactura.ExecuteNonQuery();

                //Bloque para la modificación (resta) de las unidades de cada producto selecciónado en el detalle
                cmdRestarProducto.CommandType = CommandType.StoredProcedure;
                cmdRestarProducto.Parameters.Add(new MySqlParameter("codProducto", codProducto));
                cmdRestarProducto.Parameters.Add(new MySqlParameter("cantidad", cantidad));
                cmdRestarProducto.ExecuteNonQuery();

                conexion.Close();
                conexion.Dispose();
            }
            catch(MySqlException ex)
            {
                MessageBox.Show("Error en mysql al intentar ingresar uno o más productos en factura: " + ex.Message);
            }
        }


        public DataTable dtBuscarDatosCliente(Int32 codCliente)
        {
            //DataTable dt = new DataTable();
            MySqlConnection conexion = new MySqlConnection(conexionMysql.cadena);
            //conexion.Open();
            MySqlCommand cmdBuscarDatosCliente = new MySqlCommand("spBuscarDatosCliente", conexion);
            conexion.Open();
            cmdBuscarDatosCliente.CommandType = CommandType.StoredProcedure;
            //cmdBuscarValorProducto.Parameters.Add("prodID", MySqlDbType.Int32);
            cmdBuscarDatosCliente.Parameters.Add(new MySqlParameter("codCliente", codCliente));
            //cmdBuscarValorProducto.Parameters["prodID"].Value = codProducto;
            //cmdBuscarValorProducto.ExecuteNonQuery();
            MySqlDataAdapter daDatosCliente = new MySqlDataAdapter(cmdBuscarDatosCliente);
            DataTable dt = new DataTable();
            daDatosCliente.Fill(dt);
            conexion.Dispose();
            return dt;

        }



        public DataTable dtExisteRutTelEmail(Int32 rut, Int32 telefono, String email)
        {
            //DataTable dt = new DataTable();
            MySqlConnection conexion = new MySqlConnection(conexionMysql.cadena);
            //conexion.Open();
            MySqlCommand cmdBuscarExistencia = new MySqlCommand("spListaExisteRutTelEmail", conexion);
            conexion.Open();
            cmdBuscarExistencia.CommandType = CommandType.StoredProcedure;
            cmdBuscarExistencia.Parameters.Add(new MySqlParameter("rutNew", rut));
            cmdBuscarExistencia.Parameters.Add(new MySqlParameter("telNew", telefono));
            cmdBuscarExistencia.Parameters.Add(new MySqlParameter("emailNew", email));
            MySqlDataAdapter daExistencia = new MySqlDataAdapter(cmdBuscarExistencia);
            DataTable dt = new DataTable();
            daExistencia.Fill(dt);
            conexion.Dispose();
            return dt;

        }



        public DataTable dtListaMediosPago()
        {
            MySqlConnection conexion = new MySqlConnection(conexionMysql.cadena);
            //conexion.Open();
            MySqlCommand cmdListaMediosPago = new MySqlCommand("spListaMediosPago", conexion);
            conexion.Open();
            cmdListaMediosPago.CommandType = CommandType.StoredProcedure;
            MySqlDataAdapter daListaMediosPago = new MySqlDataAdapter(cmdListaMediosPago);
            DataTable dt = new DataTable();
            daListaMediosPago.Fill(dt);
            conexion.Close();
            return dt;
        }


        public DataTable dtConsultarFacturasPorCliente(String buscador)
        {
            MySqlConnection conexion = new MySqlConnection(conexionMysql.cadena);
            //conexion.Open();
            MySqlCommand cmdConsultaFactsPorCliente = new MySqlCommand("spConsultarFacturaPorCliente", conexion);
            conexion.Open();
            cmdConsultaFactsPorCliente.CommandType = CommandType.StoredProcedure;
            cmdConsultaFactsPorCliente.Parameters.Add(new MySqlParameter("buscador", buscador));
            MySqlDataAdapter daConsultarFactsPorCliente = new MySqlDataAdapter(cmdConsultaFactsPorCliente);
            DataTable dt = new DataTable();
            daConsultarFactsPorCliente.Fill(dt);
            conexion.Dispose();
            //conexion.Close();
            return dt;
        }








        public DataTable dtListaDetalleFacturaSegunCodigo(Int32 codFactura)
        {
            MySqlConnection conexion = new MySqlConnection(conexionMysql.cadena);
            //conexion.Open();
            MySqlCommand cmdListaDetalle = new MySqlCommand("spListaDetalleSegunFactura", conexion);
            conexion.Open();
            cmdListaDetalle.CommandType = CommandType.StoredProcedure;
            cmdListaDetalle.Parameters.Add(new MySqlParameter("codFactura", codFactura));
            MySqlDataAdapter daListaDetalle = new MySqlDataAdapter(cmdListaDetalle);
            DataTable dt = new DataTable();
            daListaDetalle.Fill(dt);
            conexion.Dispose();
            //conexion.Close();
            return dt;
        }


        public DataTable dtBuscarDatosFacturaYCliente(Int32 codFactura)
        {
            MySqlConnection conexion = new MySqlConnection(conexionMysql.cadena);
            //conexion.Open();
            MySqlCommand cmdListaFacturaYCliente = new MySqlCommand("spBuscarDatosFacturaYClienteSegunCodigo", conexion);
            conexion.Open();
            cmdListaFacturaYCliente.CommandType = CommandType.StoredProcedure;
            cmdListaFacturaYCliente.Parameters.Add(new MySqlParameter("codFactura", codFactura));
            MySqlDataAdapter daListaFacturaYCliente = new MySqlDataAdapter(cmdListaFacturaYCliente);
            DataTable dt = new DataTable();
            daListaFacturaYCliente.Fill(dt);
            conexion.Dispose();
            //conexion.Close();
            return dt;
        }


        public void anularFactura(Int32 codFactura)
        {
            try
            {
                MySqlConnection conexion = new MySqlConnection(conexionMysql.cadena);
                MySqlCommand cmdAnularFactura = new MySqlCommand("spAnularFacturaSegunCodigo", conexion);
                conexion.Open();
                cmdAnularFactura.CommandType = CommandType.StoredProcedure;
                cmdAnularFactura.Parameters.Add(new MySqlParameter("codFactura", codFactura));
                cmdAnularFactura.ExecuteNonQuery();
                conexion.Close();
                conexion.Dispose();
            }
            catch (MySqlException ex)
            {
                MessageBox.Show("Error al intentar anular una factura: " + ex.Message);
            }
        }


        public DataTable dtListaProductosTodo()
        {
            MySqlConnection conexion = new MySqlConnection(conexionMysql.cadena);
            //conexion.Open();
            MySqlCommand cmdListaProdsTodo = new MySqlCommand("spListaProductosTodo", conexion);
            conexion.Open();
            cmdListaProdsTodo.CommandType = CommandType.StoredProcedure;
            MySqlDataAdapter daListaProdsTodo = new MySqlDataAdapter(cmdListaProdsTodo);
            DataTable dt = new DataTable();
            daListaProdsTodo.Fill(dt);
            conexion.Close();
            return dt;
        }

    }
}
