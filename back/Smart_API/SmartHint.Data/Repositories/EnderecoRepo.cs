using SmartHint.Data.Context.Data;
using SmartHint.Domain.Entities;
using SmartHint.Domain.Interfaces.Repositories;
using System.Data.Entity;

namespace SmartHint.Data.Repositories
{
    public class EnderecoRepo : GeralRepo, IEnderecoRepo
    {
        private readonly DataContext _context;
        public EnderecoRepo(DataContext context) : base(context)
        {
            _context = context;
        }               

        public Endereco MostrarPorEndereco(string rua)
        {
            return _context.Endereco
                          .AsNoTracking()
                          .OrderBy(ender => ender.IdEndereco)
                          .FirstOrDefault(ender => ender.Rua == rua) ?? throw new InvalidOperationException("Endereço não encontrado");
        }

        public Endereco MostrarPorId(int id)
        {
            return _context.Endereco
                          .AsNoTracking()  // Evita rastreamento de entidade para leitura
                          .OrderBy(cli => cli.IdEndereco)
                          .FirstOrDefault(ender => ender.IdEndereco == id) ?? throw new InvalidOperationException("Endereço não encontrado");
        }

        public Endereco[] MostrarTodos()
        {
            return _context.Endereco
                    .AsNoTracking()  // Evita rastreamento de entidade para leitura
                    .OrderBy(ender => ender.Rua)
                    .ToArray();
        }

    }
}
