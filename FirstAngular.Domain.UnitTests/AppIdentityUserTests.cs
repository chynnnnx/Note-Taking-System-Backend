using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FirstAngular.Domain.Entities;
  using Xunit;
 

namespace FirstAngular.Domain.UnitTests
{
    public class AppIdentityUserTests
    {
        [Fact]
        public void CanCreateUserWithFactory()
        {
             var user = AppIdentityUser.Create("Juan", "D", "Santos", "juan@example.com");

             Assert.Equal("Juan", user.FirstName);
            Assert.Equal("D", user.MiddleInitial);
            Assert.Equal("Santos", user.LastName);
            Assert.Equal("juan@example.com", user.Email);
        }

        [Fact]
        public void UpdateProfile_ChangesProperties_WhenDifferent()
        {
             var user = AppIdentityUser.Create("Juan", "D", "Santos", "juan@example.com");

             var changed = user.UpdateProfile("Jose", "M", "Garcia");

             Assert.True(changed);
            Assert.Equal("Jose", user.FirstName);
            Assert.Equal("M", user.MiddleInitial);
            Assert.Equal("Garcia", user.LastName);
        }

        [Fact]
        public void UpdateProfile_ReturnsFalse_WhenValuesAreSame()
        {
             var user = AppIdentityUser.Create("Juan", "D", "Santos", "juan@example.com");

             var changed = user.UpdateProfile("Juan", "D", "Santos");

             Assert.False(changed);
            Assert.Equal("Juan", user.FirstName);
            Assert.Equal("D", user.MiddleInitial);
            Assert.Equal("Santos", user.LastName);
        }
    }
}
