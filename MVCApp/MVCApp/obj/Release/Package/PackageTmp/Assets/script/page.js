$(document).ready(function () {
    // initialize
    var documentTitle = document.title;
    $('#gallery a[rel=gallery]').fancybox({
        helpers: {
            title: {
                type: 'inside'
            }
        },
        nextEffect: 'fade',
        afterShow: function () {
            var json = JSON.parse($(this.element).attr('data'));
            var id = $(this.element).attr('id');
            var s = '<p style="position:absolute;left:0;bottom:-10px;font-size:11px;display:block; padding:2px;opacity:0.7;background:#000;color:#fff;">相机:' + json.Camera +
		' 焦距:' + json.Focal +
		' 曝光时间:' + json.Exposure +
		' ISO:' + json.ISO +
		' 拍摄时间:' + json.CaptureTime +
		'</p>';
            $('div.fancybox-outer').css('position', 'relative').append(s).find('p').width($('div.fancybox-outer').width() - 4);
            $('div.fancybox-title').append(' <a style="float:right;color:#999;" href="map#' + id + '" title="在地图中查看">View in map</a>');
            location.hash = id;
            document.title = $(this).attr('title') + ' - ' + documentTitle;
        },
        afterClose: function () {
            document.title = documentTitle;
            //location.hash = '';
        }
    });
    var hash = location.hash;
    if (hash) {
        //$(hash).click();
    }
    $("img.scrollLoading").lazyload();
});
