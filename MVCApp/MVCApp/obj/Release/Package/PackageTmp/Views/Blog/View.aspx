<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Blog.Master" Inherits="System.Web.Mvc.ViewPage<MVCApp.Models.Blog>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    <%=ViewData["title"].ToString() %>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="mainContentPlaceHolder" runat="server">
    <%
        MVCApp.Models.Blog blog = (MVCApp.Models.Blog)ViewData["Blog"];
        MVCApp.Models.Blog previousBlog = (MVCApp.Models.Blog)ViewData["PreviousBlog"];
        MVCApp.Models.Blog nextBlog = (MVCApp.Models.Blog)ViewData["NextBlog"]; 
    %>
    <h3><%=blog.Title %></h3>
    <p class="gray"><%=blog.PostTime %> by <a href="#" title="<%=blog.UserName %>"><%=blog.UserName %></a></p>
    <div>
        <%=blog.Content %>
    </div>
    <div class="h6">
        <ol class="list-unstyled">
            <li>< Previous:
                <% if (previousBlog != null)
                   { %>
                <a href="/Blog/<%=previousBlog.Id %>/" title="<%=previousBlog.Title %>"><%=previousBlog.Title %></a></li>
            <%}
                   else
                   { %>
                None
            <%} %>
            <li>> Next:
                <% if (nextBlog != null)
                   { %>
                <a href="/Blog/<%=nextBlog.Id %>/" title="<%=nextBlog.Title %>"><%=nextBlog.Title %></a>
                <%}
                   else
                   { %>
                None
            <%} %>
            </li>
        </ol>
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
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="ScriptContent" runat="server">
</asp:Content>

<asp:Content ID="Content4" ContentPlaceHolderID="sidebarContentPlaceHolder" runat="server">
    <div class="box">
        <h4>Lastest</h4>
        <ol class="list-unstyled">
            <%List<MVCApp.Models.Blog> blogs = (List<MVCApp.Models.Blog>)ViewData["LastestBlogs"]; %>
            <% for (int i = 0, len = blogs.Count; i < len; i++)
               { %>
            <li>
                <a href="/Blog/<%=blogs[i].Id %>/" title="<%=blogs[i].Title %>"><%=blogs[i].Title %></a>
                <small>(<%=blogs[i].PostTime.ToString("yyyy-MM-dd") %>)</small>
            </li>
            <%} %>
        </ol>
    </div>
</asp:Content>
