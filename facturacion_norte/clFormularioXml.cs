using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.Windows.Forms;

using System.Globalization;
using System.Threading;

using System.Data;

//espacios de recursos para la generacion de llaves
using System.Security.Cryptography;
using System.Security.Cryptography.Xml;
//using System.Security.Cryptography.Xml.EncryptedXml;
using System.Security.Cryptography.X509Certificates;

namespace facturacion_norte
{
    class clFormularioXml
    {

        X509Certificate2 certificadoPublico;
        X509Certificate2 certificadoPrivado;

        

        

        //Codigos dentro de los bloques signature
        String digestValue = "hlmQtu/AyjUjTDhM3852wvRCr8w=";
        String signatureValue = "JG1Ig0pvSIH85kIKGRZUjkyX6CNaY08Y94j4UegTgDe8+wl61GzqjdR1rfOK9BGn93AMOo6aiAgolW0k/XklNVtM/ZzpNNS3d/fYVa1q509mAMSXbelxSM3bjoa7H6Wzd/mV1PpQ8zK5gw7mgMMP4IKxHyS92G81GEguSmzcQmA=";
        String modulus = "tNEknkb1kHiD1OOAWlLKkcH/UP5UGa6V6MYso++JB+vYMg2OXFROAF7G8BNFFPQx" + "\r\n" +
            "iuS/7y1azZljN2xq+bW3bAou1bW2ij7fxSXWTJYFZMAyndbLyGHM1e3nVmwpgEpx" + "\r\n" +
            "BHhZzPvwLb55st1wceuKjs2Ontb13J33sUb7bbJMWh0=" + "\r\n";
        
        //String modulus1 = "tNEknkb1kHiD1OOAWlLKkcH/UP5UGa6V6MYso++JB+vYMg2OXFROAF7G8BNFFPQx" + "\r\n";
        //String modulus2 = "iuS/7y1azZljN2xq+bW3bAou1bW2ij7fxSXWTJYFZMAyndbLyGHM1e3nVmwpgEpx" + "\r\n";
        //String modulus3 = "BHhZzPvwLb55st1wceuKjs2Ontb13J33sUb7bbJMWh0=";
        String exponent = "AQAB";

        String x509certificate = "MIIEgjCCA+ugAwIBAgIEAQAApzANBgkqhkiG9w0BAQUFADCBtTELMAkGA1UEBhMC" + "\r\n" +
            "Q0wxHTAbBgNVBAgUFFJlZ2lvbiBNZXRyb3BvbGl0YW5hMREwDwYDVQQHFAhTYW50" + "\r\n" +
            "aWFnbzEUMBIGA1UEChQLRS1DRVJUQ0hJTEUxIDAeBgNVBAsUF0F1dG9yaWRhZCBD" + "\r\n" +
            "ZXJ0aWZpY2Fkb3JhMRcwFQYDVQQDFA5FLUNFUlRDSElMRSBDQTEjMCEGCSqGSIb3" + "\r\n" +
            "DQEJARYUZW1haWxAZS1jZXJ0Y2hpbGUuY2wwHhcNMDMxMDAxMTg1ODE1WhcNMDQw" + "\r\n" +
            "OTMwMDAwMDAwWjCBuDELMAkGA1UEBhMCQ0wxFjAUBgNVBAgUDU1ldHJvcG9saXRh" + "\r\n" +
            "bmExETAPBgNVBAcUCFNhbnRpYWdvMScwJQYDVQQKFB5TZXJ2aWNpbyBkZSBJbXB1" + "\r\n" +
            "ZXN0b3MgSW50ZXJub3MxDzANBgNVBAsUBlBpc28gNDEjMCEGA1UEAxQaV2lsaWJh" + "\r\n" +
            "bGRvIEdvbnphbGV6IENhYnJlcmExHzAdBgkqhkiG9w0BCQEWEHdnb256YWxlekBz" + "\r\n" +
            "aWkuY2wwgZ8wDQYJKoZIhvcNAQEBBQADgY0AMIGJAoGBALxZlVh1xr9sKQIBDF/6" + "\r\n" +
            "Va+lsHQSG5AAmCWvtNTIOXN3E9EQCy7pOPHrDg6EusvoHyesZSKJbc0TnIFXZp78" + "\r\n" +
            "q7mbdHijzKqvMmyvwbdP7KK8LQfwf84W4v9O8MJeUHlbJGlo5nFACrPAeTtONbHa" + "\r\n" +
            "ReyzeMDv2EganNEDJc9c+UNfAgMBAAGjggGYMIIBlDAjBgNVHREEHDAaoBgGCCsG" + "\r\n" +
            "AQQBwQEBoAwWCjA3ODgwNDQyLTQwCQYDVR0TBAIwADA8BgNVHR8ENTAzMDGgL6At" + "\r\n" +
            "hitodHRwOi8vY3JsLmUtY2VydGNoaWxlLmNsL2UtY2VydGNoaWxlY2EuY3JsMCMG" + "\r\n" +
            "A1UdEgQcMBqgGAYIKwYBBAHBAQKgDBYKOTY5MjgxODAtNTAfBgNVHSMEGDAWgBTg" + "\r\n" +
            "KP3S4GBPs0brGsz1CJEHcjodCDCB0AYDVR0gBIHIMIHFMIHCBggrBgEEAcNSBTCB" + "\r\n" +
            "tTAvBggrBgEFBQcCARYjaHR0cDovL3d3dy5lLWNlcnRjaGlsZS5jbC8yMDAwL0NQ" + "\r\n" +
            "Uy8wgYEGCCsGAQUFBwICMHUac0VsIHRpdHVsYXIgaGEgc2lkbyB2YWxpZG8gZW4g" + "\r\n" +
            "Zm9ybWEgcHJlc2VuY2lhbCwgcXVlZGFuZG8gZWwgQ2VydGlmaWNhZG8gcGFyYSB1" + "\r\n" +
            "c28gdHJpYnV0YXJpbywgcGFnb3MsIGNvbWVyY2lvIHkgb3Ryb3MwCwYDVR0PBAQD" + "\r\n" +
            "AgTwMA0GCSqGSIb3DQEBBQUAA4GBABMfCyJF0mNXcov8iEWvjGFyyPTsXwvsYbbk" + "\r\n" +
            "OJ41wjaGOFMCInb4WY0ngM8BsDV22bGMs8oLyX7rVy16bGA8Z7WDUtYhoOM7mqXw" + "\r\n" +
            "/Hrpqjh3JgAf8zqdzBdH/q6mAbdvq/yb04JHKWPC7fMFuBoeyVWAnhmuMZfReWQi" + "\r\n" +
            "MUEHGGIW";

