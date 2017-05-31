namespace CIV.Interfaces
{
    
    public class Proxy<T, CTX> where T: class where CTX : class 
    {
        protected IFactory<T, CTX> factory;
        protected CTX context;

 		T _real;
        public T Real 
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
