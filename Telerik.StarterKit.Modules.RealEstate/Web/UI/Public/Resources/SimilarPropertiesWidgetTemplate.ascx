<%@ Control Language="C#" %>



<%@ Register Assembly="Telerik.Web.UI, Version=2013.3.1114.40, Culture=neutral, PublicKeyToken=121fae78165ba3d4" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register Assembly="Telerik.Sitefinity, Version=6.3.5000.0, Culture=neutral, PublicKeyToken=b28c218413bdf563" Namespace="Telerik.Sitefinity.Web.UI" TagPrefix="sf" %>

<sf:ResourceLinks id="resourceLinks" runat="server">
 <sf:ResourceFile JavaScriptLibrary="JQuery" />
 <sf:ResourceFile Name="Telerik.StarterKit.Modules.RealEstate.Resources.Scripts.jquery.jcarousel.min.js" AssemblyInfo="Telerik.StarterKit.Modules.RealEstate.Web.UI.Public.SimilarPropertiesWidget, Telerik.StarterKit.Modules.RealEstate"  />
</sf:ResourceLinks>
<telerik:RadListView ID="ItemsList" runat="server" ItemPlaceholderID="ItemsContainer" EnableEmbeddedSkins="false" EnableEmbeddedBaseStylesheet="false">
    <LayoutTemplate>

	    <div class="content more-properties self-clear">
		    <div class="section-heading self-clear">
			    <h2>More properties in this area</h2>
		    </div>
		    <div class="carousel">
			    <ul>
                    <asp:PlaceHolder ID="ItemsContainer" runat="server" />
			    </ul>
		    </div>
	    </div>

    </LayoutTemplate>
    <ItemTemplate>
		<li>
			<asp:HyperLink ID="hlPhoto" runat="server"><asp:Image ID="imgThumbnail" runat="server" /></asp:HyperLink>
			<h3><asp:HyperLink ID="hlDetails" runat="server" /></h3>
		</li>

    </ItemTemplate>
</telerik:RadListView>

<script language="javascript" type="text/javascript">
$(document).ready(function(){
    	// globals
	var speed = 500,
		speedFast = Math.round(speed/2);
	
	/*---------------------------------------------------------------------------------- CAROUSELS */
	
	$('.more-properties .carousel').jcarousel({
		scroll:1,
		duration:speedFast
	});
	
});
</script>