<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<MVCApp.Models.Photos>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    View
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <%MVCApp.Models.Photos p = (MVCApp.Models.Photos)ViewData["Data"]; %>
    <div class="container">
        <div class="text-center">
            <img src="<%=p.Path %>" alt="<%=p.Title %>" />
        </div>
        <!-- Duoshuo Comment BEGIN -->
        <div class="ds-thread"></div>
        <script type="text/javascript">
            var duoshuoQuery = { short_name: "walkingp" };
            (function () {
                var ds = document.createElement('script');
                ds.type = 'text/javascript'; ds.async = true;
                ds.src = 'http://static.duoshuo.com/embed.js';
                ds.charset = 'UTF-8';
                (document.getElementsByTagName('head')[0]
		        || document.getElementsByTagName('body')[0]).appendChild(ds);
            })();
        </script>
        <!-- Duoshuo Comment END -->
    </div>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="TabContent" runat="server">
            <li class="active"><a href="/Gallery/">Gallery</a></li>
            <li><a href="/Place/">Places</a></li>
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="ScriptContent" runat="server">
</asp:Content>
