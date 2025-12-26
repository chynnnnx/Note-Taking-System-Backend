using FirstAngular.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FirstAngular.Domain.UnitTests
{
    public class RefreshTokenTests
    {
        [Fact]
        public void IsActive_ReturnsTrue_WhenNotRevokedAndNotExpired()
        {
            var token = new RefreshToken
            {
                Expiration = DateTime.UtcNow.AddMinutes(10),
                IsRevoked = false
            };

            Assert.True(token.IsActive);
        }

        [Fact]
        public void IsActive_ReturnsFalse_WhenExpired()
        {
            var token = new RefreshToken
            {
                Expiration = DateTime.UtcNow.AddMinutes(-1),
                IsRevoked = false
            };

            Assert.False(token.IsActive);
        }

        [Fact]
        public void IsActive_ReturnsFalse_WhenRevoked()
        {
            var token = new RefreshToken
            {
                Expiration = DateTime.UtcNow.AddMinutes(10),
                IsRevoked = true
            };

            Assert.False(token.IsActive);
        }
    }

}
