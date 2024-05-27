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
            await _fixture.Context.SaveChangesAsync();

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
            var result = await _fixture.Context.Users.FirstOrDefaultAsync(u => u.Username == "testuser");

            // Assert
            Assert.NotNull(result);
            Assert.Equal("testuser", result.Username);
        }

        [Fact]
        public async Task UpdateUserAsync_ShouldModifyUser()
        {
            // Arrange
            var user = await _fixture.Context.Users.FirstOrDefaultAsync(u => u.Username == "testuser");
            user.Name = "UpdatedName";
            user.Surname = "UpdatedSurname";

            // Act
            _fixture.Context.Users.Update(user);
            await _fixture.Context.SaveChangesAsync();

            // Assert
            var result = await _fixture.Context.Users.FirstOrDefaultAsync(u => u.Username == "testuser");
            Assert.NotNull(result);
            Assert.Equal("UpdatedName", result.Name);
            Assert.Equal("UpdatedSurname", result.Surname);
        }

        [Fact]
        public async Task DeleteUserAsync_ShouldRemoveUser()
        {
            // Arrange
            var user = await _fixture.Context.Users.FirstOrDefaultAsync(u => u.Username == "testuser");

            // Act
            _fixture.Context.Users.Remove(user);
            await _fixture.Context.SaveChangesAsync();

            // Assert
            var result = await _fixture.Context.Users.FirstOrDefaultAsync(u => u.Username == "testuser");
            Assert.Null(result);
        }
    }
}
