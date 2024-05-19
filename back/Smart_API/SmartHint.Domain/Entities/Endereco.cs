using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartHint.Domain.Entities
{
    public class Endereco
    {
        [Key]
        public int IdEndereco { get; set; }
        public string CEP { get; set; }
        public string Rua { get; set; }
        public int Numero { get; set; }
        public string Bairro { get; set; }
        public string Cidade { get; set; }
        public string Complemento { get; set; }
        public string UF { get; set; }
        public string Estado { get; set; }


        public Endereco() { }
    }
}
