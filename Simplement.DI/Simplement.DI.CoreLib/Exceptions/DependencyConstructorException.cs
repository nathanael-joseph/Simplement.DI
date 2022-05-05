namespace Simplement.DI.CoreLib.Exceptions
{
    public class DependencyConstructorException : InvalidOperationException
    {
        public DependencyConstructorException(Type t)
            : base($"No public construcot for the dependency {t.Name} could be found.")
        { }
    }
}
