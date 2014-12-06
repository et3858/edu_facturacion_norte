using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

using System.Globalization;
using System.Threading;
using System.Xml;
using System.Diagnostics;
using System.Drawing.Printing;

//iTextSharp
using System.IO; //para el manejo de archivos
using iTextSharp;
using iTextSharp.text.pdf;
//using iTextSharp.text.xml;
//using iTextSharp.text.xml.simpleparser;
using iTextSharp.text.html.simpleparser;
using iTextSharp.text;

namespace facturacion_norte
{
    public partial class formNuevaFactura : Form
    {
        clConexion conexionMysql = new clConexion();

        DataTable dt = new DataTable();
        
        DataColumn colProductoID = new DataColumn("ID");
        DataColumn colProducto = new DataColumn("Producto");
        DataColumn colValorProducto = new DataColumn("Valor unitario");
        DataColumn colCantidad = new DataColumn("Cantidad");
        DataColumn colTotalPorCantidad = new DataColumn("Total por cantidad");


        Int32 codFacturaParaXml;



        clSentencias sentencias = new clSentencias();

        /*
        public ComboBox listadoProductos(ComboBox recibe)
        {
            recibe.DataSource = sentencias.dtListaProductos();
            recibe.ValueMember = "prodid";
            recibe.DisplayMember = "proddescripcion";
            return recibe;
        }
        */


        public void comboboxProductos() {
            DataTable dtProductos = new DataTable();
            MySqlConnection conexion = new MySqlConnection(conexionMysql.cadena);
            conexion.Open();
            dtProductos = sentencias.dtListaProductos();
            //int cod2 = Convert.ToInt32(dtProductos.Columns[0]);
            
            cbProducto.DataSource = dtProductos;
            cbProducto.DisplayMember = dtProductos.Columns[1].ColumnName;
            cbProducto.ValueMember = dtProductos.Columns[0].ColumnName.ToString();
            //cbProducto.AutoCompleteMode = AutoCompleteMode.SuggestAppend;            
            cbProducto.AutoCompleteSource = AutoCompleteSource.ListItems;            
            conexion.Close();
            //Int32 codProducto = Convert.ToInt32(dtProductos.Columns[0]);
            //valorProducto(cod2);
        }

        public void comboboxClientes() {
            DataTable dtClientes = new DataTable();
            MySqlConnection conexion = new MySqlConnection(conexionMysql.cadena);
            conexion.Open();
            dtClientes = sentencias.dtListaClientes();

            //Int32 contador = 0;


            for (Int32 i = 0; i < dtClientes.Rows.Count; i++)
            {
                if (dtClientes.Rows[i][1] == null || dtClientes.Rows[i][1].ToString() == "") {
                    dtClientes.Rows.RemoveAt(i);
                }
                //dtClientes.Rows.RemoveAt(contador);
                //contador = contador + 1;
            }

            cbClientes.DataSource = dtClientes;
            cbClientes.DisplayMember = dtClientes.Columns[1].ColumnName;
            cbClientes.ValueMember = dtClientes.Columns[0].ColumnName.ToString();
            //cbClientes.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            cbClientes.AutoCompleteSource = AutoCompleteSource.ListItems;
            conexion.Close();
        }

        
        public void valorProducto(Int32 codProducto) {
            //Int32 codProducto;
            DataTable dtValor = new DataTable();
            MySqlConnection conexion = new MySqlConnection(conexionMysql.cadena);
            conexion.Open();
            dtValor = sentencias.dtBuscarValorProducto(codProducto);
            txtValor.Text = "";
            txtValor.Text = dtValor.Rows[0][0].ToString();
            txtUnidades.Text = dtValor.Rows[0][1].ToString();
            conexion.Close();         
        }

        public void comboboxMediosPago()
        {
            DataTable dtMediosPago = new DataTable();
            MySqlConnection conexion = new MySqlConnection(conexionMysql.cadena);
            conexion.Open();
            dtMediosPago = sentencias.dtListaMediosPago();
            cbMediosPago.DataSource = dtMediosPago;
            cbMediosPago.DisplayMember = dtMediosPago.Columns[1].ColumnName;
            cbMediosPago.ValueMember = dtMediosPago.Columns[0].ColumnName.ToString();
            //cbMediosPago.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            cbMediosPago.AutoCompleteSource = AutoCompleteSource.ListItems;
            conexion.Close();
        }




        public formNuevaFactura()
        {
            InitializeComponent();
            //DataRow dr = dt.NewRow();
            dt.Columns.Add(colProductoID);
            dt.Columns.Add(colProducto);
            dt.Columns.Add(colValorProducto);
            dt.Columns.Add(colCantidad);
            dt.Columns.Add(colTotalPorCantidad);
            dgvDetalleFactura.DataSource = dt;

        }

        public void limpiarCampos()
        {
            //cbProducto.Text = "";
            txtUnidades.Text = "";
            txtCantidad.Text = "";
            txtValor.Text = "";
            cbProducto.SelectedIndex = -1;
        }

        public void limpiarDetallesYTotales()
        {
            int contadorFilas = dgvDetalleFactura.RowCount - 1;

            //for (int i = 0; i < contadorFilas; i++) //la razón de por qué i es igual a -1 es porque necesita tomar en cuenta a la primera fila para borrarlo. Si fuera un cero, la primera fila no se borraría
            for (int i = -1; i < contadorFilas; i++)
            {
                dgvDetalleFactura.Rows.Remove(dgvDetalleFactura.CurrentRow);

            }


            txtSubTotal.Text = "";
            txtIVA.Text = "";
            txtTotal.Text = "";
        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            if (txtCantidad.Text.Length > 0)
            {
                if (Convert.ToInt32(txtCantidad.Text) > Convert.ToInt32(txtUnidades.Text))
                {
                    MessageBox.Show("La cantidad a llevar no debe sobrepasar a las unidades disponibles del producto seleccionado");
                    txtCantidad.Text = "";
                    txtCantidad.Focus();
                }else if(Convert.ToInt32(txtCantidad.Text) > 20){
                    MessageBox.Show("La cantidad a llevar de tal producto no debe superar las 20 unidades");
                    txtCantidad.Text = "";
                    txtCantidad.Focus();
                }else if (Convert.ToInt32(txtCantidad.Text) == 0){
                    MessageBox.Show("Debe ser al menos 1 unidad a llevar");
                    txtCantidad.Text = "";
                    txtCantidad.Focus();
                }else{    

                    DataRow dr = dt.NewRow();
                    //DataRowd r = new DataRow();
                    //dr["producto"] = Convert.ToString(cbProducto.Text);
                    dr[colProductoID] = Convert.ToInt32(cbProducto.SelectedValue);
                    dr[colProducto] = Convert.ToString(cbProducto.Text);
                    //dr[colCantidad] = cbProducto.SelectedValue;
                    Int32 valor = Convert.ToInt32(txtValor.Text);
                    Int32 cantidad = Convert.ToInt32(txtCantidad.Text);
                    //dr["cantidad"] = Convert.ToInt32(txtCantidad.Text);
                    dr[colValorProducto] = Convert.ToInt32(txtValor.Text);
                    dr[colCantidad] = Convert.ToInt32(txtCantidad.Text);
                    //dr["totalPorCantidad"] = valor * cantidad;
                    dr[colTotalPorCantidad] = valor * cantidad;

                    //la variable comprobarFilas ayuda a identificar un mismo producto con diferentes unidades, 
                    //y así evitar una nueva línea con el mismo producto y, sin embargo, con unidades diferentes.
                    //en otras palabras, omprueba si existe un nuevo producto que no esté en detalle
                    bool comprobarFilas = true;

                    if (dgvDetalleFactura.Rows.Count > 0){
                        //MessageBox.Show("A comparar");
                        foreach (DataGridViewRow compruebaProds in dgvDetalleFactura.Rows)
                        {
                            //MessageBox.Show(Convert.ToString(dr[colProductoID]));
                            if (dr[colProductoID].Equals(compruebaProds.Cells[0].Value))
                            {
                                if (Convert.ToInt32(compruebaProds.Cells[3].Value) + Convert.ToInt32(dr[colCantidad]) > Convert.ToInt32(txtUnidades.Text))
                                {
                                    //MessageBox.Show("La cantidad a llevar no debe sobrepasar a las unidades disponibles del producto seleccionado");
                                    MessageBox.Show("No se puede sumar la nueva cantidad del producto a ingresar porque está sobrepasando el stock disponible del producto en cuestión");
                                    comprobarFilas = false;
                                }
                                else
                                {
                                    compruebaProds.Cells[3].Value = Convert.ToInt32(compruebaProds.Cells[3].Value) + Convert.ToInt32(dr[colCantidad]);
                                    compruebaProds.Cells[4].Value = Convert.ToInt32(compruebaProds.Cells[4].Value) + Convert.ToInt32(dr[colTotalPorCantidad]);
                                    comprobarFilas = false;
                                }
                                    
                            }                            
                        }
                    }

                    //en esta sentencia, si comprobanteFilas dice que no se ha encontrado una nueva fila de un mismo producto
                    //entonces permitirá que se cree una nueva fila para un producto totalmente diferente
                    if (comprobarFilas)
                    {
                        dt.Rows.Add(dr);
                    }

                    //dgvDetalleFactura.AutoGenerateColumns = false;
                    dgvDetalleFactura.DataSource = dt;
                    dgvDetalleFactura.Update();
                    Int32 sumatoria = 0;

                    foreach (DataGridViewRow filas in dgvDetalleFactura.Rows)
                    {
                        sumatoria += Convert.ToInt32(filas.Cells[4].Value);
                        //txtSubTotal.Text = sumatoria.ToString();
                    }

                    txtSubTotal.Text = sumatoria.ToString();
                    txtCantidad.ReadOnly = true;
                    btnAgregar.Enabled = false;
                    limpiarCampos();
                    cbProducto.Focus();
                }
            }
            else {
                MessageBox.Show("Por favor coloque la cantidad a llevar de ese producto");            
            }
        }
        
