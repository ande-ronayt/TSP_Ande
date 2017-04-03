var map;
var markers = [];
var points = [];
var descriptions = ["",];
var directions = [];
var distances = [];
var getDistancesStatus = 0;
var colors = ["#0030ff", "#00ff30", "#ff3000"];
$(function () {
    MapInit();
    AddEvents();
});


function MapInit() {
    map = new BMap.Map("allmap");
    map.centerAndZoom(new BMap.Point(120.1372, 30.2837), 15);

    map.enableScrollWheelZoom(true);

    var top_left_navigation = new BMap.NavigationControl();  //左上角，添加默认缩放平移控件
    map.addControl(top_left_navigation);
}

function AddEvents() {
    map.addEventListener("click", clickOnMap);
}

function clickOnMap(e) {
    addMarker(e.point, points.length+1);
    addDescription(e.point);
    UpdateToolbox();
}

function addDescription(point){
    var myGeo = new BMap.Geocoder();
    myGeo.getLocation(point, function(result){
        var indx = findIndex(result.point);
        var addComp = result.addressComponents;
        descriptions[indx] = addComp.street + ", " + addComp.streetNumber;
        UpdateToolbox();
    });
}

function addMarker(point, id){
    var marker = new BMap.Marker(point);
    var label = new BMap.Label("id="+id,{offset:new BMap.Size(20,-10)});
    points.push(point);
    map.addOverlay(marker);
    marker.setLabel(label);
    markers.push(marker);
    descriptions.push("");
}

function showInfo(e) {
    alert(e.point.lng + ", " + e.point.lat);
}

function setAsHome(indx){
    $("#homeIdInput").val(indx+1);
}

function deleteMarker(indx){
    points.splice(indx,1);
    var marker = markers[indx];
    markers.splice(indx,1);

    var allOverlay = map.getOverlays();
    for (var i = 0; i < allOverlay.length; i++){
            try{
            if(allOverlay[i].getLabel().content == ("id="+(indx+1))){
                map.removeOverlay(allOverlay[i]);
                return false;
            }
            } finally{
                continue;
            }
        }
    UpdateToolbox();
} 

function UpdateToolbox() {
    var pointTableHtml = "";
    for(var i = 0; i<points.length; i++){
        updateLabel(points[i], i+1);
        pointTableHtml += "<div class='oneMarkerRow row'>";
         pointTableHtml += "<div class='row'>";
         pointTableHtml += "<div class='col-xs-9'>";
          pointTableHtml += "<span>" + (i+1) + "</span>"   ;
         pointTableHtml += "</div>";
          pointTableHtml += "<div class='col-xs-9'>"
           pointTableHtml += "<div class='col-xl-2'>" + points[i].lng + "</div> ";
           pointTableHtml += "<div class='col-xl-2'>" + points[i].lat + "</div> ";
           pointTableHtml += "<div class='col-xl-8'>" + descriptions[i] + "</div> ";
          pointTableHtml += "</div>";
          pointTableHtml += "<div class='col-xs-2'>";
           //marker actions:
           pointTableHtml += "<div class='row'>";
            pointTableHtml += "<button onclick='setAsHome(" + i + ")' >Home</button>";
           pointTableHtml += "</div>";
           pointTableHtml += "<div class='row'>";
            pointTableHtml += "<button class='bgRed' onclick='deleteMarker(" + i + ")' >Delete</button>";
           pointTableHtml += "</div>";
          pointTableHtml += "</div>";
         pointTableHtml += "</div>";
        pointTableHtml += "</div>";
    }

    $("#pointsTable").html(pointTableHtml);
}

function updateLabel(point, id){
    var allOverlay = map.getOverlays();
    for (var i = 0; i < allOverlay.length; i++){
        try{
            var pos = allOverlay[i].getPosition();
            if (pos.lat == point.lat && pos.lng == point.lng){
                allOverlay[i].getLabel().setContent("id="+id);
            }
        }
        finally{
            continue;
        }
    }
}

