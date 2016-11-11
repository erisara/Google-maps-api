-- phpMyAdmin SQL Dump
-- version 4.5.1
-- http://www.phpmyadmin.net
--
-- Host: 127.0.0.1
-- Generation Time: Nov 11, 2016 at 11:13 AM
-- Server version: 10.1.13-MariaDB
-- PHP Version: 7.0.8

SET SQL_MODE = "NO_AUTO_VALUE_ON_ZERO";
SET time_zone = "+00:00";


/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8mb4 */;

--
-- Database: `base`
--

-- --------------------------------------------------------

--
-- Table structure for table `points`
--

CREATE TABLE `points` (
  `idp` int(100) NOT NULL,
  `ids` int(100) NOT NULL,
  `lat` float NOT NULL,
  `lng` float NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

--
-- Dumping data for table `points`
--

INSERT INTO `points` (`idp`, `ids`, `lat`, `lng`) VALUES
(60, 5, 38.5052, 22.5824),
(61, 5, 38.5825, 23.7744),
(63, 5, 38.4407, 21.3354),
(64, 5, 39.7114, 21.6101),
(67, 5, 37.9875, 23.7195),
(68, 5, 37.9269, 22.9449),
(70, 5, 38.0178, 22.1155),
(79, 8, 38.5825, 22.3352),
(80, 8, 37.5925, 22.0825),
(85, 8, 37.0815, 22.4451),
(86, 8, 37.9659, 23.7524),
(87, 8, 38.3115, 23.0383),
(91, 4, 37.9788, 23.725),
(92, 4, 41.8491, 12.832),
(93, 4, 55.7858, 37.4414),
(94, 4, 40.2041, -3.86719),
(95, 4, 37.6208, -122.3),
(96, 4, 52.4928, 13.7988),
(97, 9, 37.9788, 23.7415),
(98, 9, 41.8297, 12.5244),
(99, 9, 41.6596, 44.8462),
(100, 9, 40.6536, -74.0479),
(101, 9, 40.4198, -3.73535),
(102, 9, 37.7251, -122.476),
(103, 9, 50.1259, 8.74512),
(104, 9, 48.8123, 2.28516),
(105, 9, 51.4976, -0.153809),
(106, 10, 37.9702, 23.7415),
(107, 10, 41.7451, 12.5684),
(108, 10, 50.0444, 14.436),
(109, 10, 48.7953, 2.28515),
(110, 10, 37.656, -122.52),
(111, 10, 38.8602, -77.0361),
(112, 10, 52.4988, 13.4253),
(115, 10, 40.3817, -3.66943);

-- --------------------------------------------------------

--
-- Table structure for table `schedules`
--

CREATE TABLE `schedules` (
  `ids` int(100) NOT NULL,
  `idu` int(100) NOT NULL,
  `name` varchar(100) NOT NULL,
  `start_date` datetime NOT NULL,
  `finish_date` datetime NOT NULL,
  `informed` tinyint(1) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

--
-- Dumping data for table `schedules`
--

INSERT INTO `schedules` (`ids`, `idu`, `name`, `start_date`, `finish_date`, `informed`) VALUES
(4, 3, '3-Ios', '2016-09-25 13:46:23', '2016-10-06 22:12:14', 1),
(5, 3, '3-Trikala', '2016-09-25 16:27:01', '2016-10-28 23:59:29', 1),
(8, 4, '4-Athina', '2016-10-01 11:25:10', '2016-10-03 22:53:58', 1),
(9, 4, '4-Trip', '2016-10-08 20:03:58', '2016-11-01 15:21:26', 1),
(10, 3, '3-Trip', '2016-11-01 12:55:54', '0000-00-00 00:00:00', 0);

-- --------------------------------------------------------

--
-- Table structure for table `users`
--

CREATE TABLE `users` (
  `idu` int(100) NOT NULL,
  `username` varchar(100) NOT NULL,
  `password` varchar(100) NOT NULL,
  `fullname` varchar(100) CHARACTER SET utf8 COLLATE utf8_unicode_ci NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

--
-- Dumping data for table `users`
--

INSERT INTO `users` (`idu`, `username`, `password`, `fullname`) VALUES
(1, 'admin', '123', 'Jim Papachristodoulos'),
(2, 'admin', '123', 'Jim Papachristodoulos'),
(3, 'user1', '1', 'Maria Papa'),
(4, 'user2', '2', 'Xristos Papadopoulos');

--
-- Indexes for dumped tables
--

--
-- Indexes for table `points`
--
ALTER TABLE `points`
  ADD PRIMARY KEY (`idp`);

--
-- Indexes for table `schedules`
--
ALTER TABLE `schedules`
  ADD PRIMARY KEY (`ids`);

--
-- Indexes for table `users`
--
ALTER TABLE `users`
  ADD PRIMARY KEY (`idu`);

--
-- AUTO_INCREMENT for dumped tables
--

--
-- AUTO_INCREMENT for table `points`
--
ALTER TABLE `points`
  MODIFY `idp` int(100) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=116;
--
-- AUTO_INCREMENT for table `schedules`
--
ALTER TABLE `schedules`
  MODIFY `ids` int(100) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=11;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
