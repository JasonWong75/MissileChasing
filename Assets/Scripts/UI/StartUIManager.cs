using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartUIManager : MonoBehaviour {
    private GameObject m_StartPanel;
    private GameObject m_SettingPanel;
    private GameObject m_ShopPanel;

    private GameObject settingButton;
    private GameObject settingpanelexitButton;
    private GameObject shopPanelExitButton;
    private GameObject playButton;
    private GameObject shopButton;

    private GameObject soundIconOn;
    private GameObject soundIconOff;

    private ShopModuleManager m_ShopModuleManager;
    private SoundControl m_SoundControl;

    private UILabel m_ScoreNumber;

    private bool isSettingOpen = false;

	void Start () {
        m_StartPanel = GameObject.Find("StartPanel");
        m_SettingPanel = GameObject.Find("SettingPanel");
        m_ShopPanel = GameObject.Find("ShopPanel");

        settingButton = GameObject.Find("SettingButton");
        settingpanelexitButton = GameObject.Find("exit");
        shopPanelExitButton = GameObject.Find("exit1");
        playButton = GameObject.Find("taptoplay");
        shopButton = GameObject.Find("ShopButton");

        soundIconOff = soundIconOn = GameObject.Find("UI Root/StartPanel/AudioOff");
        soundIconOn = GameObject.Find("UI Root/StartPanel/AudioOn");

        m_ShopModuleManager = GameObject.Find("Shop").GetComponent<ShopModuleManager>();
        m_SoundControl = GameObject.Find("Main Camera").GetComponent<SoundControl>();

        m_SettingPanel.SetActive(false);
        m_ShopPanel.SetActive(false);

        m_ScoreNumber = GameObject.Find("scorenumber").GetComponent<UILabel>();
       // soundIconOn.SetActive(true);
        //soundIconOff.SetActive(false);
       // m_SoundControl.PlayerPrefebSetSound( PlayerPrefs.GetInt("Music", 0));


        UIEventListener.Get(settingButton).onClick = SettingButtonClick;
        UIEventListener.Get(settingpanelexitButton).onClick = SettingPanelExitButtonClick;
        UIEventListener.Get(playButton).onClick = PlayButtonClick;
        UIEventListener.Get(shopButton).onClick = ShopButtonClick;
        UIEventListener.Get(shopPanelExitButton).onClick = shopPanelExitButtononClick;
        
    }

	void Update () {
		
	}

    /// <summary>
    /// enter setting panel
    /// </summary>
    /// <param name="go"></param>
    private void SettingButtonClick(GameObject go)
    {
        if (m_SettingPanel.activeSelf == false)  //让只执行一次
        {
            m_SettingPanel.SetActive(true);
            m_ShopPanel.SetActive(false);
        }
    }

    /// <summary>
    /// exit setting panel
    /// </summary>
    /// <param name="go"></param>
    private void SettingPanelExitButtonClick(GameObject go)
    {
        if (m_SettingPanel.activeSelf == true)
        {
            m_SettingPanel.SetActive(false);
        }
    }

    /// <summary>
    /// enter gameUI
    /// </summary>
    /// <param name="go"></param>
    private void PlayButtonClick(GameObject go)
    {
        SceneManager.LoadScene("GameUI");
    }

    /// <summary>
    /// enter shop panel
    /// </summary>
    /// <param name="go"></param>
    private void ShopButtonClick(GameObject go)
    {
        if (m_ShopPanel.activeSelf == false)
        {            
            m_ShopPanel.SetActive(true);
            m_SettingPanel.SetActive(false);
        }
    }

    /// <summary>
    /// exit shop panel
    /// </summary>
    /// <param name="go"></param>
    private void shopPanelExitButtononClick(GameObject go)
    {
        if (m_ShopPanel.activeSelf == true)
        {
            m_ShopPanel.SetActive(false);
        }
    }

    public void ShopPanelCloure()
    {
        m_ShopPanel.SetActive(false);
    }

}

