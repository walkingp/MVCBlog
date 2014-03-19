<%@ Page Language="C#" Inherits="System.Web.Mvc.ViewPage<MVCApp.Models.Photos>" %>
<!DOCTYPE html>

<html>
<head id="Head1" runat="server">
    <meta name="viewport" content="width=device-width" />
    <title>Edit</title>
    <link rel="stylesheet" href="/Styles/layout.css" type="text/css" />
</head>
<body>
    <div>
        <h2>Edit item</h2>
        <%using (Html.BeginForm("Save", "Photos", FormMethod.Post, new { enctype = "multipart/form-data" }))
          { %>
        <p>
            <a href="<%=((MVCApp.Models.Photos)ViewData["Edit"]).Path %>" target="_blank" title="Original picture"><img src="<%=((MVCApp.Models.Photos)ViewData["Edit"]).Path %>" alt="" width="56" height="56" /></a>
        </p>
        <p>
            <label for="pName">Name:</label>
            <%=Html.TextBoxFor(model => Model.Title) %>
            <%=Html.ValidationMessageFor(model => Model.Title) %>
        </p>
        <p>
            <label for="photo">Avatar:</label>
            <input type="file" id="photo" name="photo" />
            <%=Html.HiddenFor(model=>Model.Path)%>
        </p>

        <p>
            <%=Html.HiddenFor(model=>Model.Id)%>
            <input type="submit" value="Submit" /> <a href="/" title="Cancel">Cancel</a>
        </p>
        <%} %>
    </div>
</body>
</html>
