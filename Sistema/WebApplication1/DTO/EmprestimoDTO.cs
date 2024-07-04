using app.DTO;

public class EmprestimoDTO
{
    public long Id { get; set; }
    public DateTime Data { get; set; }
    public DateTime Hora { get; set; }
    public AlunoDTO Aluno { get; set; } // Alteração aqui para usar AlunoDTO
    public LivroDTO Livro { get; set; } // Alteração aqui para usar LivroDTO
}
