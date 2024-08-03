/*
 Navicat Premium Data Transfer

 Source Server         : localhost_3306
 Source Server Type    : MySQL
 Source Server Version : 100432
 Source Host           : localhost:3306
 Source Schema         : account_cq

 Target Server Type    : MySQL
 Target Server Version : 100432
 File Encoding         : 65001

 Date: 23/07/2024 15:41:05
*/

SET NAMES utf8mb4;
SET FOREIGN_KEY_CHECKS = 0;

-- ----------------------------
-- Table structure for city_location
-- ----------------------------
DROP TABLE IF EXISTS `city_location`;
CREATE TABLE `city_location`  (
  `id` int NOT NULL AUTO_INCREMENT,
  `ip_from` bigint NULL DEFAULT NULL,
  `ip_to` bigint NULL DEFAULT NULL,
  `country_code` varchar(255) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL,
  `country_name` varchar(255) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL,
  `state` varchar(255) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL,
  `city` varchar(255) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL,
  `latitude` double(255, 9) NULL DEFAULT NULL,
  `longitude` double(255, 9) NULL DEFAULT NULL,
  `zip_code` varchar(255) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL,
  PRIMARY KEY (`id`) USING BTREE
) ENGINE = InnoDB AUTO_INCREMENT = 1 CHARACTER SET = utf8 COLLATE = utf8_general_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Records of city_location
-- ----------------------------

-- ----------------------------
-- Table structure for conquer_account
-- ----------------------------
DROP TABLE IF EXISTS `conquer_account`;
CREATE TABLE `conquer_account`  (
  `Id` int NOT NULL,
  `UserName` varchar(255) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL,
  `Password` varchar(255) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL,
  `Salt` varchar(255) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL,
  `AuthorityId` int NULL DEFAULT 1,
  `Flag` int NULL DEFAULT 0,
  `IpAddress` varchar(255) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL,
  `MacAddress` varchar(255) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL,
  `ParentId` varchar(255) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL,
  `Created` datetime(0) NOT NULL DEFAULT current_timestamp(0),
  `Modified` datetime(0) NULL DEFAULT NULL ON UPDATE CURRENT_TIMESTAMP(0),
  `Deleted` datetime(0) NULL DEFAULT NULL,
  PRIMARY KEY (`Id`) USING BTREE
) ENGINE = InnoDB CHARACTER SET = utf8 COLLATE = utf8_general_ci ROW_FORMAT = DYNAMIC;

-- ----------------------------
-- Records of conquer_account
-- ----------------------------
INSERT INTO `conquer_account` VALUES (0, '1', '1', '1', 1, 0, NULL, NULL, '5a28920a-5d82-453a-b951-27c4879a5d06', '2024-04-03 00:00:00', '2024-06-22 10:48:14', NULL);
INSERT INTO `conquer_account` VALUES (1, '2', '2', '1', 1, 0, NULL, NULL, '5a28920a-5d82-453a-b951-27c4879a5d07', '2024-06-22 10:52:05', '2024-06-22 10:52:12', NULL);
INSERT INTO `conquer_account` VALUES (2, '3', '3', '1', 1, 0, NULL, NULL, '5a28920a-5d82-453a-b951-27c4879a5d08', '2024-06-23 19:01:46', '2024-06-23 19:01:56', NULL);
INSERT INTO `conquer_account` VALUES (3, '4', '4', '1', 1, 0, NULL, NULL, '5a28920a-5d82-453a-b951-27c4879a5d09', '2024-06-25 13:25:50', '2024-06-25 13:26:02', NULL);
INSERT INTO `conquer_account` VALUES (4, '5', '5', '1', 1, 0, NULL, NULL, '5a28920a-5d82-453a-b951-27c4879a5d10', '2024-06-25 16:11:38', '2024-06-25 16:11:43', NULL);
INSERT INTO `conquer_account` VALUES (5, '6', '6', '1', 1, 0, NULL, NULL, '5a28920a-5d82-453a-b951-27c4879a5d11', '2024-06-25 17:46:46', '2024-06-25 17:46:54', NULL);
INSERT INTO `conquer_account` VALUES (6, '7', '7', '1', 1, 0, NULL, NULL, '5a28920a-5d82-453a-b951-27c4879a5d12', '2024-06-25 18:15:21', '2024-06-25 18:15:27', NULL);
INSERT INTO `conquer_account` VALUES (7, '8', '8', '1', 1, 0, NULL, NULL, '5a28920a-5d82-453a-b951-27c4879a5d13', '2024-07-18 17:15:44', '2024-07-18 17:16:02', NULL);
INSERT INTO `conquer_account` VALUES (8, '9', '9', '1', 1, 0, NULL, NULL, '5a28920a-5d82-453a-b951-27c4879a5d14', '2024-07-19 01:31:45', '2024-07-19 01:31:51', NULL);
INSERT INTO `conquer_account` VALUES (9, '123', '123', '1', 1, 0, NULL, NULL, '5a28920a-5d82-453a-b951-27c4879a5d15', '2024-07-19 01:36:16', '2024-07-19 01:38:30', NULL);
INSERT INTO `conquer_account` VALUES (10, '10', '10', '1', 1, 0, NULL, NULL, '5a28920a-5d82-453a-b951-27c4879a5d16', '2024-07-19 11:32:28', '2024-07-19 11:32:34', NULL);
INSERT INTO `conquer_account` VALUES (1000001, '11', '11', '1', 1, 0, NULL, NULL, '5a28920a-5d82-453a-b951-27c4879a5d17', '2024-07-19 13:41:10', '2024-07-19 13:41:21', NULL);
INSERT INTO `conquer_account` VALUES (1000002, '12', '12', '1', 1, 0, NULL, NULL, '5a28920a-5d82-453a-b951-27c4879a5d18', '2024-07-19 14:06:56', '2024-07-19 14:07:03', NULL);
INSERT INTO `conquer_account` VALUES (1000003, '13', '13', '1', 1, 0, NULL, NULL, '5a28920a-5d82-453a-b951-27c4879a5d19', '2024-07-20 23:46:12', '2024-07-20 23:46:16', NULL);

-- ----------------------------
-- Table structure for conquer_account_authority
-- ----------------------------
DROP TABLE IF EXISTS `conquer_account_authority`;
CREATE TABLE `conquer_account_authority`  (
  `Id` int NOT NULL,
  `Name` varchar(255) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL,
  `NormalizedName` varchar(255) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL,
  PRIMARY KEY (`Id`) USING BTREE
) ENGINE = InnoDB CHARACTER SET = utf8 COLLATE = utf8_general_ci ROW_FORMAT = DYNAMIC;

-- ----------------------------
-- Records of conquer_account_authority
-- ----------------------------

