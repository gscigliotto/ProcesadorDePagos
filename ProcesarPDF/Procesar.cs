using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using iTextSharp;
using iTextSharp.text.pdf;
using System.Text.RegularExpressions;
using System.Globalization;
using System.IO;

namespace ProcesarPDF
{
    public class Procesar
    {
        private string canonizar(string str) {
            return str.Replace("(", "").Replace(")Tj", "").Replace("$ ", "");
        }
        private string NormalizarStr(string str) {
            return canonizar(str);
        }
        private int NormalizarInt(string str)
        {
           return Convert.ToInt32(canonizar(str));
        }

        private double NormalizarDouble(string str) {
            string canon = canonizar(str);
            
            return Double.Parse(canon.Replace(".","").Replace("IMPORTE:","").Replace(")Tj",""));
        }

        private DateTime NormalizarFecha(string str) {

            return DateTime.Parse(canonizar(str));
        }
        public Comprobante ProcesarRuta(string url)   {
            //  BuscarPDF();

            string numeroTran;
            DateTime fechaTran;
            string benefTran;
            double importeTran;
            string conceptoTran;
            string refTran;
            Comprobante cpte = null;
            try
            {
                PdfReader reader = new PdfReader(url);

                byte[] pageContent = reader.GetPageContent(1); //not zero based
                byte[] utf8 = Encoding.Convert(Encoding.Default, Encoding.UTF8, pageContent);
                string textFromPage = Encoding.UTF8.GetString(utf8);
                //Averiguo si es pago de servicio o transferencia
                int posicionServicio = textFromPage.IndexOf("(Comprobante de Pago de Servicios o Impuestos)Tj");
                int posicionTran = textFromPage.IndexOf("(Transferencias)Tj");


                MatchCollection col = Regex.Matches(textFromPage, @"()(\(.*?\w*\))(Tj)");
    

                if (posicionServicio != -1)
                {


                    numeroTran = NormalizarStr(col[6].Value);
                    fechaTran = NormalizarFecha(col[2].Value);
                    benefTran = NormalizarStr(col[7].Value);
                    importeTran = NormalizarDouble(col[14].Value);
                    conceptoTran = NormalizarStr(col[8].Value);
                    refTran = NormalizarStr(col[9].Value);
                    cpte = new Comprobante(Comprobante.Tipo.PAGO, numeroTran.Replace(")", ""), fechaTran, benefTran, importeTran, conceptoTran, refTran);

                }
                else if (posicionTran != -1)
                {

                    numeroTran = NormalizarStr(col[17].Value);
                    fechaTran = NormalizarFecha(col[7].Value);
                    benefTran = NormalizarStr(col[33].Value);
                    importeTran = NormalizarDouble(col[13].Value);
                    conceptoTran = NormalizarStr(col[21].Value);
                    refTran = NormalizarStr(col[19].Value);
                    cpte = new Comprobante(Comprobante.Tipo.TRANSF, numeroTran, fechaTran, benefTran, importeTran, conceptoTran, refTran);

                }

               
            }
            catch (Exception e) {


            }
            if (cpte == null)
            {
                throw new ComrobanteIvalidoException();
            }
            return cpte;
        }


        public List<Comprobante> ObtenerComrpobantesEquipo() {
            Dictionary<string, string> cptesExistentes = FileDictionary.Read();
            string[] directories = Directory.GetFiles(@"C:\Users\gscig\Documents", "*.pdf",SearchOption.AllDirectories);
            //DirectoryInfo directory = new DirectoryInfo(@"C:\Users\gscigliotto\Documents\Documentos Guille\pagos");
            //DirectoryInfo[] directories = directory.GetDirectories();
            List<Comprobante> comprobantes = new List<Comprobante>(); 
            for (int i = 0; i < directories.Length; i++)
            {

                        try {

                            Comprobante cpt = ProcesarRuta(directories[i]);
                            try
                            {
                                cptesExistentes.Add(cpt.Numero, "SI");
                            }
                            catch (Exception e) {
                            }
                            

                            comprobantes.Add(cpt);
                        } catch (ComrobanteIvalidoException exp) {


                        }
 
            }
            FileDictionary.Write(cptesExistentes);
            return comprobantes;
        }

    }
}
