using Data.Repository.interfaces;

namespace Data
{
    public interface IUnitOfWork
    {
        IClassRepository ClassRepository {  get; }
        IStudentRepository StudentRepository { get; }
    }
}
