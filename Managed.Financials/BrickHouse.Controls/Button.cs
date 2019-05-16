using System;
using System.Web.UI.WebControls;

namespace BrickHouse.Controls
{
    public class Button : System.Web.UI.WebControls.Button
    {

        public ButtonSize Size { get; set; }
				

        protected override void OnPreRender(EventArgs e)
        {
            base.OnPreRender(e);
            base.CssClass = base.CssClass + " " + this.Size.ToString();
        }
				public Button()
				{
					
				}
    }

    [Flags()]
    public enum ButtonSize
    {
        buttonsmall = 0,
        buttonmedium = 1,
        buttonlarge = 2
    }
}
