require "Common/define"

CtrlManager = {};
local this = CtrlManager;
local ctrlList = {};	--控制器列表--

function CtrlManager.InitPanelCtr()
	for i, v in pairs(CtrlNames) do
		require ("Controller/"..tostring(v))
	end
end


function CtrlManager.Init()
	logWarn("CtrlManager.Init----->>>");
	this.InitPanelCtr();
	ctrlList[CtrlNames.Head] = HeadCtrl.New();
	return this;
end

--添加控制器--
function CtrlManager.AddCtrl(ctrlName, ctrlObj)
	ctrlList[ctrlName] = ctrlObj;
end

--获取控制器--
function CtrlManager.GetCtrl(ctrlName)
	return ctrlList[ctrlName];
end

--移除控制器--
function CtrlManager.RemoveCtrl(ctrlName)
	ctrlList[ctrlName] = nil;
end

--关闭控制器--
function CtrlManager.Close()
	logWarn('CtrlManager.Close---->>>');
end