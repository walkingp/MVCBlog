<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Manage.Master" Inherits="System.Web.Mvc.ViewPage<MVCApp.Models.Place>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    EditPlace
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="placeHolderMain" runat="server">

<div class="row">
    <div class="col-md-12">
        <div class="panel panel-default">
        <%using (Html.BeginForm("UpdatePlace", "Manage", FormMethod.Post, new { @class = "form-horizontal" }))
      { %>
            <div class="panel-heading">GPS info</div>
            <div class="panel-body">
                <div class="form-group">
                    <label for="Location">Location:</label>
                    <%=Html.TextBoxFor(model=>Model.Location, new {@class="textbox-control",style="width:200px;" })%>
                    <button type="button" id="location" onclick="GetGPS($('#Location').val());" class="btn btn-primary">Locatize</button>
                    <label for="Latitude">Latitude:</label>
                    <%=Html.TextBoxFor(model=>Model.Lati, new {@class="textbox-control",style="width:80px;" })%>
                    <label for="LongLatitude">LongLatitude:</label>
                    <%=Html.TextBoxFor(model=>Model.LongLati, new {@class="textbox-control",style="width:80px;" })%>
                    <button type="button" id="gps" onclick="GetAddr($('#Lati').val(),$('#LongLati').val());" class="btn btn-primary">Locatize</button>
                    Date :<input type="text" name="Date" value="<%=DateTime.Now.ToString("yyyy-MM-dd") %>" style="width:80px;" class="textbox-control" />
                    Star: <%=Html.DropDownListFor(model=>Model.Star,new[] {new SelectListItem(){Text="0",Value="0"},new SelectListItem(){Text="1",Value="1"},new SelectListItem(){Text="2",Value="2",Selected=true},new SelectListItem(){Text="3",Value="3"},new SelectListItem(){Text="4",Value="4"},new SelectListItem(){Text="5",Value="5"},}) %>
                </div>
                <div class="form-group">
                    <%=Html.HiddenFor(model=>Model.Id) %>
                    <%=Html.TextAreaFor(model=>Model.Notes, new {id="datetimepicker",@class="textbox-control", PlaceHolder="Notes", rows="4", style="width:100%;" }) %>
                    <p>
                    <button class="btn btn-primary" type="submit">Update</button>
                    </p>
                </div>
                <div class="form-group">
                    <div id="map_canvas" style="width:100%; height:400px;"></div>
                </div>
            </div>
        <%} %>
        </div>
    </div>
</div>
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="ScriptContent" runat="server">
    <link type="text/css" rel="stylesheet" href="/Assets/bootstrap-datepicker/bootstrap-datetimepicker.min.css" />
    <script type="text/javascript" src="http://ditu.google.cn/maps/api/js?sensor=false"></script>
    <script type="text/javascript" src="/Assets/bootstrap-datepicker/bootstrap-datetimepicker.min.js"></script>
    <script type="text/javascript" src="/Assets/bootstrap-datepicker/bootstrap-datetimepicker.zh-CN.js"></script>
    <script type="text/javascript">
        $('#datetimepicker').datetimepicker();
        var geocoder;
        var map;
        var markersArray = [];
        var documentTitle = document.title;
        var marker;
        function initialize() {
            var location = new google.maps.LatLng($('#Lati').val(), $('#LongLati').val());
            geocoder = new google.maps.Geocoder();
            var myOptions = {
                center: location,
                zoom: 5, //缩放比例
                mapTypeId: google.maps.MapTypeId.ROADMAP //地图类型 ?MapTypeId.ROADMAP ?MapTypeId.SATELLITE ?MapTypeId.HYBRID ?MapTypeId.TERRAIN
            }
            map = new google.maps.Map(document.getElementById("map_canvas"), myOptions);
            marker = new google.maps.Marker({
                map: map,
                position: location,
                draggable: true,
                animation: google.maps.Animation.DROP //坐标动画效果
            });
            google.maps.event.addListener(marker, 'drag', function () {
                updateMarkerPosition(marker.getPosition());
            });
            google.maps.event.addListener(marker, 'dragend', function () {
                geocodePosition(marker.getPosition());
            });
        }
        $(function () {
            initialize();
        });
        function updateMarkerPosition(latLng) {
            $('#Lati').val(latLng.k.toFixed(5));
            $('#LongLati').val(latLng.A.toFixed(5));
        }
        function geocodePosition(latLng) {
            GetAddr(latLng.k, latLng.A);
        }
        function GetGPS(addr) {
            var url = 'http://ditu.google.cn/maps/api/geocode/json?sensor=false&hl=zh_CN&address=' + addr;
            $.getJSON(url, function (data) {
                var geo = data.results[0].geometry.location;
                marker.setPosition(geo);
                map.panTo(geo);
                $('#Lati').val(geo.lat.toFixed(5));
                $('#LongLati').val(geo.lng.toFixed(5));
            });
        }
        function GetAddr(lati, longlati) {
            var url = 'http://ditu.google.cn/maps/api/geocode/json?sensor=false&hl=zh_CN&latlng=' + lati + ',' + longlati;
            var json = $.getJSON(url, function (data) {
                var addr = data.results[0].formatted_address;//address_components[0].long_name;
                $('#Location').val(addr);
            });
        }
    </script>
</asp:Content>
