using Biblioteca.Domain.Models;

namespace Biblioteca.Domain.Interfaces
{
    public interface ILivroRepository : IRepository<Livro>
    {
        Task<IEnumerable<Livro>> ObterLivrosPorEditora(Guid editoraId);
        Task<IEnumerable<Livro>> ObterLivrosEditoras();
        Task<Livro> ObterLivroEditora(Guid id);
    }
}
