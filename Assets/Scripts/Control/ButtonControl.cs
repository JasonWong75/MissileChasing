using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonControl : MonoBehaviour {
    private PlaneFly m_PlaneFly;

    private GameObject left;
    private GameObject right;

	// Use this for initialization
	void Start () {
        left = GameObject.Find("Left");
        right = GameObject.Find("Right");

        m_PlaneFly = GameObject.FindGameObjectWithTag("Player").GetComponent<PlaneFly>();

        UIEventListener.Get(left).onPress = LeftButton;
        UIEventListener.Get(right).onPress = RightButton;
	}
	
	// Update is called once per frame
	void Update () {       
        
	}

    /// <summary>
    ///这个仅仅在按下和松开时各发送一条信息
    /// </summary>
    /// <param name="go"></param>
    /// <param name="isPress"></param>
    private void LeftButton(GameObject go, bool isPress)
    {
        if (isPress)
        {
            Debug.Log("left press");
            m_PlaneFly.IsLeft = true;
        }
        else
        {
            Debug.Log("left release");
            m_PlaneFly.IsLeft = false;
        }
        
    }

    private void RightButton(GameObject go, bool isPress)
    {
        if (isPress)
        {
            Debug.Log("right press");
            m_PlaneFly.IsRight = true;
        }
        else
        {
            Debug.Log("right release");
            m_PlaneFly.IsRight = false;
        }
    }
    
}
