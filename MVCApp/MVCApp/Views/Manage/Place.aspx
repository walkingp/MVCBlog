<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Manage.Master" Inherits="System.Web.Mvc.ViewPage<List<MVCApp.Models.Place>>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    Place
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="placeHolderMain" runat="server">

    <h2 class="sub-header">Place Manager
        <a href="/Manage/AddPlace/" class="btn btn-primary" role="button">Add</a>
    </h2>
    <div class="table-responsive">
        <table class="table table-striped">
            <tr>
                <th>ID</th>
                <th>Title</th>
                <th>Lat/LongLati</th>
                <th>Date</th>
                <th>Star</th>
                <th>Action</th>
            </tr>
            <% for (int i = 0, len = Model.Count; i < len; i++)
                { %>
            <tr>
                <td><%=Model[i].Id %></td>
                <td><%=Model[i].Location %></td>
                <td><%=Model[i].Lati + "/" + Model[i].LongLati%></td>
                <td><%=Model[i].Date.ToString("yyyy-MM-dd") %></td>
                <td><%=Model[i].Star %></td>
                <td>
                    <%=Html.ActionLink("Edit", "EditPlace", "Manage", new { Model[i].Id }, new { })%>
                    <%=Html.ActionLink("Delete", "DeletePlace", "Manage", new { Model[i].Id }, new { onclick="return confirm('Confirm to delete?')"  })%>
                </td>
            </tr>
            <% }%>
        </table>
    </div>
    <ul class="pagination">
        <%=ViewData["html"].ToString() %>
    </ul>

</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="ScriptContent" runat="server">
</asp:Content>
