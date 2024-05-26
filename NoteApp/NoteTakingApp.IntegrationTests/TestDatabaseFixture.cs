using Microsoft.EntityFrameworkCore;
using NoteTakingApp;
using System;

namespace NoteTakingApp.IntegrationTests
{
    public class TestDatabaseFixture : IDisposable
    {
        public NoteDbContext Context { get; private set; }

        public TestDatabaseFixture()
        {
            var options = new DbContextOptionsBuilder<NoteDbContext>()
                .UseInMemoryDatabase(databaseName: "TestDatabase")
                .Options;

            Context = new NoteDbContext(options);

            SeedDatabase();
        }

        private void SeedDatabase()
        {
            // Add initial test data if needed
            Context.Users.Add(new User("testuser", "Test", "User"));
            Context.Notes.Add(new Note
            {
                Author = "testuser",
                Title = "Test Note",
                Content = "This is a test note.",
                Privacy = PrivacySetting.Private,
                Sharing = SharingSetting.Viewing
            });
            Context.SaveChanges();
        }

        public void Dispose()
        {
            Context.Database.EnsureDeleted();
            Context.Dispose();
        }
    }
}