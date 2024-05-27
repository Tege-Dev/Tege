using NoteTakingApp.IntegrationTest;
using NoteTakingApp.IntegrationTests;
using System.Threading.Tasks;
using Xunit;

namespace NoteTakingApp.IntegrationTest
{
    public class NoteDbContextIntegrationTests : IClassFixture<TestDatabaseFixture>
    {
        private readonly TestDatabaseFixture _fixture;

        public NoteDbContextIntegrationTests(TestDatabaseFixture fixture)
        {
            _fixture = fixture;
        }

        [Fact]
        public async Task GetAllNotesAsync_ShouldReturnAllNotes()
        {
            // Act
            var result = await _fixture.Context.GetAllNotesAsync();

            // Assert
            Assert.NotEmpty(result);
            Assert.Contains(result, note => note.Title == "Test Note");
        }

        [Fact]
        public async Task GetAllUserNotesAsync_ShouldReturnAllUserNotes()
        {
            // Arrange
            _fixture.Context.UserNotes.Add(new UserNote { UserName = "testuser", PublicNoteNumber = 1 });
            await _fixture.Context.SaveChangesAsync();

            // Act
            var result = await _fixture.Context.GetAllUserNotesAsync();

            // Assert
            Assert.NotEmpty(result);
            Assert.Contains(result, userNote => userNote.UserName == "testuser");
        }
    }
}