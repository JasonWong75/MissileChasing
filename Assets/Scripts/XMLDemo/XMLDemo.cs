using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Xml;  //import XML manipulation namespace

/// <summary>
/// XML Practice
/// </summary>
public class XMLDemo : MonoBehaviour {

    private string xmlPath = "Assets/datas/Web.xml";

	void Start () {
        ReadXMLByPath(xmlPath);
	}

    /// <summary>
    /// show xml data by reding the path
    /// </summary>
    /// <param name="path"></param>
    private void ReadXMLByPath(string path)
    {
        //Initialize a xml object
        XmlDocument file1 = new XmlDocument();
        file1.Load(xmlPath);
        XmlNode root = file1.SelectSingleNode("Web");
        XmlNodeList nodeList = root.ChildNodes;
        foreach(XmlNode node in nodeList)
        {
            Debug.Log(node.Attributes["id"].Value+"\n");
            Debug.Log(node.ChildNodes[0].InnerText + "\n");
            Debug.Log(node.ChildNodes[1].InnerText+ "\n");
        }
    }
	
}
