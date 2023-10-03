DROP procedure IF EXISTS `photosById`;

DELIMITER $$
CREATE DEFINER=`root`@`localhost` PROCEDURE `photosById`(
	id int
)
BEGIN 
	 SELECT 
		*
	 FROM
		photos
	 WHERE
		photos.id = id;
END$$

DELIMITER ;