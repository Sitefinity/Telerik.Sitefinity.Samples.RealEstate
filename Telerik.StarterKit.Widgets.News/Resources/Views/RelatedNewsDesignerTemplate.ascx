<%@ Control Language="C#" %>



<%@ Register Assembly="Telerik.Sitefinity" Namespace="Telerik.Sitefinity.Web.UI" TagPrefix="sf" %>
<%@ Register Assembly="Telerik.Sitefinity" Namespace="Telerik.Sitefinity.Web.UI.Fields" TagPrefix="sf" %>

<sf:ResourceLinks id="resourceLinks" runat="server">
 <sf:ResourceFile JavaScriptLibrary="JQuery" />
</sf:ResourceLinks>

<sf:FormManager ID="formManager" runat="server" />

<ul class="sfTargetList">
    <li>
        <label for="numberOfNewsToShowEditor">News limit</label>
        <input type="text" id="numberOfNewsToShowEditor" class="sfTxt" />
    </li>
</ul>


<script type="text/javascript">
    $("body").addClass("sfContentBlockDesigner");
</script>