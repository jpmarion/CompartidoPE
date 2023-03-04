using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompartidoPE.Interface
{
    public interface IResponse<T>
    {
        public IList<T> Data { get; set; }
        public IError Error { get; set; }
    }
}
