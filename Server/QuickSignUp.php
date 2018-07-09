<?PHP
$time_start = microtime(true);
//導入config
require_once('./config.php');
//導入加密類
require_once('./3DES.php');
//$requestTime = $_POST['RequestTime'];
//取得建立帳戶時間，格式為年份/月/日 時:分:秒(台北時區)
date_default_timezone_set('Asia/Taipei');
$signUpTime= date("Y/m/d H:i:s");
$ac=substr(md5($signUpTime),3,11);
$con_l = mysql_connect($db_host_load,$db_user,$db_pass) or ("Fail:1:"  . mysql_error());
if (!$con_l)
	die('Fail:1:' . mysql_error());
mysql_select_db($db_name , $con_l) or die ("Fail:6:" . mysql_error());
//$check = mysql_query("SELECT * FROM slotappdb.playeraccount WHERE `Account`='".$md5AC."'",$con_l);
//$numrows = mysql_num_rows($check);
$numrows=0;
if ($numrows == 0)
{
    $score=0;
    $kills=0;
	$shot=0;
	$criticalHit=0;
	$death=0;
	$criticalCombo=0;	
	
    //$loginCode=substr(md5($lastLogin),-5);
    //新增寫入DB連線
    $con_w = mysql_connect($db_host_write,$db_user,$db_pass,true) or ("Fail:1:"  . mysql_error());
    if (!$con_w)
        die('Fail:1:' . mysql_error());
    $signUpResult = mysql_query("INSERT INTO  Game2018_1.playeraccount (   `account` ,`score`,`kills`,`shot`,`criticalHit`,`death`,`criticalCombo`,`SignInTime`,`SignUpTime`) VALUES ( '".$ac."' , '".$score."' , '".$kills."' , '".$shot."' , '".$criticalHit."' , '".$death."' , '".$criticalCombo."','".$signUpTime."','".$signUpTime."') ; ",$con_w);
	if ($signUpResult)
	{
       //對帳號進行加密
        $rep = new Crypt3Des (); // new一個加密類
        $ACPass=$rep->encrypt ( "u.6vu4".$ac."gk4ru4");
        //計算執行時間
        $time_end = microtime(true);
        $executeTime = $time_end - $time_start;
        die("Success:".$ac.",".$ACPass.",".$score.",".$kills.",".$shot.",".$criticalHit.",".$death.",".$criticalCombo.": \nExecuteTime=".$executeTime);
	}
	else
	{
		$time_end = microtime(true);
		$executeTime = $time_end - $time_start;
		die ("Fail:8: \nExecuteTime=".$executeTime."");
	}
}
else
{
	die("Fail:9");
}
?>