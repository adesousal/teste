using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProvaCandidato.Data.Entidade
{
    [Table("Observacoes")]
    public class ClienteObservacao
    {
        [Key]
        [Column("codigo")]
        public int Codigo { get; set; }

        [Column("observacao")]
        [Display(Name = "Observações")]
        public string Observacao { get; set; }

        public Cliente Cliente { get; set; }
    }
}
