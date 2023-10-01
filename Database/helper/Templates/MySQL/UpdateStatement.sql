USE `{SCHEMA}`;
DROP procedure IF EXISTS `{TABLE}Update`;

DELIMITER $$
CREATE DEFINER=`root`@`localhost` PROCEDURE `{TABLE}Update`(
{UPDATE_INPUT},
  updateId int
)
BEGIN

  UPDATE
	{TABLE}
  SET
{UPDATE_VALUES},
	lastModified = current_timestamp()
  WHERE
	id = updateId;

END$$

DELIMITER ;