﻿//this source code was auto-generated by tolua#, do not modify it
using System;
using LuaInterface;

public class EPageTypeWrap
{
	public static void Register(LuaState L)
	{
		L.BeginEnum(typeof(EPageType));
		L.RegVar("None", get_None, null);
		L.RegVar("Normal", get_Normal, null);
		L.RegVar("PopUp", get_PopUp, null);
		L.RegVar("Fixed", get_Fixed, null);
		L.RegVar("Toppest", get_Toppest, null);
		L.RegFunction("IntToEnum", IntToEnum);
		L.EndEnum();
		TypeTraits<EPageType>.Check = CheckType;
		StackTraits<EPageType>.Push = Push;
	}

	static void Push(IntPtr L, EPageType arg)
	{
		ToLua.Push(L, arg);
	}

	static bool CheckType(IntPtr L, int pos)
	{
		return TypeChecker.CheckEnumType(typeof(EPageType), L, pos);
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_None(IntPtr L)
	{
		ToLua.Push(L, EPageType.None);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_Normal(IntPtr L)
	{
		ToLua.Push(L, EPageType.Normal);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_PopUp(IntPtr L)
	{
		ToLua.Push(L, EPageType.PopUp);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_Fixed(IntPtr L)
	{
		ToLua.Push(L, EPageType.Fixed);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_Toppest(IntPtr L)
	{
		ToLua.Push(L, EPageType.Toppest);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int IntToEnum(IntPtr L)
	{
		int arg0 = (int)LuaDLL.lua_tonumber(L, 1);
		EPageType o = (EPageType)arg0;
		ToLua.Push(L, o);
		return 1;
	}
}

