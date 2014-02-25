/// <reference name="MicrosoftAjax.js"/>
/// <reference name="Telerik.Sitefinity.Resources.Scripts.jquery-1.3.2.min-vsdoc2.js" assembly="Telerik.Sitefinity.Resources"/>
Type.registerNamespace("Telerik.StarterKit.Widgets.News");

// ------------------------------------------------------------------------
// ContentBlockDesigner class
// ------------------------------------------------------------------------
Telerik.StarterKit.Widgets.News.RelatedNewsDesigner = function (element) {
    this._propertyEditor = null;
    this._numberOfNewsToShowEditor = null;
    Telerik.StarterKit.Widgets.News.RelatedNewsDesigner.initializeBase(this, [element]);
}

Telerik.StarterKit.Widgets.News.RelatedNewsDesigner.prototype = {

    /* ************************* set up and tear down ************************* */
    initialize: function () {
        Telerik.StarterKit.Widgets.News.RelatedNewsDesigner.callBaseMethod(this, 'initialize');
    },
    dispose: function () {
        Telerik.StarterKit.Widgets.News.RelatedNewsDesigner.callBaseMethod(this, 'dispose');
    },

    /* ************************* public methods ************************* */
    applyChanges: function () {
//        this.get_propertyEditor().get_control().Value = this._numberOfNewsToShowEditor.get_value();
        var controlData = this.get_propertyEditor().get_control();
        controlData.NumberOfNewsToShow = $("#numberOfNewsToShowEditor").val();
    },

    refreshUI: function () {
        var data = this.get_propertyEditor().get_control();
        $("#numberOfNewsToShowEditor").val(data.NumberOfNewsToShow);

//        var value = this.get_propertyEditor().get_control();
//        if (value) {
//            this._numberOfNewsToShowEditor.set_value(value);
//        }
    },

    /* ************************* properties ************************* */
    // gets the reference to the propertyEditor control
    get_propertyEditor: function () {
        return this._propertyEditor;
    },
    // sets the reference fo the propertyEditor control
    set_propertyEditor: function (value) {
        this._propertyEditor = value;
    }
};
Telerik.StarterKit.Widgets.News.RelatedNewsDesigner.registerClass('Telerik.StarterKit.Widgets.News.RelatedNewsDesigner', Telerik.Sitefinity.Web.UI.ControlDesign.ControlDesignerBase);

if (typeof (Sys) !== 'undefined') Sys.Application.notifyScriptLoaded();