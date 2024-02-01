using Microsoft.AspNetCore.Mvc;

namespace Gradeo.CoreApplication.WebAPI.Controllers;
[Route("api/[controller]")]
[ApiController]
public class TestController : ControllerBase
{
    [HttpGet]
    public List<string> GetValues()
    {
        return new List<string>() { "hahahah", "xaxaxxaa", "scrscrscr" };
    }
}
