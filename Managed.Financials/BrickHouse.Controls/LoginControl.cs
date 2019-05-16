using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

namespace BrickHouse.Controls
{

	public class LoginEventArgs : System.EventArgs
	{
		public string UserName { get; set; }
		public string Password { get; set; }
	}

	public delegate void LoginEventHandler(object sender, LoginEventArgs e);

	public class LoginControl : System.Web.UI.WebControls.CompositeControl, System.Web.UI.INamingContainer
	{
		private HtmlGenericControl _container;
		protected Textbox _userName;
		protected Textbox _password;
		protected Button _loginButton;
		protected  HyperLink _forgotPassword;
		protected Label _failureMessage;
		protected HtmlGenericControl _topContainer;
		protected HtmlGenericControl _bottomContainer;

		public event LoginEventHandler LoginClick;

		#region "Public Properties"
		public Textbox Password
		{
			get
			{
				this.EnsureChildControls();
				return _password;
			}
		}
		public Textbox UserName
		{
			get
			{
				this.EnsureChildControls();
				return _userName;
			}
		}

		public String PasswordCSSClass
		{
			get
			{
				this.EnsureChildControls();
				return _password.CssClass;
			}
			set
			{
				this.EnsureChildControls();
				_password.CssClass = value;
			}
		}

		public string LoginCssClass
		{
			get { return _loginButton.CssClass; }
			set { _loginButton.CssClass = value; }
		}

		public string UserNameCssClass
		{
			get
			{
				this.EnsureChildControls();
				return _userName.CssClass;
			}
			set
			{
				this.EnsureChildControls();
				_userName.CssClass = value;
			}
		}

		public Button LoginButton
		{
			get { return _loginButton; }
		}

		public HyperLink ForgotPassword
		{
			get
			{
				this.EnsureChildControls();
				return _forgotPassword;
			}
		}
		#endregion

		protected override void CreateChildControls()
		{
			base.CreateChildControls();
			_userName = new Textbox();
			_userName.ID = "_userName";
			_userName.EnableTheming = true;
			_userName.Text = "User Name";

			_password = new Textbox();
			_password.ID = "_password";
			_password.EnableTheming = true;
			_password.TextMode = TextBoxMode.Password;
			_password.Text = "Password";

			_loginButton = new Button();
			_loginButton.EnableTheming = true;
			_loginButton.ID = "_login";
			_loginButton.Text = "Login";
			_loginButton.Click += new EventHandler(_loginButton_Click);

			_failureMessage = new Label();
			_failureMessage.ID = "_loginFailedMessage";
			_failureMessage.CssClass = "failureNotification";

			_forgotPassword = new HyperLink();
			_forgotPassword.Text = "Forgot Password";

			_topContainer = new HtmlGenericControl("div");
			_topContainer.Attributes.Add("class", "inline container");

			_bottomContainer = new HtmlGenericControl("div");
			_bottomContainer.Attributes.Add("class", "inline container");

			_container = new HtmlGenericControl("div");
			_container.Attributes.Add("class", "container");
			_topContainer.Controls.Add(new System.Web.UI.LiteralControl("Member Login: "));
			_topContainer.Controls.Add(_userName);
			_topContainer.Controls.Add(_password);
			_topContainer.Controls.Add(_loginButton);
			_bottomContainer.Controls.Add(_failureMessage);
			_bottomContainer.Controls.Add(_forgotPassword);
			_container.Controls.Add(_topContainer);
			_container.Controls.Add(_bottomContainer);
			this.Controls.Add(_container);
		}

		protected void _loginButton_Click(object sender, EventArgs e)
		{
			OnLoginClick();
		}

		protected virtual void OnLoginClick()
		{
			if (LoginClick != null)
			{
				LoginEventArgs eventArgs = new LoginEventArgs();
				eventArgs.UserName = _userName.Text;
				eventArgs.Password = _password.Text;
				LoginClick(this, eventArgs);
			}
		}

		protected override void OnLoad(EventArgs e)
		{
			base.OnLoad(e);
			_userName.Attributes.Add("onfocus", "if(this.value == \'User Name\') this.value = \'\';");
			_password.Attributes.Add("onfocus", "if(this.value == \'Password\') this.value = \'\';");
		}

		protected override void OnInit(EventArgs e)
		{
			base.OnInit(e);
			this.EnsureChildControls();
		}

		protected override void OnPreRender(EventArgs e)
		{
			base.OnPreRender(e);
			if (!Page.ClientScript.IsClientScriptBlockRegistered("PasswordDefaultEntry"))
			{
				Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "PasswordDefaultEntry", String.Format(@"<script type=""text/javascript"">function defaultPassword(){{document.getElementById('{0}').value = 'Password';}} window.setTimeout('defaultPassword()',10);</script>", _password.ClientID));
			}
		}
	}
}
