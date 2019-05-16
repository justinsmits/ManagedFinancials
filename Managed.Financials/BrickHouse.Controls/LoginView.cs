using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.UI;

namespace BrickHouse.Controls
{
	public class LoginView : System.Web.UI.WebControls.CompositeControl, System.Web.UI.INamingContainer
	{
		protected LoginControl _loginControl;
		protected System.Web.UI.HtmlControls.HtmlGenericControl _userLabel;
		public event LoginEventHandler LoginClick;

		#region "Properties"
		public String Name { get; set; }
		public string EditLink { get; set; }
		public string LogoutLink { get; set; }
		public String ForgotPasswordLink { get { return _loginControl.ForgotPassword.NavigateUrl; } set { _loginControl.ForgotPassword.NavigateUrl = value; } }
		public String PasswordCSSClass
		{
			get
			{
				this.EnsureChildControls();
				return _loginControl.CssClass;
			}
			set
			{
				this.EnsureChildControls();
				_loginControl.PasswordCSSClass = value;
			}
		}

		public string LoginCssClass
		{
			get { return _loginControl.LoginCssClass; }
			set { _loginControl.LoginCssClass = value; }
		}

		public string UserNameCssClass
		{
			get
			{
				this.EnsureChildControls();
				return _loginControl.UserNameCssClass;
			}
			set
			{
				this.EnsureChildControls();
				_loginControl.UserNameCssClass = value;
			}
		}

		public Button LoginButton
		{
			get { return _loginControl.LoginButton; }
		}

		
		#endregion

		protected override void CreateChildControls()
		{
			base.CreateChildControls();

			_loginControl = new LoginControl();
			_loginControl.ID = "_loginControl";

			_userLabel = new System.Web.UI.HtmlControls.HtmlGenericControl("span");
			_userLabel.ID = "_userLabel";
			_loginControl.LoginClick += new LoginEventHandler(_loginControl_LoginClick);

			this.Controls.Add(_loginControl);
			this.Controls.Add(_userLabel);
		}

		void _loginControl_LoginClick(object sender, LoginEventArgs e)
		{
			if (LoginClick != null)
			{
				LoginClick(sender, e);
			}
		}
		protected override void OnInit(EventArgs e)
		{
			base.OnInit(e);
			this.EnsureChildControls();
		}
		protected override void OnPreRender(EventArgs e)
		{
			base.OnPreRender(e);
			if (String.IsNullOrEmpty(this.Name))
			{
				_userLabel.Visible = false;
			}
			else
			{
				_userLabel.InnerHtml = String.Format(@"Welcome <a href='{0}'>{1}</a>! <a href='{2}'>[Logout]</a>", this.EditLink, this.Name, this.LogoutLink);
				_loginControl.Visible = false;
			}
		}

	}
}
