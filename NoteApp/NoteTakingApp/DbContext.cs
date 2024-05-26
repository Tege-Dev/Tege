using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace NoteTakingApp
{
    public class NoteDbContext : DbContext
    {
        public NoteDbContext(DbContextOptions<NoteDbContext> options)
            : base(options)
        {
        }

        public DbSet<Note> Notes { get; set; }
        public DbSet<UserNote> UserNotes { get; set; }
        public DbSet<User> Users { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            // Remove the hard-coded connection string from here
            // as you'll be passing options via constructor
             optionsBuilder.UseSqlServer("Server=tcp:tegeserver.database.windows.net,1433;Initial Catalog=tege;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;Authentication=Active Directory Default;");
        }

        public async Task<List<Note>> GetAllNotesAsync()
        {
            return await Notes.ToListAsync();
        }

        public async Task<List<UserNote>> GetAllUserNotesAsync()
        {
            return await UserNotes.ToListAsync();
        }
    }
}