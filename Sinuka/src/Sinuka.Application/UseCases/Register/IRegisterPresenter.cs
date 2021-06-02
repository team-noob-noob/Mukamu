namespace Sinuka.Application.UseCases.Register
{
    public interface IRegisterPresenter
    {
        void UsernameIsTaken();
        void EmailIsTaken();
        void UserCreated();
    }
}
