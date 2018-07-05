<?PHP
//現在時間()
date_default_timezone_set('Asia/Taipei');
$nowTime=date("Y/m/d H:i:s");
$time_start = microtime(true);
//導入config
require_once('./config.php');
//導入需求經驗取得功能
require_once('./LoadJsonData.php');
//導入3DES解密類
require_once('./3DES.php');
//導入旋轉結果計算器
require_once('./SpinCalculator.php');
//導入成就檢查函式
require_once('./AchieveGetter.php');
/////////////////////////////////////////////////////////////////Post資料///////////////////////////////////////////////////////////////////////////
$account=$_POST['AC'];
$acPass = $_POST['ACPass'];
$loginCode=$_POST['LoginCode'];
//取得Client命令次數RequestTimes
$requestTimes = $_POST['RequestTimes'];
//取得Client命令RequestTime，格式為2015-11-25 15:39:36
$requestTime = $_POST['RequestTime'];
//每線押注金額
$bet=$_POST['Bet'];
//押注線數
$line=$_POST['Line'];
//連勝次數
$keepWinTimes=$_POST['KeepWinTimes'];
//////////////////////////////////////////////////////////////取得玩家資料//////////////////////////////////////////////////////////////
$con_l = mysql_connect($db_host_load,$db_user,$db_pass) or ("'Fail:1006:"  . mysql_error());
if (!$con_l)
	die('Fail:1006:' . mysql_error());
mysql_select_db($db_name , $con_l) or die ('Fail:1007:' . mysql_error());
//取得玩家資料
$getPlayerData= mysql_query("SELECT `Money`,`SpinResult`,`RemainFreeTimes`,`BetTimes`,`RequestTimes`,`RequestTime`,`TotalBet`,`TotalWin`,`RTP`,`Level`,`EXP`,`Achieve`,`LoginCode` FROM `playeraccount` WHERE `Account`='".$account."' limit 1",$con_l);
$columnArray=mysql_fetch_row($getPlayerData);
$curMoney=$columnArray[0];//目前的金錢
$lastResult=$columnArray[1];//上一次旋轉結果盤面
$freeTimes=$columnArray[2];//剩餘的免費遊戲次數
$betTimes=$columnArray[3];//下注次數
$db_requestTimes=$columnArray[4];//完成命令次數
$db_requestTime=$columnArray[5];//命令送出時間
$totalBet=$columnArray[6];//全部下注
$totalWin=$columnArray[7];//全部贏錢
$rtp=$columnArray[8];//RTP
$level=$columnArray[9];//等級
$exp=$columnArray[10];//經驗
$achieveStr=$columnArray[11];//成就字串
$dbLoginCode=$columnArray[12];//登入碼
//取得jackpot資料
$jackpotType='Money';
$getJackpotData = mysql_query("SELECT `Value` FROM `Jackpot` WHERE `Type`='".$jackpotType."' limit 1",$con_l);
$columnArray_jackpot=mysql_fetch_row($getJackpotData);
$curJackpot=$columnArray_jackpot[0];//取得目前累積的金額
/////////////////////////////////////////////////////////////////資料驗證////////////////////////////////////////////////////////////////////////////

//解出通關碼
$c3des = new Crypt3Des ();
$plaintext=$c3des->decrypt ( $acPass );
$head=substr($plaintext,0,4);
$tail=substr($plaintext,-3);
//判斷通關碼是否合法，不合法則返回旋轉失敗
if($head != "tj;4" || $tail != "m,4")
{
	//計算執行時間
	$time_end = microtime(true);
	$executeTime = $time_end - $time_start;
    $executeTime=number_format($executeTime,4);
	die("Fail:1001: \nExecuteTime=".$executeTime."");
}
//比對玩家傳入登入碼是否跟資料庫上登入碼一致，不一致代表帳號已經被重其他裝置登入
if($dbLoginCode!=$loginCode)
{
	//計算執行時間
	$time_end = microtime(true);
	$executeTime = $time_end - $time_start;
    $executeTime=number_format($executeTime,4);
	die("Fail:1008:\nExecuteTime=".$executeTime."");
}
//比對判斷旋轉碼的時間，若是client時間較舊，代表是逾時命令(client送資料到server逾時，等client重送資料後，server又突然才接收到的過期命令)，不執行
if(strtotime($requestTime)<=strtotime($db_requestTime))
{
	//計算執行時間
	$time_end = microtime(true);
	$executeTime = $time_end - $time_start;
    $executeTime=number_format($executeTime,4);
	die("DoNothing:20001: \nExecuteTime=".$executeTime."");
}
//比對Client的命令次數與DB目前命令次數，若是Client次數少於DB目前次數代表DB已經完成命令，不重複執行此命令
if($requestTimes<$db_requestTimes)
{
	//計算執行時間
	$time_end = microtime(true);
	$executeTime = $time_end - $time_start;
    $executeTime=number_format($executeTime,4);
	//送回DB目前資料，讓Client更新
	die("Update:".$lastResult.":".$freeTimes.":".$db_requestTimes.": \nExecuteTime=".$executeTime."");
}
if($curMoney<$spendMoney)
{
	//計算執行時間
	$time_end = microtime(true);
	$executeTime = $time_end - $time_start;
    $executeTime=number_format($executeTime,4);
	die("Fail:1005: \nExecuteTime=".$executeTime."");
}
//////////////////////////////////////////////////////////////////計算玩家資料//////////////////////////////////////////////////////////////////////
//產生一個成就類
$ahieve = new AchieveGetter();
//如果成就字串不是空字串那就解析出成就陣列
if(!empty($achieveStr))
	$achieveArray=AchieveGetter::AchieveStrToArray($achieveStr);
