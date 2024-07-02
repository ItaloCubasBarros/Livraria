using app.Data;
using app.DTO;
using Npgsql;
using System.Data;
using System.Text;

namespace app.DAO
{
    public class AlunoDAO
    {
        private readonly AppDbContext _context;

        public AlunoDAO(AppDbContext context)
        {
            _context = context;
        }

        // Método para buscar um Aluno pelo ID
        public async Task<AlunoDTO> GetById(long id)
        {
            var objSelect = new StringBuilder();
            objSelect.Append("SELECT ");
            objSelect.Append("\"Livraria\".\"Aluno\".\"Id\", ");
            objSelect.Append("\"Livraria\".\"Aluno\".\"Nome\", ");
            objSelect.Append("\"Livraria\".\"Aluno\".\"Cpf\" ");
            objSelect.Append("\"Livraria\".\"Aluno\".\"Email\", ");
            objSelect.Append("\"Livraria\".\"Aluno\".\"Telefone\" ");
            objSelect.Append("\"Livraria\".\"Aluno\".\"Endereco\" ");
            //objSelect.Append("\"Livraria\".\"Aluno\".\"Nascimento\" ");
            objSelect.Append("\"Livraria\".\"Aluno\".\"Sexo\" ");
            objSelect.Append("FROM \"Livraria\".\"Aluno\" ");
            objSelect.Append("WHERE \"Livraria\".\"Aluno\".\"Id\" = @Id ");

            var parameters = new List<Npgsql.NpgsqlParameter>
    {
        new Npgsql.NpgsqlParameter("@Id", NpgsqlTypes.NpgsqlDbType.Bigint) { Value = id }
    };

            var dt = await _context.ExecuteQuery(objSelect.ToString());

            AlunoDTO aluno = null;

            if (dt != null && dt.Rows.Count > 0)
            {
                var row = dt.Rows[0];
                aluno = new AlunoDTO
                {
                    Id = Convert.ToInt64(row["Id"]),
                    Nome = row["Nome"].ToString(),
                    Cpf = row["Cpf"].ToString(),
                    Email = row["Genero"].ToString(),
                    Telefone = row["Telefone"].ToString(),
                    Endereco = row["Endereco"].ToString(),
                    Sexo = row["Sexo"].ToString(),
                    //Nascimento = row["Nascimento"]

                };
            }

            return aluno;
        }


        // Método para obter todos os Alunos
        public async Task<List<AlunoDTO>> GetAll(AlunoDTO dto)
        {
            var objSelect = new StringBuilder();
            objSelect.Append("SELECT ");
            objSelect.Append("\"Livraria\".\"Aluno\".\"Id\", ");
            objSelect.Append("\"Livraria\".\"Aluno\".\"Nome\", ");
            objSelect.Append("\"Livraria\".\"Aluno\".\"Cpf\", ");
            objSelect.Append("\"Livraria\".\"Aluno\".\"Email\", ");
            objSelect.Append("\"Livraria\".\"Aluno\".\"Telefone\", ");
            objSelect.Append("\"Livraria\".\"Aluno\".\"Endereco\", ");
            objSelect.Append("\"Livraria\".\"Aluno\".\"Sexo\" ");

            objSelect.Append("FROM \"Livraria\".\"Aluno\" ");

            objSelect.Append("WHERE 1 = 1 ");

            if (dto != null)
            {
                if (dto.Id > 0)
                {
                    objSelect.Append($"AND \"Id\" = {dto.Id} ");
                }
                if (!string.IsNullOrEmpty(dto.Nome))
                {
                    objSelect.Append($"AND \"Nome\" ILIKE '%{dto.Nome}%' ");
                }
                if (!string.IsNullOrEmpty(dto.Cpf))
                {
                    objSelect.Append($"AND \"Cpf\" ILIKE '%{dto.Cpf}%' ");
                }
                if (!string.IsNullOrEmpty(dto.Email))
                {
                    objSelect.Append($"AND \"Email\" ILIKE '%{dto.Email}%' ");
                }
                if (!string.IsNullOrEmpty(dto.Telefone))
                {
                    objSelect.Append($"AND \"Telefone\" ILIKE '%{dto.Telefone}%' ");
                }
                if (!string.IsNullOrEmpty(dto.Endereco))
                {
                    objSelect.Append($"AND \"Endereco\" ILIKE '%{dto.Endereco}%' ");
                }
                if (!string.IsNullOrEmpty(dto.Sexo))
                {
                    objSelect.Append($"AND \"Sexo\" ILIKE '%{dto.Sexo}%' ");
                }

                var dt = await _context.ExecuteQuery(objSelect.ToString(), null);

                var lstAlunos = new List<AlunoDTO>();

                if (dt != null && dt.Rows.Count > 0)
                {
                    foreach (DataRow row in dt.Rows)
                    {
                        var aluno = new AlunoDTO
                        {
                            Id = Convert.ToInt64(row["Id"]),
                            Nome = row["Nome"].ToString(),
                            Cpf = row["Cpf"].ToString(),
                            Email = row["Email"].ToString(), // Corrigido para Email
                            Telefone = row["Telefone"].ToString(),
                            Endereco = row["Endereco"].ToString(),
                            Sexo = row["Sexo"].ToString()
                        };
                        lstAlunos.Add(aluno);
                    }
                }

                return lstAlunos;
            }

            // Retorno padrão se dto for null
            return new List<AlunoDTO>();
        }

