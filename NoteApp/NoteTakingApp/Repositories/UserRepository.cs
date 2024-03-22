namespace NoteTakingApp.Repositories
{
    public class UserRepository
    {
        private readonly NoteDbContext _context;

        public UserRepository(NoteDbContext context)
        {
            _context = context;
        }
    }
}