else
	$achieveArray=array();
$ahieve->AchieveIDs=$achieveArray;
//花費金額
$spendMoney=$bet*$line;
//旋轉結果字串
$resultStr="";
$Calc=new SpinCalculator(); //建立計算機
//取得亂數旋轉結果
$result= $Calc -> GetRandomResult();
//判斷有無取得特殊圖形連線成就
for($curLine=0;$curLine<$line;$curLine++)
{
	//如果3個圖像都一樣
	if($result[$WinLinePos[$curLine][0][0]][$WinLinePos[$curLine][0][1]+1]==$result[$WinLinePos[$curLine][1][0]][$WinLinePos[$curLine][1][1]+1] &&
		$result[$WinLinePos[$curLine][0][0]][$WinLinePos[$curLine][0][1]+1]==$result[$WinLinePos[$curLine][2][0]][$WinLinePos[$curLine][2][1]+1])
	{
		switch ($result[$WinLinePos[$curLine][0][0]][$WinLinePos[$curLine][0][1]+1]) {
			case 0://大7彩金成就判斷
				$ahieve->CheckGetAchieve("Big7King",0);
				break;
			case 1://小7彩金成就判斷
				$ahieve->CheckGetAchieve("Small7King",0);
				break;
			case 5://鈴鐺彩金成就判斷
				$ahieve->CheckGetAchieve("BellKing",0);
				break;
			case 6://西瓜彩金成就判斷
				$ahieve->CheckGetAchieve("WatermelonKing",0);
				break;
			case 7://茄子彩金成就判斷
				$ahieve->CheckGetAchieve("EggplantKing",0);
				break;
            case 8://橘子彩金成就判斷
				$ahieve->CheckGetAchieve("OrangeKing",0);
				break;
            case 9://櫻桃彩金成就判斷
				$ahieve->CheckGetAchieve("CherryKing",0);
				break;
		}
	}
}
//判斷有無達成水果王成就(全部圖像都為水果)
$fruitKing=true;
for($i=0;$i<count($result);$i++)
{
	for($j=0;$j<count($result[$i]);$j++)
	{
		//0跟4的圖形不再3x3的顯示區域內
		if($j>0 && $j<4)
		{
			//如果有一個圖形不是水果就沒達成水果王
			if($result[$i][$j]!=6 && $result[$i][$j]!=7 && $result[$i][$j]!=8 && $result[$i][$j]!=9)
			{
				$fruitKing=false;
				$i=count($result)-1;
				$j=count($result[$i])-1;
			}
		}
	}
}
if($fruitKing==true)
{
	$ahieve->CheckGetAchieve("FruitKing",0);
}
$freeTimes=$Calc -> GetFreeGameTimes();//額外免費遊戲次數
//取得贏多少錢
$winResult=$Calc -> GetWinMoney($line,$bet,$result);
$winMoney=$winResult["WinMoney"];
$winLines=$winResult["WinLines"];
$jackpot=$winResult['Jackpot'];
//如果有贏得jackpot就計算jackpot取得金額
$winJackpot=0;
if($jackpot>0)
{
    $curJackpot=$curJackpot+($spendMoney*0.02);//累積彩金
    $winJackpot=$curJackpot*$jackpot;//計算贏得彩金
    $winJackpot=(int)$winJackpot;//轉整數
    $curJackpot=$curJackpot-$winJackpot;//剩餘的彩金
}
else
{
    $curJackpot=$curJackpot+($spendMoney*0.02);//累積彩金
}
$curJackpot=(int)$curJackpot;//轉整數
if($winMoney>0)//如果贏分大於0，檢查成就有沒有達成
{
	$keepWinTimes++;
	$ahieve->CheckGetAchieve("WinMoney",$winMoney);//檢查是否有取得贏分成就
	$ahieve->CheckGetAchieve("KeepWin",$keepWinTimes);//檢查是否有取得連勝成就
	$ahieve->CheckGetAchieve("WinLine",$winLines);//檢查是否有取得贏線成就
}
/////////////////////////檢查是否有取得下注次數成就
$betTimes++;
$ahieve->CheckGetAchieve("BetTimes",$betTimes);
///////////////////總下注加入這次下注的金額
$totalBet+=$spendMoney;
/////////////////總贏金額加入這次贏得金額
$totalWin+=$winMoney;
$ahieve->CheckGetAchieve("TotalWin",$totalWin);//檢查是否有取得總贏分成就
///////////////完成命令次數+1
$requestTimes++;
/////////////////////////////經驗加入這次金額，如果符合升級需求經驗則升級
$lvUpReward=0;
$exp+=$winMoney;
$requireExp=GetRequireExp($level);
if($requireExp=="Error")
	die("Fail:1009");
