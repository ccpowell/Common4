namespace DRCOG.Common.Interfaces.ActiveDirectorySupport
{
    public interface IDomainSearchConfig
    {
        string Path { get; }
        string UserName { get; }
        string Password { get; }
        bool HasCredentials { get; }
    }
}