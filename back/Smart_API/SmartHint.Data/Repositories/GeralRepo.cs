using SmartHint.Data.Context.Data;
using SmartHint.Domain.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartHint.Data.Repositories
{
    public class GeralRepo : IGeralRepo
    {
        private readonly DataContext _context;

        public GeralRepo(DataContext context)
        {
            _context = context;
        }

        public void Adicionar<T>(T entity) where T : class
        {
            _context.Add(entity);
        }

        public void Atualizar<T>(T entity) where T : class
        {
            _context.Update(entity);
        }

        public void Deletar<T>(T entity) where T : class
        {
            _context.Remove(entity);
        }

        public void DeletarVarios<T>(T[] entity) where T : class
        {
            _context.RemoveRange(entity);
        }

        public bool SalvarMudancas()
        {
            return _context.SaveChanges() > 0;
        }

        public async Task<bool> SalvarMudancasAsync()
        {
            return (await _context.SaveChangesAsync() > 0);
        }
    }
}
