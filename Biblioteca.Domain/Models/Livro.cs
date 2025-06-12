namespace Biblioteca.Domain.Models
{
    public class Livro : Entity
    {
        public Guid EditoraId { get; private set; }
        public Guid AutorId { get; private set; }
        public string Nome { get; private set; }
        public string Descricao { get; private set; }
        public string Imagem { get; private set; }
        public decimal Valor { get; private set; }
        public DateTime DataCadastro { get; private set; }
        public bool Ativo { get; private set; }

        /* EF Relations */
        public Editora Editora { get; private set; }

        public Autor Autor { get; private set; }

    }
}
