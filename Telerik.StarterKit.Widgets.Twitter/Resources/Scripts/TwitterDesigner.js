/// <reference name="MicrosoftAjax.js"/>
/// <reference name="Telerik.Sitefinity.Resources.Scripts.jquery-1.3.2.min-vsdoc2.js" assembly="Telerik.Sitefinity.Resources"/>
Type.registerNamespace("Telerik.StarterKit.Widgets.Twitter");

// ------------------------------------------------------------------------
// ContentBlockDesigner class
// ------------------------------------------------------------------------
Telerik.StarterKit.Widgets.Twitter.TwitterDesigner = function (element) {
    this._propertyEditor = null;
    this._titleEditor = null;
    this._facebookPageUrlEditor = null;
    Telerik.StarterKit.Widgets.Twitter.TwitterDesigner.initializeBase(this, [element]);
}

Telerik.StarterKit.Widgets.Twitter.TwitterDesigner.prototype = {

    /* ************************* set up and tear down ************************* */
    initialize: function () {
        Telerik.StarterKit.Widgets.Twitter.TwitterDesigner.callBaseMethod(this, 'initialize');
    },
    dispose: function () {
        Telerik.StarterKit.Widgets.Twitter.TwitterDesigner.callBaseMethod(this, 'dispose');
    },

    /* ************************* public methods ************************* */
    applyChanges: function () {

        var controlData = this.get_propertyEditor().get_control();
        controlData.Title = $("#titleEditor").val();
        controlData.TwitterProfileName = $("#twitterProfileNameEditor").val();
        controlData.NumberOfTweetsToShow = $("#numberOfTweetsToShowEditor").val();

        if (isNaN(controlData.NumberOfTweetsToShow) || controlData.NumberOfTweetsToShow <= 0) {
            controlData.NumberOfTweetsToShow = 2;
        }
    },

    refreshUI: function () {
        var data = this.get_propertyEditor().get_control();
        $("#titleEditor").val(data.Title);
        $("#twitterProfileNameEditor").val(data.TwitterProfileName);

        if (data.NumberOfTweetsToShow && !isNaN(data.NumberOfTweetsToShow) && data.NumberOfTweetsToShow > 0) {
            $("#numberOfTweetsToShowEditor").val(data.NumberOfTweetsToShow);
        }
        else {
            $("#numberOfTweetsToShowEditor").val(2);
        }

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
Telerik.StarterKit.Widgets.Twitter.TwitterDesigner.registerClass('Telerik.StarterKit.Widgets.Twitter.TwitterDesigner', Telerik.Sitefinity.Web.UI.ControlDesign.ControlDesignerBase);