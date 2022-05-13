namespace Simplement.DI.CoreLib.Exceptions
{
    public class UknownDependencyException : InvalidOperationException
    {
        public UknownDependencyException(Type t)
            : base($"Unknown dependancy {t.Name} was requested. Ensrue the dependancy was registered correctly.")
        { }
    }
}