        private void formNuevaFactura_Load(object sender, EventArgs e)
        {
            //listadoProductos(cbProducto);
            //cbProducto.SelectedIndex = 1; //(el problema)
            comboboxClientes();
            comboboxProductos();
            comboboxMediosPago();

            cbProducto.SelectedIndex = -1;
            cbClientes.SelectedIndex = -1;
            cbMediosPago.SelectedIndex = -1;

            txtValor.Text = "";
            txtCantidad.Text = "";


            lblFecha.Text = DateTime.Today.ToString("dd-MM-yyyy");
            //lblHora.Text = DateTime.UtcNow.ToString("H:mm:ss");
            
            //lblHora.Text = DateTime.Now.ToLongTimeString().ToString(cul);
            lblHora.Text = DateTime.Now.ToString("H:mm:ss", new CultureInfo("en-GB"));

        }

        //String codProd;

        private void cbProducto_SelectedIndexChanged(object sender, EventArgs e)
        {



            //txtValor.Text = cbProducto.SelectedValue.ToString();
            //MySqlConnection conexion2 = new MySqlConnection(conexionMysql.cadena);
            //conexion2.Open();            
            //Int32 codProducto = Convert.ToInt32(cbProducto.SelectedValue);
            //DataTable dtValor = new DataTable();
            //dtValor = sentencias.dtBuscarValorProducto(Convert.ToInt32(cbProducto.SelectedValue));
            //Int32 codProducto = Convert.ToInt32(cbProducto.SelectedValue.ToString());

            //codProd = cbProducto.SelectedValue.ToString();




            //limpiarCampos();

        }

        private void btnConsultar_Click(object sender, EventArgs e)
        {
            //label5.Text = cbProducto.SelectedValue.ToString();
            //Int32 codProducto = Convert.ToInt32(label5.Text);
            //if (cbProducto.Text.Length > 0)
            if (cbProducto.SelectedValue != null)
            {
                Int32 codProducto = Convert.ToInt32(cbProducto.SelectedValue);
                valorProducto(codProducto);
                txtCantidad.ReadOnly = false;
                txtCantidad.Focus();
                btnAgregar.Enabled = true;
            }
            else {
                MessageBox.Show("Por favor seleccione el producto en cuestión");
            }
        }

        private void txtSubTotal_TextChanged(object sender, EventArgs e)
        {
            double iva = 0;
            if(txtSubTotal.Text.Length > 0){
                iva = Convert.ToDouble(txtSubTotal.Text) * 0.19;
                txtIVA.Text = Convert.ToString(Convert.ToUInt32(iva));
            }

        }

        private void txtIVA_TextChanged(object sender, EventArgs e)
        {
            double total = 0;
            if (txtIVA.Text.Length > 0){
                total = Convert.ToDouble(txtSubTotal.Text) + Convert.ToDouble(txtIVA.Text);
                txtTotal.Text = Convert.ToString(Convert.ToInt32(total));
            }
        }

        private void btnLimpiarTabla_Click(object sender, EventArgs e)
        {
            //dgvDetalleFactura.DataSource = dt;
            //dgvDetalleFactura.Rows.Clear();
            
            //dgvDetalleFactura.Rows.Remove(dgvDetalleFactura.CurrentRow);
            

            int contadorFilas = dgvDetalleFactura.RowCount - 1;

            //for (int i = 0; i < contadorFilas; i++) //la razón de por qué i es igual a -1 es porque necesita tomar en cuenta a la primera fila para borrarlo. Si fuera un cero, la primera fila no se borraría
            for (int i = -1; i < contadorFilas; i++)
            {
                dgvDetalleFactura.Rows.Remove(dgvDetalleFactura.CurrentRow);
                
            }


            txtSubTotal.Text = "";
            txtIVA.Text = "";
            txtTotal.Text = "";

        }

        private void btnRemover_Click(object sender, EventArgs e)
        {
            if(dgvDetalleFactura.RowCount > 0){
                dgvDetalleFactura.Rows.Remove(dgvDetalleFactura.CurrentRow);
                txtSubTotal.Text = "";
                txtIVA.Text = "";
                txtTotal.Text = "";

                Int32 sumatoria = 0;

                foreach (DataGridViewRow filas in dgvDetalleFactura.Rows)
                {
                    sumatoria += Convert.ToInt32(filas.Cells[4].Value);
                    //txtSubTotal.Text = sumatoria.ToString();
                }

                txtSubTotal.Text = sumatoria.ToString();
            }else{
                MessageBox.Show("Ya no hay productos por borrar");
            }
        }

