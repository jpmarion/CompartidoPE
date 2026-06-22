using CompartidoPE.Interface;
using CompartidoPE.Modelo;

namespace CompartidoPE.Factory
{
    public static class FError
    {
        public static IError Build() => new Error();
    }
}