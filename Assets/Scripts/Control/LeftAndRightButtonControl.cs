using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeftAndRightButtonControl : MonoBehaviour {

    private GameObject leftButton;
    private GameObject rightButton;

    private ShopModuleManager m_ShopModuleManager;
    private ShopData ShopData;
	void Start () {
        leftButton = GameObject.Find("LeftArrow");
        rightButton = GameObject.Find("RightArrow");

        m_ShopModuleManager = gameObject.GetComponent<ShopModuleManager>();
        ShopData = new ShopData();

        UIEventListener.Get(leftButton).onClick = LeftButtonClick;
        UIEventListener.Get(rightButton).onClick = RightButtonClick;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    /// <summary>
    /// when click left button, switch shopUI
    /// </summary>
    public void LeftButtonClick(GameObject go)
    { 
        if (m_ShopModuleManager.index > 0)
        {
            m_ShopModuleManager.index--;
            m_ShopModuleManager.HideAndShow(m_ShopModuleManager.index);
            //m_ShopModuleManager.SetPlayerInfo(ShopData.XMLItemList[m_ShopModuleManager.index].Model); //到目前为止共三个list，shopdata俩ShopmoduleManager一个，但它们都是对应的
        }
        
    }

    public void RightButtonClick(GameObject go)
    {
        m_ShopModuleManager.index++;
        m_ShopModuleManager.HideAndShow(m_ShopModuleManager.index);
        //m_ShopModuleManager.SetPlayerInfo(ShopData.XMLItemList[m_ShopModuleManager.index].Model);
    }

    
}
