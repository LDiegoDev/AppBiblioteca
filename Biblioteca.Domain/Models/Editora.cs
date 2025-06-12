using Biblioteca.Domain.Enums;

namespace Biblioteca.Domain.Models
{
    public class Editora : Entity
    {
        public string Nome { get; private set; }
        public string Documento { get; private set; }
        public TipoEditora TipoEditora { get; private set; }
        public Endereco Endereco { get; private set; }
        public bool Ativo { get; private set; }

        /* EF Relations */
        public IEnumerable<Livro> Livros { get; private set; }
    }
}
