using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 飞机管理器脚本
/// </summary>
public class ShipManager : MonoBehaviour {

    private GameObject model;
    private GameObject player;

	/// <summary>
	/// 我操为什么摄像机一直不跟随因为摄像机和这个都是start(),得将这里的优先级调高到awake()就行
	/// </summary>
	void Awake () {
        string ship = PlayerPrefs.GetString("PlayerName", "ShipUI/Ship_1");
        int speed = PlayerPrefs.GetInt("Speed",5);
        int rotate = PlayerPrefs.GetInt("Rotate",5);

        model = Resources.Load<GameObject>(ship); //根据从StartUI里传来的字符串在resources里找到对应飞机角色
        player = GameObject.Instantiate(model, Vector3.zero, Quaternion.identity) as GameObject; //将飞机模型实例化
        if (ship == "ShipUI/ship_1")
        {
            player.GetComponent<Transform>().localScale = new Vector3(2.5f,2.5f,2.5f);
        }
        else if (ship == "ShipUI/ship_2")
        {
            player.GetComponent<Transform>().localScale = new Vector3(2,2,2);
        }
        else if (ship == "ShipUI/ship_3")
        {
            player.GetComponent<Transform>().localScale = new Vector3(2.2f,2.2f,2.2f);
        }
        else if (ship == "ShipUI/ship_4")
        {
            player.GetComponent<Transform>().localScale = new Vector3(0.9f, 0.9f, 0.9f);
        }


        player.AddComponent<Rigidbody>(); //Add rigidbody component to plane
        player.GetComponent<Rigidbody>().useGravity = false; //disable plane's gravity component
        
        PlaneFly myPlane = player.AddComponent<PlaneFly>(); // Add Script component to plane
        myPlane.Rotate = rotate; // edit the value of the plane
        myPlane.Speed = speed;

        player.GetComponent<StartUIShipRotate>().enabled = false; //禁用旋转组件，不用禁用回去因为预制体里的还是没有禁用旋转组件的
        player.name = "Ship_5"; //把名字标签照原来的套好
        player.tag = "Player";
    }

}
