    <link rel="stylesheet" href="style-map.css" type="text/css"/>
    <link rel="icon" type="image/png" href="Googlemaps.png">
    
<?php

	   session_start();

$servername = "localhost";
$username = "root";
$password = "";
$dbname = "base";
// Create connection
$conn = new mysqli($servername, $username, $password, $dbname);

// Check connection
if ($conn->connect_error) {
    die('<p class="paragraph">Connection failed: </p>' .$conn->connect_error);
} 
//echo "Connected successfully";
    $count=0;
    $ids=$_SESSION['ids'];
    $sqlf = "SELECT * FROM points where ids=$ids";
			$result = $conn->query($sqlf);
			if ($result->num_rows > 0) {
				echo '<option value="none"></option>';
				while($row = $result->fetch_assoc()) {
              $count=$count+1;
        }
      }

function query(){
		include_once("loginbase.php");
		$ids=$_SESSION['ids'];
		
		$sqld = "SELECT * FROM points where ids=$ids";
			$result = $con->query($sqld);
			if ($result->num_rows > 0) {
				echo '<option value="none"></option>';
				while($row = $result->fetch_assoc()) {
					echo '<option value="'.$row['idp'].'">'.$row['idp'].') '.$row['lat'].'-'.$row['lng'].'</option>';
				}
			}
			$con->close();
	}  

if($count<'9'){
if (isset($_POST['submitted'])){
  if(($_POST['aname']!='') and ($_POST['bname']!='')){ 

  $idw = $_SESSION['ids'];
  $aname = $_POST['aname'];
  $bname = $_POST['bname'];
  $sql = "insert into points values ('','$idw','$aname','$bname')";

// Create insert
if ($conn->query($sql) === TRUE) {
  header("Location: insert.php");
    //echo '<p class="paragraph">Insert was successfully</p>';
} else {
    echo '<p class="paragraph">Error insert a marker: </p>'. $conn->error;
}

$conn->close();
}else {
	echo '<p class="paragraph">Error insert a marker</p>';
}
}
}else{
  echo '<p class="paragraph">Max Markers</p>';
}

if (isset($_POST['delete'])){
	if($_POST['list']!="none"){
	    $list = $_POST['list'];
		$sql = "DELETE FROM points WHERE idp='$list'";

if ($conn->query($sql) === TRUE) {
     header("Location: insert.php");
    //echo '<p class="paragraph">Marker deleted successfully</p>';
} else {
    echo '<p class="paragraph">Error deleting marker: </p>' . $conn->error;
}
$conn->close();
	}
	else{
		echo '<p class="paragraph">First select a marker</p>';
	}
}   

 include_once("data.php");

?>
 
<!DOCTYPE html>
<html>
  <head>
    <title>Application</title>
    <meta name="viewport" content="initial-scale=1.0, user-scalable=no">
    <meta charset="utf-8">
     <link rel="stylesheet" href="jquery-ui.css" type="text/css"/>
     <link rel="stylesheet" href="style-map.css" type="text/css"/>
     <link rel="icon" type="image/png" href="Googlemaps.png">
    <style>
      html, body {
        height: 100%;
        margin: 0;
        padding: 0;
      }
      #map {
        height: 100%;
      }
      #floating-panel {
        position: absolute;
        top:2%;
        right: 0.3%;
        z-index: 5;
        background-color: #fff;
        width: 350px;
        margin: 10px auto;
        border-radius: 10px;
        background: linear-gradient(45deg, #607D8B, #cfbdef) no-repeat;
        padding: 10px;
        font-family: 'Roboto','sans-serif';      
      }
      .image{
        position:absolute;
        top:12%;
        left:0.4%;
        z-index: 5;
        width:100px; /*width of your image*/
        height:100px; /*height of your image*/
        background-image:url('Googlemaps2.png');
        margin:0; /* If you want no margin */
        padding:0; /*if your want to padding */
      }
    </style>
  </head>
  <body> 
  <div class="image"></div>
  <div id="floating-panel" class="ui-widget-content">
  
  <h1 class="title">Put your data</h1>
	<form method="post" action="insert.php" target="_self">    
  <input type="text"  placeholder="Number" id="label" name="i" class="input-texts"/>
  <input type="text"  placeholder="Latitude"  id="lat" name="aname" class="input-texts"/>
  <input type="text"  placeholder="Longitude"  id="lng" name="bname" class="input-texts"/>
  <input type="submit" value="Add A Marker" name="submitted" class="add-maker"/>
  <div class="flex">  
	<select name="list" class="input-texts">
	<?php query(); ?>
	</select>
	<input type="submit" value="Delete A Marker" name="delete" class="delete-maker"/>
  </div>
	</form>
   <div class="flex">
	<input id="latlng" type="text" value="37.9788,23.7245" class="input-texts"/>
	<input id="submit1" type="button" value="Geocode" class="geocode"/>
  </div>
  <div class="flex">
  <input id="address" type="textbox" value="Athens, Greece" class="input-texts"/>
  <input id="submit" type="button" value="Location" class="geocode"/>
  </div>
	<input onclick="drop();" type="button" value="Show Markers" class="show"/>
    <!--<input onclick="clearMarkers();" type="button" value="Hide Markers"class="show">
    <input onclick="showMarkers();" type="button" value="Show All Markers"class="show">
    <input onclick="deleteMarkers();" type="button" value="Delete Markers"class="clear">-->
    <input onclick="clearAll();" type="button" value="Clear All" class="clear"/>

  <form method="post" action="export.php" target="_self">
	<input type="submit" value="Complete Schedule" name="excel" class="submit"/>
	</form>
	</div>
    
