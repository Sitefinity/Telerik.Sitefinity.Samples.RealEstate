<%@ Control Language="C#" AutoEventWireup="true" Inherits="Templates_Languages" Codebehind="Languages.ascx.cs" %>

<asp:Panel ID="errorsPanel" runat="server" Visible="false"></asp:Panel>

<span class="left">Language</span>
<div class="language-list">
	<div class="round-btn-purple"><span><span><asp:Literal ID="ltrCurrentLanguageName" runat="server" /></span></span></div>
    <ul>
        <asp:Repeater ID="languagesRepeater" runat="server">
            <ItemTemplate>
		        <li id="langHolder" runat="server"><a id="langLink" runat="server" href="#"><div ID="langName" runat="server"></div></a></li>
            </ItemTemplate>
        </asp:Repeater>
    </ul>
</div>
