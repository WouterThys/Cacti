USE `{SCHEMA}`;
DROP procedure IF EXISTS `{TABLE}Delete`;

DELIMITER $$
CREATE DEFINER=`root`@`localhost` PROCEDURE `{TABLE}Delete`(
	primaryKey int,
	foreignKey int
)
BEGIN
	DELETE FROM {TABLE} WHERE
{DELETE_LINK_VALUES}
END$$

DELIMITER ;