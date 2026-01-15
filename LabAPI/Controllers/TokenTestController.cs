using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace LabAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class TokenTestController(ISender sender) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> Generate([FromQuery] TokenTestRequest request)
    {
        var result = await sender.Send(request);

        return Ok(result);
    }
}