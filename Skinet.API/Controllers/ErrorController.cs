using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Skinet.API.Errors;
using Skinet.Infrastructure;

namespace Skinet.API.Controllers
{
	[Route("errors/{code}")]
	[ApiExplorerSettings(IgnoreApi = true)]
	public class ErrorController : BaseApiController
	{

		public IActionResult Error(int code) {

			return new ObjectResult(new ApiResponse(code));
		}

	} 
}
