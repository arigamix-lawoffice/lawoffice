CREATE FUNCTION [GetAggregateRoleUsers]()
RETURNS TABLE 
AS
RETURN
(
    WITH
        [RoleUsersCTE] ([ID], [RoleID], [RoleName], [CalendarID], [TimeZoneID]) AS
        (
            -- выбираем статические роли и департаменты,
            -- которые либо не являются листовыми,
            -- либо для которых уже была создана метароль генератором агрегатных ролей
            SELECT [r].[ID],
                   [r].[ID] AS [RoleID],
                   [r].[Name] + N' (все)' AS [RoleName],
                   [r].[CalendarID],
                   [r].[TimeZoneID]
            FROM [Roles] AS [r] WITH (NOLOCK)
            LEFT JOIN [MetaRoles] AS [mr] WITH (NOLOCK)
                ON [mr].[GeneratorID] = '{3E832ED8-B3D6-0ACD-9524-6322A7231FEF}'
                AND [mr].[IDGuid] = [r].[ID]
            WHERE [r].[TypeID] IN (0, 2)
                AND [r].[Hidden] = 0
                AND ([mr].[GeneratorID] IS NOT NULL OR EXISTS (SELECT 1 FROM [Roles] AS [r2] WITH (NOLOCK) WHERE [r2].[ParentID] = [r].[ID] AND [r2].[TypeID] != 8))
            UNION ALL
            SELECT [r].[ID],
                   [t].[RoleID],
                   [t].[RoleName],                   
                   [t].[CalendarID],
                   [t].[TimeZoneID]
            FROM [Roles] AS [r] WITH (NOLOCK)
            INNER JOIN [RoleUsersCTE] AS [t]
                ON [r].[ParentID] = [t].[ID]
            WHERE [r].[TypeID] != 8
        )
    SELECT DISTINCT
        [r].[RoleID],
        [r].[RoleName],
        [ru].[UserID],
        [ru].[UserName],        
        [r].[CalendarID],
        [r].[TimeZoneID]
    FROM [RoleUsersCTE] AS [r]
    LEFT JOIN [RoleUsers] AS [ru] WITH (NOLOCK)
        ON [ru].[ID] = [r].[ID]
)