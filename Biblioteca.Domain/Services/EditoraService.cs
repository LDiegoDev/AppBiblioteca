using Biblioteca.Domain.Interfaces;
using Biblioteca.Domain.Models;
using Biblioteca.Domain.Models.Validations;

namespace Biblioteca.Domain.Services
{
    public class EditoraService : BaseService, IEditoraService
    {
        private readonly IEditoraRepository _editoraRepository;
        private readonly IEnderecoRepository _enderecoRepository;

        public EditoraService(IEditoraRepository EditoraRepository,
                                 IEnderecoRepository enderecoRepository,
                                 INotificador notificador) : base(notificador)
        {
            _editoraRepository = EditoraRepository;
            _enderecoRepository = enderecoRepository;
        }

        public async Task<bool> Adicionar(Editora Editora)
        {
            if (!ExecutarValidacao(new EditoraValidation(), Editora)
                || !ExecutarValidacao(new EnderecoValidation(), Editora.Endereco)) return false;

            if (_editoraRepository.Buscar(f => f.Documento == Editora.Documento).Result.Any())
            {
                Notificar("Já existe um Editora com este documento informado.");
                return false;
            }

            await _editoraRepository.Adicionar(Editora);
            return true;
        }

        public async Task<bool> Atualizar(Editora Editora)
        {
            if (!ExecutarValidacao(new EditoraValidation(), Editora)) return false;

            if (_editoraRepository.Buscar(f => f.Documento == Editora.Documento && f.Id != Editora.Id).Result.Any())
            {
                Notificar("Já existe um Editora com este documento informado.");
                return false;
            }

            await _editoraRepository.Atualizar(Editora);
            return true;
        }

        public async Task AtualizarEndereco(Endereco endereco)
        {
            if (!ExecutarValidacao(new EnderecoValidation(), endereco)) return;

            await _enderecoRepository.Atualizar(endereco);
        }

        public async Task<bool> Remover(Guid id)
        {
            if (_editoraRepository.ObterEditoraLivrosEndereco(id).Result.Livros.Any())
            {
                Notificar("O Editora possui Livros cadastrados!");
                return false;
            }

            var endereco = await _enderecoRepository.ObterEnderecoPorEditora(id);

            if (endereco != null)
            {
                await _enderecoRepository.Remover(endereco.Id);
            }

            await _editoraRepository.Remover(id);
            return true;
        }

        public void Dispose()
        {
            _editoraRepository?.Dispose();
            _enderecoRepository?.Dispose();
        }
    }
}
