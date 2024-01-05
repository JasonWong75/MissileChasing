using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 商城Item控制器，set price and so on
/// </summary>
public class ShopItemUI : MonoBehaviour {

    private Transform m_Transform;

    private UILabel ui_Speed;
    private UILabel ui_Rotate;
    private UILabel ui_Price;
    public int id; //商品的编号
    private int isTheFourthOne;
    private string modelName;
    private GameObject modelParent;
    public GameObject buyButton;
    public GameObject chooseButton;
    private GameObject model;
    private GameObject ship;
    private StartUIManager m_StartUIManager;
    private ShopModuleManager m_ShopModuleManager;
    private ShopData ShopData;

	void Awake () {  //因为awake（）先于start（）执行，由此保证下面四行能在shopmodulemanager调用前执行完，避免bug
        m_Transform = gameObject.GetComponent<Transform>();

        ui_Speed = m_Transform.Find("Speed/SpeedText").GetComponent<UILabel>();
        ui_Rotate = m_Transform.Find("Rotate/RotateText").GetComponent<UILabel>();
        ui_Price = m_Transform.Find("Buy/bg/Price").GetComponent<UILabel>();
        modelParent = m_Transform.Find("Model").gameObject;
        buyButton = m_Transform.Find("Buy/bg").gameObject;
        chooseButton = m_Transform.Find("Buy/choose").gameObject;
        chooseButton.SetActive(false);
        m_StartUIManager = GameObject.Find("UI Root").GetComponent<StartUIManager>();
        m_ShopModuleManager = GameObject.Find("Shop").GetComponent<ShopModuleManager>();
        ShopData = new ShopData();
        UIEventListener.Get(buyButton).onClick = BuyButtonClick;
        UIEventListener.Get(chooseButton).onClick = ChooseButtonClick;
   
        
    }

    /// <summary>
    /// set value to shopItem
    /// </summary>
    /// <param name="i">给第四个模型特别照顾</param>
    /// <param name="speed"></param>
    /// <param name="rotate"></param>
    /// <param name="price"></param>
    /// <param name="model"></param>
    /// <param name="state">购买状态，买了就不显示购买按钮了</param>
    /// <param name="id">shopitem里每个飞机的id</param>
    /// <param name="modelName">每个UI中的飞机模型在本来的xml文件中叫什么</param>
    public void SetUIValue(int i, string speed, string rotate, string price, GameObject model, int state, string id,string modelName)
    {
        //Assign value to UI value
        ui_Speed.text = speed;
        ui_Rotate.text = rotate;
        ui_Price.text = price;
        isTheFourthOne = i;
        this.id = int.Parse(id);
        this.modelName = modelName;


        if (state == 1)
        {
            buyButton.SetActive(false);
            chooseButton.SetActive(true);
        }


        //Instantiate plane model. set value details
        ship = NGUITools.AddChild(modelParent, model); //在shipitem/Model下安装此model

        
        Transform ship_Transform = ship.GetComponent<Transform>();
        ship.layer = 8; // 给ship设置为NGUI层
        ship_Transform.Find("Mesh").gameObject.layer = 8; //给ship的子物体mesh也设置成NGUI层
        //设置飞机模型的缩放信息
        if (isTheFourthOne != 3)
        {
            ship_Transform.localScale = new Vector3(15, 15, 15); //大小缩放
        }
        else if (isTheFourthOne == 3)
        {
            ship_Transform.localScale = new Vector3(5, 5, 5); //因为ship_4的模型过大，因此需要特别照顾一下
        }
        ship_Transform.localPosition = new Vector3(0, -54, -174); //位置调整
        ship_Transform.localRotation = Quaternion.Euler(new Vector3(20, -179.81f, 0)); //旋转
    
    }

    


    private void BuyButtonClick(GameObject go)
    {
        Debug.Log("Buy Button");
        //this 代表当前脚本对象
        SendMessageUpwards("PriceCalculation", this);
    }

    /// <summary>
    /// purchase succuessful
    /// </summary>
    public void BuyEnd()
    {
        buyButton.SetActive(false);
        chooseButton.SetActive(true);
    }

    /// <summary>
    /// 点了按钮后把当前UI的飞机模型传到StartPanel上
    /// </summary>
    /// <param name="go"></param>
    private void ChooseButtonClick(GameObject go)
    {
        Debug.Log("ship choosen");
        //SendMessageUpwards("UpdateStartPanelShip", ship);  //把选中的飞机显示到开始面板上
        m_ShopModuleManager.SetPlayerInfo(modelName, int.Parse(ui_Speed.text), int.Parse(ui_Rotate.text)); //ShopData.XMLItemList[1].Model
        m_StartUIManager.ShopPanelCloure();
    }
}
