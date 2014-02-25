/// <reference name="MicrosoftAjax.js"/>
/// <reference name="Telerik.Sitefinity.Resources.Scripts.jquery-1.3.2.min-vsdoc2.js" assembly="Telerik.Sitefinity.Resources"/>
/// <reference name="Telerik.Sitefinity.Web.UI.ControlDesign.Scripts.PageSelector.js" assembly="Telerik.Sitefinity"/>
Type.registerNamespace("Telerik.StarterKit.Modules.RealEstate.Web.UI.Public.Designers");

// ------------------------------------------------------------------------
// ContentBlockDesigner class
// ------------------------------------------------------------------------
Telerik.StarterKit.Modules.RealEstate.Web.UI.Public.Designers.SliderWidgetDesigner = function (element) {
    this._propertyEditor = null;
    this._titleEditor = null;
    this._facebookPageUrlEditor = null;
    Telerik.StarterKit.Modules.RealEstate.Web.UI.Public.Designers.SliderWidgetDesigner.initializeBase(this, [element]);

    //  Page Selector begin
    this._parentDesigner = null;
    this._googleAnalyticsCodeTextField = null;
    this._scriptEmbedPositionChoiceField = null;
    this._toogleGroupSettingsDelegate = null;
    this._pageSelectButtonForRent = null;
    this._pageSelectorForRent = null;
    this._radWindowManager = null;
    this._showPageSelectorDelegateForRent = null;
    this._pageSelectedDelegateForRent = null;
    this._processedForRent = false;
    this._processedForSale = false;
    // Page selector addition end
}

