using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    //ovo je bazni kontroler, ostali derive iz njega
    public class BaseApiController : ControllerBase
    {
        
    }
}