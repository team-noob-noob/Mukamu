namespace Sinuka.Application.UseCases.PasswordReset
{
    public interface IPasswordResetPresenter
    {
        void Done();
        void InvalidResetToken();
    }
}
