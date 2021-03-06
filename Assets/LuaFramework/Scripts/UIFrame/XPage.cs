﻿using System;
using UnityEngine;
using System.Collections;
using Object = UnityEngine.Object;
using LuaFramework;

//面板 父类
public enum EPagePosType
{
    None,
    Normal,
    PopUp,
    Fixed,
    Toppest,
}
//面板隐藏 模式
public enum EPageHideMode
{
    DoNothing,
    HideOtherOnly,
    HideOtherAndNeedBack,
}

//面板状态
public enum EPageState
{
    NONE,
    OPEN, //打开
    HIDE, //隐藏
    CLOSE,//销毁
}

public sealed class XPageLoadBind
{
    /// <summary>
    /// 绑定你自己的资源加载管理器
    /// </summary>
    /// <param name="xPage"></param>
    public static void Bind(XPage xPage)
    {
        xPage.delegateSyncLoadUI = Resources.Load;
        //xPage.delegateAsyncLoadUI = ResourcesMgr.Load;
    }
}

public sealed class XPage
{
    public string m_pageName;
    public string m_loadPath;
    public GameObject m_pageInst;
    public Transform m_pageTrans;
    public EPagePosType m_pageType = EPagePosType.None;
    public EPageHideMode m_pageMode = EPageHideMode.DoNothing;
    public EPageState m_currState = EPageState.NONE;

    private string m_luaPageCtrl;
    private string m_luaPagePanel;

    //delegate load ui function.
    public Func<string, Object> delegateSyncLoadUI = null;
    public Action<string, Action<Object>> delegateAsyncLoadUI = null;

    public XPage(string pageName, string loadPath)
    {
        m_pageName = pageName;
        m_loadPath = loadPath;
    }

    public void Awake()
    {
        m_luaPageCtrl = m_pageName + "Ctrl";
        m_luaPagePanel = m_pageName + "Panel";
        //Debug.LogError("call lua awake :(" + m_pageName + "Ctrl)");
        Util.CallMethod(m_luaPageCtrl, "Awake", this);

        //设置type和mode
        //m_pageType = EPageType.PopUp;
        //m_pageMode = EPageMode.HideOtherAndNeedBack;
    }

    public void Start()
    {
        m_currState = EPageState.OPEN;
        m_pageInst.gameObject.SetActive(true);
        AnchorUIGameObject();
        //Debug.LogError("call lua start :(" + m_pageName + "Ctrl)");
        Util.CallMethod(m_luaPagePanel, "Start", this.m_pageInst);
        Util.CallMethod(m_luaPageCtrl, "Start");
    }

    public void Rest()
    {
        m_currState = EPageState.OPEN;
        m_pageInst.gameObject.SetActive(true);
        //Debug.LogError("call lua rest :(" + m_pageName + "Ctrl)");
        Util.CallMethod(m_luaPageCtrl, "Rest");
    }

    public void Hide()
    {
        m_currState = EPageState.HIDE;
        m_pageInst.gameObject.SetActive(false);
        //Debug.LogError("call lua hide :(" + m_pageName + "Ctrl)");
        Util.CallMethod(m_luaPageCtrl, "Hide");
    }

    public void Destroy()
    {
        m_currState = EPageState.CLOSE;
        GameObject.Destroy(m_pageInst);
        //Debug.LogError("call lua destroy :(" + m_pageName + "Ctrl)");
        Util.CallMethod(m_luaPageCtrl, "Destroy");
    }

    public void LoadSync(Action<GameObject> callback)
    {
        if (this.m_pageInst == null && string.IsNullOrEmpty(m_loadPath) == false)
        {
            GameObject go = null;
            if (delegateSyncLoadUI != null)
            {
                Object o = delegateSyncLoadUI(m_loadPath);
                go = o != null ? GameObject.Instantiate(o) as GameObject : null;
            }
            else
            {
                go = GameObject.Instantiate(Resources.Load(m_loadPath)) as GameObject;
            }

            if (go == null)
            {
                Debug.LogError("[UI] Cant sync load your ui prefab.");
                return;
            }

            m_pageInst = go;
            m_pageTrans = go.transform;

            if (callback != null)
                callback(go);
        }
        else
        {
            if (callback != null)
                callback(m_pageInst);
        }
    }

