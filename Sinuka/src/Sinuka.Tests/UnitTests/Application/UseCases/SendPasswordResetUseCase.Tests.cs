using System;
using System.Threading.Tasks;
using NUnit.Framework;
using Moq;
using Sinuka.Application.Interfaces;
using Sinuka.Application.UseCases.SendPasswordReset;
using Sinuka.Core.Interfaces.Repositories;
using Sinuka.Core.Interfaces.Services;
using Sinuka.Core.Models;

namespace Sinuka.Tests.UnitTests.Application.UseCases
{
    [TestFixture, Category("Unit")]
    public class SendPasswordResetUseCaseTests
    {
        private ISendPasswordResetUseCase _sut;
        private Mock<IUserRepository> _mockUserRepo;
        private Mock<IEmailService> _mockEmailService;
        private Mock<ISendPasswordResetPresenter> _mockPresenter;

        [SetUp]
        public void Setup()
        {
            this._mockPresenter = new Mock<ISendPasswordResetPresenter>();
            this._mockEmailService = new Mock<IEmailService>();
            this._mockUserRepo = new Mock<IUserRepository>();

            this._sut = new SendPasswordResetUseCase(
                this._mockUserRepo.Object,
                this._mockEmailService.Object
            );

            this._sut.SetPresenter(this._mockPresenter.Object);
        }

        [Test]
        public async Task SendPasswordResetUseCaseShouldCallDone_IfValidEmail()
        {
            // Arrange
            this._mockUserRepo
                .Setup(x => x.FindUserByEmail(It.IsAny<string>()))
                .ReturnsAsync(new User("", "", new EmailAddress("", "")));
            var input = new SendPasswordResetInput();

            // Act
            await this._sut.Run(input);

            // Assert
            this._mockPresenter.Verify(x => x.Done());
        }

        [Test]
        public async Task SendPasswordResetUseCaseShouldCallInvalidEmail_IfInvalidEmail()
        {
            // Arrange
            this._mockUserRepo
                .Setup(x => x.FindUserByEmail(It.IsAny<string>()))
                .ReturnsAsync(null as User);
            var input = new SendPasswordResetInput();

            // Act
            await this._sut.Run(input);

            // Assert
            this._mockPresenter.Verify(x => x.InvalidEmail());
        }
    }
}
