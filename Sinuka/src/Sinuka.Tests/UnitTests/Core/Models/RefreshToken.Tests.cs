using System;
using FluentAssertions;
using NUnit.Framework;
using Sinuka.Core.Models;

namespace Sinuka.Tests.UnitTests.Core.Models
{
    [TestFixture, Category("Unit")]
    public class RefreshTokenTests
    {
        private RefreshToken _sut;

        [SetUp]
        public void Setup()
        {
            this._sut = new RefreshToken("TEST", DateTime.Now);
        }

        [Test]
        public void IsExpiredShouldReturnTrue_IfExpiresAtLessThanDateNow()
        {
            // Arrange
            this._sut.ExpiresAt = DateTime.Now - new TimeSpan(1, 0, 0);

            // Act
            var result = this._sut.IsExpired();

            // Assert
            result.Should().BeTrue();
        }

        [Test]
        public void IsExpiredShouldReturnFalse_IfExpiresAtGreaterThanDateNow()
        {
            // Arrange
            this._sut.ExpiresAt = DateTime.Now + new TimeSpan(1, 0, 0);

            // Act
            var result = this._sut.IsExpired();

            // Assert
            result.Should().BeFalse();
        }
    }
}
