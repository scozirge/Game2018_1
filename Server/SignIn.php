<?PHP
$time_start = microtime(true);
//導入取得json資料功能
//require_once('./LoadJsonData.php');
//導入config
require_once('./config.php');
//導入加密類
require_once('./3DES.php');
$ac = $_POST['AC'];
$pw = $_POST['PW'];
$requestTime = $_POST['RequestTime'];
//將帳號md5
//$md5AC=substr(md5($ac),2,15);
//將密碼md5
$md5PW=md5($pw);
//連線至DB
$con_l = mysql_connect($db_host_write,$db_user,$db_pass) or ("Fail:2:"  . mysql_error());
if (!$con_l)
	die('Fail:2:' . mysql_error());
mysql_select_db($db_name , $con_l) or die ("Fail:3:" . mysql_error());
$result = mysql_query("SELECT * FROM game2018_1.playeraccount WHERE `Account`='".$ac."'",$con_l);
$dataNum = mysql_num_rows($result );
if ($dataNum == 0)
{
	die ("Fail:4:");
}
else
{
	while($row = mysql_fetch_assoc($result ))
	{
		//帳密正確，登入成功返回玩家資料
		if ($md5PW == $row['Password'])
		{
			//帳戶
			$AC=$ac;
			//通關碼
			$rep = new Crypt3Des (); // new一個加密類
			$ACPass=$rep->encrypt ( "u.6vu4".$ac."gk4ru4");
			$PW=$pw;
			//金錢
			$Score=$row['Score'];
			//登入時間
			date_default_timezone_set('Asia/Taipei');
			$LastSignIn= date("Y/m/d H:i:s");
			//$LastLogin_DB=$row['LastLogin'];
			//取登入時間md5最後5碼做為登入驗證碼
			//$LoginCode=substr(md5($LastSignIn),-5);
            //新增寫入DB連線
            $con_w = mysql_connect($db_host_write,$db_user,$db_pass,true) or ("Fail:2:"  . mysql_error());
            if (!$con_w)
                die('Fail:2:' . mysql_error());
            mysql_select_db($db_name , $db_host_local) or die ("Fail:3:" . mysql_error());
			$set = mysql_query("UPDATE `playeraccount` SET `LastLogin` = '".$LastSignIn."' WHERE `Account` = '".$ac."' ",$con_w);
			//更新資料的回傳結果
            if($set)
			{
				//計算執行時間
				$time_end = microtime(true);
				$executeTime = $time_end - $time_start;
				die("Success:".$AC.",".$ACPass. ",".$PW. ",".$Score. ": \nExecuteTime=".$executeTime);
			}
			else
			{
				die("Fail:12");
			}
		}
		else
			die("Fail:5");
	}
}
?>