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

        public async Task<IEnumerable<Student>?> GetStudentsAsync()
        {
            var studentRecords = await _studentRepository.GetStudentsAsync();
            if (studentRecords != null)
            {
                return studentRecords;
            }
            return null;
        }

        public async Task<Student?> GetStudentByIdAsync(int studentId)
        {
            var student = await _studentRepository.GetStudentByIdAsync(studentId);
            if (student != null)
            {
                return student;
            }
            return null;
        }

        public async Task<Student?> AddStudentAsync(Student student)
        {
            var departmentData = await _departmentRepository.GetDepartmentsAsync();
            var studentData = await _studentRepository.GetStudentsAsync();
            string? departmentName = null;
            if (studentData != null && departmentData != null)
            {
                departmentName = (from dept in departmentData
                                  join std in studentData
                                  on dept.DeptId equals std.DepartmentId
                                  where dept.DeptId == student.DepartmentId
                                  select dept.DepartmentName).FirstOrDefault();
            }
            Student studentRecord = new Student()
            {
                DepartmentId = student.DepartmentId,
                StudentName = student.StudentName,
                Gender = student.Gender,
                StudentDepartment = departmentName
            };
            var addedStudent = await _studentRepository.AddStudentAsync(studentRecord);
            if (addedStudent != null)
                return addedStudent;
            return null;
        }

        public async Task<Student?> updateStudentAsync(Student student, int studentId)
        {
            var studentRecord = await GetStudentByIdAsync(studentId);
            if (studentRecord != null)
            {
                studentRecord.DepartmentId = student.DepartmentId;
                studentRecord.StudentName = student.StudentName;
                studentRecord.Gender = student.Gender;

                var updatedStudent = await _studentRepository.updateStudentAsync(studentRecord);
                if (updatedStudent != null)
                    return updatedStudent;
            }
            return null;
        }

        public async Task<Student?> DeleteStudentAsync(int studentId)
        {
            var studentRecord = await GetStudentByIdAsync(studentId);
            if (studentRecord != null)
            {
                var deletedStudent = await _studentRepository.DeleteStudentAsync(studentRecord);
                if (deletedStudent != null)
                    return deletedStudent;
            }
            return null;
        }
    }
}