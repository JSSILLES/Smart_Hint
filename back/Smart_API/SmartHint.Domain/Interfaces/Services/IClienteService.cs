using SmartHint.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartHint.Domain.Interfaces.Services
{
    public interface IClienteService
    {
        Cliente AdicionarCliente(Cliente cliente);


        Cliente? MostrarClientePorId(int clienteId);

        //Cliente? MostrarClientePorNome(string moneCliente);
        //IEnumerable<Cliente>? MostrarClientePorNome(Func<Cliente, bool> predicate);
        IEnumerable<Cliente>? MostrarClientePor(Func<Cliente, bool> predicate);

        Cliente AtualizarCliente(Cliente cliente);

        bool DeletarCliente(int clienteId);

        IEnumerable<Cliente> MostrarTodosClientes();
        //Cliente[]? MostrarTodosClientes();

    }
}
