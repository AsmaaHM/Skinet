using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Skinet.API.Dtos
{
	public class RegisterDto
	{
		[Required]
		public string DisplayName { get; set; }
		[Required]
		[EmailAddress]
		public string Email { get; set; }
		[Required]
		[RegularExpression("(?=^.{6,10}$)(?=.*\\d)(?=.*[a-z])(?=.*[A-Z])(?=.*[!@#$%^&amp;*()_+}{&quot;:;'?/&gt;.&lt;,])(?!.*\\s).*$", ErrorMessage="Password must have ....")]
		public string Password { get; set; }

	}
}
