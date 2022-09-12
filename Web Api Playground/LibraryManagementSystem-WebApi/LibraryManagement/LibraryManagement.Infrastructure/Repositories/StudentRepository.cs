using Dapper;
using LibraryManagement.Core.Contracts.Repositories;
using LibraryManagement.Core.Entities;
using LibraryManagement.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace LibraryManagement.Infrastructure.Repositories
{
    public class StudentRepository : IStudentRepository
    {
        private readonly LibraryManagementSystemDbContext _libraryDbContext;
        private readonly IDbConnection _dapperConnection;

        public StudentRepository(LibraryManagementSystemDbContext libraryDbContext, IDbConnection dapperConnection)
        {
            _libraryDbContext = libraryDbContext;
            _dapperConnection = dapperConnection;
        }

        public async Task<IEnumerable<Student>> GetStudentsAsync()
        {
            var getStudentQuery = "select * from [student]";
            var studentData = await _dapperConnection.QueryAsync<Student>(getStudentQuery);
            return studentData;
        }

        public async Task<Student?> GetStudentByIdAsync(int studentId)
        {
            if (studentId != 0)
            {
                var getStudentByIdQuery = "select * from [student] where studentId=@studentId";
                return await _dapperConnection.QueryFirstOrDefaultAsync<Student>(getStudentByIdQuery, new { studentId = studentId });
            }
            return null;
        }

        public async Task<Student> AddStudentAsync(Student student)
        {
            await _libraryDbContext.Students.AddAsync(student);
            await _libraryDbContext.SaveChangesAsync();
            return student;
        }

        public async Task<Student?> updateStudentAsync(Student student)
        {
            _libraryDbContext.Update(student);
            await _libraryDbContext.SaveChangesAsync();
            return student;
        }

        public async Task<Student?> DeleteStudentAsync(Student student)
        {
            _libraryDbContext.Students.Remove(student);
            await _libraryDbContext.SaveChangesAsync();
            return student;
        }
    }
}