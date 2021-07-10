namespace Sinuka.Core.Interfaces.Services
{
    public interface IHashingService
    {
        string Hash(string plaintext);
        bool Compare(string plaintext, string hash);
    }
}
