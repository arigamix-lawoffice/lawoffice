SELECT
  "dr"."HeadUserID",
  "dr"."HeadUserName"
FROM "DepartmentRoles" AS "dr"
INNER JOIN "RoleUsers" AS "ru" ON "ru"."ID" = "dr"."ID"
INNER JOIN "KrApprovalCommonInfo" AS "kr" ON "kr"."AuthorID" = "ru"."UserID"
WHERE "dr"."HeadUserID" IS NOT NULL
  AND "kr"."MainCardID" = #context_card_id
  #and_user_id_is("dr"."HeadUserID")
LIMIT 1