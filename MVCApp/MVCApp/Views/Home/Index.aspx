<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<MVCApp.Models.Photos>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    Index
</asp:Content>
<asp:Content ID="cntScript" ContentPlaceHolderID="ScriptContent" runat="server">
<script type="text/javascript" src="Scripts/page.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
<div class="container-fluid">
    <div id="gallery" data-toggle="modal-gallery" data-target="#modal-gallery">
    <%List<MVCApp.Models.Photos> photos = (List<MVCApp.Models.Photos>)ViewData["Photos"]; %>
    <%List<string> listJson=(List<string>)ViewData["Data"]; %>
    <% for (int i = 0, len = photos.Count; i < len; i++)
        { %>
        <a data-fancybox-group="gallery" rel="gallery" data='<%=listJson[i] %>' title="<%=string.IsNullOrEmpty(photos[i].Title) ? photos[i].FileName : photos[i].Title %>" id="_pic<%=i %>" href="<%=photos[i].Path %>">
            <img src="<%=MVCApp.Utility.ImageHelper.GetThumbImagePath(photos[i].Path) %>">
        </a>
    <% }%>
    </div>
</div>
</asp:Content>
