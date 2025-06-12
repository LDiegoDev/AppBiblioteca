using Biblioteca.Domain.Interfaces;
using Biblioteca.Domain.Models;

namespace Biblioteca.Domain.Services
{
    public class AutorService : BaseService, IAutorService
    {
        private readonly IAutorRepository _autorRepository;
        private readonly IAutorRepository _autorLivroRepository;

        public AutorService(IAutorRepository autorRepository,
                                 IAutorRepository autorLivroRepository,
                                 INotificador notificador) : base(notificador)
        {
            _autorRepository = autorRepository;
            _autorLivroRepository = autorLivroRepository;
        }

        public async Task<bool> Adicionar(Autor autor)
        {
            await _autorRepository.Adicionar(autor);

            return true;
        }

        public Task<bool> Atualizar(Autor autor)
        {
            throw new NotImplementedException();
        }
        public Task<bool> Remover(Guid id)
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            _autorRepository?.Dispose();
            _autorLivroRepository?.Dispose();
        }
    }
}
