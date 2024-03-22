using Microsoft.EntityFrameworkCore;
using NoteTakingApp.Repositories;
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
        private UserRepository userRepository;

        public DependencyInjector()
        {
            dbContext = new NoteDbContext();
            userRepository = new UserRepository(dbContext);
        }

        public NoteDbContext GetNoteDbContext()
        {
            return dbContext;
        }
        public UserRepository GetUserRepository()
        {
            return userRepository;
        }

    }

}
