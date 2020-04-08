using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProcesarPDF;
using Repositorio;
namespace ProcesadorDePagos
{
    class Program
    {
        static void Main(string[] args)
        {

            RepoStore repo = new RepoStore();
            //repo.Prueba();
            
            Procesar procesar = new Procesar();
            //procesar.ProcesarRuta(@"C:\Users\gscigliotto\Documents\Documentos Guille\pagos\2018\Diciembre\transferencia_TIO.pdf");
            List<Comprobante> comprobantes = procesar.ObtenerComrpobantesEquipo();
            repo.SendData(comprobantes);
        }
    }
}
