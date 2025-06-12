using AutoMapper;
using Biblioteca.Api.ViewModels;
using Biblioteca.Domain.Interfaces;
using Biblioteca.Domain.Models;
using Microsoft.AspNetCore.Mvc;

namespace Biblioteca.Api.Controllers
{
    [Route("api/livros")]
    public class LivrosController : MainController
    {
        private readonly ILivroRepository _livroRepository;
        private readonly ILivroService _livroService;
        private readonly IMapper _mapper;

        public LivrosController(INotificador notificador,
                                ILivroRepository livroRepository,
                                ILivroService livroService,
                                IMapper mapper) : base(notificador)
        {
            _livroRepository = livroRepository;
            _livroService = livroService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IEnumerable<LivroViewModel>> ObterTodos()
        {
            return _mapper.Map<IEnumerable<LivroViewModel>>(await _livroRepository.ObterLivrosEditoras());
        }

        [HttpGet("{id:guid}")]
        public async Task<ActionResult<LivroViewModel>> ObterPorId(Guid id)
        {
            var livroViewModel = await ObterLivro(id);

            if (livroViewModel == null) return NotFound();

            return livroViewModel;
        }

        [HttpPost]
        public async Task<ActionResult<LivroViewModel>> Adicionar(LivroViewModel livroViewModel)
        {
            if (!ModelState.IsValid) return CustomResponse(ModelState);

            var imagemNome = Guid.NewGuid() + "_" + livroViewModel.Imagem;
            if (!UploadArquivo(livroViewModel.ImagemUpload, imagemNome))
            {
                return CustomResponse(livroViewModel);
            }

            livroViewModel.Imagem = imagemNome;
            await _livroService.Adicionar(_mapper.Map<Livro>(livroViewModel));

            return CustomResponse(livroViewModel);
        }

        [HttpPut("{id:guid}")]
        public async Task<IActionResult> Atualizar(Guid id, LivroViewModel livroViewModel)
        {
            if (id != livroViewModel.Id)
            {
                NotificarErro("Os ids informados não são iguais!");
                return CustomResponse();
            }

            var livroAtualizacao = await ObterLivro(id);
            livroViewModel.Imagem = livroAtualizacao.Imagem;
            if (!ModelState.IsValid) return CustomResponse(ModelState);

            if (livroViewModel.ImagemUpload != null)
            {
                var imagemNome = Guid.NewGuid() + "_" + livroViewModel.Imagem;
                if (!UploadArquivo(livroViewModel.ImagemUpload, imagemNome))
                {
                    return CustomResponse(ModelState);
                }

                livroAtualizacao.Imagem = imagemNome;
            }

            livroAtualizacao.Nome = livroViewModel.Nome;
            livroAtualizacao.Descricao = livroViewModel.Descricao;
            livroAtualizacao.Valor = livroViewModel.Valor;
            livroAtualizacao.Ativo = livroViewModel.Ativo;

            await _livroService.Atualizar(_mapper.Map<Livro>(livroAtualizacao));

            return CustomResponse(livroViewModel);
        }

        [HttpDelete("{id:guid}")]
        public async Task<ActionResult<LivroViewModel>> Excluir(Guid id)
        {
            var livro = await ObterLivro(id);

            if (livro == null) return NotFound();

            await _livroService.Remover(id);

            return CustomResponse(livro);
        }

        private bool UploadArquivo(string arquivo, string imgNome)
        {
            if (string.IsNullOrEmpty(arquivo))
            {
                NotificarErro("Forneça uma imagem para este livro!");
                return false;
            }

            var imageDataByteArray = Convert.FromBase64String(arquivo);

            var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/imagens", imgNome);

            if (System.IO.File.Exists(filePath))
            {
                NotificarErro("Já existe um arquivo com este nome!");
                return false;
            }

            System.IO.File.WriteAllBytes(filePath, imageDataByteArray);

            return true;
        }

        private async Task<LivroViewModel> ObterLivro(Guid id)
        {
            return _mapper.Map<LivroViewModel>(await _livroRepository.ObterLivroEditora(id));
        }
    }
}