        String digestValue2 = "4OTWXyRl5fw3htjTyZXQtYEsC3E=";
        
        String signatureValue2 = "sBnr8Yq14vVAcrN/pKLD/BrqUFczKMW3y1t3JOrdsxhhq6IxvS13SgyMXbIN/T9ciRaFgNabs3pi732XhcpeiSmD1ktzbRctEbSIszYkFJY49k0eB+TVzq3eVaQr4INrymfuOnWj78BZcwKuXvDy4iAcx6/TBbAAkPFwMP9ql2s=";

        //fin de los codigos dentro de los bloque signature



        //inicio de los codigos de llave publica

        //llave publica RSA del contribuyente: módulo o base
        String rsapkModulo = "0a4O6Kbx8Qj3K4iWSP4w7KneZYeJ+g/prihYtIEolKt3cykSxl1zO8vSXu397QhTmsX7SBEudTUx++2zDXBhZw==";

        //exponente de la llave publica RSA anterior
        String rsapkExponente = "Aw==";


        //firma: es un identificador de llave
        String firma = "g1AQX0sy8NJugX52k2hTJEZAE9Cuul6pqYBdFxj1N17umW7zG/hAavCALKByHzdYAfZ3LhGTXCai5zNxOo4lDQ==";

        //<FRMT>
        String frmt = "GbmDcS9e/jVC2LsLIe1iRV12Bf6lxsILtbQiCkh6mbjckFCJ7fj/kakFTS06Jo8iS4HXvJj3oYZuey53Krniew==";

        //fin de los codigoc de llave primaria

        //String uriSetDoc = "#SetDoc";
        String uriSetDoc = "http://www.microsoft.com";
        String certificado = "certificacion/microsoft.cer";

