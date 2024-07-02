using System.Diagnostics.Tracing;

namespace app.DTO
{
    public class AlunoDTO
    {
        public long? Id { get; set; }
        public string? Nome { get; set; }
        public string? Cpf { get; set; }
        public string? Email { get; set; }
        public string? Telefone { get; set; }
        public string? Endereco { get; set; }
        //public DateTime? Nascimento { get; set; }
        public string? Sexo { get; set; }

        public LivroDTO? Livro { get; set; }
        



    }
}
