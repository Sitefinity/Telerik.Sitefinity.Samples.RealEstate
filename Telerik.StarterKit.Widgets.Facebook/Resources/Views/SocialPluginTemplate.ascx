<%@ Control Language="C#" %>

<div class="block">
	<h2 id="itemTitle" runat="server">
        <asp:Literal ID="contentTitle" runat="server" />
    </h2>
	<div class="block-content">
		<div class="item">
			<iframe id="contentIFrame" runat="server" 
                scrolling="no" 
                frameborder="0" 
                allowTransparency="true"
                style="border:none; overflow:hidden; width:276px; height:287px;">
            </iframe>
		</div>
        <asp:HyperLink ID="contentFindUsHyperlink" runat="server" CssClass="arrow-link" Target="_blank" 
            Text="Find us on Facebook" />
	</div>
</div>