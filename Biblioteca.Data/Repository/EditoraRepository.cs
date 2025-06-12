using Biblioteca.Data.Context;
using Biblioteca.Domain.Interfaces;
using Biblioteca.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Biblioteca.Data.Repository
{
    public class EditoraRepository : Repository<Editora>, IEditoraRepository
    {
        public EditoraRepository(DbContextApp context) : base(context)
        {
        }

        public async Task<Editora> ObterEditoraEndereco(Guid id)
        {
            try
            {
                return await Db.Editoras.AsNoTracking()
                    .Include(c => c.Endereco)
                    .FirstOrDefaultAsync(c => c.Id == id);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<Editora> ObterEditoraLivrosEndereco(Guid id)
        {
            try
            {
                return await Db.Editoras.AsNoTracking()
                    .Include(x => x.Livros)
                    .Include(x => x.Endereco)
                    .FirstOrDefaultAsync(x => x.Id == id);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
