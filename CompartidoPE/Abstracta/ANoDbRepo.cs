using System.Data.Common;

namespace CompartidoPE.Abstracta
{
    public abstract class ANoDbRepo
    {
        public void BeginTransaction() { }
        public void CommitTransaction() { }
        public Task ConexionOpen() => Task.CompletedTask;
        public DbConnection ObtenerConexion() => null!;
        public void RollbackTransaction() { }
    }
}