SELECT "CreatedByID", "CreatedByName"
FROM "Instances"
WHERE "ID" = #context_card_id
  #and_user_id_is("CreatedByID")