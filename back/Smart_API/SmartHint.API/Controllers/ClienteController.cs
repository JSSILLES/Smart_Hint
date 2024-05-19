using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SmartHint.Domain.Entities;
using SmartHint.Domain.Interfaces.Services;

namespace SmartHint.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClienteController : ControllerBase
    {
        private readonly IClienteService _clienteService;
        private readonly IEnderecoService _enderecoService;

        public ClienteController(IClienteService clienteService, IEnderecoService enderecoService)
        {
            _clienteService = clienteService;
            _enderecoService = enderecoService;
        }

        [HttpGet]
        public IActionResult Get()
        {
            try
            {
                var clientes = _clienteService.MostrarTodosClientes();
                if (clientes == null)
                {
                    return NoContent();
                }
                return Ok(clientes);
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError,
                    $"Erro ao tentar recuperar clientes. Erro: {ex.Message}");
            }

        }

        [HttpGet("{idPessoa}")]
        public IActionResult GetById(int idPessoa)
        {         
            try
            {
                var cliente = _clienteService.MostrarClientePorId(idPessoa);
                if (cliente == null) return NoContent();

                return Ok(cliente);
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError,
                    $"Erro ao tentar recuperar um cliente com id: {idPessoa}. Erro: {ex.Message}");
            }

        }

        [HttpGet("pesquisa/{nomeCliente}")]
        public IActionResult GetByName(string nomeCliente)
        {
            try
            {
                var clientes = _clienteService.MostrarClientePor(cli => cli.Nome.Contains(nomeCliente));
                if (clientes == null || !clientes.Any())
                    return NoContent();

                return Ok(clientes);
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError,
                    $"Erro ao tentar recuperar cliente(s) com nome: {nomeCliente}. Erro: {ex.Message}");
            }
        }

        [HttpPost]
        public IActionResult Post(Cliente model)
        {
            model.Ativo = true;
            model.DataCadastro = DateTime.Now;

            try
            {

                if (string.IsNullOrEmpty(model.Endereco.Complemento))
                {
                    model.Endereco.Complemento = string.Empty;
                }

                var endereco = _enderecoService.AdicionarEndereco(model.Endereco);

                if (endereco == null)
                {
                    // Se não foi possível adicionar o endereço, retorne um erro apropriado
                    return StatusCode(StatusCodes.Status500InternalServerError,
                        "Erro ao tentar adicionar o endereço do cliente.");
                }

                model.DataCadastro = DateTime.Now;
                model.IdEndereco = endereco.IdEndereco;
                model.Endereco.CEP = endereco.CEP;
                model.Endereco.Rua = endereco.Rua;
                model.Endereco.Numero = endereco.Numero;
                model.Endereco.Complemento = endereco.Complemento;
                model.Endereco.Bairro = endereco.Bairro;
                model.Endereco.Cidade = endereco.Cidade;
                model.Endereco.Estado = endereco.Estado;
                model.Endereco.UF = endereco.UF;

                var cliente = _clienteService.AdicionarCliente(model);
                if (cliente == null) return NoContent();

                return Ok(cliente);
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError,
                    $"Erro ao tentar adicionar um cliente com id: ${model.IdPessoa}. Erro: {ex.Message}");
            }

        }

        [HttpPut("{idCliente}")]
        public IActionResult Put(int idCliente, Cliente model)
        {
            model.Ativo = true;

            try
            {
                if (model.IdPessoa != idCliente)
                {
                    return this.StatusCode(StatusCodes.Status409Conflict,
                        "Erro ao tentar atualizar o cliente errado");
                }

                // Verifica se o cliente existe no banco de dados
                var existingCliente = _clienteService.MostrarClientePorId(idCliente);
                if (existingCliente == null)
                {
                    return NoContent();
                }

                // Verifica se um endereço foi enviado no modelo
                if (model.Endereco != null)
                {
                    // Verifica se o endereço já existe no banco de dados
                    var existingEndereco = _enderecoService.MostrarEnderecoPorId(model.Endereco.IdEndereco);
                    if (existingEndereco == null)
                    {
                        // Se o endereço não existe, adiciona-o ao banco de dados
                        existingEndereco = _enderecoService.AdicionarEndereco(model.Endereco);
                    }

                    // Associa o endereço ao cliente
                    existingCliente.IdEndereco = existingEndereco.IdEndereco;
                    existingCliente.Endereco.CEP = model.Endereco.CEP;
                    existingCliente.Endereco.Rua = model.Endereco.Rua;
                    existingCliente.Endereco.Numero = model.Endereco.Numero;
                    existingCliente.Endereco.Bairro = model.Endereco.Bairro;
                    existingCliente.Endereco.Cidade = model.Endereco.Cidade;
                    existingCliente.Endereco.Complemento = model.Endereco.Complemento;
                    existingCliente.Endereco.UF = model.Endereco.UF;
                }

                // Atualiza as propriedades do cliente existente
                existingCliente.Nome = model.Nome;
                existingCliente.Documento = model.Documento;
                existingCliente.EstadoCivil = model.EstadoCivil;
                existingCliente.Sexo = model.Sexo;
                existingCliente.NumeroTelefone = model.NumeroTelefone;
                existingCliente.Email = model.Email;


                // Salva as mudanças
                _clienteService.AtualizarCliente(existingCliente);

                return Ok(existingCliente);
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError,
                    $"Erro ao tentar atualizar um cliente com id: {model.IdPessoa}. Erro: {ex.Message}");
            }
        }

        [HttpDelete("{idCliente}")]
        public IActionResult Delete(int idCliente)
        {
            try
            {
                var cliente = _clienteService.MostrarClientePorId(idCliente);

                if (cliente == null)
                    return NotFound("Cliente não encontrado");

                if (!cliente.Ativo)
                    return BadRequest("Cliente já está inativo");

                cliente.Ativo = false;

                _clienteService.AtualizarCliente(cliente);

                // Após a exclusão, obter a lista atualizada de clientes
                var clientesRestantes = _clienteService.MostrarTodosClientes();

                return Ok(new { message = "Cliente marcado como removido", clientes = clientesRestantes });
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError,
                    $"Erro ao tentar marcar o cliente como removido. Erro: {ex.Message}");
            }

        }

    }
}
