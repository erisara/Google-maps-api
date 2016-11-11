<!DOCTYPE html>
<html>
  <head>
    <title>Schedules</title>
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
        top: 2%;
        right: 25%;
        z-index: 5;
        background-color: #fff;
        width: 750px;
        margin: 10px auto;
        border-radius: 10px;
        background: linear-gradient(45deg, #607D8B, #cfbdef) no-repeat;
        padding: 10px;
        font-family: 'Roboto','sans-serif';      
      }
      .image {
        position:absolute;
        top:10%;
        left:0.4%;
        z-index: 5;
        width:100px; /*width of your image*/
        height:100px; /*height of your image*/
        background-image:url('Googlemaps2.png');
        margin:0; /* If you want no margin */
        padding:0; /*if your want no padding */
      }
    </style>
  </head>
  <body> 

  <div class="image"></div>
<div id="floating-panel" class="ui-widget-content">
  
<?php

session_start();		
ob_start();

echo '<div class="flex"><h1 class="title"> Welcome '.$_SESSION['fullname'].' </h1>';

 if(!isset($_SESSION['idu'])) {

		header("Location: login.php");		

}
		$count1=0;	
		$condition=0;
?>

<form action="login2.php" method="post">        
 <input type="submit" name="Exit" value="Sign out" class="sign-out">
</div>
 </form>
 <form name="myForm" action="login2.php" method="post" onsubmit="return validateForm()">
<div class="flex">
 <input type="text" id="name" name="name" placeholder="Name"class="input-texts">
 <input type="submit" id="Create" name="Create" value="Create Schedule" class="submit2">
</div>
 </form>

  <p id="demo" class="paragraph"></p>

<script type="text/javascript">

function validateForm() {
	var text;
    var x = document.forms["myForm"]["name"].value;
    if (x == null || x == "") {
		text="Name of Schedule must be filled out";
		document.getElementById("demo").innerHTML = text;
		return false;
    }
}

</script>

<?php

$servername = "localhost";
$username = "root";
$password = "";
$dbname = "base";

// Create connection
$conn = new mysqli($servername, $username, $password, $dbname);
// Check connection
if ($conn->connect_error) {
    die('<p class="paragraph">Connection failed: </p>'. $conn->connect_error);
} 
$idu=$_SESSION['idu'];
$sql = "SELECT * from schedules where idu=$idu";
$result = $conn->query($sql);

