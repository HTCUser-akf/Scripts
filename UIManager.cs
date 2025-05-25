using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class UIManager
{
    private static UIManager _instance;
    private Transform uiRoot;
    public Dictionary<string, string> pathDict;
    public Dictionary<string, GameObject> prefabDict;
    public Dictionary<string, BasePanel> panelDict;

    private UIManager()
    {
        InitDicts();
    }

    public static UIManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = new UIManager();
            }
            return _instance;
        }
    }

    public Transform UIRoot
    {
        get
        {
            if (uiRoot == null)
            {
                uiRoot = GameObject.Find("Canvas").transform;
            }
            return uiRoot;
        }
    }

    private void InitDicts()
    {
        panelDict = new Dictionary<string, BasePanel>();
        prefabDict = new Dictionary<string, GameObject>();
        pathDict = new Dictionary<string, string>()
        {
            { UIConst.StartPanel, "/Menu/StartPanel" },
            { UIConst.MainPanel, "/Menu/MainPanel" },
            { UIConst.SettingPanel, "/Menu/SettingPanel" },
        };
    }

    public BasePanel OpenPanel(string name)
    {
        BasePanel panel = null;
        if (panelDict.TryGetValue(name, out panel))
        {
            Debug.Log("界面已打开" + name);
            return null;
        }
        string path = "";
        if (!pathDict.TryGetValue(name, out path))
        {
            Debug.Log("没有这个界面" + name);
            return null;
        }
        GameObject panelPrefab = null;
        if (!prefabDict.TryGetValue(name, out panelPrefab))
        {
            string realPath = "Prefab/Panel" + path;
            panelPrefab = Resources.Load<GameObject>(realPath);
            prefabDict.Add(name, panelPrefab);
            Debug.Log("加载" + realPath + "预设体");
        }
        GameObject panelObject = GameObject.Instantiate(panelPrefab, UIRoot, false);
        panel = panelObject.GetComponent<BasePanel>();
        panelDict.Add(name, panel);
        return panel;
    }

    public BasePanel ClosePanel(string name)
    {
        BasePanel panel = null;
        if (!panelDict.TryGetValue(name, out panel))
        {
            Debug.Log("未打开" + name);
            return null;
        }
        return panel;
    }


}
public class UIConst
{
    public const string StartPanel = "StartPanel";
    public const string MainPanel = "MainPanel";
    public const string SettingPanel = "SettingPanel";
}