using Biblioteca.Domain.Models;

namespace Biblioteca.Domain.Interfaces
{
    public interface IAutorRepository : IRepository<Autor>
    {
        Task<Autor> ObterAutorLivros(Guid id);
    }
}
