using System.Threading.Tasks;
using NUnit.Framework;
using Moq;
using Sinuka.Application.Interfaces;
using Sinuka.Application.UseCases.Logout;
using Sinuka.Core.Interfaces.Repositories;
using Sinuka.Core.Models;

namespace Sinuka.Tests.UnitTests.Application
{
    [TestFixture, Category("Unit")]
    public class LogoutUseCaseTests
    {
        private Mock<ILogoutPresenter> _mockPresenter;
        private Mock<ISessionRepository> _mockSessionRepo;
        private Mock<IUnitOfWork> _mockUnitOfWork;
        private LogoutUseCase _sut;

        [SetUp]
        public void Setup()
        {
            this._mockPresenter = new Mock<ILogoutPresenter>();
            this._mockSessionRepo = new Mock<ISessionRepository>();
            this._mockUnitOfWork = new Mock<IUnitOfWork>();

            this._sut = new LogoutUseCase(
                this._mockSessionRepo.Object,
                this._mockUnitOfWork.Object
            );
            
            this._sut.SetPresenter(this._mockPresenter.Object);

            this._mockUnitOfWork.Setup(x => x.Save()).ReturnsAsync(0);
        }

        [Test]
        public async Task LogoutUseCaseShouldCallRedirect_IfClientIsNotNull()
        {
            // Arrange
            this._mockSessionRepo
                .Setup(x => x.FindSessionBySessionToken(It.IsAny<string>()))
                .ReturnsAsync(new Session() { Client = new Client("", "", "", "", "") });
            var input = new LogoutInput();

            // Act
            await this._sut.Run(input);

            // Assert
            this._mockPresenter.Verify(x => x.Redirect(It.IsAny<string>()));
        }

        [Test]
        public async Task LogoutUseCaseShouldCallDone_IfClientIsNull()
        {
             // Arrange
            this._mockSessionRepo
                .Setup(x => x.FindSessionBySessionToken(It.IsAny<string>()))
                .ReturnsAsync(new Session() { Client = new Client("", "", "") });
            var input = new LogoutInput();

            // Act
            await this._sut.Run(input);

            // Assert
            this._mockPresenter.Verify(x => x.Done());
        }
    }
}
