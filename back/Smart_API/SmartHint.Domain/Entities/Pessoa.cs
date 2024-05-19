using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartHint.Domain.Entities
{
    public class Pessoa
    {
        public Pessoa() {}

        [Key]
        public int IdPessoa { get; set; }
        public string Nome { get; set; }
        public string Documento { get; set; }
        public string NumeroTelefone { get; set; }


        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        public DateTime DataCadastro { get; set; }

        [ForeignKey("Endereco")]
        public int IdEndereco { get; set; }
        public virtual Endereco Endereco { get; set; }
    }
}
