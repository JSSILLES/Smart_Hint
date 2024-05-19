using SmartHint.Domain.Enums;

namespace SmartHint.Domain.Entities
{
    public class Cliente : Pessoa
    {
        public Cliente() { }

        public EstadoCivilEnum EstadoCivil { get; set; }
        public GeneroEnum Sexo { get; set; }
        //public DateTime DataNascimento { get; set; }
        public bool Ativo { get; set; }

    }
}
