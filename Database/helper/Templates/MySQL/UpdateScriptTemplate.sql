
  UPDATE
	{TABLE}
  SET
{UPDATE_VALUES},
	lastModified = current_timestamp()
  WHERE
	id = @updateId;