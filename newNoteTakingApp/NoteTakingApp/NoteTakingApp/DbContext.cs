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

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Data Source=EGLE\\MSSQLSERVER01;Database=Notes;Integrated Security=True;MultipleActiveResultSets=True;TrustServerCertificate=True;");
        }
    }
}
