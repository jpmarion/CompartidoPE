using CompartidoPE.Interface;
using System.Collections;

namespace CompartidoPE.Abstracta
{
    public abstract class AEjecutarCU<T>(IRequest? request,
                                         IParameters<T> parameters,
                                         ISqlRepo sqlRepo)
    {
        protected abstract Task<IResponse<T>> Proceso();
        protected abstract Task<IError?> Validaciones();
        protected abstract Task Adaptador();

        protected Task ConexionOpen() => sqlRepo.ConexionOpen();
        protected void BeginTransaction() => sqlRepo.BeginTransaction();
        protected void CommitTransaction() => sqlRepo.CommitTransaction();
        protected void RollBackTransaction() => sqlRepo.RollbackTransaction();

        public async Task<IResponse<T>> Ejecutar()
        {
            try
            {
                await ConexionOpen();

                await Adaptador();

                parameters.Error = await Validaciones();
                if (parameters.Error != null)
                {
                    parameters.Response.Error = parameters.Error;
                    return parameters.Response;
                }

                BeginTransaction();
                parameters.Response = await Proceso();

                if (parameters.Response.Error == null) CommitTransaction();
                else RollBackTransaction();

                return parameters.Response;
            }
            catch (Exception ex)
            {
                RollBackTransaction();
                parameters.Error ??= parameters.Error;

                if (ex.Data.Count != 0)
                {
                    foreach (DictionaryEntry item in ex.Data)
                    {
                        parameters.Error!.NroError = item.Key?.ToString()!;
                        parameters.Error.MsgError = item.Value?.ToString()!;
                        parameters.Response.Error = parameters.Error;
                        break;
                    }
                }
                else
                {
                    parameters.Error!.NroError = ex.StackTrace!;
                    parameters.Error.MsgError = ex.Message;
                    parameters.Response.Error = parameters.Error;
                }

                return parameters.Response;
            }
        }
    }
}
