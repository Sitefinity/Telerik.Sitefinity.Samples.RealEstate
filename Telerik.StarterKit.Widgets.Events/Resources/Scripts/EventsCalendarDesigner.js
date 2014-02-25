/// <reference name="MicrosoftAjax.js"/>
/// <reference name="Telerik.Sitefinity.Resources.Scripts.jquery-1.3.2.min-vsdoc2.js" assembly="Telerik.Sitefinity.Resources"/>
Type.registerNamespace("Telerik.StarterKit.Widgets.Events");

// ------------------------------------------------------------------------
// ContentBlockDesigner class
// ------------------------------------------------------------------------
Telerik.StarterKit.Widgets.Events.EventsCalendarDesigner = function (element) {
    this._propertyEditor = null;
    this._numberOfEventsToShowEditor = null;
    Telerik.StarterKit.Widgets.Events.EventsCalendarDesigner.initializeBase(this, [element]);
}

Telerik.StarterKit.Widgets.Events.EventsCalendarDesigner.prototype = {

    /* ************************* set up and tear down ************************* */
    initialize: function () {
        Telerik.StarterKit.Widgets.Events.EventsCalendarDesigner.callBaseMethod(this, 'initialize');
    },
    dispose: function () {
        Telerik.StarterKit.Widgets.Events.EventsCalendarDesigner.callBaseMethod(this, 'dispose');
    },

    /* ************************* public methods ************************* */
    applyChanges: function () {
        var controlData = this.get_propertyEditor().get_control();
        controlData.NumberOfEventsToShow = $("#eventsPageUrlEditor").val();
    },

    refreshUI: function () {
        var data = this.get_propertyEditor().get_control();
        $("#eventsPageUrlEditor").val(data.NumberOfEventsToShow);
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
Telerik.StarterKit.Widgets.Events.EventsCalendarDesigner.registerClass('Telerik.StarterKit.Widgets.Events.EventsCalendarDesigner', Telerik.Sitefinity.Web.UI.ControlDesign.ControlDesignerBase);

if (typeof (Sys) !== 'undefined') Sys.Application.notifyScriptLoaded();