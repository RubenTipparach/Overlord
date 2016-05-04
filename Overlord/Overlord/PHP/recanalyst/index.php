<?
    /*
    * This file was designed to test the capabilites of the RecAnalyst Library.
    * A library for scanning and parsing .mgx data from Age Of Empires 2.
    * Could be useful for AI initial game state modeling.
    */


    spl_autoload_register(function ($class) {
        if (substr($class, 0, 11) === 'RecAnalyst\\') {
            $f = '' . '' . str_replace('RecAnalyst\\', '', $class) . '.php';
            if (file_exists($f)) include($f);
			echo $f;
        }
    });

	$filename = "recorded game -  09-Jul-2011 22`55`35.mgx";
    // map generator
    $ra = new RecAnalyst\RecAnalyst();
    $ra->load($filename, fopen($filename, 'r'));
    $ra->analyze();
    // generateMap returns a GD image resource
    $gd = $ra->generateMap();

    // header('Content-Type: image/png');
    // imagepng($gd);
    // imagedestroy($gd);

    // echo chat messages
    foreach ($ra->pregameChat as $chat)
    {
      echo '<' . $chat->player->name . '> ' . $chat->msg . "\n";
    }

    foreach ($ra->ingameChat as $chat)
    {
      echo '<br>'.'[' . RecAnalyst\RecAnalyst::gameTimeToString($chat->time) . '] ' . $chat->msg . "\n";
    }

	$gameInfo = $ra->gameInfo;

	echo '<br>'.$gameInfo->getPlayersString();
	echo '<br>'.$gameInfo->getPlayTime();
	echo '<br>'.$gameInfo->getObjectives();
?>

<?
	print("<br>");
    print("<h1>");
	print("This section is for my Sql profiler/sql gui sutff.");
	print("</h1>");

    // $mysqli = new mysqli("localhost:3306", "ruben", "ruben", "aoenn");
	$mysqli = new mysqli("localhost:3306", "ruben", "ruben", "aoenn");
    echo(mysqli_get_host_info($mysqli));

	// do a basic read from db.
	$ai_def_query = "SELECT Name, Author FROM ai_definition";
	if ($result = $mysqli->query($ai_def_query, MYSQLI_USE_RESULT))
	{
        // PHP docs says to use fetch_row instead... whatever blah.
		while ($row = mysqli_fetch_array($result, MYSQL_ASSOC))
		{
			printf("<br><b>Name:</b> %s, <b>Author:</b> %s", $row["Name"], $row["Author"]);
		}

		$result->close();
	}

	$mysqli->close();
?>