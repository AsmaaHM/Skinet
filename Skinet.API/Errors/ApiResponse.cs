using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Skinet.API.Errors
{
	public class ApiResponse
	{
		public ApiResponse(int status, string message = null)
		{
			Status = status;
			Message = message ?? GetDefaultMessageForStatusCode(status);
		}


		public int Status { get; set; }
		public string Message { get; set; }

		private string GetDefaultMessageForStatusCode(int status)
		{
			return status switch
			{

				400 => "A bad request",
				401 => "Not Authorized",
				500 => "Server error",
				404 => "Not found", 
				_ => null
			};
		}
	}
}
