USE `{SCHEMA}`;
CREATE TABLE `{DATA_NAME}s` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `sourceId` int(11) DEFAULT NULL,
  `sourceKey` varchar(45) DEFAULT NULL,
  `code` varchar(45) NOT NULL DEFAULT '',
  `deleted` tinyint(1) DEFAULT '0',
  `enabled` tinyint(1) DEFAULT '1',
  `showOnTerminal` tinyint(1) DEFAULT '1',
  `lastModified` timestamp NULL DEFAULT CURRENT_TIMESTAMP,
  `iconId` int(11) DEFAULT NULL,
  `description` varchar(255) DEFAULT '',
  `info` varchar(1023) DEFAULT '',
  `flagId` int(11) DEFAULT '0',
  PRIMARY KEY (`id`),
  UNIQUE KEY `code_UNIQUE` (`code`),
  KEY `fk_{DATA_NAME}s_icon_idx` (`iconId`),
  CONSTRAINT `fk_{DATA_NAME}s_icon` FOREIGN KEY (`iconId`) REFERENCES `documents` (`id`) ON DELETE SET NULL ON UPDATE CASCADE
) ENGINE=InnoDB AUTO_INCREMENT=10 DEFAULT CHARSET=latin1;