        private void btnNuevaTransaccion_Click(object sender, EventArgs e)
        {
            if (dgvDetalleFactura.RowCount > 0 && cbClientes.SelectedValue != null && cbMediosPago.SelectedValue != null){
                //MessageBox.Show("Hay filas, así que a esperar para hacer la transacción");
                Int32 codCliente = Convert.ToInt32(cbClientes.SelectedValue.ToString());
                Int32 subTotal = Convert.ToInt32(txtSubTotal.Text);
                Int32 iva = Convert.ToInt32(txtIVA.Text);
                Int32 total = Convert.ToInt32(txtTotal.Text);
                Int32 codMedioPago = Convert.ToInt32(cbMediosPago.SelectedValue.ToString());
                String desMedioPago = cbMediosPago.Text;
                //MessageBox.Show(desMedioPago);

                DialogResult resultadoDialogo = MessageBox.Show("¿Está seguro de realizar una nueva transacción?", "Facturación", MessageBoxButtons.YesNo);
                if (resultadoDialogo == DialogResult.Yes)
                {
                    //MessageBox.Show("Prueba de aceptación");
                    sentencias.agregarNuevaFactura(codCliente, subTotal, iva, total, codMedioPago);
                    //MessageBox.Show("Facturación concretada");
                    DataTable dtCodFactura = new DataTable();
                    MySqlConnection conexion = new MySqlConnection(conexionMysql.cadena);
                    conexion.Open();
                    dtCodFactura = sentencias.ultimoRegistroFactura();
                    Int32 codFactura = Convert.ToInt32(dtCodFactura.Rows[0][0].ToString());
                    String fechaRegistroFactura = dtCodFactura.Rows[0][1].ToString();
                    String horaRegistroFactura = dtCodFactura.Rows[0][2].ToString();
                    codFacturaParaXml = codFactura;//la variable codFacturaParaXml es para usarla como experimento para el registro de la misma en el documento xml

                    foreach (DataGridViewRow filas in dgvDetalleFactura.Rows)
                    {
                        sentencias.agregarDetalleFactura(codFactura, Convert.ToInt32(filas.Cells[0].Value), Convert.ToInt32(filas.Cells[3].Value), Convert.ToInt32(filas.Cells[4].Value), Convert.ToString(filas.Cells[1].Value), Convert.ToInt32(filas.Cells[2].Value));
                    }
                    MessageBox.Show("Facturación concretada");
                    /*
                    int contadorFilas = dgvDetalleFactura.RowCount - 1;

                    for (int i = 0; i < contadorFilas; i++)
                    {
                        //dgvDetalleFactura.Rows.Remove(dgvDetalleFactura.CurrentRow);
                    
                        //String[] fila = new String[]{dgvDetalleFactura.CurrentRow.ToString()};
                        //sentencias.agregarDetalleFactura(codFactura, Convert.ToInt32(fila[0]), Convert.ToInt32(fila[2]), Convert.ToInt32(fila[3]));
                        String codProducto = Convert.ToString(dgvDetalleFactura.CurrentRow.);
                    }
                    */


                    /*
                    if (cbProducto.Text.Length > 0)
                    {
                        Int32 codProducto = Convert.ToInt32(cbProducto.SelectedValue);
                        valorProducto(codProducto);
                        txtCantidad.ReadOnly = false;
                        txtCantidad.Focus();
                        btnAgregar.Enabled = true;
                    }
                    else
                    {
                        MessageBox.Show("Por favor seleccione el producto en cuestión");
                    }
                    */

                    Int32 idCliente = Convert.ToInt32(cbClientes.SelectedValue);
                    //valorProducto(codProducto);
                    DataTable dtDatosCliente = new DataTable();
                    dtDatosCliente = sentencias.dtBuscarDatosCliente(idCliente);
                    //String nombreCliente = dtDatosCliente.Rows[0][2].ToString().ToUpper();
                    //txtCantidad.ReadOnly = false;
                    //txtCantidad.Focus();
                    //btnAgregar.Enabled = true;

                    imprimirFactura(codFactura, dtDatosCliente, fechaRegistroFactura, horaRegistroFactura, desMedioPago);//Imprime en un formato de papel más amplio (Carta, Oficio, lo que sea)
                    imprimirFacturaCompacto(codFactura, dtDatosCliente, fechaRegistroFactura, horaRegistroFactura, desMedioPago);//Imprime en un formato de papel más corto (por ejemplo A6)
                    //imprimirComprobanteXmlSii(codFactura, dtDatosCliente);



                    //formPrincipal principal = new formPrincipal();
                    //principal.Refresh();
                    //principal.listafacturas();

                    conexion.Close();

                    limpiarCampos();
                    limpiarDetallesYTotales();

                    cbClientes.SelectedIndex = -1;
                    cbMediosPago.SelectedValue = -1;

                }
                
            }
            else{
                String noProducto = "No tienes agregado algún producto";
                String noCliente = "No tienes a un cliente seleccionado";
                String noOrdenCompra = "No hay un orden de compra seleccionado";
                MessageBox.Show("Si no se está facturando, estos pueden ser los siguientes problemas:\r\n\r\n" + noProducto + "\r\n" + noCliente + "\r\n" + noOrdenCompra);
            }
        }

        private void btnImprimir_Click(object sender, EventArgs e)
        {
            //se crea un nuevo archivo que solo se cargará en la memoria principal
            Document documento = new Document(PageSize.A6, 36, 36, 36, 36); //es la nueva instancia para la creación del archivo en pdf

            //se pasa el nuevo documento creado arriba y con capacidad para abrir o crear y de nombre "nuevoDocumento.pdf"
            PdfWriter.GetInstance(documento, new FileStream("nuevoDocumento.pdf", FileMode.OpenOrCreate));

            //se abre el documento
            documento.Open();

            //a partir de aqui se generará un registro de los detalles de cada producto para una nueva factura
            documento.Add(new Paragraph("Detalle de factura"));



            
            foreach (DataGridViewRow filas in dgvDetalleFactura.Rows) {
                //Paragraph parrafo = new Paragraph();
                String nombreProducto = filas.Cells[1].Value.ToString();
                String cantidadProducto = filas.Cells[3].Value.ToString();
                String totalCantidad = filas.Cells[4].Value.ToString();

                //se agregar un párrafo
                documento.Add(new Paragraph(nombreProducto + " " + cantidadProducto + " " + totalCantidad));
                
            }
            //fin de registro de los detalles de cada producto para  una nueva factura

            //se cierra el documento
            documento.Close();
        }


