using System;
namespace CIV.Common
{
    public interface IFactory<T, CTX>
    {
        T Create(CTX context);
    }
}
