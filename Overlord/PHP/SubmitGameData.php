<?
	$conn = new mysqli("localhost:3306", "root", "", "aoenn");

	// Check connection
if ($conn->connect_error) {
    die("Connection failed, unable to access backend data : " . $conn->connect_error);
} 
?>
<html>
<body>
FoodScore : <?php echo $_POST["FoodScore"]; ?><br>
WoodScore : <?php echo $_POST["WoodScore"]; ?><br>
GoldScore : <?php echo $_POST["GoldScore"]; ?><br>
StoneScore : <?php echo $_POST["StoneScore"]; ?><br>
BuildersScore : <?php echo $_POST["BuildersScore"]; ?><br>
</body>
</html>

