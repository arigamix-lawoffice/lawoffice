﻿SELECT [AuthorID], [AuthorName]
FROM [DocumentCommonInfo] WITH(NOLOCK)
WHERE [ID] = #context_card_id
	#and_user_id_is([AuthorID])