<script>
/*
function loadDoc() {
  var xhttp = new XMLHttpRequest();
  xhttp.onreadystatechange = function() {
    if (xhttp.readyState == 4 && xhttp.status == 200) {
     document.getElementById("gameEntryForm").innerHTML = xhttp.responseText;
    }
  };
  xhttp.open("POST", "SubmitGameData.php", true);
  xhttp.send();
}


function loadDoc() {
  var xhttp = new XMLHttpRequest();
  xhttp.onreadystatechange = function() {
    if (xhttp.readyState == 4 && xhttp.status == 200) {
     document.getElementById("demo").innerHTML = xhttp.responseText;
    }
  };
  xhttp.open("POST", "SubmitGameData.php", true);
  xhttp.send();
}
*/

</script>

<body>
<?
	$conn = new mysqli("localhost:3306", "root", "", "aoenn");

	// do some selects on the tables
	print("<br><h3>List of availible AIs in database.</h3>");
	$getAiNameSql = "SELECT Name, Author FROM ai_definition;";
	$result = mysqli_query($conn, $getAiNameSql);

	while($row = mysqli_fetch_assoc($result))
	{
		//%u number, %s string
		print("<br><b>Name </b>".$row["Name"]. ",<b>Author </b>".$row["Author"]);
	}

	print("<br><h3>List of games played.</h3>");
	$getAnnFeedSql = "SELECT * FROM ai_neural_network_feed;";
	$result = mysqli_query($conn, $getAnnFeedSql);
	print("<table style='width:500px' border=1>");

	print("
		<tr>
			<th>Game</th>
			<th>p2Food</th> 
			<th>p2Gold</th>
			<th>p2Stone</th>
			<th>p2Builders</th>
		</tr>");

	while($row = mysqli_fetch_assoc($result))
	{
		print("<tr>");
			print("<td>".$row["GameId"]."</td>");
			print("<td>".$row["p2Food"]."</td>");
			print("<td>".$row["p2Gold"]."</td>");
			print("<td>".$row["p2Stone"]."</td>");
			print("<td>".$row["p2Builders"]."</td>");
		print("</tr>");
	}

	print("</table>");
	// Load the script for the games.
	print("<br><h3>List of games that require data.</h3>");


	// then we'll do some more selects to detect incomplete data,
	$getAITableInputSql = "
	SELECT A.AIIndex AS PlayerId, A.GameId, 
		CASE WHEN G.IsReady IS NULL 
		-- When this is 1 or exists as 0, it shouldn't appear in this table
			THEN 0
			ELSE 1
		END AS IsReady
	FROM ai_economy_feudal_input A
		LEFT JOIN ai_economy_feudal_output_raw B
			ON A.AIIndex = B.AIIndex
			AND A.GameId = B.GameId
		LEFT JOIN ai_game_table G
			ON A.GameId = G.GameId
	WHERE G.IsReady IS NULL 
		OR G.IsReady = 0
	ORDER BY A.GameId, A.AIIndex ASC
	LIMIT 2;
	";

	$result = mysqli_query($conn, $getAITableInputSql);
	
	print("<table style='width:250px' border=1>");

	print("
		<tr>
			<th>PlayerId</th>
			<th>GameId</th>

			<th>FoodScore</th>
			<th>GoldScore</th>
			<th>StoneScore</th>
			<th>BuildersScore</th>


		</tr>");

	print("<form action='index.php' method='post'>");
	$i = 0;

	while($row = mysqli_fetch_assoc($result))
	{	
		print("<tr>");
			print("<td>".$row["PlayerId"]."</td>");
			print("<td>".$row["GameId"]."</td>");
			$_POST['PlayerId'.$i] = $row["PlayerId"];
			$_POST['GameId'.$i] = $row["GameId"];

			print("<td><input type='number' name='FoodScore".$i."'></td>");
			print("<td><input type='number' name='GoldScore".$i."'></td>");
			print("<td><input type='number' name='StoneScore".$i."'></td>");
			print("<td><input type='number' name='BuildersScore".$i."'></td>");

		print("</tr>");
		$i++;
	}

	print("<br><br><input type='submit' value='Submit'>");
	print("</form>");
	print("</table>");

	if (count($_POST) > 4)
	{
		// Submit some data
		for($i = 0; $i < 2; $i++)
		{
			$player = $_POST['PlayerId'.$i];
			// Submit to ai_economy_feudal_output_raw 
			print("<br>Game ".$_POST['GameId'.$i]." score wast posted! For p".$_POST['PlayerId'.$i]);
			print("<br>Food p".$player." score wast posted! " . $_POST['FoodScore'.$i]);
			print("<br>Gold p".$player." score wast posted! " . $_POST['GoldScore'.$i]);
			print("<br>Stone p".$player." score wast posted! " . $_POST['StoneScore'.$i]);
			print("<br>Builder p".$player." score wast posted! " . $_POST['BuildersScore'.$i]);
			
			$updateAiOutputSql = "
			UPDATE ai_economy_feudal_output_raw
			SET Food = ".$_POST['FoodScore'.$i]."
				AND Wood = ".$_POST['GoldScore'.$i]."
				AND Stone = ".$_POST['StoneScore'.$i]."
				AND Gold = ".$_POST['BuildersScore'.$i]."
			WHERE AiIndex = ".$_POST['PlayerId'.$i]."
				AND	GameId = ".$_POST['GameId'.$i].";
			";
			print('<br>'.$updateAiOutputSql);

			// Update ai_game_table via insert new game column to isReady = 0
			$insertAiGameSql = "
			INSERT INTO ai_game_table
			VALUES( ".$_POST['GameId'.$i]. ", null, now());
			";
			print('<br>'.$insertAiGameSql);
		}
	}

	mysqli_close($conn);
?>

<!--<div id='gameEntryForm'></div>-->

<!--
<form action="InputTableView.php" method="post">
  Food Score:<br>
  <input type="text" name="FoodScore" value="Mouse">
  <br><br>
  <input type="submit" value="Submit">
</form> 
-->


</body>