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
            CreateMap<BookVm, Book>();
            CreateMap<DepartmentVm, Department>();
            CreateMap<StudentVm, Student>();
            CreateMap<StaffVm, Staff>();
            CreateMap<DesignationVm, Designation>();
            CreateMap<IssueVm, Issue>();
            CreateMap<ReturnVm, Return>();
            CreateMap<BookIssuedToVm, BookIssuedTo>();
        }
    }
}