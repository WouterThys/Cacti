INSERT IGNORE INTO 
	updatescripts (
		`id`,
		`majorVersion`,
		`minorVersion`,
		`buildVersion`,
		`script`,
		`description`) 
VALUES (
	{UPDATEVALUES}
	);