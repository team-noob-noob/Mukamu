using System;
using System.Threading.Tasks;
using NUnit.Framework;
using Moq;
using Sinuka.Application.Interfaces;
using Sinuka.Application.UseCases.VerifyEmail;
using Sinuka.Core.Interfaces.Repositories;
using Sinuka.Core.Interfaces.Factories;
using Sinuka.Core.Models;

namespace Sinuka.Tests.UnitTests.Application.UseCases
{
    [TestFixture, Category("Unit")]
    public sealed class VerifyEmailUseCaseTests
    {
        private IVerifyEmailUseCase _sut;
        private Mock<IEmailAddressRepository> _mockEmailRepo;
        private Mock<IUnitOfWork> _mockUnitOfWork;
        private Mock<IVerifyEmailPresenter> _mockPresenter;

        [SetUp]
        public void Setup()
        {
            this._mockEmailRepo = new Mock<IEmailAddressRepository>();
            this._mockUnitOfWork = new Mock<IUnitOfWork>();
            this._mockPresenter = new Mock<IVerifyEmailPresenter>();

            this._sut = new VerifyEmailUseCase(
                this._mockEmailRepo.Object,
                this._mockUnitOfWork.Object
            );

            this._sut.SetPresenter(this._mockPresenter.Object);

            this._mockUnitOfWork.Setup(x => x.Save()).ReturnsAsync(0);
        }

        [Test]
        public async Task VerifyEmailUseCaseShouldCallDone_IfVerificationIsValid()
        {
            // Arrange
            this._mockEmailRepo
                .Setup(x => x.GetEmailAddressByVerificationString(It.IsAny<string>()))
                .ReturnsAsync(new EmailAddress("", ""));
            var input = new VerifyEmailInput();

            // Act
            await this._sut.Run(input);

            // Assert
            this._mockPresenter.Verify(x => x.Done());
        }

        [Test]
        public async Task VerifyEmailUseCaseShouldCallInvalidVerifyString_IfVerificationIsInvalid()
        {
            // Arrange
            this._mockEmailRepo
                .Setup(x => x.GetEmailAddressByVerificationString(It.IsAny<string>()))
                .ReturnsAsync(null as EmailAddress);
            var input = new VerifyEmailInput();

            // Act
            await this._sut.Run(input);

            // Assert
            this._mockPresenter.Verify(x => x.InvalidVerifyString());
        }
    }
}
