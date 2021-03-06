﻿//this source code was auto-generated by tolua#, do not modify it
using System;
using LuaInterface;

public class LuaFramework_SoundManagerWrap
{
	public static void Register(LuaState L)
	{
		L.BeginClass(typeof(LuaFramework.SoundManager), typeof(Manager));
		L.RegFunction("PlayBackSound", PlayBackSound);
		L.RegFunction("StopBackSound", StopBackSound);
		L.RegFunction("PlaySound", PlaySound);
		L.RegFunction("StopSound", StopSound);
		L.RegFunction("__eq", op_Equality);
		L.RegFunction("__tostring", ToLua.op_ToString);
		L.EndClass();
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int PlayBackSound(IntPtr L)
	{
		try
		{
			int count = LuaDLL.lua_gettop(L);

			if (count == 3)
			{
				LuaFramework.SoundManager obj = (LuaFramework.SoundManager)ToLua.CheckObject<LuaFramework.SoundManager>(L, 1);
				string arg0 = ToLua.CheckString(L, 2);
				string arg1 = ToLua.CheckString(L, 3);
				obj.PlayBackSound(arg0, arg1);
				return 0;
			}
			else if (count == 4)
			{
				LuaFramework.SoundManager obj = (LuaFramework.SoundManager)ToLua.CheckObject<LuaFramework.SoundManager>(L, 1);
				string arg0 = ToLua.CheckString(L, 2);
				string arg1 = ToLua.CheckString(L, 3);
				float arg2 = (float)LuaDLL.luaL_checknumber(L, 4);
				obj.PlayBackSound(arg0, arg1, arg2);
				return 0;
			}
			else
			{
				return LuaDLL.luaL_throw(L, "invalid arguments to method: LuaFramework.SoundManager.PlayBackSound");
			}
		}
		catch (Exception e)
		{
			return LuaDLL.toluaL_exception(L, e);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int StopBackSound(IntPtr L)
	{
		try
		{
			ToLua.CheckArgsCount(L, 1);
			LuaFramework.SoundManager obj = (LuaFramework.SoundManager)ToLua.CheckObject<LuaFramework.SoundManager>(L, 1);
			obj.StopBackSound();
			return 0;
		}
		catch (Exception e)
		{
			return LuaDLL.toluaL_exception(L, e);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int PlaySound(IntPtr L)
	{
		try
		{
			int count = LuaDLL.lua_gettop(L);

			if (count == 3)
			{
				LuaFramework.SoundManager obj = (LuaFramework.SoundManager)ToLua.CheckObject<LuaFramework.SoundManager>(L, 1);
				string arg0 = ToLua.CheckString(L, 2);
				string arg1 = ToLua.CheckString(L, 3);
				obj.PlaySound(arg0, arg1);
				return 0;
			}
			else if (count == 4)
			{
				LuaFramework.SoundManager obj = (LuaFramework.SoundManager)ToLua.CheckObject<LuaFramework.SoundManager>(L, 1);
				string arg0 = ToLua.CheckString(L, 2);
				string arg1 = ToLua.CheckString(L, 3);
				float arg2 = (float)LuaDLL.luaL_checknumber(L, 4);
				obj.PlaySound(arg0, arg1, arg2);
				return 0;
			}
			else
			{
				return LuaDLL.luaL_throw(L, "invalid arguments to method: LuaFramework.SoundManager.PlaySound");
			}
		}
		catch (Exception e)
		{
			return LuaDLL.toluaL_exception(L, e);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int StopSound(IntPtr L)
	{
		try
		{
			ToLua.CheckArgsCount(L, 3);
			LuaFramework.SoundManager obj = (LuaFramework.SoundManager)ToLua.CheckObject<LuaFramework.SoundManager>(L, 1);
			string arg0 = ToLua.CheckString(L, 2);
			string arg1 = ToLua.CheckString(L, 3);
			obj.StopSound(arg0, arg1);
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
}

