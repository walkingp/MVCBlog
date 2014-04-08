<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    Places
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div style="width:100%; height:100%;" id="map_canvas">
    </div>
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="ScriptContent" runat="server">
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
            data = $.getJSON('/handler/place_data.ashx?t=' + Math.random(), function (data) {
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
            for (var i = 0, len = data.length; i < len; i++) {
                var lat = data[i].Lati.replace(',','');
                var longLat = data[i].LongLati.replace(',','');
                var location = new google.maps.LatLng(lat, longLat);
                var title = data[i].Location ? data[i].Location : '未命名';
                var star = parseInt(data[i].Star);
                var d = 'Couldn\'t remember';
                if (data[i].Date) {
                    var from = new Date(data[i].Date);
                    d = from.getFullYear() + '-' + from.getMonth() + '-' + from.getDate();
                }
                var s = '<h4>' + title + '</h4>' +
                    '<p>';
                while (star-- > 0) {
                    s += '<span class="glyphicon glyphicon-star"></span>';
                }
                s += '<br /><span style="font-size:10px; color:#666;">(' + d + ')</span></p><p>' + data[i].Notes + '</p>';
                var infowindow = new google.maps.InfoWindow({
                    content: s
                });
                var marker = new google.maps.Marker({
                    map: map,
                    position: location,
                    title: title,
                    animation: google.maps.Animation.DROP //坐标动画效果
                });
                attachSecretMessage(marker, infowindow);
            }
        }
        var winArr = [];
        function attachSecretMessage(marker,infoWindow) {
            winArr.push(infoWindow);
            google.maps.event.addListener(marker, 'click', function () {
                for (var i = 0, len = winArr.length; i < len; i++) {
                    winArr[i].close();
                }

                infoWindow.open(map, marker);
            });
        }
        //google.maps.event.addDomListener(window, 'load', initialize);
        $(function () {
            initialize();
        });
    </script>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="TabContent" runat="server">
            <li><a href="/Gallery/">Gallery</a></li>
            <li class="active"><a href="/Place/">Places</a></li>
</asp:Content>
