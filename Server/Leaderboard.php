<?PHP
//開始時間
$time_start = microtime(true);
//導入config
require_once('./config.php');
//導入3DES解密類
require_once('./3DES.php');
/////////////////////////////////////////////////Post資料///////////////////////////////////////
$account=$_POST['AC'];
$acPass = $_POST['ACPass'];
//取得建立帳戶時間，格式為年份/月/日 時:分:秒(台北時區)
date_default_timezone_set('Asia/Taipei');
$nowTime= date("Y/m/d H:i:s");

/////////////////////////////////////////////////////////////////資料驗證////////////////////////////////////////////////////////////////////////////
//解出通關碼
$c3des = new Crypt3Des ();
$plaintext=$c3des->decrypt ( $acPass );
$head=substr($plaintext,0,6);
$tail=substr($plaintext,-6);
//判斷通關碼是否合法，不合法則返回讀取排行榜失敗
if($head != "u.6vu4" || $tail != "gk4ru4")
{
	//計算執行時間
	$time_end = microtime(true);
	$executeTime = $time_end - $time_start;
    $executeTime=number_format($executeTime,4);
	die("Fail:1001: \nExecuteTime=".$executeTime."");
}
/////////////////////////////////////////////////////////設定資料/////////////////////////////////////////////////////////
//上次排行榜更新時間
//$diffTime=strtotime($nowTime)-strtotime($lastUpdateTime);
$diffTime=100;
//如果距離上次更新資料庫上的排行已經超過1分鐘，那就從新更新資料庫排行榜
if($diffTime>60)
{
    //取的玩家金錢排行榜
	//////////////////////////////////////////////////////////////取得玩家資料//////////////////////////////////////////////////////////////
	$con_l = mysql_connect($db_host_load,$db_user,$db_pass) or ("'Fail:1:"  . mysql_error());
	if (!$con_l)
		die('Fail:1:' . mysql_error());
	mysql_select_db($db_name , $con_l) or die ('Fail:1:' . mysql_error());
    $getTopRank = mysql_query("SELECT `name`,`score` FROM `playeraccount` ORDER by `score` DESC LIMIT 50",$con_l);
    $currentRankStr='';
    $dataCount=0;
    //依照取得的陣列設定排行榜文字
    while ($row = mysql_fetch_row($getTopRank))
    {
        if($dataCount!=0)
            $currentRankStr.='/';
        $currentRankStr.=$row[0].'$'.$row[1];
        $dataCount++;
    }
    $con_w = mysql_connect($db_host_write,$db_user,$db_pass,true) or ("'Fail:1:"  . mysql_error());
    if (!$con_w)
        die('Fail:20:' . mysql_error());
    mysql_select_db($db_name , $con_w) or die ('Fail:1:' . mysql_error());
    //將這次抓取排行榜的時間寫到玩家資料中
	//$updateRequest = mysql_query("UPDATE `PlayerAccount` SET  `RequestTimes` = '".$requestTimes."' WHERE `Account` = '".$account."' ",$con_w);
    //計算執行時間
    $time_end = microtime(true);
    $executeTime = $time_end - $time_start;
    //送回排行榜字串給Client
    die ("Success:".$currentRankStr.": \nExecuteTime=".$executeTime."");
}
else
{
    //計算執行時間
    $time_end = microtime(true);
    $executeTime = $time_end - $time_start;
    //送回排行榜字串給Client
    die ("Success:".$currentRankStr.": \nExecuteTime=".$executeTime."");
}
?>