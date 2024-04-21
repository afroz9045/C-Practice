using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApiExplorer;

namespace EmployeeRecordBook.Api.Infrastructure.Specs
{
    public static class CustomApiConventions
    {
#nullable disable

        #region DELETE

        /// <summary>
        /// Delete convention.
        /// </summary>
        /// <param name="id"></param>
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
        [ProducesDefaultResponseType]
        [ApiConventionNameMatch(ApiConventionNameMatchBehavior.Prefix)]
        public static void Delete(
            [ApiConventionNameMatch(ApiConventionNameMatchBehavior.Suffix)]
           [ApiConventionTypeMatch(ApiConventionTypeMatchBehavior.Any)]
            object id)
        { }

        #endregion DELETE
    }
}