else
{
	if($exp>=$requireExp)
	{
		//取得升級獎勵
		$reward=GetReward($level);
		$lvUpReward+=(int)$reward["Money"];
		$freeTimes+=(int)$reward["FreeGame"];
		$exp=0;
		$level++;
	}
}
///////////////////////////////////////////////檢查成就ID是否有新的，有新的就加入贏得清單列表
$achieveReward=$ahieve->AchieveTotalReward;//新取得成就的獎勵
foreach($ahieve->AchieveIDs as $content)
{
	if(!(in_array($content,$achieveArray)))
	{
		//die("".$content);
		array_push($achieveArray,$content);
	}
}
if(!empty($achieveArray))
	$newAchieveStr=AchieveGetter::ArrayToAchieveStr($achieveArray);
else
	$newAchieveStr='';
/////////////////////////////////////////////////////計算最後玩家金錢
$finalPlayerMoney= $curMoney+$winMoney-$spendMoney+$lvUpReward+$achieveReward+$winJackpot;
//$checkStr="finalPlayerMoney=".$finalPlayerMoney."curMoney=".$curMoney."winMoney=".$winMoney."spendMoney=".$spendMoney."lvUpReward=".$lvUpReward."achieveReward=".$achieveReward."winLines=".$winLines."jackpot=".$winJackpot;
/////////////////////////////////////////////////////設定RTP
if($totalBet>0)
	$rtp=(double)$totalWin/(double)$totalBet;
////////////////////////////////////////////////////////////////////設定最後的結果到資料庫/////////////////////////////////////////////////////////////
//新增寫入DB連線
$con_w = mysql_connect($db_host_write,$db_user,$db_pass,true) or ("'Fail:1006:"  . mysql_error());
if (!$con_w)
	die('Fail:1006:' . mysql_error());
mysql_select_db($db_name , $con_w) or die ('Fail:1007:' . mysql_error());
$result = mysql_query("UPDATE `playeraccount` SET `Money` = '".$finalPlayerMoney."', `SpinResult` = '".$resultStr."', `RemainFreeTimes` = '".$freeTimes."', `BetTimes` = '".$betTimes."', `RequestTimes` = '".$requestTimes."', `RequestTime` = '".$requestTime."', `TotalBet` = '".$totalBet."', `TotalWin` = '".$totalWin."', `RTP` = '".$rtp."', `Level` = '".$level."', `EXP` = '".$exp."', `Achieve` = '".$newAchieveStr."' WHERE `Account` = '".$account."' ",$con_w);
if($jackpot>0)
    $result_jackpot = mysql_query("UPDATE `Jackpot` SET `Value` = '".$curJackpot."', `LastUpdateTime` = '".$nowTime."', `LastClaimTime` = '".$nowTime."' WHERE `Type` = '".$jackpotType."' ",$con_w);
else
{
    $result_jackpot = mysql_query("UPDATE `Jackpot` SET `Value` = '".$curJackpot."', `LastUpdateTime` = '".$nowTime."' WHERE `Type` = '".$jackpotType."' ");
}

//如果更新累積彩池到資料庫失敗
if(!$result_jackpot)
    die("Fail:20004");
//設定結果
if($result)
{
	//計算執行時間
	$time_end = microtime(true);
	$executeTime = $time_end - $time_start;
    $executeTime=number_format($executeTime,4);
    die("Success:".$resultStr.":".$freeTimes.":".$requestTimes.":".$winMoney.":".$betTimes.":".$totalWin.":".$curJackpot.":".$winJackpot.":".$executeTime.": \n".$checkStr);
}
else
{
	die("Fail:1004");
}
?>