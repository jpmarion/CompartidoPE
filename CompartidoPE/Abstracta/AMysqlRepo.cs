using System.Data;
using System.Data.Common;
using CompartidoPE.Interface;
using MySql.Data.MySqlClient;

namespace CompartidoPE.Abstracta
{
    public abstract class AMysqlRepo : ISqlRepo
    {
        private readonly string _connectionString;
        private MySqlConnection? _conexion;
        private MySqlTransaction? _mySqlTransaction;

        public AMysqlRepo(string conexion)
        {
            MySqlConnectionStringBuilder builder = new(conexion)
            {
                Pooling = true
            };

            _connectionString = builder.ConnectionString;
        }

        public AMysqlRepo(string conexion, uint minimumPoolSize, uint maximumPoolSize)
            : this(conexion)
        {
            if (minimumPoolSize > maximumPoolSize)
            {
                throw new ArgumentOutOfRangeException(
                    nameof(minimumPoolSize),
                    "El tamaño mínimo del pool no puede superar al máximo.");
            }

            MySqlConnectionStringBuilder builder = new(_connectionString)
            {
                MinimumPoolSize = minimumPoolSize,
                MaximumPoolSize = maximumPoolSize
            };

            _connectionString = builder.ConnectionString;
        }

        public void BeginTransaction()
        {
            _mySqlTransaction = _conexion!.BeginTransaction();
        }

        public void CommitTransaction()
        {
            try
            {
                _mySqlTransaction?.Commit();
            }
            finally
            {
                CerrarConexion();
            }
        }

        public async Task ConexionOpen()
        {
            if (_conexion?.State == ConnectionState.Open)
            {
                return;
            }

            CerrarConexion();
            _conexion = new MySqlConnection(_connectionString);

            try
            {
                await _conexion.OpenAsync();
            }
            catch
            {
                CerrarConexion();
                throw;
            }
        }

        public async Task<DbConnection> ObtenerNuevaConexion()
        {
            MySqlConnection conexion = new(_connectionString);

            try
            {
                await conexion.OpenAsync();
                return conexion;
            }
            catch
            {
                await conexion.DisposeAsync();
                throw;
            }
        }

        public DbConnection ObtenerConexion()
        {
            return _conexion!;
        }

        public void RollbackTransaction()
        {
            try
            {
                _mySqlTransaction?.Rollback();
            }
            finally
            {
                CerrarConexion();
            }
        }

        public void CerrarConexion()
        {
            _mySqlTransaction?.Dispose();
            _mySqlTransaction = null;

            _conexion?.Dispose();
            _conexion = null;
        }
    }
}
