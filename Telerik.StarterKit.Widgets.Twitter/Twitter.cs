using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Telerik.Sitefinity.Web.UI;
using Telerik.Sitefinity.Localization;
using Telerik.Sitefinity.Modules.GenericContent;
using Telerik.Sitefinity.Web.Utilities;
using Telerik.Sitefinity.Modules.Pages.PropertyPersisters;
using System.Web.UI;
using Telerik.Sitefinity.Web.UI.ControlDesign;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Text.RegularExpressions;
using System.Collections;
using System.Xml.Linq;
using System.Web;
using System.Globalization;
using Telerik.Web.UI;
using Telerik.StarterKit.Widgets.Twitter.Entities;

namespace Telerik.StarterKit.Widgets.Twitter
{

    [ControlDesigner(typeof(TwitterDesigner))]
    public class Twitter : SimpleView
    {

        #region Constants

        private const string layoutTemplateName = "Telerik.StarterKit.Widgets.Twitter.Resources.Views.TwitterTemplate.ascx";
        private const int DEFAULT_TWEETS_COUNT = 2;

        #endregion

        #region Properties

        [MultilingualProperty]
        public string Title
        {
            get;
            set;
        }

        [MultilingualProperty]
        public string TwitterProfileName
        {
            get;
            set;
        }

        public int NumberOfTweetsToShow
        {
            get;
            set;
        }

        /// <summary>
        /// Gets the name of the embedded layout template.
        /// </summary>
        /// <value></value>
        /// <remarks>
        /// Override this property to change the embedded template to be used with the dialog
        /// </remarks>
        protected override string LayoutTemplateName
        {
            get
            {
                return layoutTemplateName;
            }
        }

        #endregion

        #region Control references

        protected virtual ITextControl ContentTitle
        {
            get
            {
                return this.Container.GetControl<ITextControl>("contentTitle", true);
            }
        }

        protected virtual RadListView ContentTweetsList
        {
            get
            {
                return this.Container.GetControl<RadListView>("contentTweetsList", true);
            }
        }

        protected virtual HyperLink ContentFollowMeHyperlink
        {
            get
            {
                return this.Container.GetControl<HyperLink>("contentFollowMeHyperlink", true);
            }
        }

        #endregion

        #region Overridden controls

        /// <summary>
        /// Renders the HTML opening tag of the control to the specified writer. This method is used primarily by control developers.
        /// </summary>
        /// <param name="writer">A <see cref="T:System.Web.UI.HtmlTextWriter"/> that represents the output stream to render HTML content on the client.</param>
        public override void RenderBeginTag(HtmlTextWriter writer)
        {
        }

        /// <summary>
        /// Renders the HTML closing tag of the control into the specified writer. This method is used primarily by control developers.
        /// </summary>
        /// <param name="writer">A <see cref="T:System.Web.UI.HtmlTextWriter"/> that represents the output stream to render HTML content on the client.</param>
        public override void RenderEndTag(HtmlTextWriter writer)
        {
        }

        protected override void InitializeControls(GenericContainer container)
        {
            
        }

        protected override void CreateChildControls()
        {
            base.CreateChildControls();

            this.ContentTitle.Text = this.Title;

            //if Twitter Page URL is not set then the box will be invisible
            if (!this.TwitterProfileName.IsNullOrWhitespace())
            {
                //set navigation URL on 'Follow Me' link
                this.ContentFollowMeHyperlink.NavigateUrl =
                    string.Format("http://twitter.com/{0}", this.TwitterProfileName);

                //if Twitter Page URL is not set then the box will be invisible
                //if (!this.TwitterProfileName.IsNullOrWhitespace())
                //{
                BindTweets();
                //}
            }
            else
            {
                if (!this.IsBackend() && !this.IsDesignMode())
                {
                    this.Visible = false;
                }
            }
        }

        #endregion

