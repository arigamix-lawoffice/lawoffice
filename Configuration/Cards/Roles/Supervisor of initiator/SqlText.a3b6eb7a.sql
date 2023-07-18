SELECT TOP(1)
  [dr].[HeadUserID],
  [dr].[HeadUserName]
FROM [DepartmentRoles] AS [dr] WITH(NOLOCK)
INNER JOIN [RoleUsers] AS [ru] WITH(NOLOCK) ON [ru].[ID] = [dr].[ID]
INNER JOIN [KrApprovalCommonInfo] AS [kr] WITH(NOLOCK) ON [kr].[AuthorID] = [ru].[UserID]
WHERE [dr].[HeadUserID] IS NOT NULL
  AND [kr].[MainCardID] = #context_card_id
  #and_user_id_is([dr].[HeadUserID])