require "Common/define"

HeadPanel={}
local this=HeadPanel;

local gameObject;
local transform;

function HeadPanel.Start(obj)
    gameObject=obj;
    transform=obj.transform;
    this.btn=obj;
end