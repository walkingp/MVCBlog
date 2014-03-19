<%@ Page Language="C#" MasterPageFile="~/Views/Shared/Manage.Master" Inherits="System.Web.Mvc.ViewPage<MVCApp.Models.Photos>" %>

<asp:Content ID="titleContent" ContentPlaceHolderID="TitleContent" runat="server">
    Index
</asp:Content>
<asp:Content ID="cntContent" ContentPlaceHolderID="ScriptContent" runat="server">
    <script type="text/javascript">
    //Submit ajax requests
    $('#btnAjax').click(function () {
        var name = $('#pName').val();
        var photo = $('#hPhoto').val();
        $.ajax({
            url:$('#url').val(),
            data: {'pName':name,'hPhoto':photo,'isAjax':1},
            type: 'post',
            cache: false,
            success: function (ret) {
                alert(ret);
            
            },
            error: function (xhr, ajaxOptions, thrownError) {
                alert(thrownError);
            }
        });
    });    
    </script>
</asp:Content>


<asp:Content ID="Content2" ContentPlaceHolderID="placeHolderMain" runat="server">
    <%=ViewData["Message"].ToString() %>
    <h2 class="sub-header">Gallery Manager</h2>
    <div class="table-responsive">
        <table class="table table-striped">
            <tr>
                <th>ID</th>
                <th>Title</th>
                <th>Photo</th>
                <th>Action</th>
            </tr>
            <%List<MVCApp.Models.Photos> photos = (List<MVCApp.Models.Photos>)ViewData["Photos"]; %>
            <% for (int i = 0, len = photos.Count; i < len; i++)
                { %>
            <tr>
                <td><%=photos[i].Id %></td>
                <td><%=string.IsNullOrEmpty(photos[i].Title) ? photos[i].FileName : photos[i].Title %></td>
                <td><img src="<%=photos[i].Path %>" alt="" width="20" height="20" /></td>
                <td>
                    <%=Html.ActionLink("Edit","Edit","Photos",new { photos[i].Id },null)%>
                    <%=Html.ActionLink("Delete", "Delete", "Manage", new { photos[i].Id }, new { onclick="return confirm('Confirm to delete?')"  })%>
                </td>
            </tr>
            <% }%>
        </table>
        <div class="page">
            <%=ViewData["html"].ToString() %>
        </div>
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
    </div>
</asp:Content>
