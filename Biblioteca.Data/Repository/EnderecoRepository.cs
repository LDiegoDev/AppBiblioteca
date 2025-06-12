using Biblioteca.Data.Context;
using Biblioteca.Domain.Interfaces;
using Biblioteca.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Biblioteca.Data.Repository
{
    public class EnderecoRepository : Repository<Endereco>, IEnderecoRepository
    {
        public EnderecoRepository(DbContextApp context) : base(context) { }

        public async Task<Endereco> ObterEnderecoPorEditora(Guid editoraId)
        {
            return await Db.Enderecos.AsNoTracking()
                .FirstOrDefaultAsync(f => f.EditoraId == editoraId);
        }
    }
}