        private void BindTweets()
        {
            List<Tweet> list = new List<Tweet>();

            if (HttpContext.Current != null)
            {
                try
                {
                    XDocument xdocTweets = new XDocument();

                    //if the tweets number is not set then it will get two because is default value 
                    if (this.NumberOfTweetsToShow <= 0)
                    {
                        this.NumberOfTweetsToShow = DEFAULT_TWEETS_COUNT;
                    }

                    // get data from api.twitter.com
                    var url = string.Format("http://api.twitter.com/1/statuses/user_timeline.rss?screen_name={0}&count={1}",
                                            this.TwitterProfileName, this.NumberOfTweetsToShow);

                    xdocTweets = XDocument.Load(url);

                    if (xdocTweets != null)
                    {
                        //get list of tweets with properties - title, source and publication date 
                        list = (from e in xdocTweets.Descendants("item")
                                select new Tweet()
                                {
                                    Title = FormatTitle(e.Descendants("title").FirstOrDefault().Value),
                                    Source = e.Descendants(XName.Get("source", "http://api.twitter.com")).FirstOrDefault().Value,
                                    PublishedDate = FormatDate(Convert.ToDateTime((e.Descendants("pubDate").FirstOrDefault().Value)))
                                }).ToList();
                    }
                }
                catch
                {
                    return;
                }
            }

            this.ContentTweetsList.DataSource = list;
            this.ContentTweetsList.DataBind();
        }

        /// <summary>
        /// Format all twitter reserved words to real html links
        /// </summary>
        /// <param name="text"></param>
        /// <returns>formated html title text</returns>
        private string FormatTitle(string text)
        {
            //remove twitter user name before message
            if (text.ToLower().StartsWith(this.TwitterProfileName.ToLower()))
            {
                text = text.Remove(0, (this.TwitterProfileName.Count() + 1));
            }

            //replace all urls to html links
            text = Regex.Replace(text,
                            @"[A-Za-z]+://[^\s]+",
                            u => string.Format("<a href=\"{0}\">{0}</a>", u.Value),
                            RegexOptions.Compiled | RegexOptions.IgnoreCase);

            //replace all twitter names like '@name' to html links
            text = Regex.Replace(text,
                            @"@[A-Za-z0-9_]+",
                            u => string.Format("@<a href=\"http://twitter.com/{0}\">{0}</a>", u.Value.Replace("@", string.Empty)),
                            RegexOptions.Compiled | RegexOptions.IgnoreCase);

            //replace all twitter search words like '#search_word' to html links
            text = Regex.Replace(text,
                            @"#[A-Za-z0-9_]+",
                            u => string.Format("<a href=\"http://twitter.com/search?q={0}\">{0}</a>", u.Value),
                            RegexOptions.Compiled | RegexOptions.IgnoreCase);

            return text;
        }

        /// <summary>
        /// Format date (like minute ago, hour ago and etc) to string.
        /// </summary>
        /// <param name="date"></param>
        /// <returns>formatted date string</returns>
        private string FormatDate(DateTime date)
        {
            string resultDate = string.Empty;

            if (DateTime.Compare(DateTime.Now, date) >= 0)
            {
                TimeSpan ts = DateTime.Now.Subtract(date);
                if (ts.Days.Equals(0))
                {
                    if (ts.Hours.Equals(0))
                    {
                        if (ts.Minutes.Equals(1))
                        {
                            resultDate = string.Format("{0} minute ago", ts.Minutes);
                        }
                        else
                        {
                            resultDate = string.Format("{0} minutes ago", ts.Minutes);
                        }
                    }
                    else
                    {
                        if (ts.Hours.Equals(1))
                        {
                            resultDate = string.Format("{0} hour ago", ts.Hours);
                        }
                        else
                        {
                            resultDate = string.Format("{0} hours ago", ts.Hours);
                        }
                    }
                }
                else
                {
                    if (date.Year.Equals(DateTime.Now.Year))
                    {
                        resultDate = date.ToString("d MMM", new CultureInfo("en-US", false));
                    }
                    else
                    {
                        resultDate = date.ToString("d MMM yyyy", new CultureInfo("en-US", false));
                    }
                }
            }

            return resultDate;
        }
    }
}
