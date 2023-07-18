/*
 * Запрос выполняется только на PostgreSQL.
 * 
 * Скрипт удаляет все функции, сгенерированные для представлений Tessa,
 * а также очищает список этих представлений в CompiledViews
 */

SELECT proname, 
		"DropFunction"(proname) as cnt
FROM pg_proc 
INNER JOIN pg_namespace ns ON (pg_proc.pronamespace = ns.oid)
WHERE ns.nspname = 'public' AND proname like 'View_%'
GROUP BY proname;

DELETE FROM "CompiledViews";