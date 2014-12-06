using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace facturacion_norte
{
    public partial class formAgregarCliente : Form
    {
        clSentencias sentencias = new clSentencias();

        public void limpiarCasillas() {
            txtNombreNew.Text = "";
            txtApePatNew.Text = "";
            txtApeMatNew.Text = "";
            txtGiroNew.Text = "";
            txtRutNew.Text = "";
            txtRutDvNew.Text = "";
            txtDireccionNew.Text = "";
            txtComunaNew.Text = "";
            txtCiudadNew.Text = "";
            txtTelefonoNew.Text = "";
            txtEmailNew.Text = "";
        }

        public formAgregarCliente()
        {
            InitializeComponent();
        }

        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            limpiarCasillas();
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            String nombre = txtNombreNew.Text.Trim();
            String apepat = txtApePatNew.Text.Trim();
            String apemat = txtApeMatNew.Text.Trim();
            String giro = txtGiroNew.Text.Trim();
            String rut = txtRutNew.Text.Trim();
            String rutDv = txtRutDvNew.Text.Trim();
            String direccion = txtDireccionNew.Text.Trim();
            String comuna = txtComunaNew.Text.Trim();
            String ciudad = txtCiudadNew.Text.Trim();
            String telefono = txtTelefonoNew.Text.Trim();
            String email = txtEmailNew.Text.Trim();
            //int ok = 0;

            String chkemail = @"^[a-zA-Z][\w\.-]{2,28}[a-zA-Z0-9]@[a-zA-Z0-9][\w\.-]*[a-zA-Z0-9]\.[a-zA-Z][a-zA-Z\.]*[a-zA-Z]$";
            System.Text.RegularExpressions.Regex rEmail = new System.Text.RegularExpressions.Regex(chkemail);


            if(txtNombreNew.Text == "")
            {
                MessageBox.Show("campo vacio en nombre");
                txtNombreNew.Focus();
            }else if(txtApePatNew.Text == "" && rdbPersona.Checked == true)
            {
                MessageBox.Show("campo vacio en apellido paterno");
                txtApePatNew.Focus();
            }
            else if (txtApeMatNew.Text == "" && rdbPersona.Checked == true)
            {
                MessageBox.Show("campo vacio en apellido materno");
                txtApeMatNew.Focus();
            }
            else if (txtGiroNew.Text == "" && rdbEmpresa.Checked == true)
            {
                MessageBox.Show("campo vacio en Giro");
                txtGiroNew.Focus();
            }
            else if(txtRutNew.Text == "")
            {
                MessageBox.Show("campo vacio en RUT");
                txtRutNew.Focus();
            }
            else if (txtDireccionNew.Text == "")
            {
                MessageBox.Show("campo vacio en dirección");
                txtDireccionNew.Focus();
            }
            else if (txtComunaNew.Text == "")
            {
                MessageBox.Show("campo vacio en coumna");
                txtComunaNew.Focus();
            }
            else if (txtTelefonoNew.Text == "")
            {
                MessageBox.Show("campo vacio en teléfono");
                txtTelefonoNew.Focus();
            }
            else if (txtEmailNew.Text == "")
            {
                MessageBox.Show("campo vacio en email");
                txtEmailNew.Focus();
            }
            else if (!rEmail.IsMatch(txtEmailNew.Text))
            {
                MessageBox.Show("Por favor, coloque el formato correcto de email");
                txtEmailNew.Focus();
                txtEmailNew.SelectAll();
            }
            else {
                DataTable dtExiste = new DataTable();
                dtExiste = sentencias.dtExisteRutTelEmail(Convert.ToInt32(rut), Convert.ToInt32(telefono), email);

                //en la sentencia IF se comprueba si existen el email, el rut, o el telefono, lo cual impide que se registren datos redundantes
                if (dtExiste.Rows.Count > 0)
                {
                    MessageBox.Show("Rut, Teléfono o Email ya en uso. Por favor ingrese otro rut, teléfono o email");
                }
                else
                {
                    if (rdbPersona.Checked == true)
                    {
                        sentencias.agregarNuevoCliente(nombre, apepat, apemat, Convert.ToInt32(rut), rutDv, direccion, comuna, ciudad, Convert.ToInt32(telefono), email);
                        MessageBox.Show("Nuevo cliente persona fue registrado con éxito");
                        limpiarCasillas();
                    }
                    else if (rdbEmpresa.Checked == true)
                    {
                        sentencias.agregarNuevoCliente2(nombre, giro, Convert.ToInt32(rut), rutDv, direccion, comuna, ciudad, Convert.ToInt32(telefono), email);
                        MessageBox.Show("Nuevo cliente empresa fue registrado con éxito");
                        limpiarCasillas();
                    }
                }
            }
        }

        



        public String digitoVerificador(Int32 rut) 
        {
            Int32 Digito;
            Int32 Contador;
            Int32 Multiplo;
            Int32 Acumulador;
            String RutDigito;
 
            Contador = 2;
            Acumulador = 0;
            while (rut != 0)
            {
                Multiplo = (rut % 10) * Contador;
                Acumulador = Acumulador + Multiplo;
                rut = rut/10;
                Contador = Contador + 1;
                if (Contador == 8)
                {
                    Contador = 2;
                }
                
            }
 
            Digito = 11 - (Acumulador % 11);
            RutDigito = Digito.ToString().Trim();
            if (Digito == 10 )
            {
                RutDigito = "K";
            }
            if (Digito == 11)
            {
                RutDigito = "0";
            }
            return (RutDigito);

        }

        private void txtRutNew_TextChanged(object sender, EventArgs e)
        {
            if (txtRutNew.Text.Length != 0)
            {
                Int32 rut = Convert.ToInt32(txtRutNew.Text);
                String rutDv = digitoVerificador(rut);
                txtRutDvNew.Text = rutDv.ToString();
            }
            else
            {
                txtRutDvNew.Text = "";
            }
        }

        private void txtTelefonoNew_KeyPress(object sender, KeyPressEventArgs e)
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

        private void txtRutNew_KeyPress(object sender, KeyPressEventArgs e)
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

        private void btnSalir_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void rdbEmpresa_CheckedChanged(object sender, EventArgs e)
        {
            if (rdbEmpresa.Checked == true)
            {
                txtNombreNew.Text = "";
                txtNombreNew.MaxLength = 30;

                txtApePatNew.ReadOnly = true;
                txtApePatNew.TabStop = false;
                txtApePatNew.Text = "";
                txtApeMatNew.ReadOnly = true;
                txtApeMatNew.TabStop = false;
                txtApeMatNew.Text = "";

                txtGiroNew.ReadOnly = false;
                txtGiroNew.TabStop = true;
            }
            else
            {
                txtNombreNew.Text = "";
                txtNombreNew.MaxLength = 12;
                
                txtApePatNew.ReadOnly = false;
                txtApePatNew.TabStop = true;
                txtApeMatNew.ReadOnly = false;
                txtApeMatNew.TabStop = true;

                txtGiroNew.ReadOnly = true;
                txtGiroNew.TabStop = false;
                txtGiroNew.Text = "";
            }
        }

    }
}
