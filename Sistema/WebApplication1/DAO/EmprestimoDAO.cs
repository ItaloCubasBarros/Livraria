using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;
using app.Data;
using app.DTO;
using Npgsql;

namespace app.DAO
{
    public class EmprestimoDAO
    {
        private readonly AppDbContext _context;

        public EmprestimoDAO(AppDbContext context)
        {
            _context = context;
        }

        // Método para obter todos os empréstimos
        public async Task<List<EmprestimoDTO>> GetAll(EmprestimoDTO dto)
        {
            var objSelect = new StringBuilder();
            objSelect.Append("SELECT ");
            objSelect.Append("\"Livraria\".\"Emprestimo\".\"Id\", ");
            objSelect.Append("\"Livraria\".\"Emprestimo\".\"Data\", ");
            objSelect.Append("\"Livraria\".\"Emprestimo\".\"Hora\", ");
            objSelect.Append("\"Livraria\".\"Emprestimo\".\"AlunoId\", ");
            objSelect.Append("\"Livraria\".\"Emprestimo\".\"LivroId\" ");
            objSelect.Append("FROM \"Livraria\".\"Emprestimo\" ");
            objSelect.Append("WHERE 1 = 1 ");

            if (dto != null)
            {
                if (dto.Id > 0)
                {
                    objSelect.Append($"AND \"Id\" = {dto.Id} ");
                }
                if (dto.Data != null)
                {
                    objSelect.Append($"AND \"Data\" = '{dto.Data:yyyy-MM-dd}' ");
                }
                if (dto.Hora != null)
                {
                    objSelect.Append($"AND \"Hora\" = '{dto.Hora:HH:mm:ss}' ");
                }
                if (dto.Alunos != null && dto.Alunos.Id > 0)
                {
                    objSelect.Append($"AND \"AlunoId\" = {dto.Alunos.Id} ");
                }
                if (dto.Livros != null && dto.Livros.Id > 0)
                {
                    objSelect.Append($"AND \"LivroId\" = {dto.Livros.Id} ");
                }
            }

            var dt = await _context.ExecuteQuery(objSelect.ToString(), null);

            var lstEmprestimos = new List<EmprestimoDTO>();

            if (dt != null && dt.Rows.Count > 0)
            {
                foreach (DataRow row in dt.Rows)
                {
                    var emprestimo = new EmprestimoDTO
                    {
                        Id = Convert.ToInt64(row["Id"]),
                        Data = row["Data"] == DBNull.Value ? null : (DateTime?)Convert.ToDateTime(row["Data"]),
                        Hora = row["Hora"] == DBNull.Value ? null : (DateTime?)Convert.ToDateTime(row["Hora"]),
                        Alunos = new AlunoDTO { Id = Convert.ToInt64(row["AlunoId"]) },
                        Livros = new LivroDTO { Id = Convert.ToInt64(row["LivroId"]) }
                    };
                    lstEmprestimos.Add(emprestimo);
                }
            }

            return lstEmprestimos;
        }

        // Método para obter um empréstimo por Id
        public async Task<EmprestimoDTO> GetById(long id)
        {
            var objSelect = new StringBuilder();
            objSelect.Append("SELECT ");
            objSelect.Append("\"Livraria\".\"Emprestimo\".\"Id\", ");
            objSelect.Append("\"Livraria\".\"Emprestimo\".\"Data\", ");
            objSelect.Append("\"Livraria\".\"Emprestimo\".\"Hora\", ");
            objSelect.Append("\"Livraria\".\"Emprestimo\".\"AlunoId\", ");
            objSelect.Append("\"Livraria\".\"Emprestimo\".\"LivroId\" ");
            objSelect.Append("FROM \"Livraria\".\"Emprestimo\" ");
            objSelect.Append("WHERE \"Livraria\".\"Emprestimo\".\"Id\" = @Id ");

            var parameters = new List<NpgsqlParameter>
            {
                new NpgsqlParameter("@Id", NpgsqlTypes.NpgsqlDbType.Bigint) { Value = id }
            };

            var dt = await _context.ExecuteQuery(objSelect.ToString(), parameters.ToArray());

            EmprestimoDTO emprestimo = null;

            if (dt != null && dt.Rows.Count > 0)
            {
                var row = dt.Rows[0];
                emprestimo = new EmprestimoDTO
                {
                    Id = Convert.ToInt64(row["Id"]),
                    Data = row["Data"] == DBNull.Value ? null : (DateTime?)Convert.ToDateTime(row["Data"]),
                    Hora = row["Hora"] == DBNull.Value ? null : (DateTime?)Convert.ToDateTime(row["Hora"]),
                    Alunos = new AlunoDTO { Id = Convert.ToInt64(row["AlunoId"]) },
                    Livros = new LivroDTO { Id = Convert.ToInt64(row["LivroId"]) }
                };
            }

            return emprestimo;
        }

        // Método de Insert
        public async Task<int> Insert(EmprestimoDTO dto)
        {
            var objInsert = new StringBuilder();
            objInsert.Append("INSERT INTO \"Livraria\".\"Emprestimo\" (");
            objInsert.Append("\"Data\", \"Hora\", \"AlunoId\", \"LivroId\"");
            objInsert.Append(") VALUES (");
            objInsert.Append($"'{dto.Data:yyyy-MM-dd}', '{dto.Hora:HH:mm:ss}', {dto.Alunos?.Id}, {dto.Livros?.Id}");
            objInsert.Append(");");

            var id = await _context.ExecuteNonQuery(objInsert.ToString(), null);

            return id;
        }

        // Método de Update
        public async Task<int> Update(EmprestimoDTO dto)
        {
            var objUpdate = new StringBuilder();
            objUpdate.Append("UPDATE \"Livraria\".\"Emprestimo\" SET ");
            objUpdate.Append($"\"Data\" = '{dto.Data:yyyy-MM-dd}', ");
            objUpdate.Append($"\"Hora\" = '{dto.Hora:HH:mm:ss}', ");
            objUpdate.Append($"\"AlunoId\" = {dto.Alunos?.Id}, ");
            objUpdate.Append($"\"LivroId\" = {dto.Livros?.Id} ");
            objUpdate.Append($"WHERE \"Id\" = {dto.Id};");

            var id = await _context.ExecuteNonQuery(objUpdate.ToString(), null);

            return id;
        }

        // Método de Delete
        public async Task Delete(long? id)
        {
            var objDelete = new StringBuilder();
            objDelete.Append("DELETE FROM \"Livraria\".\"Emprestimo\" ");
            objDelete.Append($"WHERE \"Id\" = {id};");

            await _context.ExecuteNonQuery(objDelete.ToString(), null);
        }
    }
}