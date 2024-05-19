using SmartHint.Data.Context.Data;
using SmartHint.Domain.Entities;
using SmartHint.Domain.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartHint.Data.Repositories
{
    public class ClienteRepo : GeralRepo, IClienteRepo
    {
        private readonly DataContext _context;

        public ClienteRepo(DataContext context) : base(context)
        {
            _context = context;
        }

        public Cliente MostrarPorId(int id)
        {
            return _context.Cliente
                          .AsNoTracking()  // Evita rastreamento de entidade para leitura
                          .Include(c => c.Endereco)
                          .OrderBy(cli => cli.IdPessoa)
                          .FirstOrDefault(cli => cli.IdPessoa == id) ?? throw new InvalidOperationException("Cliente não encontrado");
        }

        //public IEnumerable<Cliente>? MostrarPorNome(string nome)
        //{
        //    return _context.Cliente
        //                  .AsNoTracking()  // Evita rastreamento de entidade para leitura
        //                  .Include(c => c.Endereco)
        //                  .Where(cli => cli.Nome.Contains(nome))
        //                  .OrderBy(cli => cli.Nome)
        //                  .ToList() ?? throw new InvalidOperationException("Cliente não encontrado");          
        //}

        public IEnumerable<Cliente>? MostrarPorNome(Func<Cliente, bool> predicate)
        {
            return _context.Cliente
                           .AsNoTracking()
                           .Include(c => c.Endereco)
                           .Where(predicate)
                           .OrderBy(cli => cli.Nome)
                           .ToList() ?? throw new InvalidOperationException("Cliente não encontrado");
        }

        public IEnumerable<Cliente> MostrarTodos()
        {

            var clientes = _context.Cliente
                    .AsNoTracking()
                    .Include(c => c.Endereco)
                    .ToList();

            return clientes;
        }

        public IEnumerable<Cliente> MostrarTodosAtivos()
        {

            var clientes = _context.Cliente
                    .AsNoTracking()
                    .Include(c => c.Endereco)
                    .Where(c => c.Ativo == true)
                    .ToList();

            return clientes;
        }

        public IEnumerable<Cliente> MostrarTodosInativos()
        {

            var clientes = _context.Cliente
                    .AsNoTracking()
                    .Include(c => c.Endereco)
                    .Where(c => c.Ativo == false)
                    .ToList();

            return clientes;
        }

       
    }
}
