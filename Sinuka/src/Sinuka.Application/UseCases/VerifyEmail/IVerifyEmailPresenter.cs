namespace Sinuka.Application.UseCases.VerifyEmail
{
    public interface IVerifyEmailPresenter
    {
        void Redirect(string uriRedirect);
        void Done();
        void InvalidVerifyString();
    }
}
