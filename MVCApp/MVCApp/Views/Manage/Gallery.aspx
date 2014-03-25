<%@ Page Language="C#" MasterPageFile="~/Views/Shared/Manage.Master" Inherits="System.Web.Mvc.ViewPage<MVCApp.Models.Photos>" %>

<asp:Content ID="titleContent" ContentPlaceHolderID="TitleContent" runat="server">
    Index
</asp:Content>
<asp:Content ID="cntContent" ContentPlaceHolderID="ScriptContent" runat="server">
<script type="text/javascript" src="/Assets/fancybox2/jquery-1.10.1.min.js"></script>
<script type="text/javascript" src="/Assets/fancybox2/jquery.mousewheel-3.0.6.pack.js"></script>
<script type="text/javascript" src="/Assets/fancybox2/jquery.fancybox.js"></script>
<link rel="stylesheet" type="text/css" href="/Assets/fancybox2/jquery.fancybox.css" media="screen" />
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
    $(document).ready(function () {
        $('#gallery a[rel=gallery]').fancybox({
            helpers: {
                title: {
                    type: 'inside'
                }
            },
            nextEffect: 'fade',
        });
    });

    </script>
</asp:Content>


<asp:Content ID="Content2" ContentPlaceHolderID="placeHolderMain" runat="server">
    <%=ViewData["Message"].ToString() %>
    <h2 class="sub-header">Gallery Manager
        <a href="/photos/add/" class="btn btn-primary" role="button">Upload</a>
    </h2>
    <div id="gallery">
    <%List<MVCApp.Models.Photos> photos = (List<MVCApp.Models.Photos>)ViewData["Photos"]; %>
    <% for (int i = 0, len = photos.Count; i < len; i++)
        { %>
        <div style="float:left; margin:5px 3px;">
        <a data-fancybox-group="gallery" rel="gallery" title="<%=string.IsNullOrEmpty(photos[i].Title) ? photos[i].FileName : photos[i].Title %>" id="_pic<%=i %>" href="<%=photos[i].Path %>"><img src="<%=MVCApp.Utility.ImageHelper.GetThumbImagePath(photos[i].Path,"s") %>" /></a>
        <br />
        <%=Html.ActionLink("Edit","Edit","Photos",new { photos[i].Id },null)%>
        <%=Html.ActionLink("Delete", "Delete", "Manage", new { photos[i].Id }, new { onclick="return confirm('Confirm to delete?')"  })%>
            </div>

    <% }%>
        <div class="clearfix"></div>
    </div>
    <ul class="pagination">
        <%=ViewData["html"].ToString() %>
    </ul>
</asp:Content>
