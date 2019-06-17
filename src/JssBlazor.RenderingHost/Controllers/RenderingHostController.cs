using System.Net;
using JssBlazor.RenderingHost.Models;
using Microsoft.AspNetCore.Mvc;

namespace JssBlazor.RenderingHost.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RenderingHostController : ControllerBase
    {
        [HttpPost]
        public ResultModel Post([FromBody]RequestModel model)
        {
            var resultModel = new ResultModel
            {
                Html = @"
<html>
<head>
  <title>JSS Blazor Rendering Host</title>
</head>
<body>
  <div>Hello world!</div>
</body>
</html>",
                Status = (int)HttpStatusCode.OK
            };
            return resultModel;
        }
    }
}
