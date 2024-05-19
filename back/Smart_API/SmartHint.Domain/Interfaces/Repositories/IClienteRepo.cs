using SmartHint.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartHint.Domain.Interfaces.Repositories
{
    public interface IClienteRepo : IGeralRepo
    {
        Cliente MostrarPorId(int id);
        //Cliente MostrarPorNome(string nome);

        //IEnumerable<Cliente>? MostrarPorNome(string nome);

        IEnumerable<Cliente>? MostrarPorNome(Func<Cliente, bool> predicate);

        IEnumerable<Cliente> MostrarTodos();
        IEnumerable<Cliente> MostrarTodosAtivos();
        IEnumerable<Cliente> MostrarTodosInativos();
    }
}