Telerik.StarterKit.Modules.RealEstate.Web.UI.Public.Designers.SliderWidgetDesigner.prototype = {

    /* ************************* set up and tear down ************************* */
    initialize: function () {
        Telerik.StarterKit.Modules.RealEstate.Web.UI.Public.Designers.SliderWidgetDesigner.callBaseMethod(this, 'initialize');

        // Page Selector begin
        this._toogleGroupSettingsDelegate = Function.createDelegate(this, function () {
            dialogBase.resizeToContent();
        });

        this._showPageSelectorDelegateForRent = Function.createDelegate(this, this._showPageSelectorForRent);
        $addHandler(this._pageSelectButtonForRent, "click", this._showPageSelectorDelegateForRent);
        this._pageSelectedDelegateForRent = Function.createDelegate(this, this._pageSelectedForRent);
        this._pageSelectorForRent.add_doneClientSelection(this._pageSelectedDelegateForRent);

        this._showPageSelectorDelegateForSale = Function.createDelegate(this, this._showPageSelectorForSale);
        $addHandler(this._pageSelectButtonForSale, "click", this._showPageSelectorDelegateForSale);
        this._pageSelectedDelegateForSale = Function.createDelegate(this, this._pageSelectedForSale);
        this._pageSelectorForSale.add_doneClientSelection(this._pageSelectedDelegateForSale);

        //jQuery("#expanderDesignSettings").click(this._toogleDesignSettingsDelegate);
        jQuery("#showSelectorForRent").click(function () {

            if (!this._processedForRent || typeof (this._processedForRent) == "undefined") {
                if ($(this).parents(".sfExpandableSection:first").hasClass("sfExpandedSection")) {
                    $(this).parents(".sfExpandableSection:first").removeClass("sfExpandedSection");
                }
                else {
                    $(this).parents(".sfExpandableSection:first").addClass("sfExpandedSection");
                }

                this._processedForRent = true;
            }
            else {
                this._processedForRent = false;
            }

            dialogBase.resizeToContent();
        });

        jQuery("#showSelectorForSale").click(function () {

            if (!this._processedForSale || typeof (this._processedForSale) == "undefined") {
                if ($(this).parents(".sfExpandableSection:first").hasClass("sfExpandedSection")) {
                    $(this).parents(".sfExpandableSection:first").removeClass("sfExpandedSection");
                }
                else {
                    $(this).parents(".sfExpandableSection:first").addClass("sfExpandedSection");
                }

                this._processedForSale = true;
            }
            else {
                this._processedForSale = false;
            }

            dialogBase.resizeToContent();
        });
        // Page selector end
    },
    dispose: function () {
        Telerik.StarterKit.Modules.RealEstate.Web.UI.Public.Designers.SliderWidgetDesigner.callBaseMethod(this, 'dispose');
    },

    /* ************************* public methods ************************* */
    applyChanges: function () {

        var controlData = this.get_propertyEditor().get_control();
        controlData.NumberOfItemsToShow = $("#numberOfItemsToShowEditor").val();

        // Page Selector
        controlData.ForRentPageId = $("#forRentPageId").val();
        //alert("applyChanges: " + controlData.ForRentPageId);
        //        alert("applyChanges 1: " + $("#PageSelectionValue").val());
        //        alert("applyChanges 2: " + controlData.PageURL);
        controlData.ForSalePageId = $("#forSalePageId").val();
    },

    refreshUI: function () {
        var data = this.get_propertyEditor().get_control();
        $("#numberOfItemsToShowEditor").val(data.NumberOfItemsToShow);

        if (data.ForRentPageId) {
            $("#forRentPageId").val(data.ForRentPageId);
            //            alert("refreshUI1: " + data.ForRentPageId);
            //            alert("refreshUI2: " + $("#forRentPageId").val());
            $("#selectedPageLabelForRent").html(data.ForRentPageTitle);
            $("#selectedPageLabelForRent").show();
        }

        if (data.ForSalePageId) {
            $("#forSalePageId").val(data.ForSalePageId);
            //            alert("refreshUI1: " + data.ForSalePageId);
            //            alert("refreshUI2: " + $("#forSalePageId").val());
            $("#selectedPageLabelForSale").html(data.ForSalePageTitle);
            $("#selectedPageLabelForSale").show();
        }
    },
    // Page Selection begin
    /* --------------------------------- event handlers --------------------------------- */
    _pageSelectedForRent: function (items) {
        //alert("_pageSelectedForRent");
        $(this.get_element()).find('#selectorTagForRent').hide();

        if (items == null)
            return;

        var selectedItem = this.get_pageSelectorForRent().get_selectedPage();

        if (selectedItem) {
            //alert("_pageSelected: " + selectedItem.Id);
            $("#forRentPageId").val(selectedItem.Id);
            $("#selectedPageLabelForRent").html(selectedItem.Title)
            $("#selectedPageLabelForRent").show();
            this.get_pageSelectButtonForRent().innerHTML = 'Change page';
            this.get_controlData().ForRentPageId = selectedItem.Id;
            this.get_controlData().ForRentPageTitle = selectedItem.Title;
        }

        dialogBase.resizeToContent();
    },

    _pageSelectedForSale: function (items) {
        //alert("_pageSelectedForSale");
        $(this.get_element()).find('#selectorTagForSale').hide();

        if (items == null)
            return;

        var selectedItem = this.get_pageSelectorForSale().get_selectedPage();

        if (selectedItem) {
            //alert("_pageSelected: " + selectedItem.Id);
            $("#forSalePageId").val(selectedItem.Id);
            $("#selectedPageLabelForSale").html(selectedItem.Title)
            $("#selectedPageLabelForSale").show();
            this.get_pageSelectButtonForSale().innerHTML = 'Change page';
            this.get_controlData().ForSalePageId = selectedItem.Id;
            this.get_controlData().ForSalePageTitle = selectedItem.Title;
        }

        dialogBase.resizeToContent();
    },

    _showPageSelectorForRent: function () {
        var selectedItem = this.get_controlData().ForRentPageId;
        if (selectedItem && selectedItem != "00000000-0000-0000-0000-000000000000") {
            this.get_pageSelectorForRent().set_selectedPageId(selectedItem);
        }
        jQuery(this.get_element()).find('#selectorTagForRent').show();
        dialogBase.resizeToContent();
    },

    _showPageSelectorForSale: function () {
        var selectedItem = this.get_controlData().ForSalePageId;
        if (selectedItem && selectedItem != "00000000-0000-0000-0000-000000000000") {
            this.get_pageSelectorForSale().set_selectedPageId(selectedItem);
        }
        jQuery(this.get_element()).find('#selectorTagForSale').show();
        dialogBase.resizeToContent();
    },
    /* --------------------------------- private methods --------------------------------- */

    /* --------------------------------- properties --------------------------------- */

    // gets the javascript control object that is being designed
    get_controlData: function () {
        return this.get_propertyEditor().get_control();
    },

    // gets the reference to the propertyEditor control
    get_propertyEditor: function () {
        return this._propertyEditor;
    },
    // sets the reference fo the propertyEditor control
    set_propertyEditor: function (value) {
        this._propertyEditor = value;
    },

    // get the reference to the button that opens page selector
    get_pageSelectButtonForRent: function () {
        if (this._pageSelectButtonForRent == null) {
            this._pageSelectButtonForRent = $get("pageSelectButtonForRent");
        }
        return this._pageSelectButtonForRent;
    },

    // sets the reference to the button that opens page selector
    set_pageSelectButtonForRent: function (value) {
        this._pageSelectButtonForRent = value;
    },

    // gets the reference to the page selector used to choose a page for showing the detail mode
    get_pageSelectorForRent: function () {
        return this._pageSelectorForRent;
    },

    // sets the reference to the page selector used to choose a page for showing the detail mode
    set_pageSelectorForRent: function (value) {
        this._pageSelectorForRent = value;
    },

    // get the reference to the button that opens page selector
    get_pageSelectButtonForSale: function () {
        if (this._pageSelectButtonForSale == null) {
            this._pageSelectButtonForSale = $get("pageSelectButtonForSale");
        }
        return this._pageSelectButtonForSale;
    },

    // sets the reference to the button that opens page selector
    set_pageSelectButtonForSale: function (value) {
        this._pageSelectButtonForSale = value;
    },

    // gets the reference to the page selector used to choose a page for showing the detail mode
    get_pageSelectorForSale: function () {
        return this._pageSelectorForSale;
    },

    // sets the reference to the page selector used to choose a page for showing the detail mode
    set_pageSelectorForSale: function (value) {
        this._pageSelectorForSale = value;
    },

    get_radWindowManager: function () {
        return this._radWindowManager;
    },

    set_radWindowManager: function (value) {
        if (this._radWindowManager != value) {
            this._radWindowManager = value;
        }
    }
    // Page Selector end
};
Telerik.StarterKit.Modules.RealEstate.Web.UI.Public.Designers.SliderWidgetDesigner.registerClass('Telerik.StarterKit.Modules.RealEstate.Web.UI.Public.Designers.SliderWidgetDesigner', Telerik.Sitefinity.Web.UI.ControlDesign.ControlDesignerBase);

if (typeof (Sys) !== 'undefined') Sys.Application.notifyScriptLoaded();

function setValueForNull(in_value) {
    if (in_value == null) {
        return "";
    }
    else {
        return in_value;
    }
}