-- ----------------------------
-- Table structure for conquer_account_login_record
-- ----------------------------
DROP TABLE IF EXISTS `conquer_account_login_record`;
CREATE TABLE `conquer_account_login_record`  (
  `Id` int NOT NULL AUTO_INCREMENT,
  `AccountId` int NULL DEFAULT NULL,
  `IpAddress` varchar(255) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL,
  `LocationId` int NULL DEFAULT NULL,
  `LoginTime` datetime(0) NULL DEFAULT NULL,
  `Success` tinyint(1) NULL DEFAULT NULL,
  PRIMARY KEY (`Id`) USING BTREE
) ENGINE = InnoDB AUTO_INCREMENT = 424 CHARACTER SET = utf8 COLLATE = utf8_general_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Records of conquer_account_login_record
-- ----------------------------
INSERT INTO `conquer_account_login_record` VALUES (1, 0, '25.47.231.41', 0, '2024-06-22 10:49:10', 1);
INSERT INTO `conquer_account_login_record` VALUES (2, 1, '25.47.231.41', 0, '2024-06-22 10:52:25', 1);
INSERT INTO `conquer_account_login_record` VALUES (3, 1, '25.47.231.41', 0, '2024-06-22 10:58:23', 1);
INSERT INTO `conquer_account_login_record` VALUES (4, 1, '25.47.231.41', 0, '2024-06-22 11:09:16', 1);
INSERT INTO `conquer_account_login_record` VALUES (5, 1, '25.47.231.41', 0, '2024-06-22 11:13:45', 1);
INSERT INTO `conquer_account_login_record` VALUES (6, 1, '25.47.231.41', 0, '2024-06-22 11:14:02', 1);
INSERT INTO `conquer_account_login_record` VALUES (7, 0, '25.47.231.41', 0, '2024-06-22 11:15:10', 1);
INSERT INTO `conquer_account_login_record` VALUES (8, 0, '25.47.231.41', 0, '2024-06-22 11:18:38', 1);
INSERT INTO `conquer_account_login_record` VALUES (9, 1, '25.47.231.41', 0, '2024-06-22 11:24:17', 1);
INSERT INTO `conquer_account_login_record` VALUES (10, 1, '25.47.231.41', 0, '2024-06-22 11:29:06', 1);
INSERT INTO `conquer_account_login_record` VALUES (11, 1, '25.47.231.41', 0, '2024-06-22 11:29:21', 1);
INSERT INTO `conquer_account_login_record` VALUES (12, 1, '25.47.231.41', 0, '2024-06-22 11:32:37', 1);
INSERT INTO `conquer_account_login_record` VALUES (13, 0, '25.47.231.41', 0, '2024-06-22 11:34:47', 1);
INSERT INTO `conquer_account_login_record` VALUES (14, 1, '25.47.231.41', 0, '2024-06-22 11:43:54', 1);
INSERT INTO `conquer_account_login_record` VALUES (15, 1, '25.47.231.41', 0, '2024-06-22 11:44:17', 1);
INSERT INTO `conquer_account_login_record` VALUES (16, 0, '25.47.231.41', 0, '2024-06-22 11:44:41', 1);
INSERT INTO `conquer_account_login_record` VALUES (17, 0, '25.47.231.41', 0, '2024-06-22 11:48:40', 1);
INSERT INTO `conquer_account_login_record` VALUES (18, 1, '25.47.231.41', 0, '2024-06-22 11:48:58', 1);
INSERT INTO `conquer_account_login_record` VALUES (19, 1, '25.47.231.41', 0, '2024-06-22 11:49:24', 1);
INSERT INTO `conquer_account_login_record` VALUES (20, 1, '25.47.231.41', 0, '2024-06-22 12:03:12', 1);
INSERT INTO `conquer_account_login_record` VALUES (21, 1, '25.47.231.41', 0, '2024-06-22 12:06:00', 1);
INSERT INTO `conquer_account_login_record` VALUES (22, 1, '25.47.231.41', 0, '2024-06-22 12:15:26', 1);
INSERT INTO `conquer_account_login_record` VALUES (23, 0, '25.47.231.41', 0, '2024-06-22 12:15:57', 1);
INSERT INTO `conquer_account_login_record` VALUES (24, 1, '25.47.231.41', 0, '2024-06-22 12:16:25', 1);
INSERT INTO `conquer_account_login_record` VALUES (25, 1, '25.47.231.41', 0, '2024-06-22 12:39:04', 1);
INSERT INTO `conquer_account_login_record` VALUES (26, 2, '25.47.231.41', 0, '2024-06-23 19:15:27', 1);
INSERT INTO `conquer_account_login_record` VALUES (27, 2, '25.47.231.41', 0, '2024-06-23 19:16:26', 1);
INSERT INTO `conquer_account_login_record` VALUES (28, 2, '25.47.231.41', 0, '2024-06-23 19:16:41', 1);
INSERT INTO `conquer_account_login_record` VALUES (29, 0, '25.47.231.41', 0, '2024-06-23 19:19:29', 1);
INSERT INTO `conquer_account_login_record` VALUES (30, 2, '25.47.231.41', 0, '2024-06-23 19:21:32', 1);
INSERT INTO `conquer_account_login_record` VALUES (31, 2, '25.47.231.41', 0, '2024-06-23 19:29:31', 1);
INSERT INTO `conquer_account_login_record` VALUES (32, 2, '25.47.231.41', 0, '2024-06-23 20:14:06', 1);
INSERT INTO `conquer_account_login_record` VALUES (33, 2, '25.47.231.41', 0, '2024-06-23 20:32:09', 1);
INSERT INTO `conquer_account_login_record` VALUES (34, 2, '25.47.231.41', 0, '2024-06-23 21:11:37', 1);
INSERT INTO `conquer_account_login_record` VALUES (35, 2, '25.47.231.41', 0, '2024-06-23 21:14:16', 1);
INSERT INTO `conquer_account_login_record` VALUES (36, 2, '25.47.231.41', 0, '2024-06-23 21:18:09', 1);
INSERT INTO `conquer_account_login_record` VALUES (37, 2, '25.47.231.41', 0, '2024-06-23 21:42:43', 1);
INSERT INTO `conquer_account_login_record` VALUES (38, 2, '25.47.231.41', 0, '2024-06-23 21:44:31', 1);
INSERT INTO `conquer_account_login_record` VALUES (39, 2, '25.47.231.41', 0, '2024-06-23 21:46:58', 1);
INSERT INTO `conquer_account_login_record` VALUES (40, 2, '25.47.231.41', 0, '2024-06-23 22:11:49', 1);
INSERT INTO `conquer_account_login_record` VALUES (41, 2, '25.47.231.41', 0, '2024-06-23 22:18:09', 1);
INSERT INTO `conquer_account_login_record` VALUES (42, 2, '25.47.231.41', 0, '2024-06-23 22:19:12', 1);
INSERT INTO `conquer_account_login_record` VALUES (43, 2, '25.47.231.41', 0, '2024-06-23 22:19:40', 1);
INSERT INTO `conquer_account_login_record` VALUES (44, 2, '25.47.231.41', 0, '2024-06-23 22:22:12', 1);
INSERT INTO `conquer_account_login_record` VALUES (45, 2, '25.47.231.41', 0, '2024-06-23 22:23:24', 1);
INSERT INTO `conquer_account_login_record` VALUES (46, 2, '25.47.231.41', 0, '2024-06-23 22:26:03', 1);
INSERT INTO `conquer_account_login_record` VALUES (47, 2, '25.47.231.41', 0, '2024-06-23 22:27:57', 1);
INSERT INTO `conquer_account_login_record` VALUES (48, 2, '25.47.231.41', 0, '2024-06-23 22:29:17', 1);
INSERT INTO `conquer_account_login_record` VALUES (49, 2, '25.47.231.41', 0, '2024-06-23 22:30:01', 1);
INSERT INTO `conquer_account_login_record` VALUES (50, 2, '25.47.231.41', 0, '2024-06-23 22:30:52', 1);
INSERT INTO `conquer_account_login_record` VALUES (51, 2, '25.47.231.41', 0, '2024-06-23 22:32:12', 1);
INSERT INTO `conquer_account_login_record` VALUES (52, 2, '25.47.231.41', 0, '2024-06-23 22:32:49', 1);
INSERT INTO `conquer_account_login_record` VALUES (53, 2, '25.47.231.41', 0, '2024-06-23 22:33:28', 1);
INSERT INTO `conquer_account_login_record` VALUES (54, 1, '25.47.231.41', 0, '2024-06-23 22:34:29', 1);
INSERT INTO `conquer_account_login_record` VALUES (55, 2, '25.47.231.41', 0, '2024-06-23 22:35:04', 1);
INSERT INTO `conquer_account_login_record` VALUES (56, 2, '25.47.231.41', 0, '2024-06-23 22:36:39', 1);
INSERT INTO `conquer_account_login_record` VALUES (57, 2, '25.47.231.41', 0, '2024-06-23 22:39:16', 1);
INSERT INTO `conquer_account_login_record` VALUES (58, 2, '25.47.231.41', 0, '2024-06-23 22:43:09', 1);
INSERT INTO `conquer_account_login_record` VALUES (59, 2, '25.47.231.41', 0, '2024-06-23 22:44:37', 1);
INSERT INTO `conquer_account_login_record` VALUES (60, 2, '25.47.231.41', 0, '2024-06-23 22:47:09', 1);
INSERT INTO `conquer_account_login_record` VALUES (61, 2, '25.47.231.41', 0, '2024-06-23 22:47:40', 1);
INSERT INTO `conquer_account_login_record` VALUES (62, 2, '25.47.231.41', 0, '2024-06-24 08:08:11', 1);
INSERT INTO `conquer_account_login_record` VALUES (63, 2, '25.47.231.41', 0, '2024-06-24 08:39:36', 1);
INSERT INTO `conquer_account_login_record` VALUES (64, 2, '25.47.231.41', 0, '2024-06-24 09:34:06', 1);
INSERT INTO `conquer_account_login_record` VALUES (65, 2, '25.47.231.41', 0, '2024-06-24 09:35:28', 1);
INSERT INTO `conquer_account_login_record` VALUES (66, 2, '25.47.231.41', 0, '2024-06-24 09:38:27', 1);
INSERT INTO `conquer_account_login_record` VALUES (67, 2, '25.47.231.41', 0, '2024-06-24 09:41:11', 1);
INSERT INTO `conquer_account_login_record` VALUES (68, 2, '25.47.231.41', 0, '2024-06-24 09:43:43', 1);
INSERT INTO `conquer_account_login_record` VALUES (69, 2, '25.47.231.41', 0, '2024-06-24 09:45:28', 1);
INSERT INTO `conquer_account_login_record` VALUES (70, 2, '25.47.231.41', 0, '2024-06-24 09:50:10', 1);
INSERT INTO `conquer_account_login_record` VALUES (71, 2, '25.47.231.41', 0, '2024-06-24 09:50:45', 1);
INSERT INTO `conquer_account_login_record` VALUES (72, 2, '25.47.231.41', 0, '2024-06-24 10:11:10', 1);
INSERT INTO `conquer_account_login_record` VALUES (73, 2, '25.47.231.41', 0, '2024-06-24 10:11:32', 1);
INSERT INTO `conquer_account_login_record` VALUES (74, 2, '25.47.231.41', 0, '2024-06-24 10:12:59', 1);
INSERT INTO `conquer_account_login_record` VALUES (75, 2, '25.47.231.41', 0, '2024-06-24 10:16:58', 1);
INSERT INTO `conquer_account_login_record` VALUES (76, 2, '25.47.231.41', 0, '2024-06-24 10:18:00', 1);
INSERT INTO `conquer_account_login_record` VALUES (77, 2, '25.47.231.41', 0, '2024-06-24 10:18:55', 1);
INSERT INTO `conquer_account_login_record` VALUES (78, 2, '25.47.231.41', 0, '2024-06-24 10:19:52', 1);
INSERT INTO `conquer_account_login_record` VALUES (79, 2, '25.47.231.41', 0, '2024-06-24 10:20:18', 1);
INSERT INTO `conquer_account_login_record` VALUES (80, 0, '25.47.231.41', 0, '2024-06-24 10:20:57', 1);
INSERT INTO `conquer_account_login_record` VALUES (81, 1, '25.47.231.41', 0, '2024-06-24 10:22:25', 1);
INSERT INTO `conquer_account_login_record` VALUES (82, 1, '25.47.231.41', 0, '2024-06-24 10:22:50', 1);
INSERT INTO `conquer_account_login_record` VALUES (83, 0, '25.47.231.41', 0, '2024-06-24 10:23:17', 1);
INSERT INTO `conquer_account_login_record` VALUES (84, 0, '25.47.231.41', 0, '2024-06-24 10:28:39', 1);
INSERT INTO `conquer_account_login_record` VALUES (85, 1, '25.47.231.41', 0, '2024-06-24 10:37:12', 1);
INSERT INTO `conquer_account_login_record` VALUES (86, 2, '25.47.231.41', 0, '2024-06-24 10:37:37', 1);
INSERT INTO `conquer_account_login_record` VALUES (87, 0, '25.47.231.41', 0, '2024-06-24 10:38:01', 1);
INSERT INTO `conquer_account_login_record` VALUES (88, 2, '25.47.231.41', 0, '2024-06-24 10:58:02', 1);
INSERT INTO `conquer_account_login_record` VALUES (89, 0, '25.47.231.41', 0, '2024-06-24 10:58:34', 1);
INSERT INTO `conquer_account_login_record` VALUES (90, 2, '25.47.231.41', 0, '2024-06-24 11:01:41', 1);
INSERT INTO `conquer_account_login_record` VALUES (91, 0, '25.47.231.41', 0, '2024-06-24 11:01:48', 1);
INSERT INTO `conquer_account_login_record` VALUES (92, 2, '25.47.231.41', 0, '2024-06-24 11:28:44', 1);
INSERT INTO `conquer_account_login_record` VALUES (93, 0, '25.47.231.41', 0, '2024-06-24 11:28:57', 1);
INSERT INTO `conquer_account_login_record` VALUES (94, 2, '25.47.231.41', 0, '2024-06-24 15:50:03', 1);
INSERT INTO `conquer_account_login_record` VALUES (95, 0, '25.47.231.41', 0, '2024-06-24 15:50:08', 1);
INSERT INTO `conquer_account_login_record` VALUES (96, 2, '25.47.231.41', 0, '2024-06-24 16:38:19', 1);
INSERT INTO `conquer_account_login_record` VALUES (97, 2, '25.47.231.41', 0, '2024-06-24 16:38:36', 1);
INSERT INTO `conquer_account_login_record` VALUES (98, 2, '25.47.231.41', 0, '2024-06-24 16:42:48', 1);
INSERT INTO `conquer_account_login_record` VALUES (99, 2, '25.47.231.41', 0, '2024-06-24 16:47:33', 1);
INSERT INTO `conquer_account_login_record` VALUES (100, 2, '25.47.231.41', 0, '2024-06-24 16:52:49', 1);
INSERT INTO `conquer_account_login_record` VALUES (101, 2, '25.47.231.41', 0, '2024-06-24 16:55:32', 1);
INSERT INTO `conquer_account_login_record` VALUES (102, 2, '25.47.231.41', 0, '2024-06-24 16:56:31', 1);
INSERT INTO `conquer_account_login_record` VALUES (103, 2, '25.47.231.41', 0, '2024-06-24 16:58:02', 1);
INSERT INTO `conquer_account_login_record` VALUES (104, 2, '25.47.231.41', 0, '2024-06-24 16:59:44', 1);
INSERT INTO `conquer_account_login_record` VALUES (105, 2, '25.47.231.41', 0, '2024-06-24 17:09:40', 1);
INSERT INTO `conquer_account_login_record` VALUES (106, 2, '25.47.231.41', 0, '2024-06-24 17:10:18', 1);
INSERT INTO `conquer_account_login_record` VALUES (107, 2, '25.47.231.41', 0, '2024-06-24 17:18:26', 1);
INSERT INTO `conquer_account_login_record` VALUES (108, 2, '25.47.231.41', 0, '2024-06-24 17:30:25', 1);
INSERT INTO `conquer_account_login_record` VALUES (109, 2, '25.47.231.41', 0, '2024-06-24 18:46:29', 1);
INSERT INTO `conquer_account_login_record` VALUES (110, 2, '25.47.231.41', 0, '2024-06-24 19:35:59', 1);
INSERT INTO `conquer_account_login_record` VALUES (111, 2, '25.47.231.41', 0, '2024-06-24 19:37:35', 1);
INSERT INTO `conquer_account_login_record` VALUES (112, 2, '25.47.231.41', 0, '2024-06-24 19:46:59', 1);
INSERT INTO `conquer_account_login_record` VALUES (113, 2, '25.47.231.41', 0, '2024-06-24 19:47:59', 1);
INSERT INTO `conquer_account_login_record` VALUES (114, 2, '25.47.231.41', 0, '2024-06-24 19:51:51', 1);
INSERT INTO `conquer_account_login_record` VALUES (115, 2, '25.47.231.41', 0, '2024-06-24 21:02:32', 1);
INSERT INTO `conquer_account_login_record` VALUES (116, 2, '25.47.231.41', 0, '2024-06-24 23:19:14', 1);
INSERT INTO `conquer_account_login_record` VALUES (117, 2, '25.47.231.41', 0, '2024-06-24 23:34:00', 1);
INSERT INTO `conquer_account_login_record` VALUES (118, 2, '25.47.231.41', 0, '2024-06-24 23:35:40', 1);
INSERT INTO `conquer_account_login_record` VALUES (119, 2, '25.47.231.41', 0, '2024-06-24 23:36:30', 1);
INSERT INTO `conquer_account_login_record` VALUES (120, 2, '25.47.231.41', 0, '2024-06-25 00:08:41', 1);
INSERT INTO `conquer_account_login_record` VALUES (121, 2, '25.47.231.41', 0, '2024-06-25 00:16:48', 1);
INSERT INTO `conquer_account_login_record` VALUES (122, 2, '25.47.231.41', 0, '2024-06-25 09:54:07', 1);
INSERT INTO `conquer_account_login_record` VALUES (123, 2, '25.47.231.41', 0, '2024-06-25 10:26:20', 1);
INSERT INTO `conquer_account_login_record` VALUES (124, 2, '25.47.231.41', 0, '2024-06-25 10:41:49', 1);
INSERT INTO `conquer_account_login_record` VALUES (125, 2, '25.47.231.41', 0, '2024-06-25 10:45:11', 1);
INSERT INTO `conquer_account_login_record` VALUES (126, 2, '25.47.231.41', 0, '2024-06-25 10:48:46', 1);
INSERT INTO `conquer_account_login_record` VALUES (127, 2, '25.47.231.41', 0, '2024-06-25 11:07:27', 1);
INSERT INTO `conquer_account_login_record` VALUES (128, 2, '25.47.231.41', 0, '2024-06-25 11:12:01', 1);
INSERT INTO `conquer_account_login_record` VALUES (129, 2, '25.47.231.41', 0, '2024-06-25 11:12:36', 1);
INSERT INTO `conquer_account_login_record` VALUES (130, 0, '25.47.231.41', 0, '2024-06-25 11:15:12', 1);
INSERT INTO `conquer_account_login_record` VALUES (131, 2, '25.47.231.41', 0, '2024-06-25 11:17:53', 1);
INSERT INTO `conquer_account_login_record` VALUES (132, 2, '25.47.231.41', 0, '2024-06-25 11:18:44', 1);
INSERT INTO `conquer_account_login_record` VALUES (133, 2, '25.47.231.41', 0, '2024-06-25 11:19:44', 1);
INSERT INTO `conquer_account_login_record` VALUES (134, 2, '25.47.231.41', 0, '2024-06-25 11:21:12', 1);
INSERT INTO `conquer_account_login_record` VALUES (135, 2, '25.47.231.41', 0, '2024-06-25 11:22:02', 1);
INSERT INTO `conquer_account_login_record` VALUES (136, 2, '25.47.231.41', 0, '2024-06-25 11:23:06', 1);
INSERT INTO `conquer_account_login_record` VALUES (137, 2, '25.47.231.41', 0, '2024-06-25 11:23:36', 1);
INSERT INTO `conquer_account_login_record` VALUES (138, 2, '25.47.231.41', 0, '2024-06-25 11:24:27', 1);
INSERT INTO `conquer_account_login_record` VALUES (139, 2, '25.47.231.41', 0, '2024-06-25 11:25:27', 1);
INSERT INTO `conquer_account_login_record` VALUES (140, 2, '25.47.231.41', 0, '2024-06-25 11:26:28', 1);
INSERT INTO `conquer_account_login_record` VALUES (141, 0, '25.47.231.41', 0, '2024-06-25 11:27:48', 1);
INSERT INTO `conquer_account_login_record` VALUES (142, 2, '25.47.231.41', 0, '2024-06-25 11:27:52', 1);
INSERT INTO `conquer_account_login_record` VALUES (143, 2, '25.47.231.41', 0, '2024-06-25 11:29:12', 1);
INSERT INTO `conquer_account_login_record` VALUES (144, 2, '25.47.231.41', 0, '2024-06-25 11:29:54', 1);
INSERT INTO `conquer_account_login_record` VALUES (145, 2, '25.47.231.41', 0, '2024-06-25 11:30:25', 1);
INSERT INTO `conquer_account_login_record` VALUES (146, 2, '25.47.231.41', 0, '2024-06-25 11:31:09', 1);
INSERT INTO `conquer_account_login_record` VALUES (147, 2, '25.47.231.41', 0, '2024-06-25 11:31:50', 1);
INSERT INTO `conquer_account_login_record` VALUES (148, 2, '25.47.231.41', 0, '2024-06-25 11:34:09', 1);
INSERT INTO `conquer_account_login_record` VALUES (149, 2, '25.47.231.41', 0, '2024-06-25 11:35:29', 1);
INSERT INTO `conquer_account_login_record` VALUES (150, 0, '25.47.231.41', 0, '2024-06-25 11:47:29', 1);
INSERT INTO `conquer_account_login_record` VALUES (151, 2, '25.47.231.41', 0, '2024-06-25 11:48:53', 1);
INSERT INTO `conquer_account_login_record` VALUES (152, 2, '25.47.231.41', 0, '2024-06-25 11:50:11', 1);
INSERT INTO `conquer_account_login_record` VALUES (153, 2, '25.47.231.41', 0, '2024-06-25 11:50:45', 1);
INSERT INTO `conquer_account_login_record` VALUES (154, 2, '25.47.231.41', 0, '2024-06-25 11:51:35', 1);
INSERT INTO `conquer_account_login_record` VALUES (155, 2, '25.47.231.41', 0, '2024-06-25 11:52:03', 1);
INSERT INTO `conquer_account_login_record` VALUES (156, 2, '25.47.231.41', 0, '2024-06-25 11:52:47', 1);
INSERT INTO `conquer_account_login_record` VALUES (157, 2, '25.47.231.41', 0, '2024-06-25 11:53:51', 1);
INSERT INTO `conquer_account_login_record` VALUES (158, 2, '25.47.231.41', 0, '2024-06-25 11:54:41', 1);
INSERT INTO `conquer_account_login_record` VALUES (159, 2, '25.47.231.41', 0, '2024-06-25 11:55:27', 1);
INSERT INTO `conquer_account_login_record` VALUES (160, 2, '25.47.231.41', 0, '2024-06-25 11:55:57', 1);
INSERT INTO `conquer_account_login_record` VALUES (161, 2, '25.47.231.41', 0, '2024-06-25 11:56:35', 1);
INSERT INTO `conquer_account_login_record` VALUES (162, 2, '25.47.231.41', 0, '2024-06-25 12:40:22', 1);
INSERT INTO `conquer_account_login_record` VALUES (163, 2, '25.47.231.41', 0, '2024-06-25 12:55:41', 1);
INSERT INTO `conquer_account_login_record` VALUES (164, 2, '25.47.231.41', 0, '2024-06-25 12:55:49', 1);
INSERT INTO `conquer_account_login_record` VALUES (165, 2, '25.47.231.41', 0, '2024-06-25 13:25:40', 1);
INSERT INTO `conquer_account_login_record` VALUES (166, 3, '25.47.231.41', 0, '2024-06-25 13:26:16', 1);
INSERT INTO `conquer_account_login_record` VALUES (167, 3, '25.47.231.41', 0, '2024-06-25 15:44:12', 1);
INSERT INTO `conquer_account_login_record` VALUES (168, 3, '25.47.231.41', 0, '2024-06-25 16:09:30', 1);
INSERT INTO `conquer_account_login_record` VALUES (169, 1, '25.47.231.41', 0, '2024-06-25 16:11:02', 1);
INSERT INTO `conquer_account_login_record` VALUES (170, 4, '25.47.231.41', 0, '2024-06-25 16:11:55', 1);
INSERT INTO `conquer_account_login_record` VALUES (171, 4, '25.47.231.41', 0, '2024-06-25 16:23:31', 1);
INSERT INTO `conquer_account_login_record` VALUES (172, 3, '25.47.231.41', 0, '2024-06-25 17:39:44', 1);
INSERT INTO `conquer_account_login_record` VALUES (173, 1, '25.47.231.41', 0, '2024-06-25 17:40:21', 1);
INSERT INTO `conquer_account_login_record` VALUES (174, 4, '25.47.231.41', 0, '2024-06-25 17:41:16', 1);
INSERT INTO `conquer_account_login_record` VALUES (175, 1, '25.47.231.41', 0, '2024-06-25 17:56:01', 1);
INSERT INTO `conquer_account_login_record` VALUES (176, 5, '25.47.231.41', 0, '2024-06-25 17:57:19', 1);
INSERT INTO `conquer_account_login_record` VALUES (177, 5, '25.47.231.41', 0, '2024-06-25 18:01:48', 1);
INSERT INTO `conquer_account_login_record` VALUES (178, 5, '25.47.231.41', 0, '2024-06-25 18:05:25', 1);
INSERT INTO `conquer_account_login_record` VALUES (179, 1, '25.47.231.41', 0, '2024-06-25 18:06:24', 1);
INSERT INTO `conquer_account_login_record` VALUES (180, 1, '25.47.231.41', 0, '2024-06-25 18:07:55', 1);
INSERT INTO `conquer_account_login_record` VALUES (181, 3, '25.47.231.41', 0, '2024-06-25 18:08:18', 1);
INSERT INTO `conquer_account_login_record` VALUES (182, 3, '25.47.231.41', 0, '2024-06-25 18:12:23', 1);
INSERT INTO `conquer_account_login_record` VALUES (183, 6, '25.47.231.41', 0, '2024-06-25 18:15:38', 1);
INSERT INTO `conquer_account_login_record` VALUES (184, 5, '25.47.231.41', 0, '2024-06-25 18:41:55', 1);
INSERT INTO `conquer_account_login_record` VALUES (185, 6, '25.47.231.41', 0, '2024-06-25 18:46:40', 1);
INSERT INTO `conquer_account_login_record` VALUES (186, 6, '25.47.231.41', 0, '2024-06-25 19:20:50', 1);
INSERT INTO `conquer_account_login_record` VALUES (187, 6, '25.47.231.41', 0, '2024-06-25 19:32:05', 1);
INSERT INTO `conquer_account_login_record` VALUES (188, 6, '25.47.231.41', 0, '2024-06-25 19:32:43', 1);
INSERT INTO `conquer_account_login_record` VALUES (189, 6, '25.47.231.41', 0, '2024-06-25 19:42:13', 1);
INSERT INTO `conquer_account_login_record` VALUES (190, 6, '25.47.231.41', 0, '2024-06-25 23:18:11', 1);
INSERT INTO `conquer_account_login_record` VALUES (191, 3, '25.47.231.41', 0, '2024-06-25 23:19:34', 1);
INSERT INTO `conquer_account_login_record` VALUES (192, 3, '25.47.231.41', 0, '2024-06-25 23:20:19', 1);
INSERT INTO `conquer_account_login_record` VALUES (193, 3, '25.47.231.41', 0, '2024-06-25 23:32:54', 1);
INSERT INTO `conquer_account_login_record` VALUES (194, 3, '25.47.231.41', 0, '2024-06-26 10:50:34', 1);
INSERT INTO `conquer_account_login_record` VALUES (195, 3, '25.47.231.41', 0, '2024-06-26 11:41:50', 1);
INSERT INTO `conquer_account_login_record` VALUES (196, 3, '25.47.231.41', 0, '2024-06-26 12:09:31', 1);
INSERT INTO `conquer_account_login_record` VALUES (197, 3, '25.47.231.41', 0, '2024-06-26 13:10:25', 1);
INSERT INTO `conquer_account_login_record` VALUES (198, 3, '25.47.231.41', 0, '2024-06-26 13:31:22', 1);
INSERT INTO `conquer_account_login_record` VALUES (199, 3, '25.47.231.41', 0, '2024-06-26 14:43:28', 1);
INSERT INTO `conquer_account_login_record` VALUES (200, 3, '25.47.231.41', 0, '2024-06-26 14:45:58', 1);
INSERT INTO `conquer_account_login_record` VALUES (201, 3, '25.47.231.41', 0, '2024-06-26 14:47:20', 1);
INSERT INTO `conquer_account_login_record` VALUES (202, 3, '25.47.231.41', 0, '2024-06-26 14:48:26', 1);
INSERT INTO `conquer_account_login_record` VALUES (203, 3, '25.47.231.41', 0, '2024-06-26 14:49:28', 1);
INSERT INTO `conquer_account_login_record` VALUES (204, 3, '25.47.231.41', 0, '2024-06-26 14:59:17', 1);
INSERT INTO `conquer_account_login_record` VALUES (205, 3, '25.47.231.41', 0, '2024-06-26 15:27:05', 1);
INSERT INTO `conquer_account_login_record` VALUES (206, 3, '25.47.231.41', 0, '2024-06-26 15:58:16', 1);
INSERT INTO `conquer_account_login_record` VALUES (207, 3, '25.47.231.41', 0, '2024-06-26 17:26:35', 1);
INSERT INTO `conquer_account_login_record` VALUES (208, 3, '25.47.231.41', 0, '2024-06-26 17:26:59', 1);
INSERT INTO `conquer_account_login_record` VALUES (209, 3, '25.47.231.41', 0, '2024-06-26 17:44:16', 1);
INSERT INTO `conquer_account_login_record` VALUES (210, 3, '25.47.231.41', 0, '2024-06-26 17:56:17', 1);
INSERT INTO `conquer_account_login_record` VALUES (211, 3, '25.47.231.41', 0, '2024-06-26 17:59:13', 1);
INSERT INTO `conquer_account_login_record` VALUES (212, 3, '25.47.231.41', 0, '2024-06-26 18:15:25', 1);
INSERT INTO `conquer_account_login_record` VALUES (213, 3, '25.47.231.41', 0, '2024-06-26 18:28:00', 1);
INSERT INTO `conquer_account_login_record` VALUES (214, 3, '25.47.231.41', 0, '2024-06-26 18:29:04', 1);
INSERT INTO `conquer_account_login_record` VALUES (215, 3, '25.47.231.41', 0, '2024-06-26 18:34:54', 1);
INSERT INTO `conquer_account_login_record` VALUES (216, 3, '25.47.231.41', 0, '2024-06-26 18:35:26', 1);
INSERT INTO `conquer_account_login_record` VALUES (217, 3, '25.47.231.41', 0, '2024-06-26 19:06:40', 1);
INSERT INTO `conquer_account_login_record` VALUES (218, 3, '25.47.231.41', 0, '2024-06-26 19:11:47', 1);
INSERT INTO `conquer_account_login_record` VALUES (219, 3, '25.47.231.41', 0, '2024-06-26 19:55:57', 1);
INSERT INTO `conquer_account_login_record` VALUES (220, 3, '25.47.231.41', 0, '2024-06-26 20:08:56', 1);
INSERT INTO `conquer_account_login_record` VALUES (221, 3, '25.47.231.41', 0, '2024-06-26 20:30:53', 1);
INSERT INTO `conquer_account_login_record` VALUES (222, 3, '25.47.231.41', 0, '2024-06-26 20:35:40', 1);
INSERT INTO `conquer_account_login_record` VALUES (223, 3, '25.47.231.41', 0, '2024-06-26 20:44:50', 1);
INSERT INTO `conquer_account_login_record` VALUES (224, 3, '25.47.231.41', 0, '2024-06-26 21:20:46', 1);
INSERT INTO `conquer_account_login_record` VALUES (225, 3, '25.47.231.41', 0, '2024-06-26 21:38:41', 1);
INSERT INTO `conquer_account_login_record` VALUES (226, 3, '25.47.231.41', 0, '2024-06-26 21:43:19', 1);
INSERT INTO `conquer_account_login_record` VALUES (227, 2, '25.47.231.41', 0, '2024-06-26 22:00:29', 1);
INSERT INTO `conquer_account_login_record` VALUES (228, 3, '25.47.231.41', 0, '2024-06-26 22:28:39', 1);
INSERT INTO `conquer_account_login_record` VALUES (229, 2, '25.47.231.41', 0, '2024-06-26 22:28:41', 1);
INSERT INTO `conquer_account_login_record` VALUES (230, 2, '25.47.231.41', 0, '2024-06-26 22:34:51', 1);
INSERT INTO `conquer_account_login_record` VALUES (231, 3, '25.47.231.41', 0, '2024-06-26 22:34:59', 1);
INSERT INTO `conquer_account_login_record` VALUES (232, 3, '25.47.231.41', 0, '2024-06-26 23:09:11', 1);
INSERT INTO `conquer_account_login_record` VALUES (233, 2, '25.47.231.41', 0, '2024-06-26 23:10:04', 1);
INSERT INTO `conquer_account_login_record` VALUES (234, 3, '25.47.231.41', 0, '2024-06-27 00:17:14', 1);
INSERT INTO `conquer_account_login_record` VALUES (235, 2, '25.47.231.41', 0, '2024-06-27 00:17:29', 1);
INSERT INTO `conquer_account_login_record` VALUES (236, 2, '25.47.231.41', 0, '2024-06-27 00:21:34', 1);
INSERT INTO `conquer_account_login_record` VALUES (237, 3, '25.47.231.41', 0, '2024-06-27 11:04:14', 1);
INSERT INTO `conquer_account_login_record` VALUES (238, 3, '25.47.231.41', 0, '2024-06-27 11:04:29', 1);
INSERT INTO `conquer_account_login_record` VALUES (239, 3, '25.47.231.41', 0, '2024-06-27 14:12:10', 1);
INSERT INTO `conquer_account_login_record` VALUES (240, 2, '25.47.231.41', 0, '2024-06-27 19:26:44', 1);
INSERT INTO `conquer_account_login_record` VALUES (241, 3, '25.47.231.41', 0, '2024-06-27 19:27:32', 1);
INSERT INTO `conquer_account_login_record` VALUES (242, 3, '25.47.231.41', 0, '2024-06-27 20:47:51', 1);
INSERT INTO `conquer_account_login_record` VALUES (243, 3, '25.47.231.41', 0, '2024-06-27 20:56:32', 1);
INSERT INTO `conquer_account_login_record` VALUES (244, 3, '25.47.231.41', 0, '2024-06-27 21:04:23', 1);
INSERT INTO `conquer_account_login_record` VALUES (245, 3, '25.47.231.41', 0, '2024-06-27 22:10:04', 1);
INSERT INTO `conquer_account_login_record` VALUES (246, 3, '25.47.231.41', 0, '2024-06-27 23:32:03', 1);
INSERT INTO `conquer_account_login_record` VALUES (247, 3, '25.47.231.41', 0, '2024-06-27 23:34:03', 1);
INSERT INTO `conquer_account_login_record` VALUES (248, 3, '25.47.231.41', 0, '2024-06-27 23:36:48', 1);
INSERT INTO `conquer_account_login_record` VALUES (249, 3, '25.47.231.41', 0, '2024-06-27 23:45:04', 1);
INSERT INTO `conquer_account_login_record` VALUES (250, 3, '25.47.231.41', 0, '2024-06-27 23:51:26', 1);
INSERT INTO `conquer_account_login_record` VALUES (251, 3, '25.47.231.41', 0, '2024-06-27 23:54:27', 1);
INSERT INTO `conquer_account_login_record` VALUES (252, 3, '25.47.231.41', 0, '2024-06-28 00:06:21', 1);
INSERT INTO `conquer_account_login_record` VALUES (253, 3, '25.47.231.41', 0, '2024-06-28 00:08:51', 1);
INSERT INTO `conquer_account_login_record` VALUES (254, 3, '25.47.231.41', 0, '2024-06-28 00:10:53', 1);
INSERT INTO `conquer_account_login_record` VALUES (255, 3, '25.47.231.41', 0, '2024-06-28 00:16:16', 1);
INSERT INTO `conquer_account_login_record` VALUES (256, 3, '25.47.231.41', 0, '2024-06-28 00:23:03', 1);
INSERT INTO `conquer_account_login_record` VALUES (257, 3, '25.47.231.41', 0, '2024-06-28 00:24:18', 1);
INSERT INTO `conquer_account_login_record` VALUES (258, 3, '25.47.231.41', 0, '2024-06-28 00:34:06', 1);
INSERT INTO `conquer_account_login_record` VALUES (259, 3, '25.47.231.41', 0, '2024-06-28 00:41:14', 1);
INSERT INTO `conquer_account_login_record` VALUES (260, 3, '25.47.231.41', 0, '2024-06-28 00:42:13', 1);
INSERT INTO `conquer_account_login_record` VALUES (261, 3, '25.47.231.41', 0, '2024-06-28 00:43:31', 1);
INSERT INTO `conquer_account_login_record` VALUES (262, 3, '25.47.231.41', 0, '2024-06-28 00:52:33', 1);
INSERT INTO `conquer_account_login_record` VALUES (263, 3, '25.47.231.41', 0, '2024-06-28 00:53:59', 1);
INSERT INTO `conquer_account_login_record` VALUES (264, 3, '25.47.231.41', 0, '2024-06-28 00:54:50', 1);
INSERT INTO `conquer_account_login_record` VALUES (265, 3, '25.47.231.41', 0, '2024-06-28 00:56:12', 1);
INSERT INTO `conquer_account_login_record` VALUES (266, 3, '25.47.231.41', 0, '2024-06-28 00:57:13', 1);
INSERT INTO `conquer_account_login_record` VALUES (267, 3, '25.47.231.41', 0, '2024-06-28 00:59:38', 1);
INSERT INTO `conquer_account_login_record` VALUES (268, 3, '25.47.231.41', 0, '2024-06-28 01:11:54', 1);
INSERT INTO `conquer_account_login_record` VALUES (269, 3, '25.47.231.41', 0, '2024-06-28 01:15:59', 1);
INSERT INTO `conquer_account_login_record` VALUES (270, 3, '25.47.231.41', 0, '2024-06-28 01:25:43', 1);
INSERT INTO `conquer_account_login_record` VALUES (271, 3, '25.47.231.41', 0, '2024-06-28 01:30:03', 1);
INSERT INTO `conquer_account_login_record` VALUES (272, 3, '25.47.231.41', 0, '2024-06-28 02:10:05', 1);
INSERT INTO `conquer_account_login_record` VALUES (273, 3, '25.47.231.41', 0, '2024-06-28 02:10:21', 1);
INSERT INTO `conquer_account_login_record` VALUES (274, 3, '25.47.231.41', 0, '2024-06-28 02:16:36', 1);
INSERT INTO `conquer_account_login_record` VALUES (275, 3, '25.47.231.41', 0, '2024-06-28 02:21:41', 1);
INSERT INTO `conquer_account_login_record` VALUES (276, 3, '25.47.231.41', 0, '2024-06-28 02:27:25', 1);
INSERT INTO `conquer_account_login_record` VALUES (277, 3, '25.47.231.41', 0, '2024-06-28 02:29:10', 1);
INSERT INTO `conquer_account_login_record` VALUES (278, 3, '25.47.231.41', 0, '2024-06-28 02:29:57', 1);
INSERT INTO `conquer_account_login_record` VALUES (279, 3, '25.47.231.41', 0, '2024-06-28 02:38:06', 1);
INSERT INTO `conquer_account_login_record` VALUES (280, 3, '25.47.231.41', 0, '2024-06-28 08:39:25', 1);
INSERT INTO `conquer_account_login_record` VALUES (281, 3, '25.47.231.41', 0, '2024-06-28 09:47:51', 1);
INSERT INTO `conquer_account_login_record` VALUES (282, 3, '25.47.231.41', 0, '2024-06-28 09:51:17', 1);
INSERT INTO `conquer_account_login_record` VALUES (283, 3, '25.47.231.41', 0, '2024-06-28 10:01:02', 1);
INSERT INTO `conquer_account_login_record` VALUES (284, 3, '25.47.231.41', 0, '2024-06-28 10:06:15', 1);
INSERT INTO `conquer_account_login_record` VALUES (285, 3, '25.47.231.41', 0, '2024-06-28 10:08:08', 1);
INSERT INTO `conquer_account_login_record` VALUES (286, 3, '25.47.231.41', 0, '2024-06-28 10:13:36', 1);
INSERT INTO `conquer_account_login_record` VALUES (287, 3, '25.47.231.41', 0, '2024-06-28 10:16:46', 1);
INSERT INTO `conquer_account_login_record` VALUES (288, 3, '25.47.231.41', 0, '2024-06-28 10:20:29', 1);
INSERT INTO `conquer_account_login_record` VALUES (289, 3, '25.47.231.41', 0, '2024-06-28 10:22:54', 1);
INSERT INTO `conquer_account_login_record` VALUES (290, 3, '25.47.231.41', 0, '2024-06-28 10:25:38', 1);
INSERT INTO `conquer_account_login_record` VALUES (291, 3, '25.47.231.41', 0, '2024-06-28 10:30:11', 1);
INSERT INTO `conquer_account_login_record` VALUES (292, 3, '25.47.231.41', 0, '2024-06-28 10:34:50', 1);
INSERT INTO `conquer_account_login_record` VALUES (293, 3, '25.47.231.41', 0, '2024-06-28 10:36:19', 1);
INSERT INTO `conquer_account_login_record` VALUES (294, 3, '25.47.231.41', 0, '2024-06-28 10:38:24', 1);
INSERT INTO `conquer_account_login_record` VALUES (295, 3, '25.47.231.41', 0, '2024-06-28 10:41:25', 1);
INSERT INTO `conquer_account_login_record` VALUES (296, 3, '25.47.231.41', 0, '2024-06-28 10:44:29', 1);
INSERT INTO `conquer_account_login_record` VALUES (297, 3, '25.47.231.41', 0, '2024-06-28 10:48:23', 1);
INSERT INTO `conquer_account_login_record` VALUES (298, 3, '25.47.231.41', 0, '2024-06-28 10:51:05', 1);
INSERT INTO `conquer_account_login_record` VALUES (299, 3, '25.47.231.41', 0, '2024-06-28 10:57:18', 1);
INSERT INTO `conquer_account_login_record` VALUES (300, 3, '25.47.231.41', 0, '2024-06-28 11:04:45', 1);
INSERT INTO `conquer_account_login_record` VALUES (301, 3, '25.47.231.41', 0, '2024-06-28 11:07:24', 1);
INSERT INTO `conquer_account_login_record` VALUES (302, 3, '25.47.231.41', 0, '2024-06-28 11:07:42', 1);
INSERT INTO `conquer_account_login_record` VALUES (303, 3, '25.47.231.41', 0, '2024-06-28 11:08:47', 1);
INSERT INTO `conquer_account_login_record` VALUES (304, 3, '25.47.231.41', 0, '2024-06-28 11:10:38', 1);
INSERT INTO `conquer_account_login_record` VALUES (305, 3, '25.47.231.41', 0, '2024-06-28 11:11:56', 1);
INSERT INTO `conquer_account_login_record` VALUES (306, 3, '25.47.231.41', 0, '2024-06-28 11:14:02', 1);
INSERT INTO `conquer_account_login_record` VALUES (307, 3, '25.47.231.41', 0, '2024-06-28 11:15:55', 1);
INSERT INTO `conquer_account_login_record` VALUES (308, 3, '25.47.231.41', 0, '2024-06-28 11:17:49', 1);
INSERT INTO `conquer_account_login_record` VALUES (309, 3, '25.47.231.41', 0, '2024-06-28 11:19:19', 1);
INSERT INTO `conquer_account_login_record` VALUES (310, 3, '25.47.231.41', 0, '2024-06-28 11:20:44', 1);
INSERT INTO `conquer_account_login_record` VALUES (311, 3, '25.47.231.41', 0, '2024-06-28 11:23:06', 1);
INSERT INTO `conquer_account_login_record` VALUES (312, 3, '25.47.231.41', 0, '2024-06-28 11:27:16', 1);
INSERT INTO `conquer_account_login_record` VALUES (313, 3, '25.47.231.41', 0, '2024-06-28 11:29:54', 1);
INSERT INTO `conquer_account_login_record` VALUES (314, 3, '25.47.231.41', 0, '2024-06-28 11:31:44', 1);
INSERT INTO `conquer_account_login_record` VALUES (315, 3, '25.47.231.41', 0, '2024-06-28 11:37:26', 1);
INSERT INTO `conquer_account_login_record` VALUES (316, 3, '25.47.231.41', 0, '2024-06-28 11:40:10', 1);
INSERT INTO `conquer_account_login_record` VALUES (317, 3, '25.47.231.41', 0, '2024-06-28 11:41:21', 1);
INSERT INTO `conquer_account_login_record` VALUES (318, 3, '25.47.231.41', 0, '2024-06-28 11:44:46', 1);
INSERT INTO `conquer_account_login_record` VALUES (319, 3, '25.47.231.41', 0, '2024-06-28 11:47:02', 1);
INSERT INTO `conquer_account_login_record` VALUES (320, 3, '25.47.231.41', 0, '2024-06-28 11:48:52', 1);
INSERT INTO `conquer_account_login_record` VALUES (321, 3, '25.47.231.41', 0, '2024-06-28 11:53:56', 1);
INSERT INTO `conquer_account_login_record` VALUES (322, 3, '25.47.231.41', 0, '2024-06-28 11:55:17', 1);
INSERT INTO `conquer_account_login_record` VALUES (323, 3, '25.47.231.41', 0, '2024-06-28 11:56:43', 1);
INSERT INTO `conquer_account_login_record` VALUES (324, 3, '25.47.231.41', 0, '2024-06-28 12:06:21', 1);
INSERT INTO `conquer_account_login_record` VALUES (325, 3, '25.47.231.41', 0, '2024-06-28 12:06:54', 1);
INSERT INTO `conquer_account_login_record` VALUES (326, 3, '25.47.231.41', 0, '2024-06-28 12:08:20', 1);
INSERT INTO `conquer_account_login_record` VALUES (327, 3, '25.47.231.41', 0, '2024-06-28 12:10:27', 1);
INSERT INTO `conquer_account_login_record` VALUES (328, 3, '25.47.231.41', 0, '2024-06-28 12:23:52', 1);
INSERT INTO `conquer_account_login_record` VALUES (329, 3, '25.47.231.41', 0, '2024-06-28 12:28:20', 1);
INSERT INTO `conquer_account_login_record` VALUES (330, 3, '25.47.231.41', 0, '2024-06-28 13:06:28', 1);
INSERT INTO `conquer_account_login_record` VALUES (331, 3, '25.47.231.41', 0, '2024-06-28 13:06:43', 1);
INSERT INTO `conquer_account_login_record` VALUES (332, 3, '25.47.231.41', 0, '2024-06-28 13:10:44', 1);
INSERT INTO `conquer_account_login_record` VALUES (333, 3, '25.47.231.41', 0, '2024-06-28 13:27:04', 1);
INSERT INTO `conquer_account_login_record` VALUES (334, 3, '25.47.231.41', 0, '2024-06-28 13:29:14', 1);
INSERT INTO `conquer_account_login_record` VALUES (335, 3, '25.47.231.41', 0, '2024-06-28 14:08:49', 1);
INSERT INTO `conquer_account_login_record` VALUES (336, 3, '25.47.231.41', 0, '2024-06-28 14:11:09', 1);
INSERT INTO `conquer_account_login_record` VALUES (337, 2, '25.47.231.41', 0, '2024-06-28 14:14:07', 1);
INSERT INTO `conquer_account_login_record` VALUES (338, 3, '25.47.231.41', 0, '2024-06-28 14:15:22', 1);
INSERT INTO `conquer_account_login_record` VALUES (339, 3, '25.47.231.41', 0, '2024-06-28 14:16:13', 1);
INSERT INTO `conquer_account_login_record` VALUES (340, 3, '25.47.231.41', 0, '2024-06-28 14:21:54', 1);
INSERT INTO `conquer_account_login_record` VALUES (341, 3, '25.47.231.41', 0, '2024-06-28 14:22:59', 1);
INSERT INTO `conquer_account_login_record` VALUES (342, 3, '25.47.231.41', 0, '2024-06-28 14:25:27', 1);
INSERT INTO `conquer_account_login_record` VALUES (343, 3, '25.47.231.41', 0, '2024-06-28 14:28:34', 1);
INSERT INTO `conquer_account_login_record` VALUES (344, 3, '25.47.231.41', 0, '2024-06-28 14:28:49', 1);
INSERT INTO `conquer_account_login_record` VALUES (345, 3, '25.47.231.41', 0, '2024-06-28 14:29:05', 1);
INSERT INTO `conquer_account_login_record` VALUES (346, 3, '25.47.231.41', 0, '2024-06-28 14:46:58', 1);
INSERT INTO `conquer_account_login_record` VALUES (347, 3, '25.47.231.41', 0, '2024-06-28 14:51:26', 1);
INSERT INTO `conquer_account_login_record` VALUES (348, 3, '25.47.231.41', 0, '2024-06-28 15:29:08', 1);
INSERT INTO `conquer_account_login_record` VALUES (349, 3, '25.47.231.41', 0, '2024-06-28 16:05:33', 1);
INSERT INTO `conquer_account_login_record` VALUES (350, 3, '25.47.231.41', 0, '2024-06-28 16:14:37', 1);
INSERT INTO `conquer_account_login_record` VALUES (351, 3, '25.47.231.41', 0, '2024-06-28 16:15:07', 1);
INSERT INTO `conquer_account_login_record` VALUES (352, 3, '25.47.231.41', 0, '2024-06-28 16:15:22', 1);
INSERT INTO `conquer_account_login_record` VALUES (353, 3, '25.47.231.41', 0, '2024-06-28 16:15:43', 1);
INSERT INTO `conquer_account_login_record` VALUES (354, 3, '25.47.231.41', 0, '2024-06-28 16:24:00', 1);
INSERT INTO `conquer_account_login_record` VALUES (355, 3, '25.47.231.41', 0, '2024-06-28 16:36:59', 1);
INSERT INTO `conquer_account_login_record` VALUES (356, 3, '25.47.231.41', 0, '2024-06-28 20:32:38', 1);
INSERT INTO `conquer_account_login_record` VALUES (357, 3, '25.47.231.41', 0, '2024-06-28 20:40:27', 1);
INSERT INTO `conquer_account_login_record` VALUES (358, 3, '25.47.231.41', 0, '2024-06-28 23:19:57', 1);
INSERT INTO `conquer_account_login_record` VALUES (359, 2, '25.47.231.41', 0, '2024-06-28 23:21:21', 1);
INSERT INTO `conquer_account_login_record` VALUES (360, 3, '25.47.231.41', 0, '2024-06-28 23:31:19', 1);
INSERT INTO `conquer_account_login_record` VALUES (361, 3, '25.47.231.41', 0, '2024-06-28 23:53:57', 1);
INSERT INTO `conquer_account_login_record` VALUES (362, 3, '25.47.231.41', 0, '2024-06-29 00:08:59', 1);
INSERT INTO `conquer_account_login_record` VALUES (363, 3, '25.47.231.41', 0, '2024-06-29 01:02:21', 1);
INSERT INTO `conquer_account_login_record` VALUES (364, 3, '25.47.231.41', 0, '2024-06-29 01:17:25', 1);
INSERT INTO `conquer_account_login_record` VALUES (365, 3, '25.47.231.41', 0, '2024-06-29 01:31:41', 1);
INSERT INTO `conquer_account_login_record` VALUES (366, 3, '25.47.231.41', 0, '2024-06-29 01:43:30', 1);
INSERT INTO `conquer_account_login_record` VALUES (367, 3, '25.47.231.41', 0, '2024-06-29 10:57:16', 1);
INSERT INTO `conquer_account_login_record` VALUES (368, 3, '25.47.231.41', 0, '2024-06-29 11:13:57', 1);
INSERT INTO `conquer_account_login_record` VALUES (369, 3, '25.47.231.41', 0, '2024-06-29 22:25:18', 1);
INSERT INTO `conquer_account_login_record` VALUES (370, 3, '25.47.231.41', 0, '2024-06-29 22:28:52', 1);
INSERT INTO `conquer_account_login_record` VALUES (371, 3, '25.47.231.41', 0, '2024-06-29 22:33:27', 1);
INSERT INTO `conquer_account_login_record` VALUES (372, 3, '25.47.231.41', 0, '2024-06-29 22:48:24', 1);
INSERT INTO `conquer_account_login_record` VALUES (373, 3, '25.47.231.41', 0, '2024-06-29 23:06:15', 1);
INSERT INTO `conquer_account_login_record` VALUES (374, 3, '25.47.231.41', 0, '2024-06-29 23:06:50', 1);
INSERT INTO `conquer_account_login_record` VALUES (375, 3, '25.47.231.41', 0, '2024-06-29 23:08:54', 1);
INSERT INTO `conquer_account_login_record` VALUES (376, 3, '25.47.231.41', 0, '2024-06-29 23:10:01', 1);
INSERT INTO `conquer_account_login_record` VALUES (377, 3, '25.47.231.41', 0, '2024-06-29 23:10:32', 1);
INSERT INTO `conquer_account_login_record` VALUES (378, 3, '25.47.231.41', 0, '2024-06-29 23:12:59', 1);
INSERT INTO `conquer_account_login_record` VALUES (379, 3, '25.47.231.41', 0, '2024-06-29 23:13:14', 1);
INSERT INTO `conquer_account_login_record` VALUES (380, 3, '25.47.231.41', 0, '2024-06-29 23:15:59', 1);
INSERT INTO `conquer_account_login_record` VALUES (381, 3, '25.47.231.41', 0, '2024-06-29 23:17:20', 1);
INSERT INTO `conquer_account_login_record` VALUES (382, 3, '25.47.231.41', 0, '2024-06-29 23:18:51', 1);
INSERT INTO `conquer_account_login_record` VALUES (383, 3, '25.47.231.41', 0, '2024-06-29 23:19:57', 1);
INSERT INTO `conquer_account_login_record` VALUES (384, 3, '25.47.231.41', 0, '2024-06-30 10:09:23', 1);
INSERT INTO `conquer_account_login_record` VALUES (385, 3, '25.47.231.41', 0, '2024-06-30 11:22:25', 1);
INSERT INTO `conquer_account_login_record` VALUES (386, 3, '25.47.231.41', 0, '2024-06-30 11:22:45', 1);
INSERT INTO `conquer_account_login_record` VALUES (387, 3, '25.47.231.41', 0, '2024-06-30 11:31:42', 1);
INSERT INTO `conquer_account_login_record` VALUES (388, 3, '25.47.231.41', 0, '2024-06-30 11:32:03', 1);
INSERT INTO `conquer_account_login_record` VALUES (389, 3, '25.47.231.41', 0, '2024-06-30 11:34:41', 1);
INSERT INTO `conquer_account_login_record` VALUES (390, 3, '25.47.231.41', 0, '2024-06-30 11:41:32', 1);
INSERT INTO `conquer_account_login_record` VALUES (391, 3, '25.47.231.41', 0, '2024-06-30 11:44:54', 1);
INSERT INTO `conquer_account_login_record` VALUES (392, 3, '25.47.231.41', 0, '2024-06-30 11:46:42', 1);
INSERT INTO `conquer_account_login_record` VALUES (393, 3, '25.47.231.41', 0, '2024-06-30 11:47:09', 1);
INSERT INTO `conquer_account_login_record` VALUES (394, 3, '25.47.231.41', 0, '2024-06-30 11:48:45', 1);
INSERT INTO `conquer_account_login_record` VALUES (395, 3, '25.47.231.41', 0, '2024-06-30 11:49:15', 1);
INSERT INTO `conquer_account_login_record` VALUES (396, 3, '25.47.231.41', 0, '2024-06-30 11:49:57', 1);
INSERT INTO `conquer_account_login_record` VALUES (397, 3, '25.47.231.41', 0, '2024-06-30 23:37:10', 1);
INSERT INTO `conquer_account_login_record` VALUES (398, 3, '25.47.231.41', 0, '2024-07-01 18:45:36', 1);
INSERT INTO `conquer_account_login_record` VALUES (399, 3, '25.47.231.41', 0, '2024-07-01 19:11:58', 1);
INSERT INTO `conquer_account_login_record` VALUES (400, 3, '25.47.231.41', 0, '2024-07-02 10:16:10', 1);
INSERT INTO `conquer_account_login_record` VALUES (401, 0, '25.47.231.41', 0, '2024-07-02 12:34:05', 1);
INSERT INTO `conquer_account_login_record` VALUES (402, 3, '25.47.231.41', 0, '2024-07-02 12:34:39', 1);
INSERT INTO `conquer_account_login_record` VALUES (403, 3, '25.47.231.41', 0, '2024-07-02 14:23:55', 1);
INSERT INTO `conquer_account_login_record` VALUES (404, 3, '25.47.231.41', 0, '2024-07-02 15:12:47', 1);
INSERT INTO `conquer_account_login_record` VALUES (405, 3, '25.47.231.41', 0, '2024-07-02 22:44:27', 1);
INSERT INTO `conquer_account_login_record` VALUES (406, 3, '25.47.231.41', 0, '2024-07-02 22:44:44', 1);
INSERT INTO `conquer_account_login_record` VALUES (407, 3, '25.47.231.41', 0, '2024-07-02 22:47:42', 1);
INSERT INTO `conquer_account_login_record` VALUES (408, 3, '25.47.231.41', 0, '2024-07-02 22:51:42', 1);
INSERT INTO `conquer_account_login_record` VALUES (409, 3, '25.47.231.41', 0, '2024-07-02 22:53:28', 1);
INSERT INTO `conquer_account_login_record` VALUES (410, 3, '25.47.231.41', 0, '2024-07-02 22:54:20', 1);
INSERT INTO `conquer_account_login_record` VALUES (411, 2, '25.47.231.41', 0, '2024-07-02 22:55:58', 1);
INSERT INTO `conquer_account_login_record` VALUES (412, 2, '25.47.231.41', 0, '2024-07-02 23:11:09', 1);
INSERT INTO `conquer_account_login_record` VALUES (413, 3, '25.47.231.41', 0, '2024-07-09 22:15:15', 1);
INSERT INTO `conquer_account_login_record` VALUES (414, 3, '25.47.231.41', 0, '2024-07-09 22:15:38', 1);
INSERT INTO `conquer_account_login_record` VALUES (415, 3, '25.47.231.41', 0, '2024-07-09 22:16:25', 1);
INSERT INTO `conquer_account_login_record` VALUES (416, 3, '25.47.231.41', 0, '2024-07-09 22:17:04', 1);
INSERT INTO `conquer_account_login_record` VALUES (417, 1, '25.47.231.41', 0, '2024-07-09 22:27:56', 1);
INSERT INTO `conquer_account_login_record` VALUES (418, 1, '25.47.231.41', 0, '2024-07-09 22:29:07', 1);
INSERT INTO `conquer_account_login_record` VALUES (419, 0, '25.47.231.41', 0, '2024-07-09 22:29:18', 1);
INSERT INTO `conquer_account_login_record` VALUES (420, 0, '25.47.231.41', 0, '2024-07-09 22:30:56', 1);
INSERT INTO `conquer_account_login_record` VALUES (421, 1, '25.47.231.41', 0, '2024-07-09 22:32:20', 1);
INSERT INTO `conquer_account_login_record` VALUES (422, 2, '25.47.231.41', 0, '2024-07-09 22:33:11', 1);
INSERT INTO `conquer_account_login_record` VALUES (423, 3, '25.47.231.41', 0, '2024-07-11 15:55:15', 1);

