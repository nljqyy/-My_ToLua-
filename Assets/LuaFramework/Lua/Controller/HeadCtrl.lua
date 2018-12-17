require "Common/define"

HeadCtrl = {};
local this = HeadCtrl;

--构建函数--
function HeadCtrl.New()
    return this;
end

function HeadCtrl.Awake(xPage)
    log("HeadCtr.Awake")
  xPage.m_pageType=EPagePosType.Normal;
  xPage.m_pageMode=EPageHideMode.DoNothing;
end

function HeadCtrl.Start()
    listener= EventTriggerListener.GetListener(HeadPanel.btn)
    listener.onPointerClick = listener.onPointerClick + Click;

end

function HeadCtrl.Reset()

end

function HeadCtrl.Hide()
  log("自己隐藏啦")

end

function HeadCtrl.Destroy()

end

function Click(o,data)
    log("Click的物体"..o.name) ;
    xpageMgr:HideCurrPage();
end

