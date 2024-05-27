using NoteTakingApp.IntegrationTest;
using NoteTakingApp.IntegrationTests;
using System.Threading.Tasks;
using Xunit;
using Microsoft.EntityFrameworkCore;

namespace NoteTakingApp.IntegrationTest
{
    public class UserDbContextIntegrationTests : IClassFixture<TestDatabaseFixture>
    {
        private readonly TestDatabaseFixture _fixture;

        public UserDbContextIntegrationTests(TestDatabaseFixture fixture)
        {
            _fixture = fixture;
        }

        [Fact]
        public async Task CreateUserAsync_ShouldAddNewUser()
        {
            // Arrange
            var newUser = new User { Username = "newuser", Name = "New", Surname = "User" };

            // Act
            _fixture.Context.Users.Add(newUser);
            _fixture.Context.SaveChanges();

            // Assert
            var result = await _fixture.Context.Users.FirstOrDefaultAsync(u => u.Username == "newuser");
            Assert.NotNull(result);
            Assert.Equal("newuser", result.Username);
            Assert.Equal("New", result.Name);
            Assert.Equal("User", result.Surname);
        }

        [Fact]
        public async Task GetUserAsync_ShouldReturnUser()
        {
            // Act
            var result = _fixture.Context.Users.FirstOrDefault(u => u.Username == "newuser");

            // Assert
            Assert.NotNull(result);
            Assert.Equal("newuser", result.Username);
        }

        [Fact]
        public async Task DeleteUserAsync_ShouldRemoveUser()
        {
            // Arrange
            var user = await _fixture.Context.Users.FirstOrDefaultAsync(u => u.Username == "newuser");

            // Act
            _fixture.Context.Users.Remove(user);
            _fixture.Context.SaveChanges();

            // Assert
            var result = await _fixture.Context.Users.FirstOrDefaultAsync(u => u.Username == "newuser");
            Assert.Null(result);
        }
    }
}