-- ----------------------------
-- Table structure for conquer_account_vip
-- ----------------------------
DROP TABLE IF EXISTS `conquer_account_vip`;
CREATE TABLE `conquer_account_vip`  (
  `Id` int NOT NULL,
  `ConquerAccountId` int NULL DEFAULT NULL,
  `VipLevel` tinyint NULL DEFAULT NULL,
  `DurationMinutes` int NULL DEFAULT NULL,
  `StartDate` datetime(0) NOT NULL DEFAULT current_timestamp(0),
  `EndDate` datetime(0) NOT NULL,
  `CreationDate` datetime(0) NOT NULL DEFAULT current_timestamp(0),
  PRIMARY KEY (`Id`) USING BTREE
) ENGINE = InnoDB CHARACTER SET = utf8 COLLATE = utf8_general_ci ROW_FORMAT = DYNAMIC;

-- ----------------------------
-- Records of conquer_account_vip
-- ----------------------------

-- ----------------------------
-- Table structure for realm
-- ----------------------------
DROP TABLE IF EXISTS `realm`;
CREATE TABLE `realm`  (
  `RealmID` varchar(50) CHARACTER SET utf8 COLLATE utf8_general_ci NOT NULL,
  `RealmIdx` int NOT NULL,
  `Name` varchar(255) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL,
  `AuthorityID` int NULL DEFAULT 0,
  `GameIPAddress` varchar(255) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL,
  `RpcIPAddress` varchar(255) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL,
  `GamePort` int NULL DEFAULT 0,
  `RpcPort` int NULL DEFAULT 0,
  `Status` tinyint NULL DEFAULT NULL,
  `Username` varchar(255) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL,
  `Password` varchar(255) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL,
  `LastPing` datetime(0) NULL DEFAULT NULL,
  `DatabaseHost` varchar(255) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL,
  `DatabaseUser` varchar(255) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL,
  `DatabasePass` varchar(255) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL,
  `DatabaseSchema` varchar(255) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL,
  `DatabasePort` varchar(255) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL,
  `Active` bit(1) NULL DEFAULT b'1',
  `ProductionRealm` bit(1) NULL DEFAULT b'1',
  `Attribute` int NULL DEFAULT NULL,
  `MasterRealmID` int NULL DEFAULT NULL,
  `CrossPort` int NULL DEFAULT NULL,
  PRIMARY KEY (`RealmID`) USING BTREE
) ENGINE = InnoDB CHARACTER SET = utf8 COLLATE = utf8_general_ci ROW_FORMAT = DYNAMIC;

