using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Managed.Service.Authentication
{
	public interface IAuthenticationProvider
	{
		 AuthenticationResponse Authenticate(string userName, string password);
	}
}
