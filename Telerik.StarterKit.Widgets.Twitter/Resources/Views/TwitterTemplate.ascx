<%@ Control Language="C#" %>

<div class="block block-twitter">
	<h2 id="itemTitle" runat="server">
        <asp:Literal ID="contentTitle" runat="server" />
    </h2>

    <telerik:RadListView ID="contentTweetsList" runat="server" ItemPlaceholderID="ItemsContainer" EnableEmbeddedSkins="false" EnableEmbeddedBaseStylesheet="false">
        <LayoutTemplate>
	        <div class="block-content">
                <asp:PlaceHolder ID="ItemsContainer" runat="server" />
		    </div>
        </LayoutTemplate>
        <ItemTemplate>
            <div class="item">
			    <%# Eval("Title")%>
			    <span class="date">
                    <%# Eval("PublishedDate")%>, via <%# Eval("Source")%>
                </span>
		    </div>
        </ItemTemplate>
    </telerik:RadListView>

    <asp:HyperLink ID="contentFollowMeHyperlink" runat="server" Text="Follow Me on Twitter" />
</div>