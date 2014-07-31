using System;
using System.Linq;
using System.Threading;
using System.Web.UI.WebControls;
using Telerik.Sitefinity.Configuration;
using Telerik.Sitefinity.Modules.Pages;
using Telerik.Sitefinity.Modules.Pages.Configuration;
using Telerik.Sitefinity.Pages.Model;
using Telerik.Sitefinity.Web;

public partial class Templates_Sitemap : System.Web.UI.UserControl
{
    private PageManager pageManager;

    protected void Page_Load(object sender, EventArgs e)
    {
        pageManager = new PageManager();
    }

    protected void lvPages_ItemDataBound(object sender, ListViewItemEventArgs e)
    {
        if (e.Item.ItemType != ListViewItemType.DataItem)
        {
            return;
        }

        PagesConfig pagesConfig = Config.Get<PagesConfig>();
        
        PageSiteNode node = (PageSiteNode)e.Item.DataItem;
        PageNode page = pageManager.GetPageNode(node.Id);
        if (!node.ShowInNavigation || string.IsNullOrEmpty(node.Title))
        {
            e.Item.Visible = false;
            return;
        }

        HyperLink hlPage = (HyperLink)e.Item.FindControl("hlPage");
        hlPage.Text = node.Title;
        hlPage.NavigateUrl = page.GetFullUrl(Thread.CurrentThread.CurrentCulture, false);

        if (node.HasChildNodes)
        {
            ListView lvPages = e.Item.FindControl("lvPages") as ListView;
            if (lvPages != null)
            {
                lvPages.DataSource = node.ChildNodes;
                lvPages.DataBind();
            }
        }
    }
}