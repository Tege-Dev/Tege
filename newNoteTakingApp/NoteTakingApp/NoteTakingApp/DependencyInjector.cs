using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NoteTakingApp
{
    public class DependencyInjector
    {
        private NoteDbContext dbContext;

        public DependencyInjector()
        {
            dbContext = new NoteDbContext();
        }

        public NoteDbContext GetNoteDbContext()
        {
            return dbContext;
        }
    }

}
