namespace Simplement.DI.CoreLib
{
    public class Scope : IDisposable
    {
        private bool _disposed = false; 
        private Container _container;
        private Dictionary<Type, object> _instanceLocks;

        internal Scope(Container container, IEnumerable<Type> _scopedTypes)
        {
            _container = container;
            _instanceLocks = new Dictionary<Type, object>();
            foreach (Type type in _scopedTypes)
            {
                _instanceLocks[type] = new object();
            }
        }

        internal object GetLock(Type type)
        {
           return _instanceLocks[type];
        }

        public void Dispose()
        {
            Dispose(true);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (_disposed)
            {
                return;
            }

            if (disposing)
            {
                _container.RemoveScope(this);
            }
            
            _disposed = true;
        }
    }

}