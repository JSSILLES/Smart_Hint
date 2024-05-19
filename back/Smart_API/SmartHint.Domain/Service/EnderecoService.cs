using Microsoft.Extensions.Logging;
using SmartHint.Domain.Entities;
using SmartHint.Domain.Interfaces.Repositories;
using SmartHint.Domain.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartHint.Domain.Service
{
    public class EnderecoService : IEnderecoService
    {
        public readonly IEnderecoRepo _enderecoRepo;
        private readonly ILogger<EnderecoService> _logger;

        public EnderecoService(IEnderecoRepo enderecoRepo, ILogger<EnderecoService> logger)
        {
            _enderecoRepo = enderecoRepo;
            _logger = logger;
        }

        public Endereco AdicionarEndereco(Endereco model)
        {
            try
            {
                _enderecoRepo.Adicionar(model);

                if (_enderecoRepo.SalvarMudancas())
                {
                    return model;
                }
                else
                {
                    throw new Exception("Falha ao salvar mudanças no repositório");
                }
            }
            catch (InvalidOperationException ex)
            {
                _logger.LogError(ex, "An InvalidOperationException occurred in the method.");
                throw;
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao adicionar endereço", ex);
            }
        }

        public Endereco AtualizarEndereco(Endereco model)
        {
            if (_enderecoRepo.MostrarPorId(model.IdEndereco) != null)
            {
                _enderecoRepo.Atualizar(model);

                if (_enderecoRepo.SalvarMudancas())
                    return model;
                else
                    throw new Exception("Falha ao salvar mudanças no repositório de endereços");
            }
            else
            {
                throw new Exception("Não foi possível encontrar o endereço com o ID especificado");
            }
        }

        public bool DeletarEndereco(int enderecoId)
        {
            var endereco = _enderecoRepo.MostrarPorId(enderecoId);
            if (endereco == null) throw new Exception("O endereço que tentou excluir, não existe");

            _enderecoRepo.Deletar(endereco);

            return _enderecoRepo.SalvarMudancas();
        }

        public Endereco MostrarEnderecoPorId(int enderecoeId)
        {
            try
            {
                var endereco = _enderecoRepo.MostrarPorId(enderecoeId);
                if (endereco == null)
                    return null;

                return endereco;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public Endereco[] MostrarTodosEnderecos()
        {
            try
            {
                var enderecos = _enderecoRepo.MostrarTodos();
                if (enderecos == null)
                    return null;

                return enderecos;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
