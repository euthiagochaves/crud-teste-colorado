using crude_teste.Domain.Interfaces;
using crude_teste.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace crude_teste.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClientesController : ControllerBase
    {
        private readonly IClienteRepository _clienteRepository;
        private readonly ILogger<ClientesController> _logger;

        /// <summary>
        /// Esta controller é responsável por expor os endpoints da API REST relacionados a entidade Cliente
        /// ela permite operações de CRUD (criar, listar os dados, atualizar e excluir) e encapsula regras de acesso aos dados
        /// faz uso de injeção de dependência para acessar o repositório e registrar logs de erro.
        /// </summary>
        public ClientesController(IClienteRepository clienteRepository, ILogger<ClientesController> logger)
        {
            _clienteRepository = clienteRepository;
            _logger = logger;
        }

        // GET: api/Clientes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Cliente>>> GetClientes()
        {
            try
            {
                var clientes = await _clienteRepository.GetAllAsync();
                return Ok(clientes);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao buscar clientes.");
                return StatusCode(500, "Ocorreu um erro ao buscar os clientes.");
            }
        }

        // GET: api/Clientes/1
        [HttpGet("{id}")]
        public async Task<ActionResult<Cliente>> GetCliente(int id)
        {
            try
            {
                var cliente = await _clienteRepository.GetByIdAsync(id);

                if (cliente == null)
                    return NotFound("Cliente não encontrado.");

                return Ok(cliente);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Erro ao buscar cliente com ID {id}.");
                return StatusCode(500, "Ocorreu um erro ao buscar o cliente.");
            }
        }

        // PUT: api/Clientes/1
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCliente(int id, Cliente cliente)
        {
            if (id != cliente.CodigoCliente)
                return BadRequest("ID do cliente não corresponde.");

            try
            {
                if (!await _clienteRepository.ExistsAsync(id))
                    return NotFound("Cliente não encontrado.");

                // Atualização de auditoria
                cliente.DataInsercao = DateTime.Now;
                cliente.UsuarioInsercao = "SYSTEM_UPDATE";

                await _clienteRepository.UpdateAsync(cliente);
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Erro ao atualizar cliente com ID {id}.");
                return StatusCode(500, "Ocorreu um erro ao atualizar o cliente.");
            }
        }

        // POST: api/Clientes
        [HttpPost]
        public async Task<ActionResult<Cliente>> PostCliente([FromBody] Cliente cliente)
        {
            try
            {
                cliente.DataInsercao = DateTime.Now;
                cliente.UsuarioInsercao = "SYSTEM";

                if (cliente.Telefones != null)
                {
                    foreach (var telefone in cliente.Telefones)
                    {
                        telefone.DataInsercao = DateTime.Now;
                        telefone.UsuarioInsercao = "SYSTEM";
                    }
                }

                await _clienteRepository.CreateAsync(cliente);

                return CreatedAtAction(nameof(GetCliente), new { id = cliente.CodigoCliente }, cliente);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao criar cliente.");
                return StatusCode(500, "Ocorreu um erro ao criar o cliente.");
            }
        }

        // DELETE: api/Clientes/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCliente(int id)
        {
            try
            {
                if (!await _clienteRepository.ExistsAsync(id))
                    return NotFound("Cliente não encontrado.");

                await _clienteRepository.DeleteAsync(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Erro ao excluir cliente com ID {id}.");
                return StatusCode(500, "Ocorreu um erro ao excluir o cliente.");
            }
        }
    }
}
