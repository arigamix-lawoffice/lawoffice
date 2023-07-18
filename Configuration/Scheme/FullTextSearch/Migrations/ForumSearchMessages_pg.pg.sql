DO $$
BEGIN
	IF NOT EXISTS ( SELECT NULL FROM pg_ts_config WHERE cfgname = 'arigamix') 
	THEN
		CREATE TEXT SEARCH CONFIGURATION public.arigamix ( COPY = pg_catalog.english );
	END IF;
END; $$
LANGUAGE PLPGSQL;