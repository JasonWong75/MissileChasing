using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

/// <summary>
/// Alpha Shop Manager, VERY IMPORTANT!
/// </summary>
public class ShopModuleManager : MonoBehaviour {

    //private string XMLPath = "Assets/Datas/ShopData.xml"; //用于读取飞机价格速度的那个xml
    //private string savePath = "Assets/Datas/SaveData.xml"; //用于存储玩家金币最高分的那个xml
    private string XMLPath;
    private string savePath;
    private string content = "<SaveData><GoldCount>0</GoldCount><HeightScore>0</HeightScore><ID0>1</ID0><ID1>0</ID1><ID2>0</ID2><ID3>0</ID3></SaveData>";

    public ShopData m_ShopData;
    private StartUIManager m_StartUIManager;

    private GameObject ShopItemUI;
    private GameObject m_choosenShip;

    private LeftAndRightButtonControl m_LRBC;

    private UILabel goldCount;
    private UILabel highestScore;
    

    public int index; //要展现的物品的索引, 在这里设置数字不可用，得在start（）设置才行！

    public List<GameObject> UIList = new List<GameObject>();  /// a list that store all Child shop UI

	void Start () {
        savePath = Application.persistentDataPath + "/SaveData.xml";
        XMLPath = Resources.Load("ShopData").ToString(); //存储了一堆文档
        if (!File.Exists(savePath))
        {
            File.WriteAllText(savePath, content);
        }


        index = 12; //在这里初始化才有用   

        m_choosenShip = GameObject.Find("StartPanel/ChoosenShip");
        m_StartUIManager = GameObject.Find("UI Root").GetComponent<StartUIManager>();

        m_ShopData = new ShopData(); //Initialize Shop object
        
        m_ShopData.ShopDataTraverse(XMLPath); //Load XML file by Path (Step4) 得到了一个装着XML的List
        m_ShopData.ReadScoreAndGold(savePath); //Load XML file(存储最高分的那个) by path
        Debug.Log(m_ShopData.goldCount + "  &  " + m_ShopData.highestScore);

        for (int i = 0; i < m_ShopData.shopState.Count; i++)
        {
            Debug.Log(m_ShopData.shopState[i]);
        }

        ShopItemUI = Resources.Load<GameObject>("UI/ShopItem");
        m_LRBC = gameObject.GetComponent<LeftAndRightButtonControl>();
        
        goldCount = GameObject.Find("StartPanel/Star/starnumber").GetComponent<UILabel>();
        highestScore = GameObject.Find("StartPanel/scorenumber").GetComponent<UILabel>();

        UpdateUI();
              
        //read score from gameUI from playerprefs
        int currentHightestScore = PlayerPrefs.GetInt("Score", 0);
        if (currentHightestScore > m_ShopData.highestScore)
        {
            //更新UI
            highestScore.text = currentHightestScore.ToString();
            //更新XML，存储新的最高分
            m_ShopData.UpdateXMLData(savePath, "HeightScore", currentHightestScore.ToString());
        }

        int currentGold = PlayerPrefs.GetInt("Gold", 0);
        if (currentGold > 0)
        {
            //更新UI
            int newGoldCount = int.Parse(goldCount.text) + currentGold;
            goldCount.text = newGoldCount.ToString();
            //更新XML，存储新的金币数
            m_ShopData.UpdateXMLData(savePath,"GoldCount", goldCount.text );
            //更新完后，把playprefe的gold数据设为0，避免以后每次打开startui都会加上一次
            PlayerPrefs.SetInt("Gold", 0);
        }



        //SetPlayerInfo(m_ShopData.XMLItemList[index].Model);

        CreateAllShopUI();
        HideAndShow(index);
    }

    private void UpdateUI()
    {
        goldCount.text = m_ShopData.goldCount.ToString(); //把XML（最高分那个）数据同步到UI上
        highestScore.text = m_ShopData.highestScore.ToString();
    }

