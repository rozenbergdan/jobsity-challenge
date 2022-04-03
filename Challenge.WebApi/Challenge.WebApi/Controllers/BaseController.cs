using Microsoft.AspNetCore.Mvc;

namespace Challenge.WebApi.Controllers
{
    //[TypeFilter(typeof(HttpDomainExceptionFilter))]
    
    [ApiController]
    [Produces("application/json")]
    public class BaseController : Controller
    {
    }
}
