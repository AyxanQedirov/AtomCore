using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AtomCore.RestAPI;

[ApiController]
[Route("api/[controller]/[action]")]
public class BaseController : ControllerBase
{
}