    /// <summary>
    /// Create ShopItem UI by using ShopItemUI
    /// </summary>
    private void CreateAllShopUI()
    {
        //创建几个UI？由List里有几个元素决定，因为一个元素就是一个XML里的<Item>
        for (int i = 0; i < m_ShopData.XMLItemList.Count; i++)
        {
            //实例化单个商品UI
            GameObject item = NGUITools.AddChild(gameObject, ShopItemUI);   //通过NGUI的工具类给当前gameObject下面加了四个ShopItem预制体UI，但因为都是同一个预制体因此他们一模一样

            //通过list 里的路径加载对应的model gameobject
            GameObject shopPlaneModel = Resources.Load<GameObject>( m_ShopData.XMLItemList[i].Model);

            item.GetComponent<ShopItemUI>().SetUIValue(i, m_ShopData.XMLItemList[i].Speed, m_ShopData.XMLItemList[i].Rotate, m_ShopData.XMLItemList[i].Price, shopPlaneModel, m_ShopData.shopState[i], m_ShopData.XMLItemList[i].Id, m_ShopData.XMLItemList[i].Model); //让他们不在一样， 这一步的时候Shop Item UI有可能还在执行start()，因此要切换成Awake（）

            UIList.Add(item); // put the generated shopUI into list        
        }
        
    }

    /// <summary>
    /// 显示和隐藏商店界面
    /// </summary>
    /// <param name="i"></param>

    public void HideAndShow(int i)
    {
        Debug.Log("now UI order: "+ i);
        //Hide all of the shopUI
        for (int j = 0; j < UIList.Count; j++)
        {
            if (j != i%4)
            {
                UIList[j].SetActive(false);
            }
            else
            {
                UIList[j].SetActive(true);
            }
        }
    }

    /// <summary>
    /// 计算商品价格
    /// </summary>
    /// <param name="item">当点击购买按钮时，信息会传递到这里</param>
    private void PriceCalculation(ShopItemUI item)
    {
        //if(xml里显示的金币数>=按钮上显示的金币数)
        if (m_ShopData.goldCount >= int.Parse(item.buyButton.GetComponent<Transform>().Find("Price").gameObject.GetComponent<UILabel>().text))
        {
            Debug.Log("purchase successful");
            item.BuyEnd(); // 隐藏购买按钮
            m_ShopData.goldCount -= int.Parse(item.buyButton.GetComponent<Transform>().Find("Price").gameObject.GetComponent<UILabel>().text); //减去消耗的金币
            UpdateUI(); //更新UI上显示的金币数
            m_ShopData.UpdateXMLData(savePath, "GoldCount", m_ShopData.goldCount.ToString()); //这里->UpdateXMLData()->更新XML文档， 这里是付款后存档金钱的数额
            m_ShopData.UpdateXMLData(savePath, "ID" + item.id, "1");  //这里->UpdateXMLData()->更新XML文档， 这里是在付款后把商品标号改为1从而隐藏buybutton
        }
        else
        {
            Debug.Log("unsuccessful");
        }
    }

    /// <summary>
    /// 将选中的飞机放到开始界面(如果有一个代码问题，自己无论如何想不明白，拿整个代码给别人看别人未必会愿意，该怎么办？)
    /// </summary>
    /// <param name="ship"></param>
    public void UpdateStartPanelShip(GameObject ship)
    {
        GameObject startPanelAddShipModel = NGUITools.AddChild(m_choosenShip, ship);
    }   

    /// <summary>
    /// 关闭商店选择面板
    /// </summary>
    private void CloseShopPanel()
    {
        gameObject.SetActive(false);
    }

    /// <summary>
    /// 存储当前角色信息到playerprefs中
    /// </summary>
    public void SetPlayerInfo(string name, int speed, int rotate)
    {
        PlayerPrefs.SetString("PlayerName", name);
        PlayerPrefs.SetInt("Speed", speed);
        PlayerPrefs.SetInt("Rotate",rotate);
    }
}
