using AutoMapper;
using LibraryManagement.Api.ViewModels;
using LibraryManagement.Core.Dtos;
using LibraryManagement.Core.Entities;

namespace LibraryManagement.Api.Configuration
{
    public class AutoMapperConfiguration : Profile
    {
        internal AutoMapperConfiguration()
        {
            // Vm to entity
            CreateMap<BookVm, Book>();
            CreateMap<DepartmentVm, Department>();
            CreateMap<StudentVm, Student>();
            CreateMap<StaffVm, Staff>();
            CreateMap<StaffUpdateVm, Staff>();
            CreateMap<DesignationVm, Designation>();
            CreateMap<IssueVm, Issue>();
            CreateMap<ReturnVm, Return>();
            CreateMap<BookIssuedToVm, BookIssuedTo>();
            CreateMap<IssueUpdateVm, Issue>();
            CreateMap<StaffVm, RegistrationVm>();

            // Entity to dto
            CreateMap<Book, BookDto>();
            CreateMap<Department, DepartmentDto>();
            CreateMap<Designation, DesignationDto>();
            CreateMap<Issue, IssueDto>();
            CreateMap<BookIssuedTo, IssueStudentOrStaffDto>();
            CreateMap<Issue, IssueStudentOrStaffDto>();
            CreateMap<IssueDto, IssueStudentOrStaffDto>();
            CreateMap<Penalty, PenaltyDto>();
            CreateMap<Return, ReturnDto>();
            CreateMap<Staff, StaffDto>();
            CreateMap<Student, StudentDto>();
        }
    }
}