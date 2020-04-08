using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProcesarPDF
{
    public interface IExportDataHandler
    {
        void SendData(List<Comprobante> comprobantes);
    }
}
