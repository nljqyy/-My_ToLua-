EventCenter = {};   --事件注册
 
function EventCenter.AddListener(eventType, func)
	if EventCenter[eventType] == nil then
		local a = {};
		table.insert(a, func);
		EventCenter[eventType] = a;
	else
		table.insert(EventCenter[eventType], func);
	end
end
 
function EventCenter.RemoveListener(eventType, func)
	local a = EventCenter[eventType];
	if a ~= nil then
		for k, v in pairs(a) do
			if(v == func) then a[k] = nil; end
		end
	end
end
 
function EventCenter.Dispatch(eventType, ...)
	local a = EventCenter[eventType];
	for k, v in pairs(a) do
		v(...);
	end
end
