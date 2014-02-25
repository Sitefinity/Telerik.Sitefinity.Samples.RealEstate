<%@ Control Language="C#" %>



<%@ Register Assembly="Telerik.Sitefinity, Version=6.3.5000.0, Culture=neutral, PublicKeyToken=b28c218413bdf563" Namespace="Telerik.Sitefinity.Web.UI.ContentUI" TagPrefix="sf" %>
<%@ Register Assembly="Telerik.Sitefinity, Version=6.3.5000.0, Culture=neutral, PublicKeyToken=b28c218413bdf563" Namespace="Telerik.Sitefinity.Web.UI" TagPrefix="sf" %>
<%@ Register Assembly="Telerik.Web.UI, Version=2013.3.1114.40, Culture=neutral, PublicKeyToken=121fae78165ba3d4" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>

<h1 class="section-title"><asp:Literal ID="Title" runat="server" /></h1>

<div class="sorting">
	<div class="left">
		<strong><%$Resources:RealEstateResources, SortBy %></strong>
		<asp:LinkButton ID="lbnSortBy_Price" runat="server" CommandArgument="Price"><span><%$Resources:RealEstateResources, Price %></span></asp:LinkButton>
		<asp:LinkButton Visible="false" ID="lbnSortBy_Location" runat="server" CommandArgument="Location"><span><%$Resources:RealEstateResources, Location %></span></asp:LinkButton>
	</div>
	<div class="right">
		<asp:LinkButton ID="lbnThumbnailView" runat="server" CssClass="thumbnails" CommandArgument="Thumbs"><span><%$Resources:RealEstateResources, ThumbnailsView %></span></asp:LinkButton>
		<asp:LinkButton ID="lbnListView" runat="server" CssClass="list" CommandArgument="Flow"><span><%$Resources:RealEstateResources, ListView %></span></asp:LinkButton>
	</div>
	<div class="center">
		<strong><%$Resources:RealEstateResources, ShowOnPage %></strong>
		<asp:LinkButton ID="lbnItemPerPage_Nine" runat="server" CommandArgument="9"><span><%$Resources:RealEstateResources, NineItems %></span></asp:LinkButton>
		<asp:LinkButton ID="lbnItemPerPage_Eighteen" runat="server" CommandArgument="18"><span><%$Resources:RealEstateResources, EighteenItems %></span></asp:LinkButton>
		<asp:LinkButton ID="lbnItemPerPage_Twentyseven" runat="server" CommandArgument="27"><span><%$Resources:RealEstateResources, TwentysevenItems %></span></asp:LinkButton>
	</div>
</div>

<telerik:RadListView ID="ItemsList_Flow" Visible="False" ItemPlaceholderID="ItemsContainer" GroupItemCount="3" runat="server" EnableEmbeddedSkins="false" EnableEmbeddedBaseStylesheet="false">
    <LayoutTemplate>
        <div class="for-rent">
            <asp:PlaceHolder ID="ItemsContainer" runat="server" />
        </div>
    </LayoutTemplate>
    <ItemTemplate>
        <div class="row">
            <sf:DetailsViewHyperLink ID="DetailsViewHyperLink1" runat="server"><asp:Image ID="Photo" runat="server" /></sf:DetailsViewHyperLink>
            <div>
                <h2><sf:DetailsViewHyperLink ID="DetailsViewHyperLink2" runat="server"><sf:FieldListView ID="Title" runat="server" Text="{0}" Properties="Title" /></sf:DetailsViewHyperLink></h2>
                <p class="price"><sf:FieldListView ID="Price" runat="server" Format="{Price:n0}" /></p>

                <telerik:RadListView ID="FeaturesList" runat="server" ItemPlaceholderID="FeaturesContainer" EnableEmbeddedSkins="false" EnableEmbeddedBaseStylesheet="false">
                    <LayoutTemplate>
				        <ul>
                            <asp:PlaceHolder ID="FeaturesContainer" runat="server" />
		                </ul>
                    </LayoutTemplate>
                    <ItemTemplate>
	                    <li><asp:Literal ID="ltrFeature" runat="server" /></li>
                    </ItemTemplate>
                </telerik:RadListView>

			    <p><sf:FieldListView ID="Description" runat="server" Text="{0}" Properties="Description" /></p>
			    <sf:DetailsViewHyperLink ID="DetailsViewHyperLink3" ToolTipDataField="Title" runat="server" Text="<%$Resources:RealEstateResources, SliderSeeDetails %>" />
		    </div>
        </div>
    </ItemTemplate>
</telerik:RadListView>

<telerik:RadListView ID="ItemsList_Thumb" Visible="False" ItemPlaceholderID="ItemsContainer" GroupPlaceholderID="GroupContainer" GroupItemCount="3" runat="server" EnableEmbeddedSkins="false" EnableEmbeddedBaseStylesheet="false">
    <LayoutTemplate>
        <div class="for-rent-thumbs">
            <asp:PlaceHolder ID="GroupContainer" runat="server" />
        </div>
    </LayoutTemplate>
    <GroupTemplate>
        <div class="row">
            <asp:PlaceHolder ID="ItemsContainer" runat="server" />
        </div>
    </GroupTemplate>
    <ItemTemplate>
        <div class="column">
            <div class="block">
                <div class="block-content">
                    <p><sf:DetailsViewHyperLink ID="DetailsViewHyperLink1" runat="server"><asp:Image ID="Photo" runat="server" /></sf:DetailsViewHyperLink></p>
                    <h3><sf:DetailsViewHyperLink ID="DetailsViewHyperLink2" runat="server"><sf:FieldListView ID="Title" runat="server" Text="{0}" Properties="Title" /></sf:DetailsViewHyperLink></h3>
                    <p class="price"><sf:FieldListView ID="Price" runat="server" Format="$ {Price:n0}" /></p>

                    <telerik:RadListView ID="FeaturesList" runat="server" ItemPlaceholderID="FeaturesContainer" EnableEmbeddedSkins="false" EnableEmbeddedBaseStylesheet="false">
                        <LayoutTemplate>
				            <ul>
                                <asp:PlaceHolder ID="FeaturesContainer" runat="server" />
		                    </ul>
                        </LayoutTemplate>
                        <ItemTemplate>
	                        <li><asp:Literal ID="ltrFeature" runat="server" /></li>
                        </ItemTemplate>
                    </telerik:RadListView>

		            <p><sf:FieldListView ID="Description" runat="server" Text="{0}" Properties="Description" /></p>
		            <sf:DetailsViewHyperLink ID="DetailsViewHyperLink3" ToolTipDataField="Title" runat="server" Text="<%$Resources:RealEstateResources, CarouselSeeDetails %>" />
                </div>
            </div>
        </div>
    </ItemTemplate>
</telerik:RadListView>

<sf:Pager id="pager" runat="server" LayoutTemplatePath="~/App_Data/Sitefinity/WebsiteTemplates/StarterKit/WidgetTemplates/Layout/Pager.ascx"></sf:Pager>