        public bool formXmlTransaccionSii(Int32 numeroFilas, DataTable dtCodigoFactura)
        {
            bool rta = false;
            String codTipoFactura = "33";
            String rutaXml = "documentosXml/TransaccionesSii.xml";
            String uriFactura = "F" + dtCodigoFactura.Rows[0][0].ToString() + "T" + codTipoFactura;
            
            try
            {
                XmlTextWriter escribirRec = new XmlTextWriter(rutaXml, System.Text.Encoding.GetEncoding("ISO-8859-1"));
                escribirRec.Formatting = Formatting.Indented;
                escribirRec.Indentation = 2;
                escribirRec.WriteStartDocument(false);
                escribirRec.WriteComment("Lista de transacciones");

                escribirRec.WriteStartElement("EnvioDTE");//inicio del bloque EnvioDTE
                escribirRec.WriteAttributeString("xmlns", null, "http://www.sii.cl/SiiDte");
                escribirRec.WriteAttributeString("xmlns", "xsi", null, "http://www.w3.org/2001/XMLSchema-instance");
                escribirRec.WriteAttributeString("xsi", "schemaLocation", null, "http://www.sii.cl/SiiDteEnvioDTE_v10.xsd");
                escribirRec.WriteAttributeString("version", null, "1.0");

                escribirRec.WriteStartElement("SetDTE"); //inicio del bloque SetDTE
                escribirRec.WriteAttributeString("ID", null, "SetDoc");
                //escribirRec.WriteElementString("hbhebhde", null); //OK

                escribirRec.WriteStartElement("Caratula");//inicio del bloque Caratula
                escribirRec.WriteAttributeString("version", null, "1.0");

                /*
                escribirRec.WriteStartElement("RespuestaExitosa");
                escribirRec.WriteEndElement();
                escribirRec.WriteStartElement("RespuestaFalida");
                escribirRec.WriteEndElement();
                */

                escribirRec.WriteEndElement();//Fin del bloque Caratula

                escribirRec.WriteStartElement("DTE");//inicio del bloque DTE
                escribirRec.WriteAttributeString("version", null, "1.0");

                escribirRec.WriteStartElement("Documento");//inicio del bloque Documento
                escribirRec.WriteAttributeString("ID", null, "F" + dtCodigoFactura.Rows[0][0].ToString() + "T33");

                escribirRec.WriteStartElement("Encabezado");//inicio del bloque Encabezado



                escribirRec.WriteEndElement();//fin del bloque Encabezado

                //aqui se hace el ciclo para los detalles
                //#############

                for (Int32 i = 1; i <= numeroFilas; i++)
                {
                    escribirRec.WriteWhitespace("\r\n");
                    escribirRec.WriteStartElement("Detalle");

                    //escribirRec.WriteStartElement("aaaaaaaaaaaaaaaaaaaaaa");
                    //escribirRec.WriteEndElement();

                    escribirRec.WriteElementString("NroLinDet", i.ToString());

                    escribirRec.WriteStartElement("CdgItem");

                    //escribirRec.WriteElementString("", "INT1");
                    //escribirRec.WriteElementString("", "11");

                    escribirRec.WriteStartElement("TpoCodigo");
                    escribirRec.WriteEndElement();
                    escribirRec.WriteStartElement("VlrCodigo");
                    escribirRec.WriteEndElement();

                    escribirRec.WriteEndElement();

                    escribirRec.WriteStartElement("NmbItem");
                    escribirRec.WriteEndElement();

                    escribirRec.WriteStartElement("DscItem");
                    escribirRec.WriteEndElement();

                    escribirRec.WriteStartElement("QtyItem");
                    escribirRec.WriteEndElement();

                    escribirRec.WriteStartElement("PrcItem");
                    escribirRec.WriteEndElement();

                    escribirRec.WriteStartElement("MontoItem");
                    escribirRec.WriteEndElement();

                    escribirRec.WriteEndElement();
                    escribirRec.WriteWhitespace("\r\n");
                }

                //#############
                //fin de detalles

                escribirRec.WriteStartElement("TED");//inicio del bloque TED (Timbre electronico DTE)
                escribirRec.WriteAttributeString("version", null, "1.0");

                escribirRec.WriteStartElement("DD");//inicio del bloque DD


                escribirRec.WriteEndElement();//fin del bloque DD

                escribirRec.WriteStartElement("FRMT");//Elemento de cadena <FRMT>
                escribirRec.WriteAttributeString("algoritmo", null, "SHA1withRSA");
                escribirRec.WriteEndElement();

                escribirRec.WriteEndElement();//fin del bloque TED (Timbre electronico DTE)

                //escribirRec.WriteElementString("TmstFirma", "2013-05-10T10:00:00");//elemento de cadena <TmstFirma> (Hora de la firma)
                escribirRec.WriteStartElement("TmstFirma");//elemento de cadena <TmstFirma> (Hora de la firma)
                escribirRec.WriteEndElement();
                
                escribirRec.WriteEndElement();//fin del bloque Documento

                escribirRec.WriteStartElement("Signature");//Inicio del bloque Signature
                escribirRec.WriteAttributeString("xmlns", null, "http://www.w3.org/2000/09/xmldsig#");

                escribirRec.WriteStartElement("SignedInfo");//inicio del bloque SignedInfo

                escribirRec.WriteStartElement("CanonicalizationMethod");//Elemento de cadena CanonicalizationMethod
                escribirRec.WriteAttributeString("Algorithm", null, "http://www.w3.org/TR/2001/REC-xml-c14n-20010315");
                escribirRec.WriteEndElement();

                escribirRec.WriteStartElement("SignatureMethod");//Elemento de cadena SignatureMethod
                escribirRec.WriteAttributeString("Algorithm", null, "http://www.w3.org/2000/09/xmldsig#rsa-sha1");
                escribirRec.WriteEndElement();

                escribirRec.WriteStartElement("Reference");//inicio del bloque Reference
                escribirRec.WriteAttributeString("URI", null, "#F" + dtCodigoFactura.Rows[0][0].ToString() + "T33");

                escribirRec.WriteStartElement("Transforms");//inicio del bloque Transforms

                escribirRec.WriteStartElement("Transform");//Elemento de cadena Transform
                escribirRec.WriteAttributeString("Algorithm", null, "http://www.w3.org/TR/2001/REC-xml-c14n-20010315");
                escribirRec.WriteEndElement();

                escribirRec.WriteEndElement();//fin del bloque Transforms

                escribirRec.WriteStartElement("DigestMethod");//Elemento de cadena DigestMethod
                escribirRec.WriteAttributeString("Algorithm", null, "http://www.w3.org/2000/09/xmldsig#sha1");
                escribirRec.WriteEndElement();

                escribirRec.WriteElementString("DigestValue", digestValue);//Elemento de cadena DigestValue

                escribirRec.WriteEndElement();//fin del bloque Reference

                escribirRec.WriteEndElement();//fin del bloque SignedInfo

                escribirRec.WriteElementString("SignatureValue", signatureValue);//Elemento de cadena SignatureValue

                escribirRec.WriteStartElement("KeyInfo");//inicio del bloque KeyInfo

                escribirRec.WriteStartElement("KeyValue");//inicio del bloque KeyValue

                escribirRec.WriteStartElement("RSAKeyValue");//inicio del bloque RSAKeyValue

                escribirRec.WriteElementString("Modulus", modulus);//Elemento de cadena Modulus
                //escribirRec.WriteString(modulus2);
                //escribirRec.WriteString(modulus3);
                escribirRec.WriteElementString("Exponent", exponent);//Elemento de cadena Exponent

                escribirRec.WriteEndElement();//fin del bloque RSAKeyValue

                escribirRec.WriteEndElement();//fin del bloque KeyValue

                escribirRec.WriteStartElement("X509Data");//inicio del bloque X509Data

                escribirRec.WriteElementString("X509Certificate", x509certificate);//Elemento de cadena X509Certificate

                escribirRec.WriteEndElement();//fin del bloque X509Data

                escribirRec.WriteEndElement();//fin del bloque KeyInfo
                
                escribirRec.WriteEndElement();//fin del bloque Signature

                escribirRec.WriteEndElement();//fin del bloque DTE

                escribirRec.WriteEndElement();//fin del bloque SetDTE

                escribirRec.WriteStartElement("Signature");//inicio del bloque Signature (parte 2)
                escribirRec.WriteAttributeString("xmlns", null, "http://www.w3.org/2000/09/xmldsig#");



                escribirRec.WriteStartElement("SignedInfo");//inicio del bloque SignedInfo (parte 2)

                escribirRec.WriteStartElement("CanonicalizationMethod");//Elemento de cadena CanonicalizationMethod
                escribirRec.WriteAttributeString("Algorithm", null, "http://www.w3.org/TR/2001/REC-xml-c14n-20010315");
                escribirRec.WriteEndElement();

                escribirRec.WriteStartElement("SignatureMethod");//Elemento de cadena SignatureMethod
                escribirRec.WriteAttributeString("Algorithm", null, "http://www.w3.org/2000/09/xmldsig#rsa-sha1");
                escribirRec.WriteEndElement();

                escribirRec.WriteStartElement("Reference");//inicio del bloque Reference
                escribirRec.WriteAttributeString("URI", null, "#SetDoc");

                escribirRec.WriteStartElement("Transforms");//inicio del bloque Transforms

                escribirRec.WriteStartElement("Transform");//Elemento de cadena Transform
                escribirRec.WriteAttributeString("Algorithm", null, "http://www.w3.org/TR/2001/REC-xml-c14n-20010315");
                escribirRec.WriteEndElement();

                escribirRec.WriteEndElement();//fin del bloque Transforms

                escribirRec.WriteStartElement("DigestMethod");//Elemento de cadena DigestMethod
                escribirRec.WriteAttributeString("Algorithm", null, "http://www.w3.org/2000/09/xmldsig#sha1");
                escribirRec.WriteEndElement();

                escribirRec.WriteElementString("DigestValue", digestValue2);//Elemento de cadena DigestValue

                escribirRec.WriteEndElement();//fin del bloque Reference

                escribirRec.WriteEndElement();//fin del bloque SignedInfo (parte 2)

                escribirRec.WriteElementString("SignatureValue", signatureValue2);//Elemento de cadena SignatureValue

                escribirRec.WriteStartElement("KeyInfo");//inicio del bloque KeyInfo

                escribirRec.WriteStartElement("KeyValue");//inicio del bloque KeyValue

                escribirRec.WriteStartElement("RSAKeyValue");//inicio del bloque RSAKeyValue

                escribirRec.WriteElementString("Modulus", modulus);//Elemento de cadena Modulus
                //escribirRec.WriteString(modulus2);
                //escribirRec.WriteString(modulus3);
                escribirRec.WriteElementString("Exponent", exponent);//Elemento de cadena Exponent

                escribirRec.WriteEndElement();//fin del bloque RSAKeyValue

                escribirRec.WriteEndElement();//fin del bloque KeyValue

                escribirRec.WriteStartElement("X509Data");//inicio del bloque X509Data

                escribirRec.WriteElementString("X509Certificate", x509certificate);//Elemento de cadena X509Certificate

                escribirRec.WriteEndElement();//fin del bloque X509Data

                escribirRec.WriteEndElement();//fin del bloque KeyInfo


                escribirRec.WriteEndElement();//fin del bloque Signature (parte 2)

                
                escribirRec.WriteEndElement();//Fin del bloque EnvioDTE
                escribirRec.WriteEndDocument();//Fin de la documentacion en xml
                escribirRec.Close();

                //A partir de este bloque se empezará a añadir un tercer bloque de signature, que es probable es el que se utilice en los próximos adelantos
                //
                //RSACryptoServiceProvider llaveRSA = new RSACryptoServiceProvider();

                //generarSignatureFinal(uriSetDoc, llaveRSA, certificado);

                //Fin del bloque signature (tercer bloque)
                //

                rta = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show("error al intentar escribir xml desde otra clase: " + ex.Message);
                rta = false;
            }
            return rta;
        }




