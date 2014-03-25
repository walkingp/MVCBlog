<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Manage.Master" Inherits="System.Web.Mvc.ViewPage<MVCApp.Models.Photos>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    Edit
</asp:Content>


<asp:Content ID="Content3" ContentPlaceHolderID="ScriptContent" runat="server">
    <script type="text/javascript" src="http://ditu.google.cn/maps/api/js?sensor=false"></script>
    <script type="text/javascript">
        var geocoder;
        var map;
        var markersArray = [];
        var documentTitle = document.title;
        var marker;
        function initialize() {
            var location=new google.maps.LatLng($('#lat').val(), $('#longlat').val());
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
            $('#Latitude').val(latLng.k.toFixed(5));
            $('#LongLatitude').val(latLng.A.toFixed(5));
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
                $('#Latitude').val(geo.lat);
                $('#LongLatitude').val(geo.lng);
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

<asp:Content ID="Content2" ContentPlaceHolderID="placeHolderMain" runat="server">
    <h2>Edit item</h2>
    <%using (Html.BeginForm("Save", "Photos", FormMethod.Post, new { enctype = "multipart/form-data", @class = "form-horizontal" }))
      { %>
    <div class="row">
        <div class="col-md-6">
            <div class="panel panel-default">
                <div class="panel-heading">
                    Basic Info
                    
                </div>
                <div class="panel-body">
                    <div class="form-group">
                        <label for="Title" class="col-sm-3 control-label">Name:</label>
                        <div class="col-sm-8">
                            <%=Html.TextBoxFor(model => Model.Title,new {@class="form-control" })%>
                            <%=Html.ValidationMessageFor(model => Model.Title) %>
                        </div>
                    </div>
                    <div class="form-group">
                        <label for="photo" class="col-sm-3 control-label">Avatar:</label>
                        <div class="col-sm-8">
                            <input type="file" id="photo" name="photo" />
                            <%=Html.HiddenFor(model=>Model.Path)%>
                        </div>
                    </div>
                    <div class="form-group">
                        <label for="CaptureTime" class="col-sm-3 control-label">Capture Time:</label>
                        <div class="col-sm-8">
                            <%=Html.TextBoxFor(model=>Model.CaptureTime, new {@class="form-control" })%>
                        </div>
                    </div>
                    <div class="form-group">
                        <label for="Camera" class="col-sm-3 control-label">Camera:</label>
                        <div class="col-sm-8">
                            <%=Html.TextBoxFor(model=>Model.Camera, new {@class="form-control" })%>
                        </div>
                    </div>
                    <div class="form-group">
                        <label for="Manufacturer" class="col-sm-3 control-label">Manufacturer:</label>
                        <div class="col-sm-8">
                            <%=Html.TextBoxFor(model => Model.Manufacturer, new {@class="form-control" })%>
                        </div>
                    </div>
                    <div class="form-group">
                        <label for="ISO" class="col-sm-3 control-label">ISO:</label>
                        <div class="col-sm-8">
                            <%=Html.TextBoxFor(model=>Model.ISO, new {@class="form-control" })%>
                        </div>
                    </div>
                    <div class="form-group">
                        <label for="Aperture" class="col-sm-3 control-label">Aperture:</label>
                        <div class="col-sm-8">
                            <%=Html.TextBoxFor(model=>Model.Aperture, new {@class="form-control" })%>
                        </div>
                    </div>
                    <div class="form-group">
                        <label for="Exposure" class="col-sm-3 control-label">Exposure:</label>
                        <div class="col-sm-8">
                            <%=Html.TextBoxFor(model=>Model.Exposure, new {@class="form-control" })%>
                        </div>
                    </div>
                    <div class="form-group">
                        <label for="Focal" class="col-sm-3 control-label">Focal:</label>
                        <div class="col-sm-8">
                            <%=Html.TextBoxFor(model=>Model.Focal, new {@class="form-control" })%>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <div class="col-md-6">
            <div class="panel panel-default">
                <div class="panel-heading">GPS info</div>
                <div class="panel-body">
                    <div class="form-group">
                        <label for="Latitude">Latitude:</label>
                        <%=Html.TextBoxFor(model=>Model.Latitude, new {@class="textbox-control",style="width:80px;" })%>
                        <%=Html.HiddenFor(model=>Model.Latitude, new {id="lat" })%>
                        <label for="LongLatitude">LongLatitude:</label>
                        <%=Html.TextBoxFor(model=>Model.LongLatitude, new {@class="textbox-control",style="width:80px;" })%>
                        <%=Html.HiddenFor(model=>Model.LongLatitude, new {id="longlat" })%>
                        <button type="button" id="gps" onclick="GetAddr($('#Latitude').val(),$('#LongLatitude').val());" class="btn btn-primary">Locatize</button>
                    </div>
                    <div class="form-group">
                        <label for="Location">Location:</label>
                        <%=Html.TextBoxFor(model=>Model.Location, new {@class="textbox-control",style="width:380px;" })%>
                        <button type="button" id="location" onclick="GetGPS($('#Location').val());" class="btn btn-primary">Locatize</button>
                    </div>
                    <div class="form-group">
                        <div id="map_canvas" style="width:100%; height:400px;"></div>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <div class="row">
        <div>
            <div class="form-group">
                <%=Html.HiddenFor(model=>Model.Id)%>
                <%=Html.HiddenFor(model=>Model.Path,new {id="hPhoto",name="hPhoto"}) %>
                <button type="submit" class="btn btn-primary">Submit</button>
                <a href="/manage/gallery/" title="Cancel">Cancel</a>
            </div>
        </div>
    </div>
    <%} %>
</asp:Content>
