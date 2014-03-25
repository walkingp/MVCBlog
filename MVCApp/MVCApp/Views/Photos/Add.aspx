<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Manage.Master" Inherits="System.Web.Mvc.ViewPage<MVCApp.Models.Photos>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    Add
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="placeHolderMain" runat="server">
    <h2>Add new item</h2>
    <%using (Html.BeginForm("Create", "Manage", FormMethod.Post, new { enctype = "multipart/form-data" }))
        { %>
    <p>
        <label for="pName">Name:</label>
        <%=Html.TextBox("pName") %>
    </p>
    <p>
        <label for="photo">Avatar:</label>
        <input type="file" id="photo" name="photo" />
        <%=Html.Hidden("hPhoto") %>
    </p>
    <div id="dragandrophandler">
        Drag & Drop Files Here
    </div>
    <div id="status1"></div>
    <p>
        <input type="submit" value="Submit" />
        <%=Html.Hidden("url",Url.Action("Create")) %>
        <input type="button" value="Submit by AJAX" id="btnAjax" />
    </p>
    <%} %>
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="ScriptContent" runat="server">
</asp:Content>
