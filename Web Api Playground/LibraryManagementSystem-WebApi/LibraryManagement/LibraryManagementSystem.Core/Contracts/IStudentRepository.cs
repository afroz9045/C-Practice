using LibraryManagement.Core.Entities;

namespace LibraryManagement.Core.Contracts
{
    public interface IStudentRepository
    {
        Task<Student> AddStudentAsync(Student student);

        Task<Student> DeleteStudentAsync(int studentId);

        Task<Student> GetStudentByIdAsync(int studentId);

        Task<dynamic> GetStudentsAsync();

        Task<Student> updateStudentAsync(Student student, int studentId);
    }
}