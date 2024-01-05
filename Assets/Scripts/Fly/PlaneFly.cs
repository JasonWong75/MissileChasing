using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaneFly : MonoBehaviour {
    private Transform m_Transform;

    private bool isLeft = false;
    private bool isRight = false;

    private bool isDead = false; // able to control plane only if isDead == false

    private MissileManager m_MissileManager;
    private RewardManager m_RewardManager;
    private GameUIManager m_GameUIManager;

    private GameObject smoke3;

    public int rewardScore = 0;

    private int speed;
    private int rotate;
    public bool IsLeft
    {
        get { return isLeft; }
        set { isLeft = value; }
    }

    public bool IsRight
    {
        get { return isRight; }
        set { isRight = value; }
    }

    public int Speed
    {
        get { return speed; }
        set { speed = value; }
    }

    public int Rotate
    {
        get { return rotate; }
        set { rotate = value; }
    }
	// Use this for initialization
	void Start () {
        m_Transform = gameObject.GetComponent<Transform>();
        m_MissileManager = GameObject.Find("MissileManager").GetComponent<MissileManager>();
        m_RewardManager = GameObject.Find("RewardManager").GetComponent<RewardManager>();
        m_GameUIManager = GameObject.Find("UI Root").GetComponent<GameUIManager>();

        smoke3 = Resources.Load<GameObject>("Smoke03");
    }
	
	// Update is called once per frame
	void Update () {
        if (isDead == false)
        {
            m_Transform.Translate(Vector3.forward*speed/10);

            if (isLeft)
            {
                m_Transform.Rotate(Vector3.down*rotate/10);
            }
            if (isRight)
            {
                m_Transform.Rotate(Vector3.up*rotate/10);
            }
        }
	}

    private void OnTriggerEnter(Collider coll)
    {
        if (coll.tag == "Boarder")
        {
            GameOver();
        }
        /*if clash the missile or wall. #Do: destroy plane and inform missile manager to stop generating missile (because missile need to use plane's transform, which has been destroied), play explode effect*/
        if (coll.tag == "Missile" )
        {
            GameObject.Destroy(coll.gameObject); // destroy the missile
            GameOver();
        }
        if (coll.tag == "Reward")
        {
            GameObject.Destroy(coll.gameObject); //吃掉奖励物品
            rewardScore++;
            //m_RewardManager.RewardCount--;
            m_GameUIManager.UpdateLabel(rewardScore);
        }
     }

    private void GameOver()
    {
        isDead = true;
        GameObject.Instantiate(smoke3, m_Transform.position, Quaternion.identity);
        m_MissileManager.SelfDestroy(); // explode all the remain missiles
        gameObject.SetActive(false); //hide plane itself
        m_MissileManager.CancelInvoke(); //stop generating missile
        m_RewardManager.StopRewardGenerate(); //stop generating rewards
        m_GameUIManager.ShowOverPanelAndHideLeftRightControlButton(); //switch UI
    }
}
