using OnlineFoodClass.Repositories.IRepository;

namespace OnlineFoodClass.Repositories
{
    public interface IUnitOfWork
    {
        ICourseRepository Course { get; }

        Task SaveAsync();
    }
}
