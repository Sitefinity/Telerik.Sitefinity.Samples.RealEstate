<%@ Register Assembly="Telerik.Web.UI, Version=2013.3.1114.40, Culture=neutral, PublicKeyToken=121fae78165ba3d4" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register Assembly="Telerik.Sitefinity, Version=6.3.5000.0, Culture=neutral, PublicKeyToken=b28c218413bdf563" Namespace="Telerik.Sitefinity.Web.UI" TagPrefix="sitefinity" %>
<%@ Register Assembly="Telerik.Sitefinity, Version=6.3.5000.0, Culture=neutral, PublicKeyToken=b28c218413bdf563" Namespace="Telerik.Sitefinity.Web.UI.ControlDesign" TagPrefix="designers" %>

<sitefinity:ResourceLinks id="resourcesLinks" runat="server">
   <sitefinity:ResourceFile Name="Styles/MaxWindow.css" />
   <sitefinity:ResourceFile JavaScriptLibrary="JQuery" />
</sitefinity:ResourceLinks>

<sitefinity:Message runat="server" ID="message" ElementTag="div" CssClass="sfMessage sfDesignerMessage" RemoveAfter="50000" 
FadeDuration="10"  />

<div id="selectorTag" style="display: none;" class="sfDesignerSelector sfFlatDialogSelector">
   <designers:ContentSelector 
       ID="selector" 
       runat="server" 
       ItemsFilter="Visible == true AND Status == Live" 
       TitleText="<%$Resources:Labels, ChooseNews %>" 
       BindOnLoad="false" 
       AllowMultipleSelection="false" 
       WorkMode="List" 
       SearchBoxInnerText="" 
       SearchBoxTitleText="<%$Resources:Labels, NarrowByTypingTitleOrAuthorOrDate %>" 
       ListModeClientTemplate="<strong class='sfItemTitle'>{{Title}}</strong><span class='sfDate'>{{PublicationDate ? PublicationDate.sitefinityLocaleFormat('dd MMM yyyy') : &quot;&quot;}} by {{Author}}</span>">
   </designers:ContentSelector>
</div>
<h2>
   <asp:Literal ID="choicesTitle" runat="server" Text="<%$Resources:Labels, WhichNewsToDisplay %>" /></h2>
<ul class="sfRadioList">
   <li>
      <asp:RadioButton runat="server" ID="contentSelect_AllItems" Checked="true" GroupName="ContentSelection" Text="<%$Resources:Labels, AllPublishedNews %>" />
   </li>
   <li>
      <asp:RadioButton runat="server" ID="contentSelect_OneItem" GroupName="ContentSelection" Text="<%$Resources:Labels, OneParticularNewsOnly %>" />
      <div class="sfExpandedPropertyDetails" id="selectorPanel" style="display:none"  >
         <asp:Label ID="selectedContentTitle" runat="server" Text="<%$Resources:Labels, NoContentSelected %>" CssClass="sfSelectedItem"></asp:Label>
         <span class="sfLinkBtn sfChange" runat="server" id="btnSelectSingleItemWrapper">
             <asp:LinkButton NavigateUrl="javascript:void(0)" runat="server" ID="btnSelectSingleItem" OnClientClick="return false;" CssClass="sfLinkBtnIn">
                <asp:Literal ID="btnSelectSingleItemLiteral" runat="server" Text="<%$Resources:Labels, SelectNews %>" />
             </asp:LinkButton>
         </span>
      </div>
   </li>
   <li>
      <asp:RadioButton runat="server" ID="contentSelect_SimpleFilter" GroupName="ContentSelection" Text="<%$Resources:Labels, SelectionOfNews %>" />
      <div id="selectorsPanel">
         <designers:FilterSelector ID="filterSelector" runat="server" AllowMultipleSelection="true" 
            ItemsContainerTag="ul" ItemTag="li" ItemsContainerCssClass="sfCheckListBox sfExpandedPropertyDetails" DisabledTextCssClass="sfTooltip">
            <Items>
                <designers:FilterSelectorItem ID="FilterSelectorItem1" runat="server" Text="<%$Resources:Labels, ByCategories %>"
                    GroupLogicalOperator="AND" ItemLogicalOperator="OR" ConditionOperator="Contains"
                    QueryDataName="Categories" QueryFieldName="Category" QueryFieldType="System.Guid">
                    <SelectorResultView>
                        <sitefinity:HierarchicalTaxonSelectorResultView ID="HierarchicalTaxonSelectorResultView1" runat="server" WebServiceUrl="~/Sitefinity/Services/Taxonomies/HierarchicalTaxon.svc"
                            AllowMultipleSelection="true" HierarchicalTreeRootBindModeEnabled="false">
                        </sitefinity:HierarchicalTaxonSelectorResultView>
                    </SelectorResultView>
                </designers:FilterSelectorItem>
                <designers:FilterSelectorItem ID="FilterSelectorItem2" runat="server" Text="<%$Resources:Labels, ByTags %>"
                    GroupLogicalOperator="AND" ItemLogicalOperator="OR" ConditionOperator="Contains"
                    QueryDataName="Types" QueryFieldName="Types" QueryFieldType="System.Guid">
                    <SelectorResultView>
                        <sitefinity:FlatTaxonSelectorResultView ID="FlatTaxonSelectorResultView1" runat="server" WebServiceUrl="~/Sitefinity/Services/Taxonomies/FlatTaxon.svc"
                            AllowMultipleSelection="true">
                        </sitefinity:FlatTaxonSelectorResultView>
                    </SelectorResultView>
                </designers:FilterSelectorItem>
                <designers:FilterSelectorItem ID="FilterSelectorItem3" runat="server" Text="<%$Resources:Labels, ByDates %>"
                    GroupLogicalOperator="AND" ItemLogicalOperator="AND" 
                    QueryDataName="Dates" QueryFieldName="PublicationDate" QueryFieldType="System.DateTime"
                    CollectionTranslatorDelegate="_translateQueryItems" 
                    CollectionBuilderDelegate="_buildQueryItems">
                    <SelectorResultView>
                        <sitefinity:DateRangeSelectorResultView ID="DateRangeSelectorResultView1" runat="server" SelectorDateRangesTitle="<%$Resources:Labels, DisplayNewsPublishedIn %>">
                        </sitefinity:DateRangeSelectorResultView>
                    </SelectorResultView>
                </designers:FilterSelectorItem>
            </Items>
         </designers:FilterSelector>
      </div>
   </li>
   <li style="display: none;">
      <asp:RadioButton runat="server" Enabled="false" ID="contentSelect_AdvancedFilter" GroupName="ContentSelection" Text="<%$Resources:Labels, AdvancedSelection %>" /><asp:Literal ID="Literal1" runat="server" Text="<%$Resources:Labels, InProcessOfImplementation %>" />
   </li>
</ul>