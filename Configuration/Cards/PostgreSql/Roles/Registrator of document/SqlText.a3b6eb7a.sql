SELECT "RegistratorID", "RegistratorName"
FROM "DocumentCommonInfo"
WHERE "ID" = #context_card_id
  #and_user_id_is("RegistratorID")