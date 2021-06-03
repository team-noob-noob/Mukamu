namespace Sinuka.Application.UseCases.Login
{
    public interface ILoginPresenter
    {
        void IncorrectCredentials();
        void SessionCreated(string session);
    }
}
