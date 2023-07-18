/*
 * Запрос выполняется только на PostgreSQL.
 *
 * Скрипт для проверки внешних ключей. Выдает только одну ошибку за раз.
 *
 * Может выполняться длительное время.
 */

do $$
  declare r record;
BEGIN
FOR r IN  (
  SELECT FORMAT(
    'UPDATE pg_constraint SET convalidated=false WHERE conname = ''%s''; ALTER TABLE %I VALIDATE CONSTRAINT %I;',
    tc.constraint_name,
    tc.table_name,
    tc.constraint_name
  ) AS x
  FROM information_schema.table_constraints AS tc
  JOIN information_schema.tables t ON t.table_name = tc.table_name and t.table_type = 'BASE TABLE'
  JOIN information_schema.key_column_usage AS kcu ON tc.constraint_name = kcu.constraint_name
  JOIN information_schema.constraint_column_usage AS ccu ON ccu.constraint_name = tc.constraint_name
  WHERE  constraint_type = 'FOREIGN KEY'
    AND tc.constraint_schema = 'public'
)
  LOOP
    EXECUTE (r.x);
  END LOOP;
END;
$$;