<script src="jquery-3.1.0.min.js"></script><!--library-jquery download from jquery.com-->
    <div id="map"></div>

<!--//////////////////////////////////////////////////////////////////-->
  <script src="jquery-1.12.4.js"></script>  <!--Libraries-->
  <script src="jquery-ui.js"></script>  <!--Libraries-->

<script type="text/javascript">
 
 $('#floating-panel').draggable({
      cursor: "move"
          });
 $(function(){
    $("#floating-panel").resizable();
  });
  
var labels = '123456789';
var labelIndex = 0;
var ar =<?php echo $json;?>;
var markers = [];
var map;

      function drop() {
          clearMarkers();    
          //int path={1,2,0,3,4};
          //for (int i = 0; i < ar.length; i++) {
            // addMarkerWithTimeout(ar[path[i]] , i * 200);
        for (var i = 0; i < ar.length; i++) {
          addMarkerWithTimeout(ar[i] , i * 200);
        }
      }

      function addMarkerWithTimeout(position, timeout) {
        window.setTimeout(function() {
          var flightPath = new google.maps.Polyline({
          path: ar,
          geodesic: true,
          strokeColor: '#FF0000',
          strokeOpacity: 1.0,
          strokeWeight: 2
        });

        flightPath.setMap(map);

          markers.push(new google.maps.Marker({
            position: position,
            label: labels[labelIndex++ % labels.length],
            map: map,
            animation: google.maps.Animation.DROP
          }));
        }, timeout);
      }

      function clearMarkers() {
        for (var i = 0; i < markers.length; i++) {
          markers[i].setMap(null);
        }
        markers = [];
      }

</script>

<!--//////////////////////////////////////////////////////////////////-->

<script>

	//window.onbeforeunload = function () {return false;}
	// In the following example, markers appear when the user clicks on the map.
// Each marker is labeled with a single numerical character.
var labels = '123456789';
var labelIndex = 0;
var markers = [];
var map;
var poly;

      function initMap() {
          map = new google.maps.Map(document.getElementById('map'), {
          zoom: 8,
          center: {lat: 37.9788, lng: 23.7245}
        });

        var geocoder = new google.maps.Geocoder;
		    var infowindow = new google.maps.InfoWindow;

          document.getElementById('submit').addEventListener('click', function() {
          geocodeAddress(geocoder, map);
        });

  var geocoder1 = new google.maps.Geocoder;
  var infowindow1 = new google.maps.InfoWindow;

  document.getElementById('submit1').addEventListener('click', function() {
   geocodeLatLng(geocoder1, map, infowindow1);
 });
  
    // This event listener calls addMarker() when the map is clicked.
   google.maps.event.addListener(map, 'click', function(event) {
   addMarker(event.latLng, map);
  });
      }
	  
      function geocodeAddress(geocoder, resultsMap) {
        var address = document.getElementById('address').value;
        geocoder.geocode({'address': address}, function(results, status) {
          if (status === google.maps.GeocoderStatus.OK) {
            resultsMap.setCenter(results[0].geometry.location);
            var marker = new google.maps.Marker({
              map: resultsMap,
              position: results[0].geometry.location
            });
			markers.push(marker);
          } else {
            alert('Geocode was not successful for the following reason: ' + status);
          }
        });
      }
	  
	  
	  function geocodeLatLng(geocoder1, map, infowindow1) {
  var input = document.getElementById('latlng').value;
  var latlngStr = input.split(',', 2);
  var latlng = {lat: parseFloat(latlngStr[0]), lng: parseFloat(latlngStr[1])};
  geocoder1.geocode({'location': latlng}, function(results, status) {
    if (status === google.maps.GeocoderStatus.OK) {
      if (results[1]) {
        map.setZoom(11);
        var marker = new google.maps.Marker({
          position: latlng,
          map: map
        });
		markers.push(marker);
        infowindow1.setContent(results[1].formatted_address);
        infowindow1.open(map, marker);
      } else {
        window.alert('No results found');
      }
    } else {
      window.alert('Geocoder failed due to: ' + status);
    }
  });
}	  

// Adds a marker to the map.
function addMarker(location, map) {
  // Add the marker at the clicked location, and add the next-available label
  // from the array of alphabetical characters.
  var marker = new google.maps.Marker({
    position: location,
    label: labels[labelIndex++ % labels.length],
    map: map
  });
   markers.push(marker);
    var input3 = document.getElementById('label').value=labelIndex;
    var lat = location.lat();
	var lng = location.lng();
	var input1 = document.getElementById('lat').value=lat;
	var input2 = document.getElementById('lng').value=lng;
    var infowindow = new google.maps.InfoWindow({
    content: 'Latitude: ' + lat + '<br>Longitude: ' + lng
  });
  infowindow.open(map,marker);
}

// Sets the map on all markers in the array.
function setMapOnAll(map) {
  for (var i = 0; i < markers.length; i++) {
    markers[i].setMap(map);
  }
}

// Removes the markers from the map, but keeps them in the array.
function clearMarkers() {
  setMapOnAll(null);
}

// Shows any markers currently in the array.
function showMarkers() {
  setMapOnAll(map);
}

// Deletes all markers in the array by removing references to them.
function deleteMarkers() {
  clearMarkers();
  markers = [];
}

function clearAll() {
  location.reload();
}
</script>

    <script src="https://maps.googleapis.com/maps/api/js?key=AIzaSyBMsPoC6zm0r1sVWPluknWxdp0KNL9MwNE&callback=initMap">
	</script>
  </body> 
  </html>