USE `{SCHEMA}`;
CREATE TABLE `{DATA_NAME}s_logs` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `actionDate` datetime NOT NULL,
  `actionType` int(11) DEFAULT NULL,
  `actionBy` int(11) DEFAULT NULL,
  `actionOn` int(11) DEFAULT NULL,
  `objectId` int(11) NOT NULL,
  `objectCode` varchar(45) DEFAULT NULL,
  PRIMARY KEY (`id`,`actionDate`,`objectId`)
) ENGINE=InnoDB AUTO_INCREMENT=10 DEFAULT CHARSET=latin1;