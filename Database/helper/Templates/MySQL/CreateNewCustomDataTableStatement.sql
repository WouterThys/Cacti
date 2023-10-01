USE `{SCHEMA}`;
CREATE TABLE `{DATA_NAME}s_customdata` (
  `id` int(11) NOT NULL,
  `code` varchar(45) DEFAULT NULL,
  `group` varchar(45) DEFAULT NULL,
  `description` varchar(255) DEFAULT NULL,
  `type` int(11) DEFAULT NULL,
  `label` varchar(45) DEFAULT NULL,
  PRIMARY KEY (`id`),
  UNIQUE KEY `id_UNIQUE` (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=10 DEFAULT CHARSET=latin1;