        public bool nodoTransaccionXmlSii(String[,] matrizDetalle, Int32 contadorDetalle, Int32 contadorCols, Int32 codFacturaNew, Int32 subTotal, Int32 iva, Int32 total, DataTable dtDatosCliente) 
        {

            //datos del emisor
            String rutEmisor = "7664511-6";
            String rutEnvia = "7664511-6";
            String razonSocialEmisor = "AREZA";
            String giroEmpresaEmisor = "PRESTACIONES DE SERVICIOS DISENO GRAFICO";
            String direccionEmisor = "AVELINO VILLAGRAN 2266";
            String comunaEmisor = "ARICA";
            String ciudadEmisor = "ARICA";
            
            
            
            //datos del receptor
            //String rutreceptor = "3333333-3";
            String rutreceptor = dtDatosCliente.Rows[0][0].ToString() + "-" + dtDatosCliente.Rows[0][1];
            //String razonSocialReceptor = "Razon social del receptor";
            String razonSocialReceptor = dtDatosCliente.Rows[0][2].ToString().ToUpper();
            String giroEmpresaReceptor = "Giro del Receptor".ToUpper();
            //String direccionReceptor = "Direccion del Receptor";
            String direccionReceptor = dtDatosCliente.Rows[0][3].ToString().ToUpper();
            //String comunaReceptor = "Comuna del receptor";
            String comunaReceptor = dtDatosCliente.Rows[0][4].ToString().ToUpper();
            //String ciudadReceptor = "Ciudad del Receptor";
            String ciudadReceptor = dtDatosCliente.Rows[0][5].ToString().ToUpper();

            //datos de fecha y hora
            String fecha = DateTime.Today.ToString("yyyy-MM-dd"); //También se lo llama "Fecha de emision"
            String hora = DateTime.Now.ToString("H:mm:ss", new CultureInfo("es-CL")); ;

            Int32 codFactura = codFacturaNew; //también se lo llama "Folio" o "codFolio"
            //String giroEmpresa = "jdnje"; //es el rubro en el que la empresa se especializa
            //String razonSocial = "diseno"; //es el nombre de la persona o empresa

            //Int32 subTotal = 10000;
            //Int32 iva = 1900;
            //Int32 total = 11900;

            XmlDocument xmlDoc;
            XmlNode raiz;

            XmlNode ident;
            XmlNode ident2;//Esto es para la descripcion de lo que es la factura, los totales, el emisor y el receptor
            XmlNode ident3;//Esto es para la descripcion de los detalles de cualquier producto integrado en la factura en cuestion
            XmlNode ident4;
            XmlNode ident5;
            XmlNode ident6;

            bool rta = false;

            try
            {
                xmlDoc = new XmlDocument();
                xmlDoc.Load("documentosXml/TransaccionesSii.xml");
                raiz = xmlDoc.DocumentElement;

                
                ident = raiz.FirstChild.FirstChild;

                ident.InnerXml = "\r\n<RutEmisor></RutEmisor>" +
                    "<RutEnvia></RutEnvia>" +
                    "<RutReceptor></RutReceptor>" +
                    "<FchResol></FchResol>" +
                    "<NroResol></NroResol>" +
                    "<TmstFirmaEnv></TmstFirmaEnv>" +
                    "\r\n<SubTotDTE><TpoDTE>33</TpoDTE><NroDTE>1</NroDTE></SubTotDTE>";
                
                //XmlElement elemento = xmlDoc.CreateElement("", "Caratula", null);
                //XmlElement nuevo = xmlDoc.CreateElement();
                //XmlElement nuevo = new XmlElement();
                /*
                elemento.AppendChild(xmlDoc.CreateWhitespace("\r\n"));
                elemento.InnerXml = "<RutEmisor></RutEmisor>\r\n" +
                    "<RutEnvia></RutEnvia>\r\n" +
                    "<RutReceptor></RutReceptor>\r\n" +
                    "<FchResol></FchResol>\r\n" +
                    "<NroResol></NroResol>\r\n" +
                    "<TmstFirmaEnv></TmstFirmaEnv>\r\n";*/

                /*
                elemento.AppendChild(xmlDoc.CreateWhitespace("\r\n"));
                elemento["RutEmisor"].InnerText = rutEmisor;
                elemento["RutEnvia"].InnerText = rutEnvia;
                elemento["RutReceptor"].InnerText = rutreceptor;
                elemento["FchResol"].InnerText = fecha;
                elemento["NroResol"].InnerText = "0";
                elemento["TmstFirmaEnv"].InnerText = fecha + "T" + hora;
                */

                ident.AppendChild(xmlDoc.CreateWhitespace("\r\n"));
                ident["RutEmisor"].InnerText = rutEmisor;
                ident["RutEnvia"].InnerText = rutEnvia;
                ident["RutReceptor"].InnerText = rutreceptor;
                ident["FchResol"].InnerText = fecha;
                ident["NroResol"].InnerText = "0";
                ident["TmstFirmaEnv"].InnerText = fecha + "T" + hora;

                //ident["TpoDTE"].InnerText = "33";
                //ident["NroDTE"].InnerText = "1";

                //ident.InsertAfter(elemento, ident.LastChild);
                ident.InsertAfter(ident.FirstChild, ident.LastChild);

                ident2 = raiz.FirstChild.FirstChild.NextSibling.FirstChild.FirstChild;//El nodo ident2 se posicionará dentro del bloque <Encabezado>

                
                ident2.InnerXml = "\r\n<IdDoc></IdDoc>" +
                    "<Emisor></Emisor>" +
                    "<Receptor></Receptor>" +
                    "<Totales></Totales>";
                                
                XmlNode ident2_1 = ident2.FirstChild.NextSibling;//El nodo ident2_1 se posicionara en la ubicación en donde esta su nodo padre ident2. En este caso corresponde a la etiqueta <IdDoc>.
                //ADVENTENCIA: para encontrar la primera posicion (o etiqueta) del nodo ident2 es necesaro poner .FirstChild.NextSibling , porque colocando .FirstChild nos dara errores de reconocimiento 
                ident2_1.InnerXml = "\r\n<TipoDTE></TipoDTE>" + 
                    "<Folio></Folio>" + 
                    "<FchEmis></FchEmis>";

                ident2_1["TipoDTE"].InnerText = "33";
                ident2_1["Folio"].InnerText = codFactura.ToString();
                ident2_1["FchEmis"].InnerText = fecha;


                ident2_1 = ident2.FirstChild.NextSibling.NextSibling;//En este caso corresponde a la etiqueta <Emisor>.
                ident2_1.InnerXml = "\r\n<RUTEmisor></RUTEmisor>" +
                    "<RznSoc></RznSoc>" +
                    "<GiroEmis></GiroEmis>" +
                    "<Acteco></Acteco>" +
                    "<CdgSIISucur></CdgSIISucur>" +
                    "<DirOrigen></DirOrigen>" +
                    "<CmnaOrigen></CmnaOrigen>" +
                    "<CiudadOrigen></CiudadOrigen>";

                ident2_1["RUTEmisor"].InnerText = rutEmisor;
                ident2_1["RznSoc"].InnerText = razonSocialEmisor;
                ident2_1["GiroEmis"].InnerText = giroEmpresaEmisor;
                ident2_1["Acteco"].InnerText = "31341";
                ident2_1["CdgSIISucur"].InnerText = "1234";
                ident2_1["DirOrigen"].InnerText = direccionEmisor;
                ident2_1["CmnaOrigen"].InnerText = comunaEmisor;
                ident2_1["CiudadOrigen"].InnerText = ciudadEmisor;

                ident2_1 = ident2.FirstChild.NextSibling.NextSibling.NextSibling;//En este caso corresponde a la etiqueta <Receptor>.
                ident2_1.InnerXml = "\r\n<RUTRecep></RUTRecep>" +
                    "<RznSocRecep></RznSocRecep>" +
                    "<GiroRecep></GiroRecep>" +
                    "<DirRecep></DirRecep>" +
                    "<CmnaRecep></CmnaRecep>" +
                    "<CiudadRecep></CiudadRecep>";

                ident2_1["RUTRecep"].InnerText = rutreceptor;
                ident2_1["RznSocRecep"].InnerText = razonSocialReceptor;
                ident2_1["GiroRecep"].InnerText = giroEmpresaReceptor;
                ident2_1["DirRecep"].InnerText = direccionReceptor;
                ident2_1["CmnaRecep"].InnerText = comunaReceptor;
                ident2_1["CiudadRecep"].InnerText = ciudadReceptor;

                ident2_1 = ident2.LastChild;//En este caso corresponde a la etiqueta <Totales>.
                ident2_1.InnerXml = "\r\n<MntNeto></MntNeto>" +
                    "<TasaIVA></TasaIVA>" +
                    "<IVA></IVA>" +
                    "<MntTotal></MntTotal>";

                ident2_1["MntNeto"].InnerText = subTotal.ToString();
                ident2_1["TasaIVA"].InnerText = "19";
                ident2_1["IVA"].InnerText = iva.ToString();
                ident2_1["MntTotal"].InnerText = total.ToString();


                //XmlElement elem = xmlDoc.CreateElement("Elemento");
                //xmlDoc.DocumentElement.AppendChild(elem);

                //String next = "NextSibling";
                //ident3 = raiz.FirstChild.FirstChild.NextSibling.FirstChild.FirstChild.NextSibling;
                //ident3 = raiz.FirstChild.FirstChild.NextSibling.FirstChild.FirstChild.NextSibling;
                ident3 = raiz.FirstChild.FirstChild.NextSibling.FirstChild.FirstChild;
                
                for (Int32 i = 0; i < contadorDetalle; i++)
                {
                    ident3 = ident3.NextSibling;
                    XmlNode ident3_1 = ident3;

                    XmlNode ident3_1_1 = ident3_1.FirstChild.NextSibling;
                    ident3_1_1["TpoCodigo"].InnerText = "INT1";
                    ident3_1_1["VlrCodigo"].InnerText = matrizDetalle[i,0];
                    //ident3 = raiz.FirstChild.FirstChild.NextSibling.FirstChild.FirstChild.NextSibling;
                    ident3_1["NmbItem"].InnerText = matrizDetalle[i,1];
                    ident3_1["QtyItem"].InnerText = matrizDetalle[i,3];
                    ident3_1["PrcItem"].InnerText = matrizDetalle[i,2];
                    ident3_1["MontoItem"].InnerText = matrizDetalle[i,4];

                    //ident3 = ident3.NextSibling;
                }
                


                ident4 = raiz.FirstChild.FirstChild.NextSibling.FirstChild.LastChild.PreviousSibling.FirstChild;//La ubicacion de la etiqueta <DD>
                //ident4.InnerXml = "<aaaaaaaaa></aaaaaaaaa>";
                ident4.InnerXml = "<RE></RE>" +
                    "<TD></TD>" +
                    "<F></F>" +
                    "<FE></FE>" +
                    "<RR></RR>" +
                    "<RSR></RSR>" +
                    "<MNT></MNT>" +
                    "<IT1></IT1>" +
                    "<CAF></CAF>" +
                    "<TSTED></TSTED>";

                ident4["RE"].InnerText = rutEmisor;
                ident4["TD"].InnerText = "33";
                ident4["F"].InnerText = codFactura.ToString();
                ident4["FE"].InnerText = fecha;
                ident4["RR"].InnerText = rutreceptor;
                ident4["RSR"].InnerText = razonSocialReceptor;
                ident4["MNT"].InnerText = total.ToString();
                ident4["IT1"].InnerText = matrizDetalle[0,1];
                ident4["TSTED"].InnerText = fecha + "T" + hora;

                XmlNode ident4_1 = ident4.LastChild.PreviousSibling;//posicion en el bloque o elemento CAF
                XmlAttribute atributo = xmlDoc.CreateAttribute("version");//se crea el atributo
                atributo.Value = "1.0";//este atributo va la tener el valor descrito
                ident4_1.Attributes.SetNamedItem(atributo);//ese atributo se adiere al elemento CAF
                //ident4_1.InnerXml = "<ooo></ooo>";
                ident4_1.InnerXml = "<DA></DA>" +
                    "<FRMA></FRMA>";

                XmlNode ident4_1_1 = ident4_1.FirstChild;//posicion en el bloque o elemento <DA>
                ident4_1_1.InnerXml = "<RE></RE>" +
                    "<RS></RS>" +
                    "<TD></TD>" +
                    "<RNG></RNG>" +
                    "<FA></FA>" +
                    "<RSAPK></RSAPK>" +
                    "<IDK></IDK>";

                ident4_1_1["RE"].InnerText = rutEmisor;
                ident4_1_1["RS"].InnerText = razonSocialEmisor;
                ident4_1_1["TD"].InnerText = "33";
                //<RNG>
                ident4_1_1["FA"].InnerText = fecha;
                //<RSAPK>
                ident4_1_1["IDK"].InnerText = "100";

                XmlNode ident4_1_1_1 = ident4_1_1.FirstChild.NextSibling.NextSibling.NextSibling;//posicion en el bloque <RNG>
                ident4_1_1_1.InnerXml = "<D></D><H></H>";
                ident4_1_1_1["D"].InnerText = "1";
                ident4_1_1_1["H"].InnerText = "200";

                XmlNode ident4_1_1_2 = ident4_1_1.LastChild.PreviousSibling;//posicion en el bloque <RSAPK>
                ident4_1_1_2.InnerXml = "<M></M><E></E>";
                ident4_1_1_2["M"].InnerText = rsapkModulo;
                ident4_1_1_2["E"].InnerText = rsapkExponente;

                XmlNode ident4_1_2 = ident4_1.LastChild;//dentro de la etiqueta <FRMA>
                XmlAttribute attFirma = xmlDoc.CreateAttribute("algoritmo");
                attFirma.Value = "SHA1withRSA";
                ident4_1_2.Attributes.SetNamedItem(attFirma);
                //ident4_1_2.InnerXml = "<aaaaaaaaaaaaaaa></aaaaaaaaaaaaaaa>";
                ident4_1["FRMA"].InnerText = firma;


                ident5 = raiz.FirstChild.FirstChild.NextSibling.FirstChild.LastChild.PreviousSibling;//etiqueta <FRMT>
                ident5["FRMT"].InnerText = frmt;

                ident6 = raiz.FirstChild.FirstChild.NextSibling.FirstChild;
                ident6["TmstFirma"].InnerText = fecha + "T" + hora;

                XmlTextWriter escribirRec = new XmlTextWriter("documentosXml/TransaccionesSii2.xml", System.Text.Encoding.GetEncoding("ISO-8859-1"));
                xmlDoc.WriteTo(escribirRec);
                escribirRec.Close();
                rta = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Hay un problema en cuanto a meter datos para el formulario xml en nodoTransaccionXmlSii: " + ex.Message);
                rta = false;
            }
            return rta;
        }

