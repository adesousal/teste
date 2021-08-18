using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProvaCandidato.Data.Entidade
{
    [Table("Cliente")]
    public class Cliente
    {
        [Key]
        [Column("codigo")]
        public int Codigo { get; set; }

        [Required]
        [Column("nome")]
        [MinLength(3)]
        [StringLength(50)]
        public string Nome { get; set; }

        [Column("data_nascimento")]
        [Display(Name = "Data de Nascimento")]
        [CustomDateRange]
        [DataType(DataType.Date), DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:d}")]
        public DateTime? DataNascimento { get; set; }

        [Column("codigo_cidade")]
        [Display(Name = "Cidade")]
        public int CidadeId { get; set; }

        public bool Ativo { get; set; }

        [ForeignKey("CidadeId")]
        public virtual Cidade Cidade { get; set; }

        [InverseProperty("Cliente")]
        public ICollection<ClienteObservacao> Observacoes { get; set; }
    }
}