SELECT [CreatedByID], [CreatedByName]
FROM [Instances] WITH(NOLOCK)
WHERE [ID] = #context_card_id
  #and_user_id_is([CreatedByID])