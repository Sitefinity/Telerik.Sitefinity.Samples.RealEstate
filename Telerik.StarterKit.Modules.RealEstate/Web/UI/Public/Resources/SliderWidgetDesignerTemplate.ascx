<%@ Control Language="C#" %>



<%@ Register Assembly="Telerik.Sitefinity" Namespace="Telerik.Sitefinity.Web.UI" TagPrefix="sf" %>
<%@ Register Assembly="Telerik.Sitefinity" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register Assembly="Telerik.Sitefinity" Namespace="Telerik.Sitefinity.Web.UI.Fields" TagPrefix="sf" %>
<%@ Register Assembly="Telerik.Sitefinity" Namespace="Telerik.Sitefinity.Web.UI.ControlDesign" TagPrefix="designers" %>
<sf:ResourceLinks id="resourceLinks" runat="server">
 <sf:ResourceFile JavaScriptLibrary="JQuery" />
</sf:ResourceLinks>
<sf:FormManager ID="formManager" runat="server" />

<input type="hidden" id="forRentPageId" value="00000000-0000-0000-0000-000000000000" />
<input type="hidden" id="forSalePageId" value="00000000-0000-0000-0000-000000000000" />

<ul class="sfTargetList">
    <li>
        <label for="numberOfItemsToShowEditor">Number of items to show</label>
        <input type="text" id="numberOfItemsToShowEditor" class="sfTxt" />
    </li>
    <li>

        
<telerik:RadWindowManager ID="windowManagerForRent" runat="server"
    Height="100%"
    Width="100%"
    Behaviors="None"
    Skin="Sitefinity"
    ShowContentDuringLoad="false"
    VisibleStatusBar="false">
    <Windows>
        <telerik:RadWindow ID="widgetEditorDialog" runat="server" Height="100" Width="100" ReloadOnShow="true" Behaviors="Close" Modal="true" />
    </Windows>
</telerik:RadWindowManager>

<div id="selectorTagForRent" style="display: none;" class="sfDesignerSelector">
    <designers:PageSelector runat="server" ID="pageSelectorForRent" AllowExternalPagesSelection="false" AllowMultipleSelection="false" />
</div>

<div class="sfExpandableSection">
    <h3><a href="javascript:void(0);" class="sfMoreDetails" id="showSelectorForRent">For Rent Page</a></h3>
    <ul class="sfTargetList" id="forRentSelectorUl">
        <li>
            <div id="pageSelectZoneForRent" class="sfExpandedPropertyDetails">
                <span style="display: none;" class="sfSelectedItem" id="selectedPageLabelForRent">Page not selected</span>
                <span class="sfLinkBtn sfChange"><asp:LinkButton CssClass="sfLinkBtnIn" ID="pageSelectButtonForRent" runat="server">Select page</asp:LinkButton></span>
            </div>
        </li>
    </ul>
</div>

<div id="selectorTagForSale" style="display: none;" class="sfDesignerSelector">
    <designers:PageSelector runat="server" ID="pageSelectorForSale" AllowExternalPagesSelection="false" AllowMultipleSelection="false" />
</div>

<div class="sfExpandableSection">
    <h3><a href="javascript:void(0);" class="sfMoreDetails" id="showSelectorForSale">For Sale Page</a></h3>
    <ul class="sfTargetList" id="forSaleSelectorUl">
        <li>
            <div id="pageSelectZoneForSale" class="sfExpandedPropertyDetails">
                <span style="display: none;" class="sfSelectedItem" id="selectedPageLabelForSale">Page not selected</span>
                <span class="sfLinkBtn sfChange"><asp:LinkButton CssClass="sfLinkBtnIn" ID="pageSelectButtonForSale" runat="server">Select page</asp:LinkButton></span>
            </div>
        </li>
    </ul>
</div>
        
        <!-- Page Selector additions end -->
    </li>
</ul>

<script type="text/javascript">
    $("body").addClass("sfContentBlockDesigner");
</script>