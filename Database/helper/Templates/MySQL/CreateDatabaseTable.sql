﻿DROP TABLE IF EXISTS `{TABLE}`;
CREATE TABLE `{TABLE}` (
{CREATE_COLUMNS}
{CREATE_PRIMARAY_KEYS}
{CREATE_CONSTRAINTS}
) ENGINE=InnoDB AUTO_INCREMENT=10 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_bin;
