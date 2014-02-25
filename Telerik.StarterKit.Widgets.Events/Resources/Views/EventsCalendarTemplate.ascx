<%@ Control Language="C#" %>



<%@ Register Assembly="Telerik.Web.UI, Version=2013.3.1114.40, Culture=neutral, PublicKeyToken=121fae78165ba3d4" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>

<div class="block block-calendar">
	<h2>Calendar</h2>

    <asp:Calendar ID="Events" runat="server" CssClass="calendar-box"
    
    DayHeaderStyle-CssClass="DayHeaderStyle-CssClass" DayStyle-CssClass="DayStyle-CssClass"
 NextPrevStyle-CssClass="NextPrevStyle-CssClass" OtherMonthDayStyle-CssClass="OtherMonthDayStyle-CssClass"
  SelectedDayStyle-CssClass="SelectedDayStyle-CssClass" SelectorStyle-CssClass="SelectorStyle-CssClass" TitleStyle-CssClass="TitleStyle-CssClass"
   TodayDayStyle-CssClass="TodayDayStyle-CssClass" WeekendDayStyle-CssClass="WeekendDayStyle-CssClass"  />
</div>