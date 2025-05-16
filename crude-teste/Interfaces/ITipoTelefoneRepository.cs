using crude_teste.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace crude_teste.Domain.Interfaces
{
    public interface ITipoTelefoneRepository
    {
        Task<IEnumerable<TipoTelefone>> GetAllAsync();
        Task<TipoTelefone> GetByIdAsync(int id);
    }
}
