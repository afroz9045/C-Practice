using LibraryManagement.Core.Contracts.Repositories;
using LibraryManagement.Core.Contracts.Services;
using LibraryManagement.Core.Entities;

namespace LibraryManagement.Core.Services
{
    public class StaffService : IStaffService
    {
        private readonly IStaffRepository _staffRepository;

        public StaffService(IStaffRepository staffRepository)
        {
            _staffRepository = staffRepository;
        }

        public async Task<Staff?> AddStaffAsync(Staff staff)
        {
            var staffDetails = await GetStaffByName(staff.StaffName);
            if (staffDetails == null)
            {
                var staffId = await GenerateStaffId();

                var staffRecord = new Staff()
                {
                    StaffId = staffId,
                    StaffName = staff.StaffName,
                    Gender = staff.Gender,
                    DesignationId = staff.DesignationId
                };
                var staffAddedResult = await _staffRepository.AddStaffAsync(staffRecord);
                if (staffAddedResult != null)
                    return staffAddedResult;
            }
            return null;
        }

        public async Task<string?> GenerateStaffId()
        {
            var recentStaffRecord = await _staffRepository.GetRecentInsertedStaff();
            if (recentStaffRecord != null && recentStaffRecord.DesignationId != null)
            {
                var firstCharacter = recentStaffRecord.StaffId.Substring(0, 1);
                var remainingNumber = Convert.ToInt32(recentStaffRecord.StaffId.Substring(1));
                var resultantStaffId = Convert.ToString(firstCharacter + (remainingNumber + 1));
                return resultantStaffId;
            }
            return "S1001";
        }

        public async Task<Staff?> GetStaffByName(string staffName)
        {
            var staffRecord = await _staffRepository.GetStaffByName(staffName);
            return staffRecord;
        }

        public async Task<IEnumerable<Staff>?> GetStaffAsync()
        {
            var staffDetails = await _staffRepository.GetStaffAsync();
            if (staffDetails != null)
                return staffDetails;
            return null;
        }

        public async Task<Staff?> GetStaffByIdAsync(string staffId)
        {
            var staffDetail = await _staffRepository.GetStaffByIdAsync(staffId);
            if (staffDetail != null)
                return staffDetail;
            return null;
        }

        public async Task<Staff?> UpdateStaffAsync(Staff staff, string staffId)
        {
            var staffRecord = await GetStaffByIdAsync(staffId);
            if (staffRecord != null)
            {
                staffRecord.StaffId = staffId;
                staffRecord.StaffName = staff.StaffName;
                staffRecord.Gender = staff.Gender;
                staffRecord.DesignationId = staff.DesignationId;

                var updatedStaffRecord = await _staffRepository.UpdateStaffAsync(staffRecord);
                if (updatedStaffRecord != null)
                    return updatedStaffRecord;
            }
            return null;
        }

        public async Task<Staff?> DeleteStaffAsync(string staffId)
        {
            var staffRecord = await GetStaffByIdAsync(staffId);
            if (staffRecord != null)
            {
                var deletedStaffRecord = await _staffRepository.DeleteStaffAsync(staffRecord);
                return deletedStaffRecord;
            }
            return null;
        }
    }
}