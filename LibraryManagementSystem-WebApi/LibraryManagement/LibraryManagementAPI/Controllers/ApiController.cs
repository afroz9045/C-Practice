using LibraryManagement.Core.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LibraryManagement.Api.Controllers
{
    //[Route("v{version:apiVersion}")]
    [ApiController]
    [Authorize]
    public class ApiController : ControllerBase
    {
    }
}