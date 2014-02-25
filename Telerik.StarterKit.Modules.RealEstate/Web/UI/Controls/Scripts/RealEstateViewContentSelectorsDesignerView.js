/// <reference name="MicrosoftAjax.js"/>
/// <reference name="Telerik.Sitefinity.Resources.Scripts.jquery-1.5.2-vsdoc.js" assembly="Telerik.Sitefinity.Resources"/>
Type._registerScript("RealEstateViewContentSelectorsDesignerView.js", ["IDesignerViewControl.js"]);
Type.registerNamespace("Telerik.StarterKit.Modules.RealEstate.Web.UI.Public.Designers");

Telerik.StarterKit.Modules.RealEstate.Web.UI.Public.Designers.RealEstateViewContentSelectorsDesignerView = function (element) {
    Telerik.StarterKit.Modules.RealEstate.Web.UI.Public.Designers.RealEstateViewContentSelectorsDesignerView.initializeBase(this, [element]);
    this._selectContentButton = null;
    this._selectedContentTitle = null;
    this._controlData = null;
    this._contentSelector = null;
    this._showContentSelectorDelegate = null;
    this._selectContentDelegate = null;
    this._parentDesigner = null;
    this._radioChoices = null;
    this._refreshing = false;
    this._filterSelector = null;
    this._currentDetailViewName = null;
    this._currentMasterViewName = null;
    this._btnNarrowSelection = null;
    this._narrowSelection = null;
    this._btnNarrowSelectionClickDelegate = null;
}

