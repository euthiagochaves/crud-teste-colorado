using crude_teste.Domain.Interfaces;
using crude_teste.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace crude_teste.Controllers
{
    public class ClientesController : Controller
    {
        private readonly IClienteRepository _clienteRepository;
        private readonly ITipoTelefoneRepository _tipoTelefoneRepository;
        private readonly ILogger<ClientesController> _logger;

        public ClientesController(
            IClienteRepository clienteRepository,
            ITipoTelefoneRepository tipoTelefoneRepository,
            ILogger<ClientesController> logger)
        {
            _clienteRepository = clienteRepository;
            _tipoTelefoneRepository = tipoTelefoneRepository;
            _logger = logger;
        }

        /// <summary>
        /// Esta controller é responsável por exibir as páginas da interface MVC relacionadas a clientes
        /// ela fornece suporte para ações CRUD de registros no frontend
        /// e utiliza repositórios para buscar e preparar os dados que serão renderizados nas views.
        /// </summary>

        // GET: Clientes
        public IActionResult Index()
        {
            return View();
        }

        // GET: Clientes/Details/5
        public async Task<IActionResult> Details(int id)
        {
            try
            {
                var cliente = await _clienteRepository.GetByIdAsync(id);
                if (cliente == null)
                    return NotFound();

                return View(cliente);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Erro ao carregar detalhes do cliente (ID: {id})");
                return RedirectToAction("Error", "Home");
            }
        }

        // GET: Clientes/Create
        public async Task<IActionResult> Create()
        {
            try
            {
                await CarregarTiposTelefone();
                return View();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao carregar formulário de criação de cliente.");
                return RedirectToAction("Error", "Home");
            }
        }

        // GET: Clientes/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            try
            {
                var cliente = await _clienteRepository.GetByIdAsync(id);

                if (cliente == null)
                    return NotFound();

                // FILTRA telefones com número válido
                cliente.Telefones = cliente.Telefones?
                    .Where(t => !string.IsNullOrWhiteSpace(t.NumeroTelefone))
                    .ToList();

                await CarregarTiposTelefone();
                return View(cliente);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Erro ao carregar formulário de edição de cliente (ID: {id})");
                return RedirectToAction("Error", "Home");
            }
        }

        // GET: Clientes/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var cliente = await _clienteRepository.GetByIdAsync(id);
                if (cliente == null)
                    return NotFound();

                return View(cliente);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Erro ao carregar confirmação de exclusão do cliente (ID: {id})");
                return RedirectToAction("Error", "Home");
            }
        }

        /// <summary>
        /// Carrega os tipos de telefone disponíveis para preencher dropdowns nas views.
        /// </summary>
        private async Task CarregarTiposTelefone()
        {
            var tiposTelefone = await _tipoTelefoneRepository.GetAllAsync();
            ViewBag.TiposTelefone = new SelectList(tiposTelefone, "CodigoTipoTelefone", "DescricaoTipoTelefone");
        }
    }
}
