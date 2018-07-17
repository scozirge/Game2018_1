<?PHP
$time_start = microtime(true);
//導入取得json資料功能
//require_once('./LoadJsonData.php');
//導入config
require_once('./config.php');
//導入加密類
require_once('./3DES.php');


$ac = $_POST['AC'];
$acPass=$_POST['ACPass'];
$NewName=$_POST['NewName'];



//連線至DB
$con_l = mysql_connect($db_host_write,$db_user,$db_pass) or ("Fail:2:"  . mysql_error());
if (!$con_l)
	die('Fail:2:' . mysql_error());
mysql_select_db($db_name , $con_l) or die ("Fail:3:" . mysql_error());
$nameCheck = mysql_query("SELECT * FROM ".$db_name.".playeraccount WHERE `name`='".$NewName."'",$con_l);
$nameNum = mysql_num_rows($nameCheck);
if($nameNum!=0)//玩家名稱重複
{
	die ("Fail:5: \nExecuteTime=".$executeTime."");
}
$result = mysql_query("SELECT * FROM ".$db_name.".playeraccount WHERE `account`='".$ac."'",$con_l);
$dataNum = mysql_num_rows($result );

if ($dataNum == 0)
{
	//取得建立帳戶時間，格式為年份/月/日 時:分:秒(台北時區)
	date_default_timezone_set('Asia/Taipei');
	$signUpTime= date("Y/m/d H:i:s");
	$ac=substr(md5($signUpTime),3,11);
	$con_l = mysql_connect($db_host_load,$db_user,$db_pass) or ("Fail:1:"  . mysql_error());
	//使用本機資料
	$score=$_POST['BestScore'];
	$kills=$_POST['Kills'];
	$shot=$_POST['Shot'];
	$criticalHit=$_POST['CriticalHit'];
	$death=$_POST['Death'];
	$criticalCombo=$_POST['CriticalCombo'];		
	//新增寫入DB連線
	$con_w = mysql_connect($db_host_write,$db_user,$db_pass,true) or ("Fail:1:"  . mysql_error());
	if (!$con_w)
		die('Fail:1:' . mysql_error());
	$signUpResult = mysql_query("INSERT INTO  ".$db_name.".playeraccount (   `account` ,`name` ,`score`,`kills`,`shot`,`criticalHit`,`death`,`criticalCombo`,`SignInTime`,`SignUpTime`) VALUES ( '".$ac."' , '".$NewName."', '".$score."' , '".$kills."' , '".$shot."' , '".$criticalHit."' , '".$death."' , '".$criticalCombo."','".$signUpTime."','".$signUpTime."') ; ",$con_w);
	if ($signUpResult)
	{
	   //對帳號進行加密
		$rep = new Crypt3Des (); // new一個加密類
		$ACPass=$rep->encrypt ( "u.6vu4".$ac."gk4ru4");
		//計算執行時間
		$time_end = microtime(true);
		$executeTime = $time_end - $time_start;
		//使用server資料
		//die("Success:".$ac.",".$ACPass.",".$name.",".$score.",".$kills.",".$shot.",".$criticalHit.",".$death.",".$criticalCombo.": \nExecuteTime=".$executeTime);
		//使用本機資料 
		die("Success1:".$ac.",".$ACPass.",".$NewName.": \nExecuteTime=".$executeTime);
	}
	else
	{
		$time_end = microtime(true);
		$executeTime = $time_end - $time_start;
		die ("Fail:12: \nExecuteTime=".$executeTime."");
	}	
}
else
{
	/////////////////////////////////////////////////////////////////資料驗證////////////////////////////////////////////////////////////////////////////
	//解出通關碼
	$c3des = new Crypt3Des ();
	$plaintext=$c3des->decrypt ( $acPass );
	$head=substr($plaintext,0,6);
	$tail=substr($plaintext,-6);
	//判斷通關碼是否合法，不合法則返回通關碼失敗
	if($head != "u.6vu4" || $tail != "gk4ru4")
	{
		//計算執行時間
		$time_end = microtime(true);
		$executeTime = $time_end - $time_start;
		$executeTime=number_format($executeTime,4);
		die("Fail:1001: \nExecuteTime=".$executeTime."");
	}
	while($row = mysql_fetch_assoc($result ))
	{
		//登入時間
		date_default_timezone_set('Asia/Taipei');
		$LastSignIn= date("Y/m/d H:i:s");
		//新增寫入DB連線
		$con_w = mysql_connect($db_host_write,$db_user,$db_pass,true) or ("Fail:2:"  . mysql_error());
		if (!$con_w)
			die('Fail:2:' . mysql_error());
		mysql_select_db($db_name , $con_w) or die ("Fail:3:" . mysql_error());
		//使用本基資料
		$set = mysql_query("UPDATE `playeraccount` SET `name` = '".$NewName."' WHERE `Account` = '".$ac."' ",$con_w);
		//更新資料的回傳結果
		if($set)
		{
			//計算執行時間
			$time_end = microtime(true);
			$executeTime = $time_end - $time_start;
			//使用本機資料 
			die("Success2:".$NewName.": \nExecuteTime=".$executeTime);
		}
		else
		{
			die("Fail:12");
		}
	}

}
?>