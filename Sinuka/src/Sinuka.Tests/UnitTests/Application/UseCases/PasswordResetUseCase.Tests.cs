using System;
using System.Threading.Tasks;
using NUnit.Framework;
using Moq;
using Sinuka.Application.Interfaces;
using Sinuka.Application.UseCases.PasswordReset;
using Sinuka.Core.Interfaces.Repositories;
using Sinuka.Core.Interfaces.Services;
using Sinuka.Core.Models;

namespace Sinuka.Tests.UnitTests.Application.UseCases
{
    [TestFixture, Category("Unit")]
    public class PasswordResetUseCaseTests
    {
        private IPasswordResetUseCase _sut;
        private Mock<IPasswordResetRepository> _mockPasswordResetRepo;
        private Mock<IHashingService> _mockHashingService;
        private Mock<IUnitOfWork> _mockUnitOfWork;
        private Mock<IPasswordResetPresenter> _mockPresenter;

        [SetUp]
        public void Setup()
        {
            this._mockUnitOfWork = new Mock<IUnitOfWork>();
            this._mockHashingService = new Mock<IHashingService>();
            this._mockPasswordResetRepo = new Mock<IPasswordResetRepository>();
            this._mockPresenter = new Mock<IPasswordResetPresenter>();

            this._sut = new PasswordResetUseCase(
                this._mockPasswordResetRepo.Object,
                this._mockHashingService.Object,
                this._mockUnitOfWork.Object
            );

            this._sut.SetPresenter(this._mockPresenter.Object);
        }

        [Test]
        public async Task PasswordResetUseCaseShouldCallInvalidResetToken_IfInvalidToken()
        {
            // Arrange
            this._mockPasswordResetRepo
                .Setup(x => x.FindPasswordResetByResetToken(It.IsAny<string>()))
                .ReturnsAsync(null as PasswordReset);
            var input = new PasswordResetInput();

            // Act
            await this._sut.Run(input);

            // Assert
            this._mockPresenter.Verify(x => x.InvalidResetToken());
        }

        [Test]
        public async Task PasswordResetUseCaseShouldCallInvalidResetToken_IfExpiredToken()
        {
            // Arrange
            this._mockPasswordResetRepo
                .Setup(x => x.FindPasswordResetByResetToken(It.IsAny<string>()))
                .ReturnsAsync(new PasswordReset(new User("", "", new EmailAddress("", "")), "", DateTime.Now - new TimeSpan(1, 0, 0)));
            var input = new PasswordResetInput();

            // Act
            await this._sut.Run(input);

            // Assert
            this._mockPresenter.Verify(x => x.InvalidResetToken());
        }

        [Test]
        public async Task PasswordResetUseCaseShouldCallDone_IfValidToken()
        {
            // Arrange
            this._mockPasswordResetRepo
                .Setup(x => x.FindPasswordResetByResetToken(It.IsAny<string>()))
                .ReturnsAsync(new PasswordReset(new User("", "", new EmailAddress("", "")), "", DateTime.Now + new TimeSpan(1, 0, 0)));
            var input = new PasswordResetInput();

            // Act
            await this._sut.Run(input);

            // Assert
            this._mockPresenter.Verify(x => x.Done());
        }
    }
}
