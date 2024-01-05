using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Reward : MonoBehaviour {

    private Transform m_Transform;

	// Use this for initialization
	void Start () {
        m_Transform = gameObject.GetComponent<Transform>();
	}
	
	// Update is called once per frame
	void Update () {
        m_Transform.Rotate(Vector3.left+Vector3.up, Space.World);
	}

    void OnDestroy()
    {
        Debug.Log("I dead");
        SendMessageUpwards("RewardCountDown");
    }
}
