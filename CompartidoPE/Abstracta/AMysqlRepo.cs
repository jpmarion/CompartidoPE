using System.Data;
using System.Data.Common;
using CompartidoPE.Interface;
using MySql.Data.MySqlClient;

namespace CompartidoPE.Abstracta
{
    public abstract class AMysqlRepo(string conexion) : ISqlRepo
    {
        private readonly string _connectionString = conexion;
        private MySqlConnection? _conexion;
        MySqlTransaction? _mySqlTransaction = null;

        public void BeginTransaction()
        {
            _mySqlTransaction = _conexion!.BeginTransaction();
        }

        public void CommitTransaction()
        {
            _mySqlTransaction?.Commit();
            _conexion!.CloseAsync();
            _conexion.Dispose();
        }

        public async Task ConexionOpen()
        {
            _conexion = new MySqlConnection(_connectionString);
            if (_conexion.State != ConnectionState.Open)
            {
                await _conexion.OpenAsync();
            }
        }

        public async Task<DbConnection> ObtenerNuevaConexion()
        {
            MySqlConnection conexion = new(_connectionString);
            if (conexion.State != ConnectionState.Open)
            {
                await conexion.OpenAsync();
            }

            return conexion;
        }

        public DbConnection ObtenerConexion()
        {
            return _conexion!;
        }

        public void RollbackTransaction()
        {
            if (_conexion != null)
            {
                if (_mySqlTransaction != null)
                {
                    _mySqlTransaction!.Rollback();
                    _conexion!.CloseAsync();
                    _conexion.Dispose();
                }
            }
        }
    }
}