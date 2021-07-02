using System;
using System.Threading.Tasks;
using NUnit.Framework;
using Moq;
using Sinuka.Application.Interfaces;
using Sinuka.Application.UseCases.Login;
using Sinuka.Core.Interfaces.Repositories;
using Sinuka.Core.Interfaces.Factories;
using Sinuka.Core.Models;

namespace Sinuka.Tests.UnitTests.Application.UseCases
{
    [TestFixture]
    [Category("Unit")]
    public class LoginUseCaseTests
    {
        private LoginUseCase _sut;
        private Mock<ILoginPresenter> _mockPresenter;
        private Mock<IUserRepository> _mockUserRepo;
        private Mock<IClientRepository> _mockClientRepo;
        private Mock<ISessionRepository> _mockSessionRepo;
        private Mock<ISessionFactory> _mockSessionFactory;
        private Mock<IRefreshTokenFactory> _mockRefreshTokenFactory;
        private Mock<IUnitOfWork> _mockUnitOfWork;

        [SetUp]
        public void Setup()
        {
            this._mockUserRepo = new Mock<IUserRepository>();
            this._mockClientRepo = new Mock<IClientRepository>();
            this._mockSessionRepo = new Mock<ISessionRepository>();
            this._mockSessionFactory = new Mock<ISessionFactory>();
            this._mockRefreshTokenFactory = new Mock<IRefreshTokenFactory>();
            this._mockUnitOfWork = new Mock<IUnitOfWork>();
            this._mockPresenter = new Mock<ILoginPresenter>();

            this._sut = new LoginUseCase(
                this._mockUserRepo.Object,
                this._mockSessionRepo.Object,
                this._mockSessionFactory.Object,
                this._mockUnitOfWork.Object,
                this._mockClientRepo.Object,
                this._mockRefreshTokenFactory.Object
            );

            this._sut.SetPresenter(this._mockPresenter.Object);

            this._mockUnitOfWork.Setup(x => x.Save()).ReturnsAsync(0);
        }

        [Test]
        public async Task ShouldCallIncorrectClient_IfClientInfoIsInvalid()
        {
            // Arrange
            this._mockClientRepo.Setup(x => x.VerifyClientData(It.IsAny<Guid>(), It.IsAny<string>(), It.IsAny<string>())).Returns(null as Client);
            var input = new LoginInput();

            // Act
            await this._sut.Run(input);

            // Assert
            this._mockPresenter.Verify(x => x.IncorrectClient());
        }

        [Test]
        public async Task ShouldCallIncorrectCredentials_IfUserCredentialsAreInvalid()
        {
            // Arrange
            this._mockClientRepo
                .Setup(x => x.VerifyClientData(It.IsAny<Guid>(), It.IsAny<string>(), It.IsAny<string>()))
                .Returns(new Client("", "", "")); 
            this._mockUserRepo
                .Setup(x => x.CheckCredentials(It.IsAny<string>(), It.IsAny<string>()))
                .ReturnsAsync(null as User);
            var input = new LoginInput();

            // Act
            await this._sut.Run(input);

            // Assert
            this._mockPresenter.Verify(x => x.IncorrectCredentials());
        }

        [Test]
        public async Task ShouldCallSessionCreated_IfValidUserAndNullPostLoginRedirect()
        {
            // Arrange
            this._mockClientRepo
                .Setup(x => x.VerifyClientData(It.IsAny<Guid>(), It.IsAny<string>(), It.IsAny<string>()))
                .Returns(new Client("", "", "")); 
            this._mockUserRepo
                .Setup(x => x.CheckCredentials(It.IsAny<string>(), It.IsAny<string>()))
                .ReturnsAsync(new User("", "", ""));
            var input = new LoginInput();

            // Act
            await this._sut.Run(input);
            
            // Assert
            this._mockPresenter
                .Verify(x => x.SessionCreated(It.IsAny<Session>(), It.IsAny<bool>()));
        }

        [Test]
        public async Task ShouldCallRedirectCreatedSession_IfValidUserAndPostLoginRedirect()
        {
            // Arrange
            this._mockClientRepo
                .Setup(x => x.VerifyClientData(It.IsAny<Guid>(), It.IsAny<string>(), It.IsAny<string>()))
                .Returns(new Client("", "", "", "", "")); 
            this._mockUserRepo
                .Setup(x => x.CheckCredentials(It.IsAny<string>(), It.IsAny<string>()))
                .ReturnsAsync(new User("", "", ""));
            var input = new LoginInput();

            // Act
            await this._sut.Run(input);

            // Assert
            this._mockPresenter
                .Verify(x => x.RedirectCreatedSession(It.IsAny<Session>(), It.IsAny<string>(), It.IsAny<bool>()));
        }
    }
}