        /*
        public void generarLlaves()
        {
            
        }
        */

        public void lalala() 
        {
            try
            {
                XmlDocument xmlDoc = new XmlDocument();
                xmlDoc.PreserveWhitespace = true;
                xmlDoc.Load("documentosXml/Transacciones.xml");

                //X509Store store = new X509Store(StoreName.Root);
                X509Store store = new X509Store(StoreName.My);
                //X509Store store = new X509Store(StoreLocation.CurrentUser);
                store.Open(OpenFlags.ReadOnly);
                X509Certificate2Collection certCollection = store.Certificates;
                X509Certificate2 cert = null;

                foreach (X509Certificate2 c in certCollection)
                {
                    MessageBox.Show("Certificado: " + c);
                    if (c.Subject == "CN=XML_ENC_TEST_CERT")
                    {
                        cert = c;
                        break;
                    }
                }

                if (cert == null)
                {
                    throw new CryptographicException("El certificado x.509 no pudo ser encontrado");
                }

                store.Close();

                Encrypt(xmlDoc, "RespuestaExitosa",  cert);

                xmlDoc.Save("documentosXml/Transacciones3.xml");

            }
            catch (Exception ex)
            {
                MessageBox.Show("Error en el asunto del certificado x509: " + ex.Message);
            }
        }

