﻿//this source code was auto-generated by tolua#, do not modify it
using System;
using LuaInterface;

public class XPageManagerWrap
{
	public static void Register(LuaState L)
	{
		L.BeginClass(typeof(XPageManager), typeof(Manager));
		L.RegFunction("GetNeedBackCount", GetNeedBackCount);
		L.RegFunction("ShowPage", ShowPage);
		L.RegFunction("HideCurrPage", HideCurrPage);
		L.RegFunction("HidePage", HidePage);
		L.RegFunction("CloseAllPages", CloseAllPages);
		L.RegFunction("Destroy", Destroy);
		L.RegFunction("__eq", op_Equality);
		L.RegFunction("__tostring", ToLua.op_ToString);
		L.RegVar("currShowXPage", get_currShowXPage, set_currShowXPage);
		L.RegVar("m_pageNeedBackPool", get_m_pageNeedBackPool, set_m_pageNeedBackPool);
		L.RegVar("m_pageDic", get_m_pageDic, set_m_pageDic);
		L.EndClass();
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetNeedBackCount(IntPtr L)
	{
		try
		{
			ToLua.CheckArgsCount(L, 1);
			XPageManager obj = (XPageManager)ToLua.CheckObject(L, 1, typeof(XPageManager));
			int o = obj.GetNeedBackCount();
			LuaDLL.lua_pushinteger(L, o);
			return 1;
		}
		catch (Exception e)
		{
			return LuaDLL.toluaL_exception(L, e);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int ShowPage(IntPtr L)
	{
		try
		{
			int count = LuaDLL.lua_gettop(L);

			if (count == 2)
			{
				XPageManager obj = (XPageManager)ToLua.CheckObject(L, 1, typeof(XPageManager));
				string arg0 = ToLua.CheckString(L, 2);
				obj.ShowPage(arg0);
				return 0;
			}
			else if (count == 3)
			{
				XPageManager obj = (XPageManager)ToLua.CheckObject(L, 1, typeof(XPageManager));
				bool arg0 = LuaDLL.luaL_checkboolean(L, 2);
				string arg1 = ToLua.CheckString(L, 3);
				obj.ShowPage(arg0, arg1);
				return 0;
			}
			else
			{
				return LuaDLL.luaL_throw(L, "invalid arguments to method: XPageManager.ShowPage");
			}
		}
		catch (Exception e)
		{
			return LuaDLL.toluaL_exception(L, e);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int HideCurrPage(IntPtr L)
	{
		try
		{
			ToLua.CheckArgsCount(L, 1);
			XPageManager obj = (XPageManager)ToLua.CheckObject(L, 1, typeof(XPageManager));
			bool o = obj.HideCurrPage();
			LuaDLL.lua_pushboolean(L, o);
			return 1;
		}
		catch (Exception e)
		{
			return LuaDLL.toluaL_exception(L, e);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int HidePage(IntPtr L)
	{
		try
		{
			ToLua.CheckArgsCount(L, 2);
			XPageManager obj = (XPageManager)ToLua.CheckObject(L, 1, typeof(XPageManager));
			string arg0 = ToLua.CheckString(L, 2);
			obj.HidePage(arg0);
			return 0;
		}
		catch (Exception e)
		{
			return LuaDLL.toluaL_exception(L, e);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int CloseAllPages(IntPtr L)
	{
		try
		{
			ToLua.CheckArgsCount(L, 1);
			XPageManager obj = (XPageManager)ToLua.CheckObject(L, 1, typeof(XPageManager));
			obj.CloseAllPages();
			return 0;
		}
		catch (Exception e)
		{
			return LuaDLL.toluaL_exception(L, e);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int Destroy(IntPtr L)
	{
		try
		{
			ToLua.CheckArgsCount(L, 1);
			XPageManager obj = (XPageManager)ToLua.CheckObject(L, 1, typeof(XPageManager));
			obj.Destroy();
			return 0;
		}
		catch (Exception e)
		{
			return LuaDLL.toluaL_exception(L, e);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int op_Equality(IntPtr L)
	{
		try
		{
			ToLua.CheckArgsCount(L, 2);
			UnityEngine.Object arg0 = (UnityEngine.Object)ToLua.ToObject(L, 1);
			UnityEngine.Object arg1 = (UnityEngine.Object)ToLua.ToObject(L, 2);
			bool o = arg0 == arg1;
			LuaDLL.lua_pushboolean(L, o);
			return 1;
		}
		catch (Exception e)
		{
			return LuaDLL.toluaL_exception(L, e);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_currShowXPage(IntPtr L)
	{
		object o = null;

		try
		{
			o = ToLua.ToObject(L, 1);
			XPageManager obj = (XPageManager)o;
			XPage ret = obj.currShowXPage;
			ToLua.PushSealed(L, ret);
			return 1;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e, o, "attempt to index currShowXPage on a nil value");
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_m_pageNeedBackPool(IntPtr L)
	{
		object o = null;

		try
		{
			o = ToLua.ToObject(L, 1);
			XPageManager obj = (XPageManager)o;
			System.Collections.Generic.Stack<XPage> ret = obj.m_pageNeedBackPool;
			ToLua.PushObject(L, ret);
			return 1;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e, o, "attempt to index m_pageNeedBackPool on a nil value");
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_m_pageDic(IntPtr L)
	{
		object o = null;

		try
		{
			o = ToLua.ToObject(L, 1);
			XPageManager obj = (XPageManager)o;
			System.Collections.Generic.Dictionary<string,XPage> ret = obj.m_pageDic;
			ToLua.PushSealed(L, ret);
			return 1;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e, o, "attempt to index m_pageDic on a nil value");
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_currShowXPage(IntPtr L)
	{
		object o = null;

		try
		{
			o = ToLua.ToObject(L, 1);
			XPageManager obj = (XPageManager)o;
			XPage arg0 = (XPage)ToLua.CheckObject(L, 2, typeof(XPage));
			obj.currShowXPage = arg0;
			return 0;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e, o, "attempt to index currShowXPage on a nil value");
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_m_pageNeedBackPool(IntPtr L)
	{
		object o = null;

		try
		{
			o = ToLua.ToObject(L, 1);
			XPageManager obj = (XPageManager)o;
			System.Collections.Generic.Stack<XPage> arg0 = (System.Collections.Generic.Stack<XPage>)ToLua.CheckObject<System.Collections.Generic.Stack<XPage>>(L, 2);
			obj.m_pageNeedBackPool = arg0;
			return 0;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e, o, "attempt to index m_pageNeedBackPool on a nil value");
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_m_pageDic(IntPtr L)
	{
		object o = null;

		try
		{
			o = ToLua.ToObject(L, 1);
			XPageManager obj = (XPageManager)o;
			System.Collections.Generic.Dictionary<string,XPage> arg0 = (System.Collections.Generic.Dictionary<string,XPage>)ToLua.CheckObject(L, 2, typeof(System.Collections.Generic.Dictionary<string,XPage>));
			obj.m_pageDic = arg0;
			return 0;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e, o, "attempt to index m_pageDic on a nil value");
		}
	}
}

