<%@ Control Language="C#" AutoEventWireup="true" Inherits="Templates_Sitemap" Codebehind="Sitemap.ascx.cs" %>

<h2 class="section-heading">Sitemap</h2>
<div class="sitemap">
    <ul>
        <asp:ListView ID="lvPages" runat="server" 
            OnItemDataBound="lvPages_ItemDataBound">
            <ItemTemplate>
			    <li><asp:HyperLink ID="hlPage" runat="server" />
                                
                    <asp:ListView ID="lvPages" runat="server" 
                        OnItemDataBound="lvPages_ItemDataBound"
                        ItemPlaceholderID="Item">
                        <LayoutTemplate>
			                <div class="block-content">
				                <ul>
                                    <asp:PlaceHolder ID="Item" runat="server" />
                                </ul>
			                </div>
                        </LayoutTemplate>
                        <ItemTemplate>
                            <li id="liPage" runat="server">
                                <asp:HyperLink ID="hlPage" runat="server" />

                                <asp:ListView ID="lvPages" runat="server" 
                                    OnItemDataBound="lvPages_ItemDataBound"
                                    ItemPlaceholderID="Item">
                                    <LayoutTemplate>
			                            <div class="block-content">
				                            <ul>
                                                <asp:PlaceHolder ID="Item" runat="server" />
                                            </ul>
			                            </div>
                                    </LayoutTemplate>
                                    <ItemTemplate>
                                        <li id="liPage" runat="server">
                                            <asp:HyperLink ID="hlPage" runat="server" />
                                        </li>
                                    </ItemTemplate>
                                </asp:ListView>
                            </li>
                        </ItemTemplate>
                    </asp:ListView>

                </li>
            </ItemTemplate>
        </asp:ListView>
    </ul>
</div>
