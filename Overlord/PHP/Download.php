
<head/>
<body>
<?


	$conn = new mysqli("localhost:3306", "root", "", "aoenn");

	// do some selects on the tables
	print("<br><h3>The newest generated AI :</h3>");
	$getAiNameSql = "SELECT idAI_List, AI, DATE_FORMAT(revDate, '%d/%m/%Y') as revDate FROM ai_list order by idAI_List DESC LIMIT 1;";
	$result = mysqli_query($conn, $getAiNameSql);

	print("<table style='width:500px' border=1 padding=5px>");
	print("<tr>");
	while($row = mysqli_fetch_assoc($result))
	{
		//%u number, %s string
		print("<td><b>AI Revision : </b>".$row["idAI_List"]. "</td><td><b>AI Name  </b>".$row["AI"]."</td><td><b>Revision Date  </b>".$row["revDate"]."</td>");
	}
	print("</tr>");

?>
</body>