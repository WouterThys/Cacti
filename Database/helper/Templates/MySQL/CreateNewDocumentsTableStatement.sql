﻿USE `{SCHEMA}`;
CREATE TABLE `{DATA_NAME}s_documents` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `{DATA_NAME}Id` int(11) DEFAULT NULL,
  `documentId` int(11) DEFAULT NULL,
  PRIMARY KEY (`id`),
  KEY `fk_{DATA_NAME}s_documents_idx` (`{DATA_NAME}Id`),
  KEY `fk_{DATA_NAME}s_documents_idx1` (`documentId`),
  CONSTRAINT `fk_{DATA_NAME}s_documents` FOREIGN KEY (`documentId`) REFERENCES `documents` (`id`) ON DELETE CASCADE ON UPDATE CASCADE,
  CONSTRAINT `fk_{DATA_NAME}s_id` FOREIGN KEY (`{DATA_NAME}Id`) REFERENCES `{DATA_NAME}s` (`id`) ON DELETE CASCADE ON UPDATE CASCADE
) ENGINE=InnoDB AUTO_INCREMENT=10 DEFAULT CHARSET=latin1;