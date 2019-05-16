using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Managed.Service.Authentication
{
	public class FormsAuthentication : IAuthenticationProvider
	{
		public AuthenticationResponse Authenticate(string userName, string password)
		{
			AuthenticationResponse response = new AuthenticationResponse();

			return response;
		}
	}
}
