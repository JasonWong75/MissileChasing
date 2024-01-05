using System.Collections;
using System.Collections.Generic;
using System.Xml;  //step1: using package

/// <summary>
/// Shop Function Module 一个读取XML文件里的价格内容，将内容整理成一个类 ，然后存储在list里可供别人调用的一个class
/// </summary>
public class ShopData {

   // private string path = "Assets/Datas/ShopData.xml"; //step2: find xml file path
    
    public List<ShopItem> XMLItemList = new List<ShopItem>();  // a list that store all info from XMLshopitem file

    public List<int> shopState = new List<int>(); //存储4个商品的购买状态


    public int goldCount = 0; //最高金币数
    public int highestScore = 0; //最高分数
    /*
	void Start () {

        XMLItemList = new List<ShopItem>();   

        ShopDataTraverse();

        ShowWhatTheListGot();

	}*/
	
    /// <summary>
    /// Test: Read XML file; Do: Store info from XML to List
    /// </summary>
    public void ShopDataTraverse(string path)
    {
        XmlDocument ShopData = new XmlDocument(); //step3: instantiate a xml file
        //ShopData.Load(path); //step4： load xml file by path
        ShopData.LoadXml(path);
        XmlNode root = ShopData.SelectSingleNode("Shop"); //step5: find xml root node
        XmlNodeList nodeList = root.ChildNodes; //step6: find node array as child of root node


        //Do: Store info from XML to List
        foreach (XmlNode node in nodeList) //step7: Traverse each node by finding its child nodes and print out the text info inside
        {           
            ShopItem m_ShopItem;
            string[] infoList = new string[5];

            for (int i = 0; i < node.ChildNodes.Count; i++)
            {
               // Debug.Log(node.ChildNodes[i].InnerText+"\n");
                infoList[i] = node.ChildNodes[i].InnerText;
            }

            m_ShopItem = new ShopItem(infoList[0], infoList[1], infoList[2], infoList[3],infoList[4]);
            XMLItemList.Add(m_ShopItem);
        }
    }

    /*
    /// <summary>
    /// to check if the List got info from XML correctly
    /// </summary>
    private void ShowWhatTheListGot()
    {
        foreach (ShopItem i in XMLItemList)
        {
            Debug.Log(i.OutputExample());
        }
    }*/

    /// <summary>
    /// read gold and hightest score from xml file  xml->ShopData
    /// </summary>
    public void ReadScoreAndGold(string path)
    {
        XmlDocument SaveData = new XmlDocument();
        SaveData.Load(path);
        XmlNode root = SaveData.SelectSingleNode("SaveData");
        XmlNodeList nodeList = root.ChildNodes;
        goldCount = int.Parse(nodeList[0].InnerText);
        highestScore = int.Parse(nodeList[1].InnerText);

        //读取商品的购买状态
        for (int i = 2; i < 6; i++)
        {
            shopState.Add(int.Parse(nodeList[i].InnerText));
        }
    }

    /// <summary>
    /// 更新XML(SaveData)文档
    /// </summary>
    /// <param name="path">xml路径</param>
    /// <param name="key">节点名称</param>
    /// <param name="value">节点里的数据</param>
    public void UpdateXMLData(string path, string key, string value)
    {
        XmlDocument UpdateData = new XmlDocument();
        UpdateData.Load(path);
        XmlNode root = UpdateData.SelectSingleNode("SaveData");
        XmlNodeList nodeList = root.ChildNodes;
        foreach (XmlNode node in nodeList)
        {
            if (node.Name == key)
            {
                node.InnerText = value;
                UpdateData.Save(path);  //整个文档存回原来路径
            }
        }
    }
}
