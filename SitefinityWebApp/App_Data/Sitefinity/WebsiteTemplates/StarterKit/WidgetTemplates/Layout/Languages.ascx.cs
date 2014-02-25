using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Templates_Languages : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {
        ltrCurrentLanguageName.Text = System.Globalization.CultureInfo.CurrentUICulture.NativeName;
    }
}