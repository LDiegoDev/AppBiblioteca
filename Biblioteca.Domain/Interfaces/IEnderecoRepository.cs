using Biblioteca.Domain.Models;

namespace Biblioteca.Domain.Interfaces
{
    public interface IEnderecoRepository : IRepository<Endereco>
    {
        Task<Endereco> ObterEnderecoPorEditora(Guid EditoraId);
    }
}
