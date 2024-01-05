using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissileManager : MonoBehaviour {

    private Transform m_Transform;

    private Transform[] createPoint;

    private GameObject prefeb_Missile3;

	void Start () {
        m_Transform = gameObject.GetComponent<Transform>();
        createPoint = GameObject.Find("CreatePoint").GetComponent<Transform>().GetComponentsInChildren<Transform>(); //get parent Transform([0]) as well as children Transform ([1][2][3])
        /*//test above
         * foreach (Transform i in createPoint)
        {
            Debug.Log(i.gameObject.name+"\n");
        }*/

        prefeb_Missile3 = Resources.Load<GameObject>("Missile_3");

        InvokeRepeating("MissileGenerate", 3, 5);
	}
	

	void Update () {
		 
	}

    private void MissileGenerate()
    {
        int index = Random.Range(0, createPoint.Length); //随即从四个角落的某一个cube里射出导弹
        GameObject.Instantiate(prefeb_Missile3, createPoint[index].position, Quaternion.identity, m_Transform); //最后一个：实例化出的导弹的父物体（的Transform）是谁
    }

    //stop missile generating. *Related: after [PlaneFly] plane has destroied itself
    public void StopCreate()
    {
        CancelInvoke(); // Cancel all of Invoke method in the class
    }

    public void SelfDestroy()
    {
        Transform[] Missiles = m_Transform.GetComponentsInChildren<Transform>();
        foreach (Transform i in Missiles)
        {
            i.gameObject.SendMessage("SelfDestructionCommand");
        }
    }
}
