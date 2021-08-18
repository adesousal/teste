using System;
using System.Web.Mvc;

namespace ProvaCandidato.Data.Entidade
{
    public class GenericaController : Controller
    {
        public int CodigoCidade { get; set; }
        public string NomeCidade { get; set; }
        public int CodigoCliente { get; set; }
        public string NomeCliente { get; set; }
        public DateTime? DataNascimento { get; set; }
        public int CidadeId { get; set; }
        public bool Ativo { get; set; }
        public virtual Cidade Cidade { get; set; }
        public int CodigoObservacao { get; set; }
        public string Observacao { get; set; }
        public virtual Cliente Cliente { get; set; }

        public GenericaController()
        {

        }

        public GenericaController(Cidade cidade)
        {
            CodigoCidade = cidade.Codigo;
            NomeCidade = cidade.Nome;
        }

        public GenericaController(Cliente cliente)
        {
            CodigoCliente = cliente.Codigo;
            DataNascimento = cliente.DataNascimento;
            CidadeId = cliente.CidadeId;
            Ativo = cliente.Ativo;
            Cidade = cliente.Cidade;
        }

        public GenericaController(ClienteObservacao observacao)
        {
            CodigoObservacao = observacao.Codigo;
            Observacao = observacao.Observacao;
            Cliente = observacao.Cliente;         
        }
    }
}