    public void LoadAsync(Action<GameObject> callback)
    {
        XPageRoot.Instance.StartCoroutine(AsyncShow(callback));
    }

    IEnumerator AsyncShow(Action<GameObject> callback)
    {
        if (this.m_pageInst == null && string.IsNullOrEmpty(m_loadPath) == false)
        {
            GameObject go = null;
            bool _loading = true;
            delegateAsyncLoadUI(m_loadPath, (o) =>
            {
                go = o != null ? GameObject.Instantiate(o) as GameObject : null;

                _loading = false;

                m_pageInst = go;
                m_pageTrans = go.transform;

                if (callback != null)
                    callback(go);
            });

            float _t0 = Time.realtimeSinceStartup;
            while (_loading)
            {
                if (Time.realtimeSinceStartup - _t0 >= 10.0f)
                {
                    Debug.LogError("[UI] WTF async load your ui prefab timeout!");
                    yield break;
                }
                yield return null;
            }
        }
        else
        {
            if (callback != null)
                callback(m_pageInst);
        }
    }

    public void LoadAsync(ResourceManager ResManager, Action<GameObject> callback)
    {
        string assetName = m_luaPagePanel;
        string abName = m_pageName.ToLower() + AppConst.ExtName;
        ResManager.LoadPrefab(abName, assetName, delegate (UnityEngine.Object[] objs)
        {
            if (objs.Length == 0) return;
            GameObject prefab = objs[0] as GameObject;
            if (prefab == null) return;

           GameObject go = prefab != null ? GameObject.Instantiate(prefab) as GameObject : null;
            m_pageInst = go;
            m_pageTrans = go.transform;
            if (callback != null)
                callback(go);
            Debug.LogWarning("CreatePanel::>> " + m_pageName + " " + prefab);
        });

    }


    protected void AnchorUIGameObject()
    {
        if (XPageRoot.Instance == null || m_pageInst == null)
            return;

        GameObject ui = m_pageInst;

        //check if this is ugui or (ngui)?
        Vector3 anchorPos = Vector3.zero;
        Vector2 sizeDel = Vector2.zero;
        Vector3 scale = Vector3.one;
        if (ui.GetComponent<RectTransform>() != null)
        {
            anchorPos = ui.GetComponent<RectTransform>().anchoredPosition;
            sizeDel = ui.GetComponent<RectTransform>().sizeDelta;
            scale = ui.GetComponent<RectTransform>().localScale;
        }
        else
        {
            anchorPos = ui.transform.localPosition;
            scale = ui.transform.localScale;
        }

        EPagePosType type = this.m_pageType;
        if (type == EPagePosType.Normal)
        {
            ui.transform.SetParent(XPageRoot.Instance.normalRoot);
        }
        else if (type == EPagePosType.PopUp)
        {
            ui.transform.SetParent(XPageRoot.Instance.popupRoot);
        }
        else if (type == EPagePosType.Fixed)
        {
            ui.transform.SetParent(XPageRoot.Instance.fixedRoot);
        }
        else if (type == EPagePosType.Toppest)
        {
            ui.transform.SetParent(XPageRoot.Instance.ToppestRoot);
        }

        if (ui.GetComponent<RectTransform>() != null)
        {
            ui.GetComponent<RectTransform>().anchoredPosition = anchorPos;
            ui.GetComponent<RectTransform>().sizeDelta = sizeDel;
            ui.GetComponent<RectTransform>().localScale = scale;
        }
        else
        {
            ui.transform.localPosition = anchorPos;
            ui.transform.localScale = scale;
        }
    }
}