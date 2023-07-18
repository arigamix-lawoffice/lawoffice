DO $$
DECLARE "Table" RECORD;
BEGIN
	FOR "Table" IN SELECT relname FROM pg_class WHERE relhastriggers AND NOT relname LIKE 'pg_%'
	LOOP
	  EXECUTE 'ALTER TABLE "' || "Table".relname || '" ENABLE TRIGGER ALL';
	END LOOP;
END $$;
