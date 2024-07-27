using OnlineFoodClass.Data;
using OnlineFoodClass.Repositories.IRepository;
using OnlineFoodClass.Repositories.Repository;

namespace OnlineFoodClass.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _dbContext;
        public UnitOfWork(ApplicationDbContext dbContext) 
        {
            _dbContext = dbContext;
            Course = new CourseRepository(_dbContext);
        }
        public ICourseRepository Course { get; private set; }

        public async Task SaveAsync()
        {
            await _dbContext.SaveChangesAsync();
        }
    }
}
