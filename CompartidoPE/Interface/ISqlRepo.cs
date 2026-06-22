using System.Data.Common;

namespace CompartidoPE.Interface
{
    public interface ISqlRepo
    {
        public void BeginTransaction();
        public void CommitTransaction();
        public void RollbackTransaction();
        public Task ConexionOpen();
        public DbConnection ObtenerConexion();
    }
}