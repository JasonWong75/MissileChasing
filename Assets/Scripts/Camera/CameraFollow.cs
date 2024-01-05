using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour {

    private Transform m_Transform;
    private Transform playerTransform;

	// Use this for initialization
	void Start () {
        m_Transform = gameObject.GetComponent<Transform>();
        playerTransform = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
	}
	
	// Update is called once per frame
	void Update () {
        m_Transform.position = playerTransform.position + new Vector3(0, 50, 0);  // let camera above player 50 meters
	}
}
