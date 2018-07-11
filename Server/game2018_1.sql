-- phpMyAdmin SQL Dump
-- version 4.8.2
-- https://www.phpmyadmin.net/
--
-- 主機: localhost
-- 產生時間： 2018-07-09 17:47:43
-- 伺服器版本: 10.1.10-MariaDB
-- PHP 版本： 5.6.19

SET SQL_MODE = "NO_AUTO_VALUE_ON_ZERO";
SET AUTOCOMMIT = 0;
START TRANSACTION;
SET time_zone = "+00:00";


/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8mb4 */;

--
-- 資料庫： `game2018_1`
--

-- --------------------------------------------------------

--
-- 資料表結構 `playeraccount`
--

CREATE TABLE `playeraccount` (
  `id` bigint(20) NOT NULL,
  `account` varchar(20) NOT NULL,
  `score` int(10) NOT NULL,
  `kills` int(20) NOT NULL,
  `shot` int(20) NOT NULL,
  `criticalHit` int(20) NOT NULL,
  `death` int(20) NOT NULL,
  `criticalCombo` int(20) NOT NULL,
  `SignInTime` datetime NOT NULL,
  `SignUpTime` datetime NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

--
-- 資料表的匯出資料 `playeraccount`
--

INSERT INTO `playeraccount` (`id`, `account`, `score`, `kills`, `shot`, `criticalHit`, `death`, `criticalCombo`, `SignInTime`, `SignUpTime`) VALUES
(2, 'fe74fcecf5b', 0, 0, 0, 0, 0, 0, '0000-00-00 00:00:00', '2018-07-09 09:23:39'),
(3, '339c55b6697', 0, 0, 0, 0, 0, 0, '0000-00-00 00:00:00', '2018-07-09 10:34:40'),
(4, '45a2eaf7bc9', 0, 0, 0, 0, 0, 0, '2018-07-09 11:52:59', '2018-07-09 11:52:59'),
(5, '9fa046dfdab', 0, 0, 0, 0, 0, 0, '2018-07-09 12:12:42', '2018-07-09 12:12:42'),
(6, '34dd9cefdb7', 0, 0, 0, 0, 0, 0, '2018-07-09 14:21:34', '2018-07-09 12:14:33'),
(7, 'e50add727cb', 82, 18, 27, 22, 15, 17, '2018-07-09 17:13:36', '2018-07-09 14:43:15');

--
-- 已匯出資料表的索引
--

--
-- 資料表索引 `playeraccount`
--
ALTER TABLE `playeraccount`
  ADD PRIMARY KEY (`id`),
  ADD KEY `account` (`account`);

--
-- 在匯出的資料表使用 AUTO_INCREMENT
--

--
-- 使用資料表 AUTO_INCREMENT `playeraccount`
--
ALTER TABLE `playeraccount`
  MODIFY `id` bigint(20) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=8;
COMMIT;

/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