-- ----------------------------
-- Records of realm
-- ----------------------------
INSERT INTO `realm` VALUES ('94390aa0-c75d-11ed-9586-0050560401e2', 1, 'WarLord', 6, '25.47.231.41', '127.0.0.1', 5816, 9921, 1, '2vOQ/9KufSH7WkyTDiH0F0YB887vU+NuDyp97CKAW44=', 'KSNMdd6bh56v0M7iY0OZIAiL1fAPvdrpp+rzDwlP3cg=', '2024-04-03 01:37:07', 'localhost', 'root', '123', 'cq6135v2', '3306', b'1', b'0', 1, NULL, 9857);

-- ----------------------------
-- Table structure for realm_user
-- ----------------------------
DROP TABLE IF EXISTS `realm_user`;
CREATE TABLE `realm_user`  (
  `PlayerId` int NOT NULL AUTO_INCREMENT,
  `RealmId` int NULL DEFAULT NULL,
  `AccountId` int NULL DEFAULT NULL,
  `CreationDate` datetime(0) NULL DEFAULT NULL,
  PRIMARY KEY (`PlayerId`) USING BTREE
) ENGINE = InnoDB AUTO_INCREMENT = 19 CHARACTER SET = utf8 COLLATE = utf8_general_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Records of realm_user
-- ----------------------------
INSERT INTO `realm_user` VALUES (8, 1, 8, '2024-07-19 01:32:43');
INSERT INTO `realm_user` VALUES (9, 1, 9, '2024-07-19 01:58:06');
INSERT INTO `realm_user` VALUES (10, 1, 9, '2024-07-19 01:58:33');
INSERT INTO `realm_user` VALUES (11, 1, 9, '2024-07-19 02:01:17');
INSERT INTO `realm_user` VALUES (12, 1, 9, '2024-07-19 02:05:05');
INSERT INTO `realm_user` VALUES (13, 1, 9, '2024-07-19 02:09:44');
INSERT INTO `realm_user` VALUES (14, 1, 9, '2024-07-19 10:20:41');
INSERT INTO `realm_user` VALUES (15, 1, 10, '2024-07-19 13:24:16');
INSERT INTO `realm_user` VALUES (16, 1, 1000001, '2024-07-19 13:42:01');
INSERT INTO `realm_user` VALUES (17, 1, 1000002, '2024-07-19 14:08:07');
INSERT INTO `realm_user` VALUES (18, 1, 1000002, '2024-07-19 14:14:01');

SET FOREIGN_KEY_CHECKS = 1;
