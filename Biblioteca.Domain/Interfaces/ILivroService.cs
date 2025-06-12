using Biblioteca.Domain.Models;

namespace Biblioteca.Domain.Interfaces
{
    public interface ILivroService : IDisposable
    {
        Task Adicionar(Livro livro);
        Task Atualizar(Livro livro);
        Task Remover(Guid id);
    }
}
