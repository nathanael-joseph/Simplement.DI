namespace Simplement.DI.CoreLib.Exceptions
{
    public class UknownDependancyException : InvalidOperationException
    {
        public UknownDependancyException(Type t)
            : base($"Unknown dependancy {t.Name} was requested. Ensrue the dependancy was registered correctly.")
        { }
    }
}
