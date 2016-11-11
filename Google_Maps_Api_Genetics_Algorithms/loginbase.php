	<link rel="stylesheet" href="style-map.css" type="text/css"/>
<?php

$servername = "localhost";
$username = "root";
$password = "";
$db="base";

$con = mysqli_connect($servername,$username,$password ,$db);

// Check connection
if (mysqli_connect_errno())
  {
  echo '<p class="paragraph">Failed to connect to MySQL: </p>'. mysqli_connect_error();
  }
//echo "Connected successfully";
?>