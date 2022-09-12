using LibraryManagement.Core.Contracts.Repositories;
using LibraryManagement.Core.Contracts.Services;
using LibraryManagement.Core.Entities;

namespace LibraryManagement.Core.Services
{
    public class DesignationService : IDesignationService
    {
        private readonly IDesignationRepository _designationRepository;

        public DesignationService(IDesignationRepository designationRepository)
        {
            _designationRepository = designationRepository;
        }

        public async Task<IEnumerable<Designation>?> GetDesignationAsync()
        {
            var designations = await _designationRepository.GetDesignationAsync();
            if (designations != null)
                return designations;
            return null;
        }

        public async Task<Designation?> GetDesignationByIdAsync(string designationId)
        {
            var designation = await _designationRepository.GetDesignationByIdAsync(designationId);
            if (designation != null)
                return designation;
            return null;
        }

        public async Task<Designation?> GetDesignationByNameAsync(string designationName)
        {
            var designation = await _designationRepository.GetDesignationByNameAsync(designationName);
            if (designation != null)
                return designation;
            return null;
        }

        public async Task<string?> GenerateDesignationId()
        {
            var recentDesignationRecord = await _designationRepository.GetRecentInsertedDesignation();
            if (recentDesignationRecord != null && recentDesignationRecord.DesignationId != null)
            {
                var firstCharacter = recentDesignationRecord.DesignationId.Substring(0, 1);
                var remainingNumber = Convert.ToInt32(recentDesignationRecord.DesignationId.Substring(1));
                var resultantDesignationId = Convert.ToString(firstCharacter + (remainingNumber + 1));
                return resultantDesignationId;
            }
            return "A100";
        }

        public async Task<Designation?> AddDesignationAsync(Designation designation)
        {
            var designationRecord = await GetDesignationByNameAsync(designation.DesignationName);
            if (designationRecord != null)
            {
                var designationId = await GenerateDesignationId();
                var designationGenerate = new Designation()
                {
                    DesignationId = designationId,
                    DesignationName = designation.DesignationName
                };
                var addedDesignation = await _designationRepository.AddDesignationAsync(designationGenerate);
                if (addedDesignation != null)
                    return addedDesignation;
            }
            return null;
        }

        public async Task<Designation?> UpdateDesignationAsync(string designationId, Designation designation)
        {
            var designationRecord = await GetDesignationByIdAsync(designationId);
            if (designationRecord != null)
            {
                designationRecord.DesignationId = designationId;
                designationRecord.DesignationName = designation.DesignationName;
                var updatedDesignation = await _designationRepository.UpdateDesignationAsync(designation);
                return updatedDesignation;
            }
            return null;
        }

        public async Task<Designation?> DeleteDesignationAsync(string designationId)
        {
            var designationRecord = await GetDesignationByIdAsync(designationId);
            if (designationRecord != null)
            {
                var deletedDesignation = await _designationRepository.DeleteDesignationAsync(designationRecord);
                if (deletedDesignation != null)
                {
                    return deletedDesignation;
                }
            }
            return null;
        }
    }
}