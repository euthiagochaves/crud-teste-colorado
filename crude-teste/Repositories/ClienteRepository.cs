using crude_teste.Domain.Interfaces;
using crude_teste.Models;
using crude_teste.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace crude_teste.Infrastructure.Repositories
{
    public class ClienteRepository : IClienteRepository
    {
        private readonly ApplicationDbContext _context;

        public ClienteRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Cliente>> GetAllAsync()
        {
            return await _context.Clientes
                .Include(c => c.Telefones)
                .ThenInclude(t => t.TipoTelefone)
                .ToListAsync();
        }

        public async Task<Cliente> GetByIdAsync(int id)
        {
            return await _context.Clientes
                .Include(c => c.Telefones)
                .ThenInclude(t => t.TipoTelefone)
                .FirstOrDefaultAsync(c => c.CodigoCliente == id);
        }

        public async Task<Cliente> CreateAsync(Cliente cliente)
        {
            // Salvar o cliente primeiro
            _context.Clientes.Add(cliente);
            await _context.SaveChangesAsync();

            // Agora que o cliente tem um CodigoCliente, atualizar os telefones
            if (cliente.Telefones != null)
            {
                foreach (var telefone in cliente.Telefones)
                {
                    telefone.CodigoCliente = cliente.CodigoCliente;
                }
                await _context.SaveChangesAsync();
            }

            return cliente;
        }

        public async Task UpdateAsync(Cliente cliente)
        {
            var clienteExistente = await _context.Clientes
                .Include(c => c.Telefones)
                .FirstOrDefaultAsync(c => c.CodigoCliente == cliente.CodigoCliente);

            if (clienteExistente == null)
                return;

            // Atualiza os dados principais
            _context.Entry(clienteExistente).CurrentValues.SetValues(cliente);

            // Telefones
            // Remove todos os existentes
            _context.Telefones.RemoveRange(clienteExistente.Telefones);

            // Adiciona os novos
            if (cliente.Telefones != null)
            {
                foreach (var telefone in cliente.Telefones)
                {
                    telefone.CodigoCliente = cliente.CodigoCliente;
                    telefone.DataInsercao = DateTime.Now;
                    telefone.UsuarioInsercao = "SYSTEM_UPDATE";
                }

                clienteExistente.Telefones = cliente.Telefones;
            }

            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var cliente = await _context.Clientes.FindAsync(id);
            if (cliente != null)
            {
                _context.Clientes.Remove(cliente);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<bool> ExistsAsync(int id)
        {
            return await _context.Clientes.AnyAsync(c => c.CodigoCliente == id);
        }
    }
}
