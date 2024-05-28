using Microsoft.EntityFrameworkCore;
using NoteTakingApp;
using System;

namespace NoteTakingApp.IntegrationTests
{
    public class TestDatabaseFixture : IDisposable
    {
        public NoteDbContext Context { get; private set; }
        private Note addedNote;
        private User addedUser;

        public TestDatabaseFixture()
        {
            // Provide the connection string for your production database
            string connectionString = "Server=tcp:tegeserver.database.windows.net,1433;Initial Catalog=tege_test;Persist Security Info=False;User ID=tege;Password=/pavasaris123;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";

            var options = new DbContextOptionsBuilder<NoteDbContext>()
                .UseSqlServer(connectionString)
                .Options;

            Context = new NoteDbContext();

            SeedDatabase();
        }

        private void SeedDatabase()
        {
            // Add initial test data if needed
            addedUser = new User("testuser", "Test", "User");
            addedNote = new Note
            {
                Author = "testuser",
                Title = "Test Note",
                Content = "This is a test note.",
                Privacy = PrivacySetting.Private,
                Sharing = SharingSetting.Viewing
            };
            Context.Users.Add(addedUser);
            Context.Notes.Add(addedNote);
            Context.SaveChanges();
        }

        public void Dispose()
        {
            Context.Users.Remove(addedUser);
            Context.Notes.Remove(addedNote);
            Context.SaveChanges();

            // Dispose the context
            Context.Dispose();
        }
    }
}

