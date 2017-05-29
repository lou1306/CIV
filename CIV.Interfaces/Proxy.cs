using System.Reflection;

namespace CIV.Interfaces
{
    
    public abstract class Proxy<T, CTX>
    {
        protected IFactory<T, CTX> factory;
        protected CTX context;

 		T _real;
        protected T Real 
        {
            get
            {
                if (_real == null)
                    _real = factory.Create(context);
				return _real;
			}
        }

        public Proxy(IFactory<T, CTX> factory, CTX context)
        {
            this.factory = factory;
            this.context = context;
        }
    }
}
