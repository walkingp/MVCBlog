<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Manage.Master" Inherits="System.Web.Mvc.ViewPage<MVCApp.Models.Blog>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    Blog
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="placeHolderMain" runat="server">
    <h2 class="sub-header">Blog Manager
        <a href="/Manage/AddBlog/" class="btn btn-primary" role="button">Add</a>
    </h2>
    <div class="table-responsive">
        <table class="table table-striped">
            <tr>
                <th>ID</th>
                <th>Title</th>
                <th>Action</th>
            </tr>
            <%List<MVCApp.Models.Blog> blogs = (List<MVCApp.Models.Blog>)ViewData["Blog"]; %>
            <% for (int i = 0, len = blogs.Count; i < len; i++)
                { %>
            <tr>
                <td><%=blogs[i].Id %></td>
                <td><%=blogs[i].Title %></td>
                <td>
                    <%=Html.ActionLink("Edit","EditBlog","Manage",new { blogs[i].Id },null)%>
                    <%=Html.ActionLink("Delete", "DeleteBlog", "Manage", new { blogs[i].Id }, new { onclick="return confirm('Confirm to delete?')"  })%>
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