        //función para la impresión de comprobante
        public void imprimirFactura(Int32 codFactura, DataTable dtDatosCliente, String fechaRegistro, String horaRegistro, String medioPago) {
            //String fecha = DateTime.Today.ToString("yyyy-MM-dd");//fecha, se contempla hoy
            String[] fecha2 = fechaRegistro.Split('-');
            String hora = horaRegistro.Replace(":","-");//hora, se contempla ahora
            //precaución: no reeplazar algún signo que separes las unidades de tiempo por dos puntos(:), da errores
            //String ruta = @"facturas\\" + fecha + "_" + hora + ".pdf";
            String ruta = @"facturas\formatoGrande\" + fechaRegistro + "___" + hora + ".pdf";

            String articuloLegal = "El acuse de recibo que se declara en este acto de acuerdo a lo dispuesto en la letra b) Art. 4 y letra c) Art. 5 de la Ley 19.963 acredita que la entrega de mercader&iacute;as o servicio(s) prestado(s) ha(n) sido recibido(s).";

            String subTotal = txtSubTotal.Text;
            String iva = txtIVA.Text;
            String total = txtTotal.Text;

            //variables a nombre
            String nombreAImprimir = "";
            //String nombrePersona = dtDatosCliente.Rows[0][2].ToString();
            //String nombreEmpresa = dtDatosCliente.Rows[0][7].ToString();
            if (dtDatosCliente.Rows[0][2].ToString() == "")
            {
                nombreAImprimir = dtDatosCliente.Rows[0][7].ToString();
            }
            else
            {
                nombreAImprimir = dtDatosCliente.Rows[0][2].ToString();
            }

            //captura el giro de la empresa si es que está
            String giroAImprimir = "";
            if (dtDatosCliente.Rows[0][8].ToString() != "")
            {
                giroAImprimir = dtDatosCliente.Rows[0][8].ToString();
            }
            else
            {
                giroAImprimir = "-";
            }

            //variables que se utilizarán para dar como resultado el nombre del número
            Double numeroANombre = Convert.ToDouble(subTotal);
            String resultadoNumeroAnombre = toText(numeroANombre);

            //Bloque clave para el lordenamiento de la impresión de los detalles por páginas
            Double divisor = Convert.ToDouble(dgvDetalleFactura.RowCount) / 10;
            
            //MessageBox.Show(Convert.ToString(Convert.ToInt32(divisor + 1)));

            String[,] matrizDetalleFactura = null;
            Int32 indizadorFilas = 0;
            //Int32 restodivi = dgvDetalleFactura.Rows.Count % 10;
            //MessageBox.Show(restodivi.ToString());

            if(dgvDetalleFactura.Rows.Count % 10 == 0)
            {
                matrizDetalleFactura = new String[Convert.ToInt32(divisor), 10];
                indizadorFilas = Convert.ToInt32(divisor);
            }
            else
            {
                matrizDetalleFactura = new String[Convert.ToInt32(divisor) + 1, 10];
                indizadorFilas = Convert.ToInt32(divisor) + 1;
            }


            //String[,] matrizDetalleFactura = new String[Convert.ToInt32(divisor) + 1, 10];

            Int32 cuentaFilas = 0;
            Int32 cuentaCols = 0;

            //Document documento = new Document();
            //PdfWriter.GetInstance(documento, new FileStream(ruta, FileMode.Create));

            foreach (DataGridViewRow filas2 in dgvDetalleFactura.Rows)
            {
                String cantProd2 = filas2.Cells[3].Value.ToString();
                String descProd2 = filas2.Cells[1].Value.ToString();
                String valorProd2 = filas2.Cells[2].Value.ToString();
                String totalCantProd2 = filas2.Cells[4].Value.ToString();
                String filaDetalleProd = cantProd2 + "|" + descProd2 + "|" + valorProd2 + "|" + totalCantProd2;
                //MessageBox.Show(filaDetalleProd);
                matrizDetalleFactura[cuentaFilas, cuentaCols] = filaDetalleProd;
                cuentaCols++;
                if (cuentaCols >= 10)
                {
                    cuentaFilas++;
                    cuentaCols = 0;
                }
            }
            
            
            //MessageBox.Show(matrizDetalleFactura[Convert.ToInt32(divisor + 1), 10]);


            Document documento = new Document();
            //BaseFont fuente = BaseFont.CreateFont(BaseFont);
            //FontFactory myFont = FontFactory.GetFont("Verdana", 12);

            //iTextSharp.text.FontFactory.Register(@"C:\Windows\Fonts\verdana.ttf", "verdana");
            iTextSharp.text.FontFactory.Register(@"fonts\verdana.ttf", "verdana");
            //iTextSharp.text.Font myFont = new iTextSharp.text.Font(FontFactory.GetFont("verdana", 10));
            //iTextSharp.text.Font myFont = new iTextSharp.text.Font(FontFactory.GetFont(@"C:\Windows\Fonts\verdana.ttf", 10));
            //iTextSharp.text.Font fuente2 = new iTextSharp.text.Font(FontFactory.Register(@"C:\windows\fonts\verdana.ttc", "Verdana"));
            //iTextSharp.text.html.simpleparser.StyleSheet estilosHtml = new iTextSharp.text.html.simpleparser.StyleSheet();
            //estilosHtml.LoadTagStyle("body", "font-family", "Times New Roman");
            PdfWriter.GetInstance(documento, new FileStream(ruta, FileMode.Create));

            //documento.Open();

            //for (Int32 h = 0; h < Convert.ToInt32(divisor); h++)
            //{


            //documento.Open();

            //for (Int32 h = 1; h <= divisor; h++)
            //{                
                //matrizDetalleFactura[h,1] = dgvDetalleFactura.Rows[][];
            //String html = "<html><head></head><body>jddnndund2";
            //String html2 = "<br />fin..............</body></html>";
            String htmlInicio = "<html><head></head><body><font face=\"verdana\">";
            
            String htmlCabecera = "<div id=\"contenedor\" width=\"500\" style=\"font-size: 0.8em; margin: 0.5em auto; background: yellow; padding: 1em; width: 800px;\">" +
                    "<table border=\"0\" width=\"100%\">" +
                    "<tr>" + "<td>" + "<!--<h3>" +
                    "<b>EDUARDO ANTONIO AR&Eacute;VALO ZAMBRANO</b><br />" +
                    "<b>PRESTACIONES DE SERVICIOS</b><br />" +
                    "<b>DISE&Ntilde;O GR&Aacute;FICO</b><br />" + "</h3>" +
                    "<h1>AREZA</h1>" +
                    "<h3>" + "<p>Avelino Villagran 2266 - Fono: 242546<br />" + "Arica - (Unknown country)</p>" +
                    "</h3>-->" + "</td>" +
                    "<td>" + "<article style=\"border: green solid 1em; padding: 1em;\">" + "<h2>" +
                    "<b><font face=\"verdana\">R.U.T.: 7.664.511-6</font></b><br />" +
                    "<b><font face=\"verdana\">COTIZACI&Oacute;N</font></b><br />" +
                    "<b><font face=\"verdana\">N&deg; " + Convert.ToString(codFactura) + "</font></b>" + "</h2>" + "</article>" +
                    "<h3><font face=\"verdana\">" + "S.I.I. - Arica<br />" + "Fecha de emision: " + fecha2[2] + "-" + fecha2[1] + "-" + fecha2[0] + "</font></h3>" +
                    "</td>" + "</tr>" + "</table><br /><br />";

            String htmlTabla = "<table border=\"0\" width=\"100%\" height=\"1\">" +
                    "<tr>" +
                    //"<td colspan=\"3\" width=\"65%\">Se&ntilde;or(es)<br /></td>" +
                    "<td colspan=\"6\" width=\"65%\"><font face=\"verdana\">" + nombreAImprimir.ToUpper() + "</font></td>" +
                    //"<td>D&iacute;a: " + fecha2[2] + "</div></td>" +
                    //"<td>Mes: " + fecha2[1] + "</td>" +
                    //"<td>A&ntilde;o: " + fecha2[0] + "</td>" +
                    "<td colspan=\"3\"><!--Fecha: --><font face=\"verdana\">" + fecha2[2] + " - " + fecha2[1] + " - " + fecha2[0] + "</font></td>" +
                    "</tr>" +
                    "<tr>" +
                    //"<td colspan=\"3\">Direcci&oacute;n: AVELINO VILLAGRAM 2266<br /></td>" +
                    "<td colspan=\"6\"><!--Direcci&oacute;n: --><font face=\"verdana\">" + dtDatosCliente.Rows[0][3].ToString().ToUpper() + "</font></td>" +
                    "<td colspan=\"3\"><font face=\"verdana\"><!--R.U.T. -->" + dtDatosCliente.Rows[0][0].ToString() + "-" + dtDatosCliente.Rows[0][1].ToString() + "</font></td>" +
                    "</tr>" +
                    "<tr>" +
                    "<td colspan=\"6\"><font face=\"verdana\"><!--Giro: -->" + giroAImprimir.ToUpper() + "</font></td>" +
                    "<td colspan=\"3\"><!--Ciudad: --><font face=\"verdana\">" + dtDatosCliente.Rows[0][5].ToString().ToUpper() + "</font></td>" +
                    "</tr>" +
                    "<tr>" +
                    "<td colspan=\"2\"><font face=\"verdana\"><!--Gu&iacute;a despacho N&deg; -->" + Convert.ToString(codFactura) + "</font></td>" +
                    "<td colspan=\"2\"><!--Condiciones de venta:<br />--><font face=\"verdana\">" + medioPago + "</font></td>" +
                    "<td colspan=\"2\"><font face=\"verdana\"><!--Orden de compra N&deg; -->" + Convert.ToString(codFactura) + "</font></td>" +
                    "<td colspan=\"3\"><font face=\"verdana\"><!--Fono: -->" + dtDatosCliente.Rows[0][6].ToString() + "</font></td>" +
                    "</tr>" +
                    "</table><br />" +
                    "<table border=\"0\" width=\"100%\" height=\"1\"><tr><td colspan=\"2\"><font face=\"verdana\">Por lo siguiente:</font></td>" +
                    "<td colspan=\"5\" align=\"center\"><font face=\"verdana\">" + nombreAImprimir.ToUpper() + "</font></td>" +
                    "<td><font face=\"verdana\">DEBE</font></td></tr></table>";

            String htmlDetallesInicio = "<table border=\"0\" width=\"100%\">" + "<tr>" +
                    "<td><font face=\"verdana\">Cantidad</font></td>" +
                    "<td colspan=\"2\"><font face=\"verdana\">Detalle</font></td>" +
                    //"<td></td>" +
                    "<td align=\"right\"><font face=\"verdana\">Valor Unitario</font></td>" +
                    "<td align=\"right\"><font face=\"verdana\">Total</font></td>" +
                    "</tr></table>";

            String htmlFilaInicio = "<table border=\"0\" width=\"100%\"><tr>";
            String htmlFilaProducto = "";
            String htmlFilaFinal = "</tr></table>";
            String htmlDetallesFinal = "<table border=\"0\" width=\"100%\"><tr>" +
                    "<td colspan=\"4\"><font face=\"verdana\">Son: " + resultadoNumeroAnombre + " s.e.u.o.</font></td>" +
                    //"<td></td>" +
                    //"<td></td>" +
                    //"<td></td>" +
                    "<td></td>" +
                    "</tr>" +

                    "<tr>" +
                    //"<td rowspan=\"3\"><h6>" + /*articuloLegal +*/ "</h6></td>" +
                    "<td rowspan=\"3\" colspan=\"2\"><font face=\"verdana\">Nombre:___________________<br />RUT:______________________<br />Recinto:____________________<br />Fecha: __/__/__ Firma:_______</font></td>" +
                    "<td rowspan=\"3\"><font face=\"verdana\">CANCELADO<br /><br />__ de __ de 20__</font></td>" +
                    "<td align=\"right\"><font face=\"verdana\">Sub Total</font></td>" +
                    "<td align=\"right\"><font face=\"verdana\">" + subTotal + "</font></td>" +
                    "</tr>" +

                    "<tr>" +
                    //"<td></td>" +
                    //"<td></td>" +
                    //"<td></td>" +
                    "<td align=\"right\"><font face=\"verdana\">19% de IVA</font></td>" +
                    "<td align=\"right\"><font face=\"verdana\">" + iva + "</font></td>" +
                    "</tr>" +

                    "<tr>" +
                    //"<td></td>" +
                    //"<td></td>" +
                    //"<td></td>" +
                    "<td align=\"right\"><font face=\"verdana\">TOTAL</font></td>" +
                    "<td align=\"right\"><font face=\"verdana\">" + total + "</font></td>" +
                    "</tr><tr><td colspan=\"5\"><h6><font face=\"verdana\">" + articuloLegal + "</font></h6></td></tr>" +
                    "</table>";
            String htmlFinal = "</div></font></body></html>";

            documento.Open();
            //documento.Add(new Paragraph("Times New Roman", myFont));
            for (Int32 h = 0; h < indizadorFilas; h++)
            {
                
                foreach (IElement elementoInicio in HTMLWorker.ParseToList(new StringReader(htmlInicio), new StyleSheet()))
                {                    
                    documento.Add(elementoInicio);
                }
            
                foreach (IElement elementoCabecera in HTMLWorker.ParseToList(new StringReader(htmlCabecera), new StyleSheet()))
                {
                    
                    documento.Add(elementoCabecera);
                }

                //documento.NewPage();
            
                foreach (IElement elementoTabla in HTMLWorker.ParseToList(new StringReader(htmlTabla), new StyleSheet()))
                {
                    documento.Add(elementoTabla);
                }
            
                foreach (IElement elementoTablaDetallesInicio in HTMLWorker.ParseToList(new StringReader(htmlDetallesInicio), new StyleSheet()))
                {
                    documento.Add(elementoTablaDetallesInicio);
                }
            
                //prueba
                Int32 capturaIndiceColumna = 0;
                                
                for (Int32 l = 0; l < 10; l++)
                {
                    capturaIndiceColumna = l;
                    if (matrizDetalleFactura[h, l] == null)
                    {
                        //break;
                        htmlFilaProducto = htmlFilaInicio +
                            "<td><font face=\"verdana\">-</font></td>" +
                            "<td colspan=\"2\"><font face=\"verdana\">-</font></td>" +
                            "<td align=\"right\"><font face=\"verdana\">-</font></td>" +
                            "<td align=\"right\"><font face=\"verdana\">-</font></td>" + htmlFilaFinal;
                    }
                    else
                    {
                        //MessageBox.Show(matrizDetalleFactura[h,l]);
                        String listadoDetalles1 = matrizDetalleFactura[h, l].ToString();
                        //MessageBox.Show(listadoDetalles1);
                        String[] listadoDetalles2 = listadoDetalles1.Split('|');

                        String cantProd2 = listadoDetalles2[0];
                        String nombreProd2 = listadoDetalles2[1];
                        String valorProd2 = listadoDetalles2[2];
                        String totalUnidsProd2 = listadoDetalles2[3];

                        htmlFilaProducto = htmlFilaInicio +
                                "<td><font face=\"verdana\">" + cantProd2 + "</font></td>" +
                                "<td colspan=\"2\"><font face=\"verdana\">" + nombreProd2 + "</font></td>" +
                            //"<td>" + nombreProducto + "</td>" +
                                "<td align=\"right\"><font face=\"verdana\">" + valorProd2 + "</font></td>" +
                                "<td align=\"right\"><font face=\"verdana\">" + totalUnidsProd2 + "</font></td>" + htmlFilaFinal;
                    }                    

                    foreach (IElement elementoTablaDetallesProductos in HTMLWorker.ParseToList(new StringReader(htmlFilaProducto), new StyleSheet()))
                    {
                        documento.Add(elementoTablaDetallesProductos);
                    }

                    //if (matrizDetalleFactura[h, l] == "")
                    //{
                    //    break;
                    //}

                }


                ////lista de detalles de productos en factura
                //foreach (DataGridViewRow filas in dgvDetalleFactura.Rows)
                //{
                //    //Paragraph parrafo = new Paragraph();
                //    String nombreProducto = filas.Cells[1].Value.ToString();
                //    String valorProducto = filas.Cells[2].Value.ToString();
                //    String cantidadProducto = filas.Cells[3].Value.ToString();
                //    String totalCantidad = filas.Cells[4].Value.ToString();

                //    //documento.Add(new Paragraph(nombreProducto + " " + cantidadProducto + " " + totalCantidad));
                //    htmlFilaProducto = htmlFilaInicio +
                //        "<td>" + cantidadProducto + "</td>" +
                //        "<td colspan=\"2\">" + nombreProducto + "</td>" +
                //        //"<td>" + nombreProducto + "</td>" +
                //        "<td align=\"right\">" + valorProducto + "</td>" +
                //        "<td align=\"right\">" + totalCantidad + "</td>" + htmlFilaFinal;

                //    foreach (IElement elementoTablaDetallesProductos in HTMLWorker.ParseToList(new StringReader(htmlFilaProducto), new StyleSheet()))
                //    {
                //        documento.Add(elementoTablaDetallesProductos);
                //    }
                //}



            
                foreach (IElement elementoTablaDetallesFinal in HTMLWorker.ParseToList(new StringReader(htmlDetallesFinal), new StyleSheet()))
                {
                    documento.Add(elementoTablaDetallesFinal);
                }

            
                foreach (IElement elementoFinal in HTMLWorker.ParseToList(new StringReader(htmlFinal), new StyleSheet()))
                {
                    documento.Add(elementoFinal);
                }

                //if (matrizDetalleFactura[h + 1, 0] == "")
                //{
                //    break;
                //}
                if (matrizDetalleFactura[h,capturaIndiceColumna] == null)
                {
                    break;
                }
                else
                {
                    documento.NewPage();
                }
                //documento.NewPage();
                //documento.Close();
            }
            documento.Close();

            
            Process process = new Process();

            String printerName = obtenerImpresora();
            //String printerName = "EPSON TX220 Series";

            process.StartInfo.FileName = ruta;
            process.StartInfo.Verb = "printto";
            process.StartInfo.Arguments = "\"" + printerName + "\"";
            process.Start();

            process.WaitForInputIdle();
            process.Kill();
            
        }


        



