using SmartHint.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartHint.Domain.Interfaces.Services
{
    public interface IEnderecoService
    {
        Endereco AdicionarEndereco(Endereco model);

        Endereco AtualizarEndereco(Endereco model);

        bool DeletarEndereco(int enderecoId);

        Endereco[] MostrarTodosEnderecos();

        Endereco MostrarEnderecoPorId(int enderecoeId);
    }
}
