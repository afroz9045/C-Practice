using AutoMapper;
using LibraryManagement.Core.Entities;
using LibraryManagementAPI.ViewModels;

namespace LibraryManagementAPI.Configuration
{
    internal class AutoMapperConfiguration : Profile
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
        }
    }
}