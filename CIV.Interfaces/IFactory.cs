using System;
namespace CIV.Interfaces
{
    public interface IFactory<T, CTX>
    {
        T Create(CTX context);
    }
}
