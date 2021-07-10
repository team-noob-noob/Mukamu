namespace Sinuka.Application.UseCases.SendPasswordReset
{
    public interface ISendPasswordResetPresenter
    {
        void Done();
        void InvalidEmail();
    }
}
