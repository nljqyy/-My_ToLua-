using UnityEngine;
using System.Collections;
using LuaFramework;

public class StartUpCommand : ControllerCommand {

    public override void Execute(IMessage message) {

        //首先检查运行环境,这个函数只在UNITY_EDITOR下有用
        //其他时候都是直接返回true的
        if (!Util.CheckEnvironment()) return;

        //如果需要显示AppView信息,需要在场景上添加GlobalGenerator对象
        //AppView里面只是显示一些消息
        GameObject gameMgr = GameObject.Find("GlobalGenerator");
        if (gameMgr != null) {
            AppView appView = gameMgr.AddComponent<AppView>();
        }
        //-----------------关联命令-----------------------
        //SocketCommand处理网关相关指令  socket消息
        AppFacade.Instance.RegisterCommand(NotiConst.DISPATCH_MESSAGE, typeof(SocketCommand));

        //-----------------初始化管理器-----------------------
        AppFacade.Instance.AddManager<LuaManager>(ManagerName.Lua);
        AppFacade.Instance.AddManager<PanelManager>(ManagerName.Panel);
        AppFacade.Instance.AddManager<SoundManager>(ManagerName.Sound);
        AppFacade.Instance.AddManager<TimerManager>(ManagerName.Timer);
        AppFacade.Instance.AddManager<NetworkManager>(ManagerName.Network);
        AppFacade.Instance.AddManager<ResourceManager>(ManagerName.Resource);
        AppFacade.Instance.AddManager<ThreadManager>(ManagerName.Thread);
        AppFacade.Instance.AddManager<ObjectPoolManager>(ManagerName.ObjectPool);
        AppFacade.Instance.AddManager<XPageManager>(ManagerName.XPage);//UI管理器
        AppFacade.Instance.AddManager<AtlasManager>(ManagerName.Atlas);//UI管理器
        //游戏下载资源,解包都在GameManager内完成
        AppFacade.Instance.AddManager<GameManager>(ManagerName.Game);
       
    }
}