using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// a script for controlling missile
/// </summary>
public class Missile : MonoBehaviour {

    private Transform m_Transform;
    private Transform playTransform;

    private Vector3 missileCurrentDirection = Vector3.forward;  //导弹当前的飞行方向 （注意与update那个区分）

    private GameObject smoke2;
	// Use this for initialization
	void Start () {
        m_Transform = gameObject.GetComponent<Transform>();  //missile position
        playTransform = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();  // player position
        smoke2 = Resources.Load<GameObject>("Smoke02");
    }
	
	// Update is called once per frame
	void Update () {
        m_Transform.Translate(Vector3.forward);  //导弹需要往哪飞（向前飞）

        Vector3 dir = playTransform.position - m_Transform.position;  // Missile chasing step1: get the "perfect" vector that missile should following
        missileCurrentDirection = Vector3.Lerp(missileCurrentDirection, dir, Time.deltaTime); // Missile chasing step2: missile slowly turn to the perfect vector we got in step1 (与摄像机跟随同理)
        m_Transform.rotation = Quaternion.LookRotation(missileCurrentDirection); //Missile chasing step3: using Quaternion.LookRotation get missile rotate (到这里前两步才有意义，此时导弹向前飞并且知道自己要怎么转弯)
    }

    // Missile hit Missile.*Do: Destroy Missile it self, play explodion effect
    private void OnTriggerEnter(Collider coll)
    {
        if (coll.tag == "Missile")
        {
            GameObject.Destroy(coll.gameObject);
            GameObject.Instantiate(smoke2, m_Transform.position, Quaternion.identity);
        }
    }

    public void SelfDestructionCommand()
    {
        GameObject.Destroy(gameObject,0.1f);
        GameObject.Instantiate(smoke2, m_Transform.position, Quaternion.identity);
        
    }
}