if ($result->num_rows > 0) {   
	//<tr><th>id</th><th>lat</th><th>lng</th></tr>
    // output data of each row
	echo '<table border="1" class="table">';
	echo '<tr><td style="font-family: Tahoma;"class="title" name="onoma" colspan="1"align="center">NAMES</td><td style="font-family: Tahoma;"class="title" name="onoma" colspan="5"align="center">ACTIONS</td></tr>';
	$count=0;
    $sum=0;
    while($row = $result->fetch_assoc()) {
		$count=$row['ids'];
		$string=$row['name'];
		$string = str_replace( array( '-','0','1','2','3','4','5','6','7','8','9' ), '', $string);
        echo '<tr>
		<td style="font-family: Tahoma;"class="title" name="onoma">'.$string.'</td>';

?>

<form action="login2.php" method="post">
<td>
<input type="submit" name="Continue<?php echo $count;?>" value="Continue" class="submit1"> 
</td>
<td>
<input type="submit" name="Points<?php echo $count;?>" value="Show Points" class="show" onClick="points()"> 
</td>
<td>
<input type="submit" name="Results<?php echo $count;?>" value="Results" class="show">
</td>
<?php if ($row['informed']=='0') { ?> 
<td><input type="submit" name="Run<?php echo $count;?>" value="Running" class="running"></td>
<?php } else { ?> 
<td><input type="submit" name="Run<?php echo $count;?>" value="Finished" class="show"></td>
<?php } ?> 
<td>
<input type="submit" name="Delete<?php echo $count;?>" value="Delete Schedule" class="delete-schedule">
</td>
</form>

<?php		

/////////////////////////////////////////////////////////////////	

if(isset($_POST['Results'.$count])){
		 		
	$_SESSION['ids']=$count;			 
	$count1=1;

}

if(isset($_POST['Delete'.$count])){
		
			include_once("loginbase.php");

	$sql = "DELETE FROM schedules WHERE ids='$count'";
	
if ($con->query($sql) === TRUE) {
    //echo '<p class="paragraph">Schedule deleted successfully</p>';
} else {
    echo '<p class="paragraph">Error deleting Schedule: </p>'. $con->error;
}
$sqlr = "DELETE FROM points WHERE ids='$count'";
	
if ($con->query($sqlr) === TRUE) {
    //echo '<p class="paragraph">Points deleted successfully</p>';
} else {
    echo '<p class="paragraph">Error deleting Points: </p>' . $con->error;
}

	header("Location: login2.php");	
	$con->close();
}

/////////////////////////////////////////////////////////	

if(isset($_POST['Run'.$count])){
		
		include_once("loginbase.php");
		
		$sqlm="Select name from schedules where ids='$count'";	
		if (!mysqli_set_charset($con, "utf8")) {
    exit();
} else {
    //printf("Current character set: %s\n", mysqli_character_set_name($con));
}		
	$query=mysqli_query($con,$sqlm);
	$row=mysqli_fetch_array($query);
	$name = $row['name'];

	$fp='c:\temp\"'.$name.'".csv';
  $fp = str_replace( array( '"', '' ), '', $fp);  //antikatastash tou xarakthra " me to keno

	if(!file_exists($fp)) {
	echo '<p class="paragraph">File not found</p>';
	//break;
} else{
	$cmd='c:\xampp\htdocs\googlemaps\TSPGenetic\TSPGenetic\bin\Debug\TSPGenetic "'.$fp.'" c:\temp\"'.$name.'".txt';
	$cmd = str_replace( array( '"', '' ), '', $cmd);  //antikatastash tou xarakthra " me to keno
$escaped_cmd = escapeshellcmd($cmd);
exec($escaped_cmd);
}

	    date_default_timezone_set('Europe/Athens');
		$date = date('Y-m-d H:i:s');  
			
		$sqle = "Update schedules set finish_date='$date',informed='1' where ids='$count'";	
			if ($con->query($sqle) === TRUE) {
				 header("Refresh: 1");
				//echo '<p class="paragraph">Running was succeeded</p>';
			} else {
				echo '<p class="paragraph">Error Running or Finished: </p>'. $con->error;
			}
			$con->close();
}
	
////////////////////////////////////////////////////////////////////////		

if(isset($_POST['Points'.$count]))
{
	    $ids=$count;
	    $condition=1;
}

///////////////////////////////////////////////////////////////////////////		

if(isset($_POST['Continue'.$count])){	
		
		include_once("loginbase.php");      

			$sqle = "Update schedules set finish_date='0000-00-00 00:00:00' , informed='0' where ids='$count'";	
			if ($con->query($sqle) === TRUE) {
				    //echo '<p class="paragraph">Update was successfully</p>';
					    $_SESSION['ids']=$count;
			        header("Location: insert.php");	
			} else {
				    echo '<p class="paragraph">Error Update: </p>' . $con->error;
			}
}

//////////////////////////////////////////////////////////////////////////

				echo '</tr>';
				$sum=$sum+1;
				continue;
    		}
    		echo '</table>';
			$conn->close();
	} else {
   			echo '<p class="paragraph">Results:0</p>';
	}

////////////////////////////////////////////////////////////////////////

	if(isset($_POST['Exit'])){
		
		include_once("loginbase.php");
		
		header("Location: logout.php");
	}

////////////////////////////////////////////////////////////////////////

if ($sum<3){
	if(isset($_POST['Create'])){
		
		include_once("loginbase.php");
		
	    $username=$_SESSION['username'];
		$sql="SELECT * FROM users WHERE username='$username'";
		if (!mysqli_set_charset($con, "utf8")) {
			exit();
		} 		
			$query=mysqli_query($con,$sql);
			$row=mysqli_fetch_array($query);
			$id = $row['idu'];

		$text=strip_tags($_POST['name']);

		$sqlj="SELECT * FROM schedules where idu='$id'";
		if (!mysqli_set_charset($con, "utf8")) {
			exit();
		} 		

	$result = $con->query($sqlj);
	$nameofschedule=0;//Δήλωση μεταβλητής ως flag
if ($result->num_rows > 0) {
    while($row = $result->fetch_assoc()) {
	  $db_text = $row['name'];
	  if("$id-$text" == $db_text)
		{
		    $nameofschedule=1;//Λειτουργία μεταβλητής ως flag			
		}
	  }
    }
        if($nameofschedule==0){
			date_default_timezone_set('Europe/Athens');
			$date = date('Y-m-d H:i:s');           

			$sqle = "insert into schedules values ('','$id','$id-$text','$date','','')";	
			if ($con->query($sqle) === TRUE) {
				echo '<p class="paragraph">Insert was successfully</p>';
			} else {
				echo '<p class="paragraph">Error Insert: </p>' . $con->error;
			}
			$sqlq="SELECT * FROM schedules WHERE idu='$id'";
			if (!mysqli_set_charset($con, "utf8")) {
			exit();
		} 
	$result = $con->query($sqlq);
	if ($result->num_rows > 0) {
		while($row1 = $result->fetch_assoc()) {	 
		$ids1 = $row1['ids'];
			}
		}
			$_SESSION['ids']=$ids1;
			header("Location: insert.php");
	}else{
	echo '<p class="paragraph">Give Another Name</p>';
	}
	}
}else
{
echo '<p class="paragraph">Max Schedules</p>';
}
?>