function loadTestData(){
    points = [
        new BMap.Point(120.132026, 30.267669), //Yuquan
        new BMap.Point(120.076403, 30.285945), //Xixi
        new BMap.Point(120.093291, 30.314383), //Zijingang
        new BMap.Point(120.159119, 30.265423), //Xihu 1
        new BMap.Point(120.169611, 30.259621), //Xihu 2
        new BMap.Point(120.215029, 30.253382), //Shimin zhongxing
        new BMap.Point(120.116862, 30.226487), //Long Jing
        new BMap.Point(120.142231, 30.219684), //Zoo
        new BMap.Point(120.031775, 30.286632), //Alibaba
        new BMap.Point(120.009641, 30.298606) //Dream Town 
    ];
    descriptions = [
        "Yuquan campus",
        "Xixi Shidi",
        "Zhejiang Daxue, Zijingang",
        "Xihu 1",
        "Xihu 2",
        "Shimin zhongxing",
        "Long Jing",
        "Zoo",
        "Alibaba",
        "Alibaba - Dream Town"
    ];

    for(var i = 0; i< points.length; i++){
        var marker = new BMap.Marker(points[i]);
        var label = new BMap.Label(("id="+(i+1)),{offset:new BMap.Size(20,-10)});
        map.addOverlay(marker);
        marker.setLabel(label);
    }

    UpdateToolbox();
}

function calculateAllDistances(){
    distances = [];
    directions = [];
    curDistance = [];
    curDirection = [];
    getDistancesStatus = 0;
    updateGetDistancesProgress();
    //fill with zero:
    for(var i =0; i<points.length+1; i++){
        curDistance = [];
        curDirection = [];
        for(var j=0; j<points.length+1; j++){
            curDistance.push(0);
            curDirection.push([]);
        }

        distances.push(curDistance);
        directions.push(curDirection);
    }

    for(var i =0; i<points.length; i++){
        var point1 = points[i];
        for(var j = 0; j < points.length; j++){
            if (i == j) continue;
            var point2 = points[j];
            findDistanceBetweenTwoPoints(point1, point2);
        }
    }    
}

function findRoute(){
    //map.clearOverlays();
    getShortestPath();
}

function getShortestPath() {
    var requestData = {};
    requestData.Count = points.length;
    requestData.Home = getHomeId();
    requestData.DistancesList = converDistancesMatrixToList(distances);
    requestData.DistancesMatrix = distances;
    requestData.TravellerAmount = getTravellerAmount();
    $.ajax({
        type: 'post',
        url: '/home/getShortestPath',
        data: JSON.stringify(requestData),
        contentType: 'application/json; charset=utf-8',
        success: getShortestPathSuccess,
        error: function (response) {
            alert("error occured");
        }
    });
}

function converDistancesMatrixToList(ds) {
    var disList = [];
    for (var i = 0; i < ds.length; i++)
        disList = disList.concat(ds[i]);
    return disList;
}

function getShortestPathSuccess(response){
    printPath(response);
    var mtspPath = response;
    var fullPath = [];
    for (var i = 0; i < mtspPath.length; i++) {
        var onePath = mtspPath[i];
        fullPath = fullPath.concat(onePath);
        for (var j = 0; j < onePath.length - 1; j++) {
            drawRoute(onePath[j], onePath[j + 1], colors[i])
        }
    }

    drawPerson(fullPath);
}

function printPath(pathes){
    var html = "";
    var j;
    for(var i = 0; i < pathes.length; i++){
        html += "<div class='row'>"
        html += "#" + (i+1) + " : ";
        for(j = 0; j < pathes[i].length-1; j++){
            html += pathes[i][j] + " - ";
        }
        html += pathes[i][j];
        html += "</div>"
    }

    $("#path").html(html);
}

function getTravellerAmount() {
    return $('#travAmInput').val();
}

