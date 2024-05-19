using SmartHint.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartHint.Domain.Interfaces.Repositories
{
    public interface IEnderecoRepo : IGeralRepo
    {
        Endereco MostrarPorId(int id);
        Endereco[] MostrarTodos();
        Endereco MostrarPorEndereco(string nome);
    }
}
