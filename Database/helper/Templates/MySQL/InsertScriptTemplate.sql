  INSERT INTO {TABLE}(	
{INSERT_INPUTS},
	lastModified)
  VALUES(	
{INSERT_VALUES},
	now());
  SELECT last_insert_id();