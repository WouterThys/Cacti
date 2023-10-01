USE `{SCHEMA}`;
DROP procedure IF EXISTS `{TABLE}Insert`;

DELIMITER $$
CREATE DEFINER=`root`@`localhost` PROCEDURE `{TABLE}Insert`(
	primaryKey int(11),
	foreignKey int(11),
	OUT lid int
)
BEGIN

	INSERT INTO {TABLE}(
{INSERT_LINK_INPUT})
	VALUES (
		primaryKey,
		foreignKey);
	SET lid = last_insert_id();

END$$

DELIMITER ;