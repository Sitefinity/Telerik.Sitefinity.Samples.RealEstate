<%@ Control Language="C#" %>



<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>

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