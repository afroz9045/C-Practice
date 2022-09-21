using LibraryManagement.Core.Contracts.Repositories;
using LibraryManagement.Core.Contracts.Services;
using LibraryManagement.Core.Entities;

namespace LibraryManagement.Core.Services
{
    public class StudentService : IStudentService
    {
        private readonly IStudentRepository _studentRepository;
        private readonly IDepartmentRepository _departmentRepository;

        public StudentService(IStudentRepository studentRepository, IDepartmentRepository departmentRepository)
        {
            _studentRepository = studentRepository;
            _departmentRepository = departmentRepository;
        }

        public Student? AddStudent(Student student, IEnumerable<Department>? departmentData)
        {
            string? departmentName = null;
            if (student != null && departmentData != null)
            {
                departmentName = (from dept in departmentData
                                  where dept.DeptId == student.DepartmentId
                                  select dept.DepartmentName).FirstOrDefault();

                Student studentRecord = new Student()
                {
                    DepartmentId = student.DepartmentId,
                    StudentName = student.StudentName,
                    Gender = student.Gender,
                    StudentDepartment = departmentName
                };
                return studentRecord;
            }
            return null;
        }

        public Student? updateStudentAsync(Student student, int studentId, Student? existingStudent)
        {
            if (existingStudent != null && student != null)
            {
                existingStudent.DepartmentId = student.DepartmentId;
                existingStudent.StudentName = student.StudentName;
                existingStudent.Gender = student.Gender;

                return existingStudent;
            }
            return null;
        }
    }
}