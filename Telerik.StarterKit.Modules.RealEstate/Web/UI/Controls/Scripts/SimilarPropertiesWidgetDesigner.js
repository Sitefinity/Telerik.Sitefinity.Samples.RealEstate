/// <reference name="MicrosoftAjax.js"/>
/// <reference name="Telerik.Sitefinity.Resources.Scripts.jquery-1.3.2.min-vsdoc2.js" assembly="Telerik.Sitefinity.Resources"/>
Type.registerNamespace("Telerik.StarterKit.Modules.RealEstate.Web.UI.Public.Designers");

// ------------------------------------------------------------------------
// ContentBlockDesigner class
// ------------------------------------------------------------------------
Telerik.StarterKit.Modules.RealEstate.Web.UI.Public.Designers.SimilarPropertiesWidgetDesigner = function (element) {
    this._propertyEditor = null;
    this._titleEditor = null;
    Telerik.StarterKit.Modules.RealEstate.Web.UI.Public.Designers.SimilarPropertiesWidgetDesigner.initializeBase(this, [element]);
}

Telerik.StarterKit.Modules.RealEstate.Web.UI.Public.Designers.SimilarPropertiesWidgetDesigner.prototype = {

    /* ************************* set up and tear down ************************* */
    initialize: function () {
        Telerik.StarterKit.Modules.RealEstate.Web.UI.Public.Designers.SimilarPropertiesWidgetDesigner.callBaseMethod(this, 'initialize');
    },
    dispose: function () {
        Telerik.StarterKit.Modules.RealEstate.Web.UI.Public.Designers.SimilarPropertiesWidgetDesigner.callBaseMethod(this, 'dispose');
    },

    /* ************************* public methods ************************* */
    applyChanges: function () {

        var controlData = this.get_propertyEditor().get_control();
        controlData.NumberOfItemsToShow = $("#numberOfItemsToShowEditor").val();
    },

    refreshUI: function () {
        var data = this.get_propertyEditor().get_control();
        $("#numberOfItemsToShowEditor").val(data.NumberOfItemsToShow);
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
    }
};
Telerik.StarterKit.Modules.RealEstate.Web.UI.Public.Designers.SimilarPropertiesWidgetDesigner.registerClass('Telerik.StarterKit.Modules.RealEstate.Web.UI.Public.Designers.SimilarPropertiesWidgetDesigner', Telerik.Sitefinity.Web.UI.ControlDesign.ControlDesignerBase);

if (typeof (Sys) !== 'undefined') Sys.Application.notifyScriptLoaded();