        //generar comprobante o factura en formato de imagen más pequeño (A6)
        public void imprimirFacturaCompacto(Int32 codFactura, DataTable dtDatosCliente, String fechaRegistro, String horaRegistro, String medioPago) 
        {
            //String fecha = DateTime.Today.ToString("yyyy-MM-dd");//fecha, se contempla hoy
            String hora = horaRegistro.Replace(":","-");//hora, se contempla ahora
            //precaución: no reeplazar algún signo que separes las unidades de tiempo por dos puntos(:), da errores
            //String ruta = @"facturas\\" + fecha + "_" + hora + ".pdf";
            String ruta = @"facturas\formatoCompacto\" + fechaRegistro + "___" + hora + ".pdf";

            String articuloLegal = "El acuse de recibo que se declara en este acto de acuerdo a lo dispuesto en la letra b) Art. 4 y letra c) Art. 5 de la Ley 19.963 acredita que la entrega de mercader&iacute;as o servicio(s) prestado(s) ha(n) sido recibido(s).";
            String[] fecha2 = fechaRegistro.Split('-');
            //String fechaDia = DateTime.Today.ToString("dd");
            //String fechaMes = DateTime.Today.ToString("MM");
            //String fechaAnyo = DateTime.Today.ToString("yyyy");

            String subTotal = txtSubTotal.Text;
            String iva = txtIVA.Text;
            String total = txtTotal.Text;

            //variables a nombre
            String nombreAImprimir = "";
            //String nombrePersona = dtDatosCliente.Rows[0][2].ToString();
            //String nombreEmpresa = dtDatosCliente.Rows[0][7].ToString();
            if (dtDatosCliente.Rows[0][2].ToString() == "")
            {
                nombreAImprimir = dtDatosCliente.Rows[0][7].ToString();
            }
            else
            {
                nombreAImprimir = dtDatosCliente.Rows[0][2].ToString();
            }

            //captura el giro de la empresa si es que está
            String giroAImprimir = "";
            if (dtDatosCliente.Rows[0][8].ToString() != "")
            {
                giroAImprimir = dtDatosCliente.Rows[0][8].ToString();
            }

            Document documento = new Document(PageSize.A6, 36, 36, 36, 36);
            PdfWriter.GetInstance(documento, new FileStream(ruta, FileMode.Create));

            //String html = "<html><head></head><body>jddnndund2";
            //String html2 = "<br />fin..............</body></html>";
            String htmlInicio = "<html><head></head><body>";

            String htmlCabecera = "<div id=\"contenedor\" width=\"500\" style=\"font-size: 0.8em; margin: 0.5em auto; background: yellow; padding: 1em; width: 800px;\">" +
            "<table border=\"0\" width=\"100%\">" +
            "<tr>" + "<td>" + "<!--<h4>" +
            "<b>EDUARDO ANTONIO AR&Eacute;VALO ZAMBRANO</b><br />" +
            "<b>PRESTACIONES DE SERVICIOS</b><br />" +
            "<b>DISE&Ntilde;O GR&Aacute;FICO</b><br />" + "</h4>" +
            "<h2>AREZA</h2>" +
            "<h5>" + "<p>Avelino Villagran 2266 - Fono: 242546<br />" + "Arica - (Unknown country)</p>" +
            "</h5>-->" +
            "<article style=\"border: green solid 1em; padding: 1em;\">" + "<h4>" +
            "<b>R.U.T.: 7.664.511-6</b><br />" +
            "<b>COTIZACI&Oacute;N N&deg; " + codFactura.ToString() + "</b>" + "</h4>" + "</article>" +
            "<h4>" + "S.I.I. - Arica<br />" + "Fecha de emision: " + fecha2[2] + "-" + fecha2[1] + "-" + fecha2[0] + "</h4>" +
            "</td>" + "</tr>" + "</table>";

            String htmlTabla = "<table border=\"0\" width=\"100%\" height=\"1\">" +
            "<tr><td>" +
            //"Se&ntilde;or(es): EDUARDO ANTONIO AR&Eacute;VALO SALGADO<br />" +
            nombreAImprimir.ToUpper() + "<br />" +
            "R.U.T. " + dtDatosCliente.Rows[0][0].ToString() + "-" + dtDatosCliente.Rows[0][1].ToString() + "<br />" +
            "Fecha: " + fecha2[2] + "-" + fecha2[1] + "-" + fecha2[0] + "<br />" +
                //"<td>" + fechaMes + "</td>" +
                //"<td>" + fechaAnyo + "</td>" +
                        
                //"</tr>" +
            //"Giro: PRESTACIONES DE SERVICIOS DISE&Ntilde;O GR&Aacute;FICO<br />" +
            "Giro: " + giroAImprimir.ToUpper() + "<br />" +
            "Direcci&oacute;n: " + dtDatosCliente.Rows[0][3].ToString().ToUpper() + "<br />" +
            "Ciudad: " + dtDatosCliente.Rows[0][5].ToString().ToUpper() + "<br />" +
                //"</tr>" +
                //"<tr><td>Gu&iacute;a despacho N&uacute;mero<br /></td></tr>" +
                "Condiciones de venta: " + medioPago + "<br />" +
                //"<tr><td>Orden de compra<br /></td></tr>" +
            "Fono: " + dtDatosCliente.Rows[0][6].ToString() + "<br />" +
            "</td></tr>" +
                //"</tr>" +
            "</table><br /><br />";

            String htmlDetallesInicio = "<table border=\"0\" width=\"100%\">" + "<tr>" +
                "<td>Cant</td>" +
                "<td colspan=\"2\">Detalle</td>" +
                //"<td></td>" +
                "<td>Valor Unit</td>" +
                "<td>Total</td>" +
                "</tr></table>";

            String htmlFilaInicio = "<table border=\"0\" width=\"100%\"><tr>";
            String htmlFilaProducto = "";
            String htmlFilaFinal = "</tr></table>";

            String htmlDetallesFinal = "<table border=\"0\" width=\"100%\"><tr>" +
                "<td colspan=\"4\"> <!-- Son:_________ s.e.u.o. --> </td>" +
                //"<td></td>" +
                //"<td></td>" +
                //"<td></td>" +
                "<td></td>" +
                "</tr>" +

                "<tr>" +
                "<td colspan=\"4\">Sub Total</td>" +
                //"<td rowspan=\"3\"> <!-- Nombre: <br />RUT: <br />Recinto: <br />Fecha: __/__/__ Firma:_______ --> </td>" +
                //"<td colspan=\"2\" align=\"right\"><!-- CANCELADO<br /><br />__ de __ de 20__ --> </td>" +
                //"<td align=\"right\">Sub Total</td>" +
                "<td align=\"right\">" + subTotal + "</td>" +
                "</tr>" +

                "<tr>" +
                "<td colspan=\"4\">19% de I.V.A.</td>" +
                //"<td></td>" +
                //"<td colspan=\"2\" align=\"right\">19% de I.V.A.</td>" +
                //"<td align=\"right\">19% de IVA</td>" +
                "<td align=\"right\">" + iva + "</td>" +
                "</tr>" +

                "<tr>" +
                "<td colspan=\"4\">TOTAL</td>" +
                //"<td></td>" +
                //"<td colspan=\"2\" align=\"right\">TOTAL</td>" +
                //"<td align=\"right\">TOTAL</td>" +
                "<td align=\"right\"><b>" + total + "</b></td>" +
                "</tr>" +

                "<tr>" +
                "<td colspan=\"5\">" +
                "<h6>" + articuloLegal + "</h6>" +
                "</td>" +
                "</tr>" +
                "</table>";

            String htmlFinal = "</div></body></html>";
            documento.Open();
            foreach (IElement elementoInicio in HTMLWorker.ParseToList(new StringReader(htmlInicio), new StyleSheet()))
            {
                documento.Add(elementoInicio);
            }

            foreach (IElement elementoCabecera in HTMLWorker.ParseToList(new StringReader(htmlCabecera), new StyleSheet()))
            {
                documento.Add(elementoCabecera);
            }

            foreach (IElement elementoTabla in HTMLWorker.ParseToList(new StringReader(htmlTabla), new StyleSheet()))
            {
                documento.Add(elementoTabla);
            }

            foreach (IElement elementoTablaDetallesInicio in HTMLWorker.ParseToList(new StringReader(htmlDetallesInicio), new StyleSheet()))
            {
                documento.Add(elementoTablaDetallesInicio);
            }

            //lista de detalles de productos en factura
            foreach (DataGridViewRow filas in dgvDetalleFactura.Rows)
            {
                //Paragraph parrafo = new Paragraph();
                String nombreProducto = filas.Cells[1].Value.ToString();
                String valorProducto = filas.Cells[2].Value.ToString();
                String cantidadProducto = filas.Cells[3].Value.ToString();
                String totalCantidad = filas.Cells[4].Value.ToString();

                //documento.Add(new Paragraph(nombreProducto + " " + cantidadProducto + " " + totalCantidad));
                htmlFilaProducto = htmlFilaInicio +
                    "<td>" + cantidadProducto + "</td>" +
                    "<td colspan=\"2\">" + nombreProducto + "</td>" +
                    //"<td>" + nombreProducto + "</td>" +
                    "<td align=\"right\">" + valorProducto + "</td>" +
                    "<td align=\"right\">" + totalCantidad + "</td>" + htmlFilaFinal;


                foreach (IElement elementoTablaDetallesProductos in HTMLWorker.ParseToList(new StringReader(htmlFilaProducto), new StyleSheet()))
                {
                    documento.Add(elementoTablaDetallesProductos);
                }




            }


            foreach (IElement elementoTablaDetallesFinal in HTMLWorker.ParseToList(new StringReader(htmlDetallesFinal), new StyleSheet()))
            {
                documento.Add(elementoTablaDetallesFinal);
            }


            foreach (IElement elementoFinal in HTMLWorker.ParseToList(new StringReader(htmlFinal), new StyleSheet()))
            {
                documento.Add(elementoFinal);
            }


            
            
            documento.Close();
            /*
            using (StreamReader streamToPrint = new StreamReader(ruta))
            {
                PrintDocument pd = new PrintDocument();
                ProcessStartInfo info = new ProcessStartInfo(ruta);
                //pd.PrintPage += new PrintPageEventHandler();
                //pd.PrinterSettings.PrinterName = "doPDF 6";

                info.Verb = "PrintTo";

                pd.Print();
                streamToPrint.Close();
            }
            */

            /*
            try
            {
                //StreamReader streamToPrint = new StreamReader("file1" + rand.ToString() + ".txt");
                StreamReader streamToPrint = new StreamReader(ruta);
                try
                {
                    Font printFont = new Font("Arial", 10);
                    PrintDocument pd = new PrintDocument();
                    pd.PrintPage += new PrintPageEventHandler(this.pd_PrintPage);
                    pd.Print();
                }
                finally
                {
                    streamToPrint.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Problemas al imprimir:\r\n" + ex.Message);
            }*/



            //en esta parte del método se procesa todo lo que da inicio a la impresión del "voucher" en impresora
            /*
            Process process = new Process();

            String printerName = obtenerImpresora();
            //String printerName = "EPSON TX220 Series";

            process.StartInfo.FileName = ruta;
            process.StartInfo.Verb = "printto";
            process.StartInfo.Arguments = "\"" + printerName + "\"";
            process.Start();

            process.WaitForInputIdle();
            process.Kill();
            */
            
        }
        

