using Microsoft.EntityFrameworkCore;

namespace NoteTakingApp
{
    public class DependencyInjector
    {
        private readonly NoteDbContext _dbContext;
        private readonly UserRepository _userRepository;

        public DependencyInjector(NoteDbContext dbContext, UserRepository userRepository)
        {
            _dbContext = dbContext;
            _userRepository = userRepository;
        }

        public NoteDbContext GetNoteDbContext()
        {
            return _dbContext;
        }

        public UserRepository GetUserRepository()
        {
            return _userRepository;
        }
    }
}