<!--////////////////////////////////////////////////////////////////////-->

</div>

<?php

	if ($count1=='1'){
		include_once("data.php");			 
    	include_once("loginbase.php");

     $count=$_SESSION['ids'];
   	 $sqlt = "SELECT * FROM points where ids='$count'";
     $result = $con->query($sqlt);

	 $sum=0;

		if ($result->num_rows > 0) {
   			 while($row = $result->fetch_assoc()) {
      		 $sum=$sum+1;
    		}
		} else {
    		echo '<p class="paragraph">Results:0</p>';
		}
		//echo $sum;
		if ($sum <= 10){
			$sum1=$sum*2+22;
		}else{
			$sum1=($sum-10)*3+42;
		}

			$sqlp = "SELECT * FROM schedules where ids='$count'";	
	
			$query1=mysqli_query($con,$sqlp);
			$row=mysqli_fetch_array($query1);
			$name = $row['name'];

			$file='/temp/"'.$name.'".txt';
			$file = str_replace( array( '"', '' ), '', $file);

			if(!file_exists($file)) {
				echo '<p class="paragraph">File not found</p>';
			} else{
				$section = '['.file_get_contents($file, false, null, (filesize ($file) -$sum1),$sum*2).']';
  			if ($section!=''){
	  			$section = str_replace( array( ' ' ), '', $section);	  
	 			 //echo '<p class=paragraph>'.$section.'</p>';
 			}else{
				echo '<p class="paragraph">Results:0</p>';
			}	
			}
?>


<div class="cordbox" class="ui-widget-content">
<?php 	
	
	$sqlf = "SELECT idp, lat, lng FROM points where ids='$count'";
	$result = $con->query($sqlf);

if ($result->num_rows > 0) {
     echo '<table align="center" border="1" class="table">';
			 echo '<tr class="paragraph1"><th>Id</th><th>Latitude</th><th>Longitude</th></tr>';
     //output data of each row
	 $i=1;
    	while($row1 = $result->fetch_assoc()) {
        	echo '<tr align="center" class="paragraph">
			<td>'.$section[$i].'</td>
			<td>'.$row1['lat'].'</td>
			<td>'.$row1['lng'].'</td>
			</tr>';
			$i=$i+2;
   			}
    echo '</table>';
	} else {
    	echo '<p class="paragraph">>>>>>Results:0<<<<<</p>';
	}   
	 $con->close();

   ?>
	</div>


 <script src="jquery-3.1.0.min.js"></script><!--library-jquery download from jquery.com-->
<script src="jquery-1.12.4.js"></script>  <!--Libraries-->
  <script src="jquery-ui.js"></script>  <!--Libraries-->

<script type="text/javascript">

$('#floating-panel').draggable({
      cursor: "move"
          });
 $('.cordbox').draggable({
      cursor: "move"
          });

      var labels = '0123456789';
      var labelIndex = 0;
      var markers = [];
      var ar1=[];
      
      var path = <?php echo $section;?>;
      var ar = <?php echo $json;?>;
     
      function initMap() {
         
        var map = new google.maps.Map(document.getElementById('map'), {
          zoom: 5,
          center: ar[0],
		  styles:[
  {
    "elementType": "geometry",
    "stylers": [
      {
        "color": "#ebe3cd"
      }
    ]
  },
  {
    "elementType": "labels.text.fill",
    "stylers": [
      {
        "color": "#523735"
      }
    ]
  },
  {
    "elementType": "labels.text.stroke",
    "stylers": [
      {
        "color": "#f5f1e6"
      }
    ]
  },
  {
    "featureType": "administrative",
    "elementType": "geometry.stroke",
    "stylers": [
      {
        "color": "#c9b2a6"
      }
    ]
  },
  {
    "featureType": "administrative.land_parcel",
    "elementType": "geometry.stroke",
    "stylers": [
      {
        "color": "#dcd2be"
      }
    ]
  },
  {
    "featureType": "administrative.land_parcel",
    "elementType": "labels.text.fill",
    "stylers": [
      {
        "color": "#ae9e90"
      }
    ]
  },
  {
    "featureType": "landscape.natural",
    "elementType": "geometry",
    "stylers": [
      {
        "color": "#dfd2ae"
      }
    ]
  },
  {
    "featureType": "poi",
    "elementType": "geometry",
    "stylers": [
      {
        "color": "#dfd2ae"
      }
    ]
  },
  {
    "featureType": "poi",
    "elementType": "labels.text.fill",
    "stylers": [
      {
        "color": "#93817c"
      }
    ]
  },
  {
    "featureType": "poi.park",
    "elementType": "geometry.fill",
    "stylers": [
      {
        "color": "#a5b076"
      }
    ]
  },
  {
    "featureType": "poi.park",
    "elementType": "labels.text.fill",
    "stylers": [
      {
        "color": "#447530"
      }
    ]
  },
  {
    "featureType": "road",
    "elementType": "geometry",
    "stylers": [
      {
        "color": "#f5f1e6"
      }
    ]
  },
  {
    "featureType": "road.arterial",
    "elementType": "geometry",
    "stylers": [
      {
        "color": "#fdfcf8"
      }
    ]
  },
  {
    "featureType": "road.highway",
    "elementType": "geometry",
    "stylers": [
      {
        "color": "#f8c967"
      }
    ]
  },
  {
    "featureType": "road.highway",
    "elementType": "geometry.stroke",
    "stylers": [
      {
        "color": "#e9bc62"
      }
    ]
  },
  {
    "featureType": "road.highway.controlled_access",
    "elementType": "geometry",
    "stylers": [
      {
        "color": "#e98d58"
      }
    ]
  },
  {
    "featureType": "road.highway.controlled_access",
    "elementType": "geometry.stroke",
    "stylers": [
      {
        "color": "#db8555"
      }
    ]
  },
  {
    "featureType": "road.local",
    "elementType": "labels.text.fill",
    "stylers": [
      {
        "color": "#806b63"
      }
    ]
  },
  {
    "featureType": "transit.line",
    "elementType": "geometry",
    "stylers": [
      {
        "color": "#dfd2ae"
      }
    ]
  },
  {
    "featureType": "transit.line",
    "elementType": "labels.text.fill",
    "stylers": [
      {
        "color": "#8f7d77"
      }
    ]
  },
  {
    "featureType": "transit.line",
    "elementType": "labels.text.stroke",
    "stylers": [
      {
        "color": "#ebe3cd"
      }
    ]
  },
  {
    "featureType": "transit.station",
    "elementType": "geometry",
    "stylers": [
      {
        "color": "#dfd2ae"
      }
    ]
  },
  {
    "featureType": "water",
    "elementType": "geometry.fill",
    "stylers": [
      {
        "color": "#b9d3c2"
      }
    ]
  },
  {
    "featureType": "water",
    "elementType": "labels.text.fill",
    "stylers": [
      {
        "color": "#92998d"
      }
    ]
  }
]
        });

        for (var i = 0; i < path.length; i++) {
           ar1[i]=ar[path[i]];
           addMarkerWithTimeout(ar1[i] , i * 200);
        }
      
        function addMarkerWithTimeout(position, timeout) {
        window.setTimeout(function() {

          var flightPath = new google.maps.Polyline({
          path: ar1,
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
      }
    </script>

	<?php }?>

<?php if ($condition=='1'){ ?>
<div class="cordbox" class="ui-widget-content">
<?php 	

include_once("loginbase.php");
	
	$sql = "SELECT idp, lat, lng FROM points where ids=$ids";
	$result = $con->query($sql);

if ($result->num_rows > 0) {
     echo '<table align="center" border="1" class="table">';
			 echo '<tr class="paragraph1"><th>Id</th><th>Latitude</th><th>Longitude</th></tr>';
     //output data of each row
    	while($row1 = $result->fetch_assoc()) {
        	echo '<tr align="center" class="paragraph">
			<td>'.$row1['idp'].'</td>
			<td>'.$row1['lat'].'</td>
			<td>'.$row1['lng'].'</td>
			</tr>';
   			}
    echo '</table>';
	} else {
    	echo '<p class="paragraph">>>>>>Results:0<<<<<</p>';
	}   
	 $con->close();
   ?>
   
	</div>
<?php }?>

  <script src="jquery-3.1.0.min.js"></script><!--library-jquery download from jquery.com-->
    <div id="map"></div>

<!--//////////////////////////////////////////////////////////////////-->

<script src="jquery-1.12.4.js"></script>  <!--Libraries-->
  <script src="jquery-ui.js"></script>  <!--Libraries-->

<?php if($count1!='1'){?>
<script type="text/javascript">
 
 $('#floating-panel').draggable({
      cursor: "move"
          });
 $('.cordbox').draggable({
      cursor: "move"
          });

  </script>

    <script>
      var map;
      function initMap() {
        map = new google.maps.Map(document.getElementById('map'), {
          center: {lat: 37.9788, lng: 23.7245},
          zoom: 8,
		  styles:[
  {
    "elementType": "geometry",
    "stylers": [
      {
        "color": "#ebe3cd"
      }
    ]
  },
  {
    "elementType": "labels.text.fill",
    "stylers": [
      {
        "color": "#523735"
      }
    ]
  },
  {
    "elementType": "labels.text.stroke",
    "stylers": [
      {
        "color": "#f5f1e6"
      }
    ]
  },
  {
    "featureType": "administrative",
    "elementType": "geometry.stroke",
    "stylers": [
      {
        "color": "#c9b2a6"
      }
    ]
  },
  {
    "featureType": "administrative.land_parcel",
    "elementType": "geometry.stroke",
    "stylers": [
      {
        "color": "#dcd2be"
      }
    ]
  },
  {
    "featureType": "administrative.land_parcel",
    "elementType": "labels.text.fill",
    "stylers": [
      {
        "color": "#ae9e90"
      }
    ]
  },
  {
    "featureType": "landscape.natural",
    "elementType": "geometry",
    "stylers": [
      {
        "color": "#dfd2ae"
      }
    ]
  },
  {
    "featureType": "poi",
    "elementType": "geometry",
    "stylers": [
      {
        "color": "#dfd2ae"
      }
    ]
  },
  {
    "featureType": "poi",
    "elementType": "labels.text.fill",
    "stylers": [
      {
        "color": "#93817c"
      }
    ]
  },
  {
    "featureType": "poi.park",
    "elementType": "geometry.fill",
    "stylers": [
      {
        "color": "#a5b076"
      }
    ]
  },
  {
    "featureType": "poi.park",
    "elementType": "labels.text.fill",
    "stylers": [
      {
        "color": "#447530"
      }
    ]
  },
  {
    "featureType": "road",
    "elementType": "geometry",
    "stylers": [
      {
        "color": "#f5f1e6"
      }
    ]
  },
  {
    "featureType": "road.arterial",
    "elementType": "geometry",
    "stylers": [
      {
        "color": "#fdfcf8"
      }
    ]
  },
  {
    "featureType": "road.highway",
    "elementType": "geometry",
    "stylers": [
      {
        "color": "#f8c967"
      }
    ]
  },
  {
    "featureType": "road.highway",
    "elementType": "geometry.stroke",
    "stylers": [
      {
        "color": "#e9bc62"
      }
    ]
  },
  {
    "featureType": "road.highway.controlled_access",
    "elementType": "geometry",
    "stylers": [
      {
        "color": "#e98d58"
      }
    ]
  },
  {
    "featureType": "road.highway.controlled_access",
    "elementType": "geometry.stroke",
    "stylers": [
      {
        "color": "#db8555"
      }
    ]
  },
  {
    "featureType": "road.local",
    "elementType": "labels.text.fill",
    "stylers": [
      {
        "color": "#806b63"
      }
    ]
  },
  {
    "featureType": "transit.line",
    "elementType": "geometry",
    "stylers": [
      {
        "color": "#dfd2ae"
      }
    ]
  },
  {
    "featureType": "transit.line",
    "elementType": "labels.text.fill",
    "stylers": [
      {
        "color": "#8f7d77"
      }
    ]
  },
  {
    "featureType": "transit.line",
    "elementType": "labels.text.stroke",
    "stylers": [
      {
        "color": "#ebe3cd"
      }
    ]
  },
  {
    "featureType": "transit.station",
    "elementType": "geometry",
    "stylers": [
      {
        "color": "#dfd2ae"
      }
    ]
  },
  {
    "featureType": "water",
    "elementType": "geometry.fill",
    "stylers": [
      {
        "color": "#b9d3c2"
      }
    ]
  },
  {
    "featureType": "water",
    "elementType": "labels.text.fill",
    "stylers": [
      {
        "color": "#92998d"
      }
    ]
  }
]
        });
      }
    </script>
<?php } ?>

<script src="https://maps.googleapis.com/maps/api/js?key=AIzaSyBMsPoC6zm0r1sVWPluknWxdp0KNL9MwNE&callback=initMap">
</script>
</body>
</html>