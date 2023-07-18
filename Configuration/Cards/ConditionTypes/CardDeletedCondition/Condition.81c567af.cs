#using Tessa.Roles.Triggers;

return context.StoreCard?.Info.TryGet<bool?>(TriggersHelper.CardDeletedTriggerMark) == true;