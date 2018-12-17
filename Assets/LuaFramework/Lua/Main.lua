
require "Common/define"
require "Common/functions"
require "Common/protocal"


--主入口函数。从这里开始lua逻辑
function Main()					
	print("logic start")	
	--resMgr:LoadPrefab("ui",{"head"},OnLoadFinished);
end



function OnLoadFinished(objs)
 local go=newObject(objs[0]);
 local canvas=GameObject.Find('Canvas')
 
  go.transform.parent=canvas.transform;
  go.transform.localPosition=Vector3.zero;
  go.name="New head"
  listener= EventTriggerListener.GetListener(go)
  listener.onPointerClick = listener.onPointerClick + Click;
end

function Click(o,data)
    log("Click的物体"..o.name) ;
end

function MoveOk()
 log('移动完成')
end

--场景切换通知
function OnLevelWasLoaded(level)
	collectgarbage("collect")
	Time.timeSinceLevelLoad = 0
end

function OnApplicationQuit()
end

