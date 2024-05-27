using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace NoteTakingApp
{
    public class NoteDbContext : DbContext
    {
        public DbSet<Note> Notes { get; set; }
        public DbSet<UserNote> UserNotes { get; set; }
        public DbSet<User> Users { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
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
