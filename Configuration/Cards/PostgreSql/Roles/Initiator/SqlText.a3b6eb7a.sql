SELECT "kr"."AuthorID", "kr"."AuthorName"
FROM "KrApprovalCommonInfo" AS "kr"
WHERE "kr"."MainCardID" = #context_card_id
  #and_user_id_is("kr"."AuthorID")