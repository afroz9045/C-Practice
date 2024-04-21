using LibraryManagement.Core.Entities;

namespace LibraryManagement.Core.Contracts.Services
{
    public interface IStudentService
    {
        Student? AddStudent(Student student, IEnumerable<Department>? departmentData);

        Student? updateStudentAsync(Student student, int studentId, Student? existingStudent);
    }
}