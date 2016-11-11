	<link rel="stylesheet" href="style-map.css" type="text/css"/>
<?php

session_start();

if(isset($_POST['excel'])){
	
	include_once("loginbase.php");

//////////////////////////////////////////////////////////////////////////	
	$id=$_SESSION['ids'];
 	$sqlq="SELECT * FROM schedules WHERE ids='$id'";
		if (!mysqli_set_charset($con, "utf8")) {
    exit();
} else {
    //printf("Current character set: %s\n", mysqli_character_set_name($con));
}		
	$result = $con->query($sqlq);
	if ($result->num_rows > 0) {
 		while($row1 = $result->fetch_assoc()) {
			$informed = $row1['informed'];	 
			$ids = $row1['ids'];
			$name=$row1['name'];
	}
}
/////////////////////////////////////////////////////////////////////////

$sql = "SELECT idp, lat, lng FROM points where ids=$ids";
$result = $con->query($sql);

if ($result->num_rows > 0) {
    // create a file pointer connected to the output stream
$out='C:\temp\"'.$name.'".csv';
$out = str_replace( array( '"', '' ), '', $out); //αντικατάσταση του χαρακτήρα " με το κενό
$output = fopen($out, 'w');

    while($row = $result->fetch_assoc()) {
// output data of each row
$cmd='"'.$row['idp'].'";"'.$row['lat'].'";"'.$row['lng'].'"';
$cmd = str_replace( array( '"', '' ), '', $cmd);  //αντικατάσταση του χαρακτήρα " με το κενό
fputcsv($output,array($cmd)); //κάνει εγγραφή μια μια τις συντεταγμένες στο αρχείο csv.    	
    }
} else {
    echo '<p class="paragraph">0 results</p>';
}
$con->close();
	header("Location: login2.php");
}

?>