namespace app.DTO
{
    public class EmprestimoDTO
    {
        public long? Id { get; set; }
        public DateTime? Data { get; set; }
        public DateTime? Hora { get; set; }
        
        public AlunoDTO? Alunos { get; set; }
        public LivroDTO? Livros { get; set; }
    }
}
