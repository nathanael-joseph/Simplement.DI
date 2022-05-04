namespace Simplement.DI.CoreLib
{
    public class Scope
    {
        private Dictionary<Type, object> _instanceLocks;
        internal Scope(IEnumerable<Type> _scopedTypes)
        {
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
    }

}