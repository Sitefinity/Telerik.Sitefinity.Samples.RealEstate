/// <reference name="MicrosoftAjax.js"/>
/// <reference name="Telerik.Sitefinity.Resources.Scripts.jquery-1.3.2.min-vsdoc2.js" assembly="Telerik.Sitefinity.Resources"/>
Type.registerNamespace("Telerik.StarterKit.Widgets.Facebook");

// ------------------------------------------------------------------------
// ContentBlockDesigner class
// ------------------------------------------------------------------------
Telerik.StarterKit.Widgets.Facebook.SocialPluginDesigner = function (element) {
    this._propertyEditor = null;
    this._titleEditor = null;
    this._facebookPageUrlEditor = null;
    Telerik.StarterKit.Widgets.Facebook.SocialPluginDesigner.initializeBase(this, [element]);
}

Telerik.StarterKit.Widgets.Facebook.SocialPluginDesigner.prototype = {

    /* ************************* set up and tear down ************************* */
    initialize: function () {
        Telerik.StarterKit.Widgets.Facebook.SocialPluginDesigner.callBaseMethod(this, 'initialize');
    },
    dispose: function () {
        Telerik.StarterKit.Widgets.Facebook.SocialPluginDesigner.callBaseMethod(this, 'dispose');
    },

    /* ************************* public methods ************************* */
    applyChanges: function () {

        var controlData = this.get_propertyEditor().get_control();
        controlData.Title = $("#titleEditor").val();
        controlData.FacebookPageUrl = $("#facebookPageUrlEditor").val();
    },

    refreshUI: function () {
        var data = this.get_propertyEditor().get_control();
        $("#titleEditor").val(data.Title);
        $("#facebookPageUrlEditor").val(data.FacebookPageUrl);

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
Telerik.StarterKit.Widgets.Facebook.SocialPluginDesigner.registerClass('Telerik.StarterKit.Widgets.Facebook.SocialPluginDesigner', Telerik.Sitefinity.Web.UI.ControlDesign.ControlDesignerBase);