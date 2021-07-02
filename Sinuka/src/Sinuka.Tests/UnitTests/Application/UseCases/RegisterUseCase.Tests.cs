using System;
using System.Threading.Tasks;
using NUnit.Framework;
using Moq;
using Sinuka.Application.Interfaces;
using Sinuka.Application.UseCases.Register;
using Sinuka.Core.Interfaces.Repositories;
using Sinuka.Core.Interfaces.Factories;
using Sinuka.Core.Models;

namespace Sinuka.Tests.UnitTests.Application
{
    [TestFixture, Category("Unit")]
    public class RegisterUseCaseTests
    {
        private RegisterUseCase _sut;
        private Mock<IUserRepository> _mockUserRepo;
        private Mock<IUserFactory> _mockUserFact;
        private Mock<IUnitOfWork> _mockUnitOfWork;
        private Mock<IRegisterPresenter> _mockPresenter;

        [SetUp]
        public void Setup()
        {
            this._mockUserRepo = new Mock<IUserRepository>();
            this._mockUserFact = new Mock<IUserFactory>();
            this._mockUnitOfWork = new Mock<IUnitOfWork>();
            this._mockPresenter = new Mock<IRegisterPresenter>();
            
            this._sut = new RegisterUseCase(
                this._mockUserRepo.Object,
                this._mockUserFact.Object,
                this._mockUnitOfWork.Object 
            );

            this._sut.SetPresenter(this._mockPresenter.Object);

            this._mockUnitOfWork.Setup(x => x.Save()).ReturnsAsync(0);
        }

        [Test]
        public async Task ShouldCallUserNameIsTaken_IfUsernameTaken()
        {
            // Arrange
            this._mockUserRepo.Setup(x => x.FindUserByUsername(It.IsAny<string>())).ReturnsAsync(new User("", "", ""));
            var input = new RegisterInput();

            // Act
            await this._sut.Run(input);

            // Assert
            this._mockPresenter.Verify(x => x.UsernameIsTaken());
        }

        [Test]
        public async Task ShouldCallEmailIsTaken_IfEmailTaken()
        {
            // Arrange
            this._mockUserRepo.Setup(x => x.FindUserByUsername(It.IsAny<string>())).ReturnsAsync(null as User);
            this._mockUserRepo.Setup(x => x.FindUserByEmail(It.IsAny<string>())).ReturnsAsync(new User("", "", ""));
            var input = new RegisterInput();

            // Act
            await this._sut.Run(input);

            // Assert
            this._mockPresenter.Verify(x => x.EmailIsTaken());
        }

        [Test]
        public async Task ShouldCallCreated_IfAccountCreated()
        {
            // Arrange
            this._mockUserRepo.Setup(x => x.FindUserByUsername(It.IsAny<string>())).ReturnsAsync(null as User);
            this._mockUserRepo.Setup(x => x.FindUserByEmail(It.IsAny<string>())).ReturnsAsync(null as User);
            var input = new RegisterInput();

            // Act
            await this._sut.Run(input);

            // Assert
            this._mockPresenter.Verify(x => x.UserCreated());
        }
    }
}
