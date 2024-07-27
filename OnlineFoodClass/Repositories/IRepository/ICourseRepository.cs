using OnlineFoodClass.Models;

namespace OnlineFoodClass.Repositories.IRepository
{
    public interface ICourseRepository : IRepository<Course>
    {
        void UpdateAsync(Course course);
    }
}
