USE `{SCHEMA}`;
DROP procedure IF EXISTS `{TABLE}Insert`;

DELIMITER $$
CREATE DEFINER=`root`@`localhost` PROCEDURE `{TABLE}Insert`(
{INSERT_INPUT},
  OUT lid int
)
BEGIN

  INSERT INTO {TABLE}(	
{INSERT_VALUES},
	lastModified)
  VALUES(	
{INSERT_VALUES},
	now());
  SET lid = last_insert_id();

END$$

DELIMITER ;