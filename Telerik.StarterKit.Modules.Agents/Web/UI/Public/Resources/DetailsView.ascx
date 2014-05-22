<%@ Control Language="C#" %>



<%@ Register Assembly="Telerik.Sitefinity" Namespace="Telerik.Sitefinity.Web.UI.ContentUI" TagPrefix="sf" %>
<%@ Register Assembly="Telerik.Sitefinity" Namespace="Telerik.Sitefinity.Web.UI" TagPrefix="sf" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>

<sf:ResourceLinks id="resourceLinks" runat="server">
 <sf:ResourceFile JavaScriptLibrary="JQuery" />
</sf:ResourceLinks>

<div class="agent-single content">

    <telerik:RadListView ID="DetailsView" ItemPlaceholderID="ItemsContainer" runat="server" EnableEmbeddedSkins="false" EnableEmbeddedBaseStylesheet="false">
        <LayoutTemplate>
            <asp:PlaceHolder ID="ItemsContainer" runat="server" />
        </LayoutTemplate>
        <ItemTemplate>
            <div class="column-small">
                <div class="block">
                    <p id="photo" runat="server"></p>
				    <h3><sf:FieldListView ID="Title" runat="server" Text="{0}" Properties="Title" /></h3>
				    <address>
					    <sf:FieldListView ID="Address" runat="server" Text="{0}" Properties="Address" /><br />
					    <sf:FieldListView ID="PostalCode" runat="server" Text="{0}" Properties="PostalCode" /><br />
					    <%$Resources:AgentsResources, Telephone %>: <sf:FieldListView ID="PhoneNumber" runat="server" Text="{0}" Properties="PhoneNumber" /><br />
					    <%$Resources:AgentsResources, Email %>: <sf:FieldListView ID="Email" runat="server" Text="<a href='mailto:{0}'>{0}</a>" Properties="Email" />
				    </address>
			    </div>
            </div>
            <input type="hidden" name="agentid" id="agentid" runat="server" clientidmode="Static" value='<%#Eval("Id") %>' />
        </ItemTemplate>
    </telerik:RadListView>

	<div class="page-content">
		<div class="form-item">
			<label><%$Resources:AgentsResources, Name %></label>
			<input name="name" id="name" type="text" class="form-text" />
		</div>
		<div class="form-item">
			<label><%$Resources:AgentsResources, Email %></label>
			<input name="email" id="email" type="text" class="form-text" />
		</div>
		<div class="form-item">
			<label><%$Resources:AgentsResources, Property %></label>
			<input name="property" id="property" type="text" class="form-text" />
		</div>
		<div class="form-item">
			<label><%$Resources:AgentsResources, Message %></label>
			<textarea name="message" id="message" rows="10" cols="80"></textarea>
		</div>
		<div class="form-item">
			<label class="regular-label">
				<input name="sendcopy" id="sendcopy" type="checkbox" />
				<%$Resources:AgentsResources, SendACopyContactForm %>
			</label>
		</div>
		<div class="form-item">
			<input type="button" class="form-submit" value="<%$Resources:AgentsResources, SendButtonTextContactForm %>" onclick="contactAgent();" />
		</div>
	</div><!-- .page-content -->
    <asp:HiddenField ID="webServiceUrlHidden" runat="server" />
    <script language="javascript" type="text/javascript">
        function contactAgent() {
            if (!validateContactAgent()) {
                return false;
            }
            var webServiceUrlBase = $('#<%= webServiceUrlHidden.ClientID %>').val();
            var agentid = $("#agentid").val();
            var fromName = $("#name").val();
            var fromEmail = $("#email").val();
            var property = $("#property").val();
            var message = $("#message").val();
            var sendcopy = $("#sendcopy").is(':checked');
            var _data = { "AgentId": agentid, "FromName": fromName, "FromEmail": fromEmail, "Property": property, "Message": message, "SendCopy": sendcopy };
            jQuery.ajax({
                url: webServiceUrlBase + 'SendMail/',
                cache: false,
                type: "PUT",
                contentType: "application/json",
                data: JSON.stringify(_data),
                success: function (data) {
                    if (data.HasSentMail) {
                        alert("<%$Resources:AgentsResources, MessageSentContactForm %>");
                        $("#fancybox-overlay, #fancybox-wrap").delay(1000).fadeOut();
                    }
                    else {
                        alert("<%$Resources:AgentsResources, MessageNotSentContactForm %>");
                    }
                },
                error: function () {
                    alert("<%$Resources:AgentsResources, MessageNotSentContactForm %>");
                }
            });
        }

        function validateContactAgent() {
            var emailRegex = RegExp("^([a-zA-Z0-9_\.\-])+\@(([a-zA-Z0-9\-])+\.)+([a-zA-Z0-9]{2,4})+$");
            var agentid = $("#agentid").val();
            var name = $("#name").val();
            var email = $("#email").val();
            var property = $("#property").val();
            var message = $("#message").val();

            if (agentid == "") {
                alert("Invalid agent selected.");
                return false;
            }

            var errorMessage = "";
            var controlToFocus = null;

            if (name == "") {
                errorMessage += "- <%$Resources:AgentsResources, NameCannotBeEmpty %>\n";
                controlToFocus = "name";
            }

            if (email != "") {
                if (!emailRegex.test(email)) {
                    errorMessage += "- <%$Resources:AgentsResources, EmailMustBeValid %>\n"

                    if (controlToFocus == null)
                        controlToFocus = "email";
                }
            }
            else {
                errorMessage += "- <%$Resources:AgentsResources, EmailCannotBeEmpty %>\n";

                if (controlToFocus == null)
                    controlToFocus = "email";
            }

            if (message == "") {
                errorMessage += "- <%$Resources:AgentsResources, MessageCannotBeEmpty %>\n";
                if (controlToFocus == null)
                    controlToFocus = "message";
            }

            if (controlToFocus != null) {
                alert(errorMessage);
                $("#" + controlToFocus).focus();
                return false;
            }

            return true;
        }
    </script>

</div>