using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System;
using System.IO;
using System.Linq;

//打包图集
public sealed class PackerAtlas : MonoBehaviour {

    /// 图集图片的路径
    public static string AtlasPath = "Assets/SpriteSheet";

    /// 打包后预制体存放路径
    public static string AtlasPrefabPath = "Assets/Prefabs/Atlas/";

    [MenuItem("Tools/打包所有图集")]
    public static void StartPackerAtlas()
    {
        string[] _patterns = { "*.png"};
        Dictionary<string, List<string>> _allFilePaths = new Dictionary<string, List<string>>();
        string _tempName = string.Empty;
        foreach (var item in _patterns)
        {
            string[] _temp = Directory.GetFiles(AtlasPath, item, SearchOption.AllDirectories);
            foreach (var path in _temp)
            {
                FileInfo _fi = new FileInfo(path);
                _tempName = _fi.Name.Replace(_fi.Extension, "").ToLower();
                _tempName = "atl_" + _tempName;
                if (!_allFilePaths.ContainsKey(_tempName))
                    _allFilePaths.Add(_tempName,new List<string>());
                _allFilePaths[_tempName].Add(path);
            }
        }
        foreach (var item in _allFilePaths)
        {
            CreateAtlasPrefab(item.Key, item.Value.ToArray());
        }
    }

    private static void CreateAtlasPrefab(string atlasName,string[] atlPath)
    {
        List<Sprite> splist = new List<Sprite>();
        foreach (var path in atlPath)
        {
            splist.AddRange(AssetDatabase.LoadAllAssetsAtPath(path).OfType<Sprite>().ToArray());
        }

        if (splist.Count>0)
        {
            GameObject go = new GameObject(atlasName);
            SpriteData spdata=go.AddComponent<SpriteData>();
            spdata.SetSP = splist.ToArray();
            string path1 = AtlasPrefabPath + atlasName + ".prefab";
            GameObject temp = PrefabUtility.CreatePrefab(path1, go);

            #region 添加ab标记
            //此处自动添加ab标记
            //如果加载方式是Resources.load()等不需要ab标记的可以把此处注释掉
            //AssetImporter importer = AssetImporter.GetAtPath(path1);
            //if (importer == null || temp == null)
            //{
            //    Debug.LogError("error: " + path1);
            //    return;
            //}
            //importer.assetBundleName = "ui-share.unity3d";

            #endregion


            GameObject.DestroyImmediate(go);
            EditorUtility.SetDirty(temp);
            AssetDatabase.SaveAssets();
                            
         }
        Resources.UnloadUnusedAssets();
        AssetDatabase.Refresh();
        Debug.Log("****图集创建成功****");
    }


}
