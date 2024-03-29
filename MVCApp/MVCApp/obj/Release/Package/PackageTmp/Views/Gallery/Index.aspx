﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<MVCApp.Models.Photos>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    Gallery
</asp:Content>
<asp:Content ID="cntScript" ContentPlaceHolderID="ScriptContent" runat="server">
<script type="text/javascript" src="/Assets/jquery.lazyload/jquery.lazyload.js"></script>
<script type="text/javascript" src="/Assets/script/page.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
<div class="container">
    <div id="gallery" data-toggle="modal-gallery" data-target="#modal-gallery">
    <%List<MVCApp.Models.Photos> photos = (List<MVCApp.Models.Photos>)ViewData["Photos"]; %>
    <%List<string> listJson=(List<string>)ViewData["Data"]; %>
    <% for (int i = 0, len = photos.Count; i < len; i++)
        { %>
        <a data-fancybox-group="gallery" rel="gallery" data='<%=listJson[i] %>' title="<%=string.IsNullOrEmpty(photos[i].Title) ? photos[i].FileName : photos[i].Title %>" id="_pic<%=i %>" href="<%=photos[i].Path %>">
            <img class="scrollLoading" src="/Assets/jquery.lazyload/ajax-loading.gif" data-original="<%=MVCApp.Utility.ImageHelper.GetThumbImagePath(photos[i].Path,"s") %>" />
        </a>
    <% }%>
    </div>
</div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="TabContent" runat="server">
            <li class="active"><a href="/Gallery/">Gallery</a></li>
            <li><a href="/Place/">Places</a></li>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="ViewContent" runat="server">
          <ul class="nav navbar-nav navbar-right">
            <li class="dropdown">
                <a href="/Gallery/" class="dropdown-toggle" data-toggle="dropdown">View Mode <b class="caret"></b></a>
                <ul class="dropdown-menu">
                    <li><a href="/Gallery/">List</a></li>
                    <li><a href="/Gallery/Map/">Map</a></li>
                </ul>
            </li>
          </ul>
</asp:Content>
