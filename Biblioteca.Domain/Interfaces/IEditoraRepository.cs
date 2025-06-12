using Biblioteca.Domain.Models;

namespace Biblioteca.Domain.Interfaces
{
    public interface IEditoraRepository : IRepository<Editora>
    {
        Task<Editora> ObterEditoraEndereco(Guid id);
        Task<Editora> ObterEditoraLivrosEndereco(Guid id);
    }
}
