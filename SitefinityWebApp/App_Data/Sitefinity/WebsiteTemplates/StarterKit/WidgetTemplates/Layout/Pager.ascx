<%@ Control Language="C#" AutoEventWireup="true" Inherits="Templates_Pager" Codebehind="Pager.ascx.cs" %>



<%@ Register Assembly="Telerik.Sitefinity, Version=6.3.5000.0, Culture=neutral, PublicKeyToken=b28c218413bdf563" Namespace="Telerik.Sitefinity.Web.UI" TagPrefix="sf" %>


<div class="clear border-top">
    <sf:SitefinityHyperLink ID="cmdFirst" runat="server" Text="<%$Resources:Labels, Paging_First %>" />
    <sf:SitefinityHyperLink ID="cmdPrev" runat="server" Text="<%$Resources:Labels, Paging_Prev %>" />
    <div runat="server" id="numeric" class="sf_pagerNumeric"></div>
    <sf:SitefinityHyperLink ID="cmdNext" runat="server" Text="<%$Resources:Labels, Paging_Next %>" />
    <sf:SitefinityHyperLink ID="cmdLast" runat="server" Text="<%$Resources:Labels, Paging_Last %>" />
</div>