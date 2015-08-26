using Imposto.Core.Domain;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;

namespace Imposto.Core.Service
{
    [Serializable]
    public class NotaFiscalService
    {
        public void GerarNotaFiscal(Pedido pedido)
        {
            NotaFiscal notaFiscal = new NotaFiscal();
            notaFiscal.EmitirNotaFiscal(pedido);
        }
        public NotaFiscalService() { }
    }
}
