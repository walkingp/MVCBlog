<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Blog.Master" Inherits="System.Web.Mvc.ViewPage<MVCApp.Models.Blog>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    walkingp's blog - just do IT.
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="mainContentPlaceHolder" runat="server">
    <%List<MVCApp.Models.Blog> blogs = (List<MVCApp.Models.Blog>)ViewData["Blogs"]; %>
    <% for (int i = 0, len = blogs.Count; i < len; i++)
       { %>
    <h3><a href="/Blog/<%=blogs[i].Id %>/" title="<%=blogs[i].Title %>"><%=blogs[i].Title %></a></h3>
    <p class="gray"><%=blogs[i].PostTime %> by <a href="/About/" title="<%=blogs[i].UserName %>"><%=blogs[i].UserName %></a></p>
    <div>
        <%=blogs[i].Content %>
    </div>
    <%} %>
    <ul class="pagination">
        <%=ViewData["page"].ToString() %>
    </ul>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="sidebarContentPlaceHolder" runat="server">
    <div class="box">
        <p>
            <img src="/images/bio-photo.jpg" alt="" class="bio-photo" /></p>
        <h4>WALKINGP</h4>
        <p>Programmer,Runner,Trithloner</p>
    </div>
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
    <div class="box">
        <h4>Links</h4>
        <ol class="list-unstyled">
            <li><a href="http://www.cnblogs.com/walkingp" title="" target="_blank">cnblogs</a></li>
            <li><a href="http://www.twitter.com/walkingp" title="" target="_blank">twitter</a></li>
        </ol>
    </div>
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="ScriptContent" runat="server">
</asp:Content>
