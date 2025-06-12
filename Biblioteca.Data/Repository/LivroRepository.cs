using Biblioteca.Data.Context;
using Biblioteca.Domain.Interfaces;
using Biblioteca.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Biblioteca.Data.Repository
{
    public class LivroRepository : Repository<Livro>, ILivroRepository
    {
        public LivroRepository(DbContextApp context) : base(context) { }
        
        public async Task<Livro> ObterLivroEditora(Guid id)
        {
            return await Db.Livros.AsNoTracking().Include(f => f.Editora)
                .FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task<IEnumerable<Livro>> ObterLivrosEditoras()
        {
            var livros = await Db.Livros.AsNoTracking().Include(e => e.Editora).Include(a => a.Autor)
                .OrderBy(p => p.Nome).ToListAsync();

            return livros;
        }

        public async Task<IEnumerable<Livro>> ObterLivrosPorEditora(Guid editoraId)
        {
            return await Buscar(p => p.EditoraId == editoraId);
        }
    }
}
