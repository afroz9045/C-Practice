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

        public async Task<Designation?> AddDesignationAsync(Designation designation, Designation? existingDesignation)
        {
            var designationId = await GenerateDesignationId();
            var designationGenerate = new Designation()
            {
                DesignationId = designationId,
                DesignationName = designation.DesignationName
            };
            return designationGenerate;
        }

        public Designation? UpdateDesignationAsync(string designationId, Designation designation, Designation existingDesignation)
        {
            existingDesignation.DesignationId = designationId;
            existingDesignation.DesignationName = designation.DesignationName;
            return existingDesignation;
        }
    }
}