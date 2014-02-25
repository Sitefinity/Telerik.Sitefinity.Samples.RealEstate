using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Sitefinity.Modules.Pages;

public partial class App_Master_BaseTemplate : System.Web.UI.MasterPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        this.Head1.DataBind();

        //var pageManager = PageManager.GetManager();
        //var template = pageManager.GetTemplates().Where(t => t.MasterPage == "~/App_Data/Sitefinity/WebsiteTemplates/StarterKitBaseTemplate/App_Master/BaseTemplate.master");
        //foreach (var temp in template)
        //{
        //    temp.MasterPage = "~/App_Data/Sitefinity/WebsiteTemplates/StarterKit/App_Master/BaseTemplate.master";
        //}

        //pageManager.SaveChanges();
    }
}