        public static void Encrypt(XmlDocument Doc, String ElementToEncrypt, X509Certificate2 Cert)
        {            
            if (Doc == null)
            {
                throw new ArgumentNullException("Doc");
            }
            if (ElementToEncrypt == null)
            {
                throw new ArgumentNullException("ElementToEncrypt");
            }
            if (Cert == null)
            {
                throw new ArgumentNullException("Cert");
            }

            XmlElement elementToEncrypt = Doc.GetElementsByTagName(ElementToEncrypt)[0] as XmlElement;
            if (elementToEncrypt == null)
            {
                throw new XmlException("El elemento especificado no fue encontrado");
            }
            
            EncryptedXml eXml = new EncryptedXml();
            EncryptedData edElement = eXml.Encrypt(elementToEncrypt, Cert);
            EncryptedXml.ReplaceElement(elementToEncrypt, edElement, false);
            
        }

        public void generarXml()
        {
            
            String resourceToSign = "http://www.microsoft.com"; //la URI a firmar
            //String resourceToSign = "#SetDoc"; //la URI a firmar
            String xmlFileName = "documentosXml/Transacciones.xml";
            //String xmlFileName = "documentosXml/xmldsig.xml"; //el nombre del archivo en el cual se guardará la firma (signature) del Xml
            String certificate = "certificacion/microsoft.cer"; //el nombre del certificado X509

            


            try
            {
                RSACryptoServiceProvider key = new RSACryptoServiceProvider();//genera la llave de firma, y que deberá coincidir con el certificado

                signDetachedResource(resourceToSign, xmlFileName, key, certificate);

            }
            catch (Exception ex)
            {
                MessageBox.Show("Algo pasó en el método generarXml:\r\n" + ex.Message);
            }
        }

