using LibraryManagement.Core.Entities;

namespace LibraryManagement.Core.Contracts.Services
{
    public interface IStudentService
    {
        Task<Student?> AddStudentAsync(Student student);
        Task<Student?> DeleteStudentAsync(int studentId);
        Task<Student?> GetStudentByIdAsync(int studentId);
        Task<IEnumerable<Student>?> GetStudentsAsync();
        Task<Student?> updateStudentAsync(Student student, int studentId);
    }
}