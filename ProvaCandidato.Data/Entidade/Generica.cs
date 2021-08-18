using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace ProvaCandidato.Data.Entidade
{
    public class Generica : Controller
    {
        public int CodigoCidade { get; set; }
        public string NomeCidade { get; set; }
        public int CodigoCliente { get; set; }
        public string NomeCliente { get; set; }
        public DateTime? DataNascimento { get; set; }
        public int CidadeId { get; set; }
        public bool Ativo { get; set; }
        public virtual Cidade Cidade { get; set; }

        public Generica()
        {

        }

        public Generica(Cidade cidade)
        {
            CodigoCidade = cidade.Codigo;
            NomeCidade = cidade.Nome;
        }

        public Generica(Cliente cliente)
        {
            CodigoCliente = cliente.Codigo;
            DataNascimento = cliente.DataNascimento;
            CidadeId = cliente.CidadeId;
            Ativo = cliente.Ativo;
            Cidade = cliente.Cidade;
        }
    }
}
