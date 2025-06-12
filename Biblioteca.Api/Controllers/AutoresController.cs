using AutoMapper;
using Biblioteca.Api.ViewModels;
using Biblioteca.Domain.Interfaces;
using Biblioteca.Domain.Models;
using Microsoft.AspNetCore.Mvc;

namespace Biblioteca.Api.Controllers
{

    [Route("api/autores")]
    public class AutoresController : MainController
    {
        private readonly IAutorRepository _autorRepository;
        private readonly IAutorService _autorService;
        private readonly IMapper _mapper;

        public AutoresController(IAutorRepository autorRepository, IMapper mapper,
                                  IAutorService autorService,
                                  INotificador notificador) : base(notificador)
        {
            _autorRepository = autorRepository;
            _mapper = mapper;
            _autorService = autorService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<AutorViewModel>>> ObterTodos()
        {
            var autores = _mapper.Map<IEnumerable<AutorViewModel>>(await _autorRepository.ObterTodos());

            return CustomResponse(autores);
        }

        [HttpGet("{id:guid}")]
        public async Task<ActionResult<AutorViewModel>> ObterPorId(Guid id)
        {
            var autor = await ObterAutorLivros(id);

            if (autor == null) return NotFound();

            return CustomResponse(autor);

        }

        [HttpPost]
        public async Task<ActionResult<AutorViewModel>> Adicionar(AutorViewModel autor)
        {
            if (!ModelState.IsValid) return CustomResponse(ModelState);

            await _autorService.Adicionar(_mapper.Map<Autor>(autor));

            return CustomResponse(autor);
        }
        private async Task<AutorViewModel> ObterAutorLivros(Guid id)
        {
            return _mapper.Map<AutorViewModel>(await _autorRepository.ObterAutorLivros(id));
        }
    }
}
