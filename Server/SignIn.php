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
$requestTime = $_POST['RequestTime'];
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
			//帳戶
			$AC=$ac;
			//通關碼
			$rep = new Crypt3Des (); // new一個加密類
			$ACPass=$rep->encrypt ( "u.6vu4".$ac."gk4ru4");
			//取資料
			$Score=$row['score'];
			$kills=$row['kills'];
			$shot=$row['shot'];
			$criticalHit=$row['criticalHit'];
			$death=$row['death'];
			$criticalCombo=$row['criticalCombo'];	
			//登入時間
			date_default_timezone_set('Asia/Taipei');
			$LastSignIn= date("Y/m/d H:i:s");
            //新增寫入DB連線
            $con_w = mysql_connect($db_host_write,$db_user,$db_pass,true) or ("Fail:2:"  . mysql_error());
            if (!$con_w)
                die('Fail:2:' . mysql_error());
            mysql_select_db($db_name , $con_w) or die ("Fail:3:" . mysql_error());
			$set = mysql_query("UPDATE `playeraccount` SET `SignInTime` = '".$LastSignIn."' WHERE `Account` = '".$ac."' ",$con_w);
			//更新資料的回傳結果
            if($set)
			{
				//計算執行時間
				$time_end = microtime(true);
				$executeTime = $time_end - $time_start;
				die("Success:". $Score.",".$kills.",".$shot.",".$criticalHit.",".$death.",".$criticalCombo. ": \nExecuteTime=".$executeTime);
			}
			else
			{
				die("Fail:12");
			}
	}
}
?>