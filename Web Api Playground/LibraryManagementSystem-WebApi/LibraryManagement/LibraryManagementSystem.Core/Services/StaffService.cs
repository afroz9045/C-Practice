using AutoMapper;
using LibraryManagement.Core.Contracts.Repositories;
using LibraryManagement.Core.Contracts.Services;
using LibraryManagement.Core.Entities;

namespace LibraryManagement.Core.Services
{
    public class StaffService : IStaffService
    {
        private readonly IStaffRepository _staffRepository;
        private readonly IMapper _mapper;

        public StaffService(IStaffRepository staffRepository, IMapper mapper)
        {
            _staffRepository = staffRepository;
            _mapper = mapper;
        }

        public async Task<Staff?> AddStaffAsync(Staff staff)
        {
            var staffId = await GenerateStaffId();

            var staffRecord = new Staff()
            {
                StaffId = staffId,
                StaffName = staff.StaffName,
                Gender = staff.Gender,
                DesignationId = staff.DesignationId
            };
            return staffRecord;
        }

        public async Task<string?> GenerateStaffId()
        {
            var recentStaffRecord = await _staffRepository.GetRecentInsertedStaff();
            if (recentStaffRecord != null && recentStaffRecord.StaffId != null)
            {
                var firstCharacter = recentStaffRecord.StaffId.Substring(0, 1);
                var remainingNumber = Convert.ToInt32(recentStaffRecord.StaffId.Substring(1));
                var resultantStaffId = Convert.ToString(firstCharacter + (remainingNumber + 1));
                return resultantStaffId;
            }
            return "S1001";
        }

        public Staff UpdateStaffAsync(Staff existingstaff, Staff updatedStaff)
        {
            existingstaff.StaffId = existingstaff.StaffId;
            existingstaff.StaffName = updatedStaff.StaffName;
            existingstaff.Gender = updatedStaff.Gender;
            existingstaff.DesignationId = updatedStaff.DesignationId;

            return existingstaff;
        }
    }
}