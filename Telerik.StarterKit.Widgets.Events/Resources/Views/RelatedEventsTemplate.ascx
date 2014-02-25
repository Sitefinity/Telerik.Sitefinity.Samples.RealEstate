<%@ Control Language="C#" %>



<%@ Register Assembly="Telerik.Web.UI, Version=2013.3.1114.40, Culture=neutral, PublicKeyToken=121fae78165ba3d4" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>

<telerik:RadListView ID="EventsList" runat="server" ItemPlaceholderID="ItemsContainer" EnableEmbeddedSkins="false" EnableEmbeddedBaseStylesheet="false">
    <LayoutTemplate>
		<div class="related-news-events">
			<h2 class="section-heading">Related Events</h2>
			<ul>
                <asp:PlaceHolder ID="ItemsContainer" runat="server" />
			</ul>
		</div>

    </LayoutTemplate>
    <ItemTemplate>
		<li><asp:HyperLink ID="hlEvent" runat="server" /></li>
    </ItemTemplate>
</telerik:RadListView>