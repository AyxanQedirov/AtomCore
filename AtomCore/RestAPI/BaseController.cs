using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
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
    protected IMediator Mediator => _mediatr ??= HttpContext.RequestServices.GetService<IMediator>();
    private IMediator? _mediatr;
    protected ISender Sender => _sender ??= HttpContext.RequestServices.GetService<ISender>();
    private ISender? _sender;
}
