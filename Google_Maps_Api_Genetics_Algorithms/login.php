<?php
    
	session_start();
	$flag=0;

	if(isset($_POST['login'])){
		
	include_once("loginbase.php");
		
	$username=strip_tags($_POST['username']);//Delete tags example:<br></br>
	$password=strip_tags($_POST['password']);
	
	//$username=stripslashes($username);
	//$password=stripslashes($password);
	//$username=mysqli_real_escape_string($dbcon, $username);
	//$password=mysqli_real_escape_string($dbcon, $password);
	//$password=md5($password);
	
	$sql="SELECT * FROM users WHERE username='$username' ";
		if (!mysqli_set_charset($con, "utf8")) {
    //printf("Error loading character set utf8: %s\n", mysqli_error($con));
    exit();
} else {
    //printf("Current character set: %s\n", mysqli_character_set_name($con));
}		
	$query=mysqli_query($con,$sql);
	$row=mysqli_fetch_array($query);
	$id = $row['idu'];
	$db_password=$row['password'];
	$user=$row['username'];
	$fullname=$row['fullname'];
	
/////////////////////////////////////////////////////////////////////////////////

	if($db_password == $password and $username == $user and $username !=''){
				$_SESSION['username']=$username;
				$_SESSION['idu']=$id;
				$_SESSION['fullname']=$fullname;
				header("Location: login2.php");
	}else{
		$flag=1;		
	}
	}
	
?>

<!DOCTYPE html>
<html>
  <head>
    <title>Sign in</title>
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
        top: 20%;
        right: 40%;
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
        top:10%;
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
	<h1 class="title">Sign in</h1>
    <form action="login.php" method="post" enctype="multipart/form-data">
	<input placeholder="Username" class="input-texts" name="username" type="text" autofocus>
	<input placeholder="Password" class="input-texts" name="password" type="password">
	<?php if($flag==1){ ?>
	<p class="paragraph">Wrong... Try again</p>
	<?php } ?>
	<input name="login" type="submit" class="submit" value="Sign in">
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

  </script>

    <script>
      var map;
      function initMap() {
        map = new google.maps.Map(document.getElementById('map'), {
          center: {lat: 37.9788, lng: 23.7245},
          zoom: 8,
          styles: [
  {
    "elementType": "geometry",
    "stylers": [
      {
        "color": "#242f3e"
      }
    ]
  },
  {
    "elementType": "labels.text.fill",
    "stylers": [
      {
        "color": "#746855"
      }
    ]
  },
  {
    "elementType": "labels.text.stroke",
    "stylers": [
      {
        "color": "#242f3e"
      }
    ]
  },
  {
    "featureType": "administrative.locality",
    "elementType": "labels.text.fill",
    "stylers": [
      {
        "color": "#d59563"
      }
    ]
  },
  {
    "featureType": "poi",
    "elementType": "labels.text.fill",
    "stylers": [
      {
        "color": "#d59563"
      }
    ]
  },
  {
    "featureType": "poi.park",
    "elementType": "geometry",
    "stylers": [
      {
        "color": "#263c3f"
      }
    ]
  },
  {
    "featureType": "poi.park",
    "elementType": "labels.text.fill",
    "stylers": [
      {
        "color": "#6b9a76"
      }
    ]
  },
  {
    "featureType": "road",
    "elementType": "geometry",
    "stylers": [
      {
        "color": "#38414e"
      }
    ]
  },
  {
    "featureType": "road",
    "elementType": "geometry.stroke",
    "stylers": [
      {
        "color": "#212a37"
      }
    ]
  },
  {
    "featureType": "road",
    "elementType": "labels.text.fill",
    "stylers": [
      {
        "color": "#9ca5b3"
      }
    ]
  },
  {
    "featureType": "road.highway",
    "elementType": "geometry",
    "stylers": [
      {
        "color": "#746855"
      }
    ]
  },
  {
    "featureType": "road.highway",
    "elementType": "geometry.stroke",
    "stylers": [
      {
        "color": "#1f2835"
      }
    ]
  },
  {
    "featureType": "road.highway",
    "elementType": "labels.text.fill",
    "stylers": [
      {
        "color": "#f3d19c"
      }
    ]
  },
  {
    "featureType": "transit",
    "elementType": "geometry",
    "stylers": [
      {
        "color": "#2f3948"
      }
    ]
  },
  {
    "featureType": "transit.station",
    "elementType": "labels.text.fill",
    "stylers": [
      {
        "color": "#d59563"
      }
    ]
  },
  {
    "featureType": "water",
    "elementType": "geometry",
    "stylers": [
      {
        "color": "#17263c"
      }
    ]
  },
  {
    "featureType": "water",
    "elementType": "labels.text.fill",
    "stylers": [
      {
        "color": "#515c6d"
      }
    ]
  },
  {
    "featureType": "water",
    "elementType": "labels.text.stroke",
    "stylers": [
      {
        "color": "#17263c"
      }
    ]
  }
]
        });
      }
    </script>

    <script src="https://maps.googleapis.com/maps/api/js?key=AIzaSyBMsPoC6zm0r1sVWPluknWxdp0KNL9MwNE&callback=initMap">
    </script>
</body>
</html>	