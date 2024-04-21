using LibraryManagement.Core.Contracts.Services;
using LibraryManagement.Core.Entities;

namespace LibraryManagement.Core.Services
{
    public class DesignationService : IDesignationService
    {
        public string? GenerateDesignationId(Designation? recentDesignationRecord)
        {
            if (recentDesignationRecord != null && recentDesignationRecord.DesignationId != null)
            {
                var firstCharacter = recentDesignationRecord.DesignationId.Substring(0, 1);
                var remainingNumber = Convert.ToInt32(recentDesignationRecord.DesignationId.Substring(1));
                var resultantDesignationId = Convert.ToString(firstCharacter + (remainingNumber + 1));
                return resultantDesignationId;
            }
            return "A100";
        }

        public Designation? AddDesignationAsync(Designation designation, Designation? recentDesignation)
        {
            var designationId = GenerateDesignationId(recentDesignation);
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