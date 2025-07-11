﻿using System.ComponentModel.DataAnnotations;

namespace Biblioteca.Api.ViewModels
{
    public class EditoraViewModel
    {
        [Key]
        public Guid Id { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        [StringLength(100, ErrorMessage = "O campo {0} precisa ter entre {2} e {1} caracteres", MinimumLength = 2)]
        public string Nome { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        [StringLength(14, ErrorMessage = "O campo {0} precisa ter entre {2} e {1} caracteres", MinimumLength = 11)]
        public string Documento { get; set; }

        public int TipoEditora { get; set; }

        public EnderecoViewModel Endereco { get; set; }

        public bool Ativo { get; set; }

        public IEnumerable<LivroViewModel> Livros { get; set; }
    }
}
