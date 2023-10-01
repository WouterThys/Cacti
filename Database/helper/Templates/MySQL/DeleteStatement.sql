USE `{SCHEMA}`;
DROP procedure IF EXISTS `{TABLE}Delete`;

DELIMITER $$
CREATE DEFINER=`root`@`localhost` PROCEDURE `{TABLE}Delete`(
  deleteId int(11)
)
BEGIN
 DELETE FROM {TABLE} WHERE id = deleteId;
END$$

DELIMITER ;