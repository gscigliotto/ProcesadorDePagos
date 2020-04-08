using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProcesarPDF
{
    public class Comprobante
    {
        public enum Tipo { PAGO = 1, TRANSF = 2 }
        public Tipo Movimiento { get; set; }
        public string Numero { get; set; }
        public DateTime Fecha { get; set; }
        public string Beneficiario { get; set; }

        public double Importe { get; set; }
        public string Concepto { get; set; }
        public string Referencia { get; set; }


        public Comprobante(Tipo Movimiento, string Numero, DateTime Fecha, string Beneficiario, double Importe, string Concepto, string Referencia) {
            this.Movimiento = Movimiento;
            this.Numero = Numero;
            this.Fecha = Fecha;
            this.Beneficiario = Beneficiario;
            this.Importe = Importe;
            this.Concepto = Concepto;
            this.Referencia = Referencia;
        }
    }
}
