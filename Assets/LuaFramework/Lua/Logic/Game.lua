require "3rd/pblua/login_pb"
require "3rd/pbc/protobuf"

local lpeg = require "lpeg"

local json = require "cjson"
local util = require "3rd/cjson/util"

local sproto = require "3rd/sproto/sproto"
local core = require "sproto.core"
local print_r = require "3rd/sproto/print_r"

require "Logic/LuaClass"
require "Logic/CtrlManager"
require "Common/functions"
require "Controller/PromptCtrl"

--管理器--
Game = {};
local this = Game;

local game; 
local transform;
local gameObject;
local WWW = UnityEngine.WWW;

function Game.InitViewPanels()
	for i = 1, #PanelNames do
		require ("View/"..tostring(PanelNames[i]))
	end
end

--初始化完成，发送链接服务器信息--
function Game.OnInitOK()
    AppConst.SocketPort = 1234;
    AppConst.SocketAddress = "127.0.0.1";
    networkMgr:SendConnect();

    --注册LuaView--
    this.InitViewPanels();
    --this.test_pblua_func();
    --coroutine.start(this.test_coroutine);

    CtrlManager.Init();
    local ctrl = CtrlManager.GetCtrl(CtrlNames.Prompt);
    if ctrl ~= nil and AppConst.ExampleMode == 1 then
       ctrl:Awake();
    end
       
    logWarn('LuaFramework InitOK--->>>');
end

--测试协同--
function Game.test_coroutine()    
    logWarn("1111");
    coroutine.wait(1);	
    logWarn("2222");
	
    local www = WWW("http://bbs.ulua.org/readme.txt");
    coroutine.www(www);
    logWarn(www.text);    	
end



--测试pblua--
function Game.test_pblua_func()
    local login = login_pb.LoginRequest();
    login.id = 2000;
    login.name = 'game';
    login.email = 'jarjin@163.com';
    
    local msg = login:SerializeToString();
    LuaHelper.OnCallLuaFunc(msg, this.OnPbluaCall);
end

--pblua callback--
function Game.OnPbluaCall(data)
    local msg = login_pb.LoginRequest();
    msg:ParseFromString(data);
    print(msg);
    print(msg.id..' '..msg.name);
end

--销毁--
function Game.OnDestroy()
	--logWarn('OnDestroy--->>>');
end
