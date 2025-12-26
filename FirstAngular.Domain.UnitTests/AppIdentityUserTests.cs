using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FirstAngular.Domain.Entities;

namespace FirstAngular.Domain.UnitTests
{
    public class AppIdentityUserTests
    {
        [Fact]
        public void Properties_HaveDefaultValues()
        {
            // Arrange
            var user = new AppIdentityUser();

            // Assert
            Assert.Equal(string.Empty, user.FirstName);
            Assert.Equal(string.Empty, user.MiddleInitial);
            Assert.Equal(string.Empty, user.LastName);
        }

        [Fact]
        public void CanSetProperties()
        {
            // Arrange
            var user = new AppIdentityUser
            {
                FirstName = "Juan",
                MiddleInitial = "D",
                LastName = "Santos"
            };

            // Assert
            Assert.Equal("Juan", user.FirstName);
            Assert.Equal("D", user.MiddleInitial);
            Assert.Equal("Santos", user.LastName);
        }
    }

}
