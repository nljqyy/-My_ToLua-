
CtrlNames = {
	Prompt = "PromptCtrl",
	Message = "MessageCtrl",
	Head ="HeadCtrl",
}
PanelNames=
{
	Head="Head"

}
--协议类型--
ProtocalType = {
	BINARY = 0,
	PB_LUA = 1,
	PBC = 2,
	SPROTO = 3,
}
--当前使用的协议类型--
TestProtoType = ProtocalType.PB_LUA;

Util = LuaFramework.Util;
AppConst = LuaFramework.AppConst;
LuaHelper = LuaFramework.LuaHelper;
ByteBuffer = LuaFramework.ByteBuffer;


resMgr = LuaHelper.GetResManager();
panelMgr = LuaHelper.GetPanelManager();
soundMgr = LuaHelper.GetSoundManager();
networkMgr = LuaHelper.GetNetManager();
xpageMgr=LuaHelper.GetXPageManager();
atlasMgr=LuaHelper.GetAtlasManager();

WWW = UnityEngine.WWW;
GameObject = UnityEngine.GameObject;