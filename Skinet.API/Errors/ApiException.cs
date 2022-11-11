using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Skinet.API.Errors
{
	public class ApiException : ApiResponse
	{
		public ApiException(int status, string message = null, string details = null) 
		: base(status, message)
		{
			Details = details; 
		}

		public string  Details { get; set; }
	}
}
