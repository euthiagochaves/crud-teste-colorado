using crude_teste.Domain.Interfaces;
using crude_teste.Models;
using crude_teste.Data;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace crude_teste.Infrastructure.Repositories
{
    public class TipoTelefoneRepository : ITipoTelefoneRepository
    {
        private readonly ApplicationDbContext _context;

        public TipoTelefoneRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<TipoTelefone>> GetAllAsync()
        {
            return await _context.TiposTelefone.ToListAsync();
        }

        public async Task<TipoTelefone> GetByIdAsync(int id)
        {
            return await _context.TiposTelefone.FindAsync(id);
        }
    }
}
