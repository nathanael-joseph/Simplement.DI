using Simplement.DI.CoreLib.Enums;

namespace Simplement.DI.CoreLib.Dependencies
{
    internal class SingletonDependency : DependencyBase, IDisposable
    {
        private readonly Lazy<object?> _instance;
        private bool _disposed;

        internal override DependencyLifetime Lifetime => DependencyLifetime.SINGLTON;
        
        internal override object? Instance 
        { 
            get => _instance.Value; 
        }

        internal SingletonDependency(Func<object?> constructor) 
            : base(constructor)
        {
            _instance = new Lazy<object?>(constructor);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    if (_instance.IsValueCreated) 
                    {
                        IDisposable? d = _instance.Value as IDisposable;
                        d?.Dispose();
                    }
                }

                _disposed = true;
            }
        }

        public void Dispose()
        {
            Dispose(disposing: true);
        }
    }
}