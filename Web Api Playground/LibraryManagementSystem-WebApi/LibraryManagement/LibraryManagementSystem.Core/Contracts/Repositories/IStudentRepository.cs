using LibraryManagement.Core.Entities;

namespace LibraryManagement.Core.Contracts.Repositories
{
    public interface IStudentRepository
    {
        Task<Student> AddStudentAsync(Student student);

        Task<Student?> DeleteStudentAsync(Student? student);

        Task<Student?> GetStudentByIdAsync(int studentId);

        Task<IEnumerable<Student>> GetStudentsAsync();

        Task<Student?> updateStudentAsync(Student student);
    }
}