using app.Data;
using app.DTO;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;

namespace app.DAO
{
    public class LivroDAO
    {
        private readonly AppDbContext _context;

        public LivroDAO(AppDbContext context)
        {
            _context = context;
        }

        // Método para buscar um livro pelo ID
        public async Task<LivroDTO> GetById(long id)
        {
            var objSelect = new StringBuilder();
            objSelect.Append("SELECT ");
            objSelect.Append("\"Livraria\".\"Livros\".\"Id\", ");
            objSelect.Append("\"Livraria\".\"Livros\".\"Titulo\", ");
            objSelect.Append("\"Livraria\".\"Livros\".\"Autor\", ");
            objSelect.Append("\"Livraria\".\"Livros\".\"Genero\" ");
            objSelect.Append("FROM \"Livraria\".\"Livros\" ");
            objSelect.Append("WHERE \"Livraria\".\"Livros\".\"Id\" = @Id ");

            var parameters = new List<Npgsql.NpgsqlParameter>
    {
        new Npgsql.NpgsqlParameter("@Id", NpgsqlTypes.NpgsqlDbType.Bigint) { Value = id }
    };

            var dt = await _context.ExecuteQuery(objSelect.ToString());

            LivroDTO livro = null;

            if (dt != null && dt.Rows.Count > 0)
            {
                var row = dt.Rows[0];
                livro = new LivroDTO
                {
                    Id = Convert.ToInt64(row["Id"]),
                    Titulo = row["Titulo"].ToString(),
                    Autor = row["Autor"].ToString(),
                    Genero = row["Genero"].ToString()
                };
            }

            return livro;
        }


        // Método para obter todos os livros
        public async Task<List<LivroDTO>> GetAll(LivroDTO dto)
        {
            var objSelect = new StringBuilder();
            objSelect.Append("SELECT ");
            objSelect.Append("\"Livraria\".\"Livros\".\"Id\", ");
            objSelect.Append("\"Livraria\".\"Livros\".\"Titulo\", ");
            objSelect.Append("\"Livraria\".\"Livros\".\"Autor\", ");
            objSelect.Append("\"Livraria\".\"Livros\".\"Genero\" ");

            objSelect.Append("FROM \"Livraria\".\"Livros\" ");

            objSelect.Append("WHERE 1 = 1 ");

            if (dto != null)
            {
                if (dto.Id > 0)
                {
                    objSelect.Append($"AND \"Id\" = {dto.Id} ");
                }
                if (!string.IsNullOrEmpty(dto.Titulo))
                {
                    objSelect.Append($"AND \"Titulo\" ILIKE '%{dto.Titulo}%' ");
                }
                if (!string.IsNullOrEmpty(dto.Autor))
                {
                    objSelect.Append($"AND \"Autor\" ILIKE '%{dto.Autor}%' ");
                }
                if (!string.IsNullOrEmpty(dto.Genero))
                {
                    objSelect.Append($"AND \"Genero\" ILIKE '%{dto.Genero}%' ");
                }
            }

            var dt = await _context.ExecuteQuery(objSelect.ToString(), null);

            var lstLivros = new List<LivroDTO>();

            if (dt != null && dt.Rows.Count > 0)
            {
                foreach (DataRow row in dt.Rows)
                {
                    var livro = new LivroDTO
                    {
                        Id = Convert.ToInt64(row["Id"]),
                        Titulo = row["Titulo"].ToString(),
                        Autor = row["Autor"].ToString(),
                        Genero = row["Genero"].ToString()
                    };
                    lstLivros.Add(livro);
                }
            }

            return lstLivros;
        }

        // Método de Insert
        public async Task<int> Insert(LivroDTO dto)
        {
            var objInsert = new StringBuilder();
            objInsert.Append("INSERT INTO \"Livraria\".\"Livros\" (");
            objInsert.Append(" \"Titulo\", \"Autor\", \"Genero\" ");
            objInsert.Append(") VALUES (");
            objInsert.Append($" '{dto.Titulo}', '{dto.Autor}', '{dto.Genero}' ");
            objInsert.Append(" ); ");

            var id = await _context.ExecuteNonQuery(objInsert.ToString(), null);

            return id;
        }

        // Método de Update
        public async Task<int> Update(LivroDTO dto)
        {
            var objUpdate = new StringBuilder();
            objUpdate.Append("UPDATE \"Livraria\".\"Livros\" SET ");
            objUpdate.Append($" \"Titulo\" = '{dto.Titulo}', ");
            objUpdate.Append($" \"Autor\" = '{dto.Autor}', ");
            objUpdate.Append($" \"Genero\" = '{dto.Genero}' ");
            objUpdate.Append($"WHERE \"Id\" = {dto.Id}; ");

            var id = await _context.ExecuteNonQuery(objUpdate.ToString(), null);

            return id;
        }

        // Método de Delete
        public async Task Delete(long? id)
        {
            var objDelete = new StringBuilder();
            objDelete.Append("DELETE FROM \"Livraria\".\"Livros\" ");
            objDelete.Append($" WHERE \"Id\" = {id} ");

            await _context.ExecuteNonQuery(objDelete.ToString(), null);
        }
    }
}
