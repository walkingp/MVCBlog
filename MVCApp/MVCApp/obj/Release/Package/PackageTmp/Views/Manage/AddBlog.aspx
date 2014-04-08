<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Manage.Master" Inherits="System.Web.Mvc.ViewPage<MVCApp.Models.Blog>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    AddBlog
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="placeHolderMain" runat="server">
    <h2>Add new blog</h2>
    <%using (Html.BeginForm("Save", "Blog", FormMethod.Post, new { enctype = "multipart/form-data" }))
        { %>
    <div class="form-group">
        <label for="Title">Title:</label>
        <%=Html.TextBoxFor(model=>Model.Title, new {@class="form-control" })%>
    </div>
    <div class="form-group">
        <label for="Content">Content:</label>
        <%=Html.TextAreaFor(model=>Model.Content, new {@class="form-control",PlaceHolder="Title",rows="8"})%>
    </div>
    <div class="form-group">
        <input type="submit" class="btn btn-primary" value="Submit" />
    </div>
    <%} %>
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="ScriptContent" runat="server">
    <link href="/Assets/summernote/summernote.css" type="text/css" rel="stylesheet" />
    <link href="//netdna.bootstrapcdn.com/font-awesome/4.0.3/css/font-awesome.min.css" type="text/css" rel="stylesheet" />
    <script type="text/javascript" src="/Assets/summernote/summernote.min.js"></script>
    <script type="text/javascript">
        $(function () {
            $('#Content').summernote({
                height: 400
            });
        });
    </script>
</asp:Content>
