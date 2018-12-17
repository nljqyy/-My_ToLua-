using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LuaFramework;
using System;

public sealed class AtlasManager : Manager
{
    //key 图集名称  value 图集数据
    private Dictionary<string, SpriteData> spDic = new Dictionary<string, SpriteData>();

    public void LoadSprite(string atlName, string spName,Action<Sprite> act)
    {
        if (!spDic.ContainsKey(atlName))
        {
            ResManager.LoadPrefab("atlas", atlName, objs =>
            {
                if (objs.Length == 0) return;
                GameObject prefab = objs[0] as GameObject;
                if (prefab == null) return;
                spDic.Add(atlName, prefab.GetComponent<SpriteData>());
                if (act != null) act(spDic[atlName].GetSprite(spName));
            });
        }
        else
           if (act != null) act(spDic[atlName].GetSprite(spName));
    }
}
