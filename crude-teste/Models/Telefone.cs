using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace crude_teste.Models
{
    public class Telefone
    {
        [Key]
        [Column("CodigoCliente", Order = 0)]
        public int CodigoCliente { get; set; }

        [Key]
        [Column("NumeroTelefone", Order = 1)]
        [Required(ErrorMessage = "O número é obrigatório")]
        [StringLength(20, ErrorMessage = "O número deve ter no máximo 20 caracteres")]
        public string NumeroTelefone { get; set; }

        [Required]
        public int CodigoTipoTelefone { get; set; }

        [StringLength(20)]
        public string Operadora { get; set; }

        public bool Ativo { get; set; }

        public DateTime? DataInsercao { get; set; }

        [StringLength(50)]
        public string? UsuarioInsercao { get; set; }
        
        // Relacionamento com Cliente
        [ForeignKey("CodigoCliente")]
        public virtual Cliente? Cliente { get; set; }
        
        // Relacionamento com TipoTelefone
        [ForeignKey("CodigoTipoTelefone")]
        public virtual TipoTelefone? TipoTelefone { get; set; }
    }
}