function getHomeId() {
    return $('#homeIdInput').val();
}

function getRandomPath(){
    var count = points.length;
    var rPath = [];
    for(var i=1; i<=count; i++){
        rPath.push(i);
    }

    //Math.floor(Math.random()*11)
    for(var i = 0; i < rPath.length; i++){
        var j = Math.floor(Math.random(rPath.length));
        var tmp = rPath[i];
        rPath[i] = rPath[j];
        rPath[j] = tmp;
    }
    rPath.push(rPath[0]);
    return rPath;
}

function drawRoute(indx1, indx2, color) {
    {
    var opacity = 0.45;
    var path = directions[indx1][indx2][0];
    map.addOverlay(new BMap.Polyline(path, {strokeColor: color,strokeOpacity:opacity,strokeWeight:6,enableMassClear:true}));
    }
}

function drawPerson(fullPath){
    var myIcon = new BMap.Icon("http://developer.baidu.com/map/jsdemo/img/Mario.png", new BMap.Size(32, 70), {    //小车图片
        //offset: new BMap.Size(0, -5),    //相当于CSS精灵
        imageOffset: new BMap.Size(0, 0)    //图片的偏移量。为了是图片底部中心对准坐标点。
      });

    var pts = getFullPath(fullPath);
    var pathLength = pts.length;
    var carMk = new BMap.Marker(pts[0],{icon:myIcon});
    map.addOverlay(carMk);
    i=0;
    function resetMkPoint(i){
        carMk.setPosition(pts[i]);
        if(i < pathLength){
            setTimeout(function(){
                i++;
                resetMkPoint(i);
            },10);
        }
    }
    setTimeout(function(){
        resetMkPoint(5);
    },100)

}


function getFullPath(ids){
    var fullPath = [];
    for(var i = 0; i<ids.length-1; i++){
        fullPath = fullPath.concat(directions[ids[i]][ids[i+1]][0]);
    }
    
    return fullPath;
}

function findDistanceBetweenTwoPoints(point1, point2){
    var driving = new BMap.DrivingRoute(map);
    driving.setPolicy(BMAP_DRIVING_POLICY_LEAST_DISTANCE);
    driving.search(point1, point2);
    driving.setSearchCompleteCallback(function (result) {
        saveRoute(result);
        writeDistance(result);
    });
}

function saveRoute(result){
    var id1 = findIndex(result.getStart().point) + 1;
    var id2 = findIndex(result.getEnd().point) + 1;

    var route = result.getPlan(0).getRoute(0);
    directions[id1][id2].push(route.getPath());
}

function writeDistance(result){
    var id1 = findIndex(result.getStart().point) + 1;
    var id2 = findIndex(result.getEnd().point) + 1;

    var distance = result.getPlan(0).getDistance();
    distance = extractDistance(distance);

    distances[id1][id2] = distance;
    getDistancesStatus++;
    updateGetDistancesProgress();
}

function updateGetDistancesProgress(){
    totalDistances = points.length*(points.length-1);
    var progress = 100.0*getDistancesStatus/totalDistances;
    var elem = document.getElementById("myBar"); 
    var width = progress;
    elem.style.width = width + '%';

    //id = getDistancesProgressBar
    var html = "Get Distances Progress: " + progress + "%";
    $("#getDistancesProgressBar").html(html);
    $("#getDistanceProgressRow").removeClass('hide');
}

function extractDistance(distance){
    var subKm = "公里";
    var subM = "米";
    var index = distance.indexOf(subKm);
    if (index != -1)
        return distance.substring(0, index);
    index = distance.indexOf(subM);
    if (index != -1)
        return distance.substring(0, index)/1000;
    alert("Problem with getting distance! Cur distance: " + distance);
    return distance;
}

function findIndex(point){
    for(var i = 0; i < points.length; i++){
        if (points[i].lat == point.lat && points[i].lng == point.lng)
            return i;
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
