SELECT #distinct [UserID], [UserName] FROM [AcquaintanceRows] WITH(NOLOCK)
WHERE #context_card_id = [CardID] #and_user_id_is(UserID)
