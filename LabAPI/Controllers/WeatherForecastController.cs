using AtomCore.i18n;
using LabAPI.i18nTest;
using Microsoft.AspNetCore.Mvc;

namespace LabAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private readonly IMessages _messages;

        public WeatherForecastController(I18n _i18n)
        {
            _messages = _i18n.GetTranslation<IMessages>();
        }

        [HttpGet()]
        public IActionResult Get()
        {
            return Ok(_messages.Hello);
        }
    }
}
