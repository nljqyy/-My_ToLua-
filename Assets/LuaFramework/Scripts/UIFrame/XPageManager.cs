using UnityEngine;
using System.Collections.Generic;
using System;
using LuaFramework;

public sealed class XPageManager : Manager
{
    public XPage currShowXPage;//当前正打开的界面
    public Stack<XPage> m_pageNeedBackPool = new Stack<XPage>();//需要返回的页面的池子，栈，先进后出
    public Dictionary<string, XPage> m_pageDic = new Dictionary<string, XPage>();//所有的页面


    public int GetNeedBackCount()
    {
        return m_pageNeedBackPool.Count;
    }

    /// <summary>
    /// 获取所有面板
    /// </summary>
    /// <returns></returns>
    private List<XPage> GetAllPages()
    {
        return new List<XPage>(m_pageDic.Values);
    }

    /// <summary>
    /// 检查面板打开类型
    /// </summary>
    /// <param name="currXPage"></param>
    private void CheckPageMode(XPage currXPage)
    {
        if (currXPage.m_pageMode == EPageHideMode.DoNothing)
        {

        }
        else if (currXPage.m_pageMode == EPageHideMode.HideOtherOnly)
        {
            HideOtherPages(currXPage);
        }
        else if (currXPage.m_pageMode == EPageHideMode.HideOtherAndNeedBack)
        {
            HideOtherPages(currXPage);
            m_pageNeedBackPool.Push(currXPage);
        }
    }

    private void HideOtherPages(XPage currXPage)
    {
        List<XPage> xpages = GetAllPages();
        int count = xpages.Count;
        for (int i = 0; i < count; i++)
        {
            XPage curr = xpages[i];
            if (curr.Equals(currXPage))
                continue;
            if (curr.m_currState == EPageState.OPEN && curr.m_pageType != EPagePosType.Fixed && curr.m_pageType != EPagePosType.Normal)
            {
                curr.Hide();
            }
        }
    }

    /// <summary>
    /// 检测面板是否在队列里
    /// </summary>
    /// <param name="pageName"></param>
    /// <returns></returns>
    private bool CheckPageExist(string pageName)
    {
        if (m_pageDic.ContainsKey(pageName))
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    /// <summary>
    /// 用相对路径获取面板名称
    /// </summary>
    /// <param name="pageLoadPath"></param>
    /// <returns></returns>
    private string GetPageName(string pageLoadPath)
    {
        string pageName = pageLoadPath.Substring(pageLoadPath.LastIndexOf("/") + 1);
        return pageName;
    }

    /// <summary>
    /// 打开面板
    /// </summary>
    /// <param name="isSync">是否同步加载</param>
    /// <param name="pageLoadPath">加载的相对路径</param>
    public void ShowPage(string pageName)
    {
        if (string.IsNullOrEmpty(pageName))
        {
            Debug.LogError("pageName--是null");
            return;
        }  
        bool isExist = CheckPageExist(pageName);
        XPage currXPage = null;
        if (isExist)
        {
            currXPage = m_pageDic[pageName];
            if (currXPage.m_currState == EPageState.HIDE)
            {
                CheckPageMode(currXPage);
                currXPage.Rest();
                currShowXPage = currXPage;
            }
        }
        else
        {
            //add
            currXPage = new XPage(pageName, "");
            currXPage.Awake();
            currXPage.LoadAsync(ResManager, o=>
            {
                m_pageDic.Add(pageName, currXPage);
                currShowXPage = currXPage;
                CheckPageMode(currXPage);
                currXPage.Start();
                Debug.LogWarning("CreatePanel::>> " + name + " " + o);
            });
        }
    }

    #region  原逻辑
    public void ShowPage(bool isSync, string pageLoadPath)
    {
        string pageName = GetPageName(pageLoadPath);
        bool isExist = CheckPageExist(pageName);
        XPage currXPage = null;
        if (isExist)
        {
            currXPage = m_pageDic[pageName];
            if (currXPage.m_currState == EPageState.HIDE)
            {
                CheckPageMode(currXPage);
                currXPage.Rest();
                currShowXPage = currXPage;
            }
        }
        else
        {
            //add
            currXPage = new XPage(pageName, pageLoadPath);
            currXPage.Awake();
            XPageLoadBind.Bind(currXPage);
            if (isSync)
            {
                currXPage.LoadSync((go) =>
                 {
                     m_pageDic.Add(pageName, currXPage);
                     currShowXPage = currXPage;
                     CheckPageMode(currXPage);
                     currXPage.Start();

                 });
            }
            else
            {
                currXPage.LoadAsync((go) =>
                {
                    m_pageDic.Add(pageName, currXPage);
                    currShowXPage = currXPage;
                    CheckPageMode(currXPage);
                    currXPage.Start();
                });
            }
        }
    }
    #endregion 



    /// <summary>
    /// 隐藏当前的页面
    /// </summary>
    public bool HideCurrPage()
    {
        if (currShowXPage != null)
        {
            if (currShowXPage.m_pageMode == EPageHideMode.HideOtherAndNeedBack)
            {
                if (m_pageNeedBackPool.Count > 0)
                {
                    if (m_pageNeedBackPool.Peek().Equals(currShowXPage))
                    {
                        XPage topPage = m_pageNeedBackPool.Pop();
                        topPage.Hide();
                        currShowXPage = null;

                        if (m_pageNeedBackPool.Count > 0)
                        {
                            XPage _curr = m_pageNeedBackPool.Peek();
                            _curr.Rest();
                            currShowXPage = _curr;
                        }
                    }
                }
            }
            else
            {
                if (currShowXPage.m_currState == EPageState.OPEN)
                {
                    currShowXPage.Hide();
                    currShowXPage = null;
                }
            }

            return true;
        }
        else
        {
            Debug.Log("currShowPage is null");
            return false;
        }
    }

    /// <summary>
    ///隐藏指定面板 
    /// </summary>
    /// <param name="pageName">Page name.</param>
    public void HidePage(string pageName)
    {
        bool isExist = CheckPageExist(pageName);
        if (isExist)
        {
            XPage _currXpage = m_pageDic[pageName];
            if (_currXpage.m_currState == EPageState.OPEN)
                _currXpage.Hide();
        }

    }

    /// <summary>
    /// 销毁所有面板
    /// </summary>
    public void CloseAllPages()
    {
        List<XPage> allPages = GetAllPages();
        int count = allPages.Count;
        for (int i = 0; i < count; i++)
        {
            allPages[i].Destroy();
            allPages[i] = null;
        }
        m_pageDic.Clear();
        m_pageNeedBackPool.Clear();
    }

    /// <summary>
    /// 销毁
    /// </summary>
    public void Destroy()
    {
        CloseAllPages();
        currShowXPage = null;
        m_pageDic = null;
        m_pageNeedBackPool = null;
        Debug.Log("~XPageMgr was destroy");
    }
}