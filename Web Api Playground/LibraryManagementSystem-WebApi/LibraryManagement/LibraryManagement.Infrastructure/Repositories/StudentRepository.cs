﻿using Dapper;
using LibraryManagement.Core.Contracts;
using LibraryManagement.Core.Dtos;
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

        public async Task<dynamic> GetStudentsAsync()
        {
            var getStudentQuery = "select * from [student]";
            var studentData = await _dapperConnection.QueryAsync(getStudentQuery);
            return studentData;
        }

        public async Task<dynamic> GetStudentByIdAsync(int studentId)
        {
            var getStudentByIdQuery = "select * from [student] where studentId=@studentId";
            return await _dapperConnection.QueryAsync(getStudentByIdQuery, new { studentId = studentId });
        }

        public async Task<Student> AddStudentAsync(StudentDto student)
        {
            Student studentRecord = new Student()
            {
                DepartmentId = student.DepartmentId,
                StudentName = student.StudentName,
                //StudentId = student.StudentId,
                Gender = student.Gender,
                StudentDepartment = await (from department in _libraryDbContext.Departments
                                           where student.DepartmentId == department.DeptId
                                           select department.DepartmentName).FirstOrDefaultAsync()
            };
            await _libraryDbContext.Students.AddAsync(studentRecord);
            await _libraryDbContext.SaveChangesAsync();
            return studentRecord;
        }

        public async Task<Student> updateStudentAsync(Student student, int studentId)
        {
            var studentRecord = await GetStudentByIdAsync(studentId);
            studentRecord.StudentId = student.StudentId;
            studentRecord.StudentName = student.StudentName;
            studentRecord.Gender = student.Gender;
            studentRecord.StudentDepartment = student.StudentDepartment;
            studentRecord.DepartmentId = student.DepartmentId;

            _libraryDbContext.Update(studentRecord);
            await _libraryDbContext.SaveChangesAsync();
            return studentRecord;
        }

        public async Task<Student> DeleteStudentAsync(int studentId)
        {
            var studentRecord = await GetStudentByIdAsync(studentId);
            _libraryDbContext.Students.Remove(studentRecord);
            await _libraryDbContext.SaveChangesAsync();
            return studentRecord;
        }
    }
}