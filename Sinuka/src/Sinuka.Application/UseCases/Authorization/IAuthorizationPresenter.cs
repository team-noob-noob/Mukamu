namespace Sinuka.Application.UseCases.Authorization
{
    public interface IAuthorizationPresenter
    {
        void InvalidToken();
        void ValidToken();
    }
}
