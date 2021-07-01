using System;
using System.Threading.Tasks;
using Moq;
using NUnit.Framework;
using Sinuka.Application.Interfaces;
using Sinuka.Application.UseCases.Authorization;
using Sinuka.Core.Interfaces.Repositories;
using Sinuka.Core.Models;

namespace Sinuka.Tests.UnitTests.Application.UseCases
{
    [TestFixture]
    [Category("Unit")]
    public class AuthorizationUseCaseTests
    {
        private AuthorizationUseCase _sut;
        private Mock<IUnitOfWork>  _mockedUnitOfWork;
        private Mock<ISessionRepository> _mockedSessionRepo;
        private Mock<IAuthorizationPresenter> _mockedPresenter;

        [SetUp]
        public void Setup()
        {
            this._mockedUnitOfWork = new Mock<IUnitOfWork>();
            this._mockedSessionRepo = new Mock<ISessionRepository>();
            this._mockedPresenter = new Mock<IAuthorizationPresenter>();
            this._sut = new AuthorizationUseCase(
                this._mockedUnitOfWork.Object,
                this._mockedSessionRepo.Object
            );

            this._sut.SetPresenter(this._mockedPresenter.Object);

            this._mockedUnitOfWork.Setup(x => x.Save()).ReturnsAsync(0);
        }

        [Test]
        public async Task ShouldCallInvalidToken_IfInputTokenIsInvalid()
        {
            // Arrange
            this._mockedSessionRepo.Setup(x => x.FindSessionBySessionToken(It.IsAny<string>())).ReturnsAsync(null as Session);
            var input = new AuthorizationInput()
            {
                Token = "Test",
            };

            // Act
            await this._sut.Run(input);

            // Assert
            this._mockedPresenter.Verify(x => x.InvalidToken());
        }

        [Test]
        public async Task ShouldCallRemoveToken_IfInputTokenIsInvalidAndHasRefreshToken()
        {
            // Arrange
            this._mockedSessionRepo
                .Setup(x => x.FindSessionBySessionToken(It.IsAny<string>()))
                .ReturnsAsync(new Session() { SessionToken = new SessionToken("TEst", DateTime.Now - new TimeSpan(1, 0, 0)) });
            var input = new AuthorizationInput()
            {
                Token = "Test",
            };

            // Act
            await this._sut.Run(input);

            // Assert
            this._mockedSessionRepo.Verify(x => x.RemoveSession(It.IsAny<Session>()));
            this._mockedPresenter.Verify(x => x.InvalidToken());
        }

        [Test]
        public async Task ShouldCallValidToken_IfInputTokenIsValid()
        {
            // Arrange
            this._mockedSessionRepo
                .Setup(x => x.FindSessionBySessionToken(It.IsAny<string>()))
                .ReturnsAsync(new Session() { SessionToken = new SessionToken("TEst", DateTime.Now + new TimeSpan(1, 0, 0)) });
            var input = new AuthorizationInput()
            {
                Token = "Test",
            };

            // Act
            await this._sut.Run(input);

            // Assert
            this._mockedPresenter.Verify(x => x.ValidToken());
            
        }
    }
}
