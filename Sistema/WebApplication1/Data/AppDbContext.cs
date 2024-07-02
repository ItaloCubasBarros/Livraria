using System;
using System.Data;
using Microsoft.Extensions.Configuration;
using Npgsql;
using System.Threading.Tasks;

namespace app.Data
{
    public class AppDbContext
    {
        private readonly string _connectionString;
        private NpgsqlConnection _connection;
        private NpgsqlTransaction _transaction;

        public AppDbContext(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
            _connection = new NpgsqlConnection(_connectionString);
        }

        public void OpenConnection()
        {
            if (_connection.State != ConnectionState.Open)
            {
                _connection.Open();
            }
        }

        public void BeginTransaction()
        {
            if (_connection.State != ConnectionState.Open)
                _connection.Open();

            _transaction = _connection.BeginTransaction();
        }

        public void Commit()
        {
            _transaction?.Commit();
            CloseConnection();
        }

        public bool TransactionIsActive()
        {
            return _transaction != null && _transaction.Connection != null;
        }

        public void Rollback()
        {
            if (_transaction != null && _transaction.Connection != null)
            {
                _transaction.Rollback();
            }
            CloseConnection();
        }

        public void CloseConnection()
        {
            if (_connection.State != ConnectionState.Closed)
                _connection.Close();
        }

        public async Task<DataTable> ExecuteQuery(string sql, NpgsqlParameter[]? param = null)
        {
            OpenConnection();  // Ensure connection is open
            using (var cmd = new NpgsqlCommand(sql, _connection, _transaction))
            {
                if (param != null)
                {
                    cmd.Parameters.AddRange(param);
                }

                using (var reader = await cmd.ExecuteReaderAsync())
                {
                    var result = new DataTable();
                    result.Load(reader);
                    return result;
                }
            }
        }

        public async Task<int> ExecuteNonQuery(string sql, NpgsqlParameter[]? param = null)
        {
            OpenConnection();  // Ensure connection is open
            using (var cmd = new NpgsqlCommand(sql, _connection, _transaction))
            {
                if (param != null)
                {
                    cmd.Parameters.AddRange(param);
                }

                return await cmd.ExecuteNonQueryAsync();
            }
        }

        public async Task<object?> ExecuteScalar(string sql, NpgsqlParameter[]? param = null)
        {
            OpenConnection();  // Ensure connection is open
            using (var cmd = new NpgsqlCommand(sql, _connection, _transaction))
            {
                if (param != null)
                {
                    cmd.Parameters.AddRange(param);
                }

                return await cmd.ExecuteScalarAsync();
            }
        }
    }
}
