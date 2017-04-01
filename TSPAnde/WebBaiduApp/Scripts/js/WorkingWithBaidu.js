var map;
var markers;
var points = [];
var distances;

$(function () {
    MapInit();
    AddEvents();
});

function MapInit() {
    map = new BMap.Map("allmap");
    map.centerAndZoom(new BMap.Point(120.1372, 30.2837), 15);
}

function AddEvents() {
    map.addEventListener("click", showInfo);
    map.addEventListener("click", AddMarker);
}

function AddMarker(e) {
    var marker = new BMap.Marker(e.point);
    points.push(e.point);
    map.addOverlay(marker);
    UpdateToolbox();
}

function showInfo(e) {
    alert(e.point.lng + ", " + e.point.lat);
}

function UpdateToolbox() {
    
}

function TestRoute() {
    {
        TwoPointDrivingRoute(points[0], points[1]);
    }
}

function TwoPointDrivingRoute(point1, point2) {
    //var myP1 = new BMap.Point(116.380967, 39.913285);    //起点
    //var myP2 = new BMap.Point(116.424374, 39.914668);    //终点
    var myP1 = point1;    //起点
    var myP2 = point2;    //终点
    var myIcon = new BMap.Icon("http://developer.baidu.com/map/jsdemo/img/Mario.png", new BMap.Size(32, 70), {    //小车图片
        //offset: new BMap.Size(0, -5),    //相当于CSS精灵
        imageOffset: new BMap.Size(0, 0)    //图片的偏移量。为了是图片底部中心对准坐标点。
    });
    //var driving2 = new BMap.DrivingRoute(map, { renderOptions: { map: map, autoViewport: true } });    //驾车实例
    //driving2.setPolicy(BMAP_DRIVING_POLICY_LEAST_DISTANCE);
    //driving2.search(myP1, myP2);    //显示一条公交线路

    window.run = function () {
        var driving = new BMap.DrivingRoute(map, { renderOptions: { map: map, autoViewport: true } });    //驾车实例
        //BMAP_DRIVING_POLICY_LEAST_TIME	最少时间。
        //BMAP_DRIVING_POLICY_LEAST_DISTANCE	最短距离。
        //BMAP_DRIVING_POLICY_AVOID_HIGHWAYS	避开高速。
        driving.setPolicy(BMAP_DRIVING_POLICY_LEAST_DISTANCE);
        driving.search(myP1, myP2);
        driving.setSearchCompleteCallback(function () {
            var pts = driving.getResults().getPlan(0).getRoute(0).getPath();    //通过驾车实例，获得一系列点的数组
            var paths = pts.length;    //获得有几个点

            var carMk = new BMap.Marker(pts[0], { icon: myIcon });
            map.addOverlay(carMk);
            i = 0;
            function resetMkPoint(i) {
                carMk.setPosition(pts[i]);
                if (i < paths) {
                    setTimeout(function () {
                        i++;
                        resetMkPoint(i);
                    }, 100);
                }
            }

            setTimeout(function() {
                resetMkPoint(5);
            }, 100);
        });
    }

    setTimeout(function () {
        run();
    }, 1500);
}
