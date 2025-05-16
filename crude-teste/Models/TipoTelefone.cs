using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace crude_teste.Models
{
    public class TipoTelefone
    {
        [Key]
        [Column("CodigoTipoTelefone")]
        public int CodigoTipoTelefone { get; set; }

        [Required(ErrorMessage = "A descrição é obrigatória")]
        [StringLength(50, ErrorMessage = "A descrição deve ter no máximo 50 caracteres")]
        public string DescricaoTipoTelefone { get; set; }

        public DateTime DataInsercao { get; set; }

        [StringLength(50)]
        public string UsuarioInsercao { get; set; }

        // Relacionamento com Telefones
        public virtual ICollection<Telefone> Telefones { get; set; }
    }
}
