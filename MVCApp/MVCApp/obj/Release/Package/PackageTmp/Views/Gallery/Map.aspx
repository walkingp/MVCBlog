﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    Gallery
</asp:Content>
<asp:Content ID="cntScript" ContentPlaceHolderID="ScriptContent" runat="server">
     <style type="text/css">
      html { height: 100% }
      body { height: 100%; margin: 0; padding: 0 }
      #map_canvas { height: 100%;color:#000; top:50px; }
    </style>
    <script type="text/javascript" src="http://ditu.google.cn/maps/api/js?sensor=false"></script>
    <script type="text/javascript">
        var geocoder;
        var map;
        var markersArray = [];
        var documentTitle = document.title;
        var data;
        function initialize() {
            data = $.getJSON('/handler/map_data.ashx', function (data) {
                renderMap(data);   
            });
        }
        function renderMap(data) {
            geocoder = new google.maps.Geocoder();
            var myOptions = {
                center: new google.maps.LatLng(35.651, 104.064),
                zoom: 5, //缩放比例
                mapTypeId: google.maps.MapTypeId.ROADMAP //地图类型 ?MapTypeId.ROADMAP ?MapTypeId.SATELLITE ?MapTypeId.HYBRID ?MapTypeId.TERRAIN 

            }
            map = new google.maps.Map(document.getElementById("map_canvas"), myOptions);
            var html = '';
            for (var i = 0, len = data.length; i < len; i++) {
                var lat = data[i].Latitude;
                var longLat = data[i].LongLatitude;
                var location = new google.maps.LatLng(lat, longLat);
                var title = data[i].Title ? data[i].Title : '未命名';
                var thumb = data[i].Path;
                var photo = '../album/photos/' + data[i].FileName;
                var marker = new google.maps.Marker({
                    map: map,
                    position: location,
                    title: title,
                    animation: google.maps.Animation.DROP //坐标动画效果
                });
                html += '<a rel="gallery" data-fancybox-group="gallery" id="_pic' + i + '" class="fancybox-effects-c" title="' + title + '" href="' + photo + '"><img src="' + thumb + '" /></a>';

                attachSecretMessage(marker, i);
            }
            $('#panel').html(html);
            $('#panel a[rel=gallery]').fancybox({
                helpers: {
                    title: {
                        type:'inside'
                    }
                },
                afterShow: function () {
                    var i = this.index;
                    var s = '<p style="position:absolute;left:0;bottom:-10px;font-size:11px;display:block; padding:2px;opacity:0.7;background:#000;color:#fff;">相机:' + (data[i]['Camera'] ? data[i]['Camera'] : 'unknown') +
				' 焦距:' + data[i]['Focal'] +
				' 曝光时间:' + data[i]['Exposure'] +
				' ISO:' + data[i]['ISO'] +
				' 拍摄时间:' + data[i]['CaptureTime'] +
				'</p>';
                    $('div.fancybox-outer').css('position', 'relative').append(s).find('p').width($('div.fancybox-outer').width() - 4);
                    $('div.fancybox-title').append(' <a style="float:right;color:#999;" href="map.html#_' + i + '" title="在地图中查看">View in map</a>');
                    document.title = $(this).attr('title') + ' - ' + documentTitle;
                },
                afterClose: function () {
                    location.hash = '';
                    document.title = documentTitle;
                }
            });
        }
        var winArr = [];
        function attachSecretMessage(m, i) {
            if (location.hash == '#_pic' + i) {
                infowindow.open(map, m); //默认打开信息窗口。
            }
            var that = i;
            google.maps.event.addListener(m, 'click', function () {
                for (var i = 0, len = winArr.length; i < len; i++) {
                    winArr[i].close();
                }
                $('#_pic' + that).click();
                location.hash = '_pic' + that;
                //curWin.open(map, m);
            });
        }
        //google.maps.event.addDomListener(window, 'load', initialize);
        $(function () {
            initialize();
            var hash = location.hash;
            if (hash) {
                $(hash).click();
            }
        });
    </script>
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="TabContent" runat="server">
            <li class="active"><a href="/Gallery/">Gallery</a></li>
            <li><a href="/Place/">Places</a></li>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div style="width:100%; height:100%;" id="map_canvas">
    </div>
    <div id="panel" style="display:none;position:absolute;left:10000px;bottom:0;"></div>
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
