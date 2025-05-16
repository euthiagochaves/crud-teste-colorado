using crude_teste.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace crude_teste.Domain.Interfaces
{
    public interface IClienteRepository
    {
        Task<IEnumerable<Cliente>> GetAllAsync();
        Task<Cliente> GetByIdAsync(int id);
        Task<Cliente> CreateAsync(Cliente cliente);
        Task UpdateAsync(Cliente cliente);
        Task DeleteAsync(int id);
        Task<bool> ExistsAsync(int id);
    }
}
