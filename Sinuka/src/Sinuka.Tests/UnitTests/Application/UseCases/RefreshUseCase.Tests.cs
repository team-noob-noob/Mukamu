using System;
using System.Threading.Tasks;
using NUnit.Framework;
using Moq;
using Sinuka.Application.Interfaces;
using Sinuka.Application.UseCases.Refresh;
using Sinuka.Core.Interfaces.Repositories;
using Sinuka.Core.Interfaces.Factories;
using Sinuka.Core.Models;

namespace Sinuka.Tests.UnitTests.Application
{
    [TestFixture, Category("Unit")]
    public class RefreshUseCaseTests
    {
        private RefreshUseCase _sut;
        private Mock<ISessionRepository> _mockSessionRepo;
        private Mock<IRefreshTokenFactory> _mockRefreshTokenFact;
        private Mock<ISessionFactory> _mockSessionFact;
        private Mock<IUnitOfWork> _mockUnitOfWork;
        private Mock<IRefreshPresenter> _mockPresenter;

        [SetUp]
        public void Setup()
        {
            this._mockRefreshTokenFact = new Mock<IRefreshTokenFactory>();
            this._mockSessionFact = new Mock<ISessionFactory>();
            this._mockSessionRepo = new Mock<ISessionRepository>();
            this._mockUnitOfWork = new Mock<IUnitOfWork>();
            this._mockPresenter = new Mock<IRefreshPresenter>();

            this._sut = new RefreshUseCase(
                this._mockSessionRepo.Object,
                this._mockRefreshTokenFact.Object,
                this._mockSessionFact.Object,
                this._mockUnitOfWork.Object
            );

            this._sut.SetPresenter(this._mockPresenter.Object);

            this._mockUnitOfWork.Setup(x => x.Save()).ReturnsAsync(0);
        }

        [Test]
        public async Task RefreshUseCaseShouldCallDone_IfValidSession()
        {
            // Arrange
            this._mockSessionRepo
                .Setup(x => x.FindSessionByRefreshToken(It.IsAny<string>()))
                .ReturnsAsync(new Session(new User("", "", new EmailAddress("", "")), new SessionToken("TEST", DateTime.Now + new TimeSpan(2, 0, 0)), new Client("", "", "")) 
                { RefreshToken = new RefreshToken("TEST", DateTime.Now + new TimeSpan(1, 0, 0)) });
            this._mockRefreshTokenFact
                .Setup(x => x.CreateRefreshToken())
                .Returns(new RefreshToken("TEST2", DateTime.Now + new TimeSpan(2, 0, 0)));
            this._mockSessionFact
                .Setup(x => x.CreateSession(It.IsAny<User>(), It.IsAny<Client>()))
                .Returns(new Session(new User("", "", new EmailAddress("", "")), new SessionToken("TEST", DateTime.Now + new TimeSpan(2, 0, 0)), new Client("", "", "")));
            var input = new RefreshInput();

            // Act
            await this._sut.Run(input);

            // Assert
            this._mockPresenter.Verify(x => x.Done(It.IsAny<Session>()));
        }

        [Test]
        public async Task RefreshUseCaseShouldCallInvalidToken_IfInvalidSession()
        {
            // Arrange
            this._mockSessionRepo
                .Setup(x => x.FindSessionByRefreshToken(It.IsAny<string>()))
                .ReturnsAsync(null as Session);
            var input = new RefreshInput();

            // Act
            await this._sut.Run(input);

            // Assert
            this._mockPresenter.Verify(x => x.InvalidToken());
        }
    }
}
