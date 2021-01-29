using API.Errors;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    //we are owerriding route we are getting from our base controller
    [Route("errors/{code}")]
    //ovime govorimo swaggeru da ovo ignorira
    [ApiExplorerSettings(IgnoreApi = true)]
    public class ErrorController : BaseApiController
    {
        public IActionResult Error(int code)
        {
            //prosljeđuju se parametri iz konstruktora u apiresponse, statuscode i message
            return new ObjectResult(new ApiResponse(code));
        }
    }
}