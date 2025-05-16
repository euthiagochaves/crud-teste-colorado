using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace crude_teste.Models
{
    public class Cliente
    {
        [Key]
        [Column("CodigoCliente")]
        public int CodigoCliente { get; set; }

        [Required(ErrorMessage = "A Razão Social é obrigatória")]
        [StringLength(100, ErrorMessage = "A Razão Social deve ter no máximo 100 caracteres")]
        public string RazaoSocial { get; set; }

        [Required(ErrorMessage = "O Nome Fantasia é obrigatório")]
        [StringLength(100, ErrorMessage = "O Nome Fantasia deve ter no máximo 100 caracteres")]
        public string NomeFantasia { get; set; }

        [Required(ErrorMessage = "O Tipo de Pessoa é obrigatório")]
        [StringLength(1, ErrorMessage = "O Tipo de Pessoa deve ser F ou J")]
        public string TipoPessoa { get; set; }

        [Required(ErrorMessage = "O Documento é obrigatório")]
        [StringLength(20, ErrorMessage = "O Documento deve ter no máximo 20 caracteres")]
        public string Documento { get; set; }

        [Required(ErrorMessage = "O Endereço é obrigatório")]
        [StringLength(100, ErrorMessage = "O Endereço deve ter no máximo 100 caracteres")]
        public string Endereco { get; set; }

        [StringLength(100, ErrorMessage = "O Complemento deve ter no máximo 100 caracteres")]
        public string Complemento { get; set; }

        [Required(ErrorMessage = "O Bairro é obrigatório")]
        [StringLength(50, ErrorMessage = "O Bairro deve ter no máximo 50 caracteres")]
        public string Bairro { get; set; }

        [Required(ErrorMessage = "A Cidade é obrigatória")]
        [StringLength(50, ErrorMessage = "A Cidade deve ter no máximo 50 caracteres")]
        public string Cidade { get; set; }

        [Required(ErrorMessage = "O CEP é obrigatório")]
        [StringLength(10, ErrorMessage = "O CEP deve ter no máximo 10 caracteres")]
        public string CEP { get; set; }

        [Required(ErrorMessage = "A UF é obrigatória")]
        [StringLength(2, ErrorMessage = "A UF deve ter 2 caracteres")]
        public string UF { get; set; }

        public DateTime? DataInsercao { get; set; }

        [StringLength(50)]
        public string? UsuarioInsercao { get; set; }

        // Relacionamento com Telefones
        public virtual ICollection<Telefone> Telefones { get; set; }
    }
}
