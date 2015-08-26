using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using System.Data.SqlClient;
using System.Xml;
using System.Data;


namespace Imposto.Core.Domain
{
    [Serializable]
    public class NotaFiscal
    {
        public int Id { get; set; }
        public int NumeroNotaFiscal { get; set; }
        public int Serie { get; set; }
        public string NomeCliente { get; set; }

        public string EstadoDestino { get; set; }
        public string EstadoOrigem { get; set; }

        public IEnumerable<NotaFiscalItem> ItensDaNotaFiscal { get; set; }

        public NotaFiscal(){}

        public void EmitirNotaFiscal(Pedido pedido)
        {
            //Exercicio 1 e 2

            ItensDaNotaFiscal = new List<NotaFiscalItem>();
            NotaFiscal notaFiscal = this;

            this.NumeroNotaFiscal = 99999;
            this.Serie = new Random().Next(Int32.MaxValue);
            this.NomeCliente = pedido.NomeCliente;

            //Exercicio 5
            this.EstadoDestino = pedido.EstadoDestino;
            this.EstadoOrigem = pedido.EstadoOrigem; 

            string strCon = "Data Source=ERON-PC\\SQLEXPRESS;Initial Catalog=Teste;Integrated Security=SSPI;User ID=Eron-PC\\Eron;Password=ev258074;";
            SqlConnection conn = new SqlConnection(strCon);

            foreach (PedidoItem itemPedido in pedido.ItensDoPedido)
            {
                NotaFiscalItem notaFiscalItem = new NotaFiscalItem();
                if ((this.EstadoOrigem == "SP") && (this.EstadoDestino == "RJ"))
                {
                    notaFiscalItem.Cfop = "6.000";                    
                }
                else if ((this.EstadoOrigem == "SP") && (this.EstadoDestino == "PE"))
                {
                    notaFiscalItem.Cfop = "6.001";
                }
                else if ((this.EstadoOrigem == "SP") && (this.EstadoDestino == "MG"))
                {
                    notaFiscalItem.Cfop = "6.002";
                }
                else if ((this.EstadoOrigem == "SP") && (this.EstadoDestino == "PB"))
                {
                    notaFiscalItem.Cfop = "6.003";
                }
                else if ((this.EstadoOrigem == "SP") && (this.EstadoDestino == "PR"))
                {
                    notaFiscalItem.Cfop = "6.004";
                }
                else if ((this.EstadoOrigem == "SP") && (this.EstadoDestino == "PI"))
                {
                    notaFiscalItem.Cfop = "6.005";
                }
                else if ((this.EstadoOrigem == "SP") && (this.EstadoDestino == "RO"))
                {
                    notaFiscalItem.Cfop = "6.006";
                }
                else if ((this.EstadoOrigem == "SP") && (this.EstadoDestino == "SE"))
                {
                    notaFiscalItem.Cfop = "6.007";
                }
                else if ((this.EstadoOrigem == "SP") && (this.EstadoDestino == "TO"))
                {
                    notaFiscalItem.Cfop = "6.008";
                }
                else if ((this.EstadoOrigem == "SP") && (this.EstadoDestino == "SE"))
                {
                    notaFiscalItem.Cfop = "6.009";
                }
                else if ((this.EstadoOrigem == "SP") && (this.EstadoDestino == "PA"))
                {
                    notaFiscalItem.Cfop = "6.010";
                }
                else if ((this.EstadoOrigem == "MG") && (this.EstadoDestino == "RJ"))
                {
                    notaFiscalItem.Cfop = "6.000";
                }
                else if ((this.EstadoOrigem == "MG") && (this.EstadoDestino == "PE"))
                {
                    notaFiscalItem.Cfop = "6.001";
                }
                else if ((this.EstadoOrigem == "MG") && (this.EstadoDestino == "MG"))
                {
                    notaFiscalItem.Cfop = "6.002";
                }
                else if ((this.EstadoOrigem == "MG") && (this.EstadoDestino == "PB"))
                {
                    notaFiscalItem.Cfop = "6.003";
                }
                else if ((this.EstadoOrigem == "MG") && (this.EstadoDestino == "PR"))
                {
                    notaFiscalItem.Cfop = "6.004";
                }
                else if ((this.EstadoOrigem == "MG") && (this.EstadoDestino == "PI"))
                {
                    notaFiscalItem.Cfop = "6.005";
                }
                else if ((this.EstadoOrigem == "MG") && (this.EstadoDestino == "RO"))
                {
                    notaFiscalItem.Cfop = "6.006";
                }
                else if ((this.EstadoOrigem == "MG") && (this.EstadoDestino == "SE"))
                {
                    notaFiscalItem.Cfop = "6.007";
                }
                else if ((this.EstadoOrigem == "MG") && (this.EstadoDestino == "TO"))
                {
                    notaFiscalItem.Cfop = "6.008";
                }
                else if ((this.EstadoOrigem == "MG") && (this.EstadoDestino == "SE"))
                {
                    notaFiscalItem.Cfop = "6.009";
                }
                else if ((this.EstadoOrigem == "MG") && (this.EstadoDestino == "PA"))
                {
                    notaFiscalItem.Cfop = "6.010";
                }
                if (this.EstadoDestino == this.EstadoOrigem)
                {
                    notaFiscalItem.TipoIcms = "60";
                    notaFiscalItem.AliquotaIcms = 0.18;
                }
                else
                {
                    notaFiscalItem.TipoIcms = "10";
                    notaFiscalItem.AliquotaIcms = 0.17;
                }
                if (notaFiscalItem.Cfop == "6.009")
                {
                    notaFiscalItem.BaseIcms = itemPedido.ValorItemPedido*0.90; //redução de base
                }
                else
                {
                    notaFiscalItem.BaseIcms = itemPedido.ValorItemPedido;
                }
                notaFiscalItem.ValorIcms = notaFiscalItem.BaseIcms*notaFiscalItem.AliquotaIcms;

                if (itemPedido.Brinde)
                {
                    notaFiscalItem.TipoIcms = "60";
                    notaFiscalItem.AliquotaIcms = 0.18;
                    notaFiscalItem.ValorIcms = notaFiscalItem.BaseIcms * notaFiscalItem.AliquotaIcms;
                }
                notaFiscalItem.NomeProduto = itemPedido.NomeProduto;
                notaFiscalItem.CodigoProduto = itemPedido.CodigoProduto;

                //Exercicio 3

                notaFiscalItem.BaseCalculoIPI = itemPedido.ValorItemPedido;
                if (itemPedido.Brinde == true)
                {
                    notaFiscalItem.AliquotaIPI = 0;
                }
                else
                {
                    notaFiscalItem.AliquotaIPI = 0.10;
                }
                notaFiscalItem.ValorIPI = notaFiscalItem.BaseCalculoIPI * notaFiscalItem.AliquotaIPI;

                string dirXML = ConfigurationManager.AppSettings["diretorioXML"] + "\\NF" + this.NumeroNotaFiscal + ".xml";
                StringWriter writer = new StringWriter();
                XmlSerializer serializer = new XmlSerializer(typeof(NotaFiscalItem));

                using (FileStream st = File.Open(dirXML, FileMode.Create, FileAccess.Write))
                {
                    serializer.Serialize(writer, notaFiscalItem);
                }
                XmlDocument xdoc = new XmlDocument();
                xdoc.LoadXml(writer.ToString());
                xdoc.Save(dirXML);
                writer.Close();


                try
                {
                    conn.Open();
                    SqlCommand cmd2 = new SqlCommand("P_NOTA_FISCAL_ITEM", conn);
                    cmd2.CommandType = CommandType.StoredProcedure;
                    cmd2.Parameters.AddWithValue("@pId", 0);
                    cmd2.Parameters.AddWithValue("@pIdNotaFiscal", notaFiscalItem.IdNotaFiscal);
                    cmd2.Parameters.AddWithValue("@pCfop", notaFiscalItem.Cfop);
                    cmd2.Parameters.AddWithValue("@pTipoIcms", notaFiscalItem.TipoIcms);
                    cmd2.Parameters.AddWithValue("@pBaseIcms", notaFiscalItem.BaseIcms);
                    cmd2.Parameters.AddWithValue("@pAliquotaIcms", notaFiscalItem.AliquotaIcms);
                    cmd2.Parameters.AddWithValue("@pValorIcms", notaFiscalItem.ValorIcms);
                    cmd2.Parameters.AddWithValue("@pNomeProduto", notaFiscalItem.NomeProduto);
                    cmd2.Parameters.AddWithValue("@pCodigoProduto", notaFiscalItem.CodigoProduto);
                    cmd2.Parameters.AddWithValue("@pBaseCalculoIPI", notaFiscalItem.BaseCalculoIPI);
                    cmd2.Parameters.AddWithValue("@pAliquotaIPI", notaFiscalItem.AliquotaIPI);
                    cmd2.Parameters.AddWithValue("@pValorIPI", notaFiscalItem.ValorIPI);
                    cmd2.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                finally
                {
                    conn.Close();
                }
            }
            try
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("P_NOTA_FISCAL", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@pId", this.Id);
                cmd.Parameters.AddWithValue("@pNumeroNotaFiscal", this.NumeroNotaFiscal);
                cmd.Parameters.AddWithValue("@pSerie", this.Serie);
                cmd.Parameters.AddWithValue("@pNomeCliente", this.NomeCliente);
                cmd.Parameters.AddWithValue("@pEstadoOrigem", this.EstadoOrigem);
                cmd.Parameters.AddWithValue("@pEstadoDestino", this.EstadoDestino);
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                conn.Close();
            }

        }
    }
}
