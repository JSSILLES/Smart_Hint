using Microsoft.Extensions.Logging;
using SmartHint.Domain.Entities;
using SmartHint.Domain.Interfaces.Repositories;
using SmartHint.Domain.Interfaces.Services;
using System.Reflection;

namespace SmartHint.Domain.Service
{
    public class ClienteService : IClienteService
    {
        public readonly IClienteRepo _clienteRepo;
        private readonly ILogger<ClienteService> _logger;

        public ClienteService(IClienteRepo clienteRepo, ILogger<ClienteService> logger)
        {
            _clienteRepo = clienteRepo;
            _logger = logger;

        }

        public Cliente AdicionarCliente(Cliente model)
        {
            try
            {
                // Tente adicionar o cliente
                _clienteRepo.Adicionar(model);

                if (_clienteRepo.SalvarMudancas())
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
                // O cliente não foi encontrado
                // Trate de acordo com sua lógica de negócios
                // Por exemplo, retornando null ou lançando uma nova exceção com uma mensagem mais específica
                _logger.LogError(ex, "An InvalidOperationException occurred in the method.");

                // Optionally, rethrow the exception for higher level handling
                throw;

            }
            catch (Exception ex)
            {
                // Lidar com outras exceções se necessário
                throw new Exception("Erro ao adicionar cliente", ex);
            }
        }

        public Cliente AtualizarCliente(Cliente model)
        {
            if (_clienteRepo.MostrarPorId(model.IdPessoa) != null)
            {
                _clienteRepo.Atualizar(model);
                if (_clienteRepo.SalvarMudancas())
                    return model;
                else
                    throw new Exception("Falha ao salvar mudanças no repositório de clientes");
            }
            else
            {
                throw new Exception("Não foi possível encontrar o cliente com o ID especificado");
            }
        }

        public bool DeletarCliente(int clienteId)
        {
            var cliente = _clienteRepo.MostrarPorId(clienteId);
            if (cliente == null) throw new Exception("O cliente que tentou atualizar, não existe");

            _clienteRepo.Deletar(cliente);
            return _clienteRepo.SalvarMudancas();
        }

        public Cliente? MostrarClientePorId(int clienteId)
        {
            try
            {
                var cliente = _clienteRepo.MostrarPorId(clienteId);
                if (cliente == null)
                    return null;

                return cliente;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public IEnumerable<Cliente>? MostrarClientePor(Func<Cliente, bool> predicate)
        {
            try
            {
                var clientes = _clienteRepo.MostrarPorNome(predicate);
                if (clientes == null)
                    return null;

                return clientes;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public IEnumerable<Cliente> MostrarTodosClientes()
        {
            try
            {
                var clientes = _clienteRepo.MostrarTodos();
                if (clientes == null)
                    return null;

                return clientes;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public IEnumerable<Cliente> MostrarTodosClientesAtivos()
        {
            try
            {
                var clientes = _clienteRepo.MostrarTodosAtivos();
                if (clientes == null)
                    return null;

                return clientes;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public IEnumerable<Cliente> MostrarTodosClientesInativos()
        {
            try
            {
                var clientes = _clienteRepo.MostrarTodosInativos();
                if (clientes == null)
                    return null;

                return clientes;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