        // Método de inserção de um aluno
        public async Task<int> Insert(AlunoDTO dto)
        {
            var objInsert = new StringBuilder();
            objInsert.Append("INSERT INTO \"Livraria\".\"Aluno\" (");
            objInsert.Append("\"Nome\", \"Cpf\", \"Email\", \"Telefone\", \"Endereco\", \"Sexo\") ");
            objInsert.Append("VALUES (");
            objInsert.Append($"@Nome, @Cpf, @Email, @Telefone, @Endereco, @Sexo) ");
            objInsert.Append("RETURNING \"Id\";");

            var parameters = new List<NpgsqlParameter>
            {
                new NpgsqlParameter("@Nome", NpgsqlTypes.NpgsqlDbType.Text) { Value = dto.Nome },
                new NpgsqlParameter("@Cpf", NpgsqlTypes.NpgsqlDbType.Text) { Value = dto.Cpf },
                new NpgsqlParameter("@Email", NpgsqlTypes.NpgsqlDbType.Text) { Value = dto.Email },
                new NpgsqlParameter("@Telefone", NpgsqlTypes.NpgsqlDbType.Text) { Value = dto.Telefone },
                new NpgsqlParameter("@Endereco", NpgsqlTypes.NpgsqlDbType.Text) { Value = dto.Endereco },
                new NpgsqlParameter("@Sexo", NpgsqlTypes.NpgsqlDbType.Text) { Value = dto.Sexo }
            };

            var id = await _context.ExecuteScalar(objInsert.ToString(), parameters.ToArray());
            return Convert.ToInt32(id);
        }

        // Método de atualização de um aluno
        public async Task<int> Update(AlunoDTO dto)
        {
            var objUpdate = new StringBuilder();
            objUpdate.Append("UPDATE \"Livraria\".\"Aluno\" SET ");
            objUpdate.Append("\"Nome\" = @Nome, ");
            objUpdate.Append("\"Cpf\" = @Cpf, ");
            objUpdate.Append("\"Email\" = @Email, ");
            objUpdate.Append("\"Telefone\" = @Telefone, ");
            objUpdate.Append("\"Endereco\" = @Endereco, ");
            objUpdate.Append("\"Sexo\" = @Sexo ");
            objUpdate.Append("WHERE \"Id\" = @Id;");

            var parameters = new List<NpgsqlParameter>
            {
                new NpgsqlParameter("@Nome", NpgsqlTypes.NpgsqlDbType.Text) { Value = dto.Nome },
                new NpgsqlParameter("@Cpf", NpgsqlTypes.NpgsqlDbType.Text) { Value = dto.Cpf },
                new NpgsqlParameter("@Email", NpgsqlTypes.NpgsqlDbType.Text) { Value = dto.Email },
                new NpgsqlParameter("@Telefone", NpgsqlTypes.NpgsqlDbType.Text) { Value = dto.Telefone },
                new NpgsqlParameter("@Endereco", NpgsqlTypes.NpgsqlDbType.Text) { Value = dto.Endereco },
                new NpgsqlParameter("@Sexo", NpgsqlTypes.NpgsqlDbType.Text) { Value = dto.Sexo },
                new NpgsqlParameter("@Id", NpgsqlTypes.NpgsqlDbType.Bigint) { Value = dto.Id }
            };

            var rowsAffected = await _context.ExecuteNonQuery(objUpdate.ToString(), parameters.ToArray());
            return rowsAffected;
        }

        // Método de exclusão de um aluno
        public async Task Delete(long id)
        {
            var objDelete = new StringBuilder();
            objDelete.Append("DELETE FROM \"Livraria\".\"Aluno\" ");
            objDelete.Append("WHERE \"Id\" = @Id");

            var parameters = new List<NpgsqlParameter>
            {
                new NpgsqlParameter("@Id", NpgsqlTypes.NpgsqlDbType.Bigint) { Value = id }
            };

            await _context.ExecuteNonQuery(objDelete.ToString(), parameters.ToArray());
        }
    } 
}