        //este método estático lo que hace es buscar la impresora instalada y utilizada por defecto
        public static string obtenerImpresora() 
        {
            for (int i = 0; i < PrinterSettings.InstalledPrinters.Count; i++)
            {
                PrinterSettings a = new PrinterSettings();
                a.PrinterName = PrinterSettings.InstalledPrinters[i].ToString();
                if (a.IsDefaultPrinter)
                {
                   return PrinterSettings.InstalledPrinters[i].ToString();

                }
            }
            return "";
        }



        public void imprimirComprobanteXmlSii(Int32 codFactura, DataTable dtDatosCliente) 
        {
            Int32 numeroFilas = dgvDetalleFactura.RowCount;
            Int32 numeroColumnas = dgvDetalleFactura.ColumnCount;
            clFormularioXml formXml = new clFormularioXml();

            DataTable dtCodigoFactura = new DataTable();
            dtCodigoFactura = sentencias.ultimoRegistroFactura();
            
            formXml.formXmlTransaccionSii(numeroFilas, dtCodigoFactura);
            String[,] matrizDetalle = new String[numeroFilas, numeroColumnas];
            //String[,] matrizDetalle;
            Int32 contadorDetalle = 0;
            Int32 contadorCols = dgvDetalleFactura.ColumnCount;
            foreach (DataGridViewRow filas in dgvDetalleFactura.Rows)
            {

                String codigoProducto = filas.Cells[0].Value.ToString();
                String nomProducto = filas.Cells[1].Value.ToString();
                String valProducto = filas.Cells[2].Value.ToString();
                String cantProducto = filas.Cells[3].Value.ToString();
                String totalCantidad = filas.Cells[4].Value.ToString();

                matrizDetalle[contadorDetalle, 0] = codigoProducto;
                matrizDetalle[contadorDetalle, 1] = nomProducto;
                matrizDetalle[contadorDetalle, 2] = valProducto;
                matrizDetalle[contadorDetalle, 3] = cantProducto;
                matrizDetalle[contadorDetalle, 4] = totalCantidad;
                
                contadorDetalle = contadorDetalle + 1;
                //foreach(DataGridViewColumn columnas in dgvDetalleFactura.Columns){
                /*
                String codigoProducto = filas.Cells[0].Value.ToString();
                String nomProducto = filas.Cells[1].Value.ToString();
                String valProducto = filas.Cells[2].Value.ToString();
                String cantProducto = filas.Cells[3].Value.ToString();
                String totalCantidad = filas.Cells[4].Value.ToString();
                */

                /*
                String codigoProducto = columnas.Cells[0].Value.ToString();
                String nomProducto = columnas.Cells[1].Value.ToString();
                String valProducto = columnas.Cells[2].Value.ToString();
                String cantProducto = columnas.Cells[3].Value.ToString();
                String totalCantidad = columnas.Cells[4].Value.ToString();
                */

                //matrizDetalle[contadorDetalle,contadorCols] = {};

                //}



                //matrizDetalle[contadorDetalle,numeroColumnas] =  new String[]{codigoProducto, nomProducto, valProducto, cantProducto, totalCantidad};
                //String[] lineaDetalle = [,,,,];
            }

            Int32 codFacturaNew = codFactura;

            Int32 subTotal = Convert.ToInt32(txtSubTotal.Text);
            Int32 iva = Convert.ToInt32(txtIVA.Text);
            Int32 total = Convert.ToInt32(txtTotal.Text);

            //El orden para enviar los datos son: matriz, el contador de filas, el contador de columnas, el lcodigo de la última factura, el subtotal, el iva, el total
            formXml.nodoTransaccionXmlSii(matrizDetalle, contadorDetalle, contadorCols, codFacturaNew, subTotal, iva, total, dtDatosCliente);
            
            formXml.generarSignatureFinal();
        }

