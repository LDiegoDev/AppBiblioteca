using AutoMapper;
using Biblioteca.Api.ViewModels;
using Biblioteca.Domain.Interfaces;
using Biblioteca.Domain.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Biblioteca.Api.Controllers
{
    [Route("api/[controller]")]
    public class EditorasController : MainController
    {
        private readonly IEditoraRepository _editoraRepository;
        private readonly IEditoraService _editoraService;
        private readonly IMapper _mapper;
        private readonly IEnderecoRepository _enderecoRepository;


        public EditorasController(IEditoraRepository editoraRepository, IMapper mapper,
                                  IEditoraService editoraService, INotificador notificador,
                                  IEnderecoRepository enderecoRepository) : base(notificador)
        {
            _editoraRepository = editoraRepository;
            _mapper = mapper;
            _editoraService = editoraService;
            _enderecoRepository = enderecoRepository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<EditoraViewModel>>> ObterTodos()
        {
            var editoras = _mapper.Map<IEnumerable<EditoraViewModel>>(await _editoraRepository.ObterTodos());

            return Ok(editoras);
        }

        [HttpGet("{id:guid}")]
        public async Task<ActionResult<EditoraViewModel>> ObterPorId(Guid id)
        {
            var editora = await ObterEditoraLivrosEndereco(id);

            if (editora.Value == null) return NotFound("Editora não encontrada !");

            return Ok(editora);
        }

        [HttpPost]
        public async Task<ActionResult<EditoraViewModel>> Adicionar(EditoraViewModel editoraViewModel)
        {
            if (!ModelState.IsValid) return CustomResponse(ModelState);

            if (editoraViewModel.Endereco == null) return BadRequest("O endereço precisa ser informado !");

            await _editoraService.Adicionar(_mapper.Map<Editora>(editoraViewModel));

            return CustomResponse(editoraViewModel);
        }

        [HttpPut("{id:guid}")]
        public async Task<ActionResult<EditoraViewModel>> Atualizar(Guid id, EditoraViewModel editoraViewModel)
        {
            if (id != editoraViewModel.Id) return BadRequest();

            if (!ModelState.IsValid) return CustomResponse(ModelState);

            await _editoraService.Atualizar(_mapper.Map<Editora>(editoraViewModel));

            return CustomResponse(editoraViewModel);
        }

        [HttpDelete("{id:guid}")]
        public async Task<ActionResult<EditoraViewModel>> Excluir(Guid id)
        {
            var editoraViewModel = await ObterEditoraEndereco(id);

            if (editoraViewModel is null) return NotFound();

            await _editoraService.Remover(id);

            return CustomResponse(editoraViewModel);
        }

        [HttpGet("obter-endereco/{id:guid}")]
        public async Task<EnderecoViewModel> ObterEnderecoPorId(Guid id)
        {
            return _mapper.Map<EnderecoViewModel>(await _enderecoRepository.ObterPorId(id));
        }

        [HttpPut("atualizar-endereco/{id:guid}")]
        public async Task<IActionResult> AtualizarEndereco(Guid id, EnderecoViewModel enderecoViewModel)
        {
            if (id != enderecoViewModel.Id)
            {
                NotificarErro("O id informado não é o mesmo que foi passado na query");
                return CustomResponse(enderecoViewModel);
            }

            if (!ModelState.IsValid) return CustomResponse(ModelState);

            await _editoraService.AtualizarEndereco(_mapper.Map<Endereco>(enderecoViewModel));

            return CustomResponse(enderecoViewModel);
        }

        private async Task<ActionResult<EditoraViewModel>> ObterEditoraLivrosEndereco(Guid id)
        {
            return _mapper.Map<EditoraViewModel>(await _editoraRepository.ObterEditoraLivrosEndereco(id));
        }

        private async Task<EditoraViewModel> ObterEditoraEndereco(Guid id)
        {
            return _mapper.Map<EditoraViewModel>(await _editoraRepository.ObterEditoraEndereco(id));
        }

    }
}
