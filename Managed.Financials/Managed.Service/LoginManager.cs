using System;
using Managed.Data;
using Managed.Service.Authentication;

namespace Managed.Service
{
	public class LoginManager
	{
		public Authentication.AuthenticationResponse Login(string userName, string password)
		{
			IAuthenticationProvider provider;

			//if we have different methods for logging in, we can add those here
			provider = new FormsAuthentication();
			return provider.Authenticate(userName, password);
		}
	}
}