        public static void signDetachedResource(String URIString, String xmlSignFileName, RSA Key, String certificate)
        {
            XmlDocument doc = new XmlDocument();//se crea un nevo documento en xml
            doc.PreserveWhitespace = false; //formatea el documento para ignorar los espacios en blanco
            doc.Load(new XmlTextReader(xmlSignFileName));//se manda a cargar el archivo en xml, si es que existe


            SignedXml signedXml = new SignedXml(doc);
            signedXml.SigningKey = Key;

            Reference reference = new Reference();
            reference.Uri = URIString;

            //XmlDsigEnvelopedSignatureTransform env = new XmlDsigEnvelopedSignatureTransform();
            //XmlDsigExcC14NTransform env = new XmlDsigExcC14NTransform();
            XmlDsigC14NTransform env = new XmlDsigC14NTransform();
            reference.AddTransform(env);


            signedXml.AddReference(reference);
            KeyInfo keyInfo = new KeyInfo();

            keyInfo.AddClause(new RSAKeyValue((RSA)Key));

            X509Certificate MSCert = X509Certificate.CreateFromCertFile(certificate);
            if(MSCert == null)
            {
                throw new CryptographicException("MSCert");
            }
            keyInfo.AddClause(new KeyInfoX509Data(MSCert));
            signedXml.KeyInfo = keyInfo;
            signedXml.ComputeSignature();
            XmlElement xmlDigitalSignature = signedXml.GetXml();

            doc.DocumentElement.AppendChild(doc.ImportNode(xmlDigitalSignature, true));
            if(doc.FirstChild is XmlDeclaration)            
            {
                doc.RemoveChild(doc.FirstChild);
            }
            //XmlTextWriter xmltw = new XmlTextWriter(xmlSignFileName, Encoding.GetEncoding("ISO-8859-1"));
            XmlTextWriter xmltw = new XmlTextWriter("documentosXml/Transacciones4.xml", Encoding.GetEncoding("ISO-8859-1"));
            xmlDigitalSignature.WriteTo(xmltw);
            xmltw.Close();
        }

        public void generarSignatureFinal()
        {
            XmlDocument doc = new XmlDocument();//se crea un nevo documento en xml
            doc.PreserveWhitespace = true; //formatea el documento para ignorar los espacios en blanco
            //doc.Load(new XmlTextReader("documentosXml/TransaccionesSii2.xml"));//se manda a cargar el archivo en xml, si es que existe
            doc.Load("documentosXml/TransaccionesSii2.xml");//se manda a cargar el archivo en xml, si es que existe

            RSACryptoServiceProvider llaveRSA = new RSACryptoServiceProvider();

            SignedXml signedXml = new SignedXml();
            signedXml.SigningKey = llaveRSA;

            //Uri uri = new Uri(uriSetDoc, UriKind.Relative);

            Reference referenciaSetDoc = new Reference();
            //Uri uriRefSetDoc = new Uri(uriSetDoc, UriKind.Relative);
            referenciaSetDoc.Uri = uriSetDoc;
            //referenciaSetDoc.Uri = uri;

            XmlDsigC14NTransform env = new XmlDsigC14NTransform();
            referenciaSetDoc.AddTransform(env);
            signedXml.AddReference(referenciaSetDoc);

            KeyInfo infoLlave = new KeyInfo();
            infoLlave.AddClause(new RSAKeyValue((RSA)llaveRSA));

            X509Certificate MSCert = X509Certificate.CreateFromCertFile(certificado);
            infoLlave.AddClause(new KeyInfoX509Data(MSCert));
            signedXml.KeyInfo = infoLlave;
            signedXml.ComputeSignature();

            XmlElement xmlDigitalSignature = signedXml.GetXml();

            XmlNode raiz = doc.DocumentElement;

            //raiz.LastChild.PreviousSibling.AppendChild(doc.ImportNode(xmlDigitalSignature, true));
            doc.DocumentElement.AppendChild(doc.ImportNode(xmlDigitalSignature, true));
            doc.Save("documentosXml/TransaccionesSii2.xml");
            /*
            if (doc.LastChild is XmlDeclaration)
            {
                doc.RemoveChild(doc.LastChild);
            }
            */
            //doc.RemoveChild(doc.FirstChild);
            //XmlTextWriter xmltw = new XmlTextWriter("documentosXml/TransaccionesSii2.xml", Encoding.GetEncoding("ISO-8859-1"));
            //XmlTextWriter xmltw = new XmlTextWriter("documentosXml/TransaccionesSii3.xml", Encoding.GetEncoding("ISO-8859-1"));

            //xmlDigitalSignature.WriteTo(xmltw);
            //xmltw.Close();
        }


        public void principalClaveRsa()
        {
            XmlDocument xmlDoc = new XmlDocument();

            try
            {
                xmlDoc.PreserveWhitespace = true;
                xmlDoc.Load("documentosXml/Transacciones.xml");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Problemas en el metodo principalClaveRsa:\r\n\r\n" + ex.Message);
            }

            //Se crea un nuevo objeto de parametro cspParameters
            //para especificar un contenedor de llave
            CspParameters cspParams = new CspParameters();
            cspParams.KeyContainerName = "XML_ENC_RSA_KEY";

            //Se crea una nueva llave RSA y lo guarda en el contenedor
            //Con esta llave se cifrará una llave simétrica, en la cual será cifrada dentro del documento xml
            RSACryptoServiceProvider rsaKay = new RSACryptoServiceProvider();
            
            

            try
            {
                EncryptRSAKey(xmlDoc, "RespuestaFalida", "EncryptedElement1", rsaKay, "rsaKey");
                xmlDoc.Save("documentosXml/Transacciones5.xml");
            }
            catch(Exception ex)
            {
                MessageBox.Show("Error en la elabopración de la llave RSA:\r\n\r\n" + ex.Message);
            }

        }

