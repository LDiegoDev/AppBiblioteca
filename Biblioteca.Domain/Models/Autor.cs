namespace Biblioteca.Domain.Models
{
    public class Autor : Entity
    {
        public string Nome { get; private set; }

        /* EF Relations */
        public IEnumerable<Livro> Livros { get; private set; }
    }
}
