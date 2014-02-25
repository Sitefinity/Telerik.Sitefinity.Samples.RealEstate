<%@ Register Assembly="Telerik.Web.UI, Version=2013.3.1114.40, Culture=neutral, PublicKeyToken=121fae78165ba3d4" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>



<div>
    <h1 id="sfToMainContent" class="sfBreadCrumb">
        Template Importer
        <span class="sfBreadCrumbBack"></span>
    </h1>
    <asp:Image ID="importerImage" runat="server" ImageUrl="~/SFTemplateImporter/TemplateImporter.Web.Controls.importer_bg.jpg" CssClass="bgimage" />

    <h2 id="hint">
    The Template Importer Module can only import templates created using the Sitefinity Template Builder. 
    More information can be found on the <a href="http://www.sitefinity.com/resources/template-builder.aspx">sitefinity.com website</a>.
    </h2>

    <style>
        
        body 
        {
            background-color: #242424;
        }
    h1.sfBreadCrumb {
        border-bottom: 1px solid #888888;
        
    }
    
    h2#hint 
    {
        position: absolute;
        left: 50%;
        width: 500px;
        margin-left: -250px;
        color: #999;
        font-size: 11px;
        padding: 0;
        margin-left: -250px;
        text-align: center;
        top: 277px;
    }
    h2#hint a 
    {
        color: #23AFF8 !important;
    }
    
    .bgimage 
    {
        position: absolute;
        left: 50%;
        margin-left: -470px;
    }
    .sfWorkArea 
    {
        position:absolute;
        width: 400px;
        left: 50%;
        margin-left: -150px;
        top: 320px;
    }
    .RadButton.RadButton_Default.rbSkinnedButton 
    {
        background-image: none;
        margin-left: 55px;
        margin-top: 12px;
    }
    .RadButton.RadButton_Default.rbSkinnedButton .rbDecorated
    {
        position: relative;
        float: left;
        background-image: none;
        background-color: #666;
        font-family: Arial;
        font-size: 12px;
        border-radius: 5px;
        color: White;
        height: 27px;
        width: 118px;
    }
    
    .RadButton.RadButton_Default.rbSkinnedButton .rbDecorated:hover
    {
        background-color: #689B30;
    }
    
    .RadUpload .ruFileWrap 
    {
        height: 27px !important;
    }
    .ruFakeInput
    {
        background-image: none !important;
        background-color: #262626 !important;
        border: 1px solid #757575 !important;
        border-radius: 5px;
        padding-top: 3px !important;
        padding-bottom: 3px !important;
        padding-left: 15px !important;
        color: White !important;
        margin-top: 2px;
    }
    .ruFileWrap.ruStyled .ruButton.ruBrowse
    {
        background-image: none !important;
        background-color: #23aff8 !important;
        border: 0px !important;
        color: White;
        font-size: 12px;
        font-family: Arial;
        border-radius: 5px;
        height: 27px !important;
        width: 70px;
        padding-top: 3px !important;
        padding-bottom: 3px !important;
    }
    </style>
    <div class="sfMain sfClearfix">
        <div class="sfWorkArea">
            <div class="sfClearfix sfHighlights">
                <div class="sfColLimit">
                    <div class="sfDashColLeft">
                        <p>
                            <asp:Label ID="ErrorLabel" runat="server" Text=""></asp:Label>
                        </p>
                        <telerik:RadUpload ID="RadUploadControl" Runat="server" Skin="Default" ControlObjectsVisibility = "None" AllowedFileExtensions=".zip"/>
                        
                        <telerik:RadButton ID="ImportButton" runat="server" Skin="Default" Text="Import template" />
                        
                    </div>
                </div>
             </div>
         </div>
    </div>
</div>


