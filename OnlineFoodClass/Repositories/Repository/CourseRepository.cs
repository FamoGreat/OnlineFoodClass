using OnlineFoodClass.Data;
using OnlineFoodClass.Models;
using OnlineFoodClass.Repositories.IRepository;

namespace OnlineFoodClass.Repositories.Repository
{
    public class CourseRepository : Repository<Course>, ICourseRepository
    {
        public CourseRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
            
        }

        public void UpdateAsync(Course course)
        {
            _dbContext.Update(course);
        }
    }
}