        public static void EncryptRSAKey(XmlDocument doc, String ElementoToEncrypt, String EncryptionElementID, RSA Alg, String KeyName)
        {
            if (doc == null)
            {
                throw new ArgumentNullException("Doc");
            }
            if (ElementoToEncrypt == null)
            {
                throw new ArgumentNullException("ElementoToEncrypt");
            }
            if (EncryptionElementID == null)
            {
                throw new ArgumentNullException("EncryptionElementID");
            }
            if (Alg == null)
            {
                throw new ArgumentNullException("Alg");
            }
            if (KeyName == null)
            {
                throw new ArgumentNullException("KeyName");
            }


            //Se crea un elemento xml específico en el xmlDocument
            //y crea un nuevo objeto xmlElement
            XmlElement elementToEncrypt = doc.GetElementsByTagName(ElementoToEncrypt)[0] as XmlElement;
            if (elementToEncrypt == null)
            {
                throw new XmlException("El elemento especioficado no fue encontrado");
            }

            RijndaelManaged sessionsKey = null;

            //RSAParameters parametrosRsa = new RSAParameters();
            //parametrosRsa.Modulus = modByte;


            try
            {
                //Se crea un anueva instancia de la clase...
                //y lo usa para cifrar el xmlElement con una nueva llave simetrica aleatoria

                //Se crea una nueva llave Rijndael de 256 bits
                sessionsKey = new RijndaelManaged();
                sessionsKey.KeySize = 256;

                EncryptedXml eXml = new EncryptedXml();

                byte[] encryptedElement = eXml.EncryptData(elementToEncrypt, sessionsKey, false);

                //Se construye un objeto EncryptedData...
                //y lo populiza con informacion cifrada
                EncryptedData edElement = new EncryptedData();
                edElement.Type = EncryptedXml.XmlEncElementUrl;
                edElement.Id = EncryptionElementID;

                //Se crea un elemento de metodo EncryptionMethod...
                //y al recibirlo él sabe cual algoritmo usar para el descifrado
                edElement.EncryptionMethod = new EncryptionMethod(EncryptedXml.XmlEncAES256Url);

                //Cifra la llave de sesión y lo añade al elemento EncryptedKeey
                EncryptedKey ek = new EncryptedKey();

                byte[] encryptedKey = EncryptedXml.EncryptKey(sessionsKey.Key, Alg, false);
                ek.CipherData = new CipherData(encryptedKey);
                ek.EncryptionMethod = new EncryptionMethod(EncryptedXml.XmlEncRSA15Url);

                //Se crea un nuevo elemento DataReference para el elemento keyInfo. Este elemento opcional especifica...
                //...cual EncryptedData utiliza esta llave. Un documento XmlDocument puede tener multiples elementos EncryptedData...
                //...que utilizan llaves diferentes
                DataReference dRef = new DataReference();
                dRef.Uri = "#" + EncryptionElementID; //especifica la Uri de EncryptedData

                //añade la DataReference a la EncryptedKey
                ek.AddReference(dRef);

                //añade la EncryptedKey al objeto EncyptedData
                edElement.KeyInfo.AddClause(new KeyInfoEncryptedKey(ek));

                //establece el elemento keyInfo con una especificacion de nombre de la llave RSA
                KeyInfoName kin = new KeyInfoName();
                
                //Eespecifica un nombre para la llave
                kin.Value = KeyName;

                //Añade el elemento KeyInfoName al objeto EncryptedKey
                ek.KeyInfo.AddClause(kin);

                //Añade los datos del elemento cifrado al objeto EncryptedData
                edElement.CipherData.CipherValue = encryptedElement;

                //Reemplaza el elemento del objeto XmlDocument original con el elemento EncryptedData
                EncryptedXml.ReplaceElement(elementToEncrypt, edElement, false);


            }
            catch (Exception ex)
            {
                MessageBox.Show("Algo paso en el metodo estatico:\r\n\r\n\r\n" + ex.Message);
            }
            finally
            {
                if (sessionsKey != null)
                {
                    sessionsKey.Clear();
                }
            }

        }


        public string GetSHA1(string str)
        {
            SHA1 sha1 = SHA1Managed.Create();
            ASCIIEncoding encoding = new ASCIIEncoding();
            byte[] stream = null;
            StringBuilder sb = new StringBuilder();
            stream = sha1.ComputeHash(encoding.GetBytes(str));
            for (int i = 0; i < stream.Length; i++) sb.AppendFormat("{0:x2}", stream[i]);
            return sb.ToString();
        }

        /*
        public string obtenerShaRsa()
        {
            String valor = "Todo lo que me importa";
            byte[] msgBytes = Encoding.ASCII.GetBytes(valor);
            SHA1 sha1 = SHA1.Create();
            byte[] digestion = sha1.ComputeHash(msgBytes, 0, msgBytes.Length);

            RSACryptoServiceProvider rsa = new RSACryptoServiceProvider();

            //String mensajeConvertido = Convert.ToBase64String();
        }*/






        //public bool VerifySignature(byte[] sign, byte[] data, RSACryptoServiceProvider rsa)
        //{
        //    bool bandera = rsa.VerifyData(data, CryptoConfig.MapNameToOID("SHA1"), sign);
        //    Response.Write(rsa.SignatureAlgorithm);
        //    if (bandera == true)
        //    {                
        //        return true;//La firma es valida
        //    }
        //    else
        //    {                
        //        return false;//La firma no es valida
        //    }
        //}

        ///** Verifica la integridad y la seguridad de los datos a traves de la llave publica del certificado digital 
        //* @param data Datos a verificar 
        //* @return Valor booleano determinando si se cumplio o no la ocndicion de seguridad 
        //*/

        //public bool checkSign(String data, String modulo, byte[] num)
        //{
        //    byte[] modByte = this.hex2bin(modulo);
        //    Response.Write(this.bin2Hex(modByte));
        //    RSAParameters param = new RSAParameters();
        //    param.Modulus = modByte;
        //    param.Exponent = num;
        //    byte[] datab = Encoding.ASCII.GetBytes(data);
        //    RSACryptoServiceProvider rsa = new RSACryptoServiceProvider();
        //    rsa.ImportParameters(param);
        //    return VerifySignature(this.signByte, datab, rsa);
        //}

        ///** **********  FORMA EN QUE LO INVOCO DENTRO DEL ASP ********************/

        //byte[] exp = new byte[] { 0x01, 0x01, 0x12 };
        //String modulo = "D6D12352422B62230FFF0E03B3BB816C5F20AE0FA644AF319FD82C0FF995736DC7ECA014E5FFFFFFF8E850DDCAF2C9A8709B7AAAAAE226EF394F56FF0FC760AE5B02FE118504499AFC9591BDB6D49897E9934C4A18E1EB13D23DFF88AC319ED2209FCDF57341E556E81FE265B5F5AC5C5E077F78466D6846C135312892EF445843B60423BBE491A172287C6FB42DE96F58D1949705B3B7AA421FDC89C34FFD7FC5470137086FA9F8F5B16F8D218FE3912058B5DE3AA4E324CD825F4A4E23BEE778A5C5B0625B1C6A0004EEBC100356F425A6279A254CDB3807ADD65C85024A9F04C25FFCE2E958AF12FAC4D62597101BF1A8F515CF389B80A14BEB6D58F59B";
        //String data = "version=" + this.version + "&user=" + this.user + "&tpa_id=" + this.tpa_id + "&expires=" + this.expires + "&action=" + this.action + "&flags=" + this.flags + "&userdata=" + this.userdata;
        //bool validacion = this.checkSign(data, modulo, exp);



        
    }
}
