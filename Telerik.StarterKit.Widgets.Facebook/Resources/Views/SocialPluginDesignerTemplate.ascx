<%@ Control Language="C#" %>



<%@ Register Assembly="Telerik.Sitefinity, Version=6.3.5000.0, Culture=neutral, PublicKeyToken=b28c218413bdf563" Namespace="Telerik.Sitefinity.Web.UI" TagPrefix="sf" %>
<%@ Register Assembly="Telerik.Sitefinity, Version=6.3.5000.0, Culture=neutral, PublicKeyToken=b28c218413bdf563" Namespace="Telerik.Sitefinity.Web.UI.Fields" TagPrefix="sf" %>

<sf:ResourceLinks id="resourceLinks" runat="server">
 <sf:ResourceFile JavaScriptLibrary="JQuery" />
</sf:ResourceLinks>

<sf:FormManager ID="formManager" runat="server" />

<ul class="sfTargetList">
    <li>
        <label for="titleEditor">Title</label>
        <input type="text" id="titleEditor" class="sfTxt" />
    </li>
    <li>
        <label for="facebookPageUrlEditor">Facebook Page Url</label>
        <input type="text" id="facebookPageUrlEditor" class="sfTxt" />
    </li>
</ul>


<script type="text/javascript">
    $("body").addClass("sfContentBlockDesigner");
</script>