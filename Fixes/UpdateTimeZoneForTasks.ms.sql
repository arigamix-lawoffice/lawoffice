declare @ZoneID int;
set @ZoneID = 0;

update [Tasks]
set 
	[TimeZoneID] = t.[ID],
	[TimeZoneUtcOffsetMinutes] = t.[UtcOffsetMinutes]
from 
	(select 
		tz.[ID], 
		tz.[UtcOffsetMinutes]
	from [TimeZones] tz with(nolock)
	where tz.[ID] = @ZoneID
	union all
	select 
		dz.[ZoneID] as [ID], 
		dz.[UtcOffsetMinutes]
	from [DefaultTimeZone] dz with(nolock)
	where dz.[ZoneID] = @ZoneID) t
