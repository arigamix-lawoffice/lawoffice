-- Скрипт требуется выполнить перед обновлением схемы на 3.2

ALTER INDEX IF EXISTS "ndx_WorkflowEngineCommandSubscriptions_CommandNodeRowIDProcessR"
RENAME TO "ndx_WorkflowEngineCommandSubscriptions_CommandNodeRowI_71d84cd7";
GO

CREATE OR REPLACE FUNCTION rename_constraint_if_exists (
    t_name text,
    old_c_name text,
    new_c_name text
)
RETURNS void AS
$$
BEGIN
IF EXISTS (
    SELECT NULL
    FROM information_schema.table_constraints
    WHERE table_name = t_name
        AND constraint_name = old_c_name
) THEN
    EXECUTE 'ALTER TABLE "' || t_name || '" RENAME CONSTRAINT "' || old_c_name || '" TO "' || new_c_name || '"';
END IF;
END;
$$ LANGUAGE 'plpgsql';
GO

SELECT
    rename_constraint_if_exists(
        'WorkflowEngineSubprocessSubscriptions',
        'fk_WorkflowEngineSubprocessSubscriptions_WorkflowEngineNodes_No',
        'fk_WorkflowEngineSubprocessSubscriptions_WorkflowEngin_b4aac806') AS "1",
    rename_constraint_if_exists(
        'WorkflowEngineSettingsObjectTypeFields',
        'fk_WorkflowEngineSettingsObjectTypeFields_WorkflowEngineSetting',
        'fk_WorkflowEngineSettingsObjectTypeFields_WorkflowEngi_daf48027') AS "2";
GO
--NORESULT
DROP FUNCTION rename_constraint_if_exists(text, text, text);
GO
