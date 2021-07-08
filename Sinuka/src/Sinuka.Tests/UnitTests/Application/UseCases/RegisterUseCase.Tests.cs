using System;
using System.Threading.Tasks;
using NUnit.Framework;
using Moq;
using Sinuka.Application.Interfaces;
using Sinuka.Application.UseCases.Register;
using Sinuka.Core.Interfaces.Repositories;
using Sinuka.Core.Interfaces.Factories;
using Sinuka.Core.Interfaces.Services;
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
        private Mock<IEmailService> _mockEmailService;
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
                this._mockEmailService.Object,
                this._mockUnitOfWork.Object 
            );

            this._sut.SetPresenter(this._mockPresenter.Object);

            this._mockUnitOfWork.Setup(x => x.Save()).ReturnsAsync(0);
        }

        [Test]
        public async Task RegisterUseCaseShouldCallUserNameIsTaken_IfUsernameTaken()
        {
            // Arrange
            this._mockUserRepo.Setup(x => x.FindUserByUsername(It.IsAny<string>())).ReturnsAsync(new User("", "", new EmailAddress("", "")));
            var input = new RegisterInput();

            // Act
            await this._sut.Run(input);

            // Assert
            this._mockPresenter.Verify(x => x.UsernameIsTaken());
        }

        [Test]
        public async Task RegisterUseCaseShouldCallEmailIsTaken_IfEmailTaken()
        {
            // Arrange
            this._mockUserRepo.Setup(x => x.FindUserByUsername(It.IsAny<string>())).ReturnsAsync(null as User);
            this._mockUserRepo.Setup(x => x.FindUserByEmail(It.IsAny<string>())).ReturnsAsync(new User("", "", new EmailAddress("", "")));
            var input = new RegisterInput();

            // Act
            await this._sut.Run(input);

            // Assert
            this._mockPresenter.Verify(x => x.EmailIsTaken());
        }

        [Test]
        public async Task RegisterUseCaseShouldCallCreated_IfAccountCreated()
        {
            // Arrange
            this._mockUserRepo.Setup(x => x.FindUserByUsername(It.IsAny<string>())).ReturnsAsync(null as User);
            this._mockUserRepo.Setup(x => x.FindUserByEmail(It.IsAny<string>())).ReturnsAsync(null as User);
            var input = new RegisterInput();

            // Act
            await this._sut.Run(input);

            // Assert
            this._mockPresenter.Verify(x => x.UserCreated());
            this._mockEmailService.Verify(x => x.SendEmail(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>()));
        }
    }
}
