using LibraryManagement.Core.Dtos;
using LibraryManagement.Core.Entities;

namespace LibraryManagement.Core.Contracts
{
    public interface IStudentRepository
    {
        Task<Student> AddStudentAsync(StudentDto student);

        Task<Student> DeleteStudentAsync(int studentId);

        Task<dynamic> GetStudentByIdAsync(int studentId);

        Task<dynamic> GetStudentsAsync();

        Task<Student> updateStudentAsync(Student student, int studentId);
    }
}