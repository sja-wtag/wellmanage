using Asp.Versioning;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace wellmanage_dotnet.Controllers
{
    [ApiVersion(1.0)]
    [ApiController]
    [Route("api/v{v:apiVersion}/user")]


    public class UserController : ControllerBase
    {
        //[Authorize]
        //[HttpPost("check-in")]
        //public async Task<IActionResult> CheckInUser()
        //{

    }
}

