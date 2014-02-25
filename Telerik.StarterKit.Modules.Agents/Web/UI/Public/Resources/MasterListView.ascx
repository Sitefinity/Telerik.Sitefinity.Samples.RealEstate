<%@ Control Language="C#" %>



<%@ Register Assembly="Telerik.Sitefinity, Version=6.3.5000.0, Culture=neutral, PublicKeyToken=b28c218413bdf563" Namespace="Telerik.Sitefinity.Web.UI.ContentUI" TagPrefix="sf" %>
<%@ Register Assembly="Telerik.Sitefinity, Version=6.3.5000.0, Culture=neutral, PublicKeyToken=b28c218413bdf563" Namespace="Telerik.Sitefinity.Web.UI" TagPrefix="sf" %>
<%@ Register Assembly="Telerik.Web.UI, Version=2013.3.1114.40, Culture=neutral, PublicKeyToken=121fae78165ba3d4" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>

<sf:ResourceLinks id="resourceLinks" runat="server">
 <sf:ResourceFile JavaScriptLibrary="JQuery" />
 <sf:ResourceFile JavaScriptLibrary="JQueryFancyBox"></sf:ResourceFile>
</sf:ResourceLinks>

<h1><%$Resources:AgentsResources, AgentsListTitle %></h1>

<telerik:RadListView ID="AgentsList" ItemPlaceholderID="ItemsContainer" GroupPlaceholderID="GroupContainer" GroupItemCount="3" runat="server" EnableEmbeddedSkins="false" EnableEmbeddedBaseStylesheet="false">
    <LayoutTemplate>
        <div class="agents">
            <asp:PlaceHolder ID="GroupContainer" runat="server" />
        </div>
    </LayoutTemplate>
    <GroupTemplate>
        <div class="row">
            <asp:PlaceHolder ID="ItemsContainer" runat="server" />
        </div>
    </GroupTemplate>
    <ItemTemplate>
        <div class="column-small">
            <div class="block">
                <p><sf:DetailsViewHyperLink ID="DetailsViewHyperLink1" runat="server"><div id="photoDiv" runat="server"></div></sf:DetailsViewHyperLink></p>
				<h3><sf:FieldListView ID="Title" runat="server" Text="{0}" Properties="Title" /></h3>
				<address>
					<sf:FieldListView ID="Address" runat="server" Text="{0}" Properties="Address" /><br />
					<sf:FieldListView ID="PostalCode" runat="server" Text="{0}" Properties="PostalCode" /><br />
					<sf:FieldListView ID="PhoneNumber" runat="server" Text="{0}" Properties="PhoneNumber" />
				</address>
				<sf:DetailsViewHyperLink ID="DetailsViewHyperLink2" ToolTipDataField="Description" runat="server" Text="<%$Resources:AgentsResources, ContactAgent %>" />
			</div>
        </div>
    </ItemTemplate>
</telerik:RadListView>

<sf:Pager id="pager" runat="server" LayoutTemplatePath="~/App_Data/Sitefinity/WebsiteTemplates/StarterKit/WidgetTemplates/Layout/Pager.ascx"></sf:Pager>

<script language="javascript" type="text/javascript">
    $(document).ready(function () {

        // globals
        var speed = 500, 
    		speedFast = Math.round(speed / 2);
        jQuery('.agents a').fancybox({
            padding: 30,
            autoDimensions: false,
            width: 780,
            height: 575,
            overlayColor: '#000',
            overlayOpacity: 0.7,
            speedIn: speedFast,
            speedOut: speedFast,
            changeSpeed: speedFast,
            centerOnScroll: true,
            onComplete: function () {
                var div = $('<div style="display:none" />').appendTo(document.body);
                var fb = $('#fancybox-content');
                fb.find('.agent-single').appendTo(div);
                fb.html('');
                fb.append(div.children());
            }
        });
    });
</script>