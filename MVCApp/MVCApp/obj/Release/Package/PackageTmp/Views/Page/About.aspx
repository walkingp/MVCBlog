<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Blog.Master" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    About
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="mainContentPlaceHolder" runat="server">

<h3>WALKINGP</h3>
<p>坐标上海，一个很菜的C#软件工程师，略懂Java、Python，写过Android程序，目前正在往前端开发发展。</p>

<p>写代码只是副业，主业是玩，爱好很杂，电影、音乐、读书、旅行，跑步、骑行、游泳。</p>

<p>目前正热衷于跑马拉松<del>和学习日语</del>。</p>

<ul>
    <li>2013年9月8日 烟台马拉松 半程 时间：1h34m50s</li>
    <li>2013年9月28日 太原马拉松 半程 时间：1h33m34s</li>
    <li>2013年11月3日	杭州马拉松 全程 时间：3h50m43s</li>
    <li>2013年11月17日	南通马拉松 半程 时间：1h34m40s</li>
    <li>2013年12月1日	上海马拉松	全程 时间：3h57m3s</li>
</ul>
<p><del>最大梦想是和那个她牵手环游地球，近期想去的地方是川西和甘南，到哈尔滨滑雪，攀登四姑娘山。</del></p>
<p>最近在备战雅思中。</p>
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
    <script type="text/javascript">
        $(function () {
            $('ul.navbar-nav li').eq(0).removeClass('active');
            $('ul.navbar-nav li').eq(2).addClass('active');
        });
    </script>
</asp:Content>

<asp:Content ID="Content4" ContentPlaceHolderID="sidebarContentPlaceHolder" runat="server">
    <div class="box">
        <p>
            <img src="/images/bio-photo.jpg" alt="" class="bio-photo" /></p>
        <h4>WALKINGP</h4>
        <p>Programmer,Runner,Trithloner</p>
    </div>
    <div class="box">
        <h4>Links</h4>
        <ol class="list-unstyled">
            <li><a href="http://www.cnblogs.com/walkingp" title="" target="_blank">cnblogs</a></li>
            <li><a href="http://www.twitter.com/walkingp" title="" target="_blank">twitter</a></li>
        </ol>
    </div>
</asp:Content>