        private void txtCantidad_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Char.IsDigit(e.KeyChar))
            {
                e.Handled = false;
            }
            else if (Char.IsControl(e.KeyChar))
            {
                e.Handled = false;
            }
            else if (Char.IsSeparator(e.KeyChar))
            {
                e.Handled = false;
            }
            else
            {
                e.Handled = true;
            }
        }


        private void pd_PrintPage(object sender, PrintPageEventArgs e)
        {
            /*
            float linesPerPage = 0;
            float yPos = 0;
            int count = 0;
            float leftMargin = e.MarginBounds.Left;
            float topMargin = e.MarginBounds.Top;
            string line = null;

            // Calculate the number of lines per page.
            linesPerPage = e.MarginBounds.Height / printFont.GetHeight(e.Graphics);

            // Print each line of the file.
            while (count < linesPerPage && ((line = streamToPrint.ReadLine()) != null))
            {
                yPos = topMargin + (count * printFont.GetHeight(e.Graphics));
                e.Graphics.DrawString(line, printFont, Brushes.Black,leftMargin, yPos, new StringFormat());
                count++;
            }

            // If more lines exist, print another page.
            if (line != null)
            {
                e.HasMorePages = true;
            }
            else
            {
                e.HasMorePages = false;
            }
            */
        }

        private void btnPruebaCertX509_Click(object sender, EventArgs e)
        {
            clFormularioXml formuXml = new clFormularioXml();
            formuXml.lalala();
            formuXml.generarXml();
            formuXml.principalClaveRsa();

            string str = "www.mejorando.la";

            string res = formuXml.GetSHA1(str);
            MessageBox.Show(res);
        }



        

        private void txtNumeroPrueba_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Char.IsDigit(e.KeyChar))
            {
                e.Handled = false;
            }
            else if (Char.IsControl(e.KeyChar))
            {
                e.Handled = false;
            }
            else if (Char.IsSeparator(e.KeyChar))
            {
                e.Handled = false;
            }
            else
            {
                e.Handled = true;
            }
        }

        private void btnNumeroNombrePrueba_Click(object sender, EventArgs e)
        {
            if (txtNumeroPrueba.Text.Length <= 0)
            {
                MessageBox.Show("Escribe un numero");
            }
            else
            {
                //Int32 numeroPrueba = Convert.ToInt32(txtNumeroPrueba.Text);
                Double numeroPrueba = Convert.ToDouble(txtNumeroPrueba.Text);
                //String resultadoNumero = NumberToWords(numeroPrueba);
                String resultadoNumero = toText(numeroPrueba);
                MessageBox.Show(numeroPrueba.ToString() + "\r\n\r\n" + resultadoNumero);
                txtNumeroPrueba.Text = "";
                txtNumeroPrueba.Focus();
            }
        }





        private string toText(double value)
        {

            string Num2Text = "";

            value = Math.Truncate(value);
            

            if (value == 0) Num2Text = "CERO";

            else if (value == 1) Num2Text = "UNO";

            else if (value == 2) Num2Text = "DOS";

            else if (value == 3) Num2Text = "TRES";

            else if (value == 4) Num2Text = "CUATRO";

            else if (value == 5) Num2Text = "CINCO";

            else if (value == 6) Num2Text = "SEIS";

            else if (value == 7) Num2Text = "SIETE";

            else if (value == 8) Num2Text = "OCHO";

            else if (value == 9) Num2Text = "NUEVE";

            else if (value == 10) Num2Text = "DIEZ";

            else if (value == 11) Num2Text = "ONCE";

            else if (value == 12) Num2Text = "DOCE";

            else if (value == 13) Num2Text = "TRECE";

            else if (value == 14) Num2Text = "CATORCE";

            else if (value == 15) Num2Text = "QUINCE";

            else if (value < 20) Num2Text = "DIECI" + toText(value - 10);

            else if (value == 20) Num2Text = "VEINTE";

            else if (value < 30) Num2Text = "VEINTI" + toText(value - 20);

            else if (value == 30) Num2Text = "TREINTA";

            else if (value == 40) Num2Text = "CUARENTA";

            else if (value == 50) Num2Text = "CINCUENTA";

            else if (value == 60) Num2Text = "SESENTA";

            else if (value == 70) Num2Text = "SETENTA";

            else if (value == 80) Num2Text = "OCHENTA";

            else if (value == 90) Num2Text = "NOVENTA";

            else if (value < 100) Num2Text = toText(Math.Truncate(value / 10) * 10) + " Y " + toText(value % 10);

            else if (value == 100) Num2Text = "CIEN";

            else if (value < 200) Num2Text = "CIENTO " + toText(value - 100);

            else if ((value == 200) || (value == 300) || (value == 400) || (value == 600) || (value == 800)) Num2Text = toText(Math.Truncate(value / 100)) + "CIENTOS";

            else if (value == 500) Num2Text = "QUINIENTOS";

            else if (value == 700) Num2Text = "SETECIENTOS";

            else if (value == 900) Num2Text = "NOVECIENTOS";

            else if (value < 1000) Num2Text = toText(Math.Truncate(value / 100) * 100) + " " + toText(value % 100);

            else if (value == 1000) Num2Text = "MIL";

            else if (value < 2000) Num2Text = "MIL " + toText(value % 1000);

            else if (value < 1000000)
            {

                Num2Text = toText(Math.Truncate(value / 1000)) + " MIL";

                if ((value % 1000) > 0) Num2Text = Num2Text + " " + toText(value % 1000);

            }

            else if (value == 1000000) Num2Text = "UN MILLÓN";

            else if (value < 2000000) Num2Text = "UN MILLÓN " + toText(value % 1000000);

            else if (value < 1000000000000)
            {

                Num2Text = toText(Math.Truncate(value / 1000000)) + " MILLONES ";

                if ((value - Math.Truncate(value / 1000000) * 1000000) > 0) Num2Text = Num2Text + " " + toText(value - Math.Truncate(value / 1000000) * 1000000);

            }

            else if (value == 1000000000000) Num2Text = "UN BILLóN";

            else if (value < 2000000000000) Num2Text = "UN BILLÓN " + toText(value - Math.Truncate(value / 1000000000000) * 1000000000000);

            else
            {

                Num2Text = toText(Math.Truncate(value / 1000000000000)) + " BILLONES";

                if ((value - Math.Truncate(value / 1000000000000) * 1000000000000) > 0) Num2Text = Num2Text + " " + toText(value - Math.Truncate(value / 1000000000000) * 1000000000000);

            }

            return Num2Text;

        }

        
    }
}
