using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartHint.Domain.Interfaces.Repositories
{
    public interface IGeralRepo
    {
        void Adicionar<T>(T entity) where T : class;
        void Atualizar<T>(T entity) where T : class;
        void Deletar<T>(T entity) where T : class;
        void DeletarVarios<T>(T[] entity) where T : class;

        bool SalvarMudancas();
    }
}
