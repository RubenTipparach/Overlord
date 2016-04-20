<script>
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
</script>

<?
	$conn = new mysqli("localhost:4040", "root", "", "aoenn");

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

			<th></th>
		</tr>");
	
	/*
		<th>p2FoodScore</th>
		<th>p2GoldScore</th>
		<th>p2StoneScore</th>
		<th>p2BuildersScore</th>
	*/

	while($row = mysqli_fetch_assoc($result))
	{	
		print("<form action='SubmitGameData.php' method='post'>");
		print("<tr>");
			print("<td>".$row["PlayerId"]."</td>");
			print("<td>".$row["GameId"]."</td>");

			print("<td><input type='number' name='FoodScore'></td>");
			print("<td><input type='number' name='GoldScore'></td>");
			print("<td><input type='number' name='StoneScore'></td>");
			print("<td><input type='number' name='BuildersScore'></td>");

			print("<td><input type='submit' value='Submit'></td>");
		print("</tr>");
		print("</form>");
	}
	
	print("</table>");

	// generate a proper form

	mysqli_close($conn);
?>