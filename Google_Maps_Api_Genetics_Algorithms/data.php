	  <link rel="stylesheet" href="style-map.css" type="text/css"/>
<?php

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

$ids = $_SESSION['ids'];

	$sql = "SELECT lat, lng FROM points where ids=$ids";
	if (!mysqli_set_charset($conn, "utf8")) {
    //printf("Error loading character set utf8: %s\n", mysqli_error($con));
    exit();
} else {
    //printf("Current character set: %s\n", mysqli_character_set_name($con));
}		
	$result = $conn->query($sql);
	$status = 'false';
if ($result->num_rows > 0) {
$data=array();
 $status = 'true';
    while($row = $result->fetch_assoc()) {
	  $data[] = '{lat: '. $row['lat'].', lng: '.$row['lng'].'}';
	}
}

if ( $status == 'true' ){

$json = json_encode( $data );
$json = str_replace( array( '"' ), ' ', $json);

}

$conn->close();

?>