Telerik.StarterKit.Modules.RealEstate.Web.UI.Public.Designers.RealEstateViewContentSelectorsDesignerView.prototype = {

    /* --------------------------------- set up and tear down --------------------------------- */

    initialize: function () {
        Telerik.StarterKit.Modules.RealEstate.Web.UI.Public.Designers.RealEstateViewContentSelectorsDesignerView.callBaseMethod(this, 'initialize');

        this._radioClickDelegate = Function.createDelegate(this, this._setContentFilter);
        this.get_radioChoices().click(this._radioClickDelegate);
        // prevent memory leaks
        $(this).unload(function () {
            jQuery.event.remove(this);
            jQuery.removeData(this);
        });

        this._showContentSelectorDelegate = Function.createDelegate(this, this._showContentSelector);
        $addHandler($get(this._selectContentButton), "click", this._showContentSelectorDelegate);

        this._selectContentDelegate = Function.createDelegate(this, this._selectContent);
        this._contentSelector.add_doneClientSelection(this._selectContentDelegate);
        if (this._btnNarrowSelection) {
            this._btnNarrowSelectionClickDelegate = Function.createDelegate(this, this._btnNarrowSelectionClick);
            $addHandler(this._btnNarrowSelection, "click", this._btnNarrowSelectionClickDelegate);
        }
    },

    dispose: function () {
        this._contentSelector.remove_doneClientSelection(this._selectContentDelegate);
        if (this._showContentSelectorDelegate) {
            $removeHandler($get(this._selectContentButton), "click", this._showContentSelectorDelegate);
            delete this._showContentSelectorDelegate;
        }
        if (this._selectContentDelegate) {
            this._contentSelector.remove_doneClientSelection(this._selectContentDelegate);
            delete this._selectContentDelegate;
        }
        if (this._radioClickDelegate) {
            this.get_radioChoices().unbind("click", this._radioClickDelegate);
            delete this._radioClickDelegate;
        }
        if (this._btnNarrowSelection) {
            $removeHandler(this._btnNarrowSelection, "click", this._btnNarrowSelectionClickDelegate);
            delete this._btnNarrowSelectionClickDelegate;
        }
        Telerik.StarterKit.Modules.RealEstate.Web.UI.Public.Designers.RealEstateViewContentSelectorsDesignerView.callBaseMethod(this, 'dispose');
    },

    /* --------------------------------- public methods --------------------------------- */

    // refereshed the user interface. Call this method in case underlying control object
    // has been changed somewhere else then through this desinger.
    refreshUI: function () {
        this._refreshing = true;
        var controlData = this.get_controlData();
        if (!controlData) {
            return;
        }
        var additionalFilter = this.get_currentMasterView().AdditionalFilter;
        if (additionalFilter)
            additionalFilter = Sys.Serialization.JavaScriptSerializer.deserialize(additionalFilter);
        this._filterSelector.set_queryData(additionalFilter);
        var disabledFilter = true;
        if (controlData.ContentViewDisplayMode != "Detail") {
            if (additionalFilter) {
                this.get_radioChoices()[2].click();
                disabledFilter = false;
            }
            else
                this.get_radioChoices()[0].click();
        }
        else {
            this.get_radioChoices()[1].click()
        }
        this._filterSelector.set_disabled(disabledFilter);
        dialogBase.resizeToContent();
        this._refreshing = false;
    },

    // once the data has been modified, call this method to apply all the changes made
    // by this designer on the underlying control object.
    applyChanges: function () {
        var displayMode = this.get_controlData().ContentViewDisplayMode;
        if (displayMode == "Automatic") {
            this.get_currentDetailView().DataItemId = '00000000-0000-0000-0000-000000000000';
        }
        this.get_currentMasterView().AdditionalFilter = null;
        if (displayMode == "Automatic" || displayMode == "Master") {
            if (this.get_radioChoices().eq(2).attr("checked")) {
                this._filterSelector.applyChanges();
                var queryData = this._filterSelector.get_queryData();
                if (queryData.QueryItems && queryData.QueryItems.length > 0)
                    queryData = Telerik.Sitefinity.JSON.stringify(queryData);
                else
                    queryData = null;
                this.get_currentMasterView().AdditionalFilter = queryData;
            }
        }
    },

    /* --------------------------------- event handlers --------------------------------- */

    // handles the content selected event of the content selector
    _selectContent: function (items) {
        jQuery(this.get_element()).find('#selectorTag').hide();
        dialogBase.resizeToContent();
        if (items == null) return;
        var selectedItems = this.get_contentSelector().getSelectedItems();
        if (selectedItems != null) {
            if (selectedItems.length > 0) {
                if (selectedItems[0].Title.hasOwnProperty('Value')) {
                    this.get_selectedContentTitle().innerHTML = selectedItems[0].Title.Value;
                } else {
                    this.get_selectedContentTitle().innerHTML = selectedItems[0].Title;
                }
                var controlData = this.get_controlData()
                this.get_currentDetailView().DataItemId = selectedItems[0].Id;
                controlData.ContentViewDisplayMode = "Detail";
                jQuery(this.get_selectedContentTitle()).show();
            }
        }
        //todo: set the values from the selector
    },
    // handles the click event of the select content button
    _showContentSelector: function () {
        this.get_contentSelector().dataBind();
        var dataItemId = this.get_currentDetailView().DataItemId;
        if (dataItemId) {
            this.get_contentSelector().set_selectedKeys([dataItemId]);
        }

        jQuery(this.get_element()).find('#selectorTag').show();
        dialogBase.resizeToContent();
    },
    _btnNarrowSelectionClick: function () {
        jQuery(this._narrowSelection).toggleClass("sfExpandedSection");
        dialogBase.resizeToContent();
    },

    /* --------------------------------- private methods --------------------------------- */

    // sets the content filter setting based on the radio button that selected the
    // filter type
    _setContentFilter: function (sender) {

        var radioID = sender.target.value;
        var controlData = this.get_controlData();
        var disabledFilter = true;
        switch (radioID) {
            case "contentSelect_AllItems":
                jQuery(this.get_element()).find('#selectorPanel').hide();
                if (!this._refreshing) {
                    controlData.ContentViewDisplayMode = "Automatic";
                }
                break;
            case "contentSelect_OneItem":
                jQuery(this.get_element()).find('#selectorPanel').show();
                if (!this._refreshing) {
                    controlData.ContentViewDisplayMode = "Detail";
                }
                break;
            case "contentSelect_SimpleFilter":
                jQuery(this.get_element()).find('#selectorPanel').hide();
                disabledFilter = false;
                if (!this._refreshing) {
                    controlData.ContentViewDisplayMode = "Automatic";
                }
                break;
            case "contentSelect_AdvancedFilter": break;
        }
        this._filterSelector.set_disabled(disabledFilter);
        dialogBase.resizeToContent();

    },

    // gets all the radio buttons in the container of this control with group name 'ContentSelection'
    get_radioChoices: function () {
        if (!this._radioChoices) {
            this._radioChoices = jQuery(this.get_element()).find(':radio[name$=ContentSelection]'); // finds radio buttons with names ending with 'ContentSelection'
        }
        return this._radioChoices;
    },

    /* --------------------------------- properties --------------------------------- */

    // gets the reference to the parent designer component
    get_parentDesigner: function () {
        return this._parentDesigner;
    },
    // sets the reference to the parent designer component
    set_parentDesigner: function (value) {
        this._parentDesigner = value;
    },

    // gets the javascript control object that is being designed
    get_controlData: function () {
        return this.get_parentDesigner().get_propertyEditor().get_control();
    },

    // gets the name of the currently selected detail view of the content view control
    get_currentDetailViewName: function () {
        return (this._currentDetailViewName) ? this._currentDetailViewName : this.get_controlData().DetailViewName;
    },

    // gets the client side representation of the currently selected detail view definition
    get_currentDetailView: function () {
        return this.get_controlData().ControlDefinition.Views[this.get_currentDetailViewName()];
    },

    // gets the name of the currently selected master view of the content view control
    get_currentMasterViewName: function () {
        return (this._currentMasterViewName) ? this._currentMasterViewName : this.get_controlData().MasterViewName;
    },

    // gets the client side representation of the currently selected master view definition
    get_currentMasterView: function () {
        return this.get_controlData().ControlDefinition.Views[this.get_currentMasterViewName()];
    },

    // gets the reference to the content selector used to choose one or more content items
    // to be displayed by the view
    get_contentSelector: function () {
        return this._contentSelector;
    },

    // gets the reference to the content selector used to choose one or more content items
    // to be displayed by the view
    set_contentSelector: function (value) {
        this._contentSelector = value;
    },

    //gets refernce to the slected content Title label
    get_selectedContentTitle: function () {
        return this._selectedContentTitle;
    },

    //sets refernce to the slected content Title label
    set_selectedContentTitle: function (value) {
        this._selectedContentTitle = value;
    },
    //gets refernce to the filter selector
    get_filterSelector: function () {
        return this._filterSelector;
    },

    //sets refernce to the filter selector
    set_filterSelector: function (value) {
        this._filterSelector = value;
    },

    get_btnNarrowSelection: function () {
        return this._btnNarrowSelection;
    },
    set_btnNarrowSelection: function (value) {
        this._btnNarrowSelection = value;
    },
    get_narrowSelection: function () {
        return this._narrowSelection;
    },
    set_narrowSelection: function (value) {
        this._narrowSelection = value;
    }
}
Telerik.StarterKit.Modules.RealEstate.Web.UI.Public.Designers.RealEstateViewContentSelectorsDesignerView.registerClass('Telerik.StarterKit.Modules.RealEstate.Web.UI.Public.Designers.RealEstateViewContentSelectorsDesignerView', Sys.UI.Control, Telerik.Sitefinity.Web.UI.ControlDesign.IDesignerViewControl);

if (typeof (Sys) !== 'undefined') Sys.Application.notifyScriptLoaded();