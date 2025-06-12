using AutoMapper;
using Biblioteca.Api.ViewModels;
using Biblioteca.Domain.Models;

namespace Biblioteca.Api.Configuration
{
    public class AutoMapperConfig : Profile
    {
        public AutoMapperConfig()
        {
            CreateMap<Editora, EditoraViewModel>().ReverseMap();
            CreateMap<Endereco, EnderecoViewModel>().ReverseMap();
            CreateMap<Autor, AutorViewModel>().ReverseMap();
            CreateMap<LivroViewModel, Livro>();

            CreateMap<Livro, LivroViewModel>()
                .ForMember(destinationMember: dest => dest.NomeEditora, memberOptions: opt => opt.MapFrom(mapExpression: src => src.Editora.Nome))
                .ForMember(destinationMember: dest => dest.NomeAutor, memberOptions: opt => opt.MapFrom(mapExpression: src => src.Autor.Nome));

        }
    }
}
