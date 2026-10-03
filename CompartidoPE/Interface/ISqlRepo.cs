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

        // Cerrar o desechar una conexión la devuelve al pool del proveedor.
        public void CerrarConexion() => ObtenerConexion()?.Dispose();
    }
}
