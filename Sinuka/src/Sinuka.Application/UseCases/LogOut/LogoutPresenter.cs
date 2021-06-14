namespace Sinuka.Application.UseCases.Logout
{
    public interface ILogoutPresenter
    {
        void Done();
        void Redirect(string url);
    }
}
