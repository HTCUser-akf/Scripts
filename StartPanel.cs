using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartPanel : BasePanel
{
    public void OnEnterGame()
    {
        UIManager.Instance.OpenPanel(UIConst.MainPanel);
    }
}
